using Microsoft.AspNetCore.Mvc;

namespace UI.Controllers
{
	public class HomeController : Controller
	{
		public IActionResult Login()
		{
			return View();
		}
	}
}
