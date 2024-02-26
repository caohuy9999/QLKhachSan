using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Markup;
using System.Xml.Linq;
using BUS;
using BUS.ClassHelper;

namespace QLKhachSan
{
    public partial class fQlUser : Form
    {
        NhanVienBUS nhanVienBUS;
        QuyenBUS quyenBUS;
        private string pathImg = "";
        public fQlUser()
        {
            InitializeComponent();
            nhanVienBUS = new NhanVienBUS(fLogin.KeystringSQL);
            quyenBUS = new QuyenBUS(fLogin.KeystringSQL);
            foreach (DataGridViewColumn column in dtgv.Columns)
            {
                column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
        }

       

        private void QlUser_Load(object sender, EventArgs e)
        {
            quyenBUS.DumpDataComBoBox(cbbChucVu);
            nhanVienBUS.LoadDataToGridView(dtgv);
        }

        private void dtgv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex != 0)
                {
                    DataGridViewRow dataGridViewRow = dtgv.SelectedRows[0];
                    txbName.Text = dataGridViewRow.Cells["cHoTen"].Value.ToString();
                    txbMaNhanVien.Text = dataGridViewRow.Cells["cMaTaiKhoan"].Value.ToString();
                    txbName.Text = dataGridViewRow.Cells["cHoTen"].Value.ToString();
                    txbMucLuong.Text = dataGridViewRow.Cells["cLuong"].Value.ToString();
                    txbTaiKhoan.Text = dataGridViewRow.Cells["cMaTaiKhoan"].Value.ToString();
                    txbMatKhau.Text = dataGridViewRow.Cells["cMatKhau"].Value.ToString();
                    cbbChucVu.Text = dataGridViewRow.Cells["cChucVu"].Value.ToString();
                    pathImg = dataGridViewRow.Cells["cPathImg"].Value.ToString();
                    if (Common.FileTonTai(Common.PathExE() + pathImg))
                    {
                        pictureBox.Image = Image.FromFile(Common.PathExE() + pathImg);
                    }
                    else
                    {
                        pictureBox.Image = null;
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        private void pictureBoxSelect_Click(object sender, EventArgs e)
        {
            pathImg = Common.SelectImage(); // Gọi hàm để chọn hình ảnh
            if (!string.IsNullOrEmpty(pathImg))
            {
                pictureBox.Image = Image.FromFile(pathImg);
                string NameFile = Common.Path_NameFile(pathImg);
                if (Common.CopyFile(pathImg, Common.PathExE() + $"\\img\\Avatar\\{NameFile}"))
                    pathImg = $"\\img\\Avatar\\{NameFile}";
            }
        }

        private void btnDangKyTaiKhoan_Click(object sender, EventArgs e)
        {
            if (nhanVienBUS.InsertNhanVien(txbTaiKhoan.Text.Trim(), txbMatKhau.Text.Trim(), txbName.Text.Trim(), cbbChucVu.Text.Trim(), Common.ConvertToDecimal(txbMucLuong.Text.Trim()),pathImg))
                nhanVienBUS.LoadDataToGridView(dtgv);
        }

        private void btnSuaThongTin_Click(object sender, EventArgs e)
        {
            if (nhanVienBUS.Update(Common.ConvertToInt32(txbMaNhanVien.Text.Trim()),txbTaiKhoan.Text.Trim(), txbMatKhau.Text.Trim(), txbName.Text.Trim(), cbbChucVu.Text.Trim(), pathImg))
                nhanVienBUS.LoadDataToGridView(dtgv);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (nhanVienBUS.XoaNhanVien(Common.ConvertToInt32(txbMaNhanVien.Text.Trim())))
                nhanVienBUS.LoadDataToGridView(dtgv);
        }

        private void txbSearchNhanVienByName_OnValueChanged(object sender, EventArgs e)
        {
            nhanVienBUS.LoadDataToGridView(dtgv, txbSearchNhanVienByName.Text.Trim());
        }
    }
}
