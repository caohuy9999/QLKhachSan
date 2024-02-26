using DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.NetworkInformation;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DAO
{
    public class PhongDAO
    {
        private DatabaseHelper dbHelper;

        public PhongDAO(string connectionString)
        {
            dbHelper = new DatabaseHelper(connectionString);
        }

        public DataTable GetAllPhong(string keyword = "", int cbbTypeRoom = 0, string cbbTinhTrangRoom = "")
        {
            string query = @"
            SELECT Phong.MaPhong, Phong.SoPhong, Phong.MaLoaiPhong, Phong.TinhTrang, LoaiPhong.TenLoaiPhong, LoaiPhong.GiaTheoDem,
            CASE
            WHEN Phong.TinhTrang = N'Đang sử dụng' THEN DatPhong.MaDatPhong
            ELSE NULL
            END AS MaDatPhong,
            CASE
            WHEN Phong.TinhTrang = N'Đang sử dụng' THEN HoaDon.MaHoaDon
            ELSE NULL
            END AS MaHoaDon
            FROM Phong
            LEFT JOIN DatPhong ON Phong.MaPhong = DatPhong.MaPhong AND DatPhong.TinhTrang = N'Đang sử dụng'
            LEFT JOIN HoaDon ON DatPhong.MaDatPhong = HoaDon.MaDatPhong
            LEFT JOIN LoaiPhong ON Phong.MaLoaiPhong = LoaiPhong.MaLoaiPhong
            WHERE 1 = 1 ";
            //-- 1Sử dụng CASE để kiểm tra tình trạng phòng và lấy thông tin từ DatPhong khi tình trạng là 'Đang sử dụng'
            //-- 2Sử dụng CASE để kiểm tra tình trạng phòng và lấy thông tin từ HoaDon khi tình trạng là 'Đang sử dụng'
            //-- 3LEFT JOIN với bảng DatPhong dựa trên cột MaPhong và điều kiện TinhTrang là 'Đang sử dụng'
            //-- 4LEFT JOIN với bảng HoaDon dựa trên cột MaDatPhong
            //-- 5LEFT JOIN với bảng LoaiPhong dựa trên cột MaLoaiPhong

            if (!string.IsNullOrEmpty(keyword))
            {
                query += " AND Phong.SoPhong LIKE @Keyword";
            }

            if (cbbTypeRoom > 0)
            {
                query += " AND Phong.MaLoaiPhong = @TypeRoom";
            }

            if (!string.IsNullOrEmpty(cbbTinhTrangRoom) && cbbTinhTrangRoom != "All")
            {
                query += $" AND Phong.TinhTrang = N'{cbbTinhTrangRoom}'";
            }

            SqlParameter[] parameters = new SqlParameter[]
   {
        new SqlParameter("@Keyword", $"%{keyword}%"),
        new SqlParameter("@TypeRoom", cbbTypeRoom),
   };



            return dbHelper.ExecuteQuery(query, parameters);
        }


        public DataTable LayDanhSachPhongTrong()
        {
            string query = "SELECT * FROM Phong WHERE TinhTrang = N'Trống'";
            return dbHelper.ExecuteQuery(query);
         
        }

        public DataTable GetPhongByID(int maPhong)
        {
            // Viết truy vấn SQL để lấy thông tin phòng bằng ID
            string query = "SELECT * FROM Phong WHERE MaPhong = @MaPhong";
            SqlParameter[] parameters = new SqlParameter[] { new SqlParameter("@MaPhong", maPhong) };

            // Sử dụng DatabaseHelper để thực hiện truy vấn và trả về dữ liệu
            return dbHelper.ExecuteQuery(query, parameters);
        }

        public int LayMaDatPhongTheoMaPhongByDangSuDung(int maPhong)
        {
            string query = $"SELECT MaDatPhong FROM DatPhong WHERE MaPhong = {maPhong} AND DatPhong.TinhTrang = N'Đang sử dụng'";
            
            DataTable result = dbHelper.ExecuteQuery(query);

            if (result != null && result.Rows.Count > 0)
            {
                // Lấy giá trị MaDatPhong từ bảng kết quả
                int maDatPhong = Convert.ToInt32(result.Rows[0]["MaDatPhong"]);
                return maDatPhong;
            }
            else
            {
                // Trả về giá trị mặc định nếu không tìm thấy
                return -1;
            }
        }


        public int GetIdHoaDon_tu_MaPhong(int maphong)
        {
            string query = @"SELECT HoaDon.MaHoaDon 
            FROM HoaDon 
            JOIN DatPhong ON HoaDon.MaDatPhong = DatPhong.MaDatPhong 
            WHERE DatPhong.MaPhong = @MaPhong AND DatPhong.TinhTrang = N'Đang sử dụng'";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@MaPhong", maphong),
            };

            DataTable data  = dbHelper.ExecuteQuery(query, parameters);
            if (data.Rows.Count > 0)
            {
                return Convert.ToInt32(data.Rows[0]["MaHoaDon"]);
                // Bây giờ bạn có MaHoaDon tương ứng với MaPhong cho phòng đó.
            }
            return -1;
        }


        public DataTable GetStatus_Room(int maPhong = 0)
        {
            string index = "";
            if (maPhong != 0)
            {
                index = $" WHERE MaPhong = {maPhong}";
            }
            string query = $"SELECT DISTINCT TinhTrang FROM Phong {index};";
            return dbHelper.ExecuteQuery(query);
        }


        public bool InsertPhong(PhongDTO phong)
        {
            // lấy số phòng lớn nhất + thêm 1:D 
            phong.SoPhong =  dbHelper.ExecuteInsertAndGetIdentity("SELECT MAX(SoPhong) + 1 AS NextSoPhong FROM Phong;");

            string query = "INSERT INTO Phong (SoPhong, MaLoaiPhong, TinhTrang) " +
                           "VALUES (@SoPhong, @MaLoaiPhong, @TinhTrang)";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@SoPhong", phong.SoPhong),
                new SqlParameter("@MaLoaiPhong", phong.MaLoaiPhong),
                new SqlParameter("@TinhTrang", phong.TinhTrang)
            };

          return  dbHelper.ExecuteInsertAndGetIdentity(query, parameters) > 0;
        }

        public bool UpdateStatusPhong(int maPhong, string tinhTrangMoi)
        {
            try
            {
               
                string query = "UPDATE Phong SET TinhTrang = @TinhTrangMoi WHERE MaPhong = @MaPhong";
                SqlParameter[] parameters = new SqlParameter[2];
                parameters[0] = new SqlParameter("@TinhTrangMoi", tinhTrangMoi);
                parameters[1] = new SqlParameter("@MaPhong", maPhong);

                int rowsAffected = dbHelper.ExecuteNonQuery(query, parameters);

                if (rowsAffected > 0)
                {
                    return true; // Trạng thái của phòng đã được cập nhật thành công
                }
                return false; // Không có bản ghi nào được cập nhật
            }
            catch (Exception ex)
            {
                // Xử lý lỗi (ném ra hoặc ghi log)
                Console.WriteLine("Lỗi: " + ex.Message);
                return false; // Đã xảy ra lỗi trong quá trình cập nhật trạng thái phòng
            }
        }

        public DataTable SearchPhongByKeyword(string keyword)
        {
            string query = "SELECT * FROM Phong WHERE SoPhong LIKE @Keyword";
            SqlParameter[] parameters = new SqlParameter[] {new SqlParameter("@Keyword", SqlDbType.NVarChar) { Value = $"%{keyword}%" }};
            return dbHelper.ExecuteQuery(query, parameters);
        }

        public DataTable SearchPhongByLoaiPhong(int idLoaiPhong)
        {
           
            string query = "SELECT * FROM Phong WHERE MaLoaiPhong = @MaLoaiPhong";
            SqlParameter[] parameters = {
                new SqlParameter("@MaLoaiPhong", idLoaiPhong)
            };
            return dbHelper.ExecuteQuery(query, parameters); ;
        }


        public DataTable GetAllSoPhong_AS_SoPhong_NVARCHAR()
        {
            return dbHelper.ExecuteQuery("SELECT MaPhong, SoPhong, N'Số Phòng ' + CAST(SoPhong AS NVARCHAR(50)) AS SoPhongMoi FROM Phong");
        }


        // Các phương thức khác cho việc cập nhật và xóa dữ liệu
    }
}

