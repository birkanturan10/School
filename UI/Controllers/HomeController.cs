using DataLayer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TypeLayer;

namespace UI.Controllers
{
	public class HomeController : Controller
	{
		Context context = new Context();
		TeacherNotesViewModel GetTeacherNotesViewModel = new TeacherNotesViewModel();

		public IActionResult Login()
		{
			return View();
		}

        [HttpPost]
        public async Task<IActionResult> Login(string TCKimlikNo, string Password)
        {
            var user = await context.tbl_students.SingleOrDefaultAsync(u => u.TCKimlikNo == TCKimlikNo && u.Password == Password);

			

            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "Geçersiz T.C. Kimlik No veya Şifre.");
                return View();
            }

            return RedirectToAction("StudentIndex"); 
        }

        public IActionResult SignUp()
		{
			return View();
		}

		[HttpPost]
		public IActionResult SignUp(string NameSurname, string TCKimlikNo, string Password)
		{
			var newStudent = new Students
			{
				NameSurname = NameSurname,
				TCKimlikNo = TCKimlikNo,
				Password = Password
			};

			context.tbl_students.Add(newStudent);
			context.SaveChanges();

			return RedirectToAction("Login");
		}

		public IActionResult TeacherLogin()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> TeacherLogin(string TCKimlikNo, string Password)
		{
			var user = await context.tbl_teachers.SingleOrDefaultAsync(u => u.TCKimlikNo == TCKimlikNo && u.Password == Password);

			if(user == null)
			{
				ModelState.AddModelError(string.Empty, "Geçersiz T.C. Kimlik No veya Şifre.");
				return View();
			}

			GlobalDegiskenler.Id = context.tbl_teachers.SingleOrDefault(u => u.TCKimlikNo == TCKimlikNo).TeacherID;

			return RedirectToAction("TeacherIndex");
		}

		public IActionResult TeacherSignUp()
		{
            var lessons = GetLessonsFromDatabase();
            ViewBag.Lessons = lessons; // Ders verilerini ViewBag aracılığıyla taşıyın
            return View();
        }

		[HttpPost]
		public IActionResult TeacherSignUp(string NameSurname, int LessonID, string TCKimlikNo, string Password)
		{
			var newTeacher = new Teachers
			{
				NameSurname = NameSurname,
				LessonID = LessonID,
				TCKimlikNo = TCKimlikNo,
				Password = Password
			};

			context.tbl_teachers.Add(newTeacher);
			context.SaveChanges();

			return RedirectToAction("TeacherLogin");
		}

        public List<Lessons> GetLessonsFromDatabase()
        {
            using (var context = new Context()) 
            {
                var lessons = context.tbl_lessons.ToList(); 
                return lessons;
            }
        }

		public List<Students> GetStudentsFromDatabase()
		{
			using (var context = new Context())
			{
				var students = context.tbl_students.ToList();
				return students;
			}
		}

		public List<Notes> GetNotesFromDatabase()
		{
			using (var context = new Context())
			{
				var notes = context.tbl_notes.ToList();
				return notes;
			}
		}

		public IActionResult StudentIndex()
		{
			return View();
		}

		public IActionResult TeacherIndex()
		{
			return View(context.tbl_teachers);
		}

		public IActionResult AddLesson()
		{
			return View();
		}

		[HttpPost]
		public IActionResult AddLesson([Bind("LessonName")] Lessons lessons)
		{
			if (ModelState.IsValid)
			{
				context.Add(lessons);
				context.SaveChanges();
				return RedirectToAction(nameof(Login));
			}
			return View(lessons);
		}

        public IActionResult ViewYourStudents()
        {
            List<Students> students = GetStudentsFromDatabase();
            List<Notes> notes = GetNotesFromDatabase();

            var studentNotesList = new List<StudentNotes>();

            foreach (var student in students)
            {
                var note = notes.FirstOrDefault(n => n.StudentID == student.StudentID);
                if (note != null)
                {
                    var studentNotes = new StudentNotes
                    {
                        Student = student,
                        Notes = note
                    };
                    studentNotesList.Add(studentNotes);
                }
            }

            TeacherNotesViewModel viewModel = new TeacherNotesViewModel
            {
                StudentNotesList = studentNotesList
            };

            return View("ViewYourStudents", viewModel);
        }

        public IActionResult EnterNotes()
		{
			GetTeacherNotesViewModel.Student = context.tbl_students.ToList();

			return View(GetTeacherNotesViewModel);
		}

        [HttpPost]
        public IActionResult EnterNotes([Bind("FirstExam,SecondExam,Project")] Notes notes, int StudentID)
        {
            if (ModelState.IsValid)
            {
                // Öğrencinin veritabanında varlığını kontrol edin
                var student = context.tbl_students.FirstOrDefault(x => x.StudentID == StudentID);

                if (student != null)
                {
                    int? LessonID = context.tbl_teachers.FirstOrDefault(x => x.TeacherID == GlobalDegiskenler.Id).LessonID;
                    notes.LessonID = LessonID;

                    // Notu ekleyin
                    context.Add(notes);
                    context.SaveChanges();

                    // Öğrencinin not bilgilerini güncelleyin
                    student.NoteId = notes.NoteID;
                    context.Update(student);
                    context.SaveChanges();

                    return RedirectToAction(nameof(TeacherIndex));
                }
                else
                {
                    // Öğrenci bulunamadı, hata işleme kodu
                    ModelState.AddModelError(string.Empty, "Öğrenci bulunamadı.");
                    return View();
                }
            }
            return View();
        }

        public class GlobalDegiskenler
		{
			public static int Id { get; set; }
		}
	}
}
