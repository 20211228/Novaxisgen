using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Novaxisgen.Models;

[Table("TyLeHoaHongTrucTiep")]
public partial class TyLeHoaHongTrucTiep
{
    [Key]
    public long Id { get; set; }

    public long GoiDauTuId { get; set; }

    public int Tang { get; set; }

    [Column(TypeName = "decimal(5, 2)")]
    public decimal TyLe { get; set; }

    [ForeignKey("GoiDauTuId")]
    [InverseProperty("TyLeHoaHongTrucTieps")]
    public virtual GoiDauTu GoiDauTu { get; set; } = null!;
}
