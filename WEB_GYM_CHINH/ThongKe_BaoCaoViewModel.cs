using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WEB_GYM_CHINH.Models
{
    public class ThongKe_BaoCaoViewModel
    {
        public int SoThanhVien { get; set; } // Tổng số thành viên
        public int SoGoiTapDangKy { get; set; } // Tổng số gói tập
        public int SoBuoiTap { get; set; } // Tổng số buổi tập
        public decimal TongDoanhThu { get; set; } // Tổng doanh thu
        public int SoLuongDichVu { get; set; } // Tổng số dịch vụ
        public int SoNhanVien { get; set; } // Tổng số nhân viên
        public string GhiChu { get; set; } // Ghi chú
        public DateTime NgayBaoCao { get; set; } // Ngày báo cáo
    }


}