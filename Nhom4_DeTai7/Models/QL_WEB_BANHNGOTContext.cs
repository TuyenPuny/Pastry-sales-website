using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace Nhom4_DeTai7.Models
{
    public class QL_WEB_BANHNGOTContext : DbContext
    {
        public QL_WEB_BANHNGOTContext() : base("name=ketnoiSQL")
        {
        }

        public DbSet<SanPham> SanPham { get; set; }
        public DbSet<GioHang> GioHang { get; set; }
        public DbSet<NguoiDung> NguoiDung { get; set; }
        public DbSet<DanhMuc> DanhMuc { get; set; }

        public DbSet<HoaDon> HoaDon { get; set; }
        public DbSet<CTHOADON> CT_HoaDon { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Cấu hình các khóa chính
            modelBuilder.Entity<DanhMuc>()
                .HasKey(d => d.id_dm);

            modelBuilder.Entity<GioHang>()
                .HasKey(g => g.ID_GH);

            modelBuilder.Entity<NguoiDung>()
                .HasKey(n => n.ID_ND);

            modelBuilder.Entity<SanPham>()
                .HasKey(s => s.id_sp);

            // Cấu hình ánh xạ bảng
            modelBuilder.Entity<NguoiDung>()
                .ToTable("NGUOIDUNG");

            modelBuilder.Entity<GioHang>()
                .ToTable("GIOHANG");

            modelBuilder.Entity<SanPham>()
                .ToTable("SANPHAM");

            modelBuilder.Entity<DanhMuc>()
                .ToTable("DANHMUC");

            modelBuilder.Entity<HoaDon>()
                .ToTable("HOADON");

            modelBuilder.Entity<CTHOADON>()
                .ToTable("CT_HOADON");
            // Gọi phương thức cơ sở
            base.OnModelCreating(modelBuilder);
        }
    }
}