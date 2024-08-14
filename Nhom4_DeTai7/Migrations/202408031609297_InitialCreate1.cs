namespace Nhom4_DeTai7.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate1 : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.DanhMucs", newName: "DANHMUC");
            RenameTable(name: "dbo.GioHangs", newName: "GIOHANG");
            RenameTable(name: "dbo.NguoiDungs", newName: "NGUOIDUNG");
            RenameTable(name: "dbo.SanPhams", newName: "SANPHAM");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.SANPHAM", newName: "SanPhams");
            RenameTable(name: "dbo.NGUOIDUNG", newName: "NguoiDungs");
            RenameTable(name: "dbo.GIOHANG", newName: "GioHangs");
            RenameTable(name: "dbo.DANHMUC", newName: "DanhMucs");
        }
    }
}
