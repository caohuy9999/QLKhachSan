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

namespace QLKhachSan
{
    public partial class fQuanLyDichVu : Form
    {
        LoaiDichVuBUS LoaiDichVuBUS;
        DichVuBUS dichvuBUS;
        public fQuanLyDichVu(int isFromMain = 0)
        {
            InitializeComponent();
            if (isFromMain == 0)
            {
                this.BackColor = Color.DodgerBlue;
                this.Padding = new Padding(1, 1, 1, 1);
            }
            else
                btnClose.Visible = false;
        }

        private void fQuanLyLoaiDichVu_Load(object sender, EventArgs e)
        {
            
            LoaiDichVuBUS = new LoaiDichVuBUS(fMain.connectionString);
            dichvuBUS = new DichVuBUS(fMain.connectionString);
            dichvuBUS.LayDanhSachDichVuViewFormQuanly(dtgvLoaiDichVu);
            LoaiDichVuBUS.LayDanhSachLoaiDichVuView_CBB(cbbTypeDichVu);
        }

        private void btnAddLoaiDV_Click(object sender, EventArgs e)
        {
            Form f = new fAddNameLoaiDichVu();
            f.ShowDialog();
            if (fAddNameLoaiDichVu.isSUCCESS)
            {
                dichvuBUS.LayDanhSachDichVuViewFormQuanly(dtgvLoaiDichVu);
                LoaiDichVuBUS.LayDanhSachLoaiDichVuView_CBB(cbbTypeDichVu);
            }

        }

        


        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dtgvLoaiDichVu_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == dtgvLoaiDichVu.Columns["cImgEdit"].Index)
            {
                string TenDichVu = DatagridviewHelper.GetStatusDataGridView(dtgvLoaiDichVu, e.RowIndex, "cTenDichVu");
                if (MessageBoxHelper.Show($"Bạn chắc chắm muốn chỉnh sửa Dịch Vụ [{TenDichVu}]") == DialogResult.No)
                {
                    return;
                }
                int MaDichVu = Convert.ToInt32(DatagridviewHelper.GetStatusDataGridView(dtgvLoaiDichVu, e.RowIndex, "cMaDichVu"));
                
                decimal gia =Convert.ToDecimal(DatagridviewHelper.GetStatusDataGridView(dtgvLoaiDichVu, e.RowIndex, "cGia"));
                
                int MaLoaiDichVu =Convert.ToInt32(DatagridviewHelper.GetStatusDataGridView(dtgvLoaiDichVu, e.RowIndex, "cMaLoaiDichVu"));
                string mota = DatagridviewHelper.GetStatusDataGridView(dtgvLoaiDichVu, e.RowIndex, "cMota");
                string tenloaiDV = DatagridviewHelper.GetStatusDataGridView(dtgvLoaiDichVu, e.RowIndex, "cTenLoaiDichVu");
                if (dichvuBUS.CapNhatDichVu(MaDichVu, TenDichVu, gia, MaLoaiDichVu))
                {
                    if (mota != "")
                    {
                        LoaiDichVuBUS.UpdateLoaiDichVu(MaLoaiDichVu, tenloaiDV, mota);
                    }
                    MessageBoxHelper.ShowMessageBox($"Update Loại Dịch Vụ [{TenDichVu}] Thành Công"); dichvuBUS.LayDanhSachDichVuViewFormQuanly(dtgvLoaiDichVu); 
                     return;
                }
                else
                {
                    MessageBoxHelper.ShowMessageBox($"Update Loại Dịch Vụ [{TenDichVu}] Thất Bại");
                }
            }

            if (e.RowIndex >= 0 && e.ColumnIndex == dtgvLoaiDichVu.Columns["cImgDelete"].Index)
            {
                string TenDichVu = DatagridviewHelper.GetStatusDataGridView(dtgvLoaiDichVu, e.RowIndex, "cTenDichVu");
                if (MessageBoxHelper.Show($"Bạn chắc chắm muốn chỉnh Xóa Dịch Vụ {TenDichVu}") == DialogResult.No)
                {
                    return;
                }
                int MaDichVu = Convert.ToInt32(DatagridviewHelper.GetStatusDataGridView(dtgvLoaiDichVu, e.RowIndex, "cMaDichVu"));
                if (dichvuBUS.XoaDichVu(MaDichVu))
                {
                    MessageBoxHelper.ShowMessageBox($"Xóa Loại Dịch Vụ [{TenDichVu}] Thành Công"); dichvuBUS.LayDanhSachDichVuViewFormQuanly(dtgvLoaiDichVu);
                    return;
                }
                else
                {
                    MessageBoxHelper.ShowMessageBox($"Xóa Loại Dịch Vụ [{TenDichVu}] Thất Bại");
                }
            }
        }

        private void cbbTypeDichVu_SelectedIndexChanged(object sender, EventArgs e)
        {
            dichvuBUS.LoadListByTypeCBB(dtgvLoaiDichVu, TypeCbb: (int)cbbTypeDichVu.SelectedValue,true);
        }

        private void txbTimkiemTheoTenDichVu_OnValueChanged(object sender, EventArgs e)
        {
            dichvuBUS.TimKiemDichVuTheoTen(dtgvLoaiDichVu, txbTimkiemTheoTenDichVu.Text.Trim(), true);
        }
    }
}
