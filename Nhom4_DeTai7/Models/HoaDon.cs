using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Nhom4_DeTai7.Models
{
    [Table("HOADON")]
    public class HoaDon
    {
        [Key]
        [StringLength(10)]
        public string ID_HD { get; set; }

        [Required]
        public int ID_ND { get; set; }

        [Required]
        public DateTime NGAYTT { get; set; }

        [Required]
        [StringLength(100)]
        public string PTTT { get; set; }

        [Required]
        public int THANHTIEN { get; set; }

        [StringLength(255)]
        public string TTMH { get; set; }

        // Navigation property
        [ForeignKey("ID_ND")]
        public virtual NguoiDung NguoiDung { get; set; }

        // Collection of ChiTietHoaDon
        public virtual ICollection<CTHOADON> ChiTietHoaDon { get; set; }
    }
}