using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Windows.Forms;
using BUS.ClassHelper;
using DAO;
using DTO;
using GUI;
using OfficeOpenXml;
using OfficeOpenXml.Style;

namespace BUS
{
    public class PhongTienIchBUS
    {
        private PhongTienIchDAO phongTienIchDAO;
        private tienichDAO tienichDAO;

        public PhongTienIchBUS(string connectionString)
        {
            phongTienIchDAO = new PhongTienIchDAO(connectionString);
            tienichDAO = new tienichDAO(connectionString);
        }

        public bool add(PhongTienIchDTO phongTienIch)
        {

            return phongTienIchDAO.InsertPhongTienIch(phongTienIch) > 0;
        }

        public bool update(PhongTienIchDTO phongTienIch)
        {
            return phongTienIchDAO.UpdatePhongTienIch(phongTienIch);
        }





        public void XoaPhongTienIch(int maPhongTienIch)
        {

            phongTienIchDAO.DeletePhongTienIch(maPhongTienIch);
        }

        public List<PhongTienIchDTO> LayDanhSachPhongTienIch()
        {
            return phongTienIchDAO.GetAllPhongTienIch();
        }

        public void displayViewDatagirdView(DataGridView dtgv, string keywordPhong = "", string keywordTienNghi = "", string typeStt = "")
        {
            DataTable Data = phongTienIchDAO.GetAllPhongTienIch_DataTable(keywordPhong, keywordTienNghi, typeStt);


            dtgv.Rows.Clear();

            int y = 0;
            foreach (DataRow row in Data.Rows)
            {
                y++;
                string pathIMG = Common.PathExE() + row["pathIMG"].ToString();

                // Kiểm tra tồn tại của hình ảnh
                Image img = Common.FileTonTai(pathIMG) ? Image.FromFile(pathIMG) : null;


                dtgv.Rows.Add(
                      y,
                      row["SoPhong"].ToString(),
                      row["TenTienIch"].ToString(),
                      row["TrangThai"].ToString(),
                      row["SoLuong"].ToString(),
                      row["MieuTa"].ToString(),
                      row["MaPhongTienIch"].ToString(),
                      row["MaPhong"].ToString(),
                      row["MaTienIch"].ToString(),
                      row["SoLuongTon"].ToString(),
                       row["pathIMG"].ToString(),
                      img,
                      Image.FromFile(@"img\edit_25px.png"),
                      Image.FromFile(@"img\cancel_25px.png")
                );
            }

            // Cân chỉnh dữ liệu trong DataGridView
            DatagridviewHelper.TitleCenter(dtgv);
            DatagridviewHelper.DataCenter(dtgv, true);
        }


        public void displayStatusTienIch_ComboBOx(ComboBox cbb)
        {
            List<string> trangThaiList = phongTienIchDAO.GetAllTrangThaiPhongTienIch();

            // Thêm "All" vào đầu danh sách
            trangThaiList.Insert(0, "All");

            // Gán danh sách vào ComboBox
            cbb.DataSource = trangThaiList;
            cbb.SelectedIndex = 0;
        }

        public bool ThemPhongTienIch(int maPhong, int maTienIch, string trangThai, int soLuong)
        {
            bool AddResult = false;
            try
            {
                PhongTienIchDTO phongTienIch = new PhongTienIchDTO
                {
                    MaPhong = maPhong,
                    MaTienIch = maTienIch,
                    TrangThai = trangThai,
                    SoLuong = soLuong
                };


                int MaPhongTienIchNew = phongTienIchDAO.InsertPhongTienIch(phongTienIch);
                // - hoặc + thêm số tồn
                AddResult = tienichDAO.UpAndDownSoLuongTienIch(maTienIch, true, soLuong);

            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi thêm PhongTienIch: " + ex.Message);

            }
            MessageBoxHelper.Show($"Update Tiện ích phòng {(AddResult ? "Thành Công" : "Thất Bại")}");
            return AddResult;
        }


        public bool SuaPhongTienIch(int MaPhongTienIch, int maPhong, int maTienIch, string trangThai, int soLuong)
        {
            bool updateResult = false;
            try
            {
                PhongTienIchDTO phongTienIch = new PhongTienIchDTO
                {
                    MaPhongTienIch = MaPhongTienIch,
                    MaPhong = maPhong,
                    MaTienIch = maTienIch,
                    TrangThai = trangThai,
                    SoLuong = soLuong
                };

                // Thực hiện cập nhật và kiểm tra kết quả
                updateResult = update(phongTienIch);
                // - hoặc + thêm số tồn

                // rieeng update thif phải trừ đi 1 đơn vị : Số Lượng -1 = kết quả thực ;
                updateResult = tienichDAO.UpAndDownSoLuongTienIch(maTienIch, true, soLuong - 1);

                return updateResult;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi cập nhật PhongTienIch: " + ex.Message);

            }

            MessageBoxHelper.Show($"Update Tiện ích phòng {(updateResult ? "Thành Công" : "Thất Bại")}");
            return updateResult;
        }






        public DataTable CustomCloneDataGridView(DataGridView originalDataGridView)
        {
            // Tạo một DataTable mới từ dữ liệu của DataGridView
            DataTable dataTable = new DataTable();

            foreach (DataGridViewColumn col in originalDataGridView.Columns)
            {
                // Kiểm tra cột có tên không mong muốn
                if (col.Name != "cIMGTienNghi" && col.Name != "cpathIMG" && col.Name != "cEdit" && col.Name != "cDelete")
                {
                    dataTable.Columns.Add(col.Name);
                }
            }

            foreach (DataGridViewRow row in originalDataGridView.Rows)
            {
                DataRow dataRow = dataTable.NewRow();
                for (int i = 0; i < originalDataGridView.Columns.Count; i++)
                {
                    if (originalDataGridView.Columns[i].Name != "cIMGTienNghi" && originalDataGridView.Columns[i].Name != "cpathIMG" && originalDataGridView.Columns[i].Name != "cEdit" && originalDataGridView.Columns[i].Name != "cDelete")
                    {
                        dataRow[originalDataGridView.Columns[i].Name] = row.Cells[i].Value;
                    }
                }
                dataTable.Rows.Add(dataRow);
            }

            //// Xóa bỏ 4 cột cuối của DataTable
            //int numberOfColumnsToRemove = 4;
            //for (int i = 0; i < numberOfColumnsToRemove; i++)
            //{
            //    if (dataTable.Columns.Count > 0)
            //    {
            //        dataTable.Columns.RemoveAt(dataTable.Columns.Count - 1);
            //    }
            //}

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
