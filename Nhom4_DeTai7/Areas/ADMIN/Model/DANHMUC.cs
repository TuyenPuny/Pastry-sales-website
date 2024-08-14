using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Nhom4_DeTai7.Areas.ADMIN.Model
{
    [Table("DANHMUC")]
    public class DANHMUC
    {
        [Key]
        [Column("ID_DM")]
        [StringLength(10)]
        public string ID_DM { get; set; }

        [Column("TEN_DM")]
        [StringLength(100)]
        public string TEN_DM { get; set; }
    }
}