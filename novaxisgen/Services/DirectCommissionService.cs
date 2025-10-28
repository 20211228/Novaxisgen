using Novaxisgen.Models;
using Microsoft.EntityFrameworkCore;
using System;

public class DirectCommissionService
{
    private readonly NovaxisgenContext _db;

    public DirectCommissionService(NovaxisgenContext db)
    {
        _db = db;
    }

    // Gọi khi userId đầu tư gói
    public async Task TinhHoaHongTrucTiepAsync(long userId, decimal soTienDauTu)
    {
        var node = await _db.CayNhiPhans.FirstOrDefaultAsync(x => x.UserId == userId);
        if (node == null || node.SponsorId == null)
            return;

        long? sponsorId = node.SponsorId;
        int level = 1;

        while (sponsorId != null && level <= 9) // tối đa 9 tầng
        {
            var sponsor = await _db.NguoiDungs.FindAsync(sponsorId);
            if (sponsor == null) break;

            // Lấy gói cao nhất của sponsor đang hoạt động
            var goiCaoNhat = await (from dt in _db.DauTuNguoiDungs
                                    join goi in _db.GoiDauTus on dt.GoiDauTuId equals goi.Id
                                    where dt.UserId == sponsor.Id && dt.TrangThai == "Đang hoạt động"
                                    orderby goi.SoTangHoaHongTrucTiep descending
                                    select goi).FirstOrDefaultAsync();

            if (goiCaoNhat == null) break;

            // Kiểm tra sponsor được nhận tối đa mấy tầng
            if (level <= goiCaoNhat.SoTangHoaHongTrucTiep)
            {
                // Lấy tỷ lệ hoa hồng theo gói sponsor và tầng
                var tyLe = await _db.TyLeHoaHongTrucTieps
                    .Where(t => t.GoiDauTuId == goiCaoNhat.Id && t.Tang == level)
                    .Select(t => t.TyLe)
                    .FirstOrDefaultAsync();

                if (tyLe > 0)
                {
                    decimal hoaHong = soTienDauTu * (tyLe / 100);

                    _db.HoaHongTrucTieps.Add(new HoaHongTrucTiep
                    {
                        NguoiNhanId = sponsor.Id,
                        NguoiTuyenDuoiId = userId,
                        CapTang = level,
                        SoTienHoaHong = hoaHong,
                        NgayTao = DateTime.Now
                    });

                    // Cộng tiền vào ví sponsor
                    var vi = await _db.ViNguoiDungs.FirstOrDefaultAsync(v => v.UserId == sponsor.Id);
                    if (vi != null)
                    {
                        vi.SoDu += hoaHong;
                        _db.ViNguoiDungs.Update(vi);
                    }
                }
            }

            // Duyệt lên tầng tiếp theo
            var sponsorNode = await _db.CayNhiPhans.FirstOrDefaultAsync(x => x.UserId == sponsor.Id);
            sponsorId = sponsorNode?.SponsorId;
            level++;
        }

        await _db.SaveChangesAsync();
    }
}
