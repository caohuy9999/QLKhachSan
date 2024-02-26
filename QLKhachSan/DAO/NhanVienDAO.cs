using QLKhachSan;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO
{
    public class NhanVienDAO
    {
        private DatabaseHelper dbHelper;

        public NhanVienDAO(string connectionString)
        {
            dbHelper = new DatabaseHelper(connectionString);
        }

        public DataTable GetAllNhanVien()
        {
            string query = "SELECT * FROM NhanVien";
            return dbHelper.ExecuteQuery(query);
        }   

        public void InsertNhanVien(string tenDangNhap, string matKhau, string hoTen, string chucVu, decimal luong)
        {
            string query = "INSERT INTO NhanVien (TenDangNhap, MatKhau, HoTen, ChucVu, Luong) " +
                           "VALUES (@TenDangNhap, @MatKhau, @HoTen, @ChucVu, @Luong)";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@TenDangNhap", tenDangNhap),
                new SqlParameter("@MatKhau", matKhau),
                new SqlParameter("@HoTen", hoTen),
                new SqlParameter("@ChucVu", chucVu),
                new SqlParameter("@Luong", luong)
            };

            dbHelper.ExecuteNonQuery(query, parameters);
        }

        // Các phương thức khác cho việc cập nhật và xóa dữ liệu
    }
}
