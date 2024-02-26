using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using BUS.ClassHelper;
using DAO; // Import lớp DAO để tương tác với cơ sở dữ liệu
using DTO;
using GUI;

namespace BUS
{
    public class DichVuBUS
    {
        private DichVuDAO dichVuDAO; // Sử dụng lớp DAO để thực hiện các thao tác cơ sở dữ liệu
        private LoaiDichVuBUS loaidichvuBUS;
        DataTable ListDichVu = null;
        private string ConnectionString;
        public DichVuBUS(string connectionString)
        {
            ConnectionString = connectionString;
            dichVuDAO = new DichVuDAO(connectionString);
            
        }


        public void TimKiemDichVuTheoTen(DataGridView dtgView,string key ,bool fQuanLyDichVu = false)
        {
            ListDichVu = dichVuDAO.TimKiemDichVuTheoTen(key);
            if (key == "")
            {
                ListDichVu = LayDanhSachDichVu();
            }
            if (fQuanLyDichVu)
            {
                ListDichVu.Columns.Add("ImgEdit", typeof(Image));
                ListDichVu.Columns.Add("ImgDelete", typeof(Image));
                ListDichVu.Columns.Add("TenLoaiDichVu", typeof(string));
                ListDichVu.Columns.Add("MoTa", typeof(string));
                int indexRow = ListDichVu.Rows.Count;

                for (int i = 0; i < indexRow; i++)
                {
                    ListDichVu.Rows[i]["ImgEdit"] = Image.FromFile(@"img\edit_25px.png");
                    ListDichVu.Rows[i]["ImgDelete"] = Image.FromFile(@"img\cancel_25px.png");

                    LoaiDichVuDTO loaiDV = loaidichvuBUS.LayLoaiDichVuTheoMa((int)ListDichVu.Rows[i]["MaLoaiDichVu"]);
                    ListDichVu.Rows[i]["TenLoaiDichVu"] = loaiDV.TenLoaiDichVu;
                    ListDichVu.Rows[i]["MoTa"] = loaiDV.MoTa;
                }
            }
            else
            {
                ListDichVu.Columns.Add("ImgAdd", typeof(Image));
                int indexRow = ListDichVu.Rows.Count;
                for (int i = 0; i < indexRow; i++)
                {
                    ListDichVu.Rows[i]["ImgAdd"] = Image.FromFile(@"img\plus_25px.png");
                }
            }
            dtgView.DataSource = ListDichVu;
        }




        // Thêm một dịch vụ mới và trả về MaDichVu mới được tạo
        public int ThemDichVu(string tenDichVu, decimal gia, int maLoaiDichVu)
        {
            // Thực hiện kiểm tra logic kinh doanh nếu cần
            // Ví dụ: kiểm tra giá trị của các tham số đầu vào trước khi gửi yêu cầu đến DAO
            // Nếu dữ liệu không hợp lệ, bạn có thể ném ra các ngoại lệ hoặc trả về mã lỗi tùy ý

            // Gọi phương thức tương ứng trong DAO
            return dichVuDAO.ThemDichVu(tenDichVu, gia, maLoaiDichVu);
        }

        // Cập nhật thông tin dịch vụ
        public bool CapNhatDichVu(int maDichVu, string tenDichVu, decimal gia, int maLoaiDichVu)
        {
            // Thực hiện kiểm tra logic kinh doanh nếu cần

            // Gọi phương thức tương ứng trong DAO
            return dichVuDAO.CapNhatDichVu(maDichVu, tenDichVu, gia, maLoaiDichVu);
        }

        // Xóa dịch vụ
        public bool XoaDichVu(int maDichVu)
        {
            // Thực hiện kiểm tra logic kinh doanh nếu cần

            // Gọi phương thức tương ứng trong DAO
            return dichVuDAO.XoaDichVu(maDichVu);
        }

        // Lấy thông tin dịch vụ theo MaDichVu
        public DataTable LayThongTinDichVuTheoMa(int maDichVu)
        {
            // Thực hiện kiểm tra logic kinh doanh nếu cần

            // Gọi phương thức tương ứng trong DAO
            return dichVuDAO.LayThongTinDichVuTheoMa(maDichVu);
        }
        // Lấy thông tin dịch vụ theo MaLoaiDichVu
        public DataTable LayThongTinDichVuTheoMaLoaiDichVu(int MaLoaiDichVu,int maDichVu = -1)
        {
            // Thực hiện kiểm tra logic kinh doanh nếu cần

            // Gọi phương thức tương ứng trong DAO
            return dichVuDAO.LayThongTinDichVuTheoMaLoaiDichVu(MaLoaiDichVu, maDichVu);
        }



