using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Nhom4_DeTai7.Areas.ADMIN.Model
{
    [Table("NGUOIDUNG")]
    public class NGUOIDUNG
    {
        [Key]
        [Column("ID_ND")]
        public int ID_ND { get; set; }

        [Column("TEN_ND")]
        [StringLength(100)]
        public string TEN_ND { get; set; }

        [Column("NGAYSINH")]
        public DateTime? NGAYSINH { get; set; }

        [Column("GIOITINH")]
        [StringLength(5)]
        public string GIOITINH { get; set; }

        [Column("EMAIL")]
        [StringLength(100)]
        public string EMAIL { get; set; }

        [Column("SDT")]
        [StringLength(10)]
        public string SDT { get; set; }

        [Column("TAIKHOAN")]
        [StringLength(30, ErrorMessage = "Tài khoản không được quá 30 ký tự.")]
        public string TAIKHOAN { get; set; }

        [Column("MATKHAU")]
        [StringLength(50)]
        public string MATKHAU { get; set; }

        [Column("VAITRO")]
        [StringLength(30)]
        public string VAITRO { get; set; }
    }
}