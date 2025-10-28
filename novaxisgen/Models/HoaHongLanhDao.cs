using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Novaxisgen.Models;

[Table("HoaHongLanhDao")]
public partial class HoaHongLanhDao
{
    [Key]
    public long Id { get; set; }

    public long NguoiNhanId { get; set; }

    public long NguoiTuyenDuoiId { get; set; }

    public int CapVip { get; set; }

    [Column(TypeName = "decimal(18, 8)")]
    public decimal SoTienHoaHong { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? NgayTao { get; set; }

    [ForeignKey("CapVip")]
    [InverseProperty("HoaHongLanhDaos")]
    public virtual CapVip CapVipNavigation { get; set; } = null!;

    [ForeignKey("NguoiNhanId")]
    [InverseProperty("HoaHongLanhDaoNguoiNhans")]
    public virtual NguoiDung NguoiNhan { get; set; } = null!;

    [ForeignKey("NguoiTuyenDuoiId")]
    [InverseProperty("HoaHongLanhDaoNguoiTuyenDuois")]
    public virtual NguoiDung NguoiTuyenDuoi { get; set; } = null!;
}
