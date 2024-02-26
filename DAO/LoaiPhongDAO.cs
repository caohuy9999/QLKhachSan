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

        public DataTable GetAllLoaiPhongByTypeRoom(int typeRoom)
        {
            string query = $"SELECT * FROM LoaiPhong WHERE MALOAIPHONG = {typeRoom}";
            return dbHelper.ExecuteQuery(query);
        }



        public void InsertLoaiPhong(string tenLoaiPhong, decimal GiaTheoDem, int sucChua, string mieuTa)
        {
            string query = "INSERT INTO LoaiPhong (TenLoaiPhong, GiaTheoDem, SucChua, MieuTa) " +
                           "VALUES (@TenLoaiPhong, @GiaTheoDem, @SucChua, @MieuTa)";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@TenLoaiPhong", tenLoaiPhong),
                new SqlParameter("@GiaTheoDem", GiaTheoDem),
                new SqlParameter("@SucChua", sucChua),
                new SqlParameter("@MieuTa", mieuTa)
            };

            dbHelper.ExecuteNonQuery(query, parameters);
        }

        public int InsertLoaiPhong(LoaiPhongDTO loaiPhong)
        {
            string query = "INSERT INTO LoaiPhong (TenLoaiPhong, GiaTheoDem, SucChua, MieuTa) " +
                           "VALUES (@TenLoaiPhong, @GiaTheoDem, @SucChua, @MieuTa)";

            SqlParameter[] parameters = new SqlParameter[]
            {
        new SqlParameter("@TenLoaiPhong", loaiPhong.TenLoaiPhong),
        new SqlParameter("@GiaTheoDem", loaiPhong.GiaTheoDem),
        new SqlParameter("@SucChua", loaiPhong.SucChua),
        new SqlParameter("@MieuTa", loaiPhong.MieuTa)
            };

            return dbHelper.ExecuteNonQuery(query, parameters);
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

        public bool UpdateLoaiPhong(LoaiPhongDTO loaiPhong)
        {
            string query = "UPDATE LoaiPhong SET TenLoaiPhong = @TenLoaiPhong, " +
                           "GiaTheoDem = @GiaTheoDem, SucChua = @SucChua, MieuTa = @MieuTa " +
                           "WHERE MaLoaiPhong = @MaLoaiPhong";

            SqlParameter[] parameters = new SqlParameter[]
            {
            new SqlParameter("@TenLoaiPhong", loaiPhong.TenLoaiPhong),
            new SqlParameter("@GiaTheoDem", loaiPhong.GiaTheoDem),
            new SqlParameter("@SucChua", loaiPhong.SucChua),
            new SqlParameter("@MieuTa", loaiPhong.MieuTa),
            new SqlParameter("@MaLoaiPhong", loaiPhong.MaLoaiPhong)
            };

            return dbHelper.ExecuteNonQuery(query, parameters) > 0;
        }

        public bool DeleteLoaiPhong(int maLoaiPhong)
        {
            string query = "DELETE FROM LoaiPhong WHERE MaLoaiPhong = @MaLoaiPhong";
            SqlParameter[] parameters = new SqlParameter[]
            {
            new SqlParameter("@MaLoaiPhong", maLoaiPhong)
            };

            return dbHelper.ExecuteNonQuery(query, parameters) > 0;
        }

        public DataTable GetPhongByMaLoaiPhong(int maLoaiPhong, string typeSTT = "", string KeywordSearch = "")
        {
            string type = "";
            string key = "";

            if (typeSTT != "")
            {
                type = $" AND Phong.TinhTrang = N'{typeSTT}'";
            }
            if (KeywordSearch != "")
            {
                key = $" AND Phong.SoPhong LIKE '%{KeywordSearch}%'";
            }

            string query = $@"SELECT DISTINCT Phong.*
                   FROM Phong
               WHERE Phong.MaLoaiPhong = @MaLoaiPhong {type} {key}";

            SqlParameter[] parameters = new SqlParameter[]
            {
        new SqlParameter("@MaLoaiPhong", maLoaiPhong)
            };

            return dbHelper.ExecuteQuery(query, parameters);
        }





    }
}
