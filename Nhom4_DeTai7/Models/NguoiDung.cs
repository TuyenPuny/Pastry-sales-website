using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nhom4_DeTai7.Models
{
    public class NguoiDung
    {
        public int ID_ND { get; set; }
        public string TEN_ND { get; set; }
        public DateTime? NGAYSINH { get; set; }
        public string GIOITINH { get; set; }
        public string EMAIL { get; set; }   
        public string SDT { get; set; }
        public string TAIKHOAN { get; set; }
        public string MATKHAU { get; set; }
        public string VAITRO { get; set; }

        public string DIA_CHI { get; set; }
    }
}