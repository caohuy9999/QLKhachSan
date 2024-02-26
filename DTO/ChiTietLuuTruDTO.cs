using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class ChiTietLuuTruDTO
    {
        public int MaLuuTru { get; set; }
        public int MaDatPhong { get; set; }
        public int SoLuongNguoi { get; set; }
        public DateTime ThoiGianVaoPhong { get; set; }
        public DateTime ThoiGianDuKienTraPhong { get; set; }
        public DateTime? ThoiGianThucTeTraPhong { get; set; }
        public string TinhTrang { get; set; }


        public ChiTietLuuTruDTO()
        {
            
        }


        public ChiTietLuuTruDTO(int maLuuTru, int maDatPhong, int soLuongNguoi, DateTime thoiGianVaoPhong, DateTime thoiGianDuKienTraPhong, DateTime? thoiGianThucTeTraPhong, string tinhTrang)
        {
            MaLuuTru = maLuuTru;
            MaDatPhong = maDatPhong;
            SoLuongNguoi = soLuongNguoi;
            ThoiGianVaoPhong = thoiGianVaoPhong;
            ThoiGianDuKienTraPhong = thoiGianDuKienTraPhong;
            ThoiGianThucTeTraPhong = thoiGianThucTeTraPhong;
            TinhTrang = tinhTrang;
        }

        // Bổ sung constructor và phương thức khác nếu cần thiết
    }

}
