using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace Nhom4_DeTai7.Areas.ADMIN.Model
{
    public class DBConnect : DbContext
    {
        public DBConnect() : base("name=ketnoiSQL")
        {
        }
        public DbSet<SANPHAM> SanPham { get; set; }
        public DbSet<NGUOIDUNG> taikhoan { get; set; }
        public DbSet<DANHMUC> DanhMuc { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            modelBuilder.Entity<SANPHAM>()
                .HasKey(s => s.ID_SP);
            modelBuilder.Entity<DANHMUC>()
                .HasKey(s => s.ID_DM);

            // Cấu hình ánh xạ bảng
            modelBuilder.Entity<SANPHAM>()
                .ToTable("SANPHAM");
            modelBuilder.Entity<DANHMUC>()
                .ToTable("DANHMUC");



            // Gọi phương thức cơ sở
            base.OnModelCreating(modelBuilder);
        }
    }
}