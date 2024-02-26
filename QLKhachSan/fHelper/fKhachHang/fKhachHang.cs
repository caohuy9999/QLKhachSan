using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BUS;
using BUS.ClassHelper;
using DTO;
using GUI;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace QLKhachSan.fHelper.fKhachHang
{
    public partial class fKhachHang : Form
    {

        KhachHangBUS khachhangBUS;
        public fKhachHang()
        {
            InitializeComponent();
            foreach (DataGridViewColumn column in dtgv.Columns)
            {
                column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
        }

        bool isScript = true;
        private void timer_Tick(object sender, EventArgs e)
        {
            if (isScript)
            {
                pnlRightMain.Width -= 50;
                if (pnlRightMain.Width <= 0)
                {
                    timer.Stop(); isScript = false;
                }
            }
            else
            {
                pnlRightMain.Width += 50;
                if (pnlRightMain.Width >= 230)
                {
                    timer.Stop(); isScript = true;
                }
            }
        }

       
        private void btnDangKyTaiKhoan_Click(object sender, EventArgs e)
        {
            txbLoaiKH.Text = "";
            txbDiaChi.Text = "";
            txbSdt.Text = "";
            txbEmail.Text = "";
            txbName.Text = "";
            btnXacNhanThongTin.ButtonText = "Xác Nhận Thêm";
            timer.Start();
        }

        private void fKhachHang_Load(object sender, EventArgs e)
        {
            timer.Start();
            khachhangBUS = new KhachHangBUS(fLogin.KeystringSQL);
            khachhangBUS.LoadDataToGridView(dtgv);
        }

        private void txbSearchbykeyword_TextChange(object sender, EventArgs e)
        {
            khachhangBUS.LoadDataToGridView(dtgv, txbSearchbykeyword.Text.Trim());
        }

        private void dtgv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {

                if (e.RowIndex >= 0 && e.ColumnIndex == dtgv.Columns["cEdit"].Index)
                {
                    string TenKH = DatagridviewHelper.GetStatusDataGridView(dtgv, e.RowIndex, "cHoTen");
                    //if (MessageBoxHelper.Show($"Bạn chắc chắm muốn Update Khách hàng [{TenKH}]") == DialogResult.No)
                    //    return;
                    btnXacNhanThongTin.ButtonText = "Xác Nhận Update";
                    timer.Start();
                }


                if (e.RowIndex >= 0 && e.ColumnIndex == dtgv.Columns["cDelete"].Index)
                {
                    string TenKH = DatagridviewHelper.GetStatusDataGridView(dtgv, e.RowIndex, "cHoTen");
                    //if (MessageBoxHelper.Show($"Bạn chắc chắm muốn Delete Khách hàng [{TenKH}]") == DialogResult.No)
                    //    return;
                    btnXacNhanThongTin.ButtonText = "Xác Nhận Delete";
                    timer.Start();

                }


                if (e.ColumnIndex != 0)
                {
                    DataGridViewRow dataGridViewRow = dtgv.SelectedRows[0];
                    txbLoaiKH.Text = dataGridViewRow.Cells["cLoaiKhachHang"].Value.ToString();
                    txbDiaChi.Text = dataGridViewRow.Cells["cDiaChi"].Value.ToString();
                    txbSdt.Text = dataGridViewRow.Cells["cSoDienThoai"].Value.ToString();
                    txbEmail.Text = dataGridViewRow.Cells["cEmail"].Value.ToString();
                    txbName.Text = dataGridViewRow.Cells["cHoTen"].Value.ToString();
                    txbMaNhanVien.Text = dataGridViewRow.Cells["cMaKhachHang"].Value.ToString();
                    
                }
            }
            catch (Exception)
            {
            }
        }

        private void btnXacNhanThongTin_Click(object sender, EventArgs e)
        {
            KhachHangDTO khachHangDTO = new KhachHangDTO() {
                MaKhachHang = Common.ConvertToInt32(txbMaNhanVien.Text.Trim()),
                HoTen = txbName.Text.Trim(),
                Email = txbEmail.Text.Trim(),
                SoDienThoai = txbSdt.Text.Trim(),
                DiaChi = txbDiaChi.Text.Trim(), 
                LoaiKhachHang = txbLoaiKH.Text.Trim()};
            bool isLoadForm = false;
            switch (btnXacNhanThongTin.ButtonText)
            {
                case "Xác Nhận Update":
                    isLoadForm = khachhangBUS.SuaThongTinKhachHang(khachHangDTO);
                    break;
                case "Xác Nhận Thêm":
                    isLoadForm = khachhangBUS.ThemKhachHang(khachHangDTO) > 0;
                    break;
                case "Xác Nhận Delete":
                    isLoadForm = khachhangBUS.XoaKhachHang(Common.ConvertToInt32(txbMaNhanVien.Text.Trim()));
                    break;
            }
            if (isLoadForm)
            {
                khachhangBUS.LoadDataToGridView(dtgv);
            }
        }
    }
}
