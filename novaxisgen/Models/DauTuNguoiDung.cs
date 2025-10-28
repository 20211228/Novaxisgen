using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Novaxisgen.Models;

[Table("DauTuNguoiDung")]
public partial class DauTuNguoiDung
{
    [Key]
    public long Id { get; set; }

    public long UserId { get; set; }

    public long GoiDauTuId { get; set; }

    public long KyHanId { get; set; }

    [Column(TypeName = "decimal(18, 8)")]
    public decimal SoTienDauTu { get; set; }

    public DateOnly? NgayBatDau { get; set; }

    public DateOnly? NgayKetThuc { get; set; }

    [StringLength(50)]
    public string? TrangThai { get; set; }

    [ForeignKey("GoiDauTuId")]
    [InverseProperty("DauTuNguoiDungs")]
    public virtual GoiDauTu GoiDauTu { get; set; } = null!;

    [ForeignKey("KyHanId")]
    [InverseProperty("DauTuNguoiDungs")]
    public virtual KyHanDauTu KyHan { get; set; } = null!;

    [InverseProperty("DauTu")]
    public virtual ICollection<LaiHangNgay> LaiHangNgays { get; set; } = new List<LaiHangNgay>();

    [ForeignKey("UserId")]
    [InverseProperty("DauTuNguoiDungs")]
    public virtual NguoiDung User { get; set; } = null!;
}
