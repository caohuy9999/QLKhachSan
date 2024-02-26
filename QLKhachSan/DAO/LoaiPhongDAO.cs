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
  
    public class LoaiPhongDAO
    {
        private DatabaseHelper dbHelper;
        public LoaiPhongDAO(string connectionString)
        {
            dbHelper = new DatabaseHelper(connectionString);
        }
        public DataTable GetAllLoaiPhong()
        {
            string query = "SELECT * FROM LoaiPhong";
            return dbHelper.ExecuteQuery(query);
        }
        public void InsertLoaiPhong(string tenLoaiPhong, decimal giaTheoGio, decimal giaTheoNgay, int sucChua, string mieuTa)
        {
            string query = "INSERT INTO LoaiPhong (TenLoaiPhong, GiaTheoGio, GiaTheoNgay, SucChua, MieuTa) " +
                           "VALUES (@TenLoaiPhong, @GiaTheoGio, @GiaTheoNgay, @SucChua, @MieuTa)";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@TenLoaiPhong", tenLoaiPhong),
                new SqlParameter("@GiaTheoGio", giaTheoGio),
                new SqlParameter("@GiaTheoNgay", giaTheoNgay),
                new SqlParameter("@SucChua", sucChua),
                new SqlParameter("@MieuTa", mieuTa)
            };

            dbHelper.ExecuteNonQuery(query, parameters);
        }
        // Các phương thức khác cho việc cập nhật và xóa dữ liệu

    }
}
