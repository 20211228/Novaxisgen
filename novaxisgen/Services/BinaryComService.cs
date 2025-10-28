using Microsoft.EntityFrameworkCore;
using Novaxisgen.Models;

namespace Novaxisgen.Services
{
    public class BinaryComService
    {
        private readonly NovaxisgenContext _context;

        public BinaryComService(NovaxisgenContext context)
        {
            _context = context;
        }


        public async Task CapNhatDoanhSoNhiPhanSauDauTu(long userId, decimal soTien)
        {
            var nodeHienTai = await _context.CayNhiPhans.FirstOrDefaultAsync(x => x.UserId == userId);
            if (nodeHienTai == null) return;

            while (nodeHienTai.PlacementId != null)
            {
                var placement = await _context.CayNhiPhans.FirstOrDefaultAsync(x => x.UserId == nodeHienTai.PlacementId);
                if (placement == null) break;

                var ds = await _context.DoanhSoNhiPhans.FirstOrDefaultAsync(x => x.UserId == placement.UserId);
                if (ds == null)
                {
                    ds = new DoanhSoNhiPhan
                    {
                        UserId = placement.UserId,
                        TongTrai = 0,
                        TongPhai = 0
                    };
                    _context.DoanhSoNhiPhans.Add(ds);
                }

                if (nodeHienTai.PlacementNode.EndsWith("0")) // trái
                    ds.TongTrai += soTien;
                else // phải
                    ds.TongPhai += soTien;

                await _context.SaveChangesAsync();

                nodeHienTai = placement;
            }
        }

        // =========================
        // 2. Tính hoa hồng nhị phân dựa trên DoanhSoNhiPhan
        // =========================
        public async Task TinhHoaHongNhiPhan()
        {
            var danhSach = await _context.DoanhSoNhiPhans.ToListAsync();

            foreach (var ds in danhSach)
            {
                decimal traiChuaTinh = (ds.TongTrai ?? 0) - (ds.DoanhSoDaTinhTrai ?? 0);
                decimal phaiChuaTinh = (ds.TongPhai ?? 0) - (ds.DoanhSoDaTinhPhai ?? 0);

                decimal doanhSoNhoHon = Math.Min(traiChuaTinh, phaiChuaTinh);

                if (doanhSoNhoHon <= 0) continue;

                // Lấy tất cả gói đầu tư của user, lấy tỷ lệ cao nhất
                var tyLeMax = await (
                                from d in _context.DauTuNguoiDungs
                                join g in _context.GoiDauTus on d.GoiDauTuId equals g.Id
                                where d.UserId == ds.UserId
                                orderby g.HoaHongNhiPhan descending
                                select g.HoaHongNhiPhan
                            ).FirstOrDefaultAsync() ?? 0;


                decimal hoaHong = doanhSoNhoHon * tyLeMax / 100;

                _context.HoaHongNhiPhans.Add(new HoaHongNhiPhan
                {
                    UserId = ds.UserId,
                    DoanhSoNhoHon = doanhSoNhoHon,
                    TyLeHoaHong = tyLeMax,
                    SoTienHoaHong = hoaHong,
                    NgayTao = DateTime.Now
                });

                var vi = await _context.ViNguoiDungs.FirstOrDefaultAsync(v => v.UserId == ds.UserId);
                if (vi != null) vi.SoDu += hoaHong;

                ds.DoanhSoDaTinhTrai += doanhSoNhoHon;
                ds.DoanhSoDaTinhPhai += doanhSoNhoHon;
                ds.CapNhatGanNhat = DateTime.Now;
            }

            await _context.SaveChangesAsync();
        }
    


        






        /*public async Task ThemNodeMoiAsync(long userId, long sponsorId, long placementId, string nhanh)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                // 1️⃣ Lấy sponsor node
                var sponsorNode = await _context.CayNhiPhans
                    .FirstOrDefaultAsync(x => x.UserId == sponsorId);

                string sponsorPath = sponsorNode != null
                    ? $"{sponsorNode.SponsorNode}_{sponsorId}"
                    : sponsorId.ToString();

                // 2️⃣ Kiểm tra nhánh trống
                var daCoNguoi = await _context.CayNhiPhans
                    .AnyAsync(x => x.PlacementId == placementId && x.PlacementNode == nhanh);

                if (daCoNguoi)
                    throw new Exception($"Vị trí {nhanh} của placement {placementId} đã có người!");

                // 3️⃣ Thêm node mới
                var node = new CayNhiPhan
                {
                    UserId = userId,
                    SponsorId = sponsorId,
                    SponsorNode = sponsorPath,
                    PlacementId = placementId,
                    PlacementNode = nhanh,
                    NgayTao = DateTime.Now
                };

                _context.CayNhiPhans.Add(node);
                await _context.SaveChangesAsync();

                await transaction.CommitAsync();
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }*/
    }
}
