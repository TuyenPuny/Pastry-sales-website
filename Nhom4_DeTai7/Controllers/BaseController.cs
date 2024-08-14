using Nhom4_DeTai7.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Nhom4_DeTai7.Controllers
{
    public class BaseController : Controller
    {
        private QL_WEB_BANHNGOTContext db = new QL_WEB_BANHNGOTContext();
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

            // Kiểm tra nếu người dùng đã đăng nhập
            if (Session["Cart"] != null)
            {
                int userId = Convert.ToInt32(Session["Cart"]);

                // Lấy số lượng sản phẩm trong giỏ hàng
                var cartCount = db.GioHang
                    .Where(g => g.ID_ND == userId)
                    .Select(g => g.ID_SP) // Chọn ID sản phẩm
                    .Distinct() // Lấy các ID sản phẩm khác nhau
                    .Count(); // Đếm số lượng ID sản phẩm khác nhau

                // Truyền số lượng đến ViewBag
                ViewBag.ProductCount = cartCount;
            }
            else
            {
                ViewBag.ProductCount = 0;
            }
        }
    }
}