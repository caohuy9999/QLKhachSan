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
    public class QuyenDAO
    {
        DatabaseHelper db;
        private string connectionString; // Chuỗi kết nối CSDL

        public QuyenDAO(string connectionString)
        {
            this.connectionString = connectionString;
            db = new DatabaseHelper(connectionString);
        }




        public List<QuyenDTO> GetAllQuyen()
        {
            List<QuyenDTO> quyenList = new List<QuyenDTO>();
            string query = "SELECT MaQuyen, TenQuyen, MieuTa FROM Quyen";
            DatabaseHelper db = new DatabaseHelper(connectionString);
            DataTable data = db.ExecuteQuery(query);

            foreach (DataRow row in data.Rows)
            {
                QuyenDTO quyen = new QuyenDTO
                {
                    MaQuyen = Convert.ToInt32(row["MaQuyen"]),
                    TenQuyen = row["TenQuyen"].ToString(),
                    MieuTa = row["MieuTa"].ToString()
                };
                quyenList.Add(quyen);
            }

            return quyenList;
        }

        // Thêm quyền mới
        public int AddQuyen(QuyenDTO quyen)
        {
            string query = "INSERT INTO Quyen (TenQuyen, MieuTa) VALUES (@TenQuyen, @MieuTa)";
            SqlParameter[] parameters = new SqlParameter[2];
            parameters[0] = new SqlParameter("@TenQuyen", quyen.TenQuyen);
            parameters[1] = new SqlParameter("@MieuTa", quyen.MieuTa);

            DatabaseHelper db = new DatabaseHelper(connectionString);
            return db.ExecuteInsertAndGetIdentity(query, parameters);
        }

        // Sửa quyền
        public bool UpdateQuyen(QuyenDTO quyen)
        {
            string query = "UPDATE Quyen SET TenQuyen = @TenQuyen, MieuTa = @MieuTa WHERE MaQuyen = @MaQuyen";
            SqlParameter[] parameters = new SqlParameter[3];
            parameters[0] = new SqlParameter("@TenQuyen", quyen.TenQuyen);
            parameters[1] = new SqlParameter("@MieuTa", quyen.MieuTa);
            parameters[2] = new SqlParameter("@MaQuyen", quyen.MaQuyen);

            DatabaseHelper db = new DatabaseHelper(connectionString);
            int rowsAffected = db.ExecuteNonQuery(query, parameters);
            return rowsAffected > 0;
        }

        // Xóa quyền
        public bool DeleteQuyen(int maQuyen)
        {
            string query = "DELETE FROM Quyen WHERE MaQuyen = @MaQuyen";
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("@MaQuyen", maQuyen);

            DatabaseHelper db = new DatabaseHelper(connectionString);
            int rowsAffected = db.ExecuteNonQuery(query, parameters);
            return rowsAffected > 0;
        }
    }

}

