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
    public class tienichDAO
    {
        private string connectionString;
        DatabaseHelper db;
        public tienichDAO(string connectionString)
        {
            this.connectionString = connectionString;
            db = new DatabaseHelper(connectionString);
        }

        public List<TienIchDTO> GetAllTienNghi(int sophong = 0 ,string KeywordSearch = "")
        {
            string querySoPhong = sophong != 0 ? $" AND Phong.MaPhong = {sophong}" : "";
            string queryKeyWord = !string.IsNullOrEmpty(KeywordSearch) ? $" AND TienIch.TenTienIch LIKE N'%{KeywordSearch}%'" : "";

            List<TienIchDTO> listTienNghi = new List<TienIchDTO>();



            string query = $@"
        SELECT PhongTienIch.*, Phong.SoPhong, TienIch.*
        FROM PhongTienIch
        JOIN Phong ON PhongTienIch.MaPhong = Phong.MaPhong
        JOIN TienIch ON PhongTienIch.MaTienIch = TienIch.MaTienIch
        WHERE 1=1 {querySoPhong} {queryKeyWord}";

            if (querySoPhong == ""  && queryKeyWord == "")
            {
                query = "SELECT * FROM TienIch";
            }
            else if(queryKeyWord != "" && querySoPhong == "")
            {
                query = $"SELECT * FROM TienIch WHERE 1=1 {queryKeyWord}";
            }


            DataTable result = db.ExecuteQuery(query);
            
                foreach (DataRow row in result.Rows)
                {
                    TienIchDTO tienNghi = new TienIchDTO
                    {
                        MaTienIch = Convert.ToInt32(row["MaTienIch"]),
                        TenTienIch = row["TenTienIch"].ToString(),
                        MieuTa = row["MieuTa"].ToString(),
                        PathIMG = row["PathIMG"].ToString(),
                        SoLuong = Convert.ToInt32(row["SoLuong"])
                    };

                    listTienNghi.Add(tienNghi);
                }
            

            return listTienNghi;
        }



        public bool InsertTienIch(TienIchDTO tienIch)
        {
            string query = "INSERT INTO TienIch (TenTienIch, MieuTa, pathIMG, SoLuong) VALUES (@TenTienIch, @MieuTa, @PathIMG, @SoLuong)";

            SqlParameter[] parameters =
            {
                new SqlParameter("@TenTienIch", tienIch.TenTienIch),
                new SqlParameter("@MieuTa", tienIch.MieuTa),
                new SqlParameter("@PathIMG", tienIch.PathIMG),
                  new SqlParameter("@SoLuong", tienIch.SoLuong)
            };

            return db.ExecuteNonQuery(query, parameters) > 0;
        }

        public bool UpdateTienIch(TienIchDTO tienIch)
        {
            string query = "UPDATE TienIch SET TenTienIch = @TenTienIch, MieuTa = @MieuTa, pathIMG = @PathIMG, SoLuong = @SoLuong WHERE MaTienIch = @MaTienIch";

            SqlParameter[] parameters =
            {
                new SqlParameter("@MaTienIch", tienIch.MaTienIch),
                new SqlParameter("@TenTienIch", tienIch.TenTienIch),
                new SqlParameter("@MieuTa", tienIch.MieuTa),
                new SqlParameter("@PathIMG", tienIch.PathIMG),
                 new SqlParameter("@SoLuong", tienIch.SoLuong)
            };

            return db.ExecuteNonQuery(query, parameters) > 0;
        }

        public bool DeleteTienIch(int maTienIch)
        {
            string query = "DELETE FROM TienIch WHERE MaTienIch = @MaTienIch";

            SqlParameter[] parameters =
            {
                new SqlParameter("@MaTienIch", maTienIch)
            };

            return db.ExecuteNonQuery(query, parameters) > 0;
        }


        public int GetSoLuongByTenTienIch(string tenTienIch)
        {
            string query = "SELECT SoLuong FROM TienIch WHERE TenTienIch = @tenTienIch";
            SqlParameter[] parameters =
           {
                new SqlParameter("@tenTienIch", tenTienIch)
            };
            object result = db.ExecuteScalar(query, parameters);
            return result != null ? Convert.ToInt32(result) : 0;
        }


        public bool UpAndDownSoLuongTienIch(int MaTienIch, bool Down = false, int soluong = 1)
        {
            string query = $"UPDATE TienIch SET SoLuong = SoLuong - {soluong} WHERE MaTienIch = @MaTienIch";

            if (!Down)
            {
                query = $"UPDATE TienIch SET SoLuong = SoLuong + {soluong} WHERE MaTienIch = @MaTienIch";
            }

            SqlParameter[] parameters = new SqlParameter[] { new SqlParameter("@MaTienIch", MaTienIch) };

            return db.ExecuteNonQuery(query, parameters) > 0;
        }
    }
}
