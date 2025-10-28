using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Novaxisgen.Models;

public partial class NovaxisgenContext : DbContext
{
    public NovaxisgenContext()
    {
    }

    public NovaxisgenContext(DbContextOptions<NovaxisgenContext> options)
        : base(options)
    {
    }

    public virtual DbSet<CapVip> CapVips { get; set; }

    public virtual DbSet<CayNhiPhan> CayNhiPhans { get; set; }

    public virtual DbSet<DauTuNguoiDung> DauTuNguoiDungs { get; set; }

    public virtual DbSet<DoanhSoNhiPhan> DoanhSoNhiPhans { get; set; }

    public virtual DbSet<GiaoDichBlockchain> GiaoDichBlockchains { get; set; }

    public virtual DbSet<GiaoDichVi> GiaoDichVis { get; set; }

    public virtual DbSet<GoiDauTu> GoiDauTus { get; set; }

    public virtual DbSet<HoaHongLaiTrenLai> HoaHongLaiTrenLais { get; set; }

    public virtual DbSet<HoaHongLanhDao> HoaHongLanhDaos { get; set; }

    public virtual DbSet<HoaHongNhiPhan> HoaHongNhiPhans { get; set; }

    public virtual DbSet<HoaHongTrucTiep> HoaHongTrucTieps { get; set; }

    public virtual DbSet<KyHanDauTu> KyHanDauTus { get; set; }

    public virtual DbSet<LaiHangNgay> LaiHangNgays { get; set; }

    public virtual DbSet<NguoiDung> NguoiDungs { get; set; }

    public virtual DbSet<TyLeHoaHongLaiLai> TyLeHoaHongLaiLais { get; set; }

    public virtual DbSet<TyLeHoaHongTrucTiep> TyLeHoaHongTrucTieps { get; set; }

