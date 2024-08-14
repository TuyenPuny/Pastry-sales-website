using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Nhom4_DeTai7.Models
{
    public class GioHang
    {
        public int ID_GH { get; set; }
        public string ID_SP { get; set; }
        public int SOLUONG { get; set; }
        public int ID_ND { get; set; }

  

        // Điều này là tùy chọn, nhưng giúp thiết lập mối quan hệ với các bảng khác
        public virtual SanPham SanPham { get; set; }
        public virtual NguoiDung NguoiDung { get; set; }

        
    }
}