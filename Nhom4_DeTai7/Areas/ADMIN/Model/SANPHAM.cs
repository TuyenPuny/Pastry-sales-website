using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Nhom4_DeTai7.Areas.ADMIN.Model
{
    [Table("SANPHAM")]
    public class SANPHAM
    {
        [Key]
        [Column("ID_SP")]
        [StringLength(10)]
        public string ID_SP { get; set; }

        [Column("TEN_SP")]
        [StringLength(100)]
        public string TEN_SP { get; set; }

        [Column("GIAGOC")]
        public int GIAGOC { get; set; }

        [Column("GIAKM")]
        public int? GIAKM { get; set; }

        [Column("KICHTHUOC")]
        public int KICHTHUOC { get; set; }

        [Column("HINHANH")]
        [StringLength(255)]
        public string HINHANH { get; set; }

        [Column("TINHTRANG")]
        [StringLength(200)]
        public string TINHTRANG { get; set; }

        [Column("SOLUONG")]
        public int SOLUONG { get; set; }

        [Column("HSD")]
        [StringLength(55)]
        public string HSD { get; set; }

        [Column("THANHPHAN")]
        [StringLength(255)]
        public string THANHPHAN { get; set; }

        [Column("MOTA")]
        [StringLength(255)]
        public string MOTA { get; set; }

        [Column("ID_DM")]
        [StringLength(10)]
        public string ID_DM { get; set; }

    }
}