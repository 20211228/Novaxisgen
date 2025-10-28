using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Novaxisgen.Models;

[Table("DoanhSoNhiPhan")]
public partial class DoanhSoNhiPhan
{
    [Key]
    public long Id { get; set; }

    public long UserId { get; set; }

    [Column(TypeName = "decimal(18, 8)")]
    public decimal? TongTrai { get; set; }

    [Column(TypeName = "decimal(18, 8)")]
    public decimal? TongPhai { get; set; }

    [Column(TypeName = "decimal(18, 8)")]
    public decimal? DoanhSoDaTinhTrai { get; set; }

    [Column(TypeName = "decimal(18, 8)")]
    public decimal? DoanhSoDaTinhPhai { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CapNhatGanNhat { get; set; }

    [ForeignKey("UserId")]
    [InverseProperty("DoanhSoNhiPhans")]
    public virtual NguoiDung User { get; set; } = null!;
}
