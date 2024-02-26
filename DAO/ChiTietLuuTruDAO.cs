using DTO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO
{
    public class ChiTietLuuTruDAO
    {
        private DatabaseHelper dbHelper;
        private string connectionString;

        public ChiTietLuuTruDAO(string connectionString)
        {
            this.connectionString = connectionString;
            dbHelper = new DatabaseHelper(connectionString);
            
        }

        public int InsertChiTietLuuTru(ChiTietLuuTruDTO chiTiet)
        {

                string query = "INSERT INTO ChiTietLuuTru (MaDatPhong, SoLuongNguoi, ThoiGianVaoPhong, ThoiGianDuKienTraPhong, ThoiGianThucTeTraPhong, TinhTrang) " +
                               "VALUES (@MaDatPhong, @SoLuongNguoi, @ThoiGianVaoPhong, @ThoiGianDuKienTraPhong, @ThoiGianThucTeTraPhong, @TinhTrang)";
                SqlParameter[] parameters =
                {
                    new SqlParameter("@MaDatPhong", chiTiet.MaDatPhong),
                    new SqlParameter("@SoLuongNguoi", chiTiet.SoLuongNguoi),
                    new SqlParameter("@ThoiGianVaoPhong", chiTiet.ThoiGianVaoPhong),
                    new SqlParameter("@ThoiGianDuKienTraPhong", chiTiet.ThoiGianDuKienTraPhong),
                    new SqlParameter("@ThoiGianThucTeTraPhong", chiTiet.ThoiGianThucTeTraPhong ?? (object)DBNull.Value),
                    new SqlParameter("@TinhTrang", chiTiet.TinhTrang)
                };

                int rowsAffected = dbHelper.ExecuteInsertAndGetIdentity(query, parameters);

                return rowsAffected;
            
        }



        public DataTable LayDanhSachChiTietLuuTruTheoMaDatPhong(int maDatPhong)
        {
           
            string query = "SELECT * FROM ChiTietLuuTru WHERE MaDatPhong = @MaDatPhong";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@MaDatPhong", maDatPhong)
            };
            return dbHelper.ExecuteQuery(query, parameters);
        }


        public DataTable LayThongTinKhachHangTheoMaDatPhong(int maDatPhong)
        {
            string sqlQuery = "SELECT KhachHang.* " +
                              "FROM KhachHang " +
                              "JOIN DatPhong ON KhachHang.MaKhachHang = DatPhong.MaKhachHang " +
                              "WHERE DatPhong.MaDatPhong = @MaDatPhong";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@MaDatPhong", maDatPhong)
            };
            
            return dbHelper.ExecuteQuery(sqlQuery, parameters);
        }



    }
}
