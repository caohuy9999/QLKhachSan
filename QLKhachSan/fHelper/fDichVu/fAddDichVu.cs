using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BUS;
using BUS.ClassHelper;
using DTO;
using GUI;

namespace QLKhachSan
{
    public partial class fAddDichVu : Form
    {
        public fAddDichVu()
        {
            InitializeComponent();
        }
        public int idHoaDon;
        private DichVuBUS dichvuBUS;
        private LoaiDichVuBUS LoaiDichVuBUs;
        private DichVuSuDungBUS dichvusudungBUS;
        private ChiTietHoaDonBUS chitiethoadonBUS;
        private HoaDonBUS hoadonBUS;
        public bool isAddDV = false;
        public fAddDichVu(int idBill)
        {
            InitializeComponent();
                idHoaDon = idBill;
        }

        private void fAddDichVu_Load(object sender, EventArgs e)
        {
            this.BackColor = Color.DodgerBlue;
            this.Padding = new Padding(1, 1, 1, 1);
            dichvusudungBUS = new DichVuSuDungBUS(fMain.connectionString);
            chitiethoadonBUS = new ChiTietHoaDonBUS(fMain.connectionString);
            hoadonBUS = new HoaDonBUS(fMain.connectionString);
            dichvuBUS = new DichVuBUS(fMain.connectionString);
            LoaiDichVuBUs = new LoaiDichVuBUS(fMain.connectionString);
            LoaiDichVuBUs.LayDanhSachLoaiDichVuView_CBB(cbbTypeDichVu);
            dichvuBUS.LoadViewDichVu(dtgvDichVu);
            Tieude.Text = $"Them Dich Vu Phòng {hoadonBUS.getSoPhongByMaHoaDon(idHoaDon)}";

            // có mã hóa đơn , lấy ra số phòng 
            cbbTypeDichVu.SelectedValue = 0;
        }

        private void bunifuCustomLabel2_Click(object sender, EventArgs e)
        {

        }

        private void bunifuMaterialTextbox1_OnValueChanged(object sender, EventArgs e)
        {

        }

        private void metroComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void bunifuCustomLabel1_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void bunifuCustomLabel3_Click(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void bunifuCustomLabel4_Click(object sender, EventArgs e)
        {

        }

        private void bunifuThinButton21_Click(object sender, EventArgs e)
        {

        }

        private void btnClose_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }




        private void dtgvDichVu_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == dtgvDichVu.Columns["cImgAdd"].Index)
            {

                dichvuBUS.add_DichVuBySelected(dtgvSelectDichVu, dtgvDichVu);
                
                dichvuBUS.SumRow(dtgvSelectDichVu, lbl_Tongtien);
            }
            
        }

        private void dtgvSelectDichVu_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == dtgvSelectDichVu.Columns["cImgDelete"].Index)
            {
                dichvuBUS.deleteRowAndSapXep(dtgvSelectDichVu, e.RowIndex,true);
                dichvuBUS.SumRow(dtgvSelectDichVu, lbl_Tongtien);
            }
        }



        private void dtgvDichVu_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
        
                int rowIndex = dtgvDichVu.CurrentCell.RowIndex;
               
                // Lấy giá trị từ cột "Số Lượng" và "Đơn Giá"
                int soluong = 0;
                decimal donGia = 0;


                string soLuongString = DatagridviewHelper.GetStatusDataGridView(dtgvDichVu, rowIndex, "cSoLuong");
                if (soLuongString == "") { /*messibox ret*/ return; }
                soluong = Convert.ToInt32(soLuongString);


                string donGiaString = DatagridviewHelper.GetStatusDataGridView(dtgvDichVu, rowIndex, "cGia");
                if (donGiaString == "")
                { return; }
                donGia = Convert.ToDecimal(donGiaString);

                // Tính giá trị mới cho cột "Thành Tiền"
                string thanhTien = (soluong * donGia).ToString("#,##0.00");
                DatagridviewHelper.SetStatusDataGridView(dtgvDichVu, rowIndex, "cThanhTien", thanhTien);
            
        }

        private void cbbTypeDichVu_SelectedIndexChanged(object sender, EventArgs e)
        {
            dichvuBUS.LoadListByTypeCBB(dtgvDichVu,TypeCbb: (int)cbbTypeDichVu.SelectedValue);
        }

        private void btnAddDichVu_Click(object sender, EventArgs e)
        {
            Form f = new fQuanLyDichVu();
            f.ShowDialog();
            LoaiDichVuBUs.LayDanhSachLoaiDichVuView_CBB(cbbTypeDichVu);
            dichvuBUS.LoadViewDichVu(dtgvDichVu);
           
        }

        private void txbTimkiemTheoTenDichVu_OnValueChanged(object sender, EventArgs e)
        {
            dichvuBUS.TimKiemDichVuTheoTen(dtgvDichVu, txbTimkiemTheoTenDichVu.Text);
        }

        private void btnSaveAddDichVu_Click(object sender, EventArgs e)
        {
            

            HoaDonDTO HoaDon = hoadonBUS.LayHoaDonTheoMa(idHoaDon);
            int maDatPhong = HoaDon.MaDatPhong;
            foreach (DataGridViewRow row in dtgvSelectDichVu.Rows)
            {
                int MaDichVu = Convert.ToInt32(DatagridviewHelper.GetStatusDataGridView(dtgvSelectDichVu, row.Index, "cMaDichVus"));
                DateTime ngaySudung = Convert.ToDateTime(DatagridviewHelper.GetStatusDataGridView(dtgvSelectDichVu, row.Index, "cThoiGianSuDung"));
                int SoLuong = Convert.ToInt32(DatagridviewHelper.GetStatusDataGridView(dtgvSelectDichVu, row.Index, "cSoLuongs"));
                decimal dongia = Convert.ToDecimal(DatagridviewHelper.GetStatusDataGridView(dtgvSelectDichVu, row.Index, "cGias"));
                decimal thanhtien = Convert.ToDecimal(DatagridviewHelper.GetStatusDataGridView(dtgvSelectDichVu, row.Index, "cThanhTiens"));





                ChiTietHoaDonDTO chitiethoadon = new ChiTietHoaDonDTO {
                    MaHoaDon = idHoaDon,
                    MaDichVu = MaDichVu,
                    SoLuong = SoLuong,
                    DonGia = dongia,
                    ThanhTien = thanhtien

                };
                int idChiTietHoaDon = chitiethoadonBUS.ThemChiTietHoaDon(chitiethoadon);


                DichVuSuDungDTO DVdsd = new DichVuSuDungDTO {
                    MaDichVu = MaDichVu,
                    MaDatPhong = maDatPhong,
                    NgaySuDung = ngaySudung,
                    SoLuong = SoLuong,
                    MaChiTietHoaDon = idChiTietHoaDon
                };
               
              
                if (dichvusudungBUS.ThemDichVuSuDung(DVdsd) > 0)
                {

                    // + tong tien DichVu Update trong bang Hoa Don
                    hoadonBUS.CongDonTongTienDichVu(HoaDon.MaHoaDon, thanhtien);


                       isAddDV = true;
                }

            }
            if (MessageBoxHelper.Show($"Thêm Dịch Vụ {(isAddDV ? "Thành Công" : "Thất Bại")} , Bạn có muốn thoát của sổ?") == DialogResult.Yes)
            {
                this.Close();
            }
            
            //DataTable dataTable = (DataTable)dtgvSelectDichVu.DataSource;


        }
    }
}
