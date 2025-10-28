using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Novaxisgen.Models;

namespace Novaxisgen.Controllers
{
    public class InvestmentsController : Controller
    {
        private readonly NovaxisgenContext _context;

        public InvestmentsController(NovaxisgenContext context)
        {
            _context = context;
        }

        // hien giao dien mua goi
        [HttpGet]
        public IActionResult InvestNow()
        {
            return View();
        }

        //xu ly mua goi
        [HttpPost]
        public async Task<IActionResult> SubmitInvest(decimal amount, decimal month)
        {
            try
            {
                if (amount < 10)
                    return Json(new { error = "Giá trị phải lớn hơn hoặc bằng 10." });

                var idString = HttpContext.Session.GetString("ID");
                if (string.IsNullOrEmpty(idString))
                    return Json(new { error = "Bạn chưa đăng nhập hoặc session hết hạn." });

                var userId = long.Parse(idString);
                var viNguoiDung = await _context.ViNguoiDungs
                    .FirstOrDefaultAsync(u => u.UserId == userId && u.TrangThai == "Đang hoạt động");

                if (viNguoiDung == null)
                    return Json(new { error = "Không tìm thấy ví người dùng đang hoạt động." });

                var maDonHang = $"GD-{DateTime.UtcNow:yyyyMMddHHmmss}-{Guid.NewGuid().ToString("N")[..6].ToUpper()}";

                var transition = new GiaoDichVi
                {
                    UserId = userId,
                    LoaiGiaoDich = "Nap",
                    SoTien = amount,
                    TrangThai = "Chờ xử lý",
                    MaDonHang = maDonHang,
                    DiaChiNhan = viNguoiDung.DiaChiVi,
                    NgayTao = DateTime.UtcNow
                };

                _context.GiaoDichVis.Add(transition);
                await _context.SaveChangesAsync();

                return Json(new
                {
                    madonhang = transition.MaDonHang,
                    sotien = transition.SoTien,
                    tiente = viNguoiDung.DonViTienTe ?? "BSC-USD",
                    viNhanTien = transition.DiaChiNhan
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi server: " + ex);
                return Json(new { error = "Lỗi backend: " + ex.Message });
            }
        }


        [HttpGet]
        public IActionResult GetTransactionStatus(string maDonHang)
        {
            var tx = _context.GiaoDichVis.FirstOrDefault(x => x.MaDonHang == maDonHang);
            if (tx == null)
                return Json(new { error = "Không tìm thấy giao dịch" });

            return Json(new
            {
                status = tx.TrangThai,
                amount = tx.SoTien,
                wallet = tx.DiaChiNhan,
               
            });
        }



        //load lich su giao dich
        [HttpGet]
        public IActionResult InvestHistory()
        {
            var userId =long.Parse( HttpContext.Session.GetString("ID"));
            if(userId == 0)
            {
                return RedirectToAction("Signin", "Logins");
            }

            var ListInvest = _context.GiaoDichVis
                .Where(x => x.UserId == userId)
                .OrderByDescending(x => x.NgayTao)
                .ToList();
            return View(ListInvest);
        }

        
        // load chi tiet giao dich
        public IActionResult InvestDetails(string madonhang)
        {
            var userID = long.Parse( HttpContext.Session.GetString("ID"));
            if(userID == 0)
            {
                return RedirectToAction("Signin", "Logins");
            }
            var transtion = _context.GiaoDichVis.FirstOrDefault(x => x.UserId == userID && x.MaDonHang == madonhang );

            return View(transtion);
        }

        // hien giao dien vi
        [HttpGet]
        public IActionResult TopupWallet()
        {
            var UserID = HttpContext.Session.GetString("ID");
            if (string.IsNullOrEmpty(UserID))
            {
                return RedirectToAction("Signin", "Logins");
            }
            long userID = long.Parse(UserID);
            var danhSachVi = _context.ViNguoiDungs
            .Where(v => v.UserId == userID)
            .OrderByDescending(v => v.NgayCapNhat)
            .ToList();
            return View(danhSachVi);
        }

        // Thay doi vi
        [HttpPost]
        public IActionResult ChangeWallet(ViNguoiDung inforWallet)
        {
            var UserID = HttpContext.Session.GetString("ID");
            if(string.IsNullOrEmpty(UserID) )
            {
                return RedirectToAction("Signin", "Logins");
            }
            long userID = long.Parse(UserID);
            var vicu = _context.ViNguoiDungs.FirstOrDefault(x => x.UserId == userID && x.TrangThai == "Đang hoạt động");
            if(vicu != null)
            {
                vicu.TrangThai = "Ngừng hoạt động";
                _context.SaveChanges();
            }
            var vimoi = new ViNguoiDung
            {
                UserId = userID,
                DiaChiVi = inforWallet.DiaChiVi,
                SoDu = 0,
                TrangThai = "Đang hoạt động"
            };
            _context.Add(vimoi);
            _context.SaveChanges();
            var danhSachVi = _context.ViNguoiDungs
            .Where(v => v.UserId == userID)
            .OrderByDescending(v => v.NgayCapNhat)
            .ToList();
            return View("TopupWallet",danhSachVi);
        }

        public IActionResult WithDraw()
        {
            return View();
        }
    }
}
