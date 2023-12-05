using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace HotelBooking.Models;

public partial class HotelBookingContext : DbContext
{
    public HotelBookingContext()
    {
    }

    public HotelBookingContext(DbContextOptions<HotelBookingContext> options)
        : base(options)
    {
    }

    public virtual DbSet<TblChiTietDatPhong> TblChiTietDatPhongs { get; set; }

    public virtual DbSet<TblDanhGium> TblDanhGia { get; set; }

    public virtual DbSet<TblDatphong> TblDatphongs { get; set; }

    public virtual DbSet<TblKhachSan> TblKhachSans { get; set; }

    public virtual DbSet<TblPhong> TblPhongs { get; set; }

    public virtual DbSet<TblUser> TblUsers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=LAPTOP-TNKCLHBF;Initial Catalog=HotelBooking;Integrated Security=True;Encrypt=True;Trust Server Certificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TblChiTietDatPhong>(entity =>
        {
            entity.HasKey(e => e.IdChitietdatphong).HasName("PK__tblChiTi__1DFDED41C6E6211B");

            entity.ToTable("tblChiTietDatPhong");

            entity.Property(e => e.IdChitietdatphong).HasColumnName("ID_Chitietdatphong");
            entity.Property(e => e.IdMadatphong).HasColumnName("ID_Madatphong");
            entity.Property(e => e.SChuthich).HasColumnName("sChuthich");
            entity.Property(e => e.SEmailkhachhang)
                .HasMaxLength(255)
                .HasColumnName("sEmailkhachhang");
            entity.Property(e => e.SSdtkhachhang)
                .HasMaxLength(20)
                .HasColumnName("sSDTkhachhang");
            entity.Property(e => e.STenuser)
                .HasMaxLength(100)
                .HasColumnName("sTenuser");

            entity.HasOne(d => d.IdMadatphongNavigation).WithMany(p => p.TblChiTietDatPhongs)
                .HasForeignKey(d => d.IdMadatphong)
                .HasConstraintName("FK__tblChiTie__ID_Ma__4222D4EF");
        });

        modelBuilder.Entity<TblDanhGium>(entity =>
        {
            entity.HasKey(e => e.IdDanhgia).HasName("PK__tblDanhG__F6080CB71BF8A15A");

            entity.ToTable("tblDanhGia");

            entity.Property(e => e.IdDanhgia).HasColumnName("ID_Danhgia");
            entity.Property(e => e.ISosaodanhgia).HasColumnName("iSosaodanhgia");
            entity.Property(e => e.IdKhachhang).HasColumnName("ID_Khachhang");
            entity.Property(e => e.IdKhachsan).HasColumnName("ID_Khachsan");
            entity.Property(e => e.SNoidung).HasColumnName("sNoidung");

            entity.HasOne(d => d.IdKhachhangNavigation).WithMany(p => p.TblDanhGia)
                .HasForeignKey(d => d.IdKhachhang)
                .HasConstraintName("FK__tblDanhGi__ID_Kh__45F365D3");

            entity.HasOne(d => d.IdKhachsanNavigation).WithMany(p => p.TblDanhGia)
                .HasForeignKey(d => d.IdKhachsan)
                .HasConstraintName("FK__tblDanhGi__ID_Kh__44FF419A");
        });

        modelBuilder.Entity<TblDatphong>(entity =>
        {
            entity.HasKey(e => e.IdMadatphong).HasName("PK__tblDatph__A4E9B33F47BA1906");

            entity.ToTable("tblDatphong");

            entity.Property(e => e.IdMadatphong).HasColumnName("ID_Madatphong");
            entity.Property(e => e.DNgaycheckin).HasColumnName("dNgaycheckin");
            entity.Property(e => e.DNgaycheckout).HasColumnName("dNgaycheckout");
            entity.Property(e => e.IdPhong).HasColumnName("ID_Phong");
            entity.Property(e => e.IdUser).HasColumnName("ID_User");
            entity.Property(e => e.SHinhthucthanhtoan).HasColumnName("sHinhthucthanhtoan");

            entity.HasOne(d => d.IdPhongNavigation).WithMany(p => p.TblDatphongs)
                .HasForeignKey(d => d.IdPhong)
                .HasConstraintName("FK__tblDatpho__ID_Ph__3F466844");

            entity.HasOne(d => d.IdUserNavigation).WithMany(p => p.TblDatphongs)
                .HasForeignKey(d => d.IdUser)
                .HasConstraintName("FK__tblDatpho__ID_Us__3E52440B");
        });

