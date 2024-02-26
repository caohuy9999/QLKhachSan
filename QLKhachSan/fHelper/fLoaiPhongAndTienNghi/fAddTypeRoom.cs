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

namespace QLKhachSan.fHelper.fLoaiPhongAndTienNghi
{
    public partial class fAddTypeRoom : Form
    {
        public static bool isAdd = false;
        LoaiPhongBUS loaiphongBUS;
       
        public fAddTypeRoom()
        {
            InitializeComponent();
            loaiphongBUS = new LoaiPhongBUS(fLogin.KeystringSQL);
            this.BackColor = Color.DodgerBlue;
            this.Padding = new Padding(1, 1, 1, 1);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            isAdd = loaiphongBUS.AddPhong(txbName.Text.Trim(),txbGiaTheoDem.Text.Trim(), txbSucChua.Text.Trim(), txbMieuTa.Text.Trim());
            if (MessageBoxHelper.Show($"Thêm Loại Phòng: {txbName.Text.Trim()}  {(isAdd ? "Thành Công" : "Thất Bại \nBạn có muốn thoát của sổ")}") == DialogResult.Yes)
                this.Close();
        }

        private void txbSucChua_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                e.Handled = true; // Chặn ký tự không hợp lệ
        }

        private void txbGiaTheoDem_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                e.Handled = true; // Chặn ký tự không hợp lệ
        }

        private void txbGiaTheoDem_TextChange(object sender, EventArgs e)
        {
            if (int.TryParse(txbGiaTheoDem.Text.Trim(), out int amount))
            {
                labelResult.Text = amount.ToString("N0") + " VND"; // Định dạng số tiền và thêm đơn vị

            }
        }

        private void btnEnd_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
