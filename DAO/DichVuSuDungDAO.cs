using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using DTO;

namespace DAO
{
    public class DichVuSuDungDAO
    {
        private readonly DatabaseHelper dbHelper;

        public DichVuSuDungDAO(string connectionString)
        {
            dbHelper = new DatabaseHelper(connectionString);
        }

        public List<DichVuSuDungDTO> LayDanhSachDichVuSuDung()
        {
            string query = "SELECT * FROM DichVuDuocSuDung";
            DataTable dataTable = dbHelper.ExecuteQuery(query);

            List<DichVuSuDungDTO> dichVuSuDungList = new List<DichVuSuDungDTO>();

            foreach (DataRow row in dataTable.Rows)
            {
                DichVuSuDungDTO dichVuSuDung = new DichVuSuDungDTO
                {
                    MaSuDung = Convert.ToInt32(row["MaSuDung"]),
                    MaDichVu = Convert.ToInt32(row["MaDichVu"]),
                    MaDatPhong = Convert.ToInt32(row["MaDatPhong"]),
                    NgaySuDung = Convert.ToDateTime(row["NgaySuDung"]),
                    SoLuong = Convert.ToInt32(row["SoLuong"]),
                    MaChiTietHoaDon = Convert.ToInt32(row["MaChiTietHoaDon"])
                };

                dichVuSuDungList.Add(dichVuSuDung);
            }

            return dichVuSuDungList;
        }

        public DataTable LayDanhSachDichVuSuDungByMaDatPhong(int MaDatPhong)
        {
            string query = "SELECT * FROM DichVuDuocSuDung WHERE MaDatPhong = @MaDatPhong";
            

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@MaDatPhong", MaDatPhong)
            };
            return dbHelper.ExecuteQuery(query, parameters);
        }


        public int ThemDichVuSuDung(DichVuSuDungDTO dichVuSuDung)
        {
            string query = "INSERT INTO DichVuDuocSuDung (MaDichVu, MaDatPhong, NgaySuDung, SoLuong, MaChiTietHoaDon) VALUES (@MaDichVu, @MaDatPhong, @NgaySuDung, @SoLuong, @MaChiTietHoaDon)";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@MaDichVu", dichVuSuDung.MaDichVu),
                new SqlParameter("@MaDatPhong", dichVuSuDung.MaDatPhong),
                new SqlParameter("@NgaySuDung", dichVuSuDung.NgaySuDung),
                new SqlParameter("@SoLuong", dichVuSuDung.SoLuong),
                new SqlParameter("@MaChiTietHoaDon", dichVuSuDung.MaChiTietHoaDon)
            };

            int affectedRows = dbHelper.ExecuteInsertAndGetIdentity(query, parameters);
            return affectedRows;
        }

        public int CapNhatDichVuSuDung(DichVuSuDungDTO dichVuSuDung)
        {
            string query = "UPDATE DichVuDuocSuDung SET MaDichVu = @MaDichVu, MaDatPhong = @MaDatPhong, NgaySuDung = @NgaySuDung, SoLuong = @SoLuong, MaChiTietHoaDon = @MaChiTietHoaDon WHERE MaSuDung = @MaSuDung";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@MaSuDung", dichVuSuDung.MaSuDung),
                new SqlParameter("@MaDichVu", dichVuSuDung.MaDichVu),
                new SqlParameter("@MaDatPhong", dichVuSuDung.MaDatPhong),
                new SqlParameter("@NgaySuDung", dichVuSuDung.NgaySuDung),
                new SqlParameter("@SoLuong", dichVuSuDung.SoLuong),
                new SqlParameter("@MaChiTietHoaDon", dichVuSuDung.MaChiTietHoaDon)
            };

            int affectedRows = dbHelper.ExecuteNonQuery(query, parameters);
            return affectedRows;
        }

        public int XoaDichVuSuDung(int maSuDung)
        {
            string query = "DELETE FROM DichVuDuocSuDung WHERE MaSuDung = @MaSuDung";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@MaSuDung", maSuDung)
            };

            int affectedRows = dbHelper.ExecuteNonQuery(query, parameters);
            return affectedRows;
        }
    }
}
