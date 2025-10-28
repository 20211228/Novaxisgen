using Microsoft.AspNetCore.Mvc;
using Novaxisgen.Models;
using Novaxisgen.Services;

namespace Novaxisgen.Controllers
{
	public class TestController : Controller
	{
        private readonly BinaryComService _commissionService;
        private readonly VipService _vipService;
        private readonly HoaHongLanhDaoService _hoaHongLanhDaoService;

        public TestController(BinaryComService commissionService, VipService vipService,HoaHongLanhDaoService hoaHongLanhDaoService)
        {
            _commissionService = commissionService;
            _vipService = vipService;
            _hoaHongLanhDaoService = hoaHongLanhDaoService;
        }

        // Test tính hoa hồng trực tiếp
        [HttpGet]
        public async Task<IActionResult> TestDBView(long userId, decimal soTien)
        {
            await _commissionService.CapNhatDoanhSoNhiPhanSauDauTu(userId, soTien);
            await _vipService.CapNhatVipChoCayAsync(userId);
            await _commissionService.TinhHoaHongNhiPhan();
            await _hoaHongLanhDaoService.TinhHoaHongLanhDaoAsync(userId,soTien);
            ViewBag.Message = $"✅ Đã tính hoa hồng cho userId={userId} với số tiền {soTien} USDT";
            return View();
        }
    }
}
