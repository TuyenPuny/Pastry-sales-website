using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Nhom4_DeTai7.Models
{
    public class DanhMuc
    {
        public string id_dm { get; set; }
        public string tendm { get; set; }

        public List<DanhMuc> danhsachDM { get; set; }

        public DanhMuc()
        {
            // Khởi tạo danh sách khi khởi tạo đối tượng
            danhsachDM = new List<DanhMuc>();
        }

        public void loadDanhMuc()
        {
            string conStr = System.Configuration.ConfigurationManager.ConnectionStrings["ketnoiSQL"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(conStr))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT id_dm, ten_dm FROM DANHMUC", conn))
                {
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        danhsachDM.Clear(); // Đảm bảo danh sách được làm sạch trước khi thêm mới
                        while (dr.Read())
                        {
                            danhsachDM.Add(new DanhMuc
                            {
                                id_dm = dr["ID_DM"].ToString(),
                                tendm = dr["TEN_DM"].ToString()
                            });
                        }
                    }
                }
            }
        }
    }
}