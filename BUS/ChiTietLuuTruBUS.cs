using BUS.ClassHelper;
using DAO;
using DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BUS
{
    public class ChiTietLuuTruBUS
    {
        private string connectionString; // Chuỗi kết nối đến cơ sở dữ liệu
        private ChiTietLuuTruDAO ChiTietLuuTruDAO;
        private PhongDAO phongDAO;
        public ChiTietLuuTruBUS(string connectionString)
        {
            this.connectionString = connectionString;
            ChiTietLuuTruDAO = new ChiTietLuuTruDAO(connectionString);
             phongDAO = new PhongDAO(connectionString);
    }

        public int ThemChiTietLuuTru(ChiTietLuuTruDTO chiTietLuuTru)
        {
            
            return ChiTietLuuTruDAO.InsertChiTietLuuTru(chiTietLuuTru);
        }

        public void  LayDanhSachChiTietLuuTrubyMaDatPhong(int maPhong, Label lblNameKH, DateTimePicker DateTimeStart, Label lblStopByNight, Label lblNumberNguoi, Label DateTimeStarts = null)
        {
            try
            {
               int maDatPhong = phongDAO.LayMaDatPhongTheoMaPhongByDangSuDung(maPhong);
                DataTable data = ChiTietLuuTruDAO.LayDanhSachChiTietLuuTruTheoMaDatPhong(maDatPhong);
                // Khởi tạo danh sách chi tiết lưu trữ để trả về
               
                // Duyệt qua từng dòng dữ liệu trong DataTable và thêm vào danh sách
                foreach (DataRow row in data.Rows)
                {

                      lblNameKH.Text = GetNameKHbyMaDatPhong(maDatPhong);
                    //MaLuuTru = Convert.ToInt32(row["MaLuuTru"]),
                    //MaDatPhong = Convert.ToInt32(row["MaDatPhong"]),
                    if (lblNumberNguoi!= null)
                        lblNumberNguoi.Text = $"{row["SoLuongNguoi"]} Người";
                

                    if (DateTimeStart != null)
                        DateTimeStart.Value = Convert.ToDateTime(row["ThoiGianVaoPhong"]);
                    else
                        DateTimeStarts.Text = row["ThoiGianVaoPhong"].ToString();

                    if (lblStopByNight != null)
                        lblStopByNight.Text = $"{tinhsoNgay(Convert.ToDateTime(row["ThoiGianVaoPhong"]), Convert.ToDateTime(row["ThoiGianDuKienTraPhong"]))} Đêm";
                   
                    //  ThoiGianThucTeTraPhong = row["ThoiGianThucTeTraPhong"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(row["ThoiGianThucTeTraPhong"]),
                }
            }
            catch (Exception ex)
            {
                // Xử lý ngoại lệ nếu cần
                
            }
        }





        public string GetNameKHbyMaDatPhong(int maDatPhong)
        {
            DataTable data = ChiTietLuuTruDAO.LayThongTinKhachHangTheoMaDatPhong(maDatPhong);
            // Kiểm tra xem có dữ liệu trong DataTable hay không
            if (data.Rows.Count > 0)
            {
                // Lấy giá trị từ cột "hoten" của dòng đầu tiên (giả sử chỉ cần lấy một giá trị)
                return data.Rows[0]["hoten"].ToString();

                // Sử dụng giá trị hoTen theo nhu cầu của bạn
            }

            return "";
        }







       public int tinhsoNgay(DateTime DateTimeStart, DateTime DateTimeStop)
        {
            TimeSpan thoiGianLuuTru = DateTimeStop - DateTimeStart;
            int soNgay = (int)Math.Ceiling(thoiGianLuuTru.TotalDays); // Số ngày làm tròn lên
            return soNgay;
        }

        // Triển khai các phương thức khác để gọi lớp DAO và thực hiện các logic kinh doanh liên quan đến ChiTietLuuTru
    }
}
