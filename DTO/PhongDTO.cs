using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DTO
{
    public class PhongDTO
    {
        public int MaPhong { get; set; }
        public int SoPhong { get; set; }
        public int MaLoaiPhong { get; set; }
        public string TinhTrang { get; set; }
        public Color Roomcolor { get; set; }
        public string IdBill { get; set; }

        public string GiaTheoDem { get; set; }
        public string TenLoaiPhong { get; set; }

        public PhongDTO()
        {
           
            
        }

        public PhongDTO(int maPhong, int soPhong, int maLoaiPhong, string tinhTrang)
        {
            MaPhong = maPhong;
            SoPhong = soPhong;
            MaLoaiPhong = maLoaiPhong;
            TinhTrang = tinhTrang;
        }
    }
}