        // Lấy danh sách tất cả dịch vụ
        public DataTable LayDanhSachDichVu()
        {
            // Thực hiện kiểm tra logic kinh doanh nếu cần
            // Gọi phương thức tương ứng trong DAO
            return dichVuDAO.LayDanhSachDichVu();
        }


        public void LayDanhSachDichVu(DataGridView dtgView)
        {
            ListDichVu.Columns.Add("ImgAdd", typeof(Image));
            
            int indexRow = ListDichVu.Rows.Count;

            for (int i = 0; i < indexRow; i++)
            {
                ListDichVu.Rows[i]["ImgAdd"] = Image.FromFile(@"img\plus_25px.png");
            }
            dtgView.DataSource = ListDichVu;
        }

        public void LayDanhSachDichVuViewFormQuanly(DataGridView dtgView)
        {
            loaidichvuBUS = new LoaiDichVuBUS(ConnectionString);
            DataTable dataLoaiDichVu = LayDanhSachDichVu();

            // Đặt dữ liệu cho DataGridView
            //dtgView.Rows.Clear(); // Xóa bỏ các hàng hiện có (nếu có)
               
                dataLoaiDichVu.Columns.Add("ImgEdit", typeof(Image));
            dataLoaiDichVu.Columns.Add("ImgDelete", typeof(Image));
            dataLoaiDichVu.Columns.Add("TenLoaiDichVu", typeof(string));
            dataLoaiDichVu.Columns.Add("MoTa", typeof(string));
            int indexRow = dataLoaiDichVu.Rows.Count;

                for (int i = 0; i < indexRow; i++)
                {
                dataLoaiDichVu.Rows[i]["ImgEdit"] = Image.FromFile(@"img\edit_25px.png");
                dataLoaiDichVu.Rows[i]["ImgDelete"] = Image.FromFile(@"img\cancel_25px.png");

                LoaiDichVuDTO loaiDV = loaidichvuBUS.LayLoaiDichVuTheoMa((int)dataLoaiDichVu.Rows[i]["MaLoaiDichVu"]);
                dataLoaiDichVu.Rows[i]["TenLoaiDichVu"] = loaiDV.TenLoaiDichVu;
                dataLoaiDichVu.Rows[i]["MoTa"] = loaiDV.MoTa;
                }
            dtgView.DataSource = dataLoaiDichVu;
            
        }


        public void LayDanhSachDichVu(DataGridView dtgView, int typeCbb , bool fQuanLyDichVu = false)
        {

            DataTable dataTable = new DataTable();
            if (typeCbb == 0)
            {
                dataTable = LayDanhSachDichVu();
            }
            else
            {
                dataTable = LayThongTinDichVuTheoMaLoaiDichVu(typeCbb);
            }

            if (fQuanLyDichVu)
            {
                dataTable.Columns.Add("ImgEdit", typeof(Image));
                dataTable.Columns.Add("ImgDelete", typeof(Image));
                dataTable.Columns.Add("TenLoaiDichVu", typeof(string));
                dataTable.Columns.Add("MoTa", typeof(string));
                int indexRow = dataTable.Rows.Count;

                for (int i = 0; i < indexRow; i++)
                {
                    dataTable.Rows[i]["ImgEdit"] = Image.FromFile(@"img\edit_25px.png");
                    dataTable.Rows[i]["ImgDelete"] = Image.FromFile(@"img\cancel_25px.png");

                    LoaiDichVuDTO loaiDV = loaidichvuBUS.LayLoaiDichVuTheoMa((int)dataTable.Rows[i]["MaLoaiDichVu"]);
                    dataTable.Rows[i]["TenLoaiDichVu"] = loaiDV.TenLoaiDichVu;
                    dataTable.Rows[i]["MoTa"] = loaiDV.MoTa;
                }
            }
            else
            {
                dataTable.Columns.Add("ImgAdd", typeof(Image));
                int indexRow = dataTable.Rows.Count;

                for (int i = 0; i < indexRow; i++)
                {
                    dataTable.Rows[i]["ImgAdd"] = Image.FromFile(@"img\plus_25px.png");
                }
            }
            dtgView.DataSource = dataTable;
        }

        public void LayDanhSachDichVu(ComboBox cbbTypeDichVu)
        {

            DataRow newRow = ListDichVu.NewRow();
            newRow["TenDichVu"] = "All"; // Đặt giá trị cho cột "ColumnName1"
            newRow["MaDichVu"] = 0; // Đặt giá trị cho cột "ColumnName2"

            ListDichVu.Rows.InsertAt(newRow, 0);// Chèn hàng mới vào đầu DataTable ở vị trí 0

            cbbTypeDichVu.DisplayMember = "TenDichVu"; // Hiển thị Tên Loại phòng trong ComboBox
            cbbTypeDichVu.ValueMember = "MaDichVu"; // Giá trị thực sự (ID) của Loại phòng

            cbbTypeDichVu.DataSource = ListDichVu;
        }

