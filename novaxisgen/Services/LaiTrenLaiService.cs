using Microsoft.EntityFrameworkCore;
using Novaxisgen.Models;

namespace Novaxisgen.Services
{
    public class LaiTrenLaiService
    {
        private readonly NovaxisgenContext _context;

        public LaiTrenLaiService(NovaxisgenContext context)
        {
            _context = context;
        }
        public async Task TinhHoaHongLaiTrenLaiHangNgay()
        {
            var homNay =DateOnly.FromDateTime( DateTime.Today);

            var danhSachLai = await _context.LaiHangNgays
                .Where(x => x.Ngay == homNay)
                .ToListAsync();

            foreach (var lai in danhSachLai)
            {
                var cay = await _context.CayNhiPhans
                    .FirstOrDefaultAsync(c => c.UserId == lai.UserId);
                if (cay == null) continue;

                long? sponsorId = cay.SponsorId;
                int capTang = 1;

                while (sponsorId.HasValue)
                {
                    var dauTuSponsor = await _context.DauTuNguoiDungs
                        .FirstOrDefaultAsync(d => d.UserId == sponsorId.Value);
                    if (dauTuSponsor == null) break;

                    var goiId = dauTuSponsor.GoiDauTuId;
                    var soTang = await _context.GoiDauTus
                        .Where(g => g.Id == goiId)
                        .Select(g => g.SoTangHoaHongTrucTiep)
                        .FirstOrDefaultAsync();

                    if (capTang > soTang) break;

                    var tyLe = await _context.TyLeHoaHongLaiLais
                        .Where(t => t.GoiDauTuId == goiId && t.Tang == capTang)
                        .Select(t => t.TyLe)
                        .FirstOrDefaultAsync();

                    if (tyLe > 0)
                    {
                        var hoaHong = lai.SoTienLai * (tyLe / 100);
                        _context.HoaHongLaiTrenLais.Add(new HoaHongLaiTrenLai
                        {
                            NguoiNhanId = sponsorId.Value,
                            NguoiTuyenDuoiId = lai.UserId,
                            CapTang = capTang,
                            SoTienHoaHong = hoaHong,
                            Ngay = homNay,
                            NgayTao = DateTime.Now
                        });
                    }

                    var sponsor = await _context.CayNhiPhans
                        .FirstOrDefaultAsync(c => c.UserId == sponsorId);
                    sponsorId = sponsor?.SponsorId;
                    capTang++;
                }
            }

            await _context.SaveChangesAsync();
        }

    }
}
