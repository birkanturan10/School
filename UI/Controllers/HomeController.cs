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
        StudentGradesViewModel GradesViewModel = new StudentGradesViewModel();

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

            HttpContext.Session.SetInt32("OgrenciId", user.StudentID);

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
            var ogrenciId = HttpContext.Session.GetInt32("OgrenciId");

            if (ogrenciId == null)
            {
                return RedirectToAction("Login");
            }

            GradesViewModel.NotesWithLessons = context.tbl_notes
                .Join(context.tbl_lessons,
                      note => note.LessonID,
                      lesson => lesson.LessonID,
                      (note, lesson) => new NotesWithLessonsViewModel
                      {
                          NoteID = note.NoteID,
                          StudentID = note.StudentID,
                          LessonName = lesson.LessonName,
                          FirstExam = note.FirstExam,
                          SecondExam = note.SecondExam,
                          Project = note.Project,
                          AverageNote = note.AverageNote,
                          DidItPass = note.DidItPass
                      })
                .Where(note => note.StudentID == ogrenciId)
                .ToList();

            return View(GradesViewModel);
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
				var LessonID = context.tbl_teachers.FirstOrDefault(x => x.TeacherID == GlobalDegiskenler.Id).LessonID;
                var note = notes.OrderByDescending(x => x.NoteID).FirstOrDefault(n => n.StudentID == student.StudentID && n.LessonID == LessonID);
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
                var student = context.tbl_students.FirstOrDefault(s => s.StudentID == StudentID);

                if (student != null)
                {
                    if (notes.FirstExam >= 0 && notes.SecondExam >= 0 && notes.Project >= 0)
                    {
                        notes.AverageNote = (notes.FirstExam + notes.SecondExam + notes.Project) / 3;

                        if (notes.AverageNote >= 50)
                        {
                            notes.DidItPass = "Geçti";
                        }
                        else
                        {
                            notes.DidItPass = "Kaldı";
                        }

                        int? LessonID = context.tbl_teachers.FirstOrDefault(x => x.TeacherID == GlobalDegiskenler.Id).LessonID;

                        int? count = context.tbl_notes.Count();
                        int primary = context.tbl_notes.Where(x => x.StudentID == StudentID && x.LessonID == LessonID).Count();

                        if (count > 0)
                        {
                            var existingNote = context.tbl_notes.FirstOrDefault(x => x.StudentID == StudentID && x.LessonID == LessonID);

                            if (existingNote != null)
                            {
                                // İlgili kaydı güncelle
                                existingNote.FirstExam = notes.FirstExam;
                                existingNote.SecondExam = notes.SecondExam;
                                existingNote.Project = notes.Project;
                                // Diğer alanları güncelle...

                                context.SaveChanges();
                            }
                            else
                            {
                                // Yeni bir kayıt ekle
                                notes.LessonID = LessonID;
                                notes.NoteID = primary;
                                notes.StudentID = student.StudentID;

                                context.tbl_notes.Add(notes);
                                context.SaveChanges();
                            }
                        }
                        else
                        {
                            // Yeni bir kayıt ekle
                            notes.LessonID = LessonID;
                            notes.StudentID = student.StudentID;

                            context.tbl_notes.Add(notes);
                            context.SaveChanges();
                        }

                        return RedirectToAction(nameof(TeacherIndex));
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Notlar geçersiz.");
                        return View();
                    }
                }
                else
                {
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
