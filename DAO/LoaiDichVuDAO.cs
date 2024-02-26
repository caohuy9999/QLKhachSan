using DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DAO
{
    public class LoaiDichVuDAO
    {
        private readonly DatabaseHelper dbHelper;

        public LoaiDichVuDAO(string connectionString)
        {
            dbHelper = new DatabaseHelper(connectionString);
        }

        public List<LoaiDichVuDTO> LayDanhSachLoaiDichVu()
        {
            List<LoaiDichVuDTO> danhSachLoaiDichVu = new List<LoaiDichVuDTO>();
            string query = "SELECT * FROM LoaiDichVu";

            DataTable dataTable = dbHelper.ExecuteQuery(query);

            foreach (DataRow row in dataTable.Rows)
            {
                danhSachLoaiDichVu.Add(new LoaiDichVuDTO
                {
                    MaLoaiDichVu = Convert.ToInt32(row["MaLoaiDichVu"]),
                    TenLoaiDichVu = row["TenLoaiDichVu"].ToString(),
                    MoTa = row["MoTa"].ToString()
                });
            }

            return danhSachLoaiDichVu;
        }
        public DataTable LayDanhSachLoaiDichVu_DataTable()
        {
            string query = "SELECT * FROM LoaiDichVu";
            return dbHelper.ExecuteQuery(query);
        }



        public LoaiDichVuDTO LayLoaiDichVuTheoMa(int maLoaiDichVu)
        {
            string query = "SELECT * FROM LoaiDichVu WHERE MaLoaiDichVu = @MaLoaiDichVu";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@MaLoaiDichVu", maLoaiDichVu)
            };

            DataTable dataTable = dbHelper.ExecuteQuery(query, parameters);

            if (dataTable.Rows.Count == 0)
            {
                return null;
            }

            DataRow row = dataTable.Rows[0];
            return new LoaiDichVuDTO
            {
                MaLoaiDichVu = Convert.ToInt32(row["MaLoaiDichVu"]),
                TenLoaiDichVu = row["TenLoaiDichVu"].ToString(),
                MoTa = row["MoTa"].ToString()
            };
        }

        public bool ThemLoaiDichVu(LoaiDichVuDTO loaiDichVu)
        {
            string query = "INSERT INTO LoaiDichVu (TenLoaiDichVu, MoTa) VALUES (@TenLoaiDichVu, @MoTa)";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@TenLoaiDichVu", loaiDichVu.TenLoaiDichVu),
                new SqlParameter("@MoTa", loaiDichVu.MoTa)
            };

            return dbHelper.ExecuteNonQuery(query, parameters) > 0;
        }
        public int INSERTLoaiDichVu(LoaiDichVuDTO loaiDichVu)
        {
            string query = "INSERT INTO LoaiDichVu (TenLoaiDichVu, MoTa) VALUES (@TenLoaiDichVu, @MoTa)";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@TenLoaiDichVu", loaiDichVu.TenLoaiDichVu),
                new SqlParameter("@MoTa", loaiDichVu.MoTa)
            };

            return dbHelper.ExecuteInsertAndGetIdentity(query, parameters);
        }





        public bool CapNhatLoaiDichVu(LoaiDichVuDTO loaiDichVu)
        {
            string query = "UPDATE LoaiDichVu SET TenLoaiDichVu = @TenLoaiDichVu, MoTa = @MoTa WHERE MaLoaiDichVu = @MaLoaiDichVu";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@MaLoaiDichVu", loaiDichVu.MaLoaiDichVu),
                new SqlParameter("@TenLoaiDichVu", loaiDichVu.TenLoaiDichVu),
                new SqlParameter("@MoTa", loaiDichVu.MoTa)
            };

            return dbHelper.ExecuteNonQuery(query, parameters) > 0;
        }

        public bool XoaLoaiDichVu(int maLoaiDichVu)
        {
            string query = "DELETE FROM LoaiDichVu WHERE MaLoaiDichVu = @MaLoaiDichVu";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@MaLoaiDichVu", maLoaiDichVu)
            };

            return dbHelper.ExecuteNonQuery(query, parameters) > 0;
        }
    }
}
