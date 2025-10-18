using Microsoft.AspNetCore.Mvc;

namespace Novaxisgen.Controllers
{
    public class Logins : Controller
    {
        public IActionResult Signin()
        {
            return View();
        }
        public IActionResult SignUp()
        {
            return View();
        }
    }
}
