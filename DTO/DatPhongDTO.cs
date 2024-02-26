using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class DatPhongDTO
    {
        public int MaDatPhong { get; set; }
        public int MaKhachHang { get; set; }
        public int MaPhong { get; set; }
        public DateTime NgayNhanPhong { get; set; }
        public DateTime NgayTraPhong { get; set; }
        public string TinhTrang { get; set; }

        public DatPhongDTO()
        {
            // Hàm khởi tạo mặc định
        }

        public DatPhongDTO(int maKhachHang, int maPhong, DateTime ngayNhanPhong, DateTime ngayTraPhong, string tinhTrang)
        {
            MaKhachHang = maKhachHang;
            MaPhong = maPhong;
            NgayNhanPhong = ngayNhanPhong;
            NgayTraPhong = ngayTraPhong;
            TinhTrang = tinhTrang;
        }
    }
}
