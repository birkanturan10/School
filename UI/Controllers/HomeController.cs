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

		public IActionResult TeacherSignUp()
		{
			return View();
		}

		public IActionResult StudentIndex()
		{
			return View();
		}

		public IActionResult TeacherIndex()
		{
			return View();
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
	}
}
