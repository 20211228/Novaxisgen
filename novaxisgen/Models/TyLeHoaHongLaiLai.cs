using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Novaxisgen.Models;

[Table("TyLeHoaHongLaiLai")]
public partial class TyLeHoaHongLaiLai
{
    [Key]
    public long Id { get; set; }

    public long GoiDauTuId { get; set; }

    public int Tang { get; set; }

    [Column(TypeName = "decimal(5, 2)")]
    public decimal TyLe { get; set; }

    [ForeignKey("GoiDauTuId")]
    [InverseProperty("TyLeHoaHongLaiLais")]
    public virtual GoiDauTu GoiDauTu { get; set; } = null!;
}
