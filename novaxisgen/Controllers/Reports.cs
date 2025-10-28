using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Novaxisgen.Models;

namespace Novaxisgen.Controllers
{
    public class Reports : Controller
    {
        private readonly NovaxisgenContext _context;

        public Reports(NovaxisgenContext context)
        {
            _context = context;
        }

        public IActionResult DailyEarning()
        {
            var userId = long.Parse(HttpContext.Session.GetString("ID")); // Lấy ID người dùng đang đăng nhập
            var laiList = _context.LaiHangNgays
                .Include(d => d.DauTu)
                .ThenInclude(d => d.GoiDauTu)
                .Where(x => x.UserId == userId)
                .OrderByDescending(x => x.Ngay)
                .ToList();

            return View(laiList);
        }

        public async Task<IActionResult> YourBonus()
        {
            var userId = long.Parse(HttpContext.Session.GetString("ID")); // Lấy ID người dùng đang đăng nhập
            var list = await _context.HoaHongTrucTieps
               .Include(x => x.NguoiTuyenDuoi)
               .Where(x => x.NguoiNhanId == userId)
               .OrderByDescending(x => x.NgayTao)
               .ToListAsync();

            decimal tonghoahongtructiep = list.Sum(x => x.SoTienHoaHong ?? 0);
            
            ViewBag.TongHoaHong = tonghoahongtructiep;
            return View(list);
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
