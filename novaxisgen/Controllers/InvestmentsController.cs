using Microsoft.AspNetCore.Mvc;

namespace Novaxisgen.Controllers
{
    public class InvestmentsController : Controller
    {
        public IActionResult InvestNow()
        {
            return View();
        }

        public IActionResult InvestHistory()
        {
            return View();
        }

        public IActionResult InvestDetails()
        {
            return View();
        }

        public IActionResult TopupWallet()
        {
            return View();
        }


        public IActionResult WithDraw()
        {
            return View();
        }
    }
}
