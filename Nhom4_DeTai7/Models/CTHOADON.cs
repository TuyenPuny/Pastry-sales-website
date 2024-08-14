using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Nhom4_DeTai7.Models
{
    [Table("CT_HOADON")]
    public class CTHOADON
    {
        [Key, Column(Order = 0)]
        [StringLength(10)]
        public string ID_HD { get; set; }

        [Key, Column(Order = 1)]
        [StringLength(10)]
        public string ID_SP { get; set; }

        [Required]
        public int SOLUONG { get; set; }

        [Required]
        public int DONGIA { get; set; }

        // Navigation properties
        [ForeignKey("ID_HD")]
        public virtual HoaDon HoaDon { get; set; }

        [ForeignKey("ID_SP")]
        public virtual SanPham SanPham { get; set; }
    }
}