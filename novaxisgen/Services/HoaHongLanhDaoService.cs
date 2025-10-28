using Microsoft.EntityFrameworkCore;
using Novaxisgen.Models;

namespace Novaxisgen.Services
{
    public class HoaHongLanhDaoService
    {
        private readonly NovaxisgenContext _context;

        public HoaHongLanhDaoService(NovaxisgenContext context)
        {
            _context = context;
        }
        public async Task TinhHoaHongLanhDaoAsync(long nguonId, decimal soTienPhatSinh)
        {
            // Cache cây nhị phân và người dùng
            var cay = await _context.CayNhiPhans
                .AsNoTracking()
                .ToListAsync();

            var nguoiDungs = await _context.NguoiDungs
                .AsNoTracking()
                .ToListAsync();

            // Cache toàn bộ cấp VIP (để không truy vấn lại trong vòng lặp)
            var capVips = await _context.CapVips
                .AsNoTracking()
                .ToDictionaryAsync(x => x.Id, x => x.MucHoaHong);

            // Tìm người đầu tư trong cây
            var currentNode = cay.FirstOrDefault(x => x.UserId == nguonId);
            if (currentNode == null)
                return;

            decimal mucHoaHongCon = 0; // Hoa hồng VIP thấp nhất (bắt đầu từ 0%)

            // Duyệt ngược lên cây cha
            while (currentNode.PlacementId != null)
            {
                var chaNode = cay.FirstOrDefault(x => x.UserId == currentNode.PlacementId);
                if (chaNode == null)
                    break;

                var cha = nguoiDungs.FirstOrDefault(x => x.Id == chaNode.UserId);
                if (cha?.CapVip == null)
                    break;

                // Lấy phần trăm hoa hồng từ cache (CapVips)
                if (!capVips.TryGetValue(cha.CapVip.Value, out var mucHoaHongCha))
                    break;

                // Tính phần chênh lệch
                decimal chenhlech = mucHoaHongCha - mucHoaHongCon;

                if (chenhlech > 0)
                {
                    decimal soTienHoaHong = soTienPhatSinh * (chenhlech / 100m);

                    _context.HoaHongLanhDaos.Add(new HoaHongLanhDao
                    {
                        NguoiNhanId = cha.Id,
                        NguoiTuyenDuoiId = nguonId,
                        CapVip = cha.CapVip ?? 0,
                        SoTienHoaHong = soTienHoaHong,
                        NgayTao = DateTime.Now
                    });

                    // Cập nhật mức dưới để dùng cho cấp tiếp theo
                    mucHoaHongCon = mucHoaHongCha;
                }

                // Lên cha kế tiếp
                currentNode = chaNode;
            }

            await _context.SaveChangesAsync();
        }


    }
}
