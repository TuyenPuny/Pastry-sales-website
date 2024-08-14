
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Data.SqlClient;
using Nhom4_DeTai7.Areas.ADMIN.Model;
using System.Data.Entity.Infrastructure;

namespace Nhom4_DeTai7.Areas.ADMIN.Controllers
{
    public class DanhMucController : Controller
    {
        private DBConnect db = new DBConnect();
        // GET: ADMIN/DanhMuc
        public ActionResult QLDanhMuc()
        {
            var listDM = db.DanhMuc.ToList();
            return View(listDM);
        }
        
        // thêm danh mục
        public ActionResult ThemDanhMuc()
        {
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ThemDanhMuc(DANHMUC danhMuc)
        {
            if (ModelState.IsValid)
            {
                // Kiểm tra trùng mã danh mục
                var existingDanhMuc = db.DanhMuc.FirstOrDefault(dm => dm.ID_DM == danhMuc.ID_DM);
                if (existingDanhMuc != null)
                {
                    ModelState.AddModelError("ID_DM", "Mã danh mục đã tồn tại.");
                    return View(danhMuc);
                }

                db.DanhMuc.Add(danhMuc);
                db.SaveChanges();

                return RedirectToAction("QLDanhMuc");
            }

            return View(danhMuc);
        }

        // Sửa danh mục 
        public ActionResult EditCategory(string ID_DM)
        {
            try
            {
                var categoryToEdit = db.DanhMuc.Find(ID_DM);
                if (categoryToEdit != null)
                {
                    return View(categoryToEdit);
                }
                else
                {
                    return RedirectToAction("QLDanhMuc");
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Lỗi khi tải dữ liệu danh mục: {ex.Message}";
                return RedirectToAction("QLDanhMuc");
            }
        }



        [HttpPost]
        public ActionResult EditCategory(DANHMUC category)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // Find the existing category in the database
                    var categoryInDb = db.DanhMuc.Find(category.ID_DM);
                    if (categoryInDb != null)
                    {
                        // Update properties of the existing category
                        categoryInDb.TEN_DM = category.TEN_DM;

                        // Save changes to the database
                        db.SaveChanges();
                        return RedirectToAction("QLDanhMuc");
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "Không tìm thấy danh mục.";
                    }
                }
                else
                {
                    TempData["ErrorMessage"] = "Dữ liệu không hợp lệ.";
                    TempData["ModelStateErrors"] = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                }

                return View(category);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Lỗi khi lưu dữ liệu: {ex.Message}";
                return RedirectToAction("QLDanhMuc");
            }
        }


        // Xóa danh mục
        public ActionResult Delete(string ID_DM)
        {
            try
            {
                var productToDelete = db.DanhMuc.Find(ID_DM);
                if (productToDelete != null)
                {
                    db.DanhMuc.Remove(productToDelete);
                    db.SaveChanges();
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

            return RedirectToAction("QLDanhMuc");
        }

        public ActionResult QLTest()
        {
            return View();
        }

        
    }
}