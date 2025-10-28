using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Novaxisgen.Models;

[Table("GiaoDichBlockchain")]
public partial class GiaoDichBlockchain
{
    [Key]
    public long Id { get; set; }

    public long UserId { get; set; }

    [StringLength(100)]
    public string TxHash { get; set; } = null!;

    [StringLength(255)]
    public string? TuVi { get; set; }

    [StringLength(255)]
    public string? DenVi { get; set; }

    [Column(TypeName = "decimal(18, 8)")]
    public decimal SoTien { get; set; }

    [StringLength(50)]
    public string? TokenSymbol { get; set; }

    [StringLength(50)]
    public string? TrangThai { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? NgayTao { get; set; }

    [ForeignKey("UserId")]
    [InverseProperty("GiaoDichBlockchains")]
    public virtual NguoiDung User { get; set; } = null!;
}
