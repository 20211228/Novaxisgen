using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Novaxisgen.Models;

[Table("GiaoDichVi")]
public partial class GiaoDichVi
{
    [Key]
    public long Id { get; set; }

    public long UserId { get; set; }

    [StringLength(50)]
    public string? LoaiGiaoDich { get; set; }

    [StringLength(50)]
    public string? MaDonHang { get; set; }

    [Column(TypeName = "decimal(18, 8)")]
    public decimal SoTien { get; set; }

    [StringLength(255)]
    public string? DiaChiNhan { get; set; }

    [StringLength(50)]
    public string? TrangThai { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? NgayTao { get; set; }

    [ForeignKey("UserId")]
    [InverseProperty("GiaoDichVis")]
    public virtual NguoiDung User { get; set; } = null!;
}
