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
    public class KhachHangDAO
    {
        private DatabaseHelper dbHelper;

        public KhachHangDAO(string connectionString)
        {
            dbHelper = new DatabaseHelper(connectionString);
        }

        public List<KhachHangDTO> LayDanhSachKhachHang()
        {
            string query = "SELECT * FROM KhachHang";
            DataTable dataTable = dbHelper.ExecuteQuery(query);

            List<KhachHangDTO> danhSachKhachHang = new List<KhachHangDTO>();

            foreach (DataRow row in dataTable.Rows)
            {
                KhachHangDTO khachHang = new KhachHangDTO
                {
                    MaKhachHang = Convert.ToInt32(row["MaKhachHang"]),
                    HoTen = row["HoTen"].ToString(),
                    Email = row["Email"].ToString(),
                    SoDienThoai = row["SoDienThoai"].ToString(),
                    DiaChi = row["DiaChi"].ToString(),
                    LoaiKhachHang = row["LoaiKhachHang"].ToString()
                };

                danhSachKhachHang.Add(khachHang);
            }

            return danhSachKhachHang;
        }

        public DataTable LayDanhSachKhachHangDataTable()
        {
            return dbHelper.ExecuteQuery("SELECT * FROM KhachHang");
        }



        public KhachHangDTO LayKhachHangTheoEmail(string email)
        {
            string query = "SELECT * FROM KhachHang WHERE Email = @Email";
            SqlParameter[] parameters = new SqlParameter[]
            {
        new SqlParameter("@Email", SqlDbType.NVarChar) { Value = email }
            };

            DataTable result = dbHelper.ExecuteQuery(query, parameters);

            // Kiểm tra xem có bất kỳ bản ghi nào được trả về không
            if (result.Rows.Count > 0)
            {
                DataRow row = result.Rows[0]; // Lấy bản ghi đầu tiên

                // Tạo một đối tượng KhachHangDTO và chuyển dữ liệu từ DataRow vào đối tượng này
                KhachHangDTO khachHang = new KhachHangDTO
                {
                    MaKhachHang = (int)row["MaKhachHang"],
                    HoTen = row["HoTen"].ToString(),
                    Email = row["Email"].ToString(),
                    SoDienThoai = row["SoDienThoai"].ToString(),
                    DiaChi = row["DiaChi"].ToString(),
                    LoaiKhachHang = row["LoaiKhachHang"].ToString()
                };

                return khachHang;
            }
            else
            {
                return null; // Trả về null nếu không tìm thấy khách hàng nào có địa chỉ email tương ứng
            }
        }




        public DataTable TimKiemKhachHangTheoTuKhoa(string tuKhoa)
        {
            string query = "SELECT KhachHang.* FROM KhachHang WHERE HoTen LIKE @TuKhoa OR Email LIKE @TuKhoa";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@TuKhoa", "%" + tuKhoa + "%")
            };

            DataTable data = dbHelper.ExecuteQuery(query, parameters);
            return data;
        }


        public int ThemKhachHang(KhachHangDTO khachHang)
        {
           
                string query = "INSERT INTO KhachHang (HoTen, Email, SoDienThoai, DiaChi, LoaiKhachHang) " +
                               "VALUES (@HoTen, @Email, @SoDienThoai, @DiaChi, @LoaiKhachHang); " +
                               "SELECT SCOPE_IDENTITY();"; // Lấy giá trị ID vừa thêm

                SqlParameter[] parameters = new SqlParameter[]
                {
            new SqlParameter("@HoTen", khachHang.HoTen),
            new SqlParameter("@Email", khachHang.Email),
            new SqlParameter("@SoDienThoai", khachHang.SoDienThoai),
            new SqlParameter("@DiaChi", khachHang.DiaChi),
            new SqlParameter("@LoaiKhachHang", khachHang.LoaiKhachHang)
                };

                // Thực hiện truy vấn và trả về ID mới thêm
                int newKhachHangId = Convert.ToInt32(dbHelper.ExecuteScalar(query, parameters));

                return newKhachHangId;
            
        }


        public KhachHangDTO KiemTraVaLayKhachHang(string email,string name , string sdt , string diachi)
        {
            
                string query = "SELECT * FROM KhachHang WHERE Email = @Email";
                SqlParameter[] parameters = new SqlParameter[] { new SqlParameter("@Email", email) };

                // Thực hiện truy vấn để kiểm tra khách hàng dựa trên Email
                DataTable result = dbHelper.ExecuteQuery(query, parameters);

                if (result.Rows.Count > 0)
                {
                    // Khách hàng đã tồn tại, lấy thông tin từ cơ sở dữ liệu và trả về
                    KhachHangDTO existingKhachHang = new KhachHangDTO
                    {
                        MaKhachHang = Convert.ToInt32(result.Rows[0]["MaKhachHang"]),
                        HoTen = result.Rows[0]["HoTen"].ToString(),
                        Email = result.Rows[0]["Email"].ToString(),
                        SoDienThoai = result.Rows[0]["SoDienThoai"].ToString(),
                        DiaChi = result.Rows[0]["DiaChi"].ToString(),
                        // Các trường thông tin khác
                    };
                    return existingKhachHang;
                }
                else
                {
                    // Khách hàng chưa tồn tại, bạn có thể thêm mới
                    KhachHangDTO newKhachHang = new KhachHangDTO
                    {
                        HoTen = name,
                        Email = email,  // Sử dụng Email đã truyền vào
                        SoDienThoai = sdt,
                        DiaChi = diachi,
                        LoaiKhachHang = "Thường"
                    };

                    // Thực hiện thêm mới khách hàng và lấy ID sau khi thêm mới
                    int newKhachHangId = ThemKhachHang(newKhachHang);
                    newKhachHang.MaKhachHang = newKhachHangId;

                    return newKhachHang;
                }
            
        }















        // Sửa thông tin của một khách hàng trong cơ sở dữ liệu
        public bool SuaThongTinKhachHang(KhachHangDTO khachHang)
        {
            string query = "UPDATE KhachHang " +
                           "SET HoTen = @HoTen, Email = @Email, SoDienThoai = @SoDienThoai, DiaChi = @DiaChi, LoaiKhachHang = @LoaiKhachHang " +
                           "WHERE MaKhachHang = @MaKhachHang";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@MaKhachHang", khachHang.MaKhachHang),
                new SqlParameter("@HoTen", khachHang.HoTen),
                new SqlParameter("@Email", khachHang.Email),
                new SqlParameter("@SoDienThoai", khachHang.SoDienThoai),
                new SqlParameter("@DiaChi", khachHang.DiaChi),
                new SqlParameter("@LoaiKhachHang", khachHang.LoaiKhachHang)
            };

            int rowsAffected = dbHelper.ExecuteNonQuery(query, parameters);

            return rowsAffected > 0;
        }


        // Xóa một khách hàng khỏi cơ sở dữ liệu
        public bool XoaKhachHang(int maKhachHang)
        {
            string query = "DELETE FROM KhachHang WHERE MaKhachHang = @MaKhachHang";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@MaKhachHang", maKhachHang)
            };

            int rowsAffected = dbHelper.ExecuteNonQuery(query, parameters);

            return rowsAffected > 0;
        }


        // Lấy thông tin của một khách hàng bằng mã khách hàng
        public KhachHangDTO LayThongTinKhachHang(int maKhachHang)
        {
            string query = "SELECT * FROM KhachHang WHERE MaKhachHang = @MaKhachHang";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@MaKhachHang", maKhachHang)
            };

            DataTable dataTable = dbHelper.ExecuteQuery(query, parameters);

            if (dataTable.Rows.Count > 0)
            {
                DataRow row = dataTable.Rows[0];

                KhachHangDTO khachHang = new KhachHangDTO
                {
                    MaKhachHang = Convert.ToInt32(row["MaKhachHang"]),
                    HoTen = row["HoTen"].ToString(),
                    Email = row["Email"].ToString(),
                    SoDienThoai = row["SoDienThoai"].ToString(),
                    DiaChi = row["DiaChi"].ToString(),
                    LoaiKhachHang = row["LoaiKhachHang"].ToString()
                };

                return khachHang;
            }

            return null;
        }


        // Lấy id vua Inserted vaof :D
        public int LayLastInsertedID()
        {
            try
            {
                string query = "SELECT SCOPE_IDENTITY() AS LastInsertedID";

                DataTable dataTable = dbHelper.ExecuteQuery(query);

                if (dataTable.Rows.Count > 0)
                {
                    return Convert.ToInt32(dataTable.Rows[0]["LastInsertedID"]);
                }
            }
            catch (Exception)
            {

               
            }
         

            return -1; // Trả về -1 nếu không lấy được giá trị ID
        }


        public string[] LayDanhSachEmailKhachHang()
        {
            List<string> danhSachEmail = new List<string>();
                string query = "SELECT Email FROM KhachHang";
               DataTable  DT = dbHelper.ExecuteQuery(query);
            for (int i = 0; i < DT.Rows.Count; i++)
            {
                string email = DT.Rows[i]["Email"].ToString();
                danhSachEmail.Add(email);
            }
            return danhSachEmail.ToArray();
        }
    }
}
