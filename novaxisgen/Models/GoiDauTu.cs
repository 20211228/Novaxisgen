using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Novaxisgen.Models;

[Table("GoiDauTu")]
public partial class GoiDauTu
{
    [Key]
    public long Id { get; set; }

    [StringLength(100)]
    public string? TenGoi { get; set; }

    [StringLength(255)]
    public string? MoTa { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? SoTien { get; set; }

    public int? SoTangHoaHongTrucTiep { get; set; }

    public int? SoTangHoaHongLai { get; set; }

    [Column(TypeName = "decimal(5, 2)")]
    public decimal? HoaHongNhiPhan { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? NgayTao { get; set; }

    [InverseProperty("GoiDauTu")]
    public virtual ICollection<DauTuNguoiDung> DauTuNguoiDungs { get; set; } = new List<DauTuNguoiDung>();

    [InverseProperty("GoiDauTu")]
    public virtual ICollection<KyHanDauTu> KyHanDauTus { get; set; } = new List<KyHanDauTu>();

    [InverseProperty("GoiDauTu")]
    public virtual ICollection<TyLeHoaHongLaiLai> TyLeHoaHongLaiLais { get; set; } = new List<TyLeHoaHongLaiLai>();

    [InverseProperty("GoiDauTu")]
    public virtual ICollection<TyLeHoaHongTrucTiep> TyLeHoaHongTrucTieps { get; set; } = new List<TyLeHoaHongTrucTiep>();
}
