using DTO;
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
    public class PhongDAO
    {
        private DatabaseHelper dbHelper;

        public PhongDAO(string connectionString)
        {
            dbHelper = new DatabaseHelper(connectionString);
        }

        public DataTable GetAllPhong()
        {
            string query = "SELECT * FROM Phong";
            return dbHelper.ExecuteQuery(query);
        }

        public void InsertPhong(PhongDTO phong)
        {
            string query = "INSERT INTO Phong (SoPhong, MaLoaiPhong, TinhTrang) " +
                           "VALUES (@SoPhong, @MaLoaiPhong, @TinhTrang)";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@SoPhong", phong.SoPhong),
                new SqlParameter("@MaLoaiPhong", phong.MaLoaiPhong),
                new SqlParameter("@TinhTrang", phong.TinhTrang)
            };

            dbHelper.ExecuteNonQuery(query, parameters);
        }

        // Các phương thức khác cho việc cập nhật và xóa dữ liệu
    }
}

