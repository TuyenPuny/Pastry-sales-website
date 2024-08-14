using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Nhom4_DeTai7.Models
{
    public class SanPham
    {
        public string id_sp { get; set; }
        public string ten_sp { get; set; }
        public int giagoc { get; set; }

        public int? giakm { get; set; }
        public int kichthuoc { get; set; }
        public int soluong { get; set; }
        public string hinhanh { get; set; }
        public string tinhtrang { get; set; }
        public string hansudung { get; set; }

        public string thanhphan { get; set; }
        public string mota { get; set; }
        public string id_dm { get; set; }

        public List<SanPham> danhsachSP { get; set; }

        public List<SanPham> danhsachSP_ten { get; set; }

        public List<SanPham> danhsachMoiRaLo { get; set; }
        public List<SanPham> danhsachKhuyenMai { get; set; }

        public List<SanPham> chitietSanPham { get; set; }

        public virtual ICollection<GioHang> GioHang { get; set; }


        public void loadSPM_KM()
        {
            //string conStr = @"Server=CONGDUNG-1409\SQLEXPRESS;Database=baikiemtra1;Integrated Security=True";
            string conStr = System.Configuration.ConfigurationManager.ConnectionStrings["ketnoiSQL"].ConnectionString;
            SqlConnection conn = new SqlConnection(conStr);
            conn.Open();

            // Load danh sách sản phẩm mới ra lò
            SqlCommand cmdMoiRaLo = new SqlCommand("SELECT ID_SP, TEN_SP, GIAGOC, HINHANH, TINHTRANG FROM SANPHAM WHERE TINHTRANG = N'Mới ra lò'", conn);
            SqlDataReader drMoiRaLo = cmdMoiRaLo.ExecuteReader();
            danhsachMoiRaLo = new List<SanPham>();
            while (drMoiRaLo.Read())
            {
                danhsachMoiRaLo.Add(new SanPham
                {
                    id_sp = drMoiRaLo.GetValue(0).ToString(),
                    ten_sp = drMoiRaLo.GetValue(1).ToString(),
                    giagoc = Convert.ToInt32(drMoiRaLo.GetValue(2)),
                    hinhanh = drMoiRaLo.GetValue(3).ToString(),
                    tinhtrang = drMoiRaLo.GetValue(4).ToString()
                }); ;
            }
            drMoiRaLo.Close();

            // Load danh sách sản phẩm khuyến mãi
            SqlCommand cmdKhuyenMai = new SqlCommand("SELECT ID_SP, TEN_SP, GIAGOC, GIAKM, HINHANH, TINHTRANG FROM SANPHAM WHERE TINHTRANG = N'Khuyến mãi'", conn);
            SqlDataReader drKhuyenMai = cmdKhuyenMai.ExecuteReader();
            danhsachKhuyenMai = new List<SanPham>();
            while (drKhuyenMai.Read())
            {
                danhsachKhuyenMai.Add(new SanPham
                {
                    id_sp = drKhuyenMai.GetValue(0).ToString(),
                    ten_sp = drKhuyenMai.GetValue(1).ToString(),
                    giagoc = Convert.ToInt32(drKhuyenMai.GetValue(2)),
                    giakm = drKhuyenMai.IsDBNull(3) ? (int?)null : Convert.ToInt32(drKhuyenMai.GetValue(3)),
                    hinhanh = drKhuyenMai.GetValue(4).ToString(),
                    tinhtrang = drKhuyenMai.GetValue(5).ToString()
                });
            }
            drKhuyenMai.Close();
        }

        public void loadSP()
        {
            string conStr = System.Configuration.ConfigurationManager.ConnectionStrings["ketnoiSQL"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(conStr))
            {
                conn.Open();
                SqlCommand cmdSP = new SqlCommand("SELECT ID_SP, TEN_SP, GIAGOC, GIAKM, KICHTHUOC, HINHANH, TINHTRANG, THANHPHAN, ID_DM FROM SANPHAM", conn);
                using (SqlDataReader drSP = cmdSP.ExecuteReader())
                {
                    danhsachSP = new List<SanPham>();
                    while (drSP.Read())
                    {
                        danhsachSP.Add(new SanPham
                        {
                            id_sp = drSP["ID_SP"].ToString(),
                            ten_sp = drSP["TEN_SP"].ToString(),
                            giagoc = Convert.ToInt32(drSP["GIAGOC"]),
                            giakm = drSP.IsDBNull(drSP.GetOrdinal("GIAKM")) ? (int?)null : Convert.ToInt32(drSP["GIAKM"]),
                            kichthuoc = Convert.ToInt32(drSP["KICHTHUOC"]),
                            hinhanh = drSP["HINHANH"].ToString(),
                            tinhtrang = drSP["TINHTRANG"].ToString(),
                            thanhphan = drSP["THANHPHAN"].ToString(),
                            id_dm = drSP["ID_DM"].ToString()
                        });
                    }
                }
            }
        }

        public void loadSP_DM(string danhmuc)
        {
            //string conStr = @"Server=CONGDUNG-1409\SQLEXPRESS;Database=baikiemtra1;Integrated Security=True";
            string conStr = System.Configuration.ConfigurationManager.ConnectionStrings["ketnoiSQL"].ConnectionString;
            SqlConnection conn = new SqlConnection(conStr);
            conn.Open();
            SqlCommand cmdSP = new SqlCommand("SELECT ID_SP, TEN_SP, GIAGOC, GIAKM, KICHTHUOC, HINHANH, TINHTRANG, THANHPHAN, ID_DM FROM SANPHAM where ID_DM = @id", conn);
            cmdSP.Parameters.AddWithValue("@id", danhmuc);
            SqlDataReader drSP = cmdSP.ExecuteReader();
            danhsachSP = new List<SanPham>();
            while (drSP.Read())
            {
                danhsachSP.Add(new SanPham
                {
                    id_sp = drSP["ID_SP"].ToString(),
                    ten_sp = drSP["TEN_SP"].ToString(),
                    giagoc = Convert.ToInt32(drSP["GIAGOC"]),
                    giakm = drSP.IsDBNull(drSP.GetOrdinal("GIAKM")) ? (int?)null : Convert.ToInt32(drSP["GIAKM"]),
                    kichthuoc = Convert.ToInt32(drSP["KICHTHUOC"]),
                    hinhanh = drSP["HINHANH"].ToString(),
                    tinhtrang = drSP["TINHTRANG"].ToString(),
                    thanhphan = drSP["THANHPHAN"].ToString(),
                    id_dm = drSP["ID_DM"].ToString()
                });
            }
            drSP.Close();
        }

        public void searchSP(string ten)
        {
            if (danhsachSP == null)
            {
                throw new InvalidOperationException("Danh sách sản phẩm chưa được tải.");
            }

            danhsachSP_ten = danhsachSP.Where(sp => sp.ten_sp.IndexOf(ten, StringComparison.OrdinalIgnoreCase) >= 0).ToList();
        }

        // chi tiết sản phẩm
        public void CT_SanPham(string id)
        {
            string conStr = System.Configuration.ConfigurationManager.ConnectionStrings["ketnoiSQL"].ConnectionString;
            SqlConnection conn = new SqlConnection(conStr);
            conn.Open();
            SqlCommand cmdSP = new SqlCommand("SELECT * FROM SANPHAM WHERE ID_SP = @ID_SP", conn);
            cmdSP.Parameters.AddWithValue("@ID_SP", id);
            SqlDataReader drSP = cmdSP.ExecuteReader();
            chitietSanPham = new List<SanPham>();
            while (drSP.Read())
            {
                chitietSanPham.Add(new SanPham
                {
                    id_sp = drSP["ID_SP"].ToString(),
                    ten_sp = drSP["TEN_SP"].ToString(),
                    giagoc = Convert.ToInt32(drSP["GIAGOC"]),
                    giakm = drSP.IsDBNull(drSP.GetOrdinal("GIAKM")) ? (int?)null : Convert.ToInt32(drSP["GIAKM"]),
                    kichthuoc = Convert.ToInt32(drSP["KICHTHUOC"]),
                    hinhanh = drSP["HINHANH"].ToString(),
                    tinhtrang = drSP["TINHTRANG"].ToString(),
                    soluong = Convert.ToInt32(drSP["SOLUONG"]),
                    hansudung = drSP["HSD"].ToString(),
                    thanhphan = drSP["THANHPHAN"].ToString(),
                    mota = drSP["MOTA"].ToString(),
                    id_dm = drSP["ID_DM"].ToString()
                });
            }
            drSP.Close();
        }
    }
}