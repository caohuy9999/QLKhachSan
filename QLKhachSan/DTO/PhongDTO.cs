using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class PhongDTO
    {
        public int MaPhong { get; set; }
        public string SoPhong { get; set; }
        public int MaLoaiPhong { get; set; }
        public string TinhTrang { get; set; }

        public PhongDTO()
        {
            // Hàm khởi tạo mặc định
        }

        public PhongDTO(int maPhong, string soPhong, int maLoaiPhong, string tinhTrang)
        {
            MaPhong = maPhong;
            SoPhong = soPhong;
            MaLoaiPhong = maLoaiPhong;
            TinhTrang = tinhTrang;
        }
    }
}
