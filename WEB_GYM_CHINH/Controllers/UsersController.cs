using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WEB_GYM_CHINH.Models;

namespace WEB_GYM_CHINH.Controllers
{
    public class UsersController : Controller
    {
        private DBWebGymEntities db = new DBWebGymEntities();

        [HttpGet]
        public ActionResult DangKy()
        {
            return View();
        }

        // POST: User/DangKy
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DangKy(THANHVIEN thanhVien, string ConfirmPassword)
        {
            if (ModelState.IsValid)
            {
                // Kiểm tra các trường bắt buộc
                if (string.IsNullOrEmpty(thanhVien.HoTen))
                    ModelState.AddModelError(string.Empty, "Họ và tên không được để trống");
                if (string.IsNullOrEmpty(thanhVien.Email))
                    ModelState.AddModelError(string.Empty, "Email không được để trống");
                if (string.IsNullOrEmpty(thanhVien.SDT))
                    ModelState.AddModelError(string.Empty, "Số điện thoại không được để trống");
                if (string.IsNullOrEmpty(thanhVien.MatKhau))
                    ModelState.AddModelError(string.Empty, "Mật khẩu không được để trống");

                // Kiểm tra mật khẩu xác nhận
                if (thanhVien.MatKhau != ConfirmPassword)
                    ModelState.AddModelError(string.Empty, "Mật khẩu xác nhận không khớp");

                // Kiểm tra email đã tồn tại
                if (db.THANHVIENs.Any(t => t.Email == thanhVien.Email))
                    ModelState.AddModelError(string.Empty, "Email đã được sử dụng");

                // Kiểm tra số điện thoại đã tồn tại
                if (db.THANHVIENs.Any(t => t.SDT == thanhVien.SDT))
                    ModelState.AddModelError(string.Empty, "Số điện thoại đã được sử dụng");

                if (ModelState.IsValid)
                {
                    // Đặt các giá trị mặc định
                    thanhVien.NgayDangKy = DateTime.Now;
                    thanhVien.TrangThai = "Hoạt động";

                    // Lưu vào CSDL
                    db.THANHVIENs.Add(thanhVien);
                    db.SaveChanges();
                }
            }
            else
            {
                return View();
            }

            // Nếu có lỗi, truyền lại danh sách gói tập và dữ liệu đã nhập
            return RedirectToAction("DangNhap");
        }

        // GET: User/DangNhap
        [HttpGet]
        public ActionResult DangNhap()
        {
            return View();
        }

        // POST: User/DangNhap
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DangNhap(string Email, string MatKhau)
        {
            if (string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(MatKhau))
            {
                ViewBag.ErrorMessage = "Vui lòng nhập đầy đủ Email và Mật khẩu.";
                return View();
            }

            // Tìm thành viên theo Email và kiểm tra mật khẩu
            var thanhVien = db.THANHVIENs.FirstOrDefault(t => t.Email == Email && t.MatKhau == MatKhau);
            if (thanhVien != null)
            {
                // Lưu thông tin đăng nhập vào Session (giả sử sử dụng Session)
                Session["MaThanhVien"] = thanhVien.MaThanhVien;
                Session["HoTen"] = thanhVien.HoTen;

                // Chuyển hướng đến trang chủ hoặc trang thành viên
                return RedirectToAction("TrangChu", "Home");
            }
            else
            {
                ViewBag.ErrorMessage = "Email hoặc mật khẩu không đúng.";
                return View();
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

    }
}