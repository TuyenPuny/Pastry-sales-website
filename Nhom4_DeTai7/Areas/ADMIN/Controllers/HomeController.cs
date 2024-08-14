
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Data.SqlClient;
using Nhom4_DeTai7.Areas.ADMIN.Model;
using System.Data.Entity.Infrastructure;
using System.Diagnostics;

namespace Nhom4_DeTai7.Areas.ADMIN.Controllers
{
    public class HomeController : Controller
    {
        private DBConnect db = new DBConnect();
        // GET: ADMIN/Home
        public ActionResult Index()
        {
            var listPro = db.SanPham.ToList();
            return View(listPro);
        }
        // chi tiết sản phẩm 
        public ActionResult Details(string ID_SP)
        {

            var product = db.SanPham.FirstOrDefault(p => p.ID_SP == ID_SP);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }
        // Thêm sản phẩm 
        public ActionResult CreateSP()
        {
            ViewBag.ID_DM = new SelectList(db.DanhMuc, "ID_DM", "TEN_DM");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SANPHAM sanpham)
        {
            Debug.WriteLine("Action Create called");
            if (ModelState.IsValid)
            {
                // Kiểm tra mã sản phẩm có bị trùng hay không
                var existingProduct = db.SanPham.FirstOrDefault(sp => sp.ID_SP == sanpham.ID_SP);
                if (existingProduct != null)
                {
                    ModelState.AddModelError("ID_SP", "Mã sản phẩm đã tồn tại.");
                    ViewBag.ID_DM = new SelectList(db.DanhMuc, "ID_DM", "TEN_DM", sanpham.ID_DM);
                    return View(sanpham);
                }

                // Thêm sản phẩm vào cơ sở dữ liệu
                try
                {
                    db.SanPham.Add(sanpham);
                    db.SaveChanges();
                    TempData["SuccessMessage"] = "Đã thêm sản phẩm thành công.";
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Thêm sản phẩm không thành công. Vui lòng thử lại sau.");
                    ViewBag.ID_DM = new SelectList(db.DanhMuc, "ID_DM", "TEN_DM", sanpham.ID_DM);
                    return View(sanpham);
                }
            }

            ViewBag.ID_DM = new SelectList(db.DanhMuc, "ID_DM", "TEN_DM", sanpham.ID_DM);
            return View(sanpham);
        }

        // SỬA SẢN PHẨM
        public ActionResult EditSP(string ID_SP)
        {
            try
            {
                var categoryToEdit = db.SanPham.Find(ID_SP);
                if (categoryToEdit != null)
                {
                    return View(categoryToEdit);
                }
                else
                {
                    TempData["ErrorMessage"] = "Không tìm thấy danh mục.";
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Lỗi khi tải dữ liệu danh mục: {ex.Message}";
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditSP(SANPHAM sanpham)
        {
            if (ModelState.IsValid)
            {
                // Kiểm tra mã sản phẩm có tồn tại hay không
                var existingProduct = db.SanPham.FirstOrDefault(sp => sp.ID_SP == sanpham.ID_SP);
                if (existingProduct == null)
                {
                    ModelState.AddModelError("ID_SP", "Mã sản phẩm không tồn tại.");
                    ViewBag.ID_DM = new SelectList(db.DanhMuc, "ID_DM", "TEN_DM", sanpham.ID_DM);
                    return View(sanpham);
                }

                // Cập nhật thông tin sản phẩm
                existingProduct.TEN_SP = sanpham.TEN_SP;
                existingProduct.GIAGOC = sanpham.GIAGOC;
                existingProduct.GIAKM = sanpham.GIAKM;
                existingProduct.KICHTHUOC = sanpham.KICHTHUOC;
                existingProduct.HINHANH = sanpham.HINHANH;
                existingProduct.TINHTRANG = sanpham.TINHTRANG;
                existingProduct.SOLUONG = sanpham.SOLUONG;
                existingProduct.HSD = sanpham.HSD;
                existingProduct.THANHPHAN = sanpham.THANHPHAN;
                existingProduct.MOTA = sanpham.MOTA;
                existingProduct.ID_DM = sanpham.ID_DM;

                try
                {
                    db.SaveChanges();
                    TempData["SuccessMessage"] = "Đã cập nhật sản phẩm thành công.";
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Cập nhật sản phẩm không thành công. Vui lòng thử lại sau.");
                    ViewBag.ID_DM = new SelectList(db.DanhMuc, "ID_DM", "TEN_DM", sanpham.ID_DM);
                    return View(sanpham);
                }
            }

            ViewBag.ID_DM = new SelectList(db.DanhMuc, "ID_DM", "TEN_DM", sanpham.ID_DM);
            return View(sanpham);
        }

        // XÓA SẢN PHẨM

        public ActionResult Delete(string ID_SP)
        {
            try
            {
                var productToDelete = db.SanPham.Find(ID_SP);
                if (productToDelete != null)
                {
                    db.SanPham.Remove(productToDelete);
                    db.SaveChanges();
                    TempData["SuccessMessage"] = "Xóa sản phẩm thành công.";
                }
                else
                {
                    TempData["ErrorMessage"] = "Không tìm thấy sản phẩm.";
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Lỗi khi xóa sản phẩm: {ex.Message}";
            }

            return RedirectToAction("Index");
        }
    }
}
