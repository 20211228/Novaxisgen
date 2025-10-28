using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Novaxisgen.Models;

[Table("HoaHongNhiPhan")]
public partial class HoaHongNhiPhan
{
    [Key]
    public long Id { get; set; }

    public long UserId { get; set; }

    [Column(TypeName = "decimal(18, 8)")]
    public decimal DoanhSoNhoHon { get; set; }

    [Column(TypeName = "decimal(5, 2)")]
    public decimal TyLeHoaHong { get; set; }

    [Column(TypeName = "decimal(18, 8)")]
    public decimal SoTienHoaHong { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? NgayTao { get; set; }

    [ForeignKey("UserId")]
    [InverseProperty("HoaHongNhiPhans")]
    public virtual NguoiDung User { get; set; } = null!;
}
