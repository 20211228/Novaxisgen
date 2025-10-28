using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Novaxisgen.Models;

[Table("NguoiDung")]
[Index("Username", Name = "UQ__NguoiDun__536C85E4FB24E0F0", IsUnique = true)]
[Index("Email", Name = "UQ__NguoiDun__A9D10534B0896CFD", IsUnique = true)]
public partial class NguoiDung
{
    [Key]
    public long Id { get; set; }

    [StringLength(50)]
    public string Username { get; set; } = null!;

    [StringLength(255)]
    public string? HoTen { get; set; }

    [StringLength(255)]
    public string Email { get; set; } = null!;

    [StringLength(255)]
    public string MatKhau { get; set; } = null!;

    [StringLength(255)]
    public string? GoogleAuthKey { get; set; }

    public int? CapVip { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? NgayTao { get; set; }

    [ForeignKey("CapVip")]
    [InverseProperty("NguoiDungs")]
    public virtual CapVip? CapVipNavigation { get; set; }

    [InverseProperty("Placement")]
    public virtual ICollection<CayNhiPhan> CayNhiPhanPlacements { get; set; } = new List<CayNhiPhan>();

    [InverseProperty("Sponsor")]
    public virtual ICollection<CayNhiPhan> CayNhiPhanSponsors { get; set; } = new List<CayNhiPhan>();

    [InverseProperty("User")]
    public virtual ICollection<CayNhiPhan> CayNhiPhanUsers { get; set; } = new List<CayNhiPhan>();

    [InverseProperty("User")]
    public virtual ICollection<DauTuNguoiDung> DauTuNguoiDungs { get; set; } = new List<DauTuNguoiDung>();

    [InverseProperty("User")]
    public virtual ICollection<DoanhSoNhiPhan> DoanhSoNhiPhans { get; set; } = new List<DoanhSoNhiPhan>();

    [InverseProperty("User")]
    public virtual ICollection<GiaoDichBlockchain> GiaoDichBlockchains { get; set; } = new List<GiaoDichBlockchain>();

    [InverseProperty("User")]
    public virtual ICollection<GiaoDichVi> GiaoDichVis { get; set; } = new List<GiaoDichVi>();

    [InverseProperty("NguoiNhan")]
    public virtual ICollection<HoaHongLaiTrenLai> HoaHongLaiTrenLaiNguoiNhans { get; set; } = new List<HoaHongLaiTrenLai>();

    [InverseProperty("NguoiTuyenDuoi")]
    public virtual ICollection<HoaHongLaiTrenLai> HoaHongLaiTrenLaiNguoiTuyenDuois { get; set; } = new List<HoaHongLaiTrenLai>();

    [InverseProperty("NguoiNhan")]
    public virtual ICollection<HoaHongLanhDao> HoaHongLanhDaoNguoiNhans { get; set; } = new List<HoaHongLanhDao>();

    [InverseProperty("NguoiTuyenDuoi")]
    public virtual ICollection<HoaHongLanhDao> HoaHongLanhDaoNguoiTuyenDuois { get; set; } = new List<HoaHongLanhDao>();

    [InverseProperty("User")]
    public virtual ICollection<HoaHongNhiPhan> HoaHongNhiPhans { get; set; } = new List<HoaHongNhiPhan>();

    [InverseProperty("NguoiNhan")]
    public virtual ICollection<HoaHongTrucTiep> HoaHongTrucTiepNguoiNhans { get; set; } = new List<HoaHongTrucTiep>();

    [InverseProperty("NguoiTuyenDuoi")]
    public virtual ICollection<HoaHongTrucTiep> HoaHongTrucTiepNguoiTuyenDuois { get; set; } = new List<HoaHongTrucTiep>();

    [InverseProperty("User")]
    public virtual ICollection<LaiHangNgay> LaiHangNgays { get; set; } = new List<LaiHangNgay>();

    [InverseProperty("User")]
    public virtual ICollection<ViNguoiDung> ViNguoiDungs { get; set; } = new List<ViNguoiDung>();
}
