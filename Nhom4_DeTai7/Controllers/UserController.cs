using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Facebook;
using Google.Apis.Auth;
using Nhom4_DeTai7.Models;

namespace Nhom4_DeTai7.Controllers
{
    public class UserController : BaseController
    {
        // GET: User
        public ActionResult Login()
        {
            SqlConnection conn;
            var u = Request.Form["Username"];
            var p = Request.Form["pass"];
            if (u == null) return View();
            string p2 = BitConverter.ToString(MD5.Create().ComputeHash(Encoding.ASCII.GetBytes(p))).Replace("-", "");
            string connStr = System.Configuration.ConfigurationManager.ConnectionStrings["ketnoiSQL"].ConnectionString;
            conn = new SqlConnection(connStr);
            conn.Open();
            if (conn == null)
            {
                return Content("No connect");
            }
            String sql = "select * from NGUOIDUNG where TAIKHOAN = '" + u + "' and MATKHAU = '" + p2 + "'";
            SqlCommand comm = new SqlCommand(sql, conn);
            SqlDataReader data = comm.ExecuteReader();
            if (data.HasRows)
            {
                data.Read();
                Session["Login"] = data.GetValue(1).ToString();
                Session["SDT"] = data.GetValue(5).ToString();
                Session["Cart"] = data.GetValue(0).ToString();
                Session["Role"] = data.GetValue(8).ToString();
                if (Session["Role"].ToString() == "Admin")
                {
                    return RedirectToAction("Home", "ADMIN");
                }
                return RedirectToAction("Index", "Home");

            }
            else if (u != null && p != null)
            {
                ViewBag.Message = "Đăng nhập thất bại, vui lòng kiểm tra lại thông tin.";
                return View();
            }
            else
            {
                return View();
            }
            return View();
        }

        public ActionResult Register()
        {
            SqlConnection conn;
            var u = Request.Form["Username"];
            var e = Request.Form["Email"];
            var p = Request.Form["pass"];
            var np = Request.Form["pass2"];
            if (p != np)
            {
                ViewBag.Message = "Mật khẩu xác nhận không trùng khớp.";
                return View();
            }
            if (u == "" || u == null || e == "" || e == null || p == "" || p == null)
            {
                ViewBag.Message = "Vui lòng nhập đầy đủ thông tin";
                return View();
            }
            string p3 = BitConverter.ToString(MD5.Create().ComputeHash(Encoding.ASCII.GetBytes(p))).Replace("-", "");
            string connStr = System.Configuration.ConfigurationManager.ConnectionStrings["ketnoiSQL"].ConnectionString;
            conn = new SqlConnection(connStr);
            conn.Open();
            if (conn == null)
            {
                return Content("No connect");
            }
            //Kiểm tra username trùng
            string checkUserSql = "select * from NGUOIDUNG where TAIKHOAN = @Username";
            SqlCommand checkUserCmd = new SqlCommand(checkUserSql, conn);
            checkUserCmd.Parameters.AddWithValue("@Username", u);

            SqlDataReader data = checkUserCmd.ExecuteReader();
            if (data.HasRows)
            {

                ViewBag.Message = "Username đã tồn tại";
                return View();
            }
            data.Close();

            string query1 = "INSERT INTO NGUOIDUNG (TEN_ND, NGAYSINH, GIOITINH, EMAIL, SDT, TAIKHOAN, MATKHAU, VAITRO) VALUES ( NULL, NULL, NULL, '" + e + "', NULL, '" + u + "', '" + p3 + "', N'User')";
            SqlCommand insertCmd = new SqlCommand(query1, conn);
            int rowsAffected = insertCmd.ExecuteNonQuery();
            if (rowsAffected > 0)
            {
                // Đăng ký thành công
                return RedirectToAction("Login", "User");
            }
            else
            {
                // Đăng ký thất bại
                ViewBag.Message = "Đăng ký thất bại";
                return View();
            }
            return View();
        }

        [HttpPost]
        public ActionResult Logout()
        {
            Session["Login"] = null;
            return RedirectToAction("Index", "Home");
        }


