using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using WEB_GYM_CHINH.Models;

namespace WEB_GYM_CHINH.Controllers
{
    public class ThongKe_BaoCaoController : Controller
    {
        private DBWebGymEntities db = new DBWebGymEntities();

        // GET: ThongKe_BaoCao
        public ActionResult Index(DateTime? fromDate, DateTime? toDate, string filterType = "ngay")
        {
            var model = new ThongKe_BaoCaoViewModel();

            // 1. Xử lý khoảng thời gian lọc dữ liệu
            DateTime batDau = fromDate ?? DateTime.Now.AddDays(-30);
            DateTime ketThuc = toDate ?? DateTime.Now;

            switch (filterType)
            {
                case "thang":
                    batDau = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                    ketThuc = batDau.AddMonths(1).AddDays(-1);
                    break;

                case "quy":
                    int quy = (DateTime.Now.Month - 1) / 3 + 1;
                    batDau = new DateTime(DateTime.Now.Year, (quy - 1) * 3 + 1, 1);
                    ketThuc = batDau.AddMonths(3).AddDays(-1);
                    break;

                case "nam":
                    batDau = new DateTime(DateTime.Now.Year, 1, 1);
                    ketThuc = new DateTime(DateTime.Now.Year, 12, 31);
                    break;
            }

            // 2. Lấy dữ liệu thống kê
            model.SoThanhVien = db.DK_THANHVIEN.Count(m => m.NgayBatDau >= batDau && m.NgayBatDau <= ketThuc);

            model.SoGoiTapDangKy = db.DK_THANHVIEN
                .Where(m => m.NgayBatDau >= batDau && m.NgayBatDau <= ketThuc)
                .Select(m => m.MaGoiTap)
                .Distinct()
                .Count();

            model.SoBuoiTap = db.CHAMCONGs.Count(c => c.NgayTap >= batDau && c.NgayTap <= ketThuc);

            model.TongDoanhThu = db.THANHTOANs
                .Where(t => t.NgayThanhToan >= batDau && t.NgayThanhToan <= ketThuc)
                .Sum(t => (decimal?)t.SoTien) ?? 0;

            model.SoLuongDichVu = db.CHONDICHVUs.Count(); // hoặc lọc theo thời gian nếu có NgayDangKy

            model.SoNhanVien = db.NHANVIENs.Count(); // hoặc lọc theo trạng thái nếu có

            // 3. Ghi chú và thời gian báo cáo
            model.GhiChu = $"Báo cáo thống kê theo {filterType} từ {batDau:dd/MM/yyyy} đến {ketThuc:dd/MM/yyyy}";
            model.NgayBaoCao = DateTime.Now;

            return View(model);
        }


        // Export to Excel
        public ActionResult ExportToCsv()
        {
            var sb = new StringBuilder();
            sb.AppendLine("Tiêu chí,Giá trị");
            sb.AppendLine("Số thành viên,100");
            sb.AppendLine("Số gói tập đã đăng ký,5");
            sb.AppendLine($"Ngày báo cáo,{DateTime.Now:dd/MM/yyyy HH:mm}");

            byte[] fileBytes = Encoding.UTF8.GetBytes(sb.ToString());

            return File(fileBytes, "text/csv", "ThongKeBaoCao.csv");
        }


    }
}