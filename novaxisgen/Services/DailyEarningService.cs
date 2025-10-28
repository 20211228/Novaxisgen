using Microsoft.EntityFrameworkCore;
using Novaxisgen.Models;

namespace Novaxisgen.Services
{
    public class DailyEarningService
    {
        private readonly NovaxisgenContext _context;

        public DailyEarningService(NovaxisgenContext context)
        {
            _context = context;
        }

        public async Task DailyEarning()
        {
            var today = DateOnly.FromDateTime(DateTime.Now);

            var listInvest =await _context.DauTuNguoiDungs
                .Include(d => d.KyHan)
                .Where(d => d.TrangThai == "Đang hoạt động")
                .ToListAsync();

            foreach(var d in listInvest)
            {
                if(d.NgayKetThuc != null && today > d.NgayKetThuc)
                {
                    d.TrangThai = "Complete";
                    continue;
                }


                bool datinh = await _context.LaiHangNgays
                    .AnyAsync(l => l.DauTuId == d.Id && l.Ngay == today);

                if( datinh ) continue;

                decimal laiNgay = d.KyHan.LaiSuatNgay / 100m;
                decimal soTienLai = d.SoTienDauTu*laiNgay;

                _context.LaiHangNgays.Add(new Models.LaiHangNgay
                {
                    UserId = d.UserId,
                    DauTuId = d.Id,
                    Ngay = today,
                    SoTienLai = soTienLai
                });

                var vi = _context.ViNguoiDungs.FirstOrDefault(v => v.UserId == d.UserId);
                if (vi != null)
                    vi.SoDu += soTienLai;
            }

            await _context.SaveChangesAsync();
        }
    }
}
