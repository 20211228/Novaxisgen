using Microsoft.AspNetCore.Mvc;
using Novaxisgen.Models;

namespace Novaxisgen.Controllers
{
    public class InformationsController : Controller
    {
        private readonly NovaxisgenContext _context;

        public InformationsController(NovaxisgenContext context)
        {
            _context = context;
        }

        public IActionResult AccountInfor()
        {
            var UserID = HttpContext.Session.GetString("ID");
            if(string.IsNullOrEmpty(UserID))
            {
                return RedirectToAction("Signin", "Logins");
            }
            long userID = long.Parse(UserID);
            var user = _context.NguoiDungs.Find(userID);
            return View(user);
        }



        [HttpPost]
        public IActionResult AccountInfor(NguoiDung infor)
        {
            var UserID = HttpContext.Session.GetString("ID");
            if (string.IsNullOrEmpty(UserID))
            {
                return RedirectToAction("Signin", "Logins");
            }
            long userID = long.Parse(UserID);
            var user = _context.NguoiDungs.Find(userID);
            if (user == null) { return RedirectToAction("Signin", "Logins"); }

            user.HoTen = infor.HoTen;
            user.Email = infor.Email;
            _context.SaveChanges();
            ViewBag.Message = "Complete";
            return View(user);
        }


        [HttpPost]
        public IActionResult ChangePass(NguoiDung nguoiDung)
        {
            var UserID = HttpContext.Session.GetString("ID");
            if (string.IsNullOrEmpty(UserID))
            {
                return RedirectToAction("Signin", "Logins");
            }
            long userID = long.Parse(UserID);
            var user = _context.NguoiDungs.Find(userID);
            if (user == null) { return RedirectToAction("Signin", "Logins"); }

            user.MatKhau = nguoiDung.MatKhau;
            _context.SaveChanges();
            ViewBag.Message = "Complete";
            return View("AccountInfor",user);
        }
    }
}
