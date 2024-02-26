using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class NhanVienDTO
    {
        public int MaNhanVien { get; set; }
        public string TenDangNhap { get; set; }
        public string MatKhau { get; set; }
        public string HoTen { get; set; }
        public string ChucVu { get; set; }
        public decimal Luong { get; set; }

        // Hàm dựng không tham số (constructor)
        public NhanVienDTO()
        {
            // Mặc định, bạn có thể để các giá trị là giá trị mặc định hoặc null
        }

        // Hàm dựng với tham số để khởi tạo các giá trị ban đầu
        public NhanVienDTO(int maNhanVien, string tenDangNhap, string matKhau, string hoTen, string chucVu, decimal luong)
        {
            MaNhanVien = maNhanVien;
            TenDangNhap = tenDangNhap;
            MatKhau = matKhau;
            HoTen = hoTen;
            ChucVu = chucVu;
            Luong = luong;
        }
    }

}
