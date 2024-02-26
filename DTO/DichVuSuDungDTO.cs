using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class DichVuSuDungDTO
    {
        public int MaSuDung { get; set; }
        public int MaDichVu { get; set; }
        public int MaDatPhong { get; set; }
        public DateTime NgaySuDung { get; set; }
        public int SoLuong { get; set; }
        public int MaChiTietHoaDon { get; set; }
    }
}
