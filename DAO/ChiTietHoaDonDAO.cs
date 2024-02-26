using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using DTO;

namespace DAO
{
    public class ChiTietHoaDonDAO
    {
        private DatabaseHelper databaseHelper;

        public ChiTietHoaDonDAO(string connectionString)
        {
            databaseHelper = new DatabaseHelper(connectionString);
        }

        // get id luon
        public int ThemChiTietHoaDon(ChiTietHoaDonDTO chiTietHoaDon) 
        {
            string query = "INSERT INTO ChiTietHoaDon (MaHoaDon, MaDichVu, SoLuong, DonGia, ThanhTien) VALUES (@MaHoaDon, @MaDichVu, @SoLuong, @DonGia, @ThanhTien)";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@MaHoaDon", chiTietHoaDon.MaHoaDon),
                new SqlParameter("@MaDichVu", chiTietHoaDon.MaDichVu),
                new SqlParameter("@SoLuong", chiTietHoaDon.SoLuong),
                new SqlParameter("@DonGia", chiTietHoaDon.DonGia),
                new SqlParameter("@ThanhTien", chiTietHoaDon.ThanhTien)
            };

            return databaseHelper.ExecuteInsertAndGetIdentity(query, parameters);
        }

        public int CapNhatChiTietHoaDon(ChiTietHoaDonDTO chiTietHoaDon)
        {
            string query = "UPDATE ChiTietHoaDon SET MaHoaDon = @MaHoaDon, MaDichVu = @MaDichVu, SoLuong = @SoLuong, DonGia = @DonGia, ThanhTien = @ThanhTien WHERE MaChiTietHoaDon = @MaChiTietHoaDon";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@MaChiTietHoaDon", chiTietHoaDon.MaChiTietHoaDon),
                new SqlParameter("@MaHoaDon", chiTietHoaDon.MaHoaDon),
                new SqlParameter("@MaDichVu", chiTietHoaDon.MaDichVu),
                new SqlParameter("@SoLuong", chiTietHoaDon.SoLuong),
                new SqlParameter("@DonGia", chiTietHoaDon.DonGia),
                new SqlParameter("@ThanhTien", chiTietHoaDon.ThanhTien)
            };

            return databaseHelper.ExecuteNonQuery(query, parameters);
        }

        public int XoaChiTietHoaDon(int maChiTietHoaDon)
        {
            string query = "DELETE FROM ChiTietHoaDon WHERE MaChiTietHoaDon = @MaChiTietHoaDon";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@MaChiTietHoaDon", maChiTietHoaDon)
            };

            return databaseHelper.ExecuteNonQuery(query, parameters);
        }

        public List<ChiTietHoaDonDTO> LayDanhSachChiTietHoaDonTheoMaHoaDon(int maHoaDon)
        {
            string query = "SELECT * FROM ChiTietHoaDon WHERE MaHoaDon = @MaHoaDon";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@MaHoaDon", maHoaDon)
            };

            DataTable dataTable = databaseHelper.ExecuteQuery(query, parameters);

            List<ChiTietHoaDonDTO> danhSachChiTietHoaDon = new List<ChiTietHoaDonDTO>();

            foreach (DataRow row in dataTable.Rows)
            {
                ChiTietHoaDonDTO chiTietHoaDon = new ChiTietHoaDonDTO
                {
                    MaChiTietHoaDon = Convert.ToInt32(row["MaChiTietHoaDon"]),
                    MaHoaDon = Convert.ToInt32(row["MaHoaDon"]),
                    MaDichVu = Convert.ToInt32(row["MaDichVu"]),
                    SoLuong = Convert.ToInt32(row["SoLuong"]),
                    DonGia = Convert.ToDecimal(row["DonGia"]),
                    ThanhTien = Convert.ToDecimal(row["ThanhTien"])
                };

                danhSachChiTietHoaDon.Add(chiTietHoaDon);
            }

            return danhSachChiTietHoaDon;
        }

        public List<ChiTietHoaDonDTO> GetChiTietHoaDonByMaHoaDon(int maHoaDon)
        {
            List<ChiTietHoaDonDTO> chiTietHoaDonList = new List<ChiTietHoaDonDTO>();


            string query = @" SELECT CTHD.MaChiTietHoaDon,CTHD.MaHoaDon,CTHD.MaDichVu,CTHD.SoLuong,CTHD.DonGia,CTHD.ThanhTien,
                DVDDS.MaSuDung,DVDDS.MaDatPhong,DVDDS.NgaySuDung,
                DV.TenDichVu
                FROM ChiTietHoaDon AS CTHD
                JOIN DichVuDuocSuDung AS DVDDS ON CTHD.MaChiTietHoaDon = DVDDS.MaChiTietHoaDon
                JOIN DichVu AS DV ON CTHD.MaDichVu = DV.MaDichVu
                WHERE CTHD.MaHoaDon = @MaHoaDon;";

            SqlParameter[] parameters = { new SqlParameter("@MaHoaDon", maHoaDon) };

                DataTable dataTable = databaseHelper.ExecuteQuery(query, parameters);

                foreach (DataRow row in dataTable.Rows)
                {
                    ChiTietHoaDonDTO chiTietHoaDon = new ChiTietHoaDonDTO
                    {
                        MaChiTietHoaDon = Convert.ToInt32(row["MaChiTietHoaDon"]),
                        MaHoaDon = Convert.ToInt32(row["MaHoaDon"]),
                        MaDichVu = Convert.ToInt32(row["MaDichVu"]),
                        SoLuong = Convert.ToInt32(row["SoLuong"]),
                        DonGia = Convert.ToDecimal(row["DonGia"]),
                        ThanhTien = Convert.ToDecimal(row["ThanhTien"]),
                        DichVuDuocSuDung = new DichVuSuDungDTO
                        {
                            MaSuDung = Convert.ToInt32(row["MaSuDung"]),
                            MaDatPhong = Convert.ToInt32(row["MaDatPhong"]),
                            NgaySuDung = Convert.ToDateTime(row["NgaySuDung"]),
                        },
                        DichVu = new DichVuDTO
                        {
                            TenDichVu = row["TenDichVu"].ToString()
                        }
                    };

                    chiTietHoaDonList.Add(chiTietHoaDon);
                }
            

            return chiTietHoaDonList;
        }
        // Thêm các phương thức khác cho tương tác với bảng ChiTietHoaDon nếu cần.
    }
}