        public ActionResult Info()
        {
            var username = Session["Cart"] as string;
            var DiaChi = Request.Form["diaChiFull"];

            if (string.IsNullOrEmpty(username))
            {
                return RedirectToAction("Login", "User");
            }

            string connStr = System.Configuration.ConfigurationManager.ConnectionStrings["ketnoiSQL"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();

                string sql = "SELECT * FROM NGUOIDUNG WHERE ID_ND = @Username";
                SqlCommand comm = new SqlCommand(sql, conn);
                comm.Parameters.AddWithValue("@Username", username);
                SqlDataReader data = comm.ExecuteReader();

                if (data.Read())
                {
                    var user = new NguoiDung
                    {
                        ID_ND = Convert.ToInt32(data["ID_ND"]),
                        TEN_ND = data["TEN_ND"].ToString(),
                        NGAYSINH = data["NGAYSINH"] != DBNull.Value ? (DateTime?)Convert.ToDateTime(data["NGAYSINH"]) : null,
                        GIOITINH = data["GIOITINH"].ToString(),
                        EMAIL = data["EMAIL"].ToString(),
                        SDT = data["SDT"].ToString(),
                        TAIKHOAN = data["TAIKHOAN"].ToString(),
                        MATKHAU = data["MATKHAU"].ToString(),
                        VAITRO = data["VAITRO"].ToString(),
                        
                    };
                    ViewBag.NS = user.NGAYSINH.HasValue ? user.NGAYSINH.Value.ToString("yyyy-MM-dd"):"";
                    var diaChiFull = data["DIA_CHI"].ToString();
                    var diaChiParts = diaChiFull.Split(new[] { ',' }, 4);

                    ViewBag.Tinh = diaChiParts.Length > 0 ? diaChiParts[0].Trim() : "";
                    ViewBag.Quan = diaChiParts.Length > 1 ? diaChiParts[1].Trim() : "";
                    ViewBag.Phuong = diaChiParts.Length > 2 ? diaChiParts[2].Trim() : "";
                    user.DIA_CHI = diaChiParts.Length > 3 ? diaChiParts[3].Trim() : "";
                    string diachiview = user.DIA_CHI;
                    ViewBag.diachiview = diachiview;
                    Session["Tinh"] = ViewBag.Tinh;
                    Session["Quan"] = ViewBag.Quan;
                    Session["Phuong"] = ViewBag.Phuong;
                    Session["DiaChi"] = ViewBag.diachiview;
                    return View(user);
                }
                else
                {
                    return RedirectToAction("Login", "User");
                }
            }
        }

        [HttpPost]
        public ActionResult Update(NguoiDung model)
        {
            var username = Session["Cart"] as string;
            var DiaChi = Request.Form["diaChiFull"];

            if (string.IsNullOrEmpty(username))
            {
                return RedirectToAction("Login", "User");
            }
            var passw = Request.Form["MATKHAU"];
            string passw3 = BitConverter.ToString(MD5.Create().ComputeHash(Encoding.ASCII.GetBytes(passw))).Replace("-", "");
            string connStr = System.Configuration.ConfigurationManager.ConnectionStrings["ketnoiSQL"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();

                string sql = "UPDATE NGUOIDUNG SET TEN_ND = @TenNd, EMAIL = @Email, SDT = @Sdt,  NGAYSINH = @NgaySinh, GIOITINH = @GioiTinh, MATKHAU = '" + passw3 + "',DIA_CHI =@DIACHI WHERE ID_ND = @Username";
                SqlCommand comm = new SqlCommand(sql, conn);
                comm.Parameters.AddWithValue("@TenNd", model.TEN_ND);
                comm.Parameters.AddWithValue("@Email", model.EMAIL);
                comm.Parameters.AddWithValue("@Sdt", model.SDT ?? (object)DBNull.Value);
                comm.Parameters.AddWithValue("@NgaySinh", model.NGAYSINH.HasValue ? (object)model.NGAYSINH.Value : DBNull.Value);
                comm.Parameters.AddWithValue("@GioiTinh", model.GIOITINH);
                comm.Parameters.AddWithValue("@Username", username);
                comm.Parameters.AddWithValue("@DIACHI", DiaChi);
                int rowsAffected = comm.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    ViewBag.Message = "Cập nhật thành công! Vui lòng đăng nhập lại!!!";
                    return View("Login", model);
                }
                else
                {
                    ViewBag.Message = "Cập nhật thất bại!";
                }

                return View("Info", model);
            }
        }
        [HttpPost]
        public async Task<ActionResult> GoogleLogin(string idtoken)
        {
            try
            {
                var payload = await GoogleJsonWebSignature.ValidateAsync(idtoken);
                var email = payload.Email;
                var name = payload.Name;

                Session["Login"] = name;
                Session["Email"] = email;

                // Trả về JSON để client biết đăng nhập thành công
                return Json(new { success = true, message = "Đăng nhập thành công!" });
            }
            catch (Exception ex)
            {
                // Trả về JSON để client biết có lỗi xảy ra
                return Json(new { success = false, message = "Lỗi đăng nhập!" });
            }
        }

        private readonly string _appId = "1498459930772142"; // Your Facebook App ID
        private readonly string _appSecret = "YOUR_FACEBOOK_APP_SECRET"; // Your Facebook App Secret

        [HttpPost]
        public async Task<ActionResult> FacebookLogin(string accessToken)
        {
            try
            {
                var client = new FacebookClient(accessToken);
                dynamic result = await client.GetTaskAsync("me?fields=name,email");

                var email = result.email;
                var name = result.name;

                // Lưu thông tin người dùng vào cơ sở dữ liệu hoặc xác thực
                Session["Login"] = name;
                Session["Email"] = email;

                // Trả về JSON để client biết đăng nhập thành công
                return Json(new { success = true, message = "Đăng nhập thành công!" });
            }
            catch (Exception ex)
            {
                // Trả về JSON để client biết có lỗi xảy ra
                return Json(new { success = false, message = "Lỗi đăng nhập!" });
            }
        }
    }
}