using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class KhachHangDTO
    {
        public int MaKhachHang { get; set; }
        public string HoTen { get; set; }
        public string Email { get; set; }
        public string SoDienThoai { get; set; }
        public string DiaChi { get; set; }
        public string LoaiKhachHang { get; set; }

        public KhachHangDTO()
        {
        }

        public KhachHangDTO(int maKhachHang, string hoTen, string email, string soDienThoai, string diaChi, string loaiKhachHang)
        {
            MaKhachHang = maKhachHang;
            HoTen = hoTen;
            Email = email;
            SoDienThoai = soDienThoai;
            DiaChi = diaChi;
            LoaiKhachHang = loaiKhachHang;
        }
    }

}
