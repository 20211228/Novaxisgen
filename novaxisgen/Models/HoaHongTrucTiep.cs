using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Novaxisgen.Models;

[Table("HoaHongTrucTiep")]
public partial class HoaHongTrucTiep
{
    [Key]
    public long Id { get; set; }

    public long NguoiNhanId { get; set; }

    public long NguoiTuyenDuoiId { get; set; }

    public int CapTang { get; set; }

    [Column(TypeName = "decimal(18, 8)")]
    public decimal? SoTienHoaHong { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? NgayTao { get; set; }

    [ForeignKey("NguoiNhanId")]
    [InverseProperty("HoaHongTrucTiepNguoiNhans")]
    public virtual NguoiDung NguoiNhan { get; set; } = null!;

    [ForeignKey("NguoiTuyenDuoiId")]
    [InverseProperty("HoaHongTrucTiepNguoiTuyenDuois")]
    public virtual NguoiDung NguoiTuyenDuoi { get; set; } = null!;
}
