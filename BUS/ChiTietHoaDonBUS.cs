using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using Bunifu.Framework.UI;
using BUS.ClassHelper;
using DAO; // Import lớp DAO để sử dụng các phương thức truy xuất dữ liệu.
using DTO;
using OfficeOpenXml.Style;
using OfficeOpenXml;

namespace BUS
{
    public class ChiTietHoaDonBUS
    {
        private ChiTietHoaDonDAO chiTietHoaDonDAO;
        public ChiTietHoaDonBUS(string connectionString)
        {
            chiTietHoaDonDAO = new ChiTietHoaDonDAO(connectionString);
        }

        public int ThemChiTietHoaDon(ChiTietHoaDonDTO chiTietHoaDon)
        {
            return chiTietHoaDonDAO.ThemChiTietHoaDon(chiTietHoaDon);
        }

        public int CapNhatChiTietHoaDon(ChiTietHoaDonDTO chiTietHoaDon)
        {
            return chiTietHoaDonDAO.CapNhatChiTietHoaDon(chiTietHoaDon);
        }

        public int XoaChiTietHoaDon(int maChiTietHoaDon)
        {
            return chiTietHoaDonDAO.XoaChiTietHoaDon(maChiTietHoaDon);
        }

        public List<ChiTietHoaDonDTO> LayDanhSachChiTietHoaDonTheoMaHoaDon(int maHoaDon)
        {
            return chiTietHoaDonDAO.LayDanhSachChiTietHoaDonTheoMaHoaDon(maHoaDon);
        }

        public List<ChiTietHoaDonDTO> GetChiTietHoaDonByMaHoaDon(int maHoaDon)
        {
          
            return chiTietHoaDonDAO.GetChiTietHoaDonByMaHoaDon(maHoaDon);
        }


        public void DisplayChiTietHoaDonOnDataGridView(int maHoaDon, DataGridView dataGridView, BunifuCustomLabel lblTongTienDV,bool FromChiTietHoaDOn = false)
        {
            List<ChiTietHoaDonDTO> chiTietHoaDonList = GetChiTietHoaDonByMaHoaDon(maHoaDon);
            // Xoá dữ liệu hiện tại trên DataGridView
            dataGridView.Rows.Clear();
            decimal tongTienDV = 0; // Khởi tạo biến tổng tiền dịch vụ
            int y = 0;
            // Thêm dữ liệu từ danh sách ChiTietHoaDonDTO vào DataGridView
            foreach (ChiTietHoaDonDTO chiTietHoaDon in chiTietHoaDonList)
            {
                DataGridViewRow row = new DataGridViewRow();
                row.CreateCells(dataGridView); y++;
                row.Cells[0].Value = y.ToString();
                row.Cells[1].Value = chiTietHoaDon.MaChiTietHoaDon;
                row.Cells[2].Value = chiTietHoaDon.DichVu.TenDichVu;
                row.Cells[3].Value = chiTietHoaDon.MaHoaDon;
                row.Cells[4].Value = chiTietHoaDon.MaDichVu;
                row.Cells[5].Value = chiTietHoaDon.SoLuong;
                row.Cells[6].Value = chiTietHoaDon.DonGia;
                row.Cells[7].Value = chiTietHoaDon.ThanhTien;
                row.Cells[8].Value = chiTietHoaDon.DichVuDuocSuDung.MaDatPhong;
                row.Cells[9].Value = chiTietHoaDon.DichVuDuocSuDung.NgaySuDung;
               
                try
                {
                    row.Cells[10].Value = chiTietHoaDon.DichVuDuocSuDung.MaSuDung;
                }
                catch (Exception)
                {
                }
                dataGridView.Rows.Add(row);

                // Cộng thêm giá trị của ThanhTien vào tổng tiền dịch vụ
                tongTienDV += chiTietHoaDon.ThanhTien;
            }
            if (tongTienDV != 0)
            {
                if (FromChiTietHoaDOn)
                {
                    lblTongTienDV.Text = $"Mã Hóa Đơn : {maHoaDon},Thành Tiền : {tongTienDV.ToString("#,##0")} VNĐ";
                }
                else
                {
                    lblTongTienDV.Text = $"Tổng Tiền Dịch Vụ : {tongTienDV.ToString("#,##0")} VNĐ";
                }
             
                return;
            }
            lblTongTienDV.Text = "0 VNĐ";
        }


        public void DumpStatusCBB_Phong(int maHoaDon,ComboBox cbb)
        {
            List<ChiTietHoaDonDTO> chiTietHoaDonList = GetChiTietHoaDonByMaHoaDon(maHoaDon);

         
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


      



    }
}
