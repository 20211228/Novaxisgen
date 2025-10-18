using Microsoft.AspNetCore.Mvc;

namespace Novaxisgen.Controllers
{
    public class Reports : Controller
    {
        public IActionResult DailyEarning()
        {
            return View();
        }

        public IActionResult YourBonus()
        {
            return View();
        }

        public IActionResult BinaryReport()
        {
            return View();
        }

        public IActionResult BelowBonus()
        {
            return View();
        }

        public IActionResult LeaderBonus()
        {
            return View();
        }
    }
}
