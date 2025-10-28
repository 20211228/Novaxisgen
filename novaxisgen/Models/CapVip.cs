using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Novaxisgen.Models;

[Table("CapVIP")]
public partial class CapVip
{
    [Key]
    public int Id { get; set; }

    [StringLength(50)]
    public string TenCap { get; set; } = null!;

    [StringLength(200)]
    public string? MoTa { get; set; }

    [Column(TypeName = "decimal(5, 2)")]
    public decimal MucHoaHong { get; set; }

    [StringLength(200)]
    public string DieuKienDatVip { get; set; } = null!;

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? DieuKienDoanhSo { get; set; }

    public int? DieuKienCapCon { get; set; }

    [InverseProperty("CapVipNavigation")]
    public virtual ICollection<HoaHongLanhDao> HoaHongLanhDaos { get; set; } = new List<HoaHongLanhDao>();

    [InverseProperty("CapVipNavigation")]
    public virtual ICollection<NguoiDung> NguoiDungs { get; set; } = new List<NguoiDung>();
}
