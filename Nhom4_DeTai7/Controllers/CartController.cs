using Nhom4_DeTai7.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Configuration;
using Dapper;
using System.Net;

namespace Nhom4_DeTai7.Controllers
{
    public class CartController : BaseController
    {
        // GET: Cart

        private QL_WEB_BANHNGOTContext db = new QL_WEB_BANHNGOTContext();

        
        
        // GET: Cart
        public ActionResult Cart()
        {
            if (Session["Cart"] == null)
            {
                return RedirectToAction("Login", "User");
            }

            int userId = Convert.ToInt32(Session["Cart"]);

            var cartItems = db.GioHang
                .Where(g => g.ID_ND == userId)
                .Select(g => new CartItemViewModel
                {
                    ID_GH = g.ID_GH,
                    SOLUONG = g.SOLUONG,
                    TenSP = g.SanPham.ten_sp,
                    GiaGoc = g.SanPham.giagoc,
                    GiaKM = g.SanPham.giakm ?? 0,
                    HinhAnh = g.SanPham.hinhanh,
                    TotalPrice = g.SOLUONG * g.SanPham.giagoc,
                    SLTON = g.SanPham.soluong
                })
                .ToList();

            ViewBag.CartItems = cartItems;
            ViewBag.TotalAmount = cartItems.Sum(item => item.TotalPrice);
            
            return View(cartItems);
        }

        // POST: AddToCart
        [HttpPost]
        public JsonResult AddToCart(string productId, int quantity)
        {
            if (Session["Login"] == null)
            {
                return Json(new { success = false, message = "Bạn cần đăng nhập để thêm sản phẩm vào giỏ hàng." });
            }

            int userId = Convert.ToInt32(Session["Cart"]);

            // Lấy số lượng còn lại của sản phẩm từ bảng SANPHAM
            var availableQuantity = db.SanPham
                .Where(p => p.id_sp == productId) // Chú ý tên cột là ID_SP
                .Select(p => p.soluong) // Chú ý tên cột là SOLUONG
                .FirstOrDefault();

            // Tìm kiếm sản phẩm trong giỏ hàng của người dùng
            var existingItem = db.GioHang
                .FirstOrDefault(g => g.ID_SP == productId && g.ID_ND == userId);

            // Tính tổng số lượng cần thêm vào giỏ hàng
            int totalQuantity = existingItem != null ? existingItem.SOLUONG + quantity : quantity;

            // Kiểm tra nếu số lượng cần thêm vào vượt quá số lượng còn lại trong kho
            if (availableQuantity < totalQuantity)
            {
                return Json(new { success = false, message = "Số lượng sản phẩm trong giỏ hàng vượt quá số lượng trong kho." });
            }

            // Nếu sản phẩm đã có trong giỏ hàng, cập nhật số lượng
            if (existingItem != null)
            {
                existingItem.SOLUONG = totalQuantity;
                db.Entry(existingItem).State = EntityState.Modified;
            }
            // Nếu sản phẩm chưa có trong giỏ hàng, thêm mới
            else
            {
                var newItem = new GioHang
                {
                    ID_SP = productId,
                    SOLUONG = quantity,
                    ID_ND = userId
                };
                db.GioHang.Add(newItem);
            }

            // Lưu các thay đổi vào cơ sở dữ liệu
            db.SaveChanges();

            // Trả về kết quả dưới dạng JSON
            return Json(new { success = true });
        }

        [HttpPost]
        public ActionResult UpdateQuantity(int idGH, int qualityyyy)
        {
            if (qualityyyy <= 0)
            {
                // Xử lý lỗi nếu quantity không hợp lệ
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Số lượng không hợp lệ");
            }

            // Kiểm tra và cập nhật số lượng trong cơ sở dữ liệu
            using (var context = new QL_WEB_BANHNGOTContext())
            {
                var itemToUpdate = context.GioHang.SingleOrDefault(g => g.ID_GH == idGH);
                if (itemToUpdate != null)
                {
                    itemToUpdate.SOLUONG = qualityyyy;
                    context.SaveChanges();
                }
            }

            // Sau khi cập nhật thành công, chuyển hướng về trang giỏ hàng
            return RedirectToAction("Cart");
        }

        public ActionResult Delete(int idGH)
        {
            using (var context = new QL_WEB_BANHNGOTContext())
            {
                var itemToRemove = context.GioHang.SingleOrDefault(g => g.ID_GH == idGH);
                if (itemToRemove != null)
                {
                    context.GioHang.Remove(itemToRemove);
                    context.SaveChanges();
                }
            }

            // Sau khi xóa thành công, chuyển hướng về trang giỏ hàng
            return RedirectToAction("Cart");
        }

        
      
        [HttpGet]
        public ActionResult Thanhtoann(string totalAmount, string finalAmount)
        {
            string userName = Session["Login"]?.ToString();
            string sdt = Session["SDT"]?.ToString();
            ViewBag.TotalAmount = totalAmount;
            ViewBag.FinalAmount = finalAmount;
            ViewBag.UserName = userName;
            ViewBag.SDT = sdt;
            ViewBag.Tinh = Session["Tinh"];
            ViewBag.Quan = Session["Quan"];
            ViewBag.Phuong = Session["Phuong"];
            ViewBag.DiaChiView = Session["DiaChi"];

            return View();
        }

        [HttpPost]
        public ActionResult Thanhtoann(string finalAmount, string name, string phone, string paymentMethod)
        {

            string TTMH = "";
            finalAmount = Request.Form["finalAmount"];
            string cleanAmount = finalAmount.Replace(".", "");
            int parsedAmount;
            bool isValid = int.TryParse(cleanAmount, out parsedAmount);
            if (!isValid)
            {
                
                parsedAmount = 0; 
            }
            string userName = Session["Login"]?.ToString();
            int userId = Convert.ToInt32(Session["Cart"]);

            // Tạo ID cho hóa đơn mới
            string billId = Guid.NewGuid().ToString("N").Substring(0, 10); // Hoặc tạo ID theo cách khác nếu cần
            ViewBag.billId = billId;
            // Tạo hóa đơn mới
            var hoaDon = new HoaDon
            {
                ID_HD = billId,
                ID_ND = userId,
                NGAYTT = DateTime.Now,
                PTTT = paymentMethod,
                THANHTIEN = parsedAmount,
                TTMH = "Đã Thanh Toán"
            };

            db.HoaDon.Add(hoaDon);
            db.SaveChanges(); // Lưu hóa đơn vào cơ sở dữ liệu

            // Lấy các sản phẩm được chọn từ giỏ hàng
            var selectedItemIds = Request.Form.GetValues("dummy");
            if (selectedItemIds != null && selectedItemIds.Any())
            {
                foreach (var productId in selectedItemIds)
                {
                    var cartItem = db.GioHang.FirstOrDefault(g => g.ID_SP == productId && g.ID_ND == userId);
                    if (cartItem != null)
                    {
                        var chiTietHoaDon = new CTHOADON
                        {
                            ID_HD = billId,
                            ID_SP = productId,
                            SOLUONG = cartItem.SOLUONG,
                            DONGIA = cartItem.SanPham.giagoc
                        };

                        db.CT_HoaDon.Add(chiTietHoaDon);
                    }
                }
                db.SaveChanges(); // Lưu chi tiết hóa đơn vào cơ sở dữ liệu
            }

            //db.SaveChanges(); // Lưu thay đổi vào cơ sở dữ liệu

            return RedirectToAction("thanhcong");
        }
        public ActionResult thanhcong()
        {
            return View();
        }
    }
}