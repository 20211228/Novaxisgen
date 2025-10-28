using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Novaxisgen.Models;

[Table("LaiHangNgay")]
public partial class LaiHangNgay
{
    [Key]
    public long Id { get; set; }

    public long UserId { get; set; }

    public long DauTuId { get; set; }

    public DateOnly Ngay { get; set; }

    [Column(TypeName = "decimal(18, 8)")]
    public decimal SoTienLai { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? NgayTao { get; set; }

    [ForeignKey("DauTuId")]
    [InverseProperty("LaiHangNgays")]
    public virtual DauTuNguoiDung DauTu { get; set; } = null!;

    [ForeignKey("UserId")]
    [InverseProperty("LaiHangNgays")]
    public virtual NguoiDung User { get; set; } = null!;
}
