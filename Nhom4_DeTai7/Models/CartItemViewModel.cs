using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nhom4_DeTai7.Models
{
    public class CartItemViewModel
    {
        public int ID_GH { get; set; }
        public int SOLUONG { get; set; }
        public string TenSP { get; set; }
        public int GiaGoc { get; set; }
        public int GiaKM { get; set; }
        public string HinhAnh { get; set; }
        public int TotalPrice { get; set; }

        public int SLTON { get; set; }


    }
}