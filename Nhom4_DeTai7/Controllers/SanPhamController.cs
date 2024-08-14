using Nhom4_DeTai7.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Nhom4_DeTai7.Controllers
{
    public class SanPhamController : BaseController
    {
        private QL_WEB_BANHNGOTContext db = new QL_WEB_BANHNGOTContext();
        private readonly SanPham model = new SanPham();

        // GET: SanPham
        public ActionResult SanPham(string ten)
        {
            // Tải dữ liệu nếu chưa được tải
            if (model.danhsachSP == null || !model.danhsachSP.Any())
            {
                model.loadSP();
            }

            // Tải danh mục nếu chưa được tải
            if (ViewData["dm"] == null)
            {
                DanhMuc data2 = new DanhMuc();
                data2.loadDanhMuc();
                ViewData["dm"] = data2;
            }

            if (!string.IsNullOrEmpty(ten))
            {
                model.searchSP(ten);
            }
            else
            {
                model.danhsachSP_ten = model.danhsachSP; // Nếu không có tìm kiếm, hiển thị tất cả sản phẩm
            }

            model.loadSPM_KM(); // Load dữ liệu khuyến mãi và mới ra lò

            return View(model);
        }

        public ActionResult DanhMuc(string id, string ten)
        {
            if (model.danhsachSP == null || !model.danhsachSP.Any())
            {
                model.loadSP_DM(id);
            }

            if (ViewData["dm"] == null)
            {
                DanhMuc data2 = new DanhMuc();
                data2.loadDanhMuc();
                ViewData["dm"] = data2;
            }

            if (!string.IsNullOrEmpty(ten))
            {
                model.searchSP(ten);
            }
            else
            {
                model.danhsachSP_ten = model.danhsachSP; // Nếu không có tìm kiếm, hiển thị tất cả sản phẩm
            }

            model.loadSPM_KM(); // Load dữ liệu khuyến mãi và mới ra lò

            return View(model);
        }

        // GET: KhuyenMai
        public ActionResult KhuyenMai()
        {
            model.loadSPM_KM();
            model.danhsachSP_ten = model.danhsachKhuyenMai;

            // Tải danh mục
            if (ViewData["dm"] == null)
            {
                DanhMuc data2 = new DanhMuc();
                data2.loadDanhMuc();
                ViewData["dm"] = data2;
            }

            return View("SanPham", model);
        }

        // GET: MoiRaLo
        public ActionResult MoiRaLo()
        {
            model.loadSPM_KM();
            model.danhsachSP_ten = model.danhsachMoiRaLo;

            // Tải danh mục
            if (ViewData["dm"] == null)
            {
                DanhMuc data2 = new DanhMuc();
                data2.loadDanhMuc();
                ViewData["dm"] = data2;
            }

            return View("SanPham", model);
        }

        public ActionResult chiTietSanPham(string id)
        {
            SanPham model = new SanPham();
            model.CT_SanPham(id);

            if (model.chitietSanPham != null && model.chitietSanPham.Count > 0)
            {
                model = model.chitietSanPham.First();
            }
            else
            {
                return HttpNotFound();
            }

            return View("chiTietSanPham", model);
        }


    }
}
