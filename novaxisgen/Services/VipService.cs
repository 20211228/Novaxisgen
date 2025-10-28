using Novaxisgen.Models;
using Microsoft.EntityFrameworkCore;

namespace Novaxisgen.Services
{
    public class VipService
    {
        private readonly NovaxisgenContext _context;

        public VipService(NovaxisgenContext context)
        {
            _context = context;
        }

        // Gọi khi có người đầu tư => cập nhật toàn cây cha
        public async Task CapNhatVipChoCayAsync(long userId)
        {
            var currentNode = await _context.CayNhiPhans.FirstOrDefaultAsync(x => x.UserId == userId);
            if (currentNode == null) return;

            while (currentNode.PlacementId != null)
            {
                var cha = await _context.NguoiDungs.FindAsync(currentNode.PlacementId);
                if (cha == null) break;

                await CapNhatVipAsync(cha.Id);

                // Lên tiếp cha của cha
                currentNode = await _context.CayNhiPhans.FirstOrDefaultAsync(x => x.UserId == cha.Id);
            }
        }

        // Cập nhật cấp VIP cho 1 người cụ thể
        public async Task CapNhatVipAsync(long userId)
        {
            var user = await _context.NguoiDungs.FindAsync(userId);
            if (user == null) return;

            var capVips = await _context.CapVips.OrderBy(c => c.Id).ToListAsync();

            foreach (var cap in capVips)
            {
                bool duDieuKien = await KiemTraDieuKienAsync(userId, cap);
                if (duDieuKien && (user.CapVip == null || user.CapVip < cap.Id))
                {
                    user.CapVip = cap.Id;
                    _context.NguoiDungs.Update(user);
                    await _context.SaveChangesAsync();
                }
            }
        }

        // Kiểm tra điều kiện đạt VIP
        private async Task<bool> KiemTraDieuKienAsync(long userId, CapVip cap)
        {
            var doanhSo = await _context.DoanhSoNhiPhans.FirstOrDefaultAsync(x => x.UserId == userId);
            if (doanhSo == null) return false;

            var cay = await _context.CayNhiPhans.Where(x => x.PlacementId == userId).ToListAsync();
            var trai = cay.FirstOrDefault(x => x.PlacementNode.EndsWith("-0"));
            var phai = cay.FirstOrDefault(x => x.PlacementNode.EndsWith("-1"));

            // --- Điều kiện doanh số ---
            if (cap.DieuKienDoanhSo.HasValue)
            {
                if (doanhSo.TongTrai < cap.DieuKienDoanhSo || doanhSo.TongPhai < cap.DieuKienDoanhSo)
                    return false;
            }

            // --- Điều kiện có nhánh đạt VIP con ---
            if (cap.DieuKienCapCon.HasValue)
            {
                if (trai == null || phai == null) return false;

                var userTrai = await _context.NguoiDungs.FirstOrDefaultAsync(x => x.Id == trai.UserId);
                var userPhai = await _context.NguoiDungs.FirstOrDefaultAsync(x => x.Id == phai.UserId);

                if (userTrai?.CapVip < cap.DieuKienCapCon || userPhai?.CapVip < cap.DieuKienCapCon)
                    return false;
            }

            return true;
        }
    }
}
