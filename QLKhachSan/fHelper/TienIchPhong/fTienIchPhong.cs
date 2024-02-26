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

namespace QLKhachSan.fHelper.TienIchPhong
{
    public partial class fTienIchPhong : Form
    {
        PhongTienIchBUS phongtienichBUS;
        public fTienIchPhong()
        {
            InitializeComponent();
            phongtienichBUS = new PhongTienIchBUS(fLogin.KeystringSQL); 
        }
         


        void DumpCombobox()
        {
            phongtienichBUS.displayStatusTienIch_ComboBOx(cbbStatusTienNghi);
        }




        private void txtSearchPhongByKeyword_TextChange(object sender, EventArgs e)
        {
            phongtienichBUS.displayViewDatagirdView(dtgv, txtSearchPhongByKeyword.Text.Trim(), txbKeyWordNameTienich.Text.Trim(), cbbStatusTienNghi.Text.Trim() == "All" ? "" : cbbStatusTienNghi.Text.Trim());
        }

        private void txbKeyWordNameTienich_TextChange(object sender, EventArgs e)
        {
            phongtienichBUS.displayViewDatagirdView(dtgv, txtSearchPhongByKeyword.Text.Trim(), txbKeyWordNameTienich.Text.Trim(), cbbStatusTienNghi.Text.Trim() == "All" ? "" : cbbStatusTienNghi.Text.Trim());
        }

        private void cbbStatusTienNghi_SelectedIndexChanged(object sender, EventArgs e)
        {
            phongtienichBUS.displayViewDatagirdView(dtgv, txtSearchPhongByKeyword.Text.Trim(), txbKeyWordNameTienich.Text.Trim(), cbbStatusTienNghi.Text.Trim() == "All" ? "" : cbbStatusTienNghi.Text.Trim());
        }

        private void fTienIchPhong_Load(object sender, EventArgs e)
        {
          
            DumpCombobox();
            
        }

        private void txtSearchPhongByKeyword_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                e.Handled = true; // Chặn ký tự không hợp lệ
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Common.ShowForm(new fScriptTienIchPhong());
            if (fScriptTienIchPhong.isAtc)
            {
                phongtienichBUS.displayViewDatagirdView(dtgv, txtSearchPhongByKeyword.Text.Trim(), txbKeyWordNameTienich.Text.Trim(), cbbStatusTienNghi.Text.Trim() == "All" ? "" : cbbStatusTienNghi.Text.Trim());
            }
        }

        private void dtgv_CellClick(object sender, DataGridViewCellEventArgs e)
        {

         int maphongtienich = Common.ConvertToInt32(DatagridviewHelper.GetStatusDataGridView(dtgv, e.RowIndex, "cMaPhongTienIch"));
       
            if (e.RowIndex >= 0 && e.ColumnIndex == dtgv.Columns["cEdit"].Index)
            {
                string sophong = DatagridviewHelper.GetStatusDataGridView(dtgv, e.RowIndex, "cSoPhong");
                int maPhong = Common.ConvertToInt32(DatagridviewHelper.GetStatusDataGridView(dtgv, e.RowIndex, "cMaPhong"));
                int maTienIch = Common.ConvertToInt32(DatagridviewHelper.GetStatusDataGridView(dtgv, e.RowIndex, "cMaTienIch"));
                string trangThai = DatagridviewHelper.GetStatusDataGridView(dtgv, e.RowIndex, "cTrangThai");
                int soLuong = Common.ConvertToInt32(DatagridviewHelper.GetStatusDataGridView(dtgv, e.RowIndex, "cSoLuong"));
                string soPhong = DatagridviewHelper.GetStatusDataGridView(dtgv, e.RowIndex, "cSoPhong");

                PhongTienIchDTO dto = new PhongTienIchDTO {
                    MaPhongTienIch =  maphongtienich,
                    MaPhong = maPhong,
                    MaTienIch = maTienIch,
                    TrangThai = trangThai,
                    SoLuong = soLuong
                };
                Common.ShowForm(new fScriptTienIchPhong(dto, sophong));
                if (fScriptTienIchPhong.isAtc)
                {
                    phongtienichBUS.displayViewDatagirdView(dtgv, txtSearchPhongByKeyword.Text.Trim(), txbKeyWordNameTienich.Text.Trim(), cbbStatusTienNghi.Text.Trim() == "All" ? "" : cbbStatusTienNghi.Text.Trim());
                }
            }



            if (e.RowIndex >= 0 && e.ColumnIndex == dtgv.Columns["cDelete"].Index)
            {
                string tentienich = DatagridviewHelper.GetStatusDataGridView(dtgv, e.RowIndex, "cTenTienIch");
                if (MessageBoxHelper.Show($"Bạn thực sự muốn xóa Tiện ích: {tentienich}") == DialogResult.No)
                    return;
                phongtienichBUS.XoaPhongTienIch(maphongtienich);
            }
        }

        private void btnInBaoCao_Click(object sender, EventArgs e)
        {
            DataTable data = phongtienichBUS.CustomCloneDataGridView(dtgv);
           string tile = "In Báo Cáo Điểm " + DateTime.Now.ToString();
           phongtienichBUS.ExportToExcel(data, tile, FileDialogHelper.SelectFolder("Chọn đường dẫn Lưu File Báo Cáo", Common.PathExE()));

        }
    }
}
