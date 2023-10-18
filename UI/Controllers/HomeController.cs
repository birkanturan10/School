using Microsoft.AspNetCore.Mvc;

namespace UI.Controllers
{
	public class HomeController : Controller
	{
		public IActionResult Login()
		{
			return View();
		}

		public IActionResult SignUp()
		{
			return View();
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
	}
}
