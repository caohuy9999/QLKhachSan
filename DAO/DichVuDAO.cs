using DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using static DTO.DichVuDTO;

namespace DAO
{
    public class DichVuDAO
    {
        private string connectionString;
        private DatabaseHelper dbHelper;
        public DichVuDAO(string connectionString)
        {
            this.connectionString = connectionString;
            dbHelper = new DatabaseHelper(connectionString);
        }

        // Thêm một dịch vụ mới và trả về MaDichVu mới được tạo
        public int ThemDichVu(string tenDichVu, decimal gia, int maLoaiDichVu)
        {
            string query = "INSERT INTO DichVu (TenDichVu, Gia, MaLoaiDichVu) VALUES (@TenDichVu, @Gia, @MaLoaiDichVu); SELECT SCOPE_IDENTITY();";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@TenDichVu", tenDichVu),
                new SqlParameter("@Gia", gia),
                new SqlParameter("@MaLoaiDichVu", maLoaiDichVu)
            };

            // Sử dụng DatabaseHelper để thực hiện truy vấn và lấy MaDichVu mới
            return dbHelper.ExecuteInsertAndGetIdentity(query, parameters);
        }

        // Cập nhật thông tin dịch vụ
        public bool CapNhatDichVu(int maDichVu, string tenDichVu, decimal gia, int maLoaiDichVu)
        {
            string query = "UPDATE DichVu SET TenDichVu = @TenDichVu, Gia = @Gia, MaLoaiDichVu = @MaLoaiDichVu WHERE MaDichVu = @MaDichVu";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@MaDichVu", maDichVu),
                new SqlParameter("@TenDichVu", tenDichVu),
                new SqlParameter("@Gia", gia),
                new SqlParameter("@MaLoaiDichVu", maLoaiDichVu)
            };

            // Sử dụng DatabaseHelper để thực hiện truy vấn
            DatabaseHelper dbHelper = new DatabaseHelper(connectionString);
            return dbHelper.ExecuteNonQuery(query, parameters) > 0;
        }

        // Xóa dịch vụ
        public bool XoaDichVu(int maDichVu)
        {
            string query = "DELETE FROM DichVu WHERE MaDichVu = @MaDichVu";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@MaDichVu", maDichVu)
            };

            // Sử dụng DatabaseHelper để thực hiện truy vấn
            DatabaseHelper dbHelper = new DatabaseHelper(connectionString);
            return dbHelper.ExecuteNonQuery(query, parameters) > 0;
        }

        // Lấy thông tin dịch vụ theo MaDichVu
        public DataTable LayThongTinDichVuTheoMa(int maDichVu)
        {
            string query = "SELECT * FROM DichVu WHERE MaDichVu = @MaDichVu";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@MaDichVu", maDichVu)
            };

            // Sử dụng DatabaseHelper để thực hiện truy vấn và lấy dữ liệu vào DataTable
            DatabaseHelper dbHelper = new DatabaseHelper(connectionString);
            return dbHelper.ExecuteQuery(query, parameters);
        }

        // Lấy thông tin dịch vụ theo MaLoaiDichVu
        public DataTable LayThongTinDichVuTheoMaLoaiDichVu(int MaLoaiDichVu, int maDichVu = -1)
        {
            string query = "SELECT * FROM DichVu WHERE MaLoaiDichVu = @MaLoaiDichVu";
            if (maDichVu != -1)
            {
                query = "SELECT * FROM DichVu WHERE MaLoaiDichVu = @MaLoaiDichVu AND MaDichVu = @MaDichVu";
                SqlParameter[] parameters = new SqlParameter[]
                {
                new SqlParameter("@MaLoaiDichVu", MaLoaiDichVu),
                new SqlParameter("@MaDichVu", maDichVu)
                };
                return dbHelper.ExecuteQuery(query, parameters);
            }
            else
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                new SqlParameter("@MaLoaiDichVu", MaLoaiDichVu)
                };
                return dbHelper.ExecuteQuery(query, parameters);
            }
        }

        // Lấy danh sách tất cả dịch vụ
        public DataTable LayDanhSachDichVu()
        {
            string query = "SELECT * FROM DichVu";

            // Sử dụng DatabaseHelper để thực hiện truy vấn và lấy dữ liệu vào DataTable
            return dbHelper.ExecuteQuery(query);
        }


        public DataTable TimKiemDichVuTheoTen(string tenDichVu)
        {
            // Sử dụng câu truy vấn SQL để tìm kiếm dịch vụ theo TenDichVu (có thể dùng LIKE để tìm kiếm một phần của tên dịch vụ)
            string query = "SELECT MaDichVu, TenDichVu, Gia, MaLoaiDichVu FROM DichVu WHERE TenDichVu LIKE @TenDichVu";
            SqlParameter[] parameters = new SqlParameter[]
           {
                new SqlParameter("@TenDichVu", "%" + tenDichVu + "%")
           };
            return dbHelper.ExecuteQuery(query, parameters);
        }

    }
}
