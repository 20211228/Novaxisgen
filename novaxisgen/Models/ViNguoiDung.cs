using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Novaxisgen.Models;

[Table("ViNguoiDung")]
public partial class ViNguoiDung
{
    [Key]
    public long Id { get; set; }

    public long UserId { get; set; }

    [StringLength(255)]
    public string? DiaChiVi { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? NgayCapNhat { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? SoDu { get; set; }

    [StringLength(10)]
    public string? DonViTienTe { get; set; }

    [StringLength(50)]
    public string? TrangThai { get; set; }

    [StringLength(50)]
    public string? TokenSymbol { get; set; }

    [ForeignKey("UserId")]
    [InverseProperty("ViNguoiDungs")]
    public virtual NguoiDung User { get; set; } = null!;
}
