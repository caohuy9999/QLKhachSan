using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class HoaDonDTO
    {
        public int MaHoaDon { get; set; }
        public int MaDatPhong { get; set; }
        public int MaKhachHang { get; set; }
        public int MaNhanVien { get; set; }
        public DateTime NgayLapHoaDon { get; set; }
        public decimal TongTienPhong { get; set; }
        public decimal TongTienDichVu { get; set; }
        public decimal TongTien { get; set; }
        public string TinhTrang { get; set; }
        public bool DaThanhToan { get; set; }

        // Constructor với tham số
        public HoaDonDTO(int maDatPhong, int maKhachHang, int maNhanVien, DateTime ngayLapHoaDon, decimal tongTienPhong, decimal tongTienDichVu, decimal tongTien, string tinhTrang, bool daThanhToan)
        {
            MaDatPhong = maDatPhong;
            MaKhachHang = maKhachHang;
            MaNhanVien = maNhanVien;
            NgayLapHoaDon = ngayLapHoaDon;
            TongTienPhong = tongTienPhong;
            TongTienDichVu = tongTienDichVu;
            TongTien = tongTien;
            TinhTrang = tinhTrang;
            DaThanhToan = daThanhToan;
        }

        // Constructor mặc định
        public HoaDonDTO()
        {
            // Constructor mặc định không có tham số
        }
    }

}
