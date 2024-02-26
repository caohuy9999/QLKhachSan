using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
   public class LoaiPhongDTO
    {
        public int MaLoaiPhong { get; set; }
        public string TenLoaiPhong { get; set; }
        public decimal GiaTheoDem { get; set; }
        
        public int SucChua { get; set; }
        public string MieuTa { get; set; }

        // Hàm khởi tạo mặc định
        public LoaiPhongDTO()
        {
            // Mặc định, bạn có thể để các giá trị là giá trị mặc định hoặc null
        }

        // Hàm khởi tạo với tham số để khởi tạo các giá trị ban đầu
        public LoaiPhongDTO(int maLoaiPhong, string tenLoaiPhong, decimal giaTheoDem, int sucChua, string mieuTa)
        {
            MaLoaiPhong = maLoaiPhong;
            TenLoaiPhong = tenLoaiPhong;
            GiaTheoDem = giaTheoDem;
            SucChua = sucChua;
            MieuTa = mieuTa;
        }



    }
}
