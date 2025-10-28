using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Novaxisgen.Models;

namespace Novaxisgen.Controllers
{
    public class NetworksController : Controller
    {
        private readonly NovaxisgenContext _context;

        public NetworksController(NovaxisgenContext context)
        {
            _context = context;
        }

        public IActionResult DirectIndications()
        {

            var UserID = HttpContext.Session.GetString("ID");
            if (string.IsNullOrEmpty(UserID))
                return RedirectToAction("Signin", "Logins");


            // 3️⃣ Lấy danh sách node con trong cây nhị phân (giới thiệu trực tiếp)
            var danhSach = _context.CayNhiPhans
                .Where(x => x.SponsorId == long.Parse(UserID))
                .Select(x => new
                {
                    x.User.Username,
                    x.User.Email,
                    x.User.NgayTao,
                    x.PlacementNode,
                    soTien = _context.DauTuNguoiDungs
                                .Where(d => d.UserId == x.UserId)
                                .OrderBy(d => d.Id)
                                .Select(d => (decimal?)d.SoTienDauTu)
                                .FirstOrDefault()
                })
                .ToList() // chuyển sang LINQ to Objects
                .Select(x => new
                {
                    x.Username,
                    x.Email,
                    x.NgayTao,
                    x.PlacementNode,
                    Nhanh = !string.IsNullOrEmpty(x.PlacementNode)
                        ? (
                            x.PlacementNode.Split('-').LastOrDefault() == "1"
                                ? "Phải"
                                : "Trái"
                          )
                        : null, // nếu null hoặc rỗng thì để null
                    x.soTien
                })
                .ToList();

            return View(danhSach);

        }



        //cây nhị phân
        public IActionResult BinaryTree()
        {
            var userIdString = HttpContext.Session.GetString("ID");
            if (string.IsNullOrEmpty(userIdString))
            {
                return RedirectToAction("DangNhap", "NguoiDung");
            }
            var cay = BuildBinaryTree(long.Parse(userIdString));


            return View(cay);
        }


        private CayNhiPhanViewModel? BuildBinaryTree(long userId)
        {
            var root = _context.CayNhiPhans
                .Include(x => x.User)
                .FirstOrDefault(x => x.UserId == userId);

            if (root == null) return null;

            var vm = new CayNhiPhanViewModel
            {
                UserId = root.UserId,
                Username = root.User.Username,
                HoTen = root.User.HoTen,
                NhaBaoTro = root.SponsorId?.ToString()
            };

            // Tìm 2 nhánh
            var left = _context.CayNhiPhans
                .Include(x => x.User)
                .FirstOrDefault(x => x.PlacementNode == $"{userId}-0");
            var right = _context.CayNhiPhans
                .Include(x => x.User)
                .FirstOrDefault(x => x.PlacementNode == $"{userId}-1");

            vm.Trai = left != null ? BuildBinaryTree(left.UserId) : null;
            vm.Phai = right != null ? BuildBinaryTree(right.UserId) : null;

            return vm;
        }



    }
}
