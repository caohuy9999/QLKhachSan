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
using GUI;

namespace QLKhachSan.fHelper.fHoaDon
{
    public partial class fLichSuHoaDon : Form
    {
        private bool ActLoadForm = false;
        HoaDonBUS hoadonBUS;
        NhanVienBUS nhanvienBUS;
        public fLichSuHoaDon()
        {
            InitializeComponent();
            hoadonBUS = new HoaDonBUS(fLogin.KeystringSQL);
            nhanvienBUS = new NhanVienBUS(fLogin.KeystringSQL);
        }

        void pullDataCbb()
        {
            nhanvienBUS.dumpComboboxData(cbbTypeNhanVien);
            hoadonBUS.dumpComboboxDataTinhTrang(cbbHinhThucThanhToan);
            hoadonBUS.dumpComboboxDataTrangThai(cbbTrangThaiThanhToan);
        }




        private void fLichSuHoaDon_Load(object sender, EventArgs e)
        {
            hoadonBUS.displayViewDatagirdView(dtgv);
            pullDataCbb();
            ActLoadForm = true;
        }

        private void cbbHinhThucThanhToan_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!ActLoadForm)
                return;
            hoadonBUS.displayViewDatagirdView(dtgv, default, default, cbbTrangThaiThanhToan.Text.Trim(), cbbHinhThucThanhToan.Text.Trim(),cbbTypeNhanVien.Text, txbTimkiemTheoTenDichVu.Text.Trim());
        }

        private void cbbTrangThaiThanhToan_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!ActLoadForm)
                return;
            hoadonBUS.displayViewDatagirdView(dtgv, default, default, cbbTrangThaiThanhToan.Text.Trim(), cbbHinhThucThanhToan.Text.Trim(), cbbTypeNhanVien.Text, txbTimkiemTheoTenDichVu.Text.Trim());
        }

        private void cbbTypeNhanVien_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!ActLoadForm)
                return;
            hoadonBUS.displayViewDatagirdView(dtgv, default, default, cbbTrangThaiThanhToan.Text.Trim(), cbbHinhThucThanhToan.Text.Trim(), cbbTypeNhanVien.Text, txbTimkiemTheoTenDichVu.Text.Trim());
        }

        private void txbTimkiemTheoTenDichVu_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                e.Handled = true; // Chặn ký tự không hợp lệ
        }

        private void txbTimkiemTheoTenDichVu_TextChange(object sender, EventArgs e)
        {
            if (!ActLoadForm)
                return;
            hoadonBUS.displayViewDatagirdView(dtgv, default, default, cbbTrangThaiThanhToan.Text.Trim(), cbbHinhThucThanhToan.Text.Trim(), cbbTypeNhanVien.Text, txbTimkiemTheoTenDichVu.Text.Trim());
        }

        private void DateTimeStart_ValueChanged(object sender, EventArgs e)
        {
            if (!ActLoadForm)
                return;
            hoadonBUS.displayViewDatagirdView(dtgv, DateTimeStart.Value, DateTimeStop.Value, cbbTrangThaiThanhToan.Text.Trim(), cbbHinhThucThanhToan.Text.Trim(), cbbTypeNhanVien.Text, txbTimkiemTheoTenDichVu.Text.Trim());
        }

        private void DateTimeStop_ValueChanged(object sender, EventArgs e)
        {
            if (!ActLoadForm)
                return;
            hoadonBUS.displayViewDatagirdView(dtgv, DateTimeStart.Value, DateTimeStop.Value, cbbTrangThaiThanhToan.Text.Trim(), cbbHinhThucThanhToan.Text.Trim(), cbbTypeNhanVien.Text, txbTimkiemTheoTenDichVu.Text.Trim());
        }

        private void btnInBaoCao_Click(object sender, EventArgs e)
        {
            DataTable data = hoadonBUS.CustomCloneDataGridView(dtgv);
            string tile = "In Báo Hóa Đơn " + DateTime.Now.ToString();
            hoadonBUS.ExportToExcel(data, tile, FileDialogHelper.SelectFolder("Chọn đường dẫn Lưu File Báo Cáo", Common.PathExE()));
            
        }

        private void dtgv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == dtgv.Columns["cIMG"].Index)
            {
                int mahoadon = Common.ConvertToInt32(DatagridviewHelper.GetStatusDataGridView(dtgv, e.RowIndex, "cMaHoaDon")); 
                Common.ShowForm(new fChitiethoadon(mahoadon));
            }
        }
    }
}
