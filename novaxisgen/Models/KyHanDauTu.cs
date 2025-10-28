using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Novaxisgen.Models;

[Table("KyHanDauTu")]
public partial class KyHanDauTu
{
    [Key]
    public long Id { get; set; }

    public long GoiDauTuId { get; set; }

    public int SoThang { get; set; }

    [Column(TypeName = "decimal(5, 2)")]
    public decimal LaiSuatNgay { get; set; }

    [InverseProperty("KyHan")]
    public virtual ICollection<DauTuNguoiDung> DauTuNguoiDungs { get; set; } = new List<DauTuNguoiDung>();

    [ForeignKey("GoiDauTuId")]
    [InverseProperty("KyHanDauTus")]
    public virtual GoiDauTu GoiDauTu { get; set; } = null!;
}