    public virtual DbSet<ViNguoiDung> ViNguoiDungs { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=NovaxisgenDB");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CapVip>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__CapVIP__3214EC071425F1A9");
        });

        modelBuilder.Entity<CayNhiPhan>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__CayNhiPh__3214EC07D9F3DF19");

            entity.Property(e => e.NgayTao).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Placement).WithMany(p => p.CayNhiPhanPlacements).HasConstraintName("FK__CayNhiPha__Place__619B8048");

            entity.HasOne(d => d.Sponsor).WithMany(p => p.CayNhiPhanSponsors).HasConstraintName("FK__CayNhiPha__Spons__628FA481");

            entity.HasOne(d => d.User).WithMany(p => p.CayNhiPhanUsers)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__CayNhiPha__UserI__60A75C0F");
        });

        modelBuilder.Entity<DauTuNguoiDung>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__DauTuNgu__3214EC07D2C8DEB6");

            entity.Property(e => e.NgayBatDau).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.TrangThai).HasDefaultValue("Đang hoạt động");

            entity.HasOne(d => d.GoiDauTu).WithMany(p => p.DauTuNguoiDungs)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__DauTuNguo__GoiDa__4F7CD00D");

            entity.HasOne(d => d.KyHan).WithMany(p => p.DauTuNguoiDungs)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__DauTuNguo__KyHan__5070F446");

            entity.HasOne(d => d.User).WithMany(p => p.DauTuNguoiDungs)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__DauTuNguo__UserI__4E88ABD4");
        });

        modelBuilder.Entity<DoanhSoNhiPhan>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__DoanhSoN__3214EC07DBCD3D37");

            entity.Property(e => e.CapNhatGanNhat).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.DoanhSoDaTinhPhai).HasDefaultValue(0m);
            entity.Property(e => e.DoanhSoDaTinhTrai).HasDefaultValue(0m);
            entity.Property(e => e.TongPhai).HasDefaultValue(0m);
            entity.Property(e => e.TongTrai).HasDefaultValue(0m);

            entity.HasOne(d => d.User).WithMany(p => p.DoanhSoNhiPhans)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__DoanhSoNh__UserI__6A30C649");
        });

        modelBuilder.Entity<GiaoDichBlockchain>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__GiaoDich__3214EC07FB133DBE");

            entity.Property(e => e.NgayTao).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.TrangThai).HasDefaultValue("Hoàn tất");

            entity.HasOne(d => d.User).WithMany(p => p.GiaoDichBlockchains)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__GiaoDichB__UserI__05D8E0BE");
        });

        modelBuilder.Entity<GiaoDichVi>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__GiaoDich__3214EC077C9B65EE");

            entity.Property(e => e.NgayTao).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.TrangThai).HasDefaultValue("Chờ xử lý");

            entity.HasOne(d => d.User).WithMany(p => p.GiaoDichVis)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__GiaoDichV__UserI__787EE5A0");
        });

        modelBuilder.Entity<GoiDauTu>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__GoiDauTu__3214EC07E74A3E73");

            entity.Property(e => e.NgayTao).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.SoTangHoaHongLai).HasDefaultValue(1);
            entity.Property(e => e.SoTangHoaHongTrucTiep).HasDefaultValue(1);
        });

        modelBuilder.Entity<HoaHongLaiTrenLai>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__HoaHongL__3214EC07A558616F");

            entity.Property(e => e.Ngay).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.NgayTao).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.NguoiNhan).WithMany(p => p.HoaHongLaiTrenLaiNguoiNhans)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__HoaHongLa__Nguoi__7D439ABD");

            entity.HasOne(d => d.NguoiTuyenDuoi).WithMany(p => p.HoaHongLaiTrenLaiNguoiTuyenDuois)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__HoaHongLa__Nguoi__7E37BEF6");
        });

        modelBuilder.Entity<HoaHongLanhDao>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__HoaHongL__3214EC0746E42BAE");

            entity.Property(e => e.NgayTao).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.CapVipNavigation).WithMany(p => p.HoaHongLanhDaos)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__HoaHongLa__CapVi__73BA3083");

            entity.HasOne(d => d.NguoiNhan).WithMany(p => p.HoaHongLanhDaoNguoiNhans)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__HoaHongLa__Nguoi__71D1E811");

            entity.HasOne(d => d.NguoiTuyenDuoi).WithMany(p => p.HoaHongLanhDaoNguoiTuyenDuois)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__HoaHongLa__Nguoi__72C60C4A");
        });

        modelBuilder.Entity<HoaHongNhiPhan>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__HoaHongN__3214EC0783FA1DE1");

            entity.Property(e => e.NgayTao).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.User).WithMany(p => p.HoaHongNhiPhans)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__HoaHongNh__UserI__6E01572D");
        });

        modelBuilder.Entity<HoaHongTrucTiep>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__HoaHongT__3214EC07D60F7F3C");

            entity.Property(e => e.NgayTao).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.NguoiNhan).WithMany(p => p.HoaHongTrucTiepNguoiNhans)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__HoaHongTr__Nguoi__59063A47");

            entity.HasOne(d => d.NguoiTuyenDuoi).WithMany(p => p.HoaHongTrucTiepNguoiTuyenDuois)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__HoaHongTr__Nguoi__59FA5E80");
        });

        modelBuilder.Entity<KyHanDauTu>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__KyHanDau__3214EC071C5DC36F");

            entity.HasOne(d => d.GoiDauTu).WithMany(p => p.KyHanDauTus)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__KyHanDauT__GoiDa__49C3F6B7");
        });

        modelBuilder.Entity<LaiHangNgay>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__LaiHangN__3214EC0717A63FE6");

            entity.Property(e => e.NgayTao).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.DauTu).WithMany(p => p.LaiHangNgays)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__LaiHangNg__DauTu__5535A963");

            entity.HasOne(d => d.User).WithMany(p => p.LaiHangNgays)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__LaiHangNg__UserI__5441852A");
        });

        modelBuilder.Entity<NguoiDung>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__NguoiDun__3214EC0790776CB9");

            entity.Property(e => e.NgayTao).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.CapVipNavigation).WithMany(p => p.NguoiDungs).HasConstraintName("FK__NguoiDung__CapVi__3C69FB99");
        });

        modelBuilder.Entity<TyLeHoaHongLaiLai>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__TyLeHoaH__3214EC07DA13AEB1");

            entity.HasOne(d => d.GoiDauTu).WithMany(p => p.TyLeHoaHongLaiLais)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__TyLeHoaHo__GoiDa__01142BA1");
        });

        modelBuilder.Entity<TyLeHoaHongTrucTiep>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__TyLeHoaH__3214EC07E39A306A");

            entity.HasOne(d => d.GoiDauTu).WithMany(p => p.TyLeHoaHongTrucTieps)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__TyLeHoaHo__GoiDa__5CD6CB2B");
        });

        modelBuilder.Entity<ViNguoiDung>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ViNguoiD__3214EC0764AB0973");

            entity.Property(e => e.DonViTienTe).HasDefaultValue("USDT");
            entity.Property(e => e.NgayCapNhat).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.TrangThai).HasDefaultValue("Đang hoạt động");

            entity.HasOne(d => d.User).WithMany(p => p.ViNguoiDungs)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ViNguoiDu__UserI__4222D4EF");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
