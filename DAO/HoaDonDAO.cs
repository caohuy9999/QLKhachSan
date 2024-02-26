using DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DAO
{
    public class HoaDonDAO
    {
        private DatabaseHelper dbHelper;

        public HoaDonDAO(string connectionString)
        {
            dbHelper = new DatabaseHelper(connectionString);
        }

        public int ThemHoaDon(HoaDonDTO hoaDon)
        {
            string query = "INSERT INTO HoaDon (MaDatPhong, MaKhachHang, MaNhanVien, NgayLapHoaDon, " +
                           "TongTienPhong, TongTienDichVu, TongTien, TinhTrang, DaThanhToan) " +
                           "VALUES (@MaDatPhong, @MaKhachHang, @MaNhanVien, @NgayLapHoaDon, " +
                           "@TongTienPhong, @TongTienDichVu, @TongTien, @TinhTrang, @DaThanhToan);";

            SqlParameter[] parameters = new SqlParameter[]
            {
            new SqlParameter("@MaDatPhong", hoaDon.MaDatPhong),
            new SqlParameter("@MaKhachHang", hoaDon.MaKhachHang),
            new SqlParameter("@MaNhanVien", hoaDon.MaNhanVien),
            new SqlParameter("@NgayLapHoaDon", hoaDon.NgayLapHoaDon),
            new SqlParameter("@TongTienPhong", hoaDon.TongTienPhong),
            new SqlParameter("@TongTienDichVu", hoaDon.TongTienDichVu),
            new SqlParameter("@TongTien", hoaDon.TongTien),
            new SqlParameter("@TinhTrang", hoaDon.TinhTrang),
            new SqlParameter("@DaThanhToan", hoaDon.DaThanhToan)
            };

            return dbHelper.ExecuteInsertAndGetIdentity(query, parameters);
        }

        // Thêm các phương thức khác để thực hiện các thao tác CRUD khác trên bảng Hóa đơn
        // Cập nhật hóa đơn
        public bool CapNhatHoaDon(HoaDonDTO hoaDon)
        {
                string query = "UPDATE HoaDon SET MaDatPhong = @MaDatPhong, MaKhachHang = @MaKhachHang, " +
                               "MaNhanVien = @MaNhanVien, NgayLapHoaDon = @NgayLapHoaDon, " +
                               "TongTienPhong = @TongTienPhong, TongTienDichVu = @TongTienDichVu, " +
                               "TongTien = @TongTien, TinhTrang = @TinhTrang, DaThanhToan = @DaThanhToan " +
                               "WHERE MaHoaDon = @MaHoaDon";
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@MaDatPhong", hoaDon.MaDatPhong),
                    new SqlParameter("@MaKhachHang", hoaDon.MaKhachHang),
                    new SqlParameter("@MaNhanVien", hoaDon.MaNhanVien),
                    new SqlParameter("@NgayLapHoaDon", hoaDon.NgayLapHoaDon),
                    new SqlParameter("@TongTienPhong", hoaDon.TongTienPhong),
                    new SqlParameter("@TongTienDichVu", hoaDon.TongTienDichVu),
                    new SqlParameter("@TongTien", hoaDon.TongTien),
                    new SqlParameter("@TinhTrang", hoaDon.TinhTrang),
                    new SqlParameter("@DaThanhToan", hoaDon.DaThanhToan),
                    new SqlParameter("@MaHoaDon", hoaDon.MaHoaDon)
                };

                return dbHelper.ExecuteNonQuery(query, parameters) > 0;
            
        }

        public bool CongDonTongTienDichVu(int maHoaDon, decimal soTienThem)
        {
            string query = "UPDATE HoaDon SET TongTienDichVu = TongTienDichVu + @SoTienThem WHERE MaHoaDon = @MaHoaDon";

            SqlParameter[] parameters = new SqlParameter[]
            {
        new SqlParameter("@SoTienThem", soTienThem),
        new SqlParameter("@MaHoaDon", maHoaDon)
            };

            return dbHelper.ExecuteNonQuery(query, parameters) > 0;
        }


        public DataTable LayDanhSachHoaDonTheoKhachHang(int maKhachHang)
        {
           
                string query = "SELECT * FROM HoaDon WHERE MaKhachHang = @MaKhachHang";
                SqlParameter[] parameters = new SqlParameter[] { new SqlParameter("@MaKhachHang", maKhachHang) };

                return dbHelper.ExecuteQuery(query, parameters);
            
        }


        public List<HoaDonDTO> LayHoaDonTheoNgayThangNam(DateTime ngayThangNam)
        {
            List<HoaDonDTO> danhSachHoaDon = new List<HoaDonDTO>();

            string query = "SELECT * FROM HoaDon WHERE CAST(NgayLapHoaDon AS DATE) = @NgayThangNam";
            SqlParameter[] parameters = new SqlParameter[]
            {
        new SqlParameter("@NgayThangNam", ngayThangNam.Date)
            };

            // Sử dụng DatabaseHelper để thực hiện truy vấn
            DataTable dataTable = dbHelper.ExecuteQuery(query, parameters);

            // Duyệt qua các dòng trong DataTable và chuyển chúng thành danh sách HoaDonDTO
            foreach (DataRow row in dataTable.Rows)
            {
                HoaDonDTO hoaDon = new HoaDonDTO();
                hoaDon.MaHoaDon = (int)row["MaHoaDon"];
                hoaDon.MaDatPhong = (int)row["MaDatPhong"];
                // Gán các trường dữ liệu khác tương tự ở đây và thêm vào danh sách

                danhSachHoaDon.Add(hoaDon);
            }

            return danhSachHoaDon;
        }

        // Lấy thông tin hóa đơn dựa trên mã hóa đơn
        public DataTable LayHoaDonTheoMa(int maHoaDon)
        {
            string query = "SELECT * FROM HoaDon WHERE MaHoaDon = @MaHoaDon";
            SqlParameter[] parameters = new SqlParameter[] { new SqlParameter("@MaHoaDon", maHoaDon) };
            return dbHelper.ExecuteQuery(query, parameters);
        }

        public decimal LayGiaTheoDemTheoMaDatPhong(int maDatPhong)
        {

            decimal giaTheoDem = 0;
            string query = @"SELECT LP.GiaTheoDem
                    FROM LoaiPhong LP
                    INNER JOIN Phong P ON LP.MaLoaiPhong = P.MaLoaiPhong
                    INNER JOIN DatPhong DP ON P.MaPhong = DP.MaPhong
                    WHERE DP.MaDatPhong = @MaDatPhong";
            SqlParameter[] parameters = new SqlParameter[]
            {
        new SqlParameter("@MaDatPhong", maDatPhong)
            };

            object result = dbHelper.ExecuteScalar(query, parameters);

            if (result != null && result != DBNull.Value)
            {
                giaTheoDem = Convert.ToDecimal(result);
            }
            else
            {
                // Xử lý trường hợp không có kết quả, ví dụ: giaTheoDem = 0 hoặc ném ngoại lệ.
            }

            return giaTheoDem;
        }



        public List<HoaDonDTO> GetAllHoaDon()
        {
            List<HoaDonDTO> hoaDonList = new List<HoaDonDTO>();
            string query = "SELECT * FROM HoaDon";
            using (DataTable dataTable = dbHelper.ExecuteQuery(query))
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    HoaDonDTO hoaDon = new HoaDonDTO
                    {
                        MaHoaDon = Convert.ToInt32(row["MaHoaDon"]),
                        MaDatPhong = Convert.ToInt32(row["MaDatPhong"]),
                        MaKhachHang = Convert.ToInt32(row["MaKhachHang"]),
                        MaNhanVien = Convert.ToInt32(row["MaNhanVien"]),
                        NgayLapHoaDon = Convert.ToDateTime(row["NgayLapHoaDon"]),
                        TongTienPhong = Convert.ToDecimal(row["TongTienPhong"]),
                        TongTienDichVu = Convert.ToDecimal(row["TongTienDichVu"]),
                        TongTien = Convert.ToDecimal(row["TongTien"]),
                        TinhTrang = row["TinhTrang"].ToString(),
                        DaThanhToan = Convert.ToBoolean(row["DaThanhToan"])
                    };

                    hoaDonList.Add(hoaDon);
                }
            }

            return hoaDonList;
        }

        public DataTable GetAllBill_CBB_tinhtrang()
        {
            return dbHelper.ExecuteQuery("SELECT DISTINCT HoaDon.TinhTrang FROM HoaDon");
        }
        public DataTable GetAllBill_CBB_trangthai()
        {
            return dbHelper.ExecuteQuery("SELECT DISTINCT HoaDon.DaThanhToan FROM HoaDon");
        }


        public DataTable GetAllHoaDon_DataTable(DateTime StartTime = default, DateTime EndTime = default, string tinhtrangthanhtoan = "", string trangthaithanhtoan = "", string nhanvienthanhtoan = "", string maphong = "")
        {
            string query = @"SELECT  HoaDon.MaHoaDon, 
                    Phong.SoPhong,
                    Phong.MaPhong,
                    HoaDon.MaDatPhong,
                    HoaDon.MaKhachHang,
                    KhachHang.HoTen AS HoTenKH,
                    HoaDon.MaNhanVien, 
                    NhanVien.HoTen AS HoTenNV,
                    HoaDon.NgayLapHoaDon, 
                    HoaDon.TongTienPhong,
                    HoaDon.TongTienDichVu,
                    HoaDon.TongTien,
                    HoaDon.TinhTrang AS TinhTrangThanhToan,
                    HoaDon.DaThanhToan AS TrangThaiThanhToan
                    FROM  HoaDon
                    INNER JOIN KhachHang ON HoaDon.MaKhachHang = KhachHang.MaKhachHang
                    INNER JOIN NhanVien ON HoaDon.MaNhanVien = NhanVien.MaNhanVien
                    INNER JOIN DatPhong ON HoaDon.MaDatPhong = DatPhong.MaDatPhong
                    INNER JOIN Phong ON DatPhong.MaPhong = Phong.MaPhong
                    WHERE 1 = 1";

            // Thêm điều kiện cho thời gian
            if (StartTime != default && EndTime != default)
            {
                query += $" AND HoaDon.NgayLapHoaDon >= '{StartTime.ToString("yyyy-MM-dd")}' AND HoaDon.NgayLapHoaDon <= '{EndTime.ToString("yyyy-MM-dd")}'";
                //   query += $" AND HoaDon.NgayLapHoaDon BETWEEN '{StartTime.ToString("yyyy-MM-dd")}' AND '{EndTime.ToString("yyyy-MM-dd")}'";
            }

            
            if (!string.IsNullOrEmpty(tinhtrangthanhtoan) && tinhtrangthanhtoan != "All")
            {
                query += $" AND HoaDon.DaThanhToan = {(tinhtrangthanhtoan == "Chưa Thanh Toán" ? 0 : 1)}";
            }

            // Thêm điều kiện cho trạng thái thanh toán
            if (!string.IsNullOrEmpty(trangthaithanhtoan) && trangthaithanhtoan != "All")
            {
                query += $" AND HoaDon.TinhTrang = N'{trangthaithanhtoan}'";
            }

            // Thêm điều kiện cho nhân viên thanh toán
            if (!string.IsNullOrEmpty(nhanvienthanhtoan) && nhanvienthanhtoan != "All")
            {
                query += $" AND NhanVien.HoTen = N'{nhanvienthanhtoan}'";
            }

            // Thêm điều kiện cho mã phòng
            if (!string.IsNullOrEmpty(maphong))
            {
                query += $" AND HoaDon.MaHoaDon LIKE N'%{maphong}%'";
            }

            // Thực hiện truy vấn
            return dbHelper.ExecuteQuery(query);
        }




        public DataTable LayThongTinHoaDon(int maHoaDon)
        {
            string query = @"SELECT hd.*,  nv.*,  ctl.*,  p.*,  kh.*, lp.*
FROM HoaDon hd
JOIN NhanVien nv ON hd.MaNhanVien = nv.MaNhanVien
JOIN DatPhong dp ON hd.MaDatPhong = dp.MaDatPhong
JOIN ChiTietLuuTru ctl ON dp.MaDatPhong = ctl.MaDatPhong
JOIN Phong p ON dp.MaPhong = p.MaPhong
JOIN KhachHang kh ON hd.MaKhachHang = kh.MaKhachHang
JOIN LoaiPhong lp ON p.MaLoaiPhong = lp.MaLoaiPhong
WHERE hd.MaHoaDon = @MaHoaDon";

            SqlParameter[] parameters = new SqlParameter[]
            {
        new SqlParameter("@MaHoaDon", maHoaDon)
            };

            return dbHelper.ExecuteQuery(query, parameters);
        }

        public int getSoPhongByMaHoaDon(int mahoadon)
        {
            string query = $@"SELECT Phong.SoPhong
            FROM HoaDon
            JOIN DatPhong ON HoaDon.MaDatPhong = DatPhong.MaDatPhong
            JOIN Phong ON DatPhong.MaPhong = Phong.MaPhong
            WHERE HoaDon.MaHoaDon = {mahoadon}";

            return Convert.ToInt32(dbHelper.ExecuteScalar(query));

        }

    }

}
