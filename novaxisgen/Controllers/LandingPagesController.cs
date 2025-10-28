using Microsoft.AspNetCore.Mvc;

namespace Novaxisgen.Controllers
{
	public class LandingPagesController : Controller
	{
		public IActionResult LandingPage()
		{
			return View();
		}
	}
}
