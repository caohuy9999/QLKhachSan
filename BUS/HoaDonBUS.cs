using BUS.ClassHelper;
using DAO;
using DTO;
using GUI;
using OfficeOpenXml.Style;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Bunifu.Framework.UI;
using WindowsFormsControlLibrary1;

namespace BUS
{
    public class HoaDonBUS
    {
        private HoaDonDAO hoaDonDAO;
        private ChiTietLuuTruBUS chitietluutruBUS;
        private ChiTietHoaDonBUS chitiethoadon;
        public HoaDonBUS(string connectionString)
        {
            chitietluutruBUS = new ChiTietLuuTruBUS(connectionString);
            chitiethoadon = new ChiTietHoaDonBUS(connectionString); 
            hoaDonDAO = new HoaDonDAO(connectionString);
        }

        public int ThemHoaDon(HoaDonDTO hoaDon)
        {
            int rowsAffected = hoaDonDAO.ThemHoaDon(hoaDon);
            return rowsAffected;
        }

        public bool CongDonTongTienDichVu(int maHoaDon, decimal soTienThem)
        {
            return hoaDonDAO.CongDonTongTienDichVu(maHoaDon, soTienThem);
        }

        public HoaDonDTO LayHoaDonTheoMa(int MAhoaDon)
        {
            DataTable Data = hoaDonDAO.LayHoaDonTheoMa(MAhoaDon);
            if (Data.Rows.Count > 0)
            {
                // Lấy dòng đầu tiên từ bảng kết quả (do bạn có thể có nhiều hóa đơn với cùng mã)
                DataRow row = Data.Rows[0];

                // Tạo đối tượng HoaDonDTO từ dữ liệu trong dòng
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

                return hoaDon;
            }
            return null; // Trả về null nếu không tìm thấy hóa đơn với mã đã cho
           
        }


        public bool ThanhToan(int maHoaDon, decimal TongTienDV, string phuongThucThanhToan, string tinhtrang = "", DateTime timehientai = default)
        {
            // Lấy thông tin hóa đơn theo mã
            HoaDonDTO hoaDon = LayHoaDonTheoMa(maHoaDon);

            if (hoaDon != null)
            {
                // Kiểm tra xem hóa đơn đã thanh toán chưa
                if (hoaDon.DaThanhToan)
                {
                    return false; // thanh toán trước đó r
                }

             
                bool thanhToanThanhCong = false;
                if (phuongThucThanhToan == "Tiền Mặt")
                {
                    if (timehientai == default)
                    {
                        timehientai = DateTime.Now;
                    }
                    // tính tiền
                    // đơn giá phòng
                    
                    decimal TongTienPhong = Common.TinhGiaPhong(hoaDon.NgayLapHoaDon, timehientai, hoaDonDAO.LayGiaTheoDemTheoMaDatPhong(hoaDon.MaDatPhong));
                    decimal TongTienHoaDon = TongTienPhong + TongTienDV;
                    // Thực hiện thanh toán bằng tiền mặt
                    if (TongTienHoaDon >= hoaDon.TongTien)
                    {
                        hoaDon.DaThanhToan = true;
                        hoaDon.TongTienPhong = TongTienPhong;
                        hoaDon.TongTienDichVu = TongTienDV;
                        hoaDon.TinhTrang = tinhtrang;
                        if (tinhtrang == "")
                            hoaDon.TinhTrang = phuongThucThanhToan;

                        thanhToanThanhCong = hoaDonDAO.CapNhatHoaDon(hoaDon);
                    }

                }
                else if (phuongThucThanhToan == "Thẻ Tín Dụng")
                {
                    //  gọi dịch vụ thanh toán của bên thứ ba 
                }

                return thanhToanThanhCong;
            }
            else
            {
                return false; // Không tìm thấy hóa đơn với mã đã cho
            }
        }