        public void LoadViewDichVu(DataGridView dtgView = null, int TypeCbb = -1)
        {
            ListDichVu = LayDanhSachDichVu();
            if (TypeCbb != -1)
            {
                LayDanhSachDichVu(dtgView, TypeCbb);return;
            }
            if (dtgView != null)
            {
                LayDanhSachDichVu(dtgView);
            }
           
        }

        public void LoadListByTypeCBB(DataGridView dtgView ,int TypeCbb,bool fQuanLyDichVu = false)
        {
           LayDanhSachDichVu(dtgView, TypeCbb, fQuanLyDichVu);
        }




        public void deleteRowAndSapXep(DataGridView dtgv, int rowIndex, bool SapXep = false)
        {
            try
            {
                // tien hanh xoa
                dtgv.Rows.RemoveAt(rowIndex);
                if (SapXep)
                {
                    // Cập nhật lại số thứ tự cho tất cả các dòng còn lại
                    for (int i = 0; i < dtgv.Rows.Count; i++)
                    {
                        dtgv.Rows[i].Cells[0].Value = (i + 1).ToString();
                    }
                }
            }
            catch (Exception)
            {


            }
        }

        public void SumRow(DataGridView dtgv, Label lbl_Tongtien)
        {
            try
            {
                double kq = 0;

                // Tính tổng giá trị cột "cGiaTheoDems" cho từng dòng
                for (int i = 0; i < dtgv.Rows.Count; i++)
                {
                    double giaTheoDem = 0;
                    if (double.TryParse(dtgv.Rows[i].Cells["cThanhTiens"].Value.ToString(), out giaTheoDem))
                    {
                        kq += giaTheoDem;
                    }
                }

                // Cập nhật giá trị của lbl_Tongtien
                lbl_Tongtien.Text = $"Tổng Thành Tiền : {kq.ToString("#,##0")}";
            }
            catch (Exception)
            {
                // Xử lý ngoại lệ nếu cần
            }
        }



        public void add_DichVuBySelected(DataGridView dtgvSelect, DataGridView dtgvDichVu)
        {
            // datagridview  get du lieu :D
            string id = DatagridviewHelper.GetStatusDataGridView(dtgvDichVu, dtgvDichVu.CurrentRow.Index, "cMaDichVu");
            string soluong = DatagridviewHelper.GetStatusDataGridView(dtgvDichVu, dtgvDichVu.CurrentRow.Index, "cSoLuong");
            string thanhTien = DatagridviewHelper.GetStatusDataGridView(dtgvDichVu, dtgvDichVu.CurrentRow.Index, "cThanhTien");
            if (soluong == "")
            {
                MessageBoxHelper.ShowMessageBox("Vui lòng điền số lượng sử dụng"); return;
            }

            DataRow[] rows = ListDichVu.Select($"MaDichVu = {id}");
            if (rows.Length > 0)
            {
                int rowIndex = dtgvSelect.Rows.Add(); // Thêm một dòng mới vào DataGridView và lấy chỉ số hàng
                DatagridviewHelper.SetStatusDataGridView(dtgvSelect, rowIndex, "cSTT", rowIndex + 1);
                DatagridviewHelper.SetStatusDataGridView(dtgvSelect, rowIndex, "cTenDichVus", rows[0]["TenDichVu"]);
                DatagridviewHelper.SetStatusDataGridView(dtgvSelect, rowIndex, "cMaDichVus", rows[0]["MaDichVu"]);
                DatagridviewHelper.SetStatusDataGridView(dtgvSelect, rowIndex, "cSoluongs", soluong);
                DatagridviewHelper.SetStatusDataGridView(dtgvSelect, rowIndex, "cGias", rows[0]["Gia"]);
                DatagridviewHelper.SetStatusDataGridView(dtgvSelect, rowIndex, "cMaLoaiDichVus", rows[0]["MaLoaiDichVu"]);
                DatagridviewHelper.SetStatusDataGridView(dtgvSelect, rowIndex, "cThanhTiens", thanhTien);
                DatagridviewHelper.SetStatusDataGridView(dtgvSelect, rowIndex, "cThoiGianSuDung", DateTime.Now);
                DatagridviewHelper.SetStatusDataGridView(dtgvSelect, rowIndex, "cImgDelete", Image.FromFile(@"img\cancel_25px.png"));
            }
        }





    }
}
