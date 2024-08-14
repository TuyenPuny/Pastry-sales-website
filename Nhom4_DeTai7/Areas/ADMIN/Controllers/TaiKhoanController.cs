using Nhom4_DeTai7.Areas.ADMIN.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Nhom4_DeTai7.Areas.ADMIN.Controllers
{
    public class TaiKhoanController : Controller
    {
        private DBConnect db = new DBConnect();
        // GET: ADMIN/TaiKhoan
        public ActionResult ListACC()
        {
            var listTK = db.taikhoan.ToList();
            return View(listTK);
        }

        // Thêm tài khoản 
        public ActionResult AddACC()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddACC(NGUOIDUNG nguoiDung)
        {
            if (ModelState.IsValid)
            {
                // Kiểm tra xem có bất kỳ trường nào còn trống không
                if (string.IsNullOrEmpty(nguoiDung.TEN_ND) ||
                    string.IsNullOrEmpty(nguoiDung.EMAIL) ||
                    string.IsNullOrEmpty(nguoiDung.TAIKHOAN) ||
                    string.IsNullOrEmpty(nguoiDung.MATKHAU))
                {
                    ModelState.AddModelError("", "Thông tin chưa đầy đủ. Vui lòng kiểm tra lại.");
                    return View(nguoiDung);
                }

                // Kiểm tra xem tài khoản, email hoặc số điện thoại có bị trùng không
                var existingUser = db.taikhoan
                    .Where(u => u.ID_ND != nguoiDung.ID_ND)
                    .Where(u => u.TAIKHOAN == nguoiDung.TAIKHOAN || u.EMAIL == nguoiDung.EMAIL || u.SDT == nguoiDung.SDT)
                    .FirstOrDefault();

                if (existingUser != null)
                {
                    ModelState.AddModelError("", "Tài khoản, email hoặc số điện thoại đã tồn tại. Vui lòng kiểm tra lại.");
                    return View(nguoiDung);
                }

                try
                {
                    // Mã hóa mật khẩu
                    nguoiDung.MATKHAU = EncryptPassword(nguoiDung.MATKHAU);

                    db.taikhoan.Add(nguoiDung);
                    db.SaveChanges();
                    return RedirectToAction("ListACC");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Có lỗi xảy ra: " + ex.Message);
                }
            }

            return View(nguoiDung);
        }
        private string EncryptPassword(string password)
        {
            using (MD5 md5 = MD5.Create())
            {
                byte[] inputBytes = Encoding.ASCII.GetBytes(password);
                byte[] hashBytes = md5.ComputeHash(inputBytes);
                return BitConverter.ToString(hashBytes).Replace("-", "").ToLowerInvariant();
            }
        }

        // Sửa thông tin 
        public ActionResult Edit(int ID_ND)
        {
            var nguoiDung = db.taikhoan.Find(ID_ND);
            if (nguoiDung == null)
            {
                return HttpNotFound();
            }
            return View(nguoiDung);
        }

        // POST: ADMIN/NguoiDung/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(NGUOIDUNG nguoiDung)
        {
            if (ModelState.IsValid)
            {
                // Kiểm tra xem có bất kỳ trường nào còn trống không
                if (string.IsNullOrEmpty(nguoiDung.TEN_ND) ||
                    string.IsNullOrEmpty(nguoiDung.EMAIL) ||
                    string.IsNullOrEmpty(nguoiDung.TAIKHOAN) ||
                    (string.IsNullOrEmpty(nguoiDung.MATKHAU) && nguoiDung.ID_ND != 0)) // Nếu mật khẩu không có và không phải là tài khoản mới
                {
                    ModelState.AddModelError("", "Thông tin chưa đầy đủ. Vui lòng kiểm tra lại.");
                    return View(nguoiDung);
                }

                // Kiểm tra xem tài khoản, email hoặc số điện thoại có bị trùng không
                var existingUser = db.taikhoan
                    .Where(u => u.ID_ND != nguoiDung.ID_ND)
                    .Where(u => u.TAIKHOAN == nguoiDung.TAIKHOAN || u.EMAIL == nguoiDung.EMAIL || u.SDT == nguoiDung.SDT)
                    .FirstOrDefault();

                if (existingUser != null)
                {
                    ModelState.AddModelError("", "Tài khoản, email hoặc số điện thoại đã tồn tại. Vui lòng kiểm tra lại.");
                    return View(nguoiDung);
                }

                try
                {
                    var userToUpdate = db.taikhoan.Find(nguoiDung.ID_ND);
                    if (userToUpdate != null)
                    {
                        // Cập nhật thông tin người dùng
                        userToUpdate.TEN_ND = nguoiDung.TEN_ND;
                        userToUpdate.EMAIL = nguoiDung.EMAIL;
                        userToUpdate.TAIKHOAN = nguoiDung.TAIKHOAN;

                        // Nếu mật khẩu được nhập, mã hóa và cập nhật
                        if (!string.IsNullOrEmpty(nguoiDung.MATKHAU))
                        {
                            userToUpdate.MATKHAU = EncryptPassword(nguoiDung.MATKHAU);
                        }

                        // Cập nhật các thông tin khác nếu cần
                        // userToUpdate.[Property] = nguoiDung.[Property];

                        db.Entry(userToUpdate).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
                        return RedirectToAction("ListACC");
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Có lỗi xảy ra: " + ex.Message);
                }
            }

            return View(nguoiDung);
        }

        // XÓA TÀI KHOẢN

        [HttpPost]
        public ActionResult DeleteSelected(int[] selectedIds)
        {
            if (selectedIds == null || selectedIds.Length == 0)
            {
                ModelState.AddModelError("", "Bạn chưa chọn tài khoản nào để xóa.");
                return RedirectToAction("ListACC");
            }

            foreach (var id in selectedIds)
            {
                var user = db.taikhoan.Find(id);
                if (user != null)
                {
                    // Kiểm tra vai trò của người dùng
                    if (user.VAITRO != "Admin")
                    {
                        db.taikhoan.Remove(user);
                    }
                    else
                    {
                        ModelState.AddModelError("", $"Tài khoản có ID {id} không thể xóa vì có vai trò là Admin.");
                    }
                }
            }

            db.SaveChanges();
            return RedirectToAction("ListACC");
        }
    }
}
