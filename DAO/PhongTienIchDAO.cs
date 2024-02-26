using DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography;

namespace DAO
{
    public class PhongTienIchDAO
    {
        private string connectionString;
        DatabaseHelper dbHelper;
        public PhongTienIchDAO(string connectionString)
        {
            this.connectionString = connectionString;
            dbHelper = new DatabaseHelper(connectionString);
        }

        public int InsertPhongTienIch(PhongTienIchDTO phongTienIch)
        {
            string query = "INSERT INTO PhongTienIch (MaPhong, MaTienIch, TrangThai, SoLuong) " +
                           "VALUES (@MaPhong, @MaTienIch, @TrangThai, @SoLuong)";

            SqlParameter[] parameters = new SqlParameter[]
            {
            new SqlParameter("@MaPhong", phongTienIch.MaPhong),
            new SqlParameter("@MaTienIch", phongTienIch.MaTienIch),
            new SqlParameter("@TrangThai", phongTienIch.TrangThai),
            new SqlParameter("@SoLuong", phongTienIch.SoLuong)
            };

            return dbHelper.ExecuteInsertAndGetIdentity(query, parameters);
        }
        public bool UpdatePhongTienIch(PhongTienIchDTO phongTienIch)
        {
            string query = "UPDATE PhongTienIch " +
                           "SET MaPhong = @MaPhong, MaTienIch = @MaTienIch, TrangThai = @TrangThai, SoLuong = @SoLuong " +
                           "WHERE MaPhongTienIch = @MaPhongTienIch";

            SqlParameter[] parameters = new SqlParameter[]
            {
            new SqlParameter("@MaPhong", phongTienIch.MaPhong),
            new SqlParameter("@MaTienIch", phongTienIch.MaTienIch),
            new SqlParameter("@TrangThai", phongTienIch.TrangThai),
            new SqlParameter("@SoLuong", phongTienIch.SoLuong),
            new SqlParameter("@MaPhongTienIch", phongTienIch.MaPhongTienIch)
            };

          return  dbHelper.ExecuteNonQuery(query, parameters)>0;
        }

        public bool DeletePhongTienIch(int maPhongTienIch)
        {
         return  dbHelper.ExecuteNonQuery($"DELETE FROM PhongTienIch WHERE MaPhongTienIch = {maPhongTienIch}") > 0;
        }

        public List<PhongTienIchDTO> GetAllPhongTienIch()
        {
            List<PhongTienIchDTO> phongTienIchList = new List<PhongTienIchDTO>();

            string query = "SELECT * FROM PhongTienIch";

            DataTable dataTable = dbHelper.ExecuteQuery(query);

            foreach (DataRow row in dataTable.Rows)
            {
                PhongTienIchDTO phongTienIch = new PhongTienIchDTO
                {
                    MaPhongTienIch = Convert.ToInt32(row["MaPhongTienIch"]),
                    MaPhong = Convert.ToInt32(row["MaPhong"]),
                    MaTienIch = Convert.ToInt32(row["MaTienIch"]),
                    TrangThai = row["TrangThai"].ToString(),
                    SoLuong = Convert.ToInt32(row["SoLuong"])
                };

                phongTienIchList.Add(phongTienIch);
            }

            return phongTienIchList;
        }

        public DataTable GetAllPhongTienIch_DataTable(string tukhoaPhong = "", string tukhoaTienNghi = "", string typeStatusTienNghi = "")
        {
            string query1 = string.IsNullOrEmpty(tukhoaPhong) ? "" : $" AND Phong.SoPhong LIKE '%{tukhoaPhong}%'";
            string query2 = string.IsNullOrEmpty(tukhoaTienNghi) ? "" : $" AND TienIch.TenTienIch LIKE N'%{tukhoaTienNghi}%'";
            string query3 = string.IsNullOrEmpty(typeStatusTienNghi) ? "" : $" AND PhongTienIch.TrangThai = N'{typeStatusTienNghi}'";


            string query = $@"SELECT  PhongTienIch.*, 
            Phong.SoPhong,TienIch.TenTienIch, TienIch.MieuTa,TienIch.pathIMG,TienIch.SoLuong AS soluongTon
            FROM  PhongTienIch
            JOIN Phong ON PhongTienIch.MaPhong = Phong.MaPhong
            JOIN TienIch ON PhongTienIch.MaTienIch = TienIch.MaTienIch
            WHERE 1 = 1 {query1} {query2} {query3}";

            return dbHelper.ExecuteQuery(query);
        }




        public List<string> GetAllTrangThaiPhongTienIch()
        {
            List<string> trangThaiList = new List<string>();

            DataTable data = dbHelper.ExecuteQuery("SELECT DISTINCT TrangThai FROM PhongTienIch");

            if (data != null && data.Rows.Count > 0)
            {
                foreach (DataRow row in data.Rows)
                {
                    string trangThai = row["TrangThai"].ToString();
                    trangThaiList.Add(trangThai);
                }
            }
            return trangThaiList;
        }

        




    }
}
