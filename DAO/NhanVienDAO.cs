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
    public class NhanVienDAO
    {
        private DatabaseHelper dbHelper;

        public NhanVienDAO(string connectionString)
        {
            dbHelper = new DatabaseHelper(connectionString);
        }

        public List<NhanVienDTO> GetAllNhanVien()
        {
            List<NhanVienDTO> nhanVienList = new List<NhanVienDTO>();

            
                DataTable data = dbHelper.ExecuteQuery("SELECT MaNhanVien, TenDangNhap, MatKhau, HoTen, ChucVu, Luong FROM NhanVien");

                foreach (DataRow row in data.Rows)
                {
                    NhanVienDTO nhanVien = new NhanVienDTO
                    {
                        MaNhanVien = Convert.ToInt32(row["MaNhanVien"]),
                        TenDangNhap = row["TenDangNhap"].ToString(),
                        MatKhau = row["MatKhau"].ToString(),
                        HoTen = row["HoTen"].ToString(),
                        ChucVu = row["ChucVu"].ToString(),
                        Luong = Convert.ToDecimal(row["Luong"])
                    };
                    nhanVienList.Add(nhanVien);
                }
            

            return nhanVienList;
        }


        public DataTable GetAllNhanVienDataTable ()
        {
            string query = "SELECT * FROM NhanVien";
            return dbHelper.ExecuteQuery(query);
        }

        public DataTable isLoginDAO(string taikhoan,string matkhau)
        {
            
            
            string query = @"SELECT * FROM NhanVien WHERE NhanVien.TenDangNhap = @TenDangNhap AND NhanVien.MatKhau = @MatKhau";
            SqlParameter[] parameters = new SqlParameter[]
           {
                new SqlParameter("@TenDangNhap", taikhoan),
                new SqlParameter("@MatKhau", matkhau)
           };
            return dbHelper.ExecuteQuery(query, parameters);
        }

        

        public int InsertNhanVien(string tenDangNhap, string matKhau, string hoTen, string chucVu, decimal luong, string pathAvatar)
        {
            string query = "INSERT INTO NhanVien (TenDangNhap, MatKhau, HoTen, ChucVu, Luong, PathAvatar) " +
                           "VALUES (@TenDangNhap, @MatKhau, @HoTen, @ChucVu, @Luong, @PathAvatar)";

            SqlParameter[] parameters = new SqlParameter[]
            {
        new SqlParameter("@TenDangNhap", tenDangNhap),
        new SqlParameter("@MatKhau", matKhau),
        new SqlParameter("@HoTen", hoTen),
        new SqlParameter("@ChucVu", chucVu),
        new SqlParameter("@Luong", luong),
        new SqlParameter("@PathAvatar", pathAvatar)
            };

            return dbHelper.ExecuteNonQuery(query, parameters);
        }

        public bool UpdateNhanVien(NhanVienDTO nhanVien)
        {

            string query = "UPDATE NhanVien SET TenDangNhap = @TenDangNhap, MatKhau = @MatKhau, HoTen = @HoTen, " +
                   "ChucVu = @ChucVu, Luong = @Luong, PathAvatar = @PathAvatar WHERE MaNhanVien = @MaNhanVien";

            SqlParameter[] parameters = new SqlParameter[]
            {
        new SqlParameter("@TenDangNhap", nhanVien.TenDangNhap),
        new SqlParameter("@MatKhau", nhanVien.MatKhau),
        new SqlParameter("@HoTen", nhanVien.HoTen),
        new SqlParameter("@ChucVu", nhanVien.ChucVu),
        new SqlParameter("@Luong", nhanVien.Luong),
        new SqlParameter("@MaNhanVien", nhanVien.MaNhanVien),
        new SqlParameter("@PathAvatar", nhanVien.PathAvatar)
            };

            return dbHelper.ExecuteNonQuery(query, parameters) > 0;

        }

        public bool DeleteNhanVien(int maNhanVien)
        {
            
                string query = "DELETE FROM NhanVien WHERE MaNhanVien = @MaNhanVien";

                SqlParameter parameter = new SqlParameter("@MaNhanVien", maNhanVien);

                return dbHelper.ExecuteNonQuery(query, new SqlParameter[] { parameter }) > 0;
            
        }

        public DataTable TimKiemNhanVienTheoTuKhoa(string tuKhoa)
        {
          
            string query = "SELECT * FROM NhanVien WHERE HoTen LIKE @TuKhoa";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@TuKhoa", "%" + tuKhoa + "%")
            };

            return dbHelper.ExecuteQuery(query, parameters);
        }

        public bool IsUsernameExists(string tenDangNhap)
        {
            string query = "SELECT COUNT(*) FROM NhanVien WHERE TenDangNhap = @TenDangNhap";

            SqlParameter parameter = new SqlParameter("@TenDangNhap", tenDangNhap);

            int count = Convert.ToInt32(dbHelper.ExecuteScalar(query, new SqlParameter[] { parameter }));

            return count > 0;
        }


        public string GetPathAvatarForNhienVien(int maTaiKhoan)
        {
            string query = "SELECT NhanVien.PathAvatar FROM NhanVien WHERE MaNhanVien = @MaTaiKhoan";

            SqlParameter[] parameters = new SqlParameter[]
            {
        new SqlParameter("@MaTaiKhoan", maTaiKhoan)
            };
            DataTable pathAvatar = dbHelper.ExecuteQuery(query, parameters);

            if (pathAvatar.Rows.Count > 0)
            {
                return pathAvatar.Rows[0]["PathAvatar"].ToString();
            }
            return null;
        }


    }
}