        public void displayViewDatagirdView(DataGridView dtgv, DateTime StartTime = default, DateTime EndTime = default, string tinhtrangthanhtoan = "", string trangthaithanhtoan = "", string nhanvienthanhtoan = "", string maphong = "")
        {
            DataTable Data = hoaDonDAO.GetAllHoaDon_DataTable(StartTime,EndTime,tinhtrangthanhtoan,trangthaithanhtoan,nhanvienthanhtoan,maphong);
            dtgv.Rows.Clear();

            if (Data.Rows.Count > 0)
            {
                int y = 0;
                foreach (DataRow row in Data.Rows)
                {
                    y++;
                    dtgv.Rows.Add(
                           y,
                           Image.FromFile(@"img\info_25px.png"),
                           row["MaHoaDon"].ToString(),
                           row["MaPhong"].ToString(),
                           row["SoPhong"].ToString(),
                           row["MaDatPhong"].ToString(),
                           row["MaKhachHang"].ToString(),
                           row["HoTenKH"].ToString(),
                           row["MaNhanVien"].ToString(),
                           row["HoTenNV"].ToString(),
                           row["NgayLapHoaDon"].ToString(),
                           row["TongTienPhong"].ToString(),
                           row["TongTienDichVu"].ToString(),
                           row["TongTien"].ToString(),
                           row["TinhTrangThanhToan"].ToString(),
                           row["TrangThaiThanhToan"].ToString()
                           );
                }

                // Cân chỉnh dữ liệu trong DataGridView
                DatagridviewHelper.TitleCenter(dtgv);
                DatagridviewHelper.DataCenter(dtgv, true);
            }
        ;
        }

        public void dumpComboboxDataTinhTrang(ComboBox cbb)
        {
          DataTable data  =  hoaDonDAO.GetAllBill_CBB_tinhtrang();
            cbb.Items.Clear(); 
            cbb.Items.Add("All");
            if (data.Rows.Count > 0)
            {
                foreach (DataRow row in data.Rows)
                {
                    cbb.Items.Add(row["TinhTrang"].ToString());
                }
                cbb.SelectedIndex = 0;
            }
            
        }
        public void dumpComboboxDataTrangThai(ComboBox cbb)
        {
            DataTable data = hoaDonDAO.GetAllBill_CBB_trangthai();
            cbb.Items.Clear();
            cbb.Items.Add("All");
            if (data.Rows.Count > 0)
            {
                foreach (DataRow row in data.Rows)
                {
                    if (Convert.ToBoolean(row["DaThanhToan"].ToString()))
                    {
                        cbb.Items.Add("Đã Thanh Toán");
                    }
                    else
                    {
                        cbb.Items.Add("Chưa Thanh Toán");
                    }
                }
                cbb.SelectedIndex = 0;
            }

        }


        public DataTable CustomCloneDataGridView(DataGridView originalDataGridView)
        {
            // Tạo một DataTable mới từ dữ liệu của DataGridView
            DataTable dataTable = new DataTable();

            foreach (DataGridViewColumn col in originalDataGridView.Columns)
            {
                // Kiểm tra cột có tên không mong muốn
                if (col.Name != "cIMGTienNghi" && col.Name != "cpathIMG" && col.Name != "cEdit" && col.Name != "cDelete" && col.Name != "cIMG")
                {
                    dataTable.Columns.Add(col.Name);
                }
            }

            foreach (DataGridViewRow row in originalDataGridView.Rows)
            {
                DataRow dataRow = dataTable.NewRow();
                for (int i = 0; i < originalDataGridView.Columns.Count; i++)
                {
                    if (originalDataGridView.Columns[i].Name != "cIMGTienNghi" && originalDataGridView.Columns[i].Name != "cpathIMG" && originalDataGridView.Columns[i].Name != "cEdit" && originalDataGridView.Columns[i].Name != "cDelete" && originalDataGridView.Columns[i].Name != "cIMG")
                    {
                        dataRow[originalDataGridView.Columns[i].Name] = row.Cells[i].Value;
                    }
                }
                dataTable.Rows.Add(dataRow);
            }

            return dataTable;
        }