        modelBuilder.Entity<TblKhachSan>(entity =>
        {
            entity.HasKey(e => e.IdKhachsan).HasName("PK__tblKhach__AFF3A95928126AA3");

            entity.ToTable("tblKhachSan");

            entity.Property(e => e.IdKhachsan).HasColumnName("ID_Khachsan");
            entity.Property(e => e.SAnhkhachsan).HasColumnName("sAnhkhachsan");
            entity.Property(e => e.SDanhgia)
                .HasColumnType("decimal(3, 2)")
                .HasColumnName("sDanhgia");
            entity.Property(e => e.SDiachi)
                .HasMaxLength(255)
                .HasColumnName("sDiachi");
            entity.Property(e => e.SMotakhachsan).HasColumnName("sMotakhachsan");
            entity.Property(e => e.STenkhachsan)
                .HasMaxLength(255)
                .HasColumnName("sTenkhachsan");
            entity.Property(e => e.IdUser).HasColumnName("ID_User");
            entity.HasOne(d => d.IdUserNavigation).WithMany(p => p.TblKhachSans)
                .HasForeignKey(d => d.IdUser)
                .HasConstraintName("FK__tblKhach__ID_Us__3E52440B");
        });

        modelBuilder.Entity<TblPhong>(entity =>
        {
            entity.HasKey(e => e.IdPhong).HasName("PK__tblPhong__DA88924DFF715CFB");

            entity.ToTable("tblPhong");

            entity.Property(e => e.IdPhong).HasColumnName("ID_Phong");
            entity.Property(e => e.FGiaphong).HasColumnName("fGiaphong");
            entity.Property(e => e.IdKhachsan).HasColumnName("ID_Khachsan");
            entity.Property(e => e.SAnhphong).HasColumnName("sAnhphong");
            entity.Property(e => e.SKieuPhong)
                .HasMaxLength(50)
                .HasColumnName("sKieuPhong");
            entity.Property(e => e.SMotaphong).HasColumnName("sMotaphong");
            entity.Property(e => e.SPhongcontrong).HasColumnName("sPhongcontrong");
            entity.Property(e => e.STiennghi).HasColumnName("sTiennghi");

            entity.HasOne(d => d.IdKhachsanNavigation).WithMany(p => p.TblPhongs)
                .HasForeignKey(d => d.IdKhachsan)
                .HasConstraintName("FK__tblPhong__ID_Kha__3B75D760");
        });

        modelBuilder.Entity<TblUser>(entity =>
        {
            entity.HasKey(e => e.IdUser).HasName("PK__tblUser__ED4DE4420F41A9A0");

            entity.ToTable("tblUser");

            entity.Property(e => e.IdUser).HasColumnName("ID_User");
            entity.Property(e => e.SEmail)
                .HasMaxLength(255)
                .HasColumnName("sEmail");
            entity.Property(e => e.SLoaiuser)
                .HasMaxLength(20)
                .HasColumnName("sLoaiuser");
            entity.Property(e => e.SMatkhau)
                .HasMaxLength(255)
                .HasColumnName("sMatkhau");
            entity.Property(e => e.SSdt)
                .HasMaxLength(20)
                .HasColumnName("sSDT");
            entity.Property(e => e.STendaydu)
                .HasMaxLength(100)
                .HasColumnName("sTendaydu");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
