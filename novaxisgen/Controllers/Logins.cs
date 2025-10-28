using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using novaxisgen.Controllers;
using Novaxisgen.Models;
using Novaxisgen.Services;

namespace Novaxisgen.Controllers
{
    public class Logins : Controller
    {
        private readonly NovaxisgenContext _context;
        

        public Logins(NovaxisgenContext context)
        {
            _context = context;
          
        }

        // load login page, 
        [HttpGet]
        public IActionResult Signin()
        {
            return View();
        }

        // xu ly dang nhap
        [HttpPost]
        public IActionResult Signin(string username, string password)
        {
            var user = _context.NguoiDungs
               .FirstOrDefault(u => u.Username == username && u.MatKhau == password);
            if (user != null)
            {
                HttpContext.Session.SetString("ID", user.Id.ToString());
                HttpContext.Session.SetString("Username", username);
                HttpContext.Session.SetString("googleAuthKey", user.GoogleAuthKey ?? "");
                HttpContext.Session.SetString("CapVIP", user.CapVip.ToString() ?? "");
                return RedirectToAction("Index", "Home");
            }
            return View();
        }


        [HttpGet]
        public IActionResult SignUp(long? sponsorId, long? placementId, string nhanh)
        {
            ViewBag.SponsorID = sponsorId;
            ViewBag.PlacementId = placementId;
            ViewBag.Nhanh = nhanh;

            if (sponsorId != null)
            {
                var cha = _context.NguoiDungs.FirstOrDefault(x => x.Id == sponsorId);
                if (cha != null)
                {
                    ViewBag.TenCha = cha.Username;
                }
            }

            return View();
        }

        
        [HttpPost]
        public async Task<IActionResult> SignUpNew(string username, string email, string matkhau, long? sponsorId, long? placementId, string nhanh)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(matkhau))
            {
                ModelState.AddModelError("", "Vui lòng nhập đầy đủ thông tin.");
                return View("SignUp");
            }

            // 🔹 Kiểm tra email tồn tại
            var existingUser = await _context.NguoiDungs
                .FirstOrDefaultAsync(u => u.Email.ToLower() == email.ToLower());
            if (existingUser != null)
            {
                ModelState.AddModelError("", "Email đã tồn tại. Vui lòng chọn email khác.");
                return View("SignUp");
            }

            // 🔹 Tạo người dùng mới
            var user = new NguoiDung
            {
                Username = username,
                Email = email,
                MatKhau = matkhau,

            };
            _context.NguoiDungs.Add(user);
            await _context.SaveChangesAsync();

            var walletService = new WalletService();
            var wallet = walletService.TaoViMoi();

            var tokens = new List<string> { "BSC-USD", "NOVAX" };
            foreach (var token in tokens)
            {
                _context.ViNguoiDungs.Add(new ViNguoiDung
                {
                    UserId = user.Id,
                    DiaChiVi = wallet.Address,
                    TokenSymbol = token,
                    SoDu = 0
                });
            }
            await _context.SaveChangesAsync();

            // 4️⃣ Xử lý cây nhị phân
            if (sponsorId == null || placementId == null)
            {
                // Người đầu tiên trong hệ thống (root)
                var rootNode = new CayNhiPhan
                {
                    UserId = user.Id,
                    SponsorId = null,
                    SponsorNode = null,
                    PlacementId = null,
                    PlacementNode = null,
                    NgayTao = DateTime.Now
                };
                _context.CayNhiPhans.Add(rootNode);
            }
            else
            {
                // 4.1️⃣ Sinh sponsorNode
                var countDownline = _context.CayNhiPhans.Count(p => p.SponsorId == sponsorId);
                string sponsorNode = $"{sponsorId}-{countDownline + 1}";

                // 4.2️⃣ Sinh placementNode
                int placementValue = nhanh.ToLower() == "left" ? 0 : 1;
                string placementNode = $"{placementId}-{placementValue}";

                // 4.3️⃣ Kiểm tra xem nhánh đã có người chưa
                bool daCoNguoi = _context.CayNhiPhans.Any(x => x.PlacementNode == placementNode);
                if (daCoNguoi)
                {
                    ModelState.AddModelError("", $"Nhánh {placementNode} của Placement ID {placementId} đã có người.");
                    return View("SignUp");
                }

                // 4.4️⃣ Lưu node mới
                var node = new CayNhiPhan
                {
                    UserId = user.Id,
                    SponsorId = sponsorId,
                    SponsorNode = sponsorNode,
                    PlacementId = placementId,
                    PlacementNode = placementNode,
                    NgayTao = DateTime.Now
                };
                _context.CayNhiPhans.Add(node);
            }

            await _context.SaveChangesAsync();

            ViewBag.ThongBao = "Đăng ký thành công!";
            return RedirectToAction("Signin", "Logins");
        }
    }
}