        public void ExportToExcel(DataTable dataTable, string title, string filePath)
        {
            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial; // Đặt LicenseContext thành NonCommercial
                                                                                      // Create a new Excel package
            using (var package = new ExcelPackage())
            {
                // Add a new worksheet
                var worksheet = package.Workbook.Worksheets.Add("Sheet1");

                // Set the title
                worksheet.Cells["A1"].Value = title;
                worksheet.Cells["A1:H1"].Merge = true;
                worksheet.Cells["A1:H1"].Style.Font.Size = 16;
                worksheet.Cells["A1:H1"].Style.Font.Bold = true;
                worksheet.Cells["A1:H1"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                // Export DataTable data to Excel
                int rowStart = 3;
                int colStart = 1;

                // Header row
                for (int col = 0; col < dataTable.Columns.Count; col++)
                {

                    worksheet.Cells[rowStart, colStart + col].Value = dataTable.Columns[col].ColumnName;
                    worksheet.Cells[rowStart, colStart + col].Style.Font.Bold = true;
                    worksheet.Cells[rowStart, colStart + col].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                }

                // Data rows
                for (int row = 0; row < dataTable.Rows.Count; row++)
                {
                    for (int col = 0; col < dataTable.Columns.Count; col++)
                    {
                        worksheet.Cells[rowStart + row + 1, colStart + col].Value = dataTable.Rows[row][col];
                        worksheet.Cells[rowStart + row + 1, colStart + col].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                    }
                }
                // AutoFit columns
                worksheet.Cells.AutoFitColumns();

                // Save the file
                string pathFileExcel = filePath + "\\Báo Cáo Tiện Nghi Phòng " + DateTime.Now.ToString("dd-MM-yyyy HH_mm_ss") + ".xlsx";
                package.SaveAs(new FileInfo(pathFileExcel));

                if (Common.FileTonTai(pathFileExcel))
                {
                    if (MessageBoxHelper.Show("Đã Xuất File thành công, mở File ngay lập tức?") == DialogResult.Yes)
                    {
                        try
                        {
                            Process.Start(pathFileExcel);
                        }
                        catch (Exception ex)
                        {
                            // Xử lý lỗi nếu tệp Excel không thể mở.
                            MessageBoxHelper.Show("Không thể mở tệp Excel. Lỗi: " + ex.Message);
                        }
                    }
                }
                else
                {
                    MessageBoxHelper.ShowMessageBox("Đã Xuất File Thất Bại, Thử lại sau!");
                }
            }
        }

        public void XuatHoaDon(DataGridView dtgv, int idhoadon, int maphong, DateTime timehientai, Label lblNameKH, Label lblStopByNight, Label lblNumberNguoi, Label DateTimeStarts, BunifuCustomLabel lblTongTienDV, Label lblSoHoaDon, Label NameNV , Label SOpHONG)
        {
            DataTable data = hoaDonDAO.LayThongTinHoaDon(idhoadon);
            chitiethoadon.DisplayChiTietHoaDonOnDataGridView(idhoadon, dtgv, lblTongTienDV);
            HoaDonDTO infoBill = LayHoaDonTheoMa(idhoadon);
            lblSoHoaDon.Text = infoBill.MaHoaDon.ToString();
            lblNameKH.Text = data.Rows[0]["HoTen1"].ToString();
            lblStopByNight.Text = chitietluutruBUS.tinhsoNgay(Common.ConvertToDateTime(data.Rows[0]["ThoiGianVaoPhong"]), timehientai).ToString();
            lblNumberNguoi.Text = data.Rows[0]["SoLuongNguoi"].ToString();
            DateTimeStarts.Text = infoBill.NgayLapHoaDon.ToString();



            lblTongTienDV.Text = (Common.TinhGiaPhong(Common.ConvertToDateTime(data.Rows[0]["ThoiGianVaoPhong"]), timehientai, Common.ConvertToDecimal(data.Rows[0]["GiaTheoDem"])) + Common.ConvertToDecimal(data.Rows[0]["TongTienDichVu"])).ToString("#,##0")+"VNĐ";
            NameNV.Text = data.Rows[0]["HoTen"].ToString();
            SOpHONG.Text = data.Rows[0]["SoPhong"].ToString();

            //chitietluutruBUS.LayDanhSachChiTietLuuTrubyMaDatPhong(maphong, lblNameKH, null, lblStopByNight, lblNumberNguoi, DateTimeStarts); // view lên lbl
           
        }

        public int getSoPhongByMaHoaDon(int mahoadon)
        {
            return hoaDonDAO.getSoPhongByMaHoaDon(mahoadon);

        }

    }

}
