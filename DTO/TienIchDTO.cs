using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class TienIchDTO
    {
        public int MaTienIch { get; set; }
        public string TenTienIch { get; set; }
        public string MieuTa { get; set; }
        public string PathIMG { get; set; } // Thêm cột mới
        public int SoLuong { get; set; }// Thêm cột mới
    }
}
