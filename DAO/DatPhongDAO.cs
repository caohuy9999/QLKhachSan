using DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO
{
    public class DatPhongDAO
    {
        private DatabaseHelper dbHelper;

        public DatPhongDAO(string connectionString)
        {
            dbHelper = new DatabaseHelper(connectionString);
        }

        public DataTable GetAllDatPhong()
        {
            string query = "SELECT * FROM DatPhong";
            return dbHelper.ExecuteQuery(query);
        }



        public DataTable PhongkhadungdeBook(DateTime thoiGianBatDau, DateTime thoiGianKetThuc)
        {
            // Tạo câu truy vấn SQL với thời gian bắt đầu và kết thúc
            string query = "SELECT * FROM Phong WHERE TinhTrang = N'Trống' AND MaPhong NOT IN " +
                           "(SELECT MaPhong FROM DatPhong WHERE " +
                           "(NgayNhanPhong >= @StartTime AND NgayNhanPhong <= @EndTime) " +
                           "OR (NgayTraPhong >= @StartTime AND NgayTraPhong <= @EndTime))";

            // Thực hiện truy vấn SQL với tham số
            SqlParameter[] parameters = new SqlParameter[2];
            parameters[0] = new SqlParameter("@StartTime", thoiGianBatDau);
            parameters[1] = new SqlParameter("@EndTime", thoiGianKetThuc);

            return dbHelper.ExecuteQuery(query, parameters);

        }

        public DataTable GetLoaiPhongByMaLoaiPhong(int maLoaiPhong)
        {
            string query = "SELECT * FROM LoaiPhong WHERE MaLoaiPhong = @MaLoaiPhong";
            SqlParameter[] parameters = new SqlParameter[]
            {
        new SqlParameter("@MaLoaiPhong", maLoaiPhong)
            };
            return dbHelper.ExecuteQuery(query, parameters);
        }


        public int InsertDatPhong(DatPhongDTO datPhong)
        {

            string query = "INSERT INTO DatPhong (MaKhachHang, MaPhong, NgayNhanPhong, NgayTraPhong, TinhTrang) " +
                           "VALUES (@MaKhachHang, @MaPhong, @NgayNhanPhong, @NgayTraPhong, @TinhTrang)";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@MaKhachHang", datPhong.MaKhachHang),
                new SqlParameter("@MaPhong", datPhong.MaPhong),
                new SqlParameter("@NgayNhanPhong", datPhong.NgayNhanPhong),
                new SqlParameter("@NgayTraPhong", datPhong.NgayTraPhong),
                new SqlParameter("@TinhTrang", datPhong.TinhTrang)
            };
            int newID = dbHelper.ExecuteInsertAndGetIdentity(query, parameters);


            return newID;
        }




        public bool UpdateDatPhong(DatPhongDTO datPhong)
        {
            string query = "UPDATE DatPhong SET MaKhachHang = @MaKhachHang, MaPhong = @MaPhong, " +
                           "NgayNhanPhong = @NgayNhanPhong, NgayTraPhong = @NgayTraPhong, TinhTrang = @TinhTrang " +
                           "WHERE MaDatPhong = @MaDatPhong";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@MaKhachHang", datPhong.MaKhachHang),
                new SqlParameter("@MaPhong", datPhong.MaPhong),
                new SqlParameter("@NgayNhanPhong", datPhong.NgayNhanPhong),
                new SqlParameter("@NgayTraPhong", datPhong.NgayTraPhong),
                new SqlParameter("@TinhTrang", datPhong.TinhTrang),
                new SqlParameter("@MaDatPhong", datPhong.MaDatPhong)
            };

            return dbHelper.ExecuteNonQuery(query, parameters) > 0;
        }

        public bool DeleteDatPhong(int maDatPhong)
        {
            string query = "DELETE FROM DatPhong WHERE MaDatPhong = @MaDatPhong";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@MaDatPhong", maDatPhong)
            };

            return dbHelper.ExecuteNonQuery(query, parameters) > 0;
        }


        public bool UpdateDatPhongTinhTrangByMaHoaDon(int maHoaDon, string tinhTrang)
        {
            try
            {
                // Tạo câu truy vấn SQL để cập nhật trường TinhTrang của đặt phòng dựa trên ID hóa đơn
                string query = "UPDATE DatPhong SET TinhTrang = @TinhTrang " +
                               "WHERE MaDatPhong IN (SELECT MaDatPhong FROM HoaDon WHERE MaHoaDon = @MaHoaDon)";

                // Tạo mảng tham số
                SqlParameter[] parameters = new SqlParameter[]
                {
            new SqlParameter("@TinhTrang", tinhTrang),
            new SqlParameter("@MaHoaDon", maHoaDon)
                };

                // Thực hiện câu truy vấn
                int rowsAffected = dbHelper.ExecuteNonQuery(query, parameters);

                // Kiểm tra xem có dòng nào bị ảnh hưởng không
                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                // Xử lý nếu có lỗi
                Console.WriteLine($"Lỗi trong quá trình cập nhật trạng thái đặt phòng: {ex.Message}");
                return false;
            }
        }


    }
}
