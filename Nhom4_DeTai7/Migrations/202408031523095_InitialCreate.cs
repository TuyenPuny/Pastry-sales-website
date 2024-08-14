namespace Nhom4_DeTai7.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DanhMucs",
                c => new
                    {
                        id_dm = c.String(nullable: false, maxLength: 128),
                        tendm = c.String(),
                        DanhMuc_id_dm = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.id_dm)
                .ForeignKey("dbo.DanhMucs", t => t.DanhMuc_id_dm)
                .Index(t => t.DanhMuc_id_dm);
            
            CreateTable(
                "dbo.GioHangs",
                c => new
                    {
                        ID_GH = c.Int(nullable: false, identity: true),
                        ID_SP = c.String(maxLength: 128),
                        SOLUONG = c.Int(nullable: false),
                        ID_ND = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID_GH)
                .ForeignKey("dbo.NguoiDungs", t => t.ID_ND, cascadeDelete: true)
                .ForeignKey("dbo.SanPhams", t => t.ID_SP)
                .Index(t => t.ID_SP)
                .Index(t => t.ID_ND);
            
            CreateTable(
                "dbo.NguoiDungs",
                c => new
                    {
                        ID_ND = c.Int(nullable: false, identity: true),
                        TEN_ND = c.String(),
                        NGAYSINH = c.DateTime(),
                        GIOITINH = c.String(),
                        EMAIL = c.String(),
                        SDT = c.String(),
                        TAIKHOAN = c.String(),
                        MATKHAU = c.String(),
                        VAITRO = c.String(),
                    })
                .PrimaryKey(t => t.ID_ND);
            
            CreateTable(
                "dbo.SanPhams",
                c => new
                    {
                        id_sp = c.String(nullable: false, maxLength: 128),
                        ten_sp = c.String(),
                        giagoc = c.Int(nullable: false),
                        kichthuoc = c.Int(nullable: false),
                        giakm = c.Int(),
                        hinhanh = c.String(),
                        tinhtrang = c.String(),
                        thanhphan = c.String(),
                        id_dm = c.String(),
                        SanPham_id_sp = c.String(maxLength: 128),
                        SanPham_id_sp1 = c.String(maxLength: 128),
                        SanPham_id_sp2 = c.String(maxLength: 128),
                        SanPham_id_sp3 = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.id_sp)
                .ForeignKey("dbo.SanPhams", t => t.SanPham_id_sp)
                .ForeignKey("dbo.SanPhams", t => t.SanPham_id_sp1)
                .ForeignKey("dbo.SanPhams", t => t.SanPham_id_sp2)
                .ForeignKey("dbo.SanPhams", t => t.SanPham_id_sp3)
                .Index(t => t.SanPham_id_sp)
                .Index(t => t.SanPham_id_sp1)
                .Index(t => t.SanPham_id_sp2)
                .Index(t => t.SanPham_id_sp3);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.GioHangs", "ID_SP", "dbo.SanPhams");
            DropForeignKey("dbo.SanPhams", "SanPham_id_sp3", "dbo.SanPhams");
            DropForeignKey("dbo.SanPhams", "SanPham_id_sp2", "dbo.SanPhams");
            DropForeignKey("dbo.SanPhams", "SanPham_id_sp1", "dbo.SanPhams");
            DropForeignKey("dbo.SanPhams", "SanPham_id_sp", "dbo.SanPhams");
            DropForeignKey("dbo.GioHangs", "ID_ND", "dbo.NguoiDungs");
            DropForeignKey("dbo.DanhMucs", "DanhMuc_id_dm", "dbo.DanhMucs");
            DropIndex("dbo.SanPhams", new[] { "SanPham_id_sp3" });
            DropIndex("dbo.SanPhams", new[] { "SanPham_id_sp2" });
            DropIndex("dbo.SanPhams", new[] { "SanPham_id_sp1" });
            DropIndex("dbo.SanPhams", new[] { "SanPham_id_sp" });
            DropIndex("dbo.GioHangs", new[] { "ID_ND" });
            DropIndex("dbo.GioHangs", new[] { "ID_SP" });
            DropIndex("dbo.DanhMucs", new[] { "DanhMuc_id_dm" });
            DropTable("dbo.SanPhams");
            DropTable("dbo.NguoiDungs");
            DropTable("dbo.GioHangs");
            DropTable("dbo.DanhMucs");
        }
    }
}
