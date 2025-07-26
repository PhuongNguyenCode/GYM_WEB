using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WEB_GYM_CHINH.Models;
using System.Data.Entity;
namespace WEB_GYM_CHINH.Controllers
{
    public class ChamCongController : Controller
    {
        DBWebGymEntities db = new DBWebGymEntities();

        // GET: ChamCong/Create
        public ActionResult Create()
        {
            var listDangKy = (from dk in db.DK_THANHVIEN
                              join gt in db.GOITAPs on dk.MaGoiTap equals gt.MaGoiTap
                              select new
                              {
                                  dk.MaDKTV,
                                  HienThi = dk.HoTen + " - " + gt.TenGoiTap,
                                  dk.NgayBatDau,
                                  dk.NgayKetThuc
                              }).ToList();

            ViewBag.MaDKTV = new SelectList(listDangKy, "MaDKTV", "HienThi");

            // Dữ liệu này sẽ dùng cho JavaScript để set min/max theo gói tập tương ứng
            ViewBag.MaDKTVData = listDangKy.Select(dk => new
            {
                dk.MaDKTV,
                NgayBatDau = dk.NgayBatDau.ToString("yyyy-MM-dd"),
                NgayKetThuc = dk.NgayKetThuc?.ToString("yyyy-MM-dd")
            });

            // Mặc định nếu chưa chọn gói nào thì giới hạn 30 ngày từ hôm nay
            ViewBag.NgayBatDau = DateTime.Today.ToString("yyyy-MM-dd");
            ViewBag.NgayKetThuc = DateTime.Today.AddDays(30).ToString("yyyy-MM-dd");

            return View();
        }



        // POST: ChamCong/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CHAMCONG chamCong)
        {
            if (ModelState.IsValid)
            {
                // Tìm bản ghi đăng ký tập tương ứng
                var dk = db.DK_THANHVIEN.Find(chamCong.MaDKTV);
                if (dk == null)
                {
                    ModelState.AddModelError("", "Không tìm thấy thông tin đăng ký.");
                }
                else if (chamCong.NgayTap < dk.NgayBatDau || chamCong.NgayTap > dk.NgayKetThuc)
                {
                    ModelState.AddModelError("", "Ngày chấm công không nằm trong thời gian gói.");
                }
                else
                {
                    // Kiểm tra đã chấm công ngày này chưa
                    bool isExists = db.CHAMCONGs.Any(c => c.MaDKTV == chamCong.MaDKTV && c.NgayTap == chamCong.NgayTap);
                    if (isExists)
                    {
                        ModelState.AddModelError("", "Đã chấm công ngày này.");
                    }
                    else
                    {
                        db.CHAMCONGs.Add(chamCong);

                        try
                        {
                            db.SaveChanges();

                            // Tính số buổi còn lại
                            int soBuoiDaCham = db.CHAMCONGs.Count(c => c.MaDKTV == chamCong.MaDKTV);
                            int soConLai = (dk.SoBuoi ?? 0) - soBuoiDaCham;

                            ViewBag.Message = "Chấm công thành công. Số buổi còn lại: " + soConLai;
                        }
                        catch (Exception ex)
                        {
                            ModelState.AddModelError("", "Lỗi khi lưu dữ liệu: " + ex.ToString());
                        }
                    }
                }
            }

            // Hiển thị danh sách gói tập để chọn
            var dkThanhVienList = db.DK_THANHVIEN
    .ToList() // Lấy dữ liệu về trước, sau đó xử lý trên RAM
    .Select(d => new
    {
        d.MaDKTV,
        ThongTin = d.HoTen + " - Gói: " + d.GOITAP.TenGoiTap + " (" +
                   ((DateTime)d.NgayBatDau).ToString("dd/MM/yyyy") + " - " +
                   ((DateTime)d.NgayKetThuc).ToString("dd/MM/yyyy") + ")"
    }).ToList();


            ViewBag.MaDKTV = new SelectList(dkThanhVienList, "MaDKTV", "ThongTin", chamCong.MaDKTV);
            return View(chamCong);
        }

        // GET: Chấm công cho 1 gói cụ thể
        public ActionResult Index(int? maChiTiet)
        {
            if (maChiTiet == null)
            {
                // Gợi ý chọn chi tiết đầu tiên có trong hệ thống
                var first = db.CT_DANGKYGOI.FirstOrDefault();
                if (first == null)
                    return Content("Chưa có chi tiết đăng ký nào.");

                return RedirectToAction("Index", new { maChiTiet = first.MaChiTiet });
            }

            var chiTiet = db.CT_DANGKYGOI.FirstOrDefault(c => c.MaChiTiet == maChiTiet);
            if (chiTiet == null)
                return HttpNotFound("Không tìm thấy chi tiết đăng ký");

            var ngayDaCham = db.CHAMCONGs
                               .Where(c => c.MaChiTiet == maChiTiet)
                               .OrderByDescending(c => c.NgayTap)
                               .Select(c => c.NgayTap)
                               .ToList();

            var soBuoiDaTap = ngayDaCham.Count;
            var soBuoiConLai = chiTiet.SoBuoi - soBuoiDaTap;

            ViewBag.NgayDaCham = ngayDaCham;
            ViewBag.SoBuoiConLai = soBuoiConLai;
            ViewBag.MaChiTiet = maChiTiet;

            return View();
        }

        // POST: Chấm công ngày hôm nay
        [HttpPost]
        public ActionResult ChamCongNgay(int maChiTiet, DateTime ngayTap)
        {
            var chiTiet = db.CT_DANGKYGOI.FirstOrDefault(c => c.MaChiTiet == maChiTiet);
            if (chiTiet == null)
                return HttpNotFound("Không tìm thấy chi tiết đăng ký");

            // Kiểm tra số buổi đã tập
            int soBuoiDaCham = db.CHAMCONGs.Count(c => c.MaChiTiet == maChiTiet);
            if (soBuoiDaCham >= chiTiet.SoBuoi)
            {
                TempData["Error"] = "Đã đủ số buổi tập, không thể chấm thêm!";
                return RedirectToAction("Index", new { maChiTiet });
            }

            // Kiểm tra nếu ngày đã chấm rồi
            bool daCham = db.CHAMCONGs.Any(c => c.MaChiTiet == maChiTiet && c.NgayTap == ngayTap);
            if (daCham)
            {
                TempData["Warning"] = "Ngày này đã được chấm công!";
            }
            else
            {
                db.CHAMCONGs.Add(new CHAMCONG
                {
                    MaChiTiet = maChiTiet,
                    NgayTap = ngayTap
                });
                db.SaveChanges();
                TempData["Success"] = $"Đã chấm công cho ngày {ngayTap:dd/MM/yyyy}!";
            }

            return RedirectToAction("Index", new { maChiTiet });
        }

    }
}