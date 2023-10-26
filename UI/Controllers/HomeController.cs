using DataLayer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TypeLayer;

namespace UI.Controllers
{
	public class HomeController : Controller
	{
		Context context = new Context();

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
			List<Students> Student = GetStudentsFromDatabase();
			List<Notes> Note = GetNotesFromDatabase();

			TeacherNotesViewModel viewModel = new TeacherNotesViewModel
			{
				Student = Student,
				Note = Note
			};

			return View("EnterNotes", viewModel);
		}

		public IActionResult EnterNotes()
		{
			return View();
		}

		//[HttpPost]
		//public IActionResult EnterNotes([Bind("NameSurname,FirstExam,SecondExam,Project")] TeacherNotesViewModel teacherNotesViewModel)
		//{
		//	if (ModelState.IsValid)
		//	{
		//		context.Add(teacherNotesViewModel);
		//		context.SaveChanges();
		//		return RedirectToAction(nameof(TeacherIndex));
		//	}
		//	return View(teacherNotesViewModel);
		//}
	}
}
