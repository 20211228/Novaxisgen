using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Novaxisgen.Models;

[Table("CayNhiPhan")]
public partial class CayNhiPhan
{
    [Key]
    public long Id { get; set; }

    public long UserId { get; set; }

    public long? PlacementId { get; set; }

    public long? SponsorId { get; set; }

    [StringLength(255)]
    public string? SponsorNode { get; set; }

    [StringLength(255)]
    public string? PlacementNode { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? NgayTao { get; set; }

    [ForeignKey("PlacementId")]
    [InverseProperty("CayNhiPhanPlacements")]
    public virtual NguoiDung? Placement { get; set; }

    [ForeignKey("SponsorId")]
    [InverseProperty("CayNhiPhanSponsors")]
    public virtual NguoiDung? Sponsor { get; set; }

    [ForeignKey("UserId")]
    [InverseProperty("CayNhiPhanUsers")]
    public virtual NguoiDung User { get; set; } = null!;
}
