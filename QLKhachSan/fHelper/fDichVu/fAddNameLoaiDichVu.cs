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

namespace QLKhachSan
{
    public partial class fAddNameLoaiDichVu : Form
    {
        public fAddNameLoaiDichVu()
        {
            InitializeComponent();
        }
        internal static bool isSUCCESS = false;
        LoaiDichVuBUS LoaiDichVuBUS;
        DichVuBUS dichvuBUS;
        private void cbbTypeNameLoaiDV_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void txbGiaTienDichVu_TextChanged(object sender, EventArgs e)
        {
            
            if (int.TryParse(txbGiaTienDichVu.Text.Trim(), out int amount))
            {
                labelResult.Text = amount.ToString("N0") + " VND"; // Định dạng số tiền và thêm đơn vị
              
            }
        }

        private void txbGiaTienDichVu_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Kiểm tra nếu ký tự vừa nhập không phải là số và không phải ký tự điều hướng (ví dụ: backspace)
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; // Chặn ký tự không hợp lệ
            }
        }

        private void fAddNameLoaiDichVu_Load(object sender, EventArgs e)
        {
            this.BackColor = Color.DodgerBlue;
            this.Padding = new Padding(1, 1, 1, 1);
            LoaiDichVuBUS = new LoaiDichVuBUS(fMain.connectionString);
            dichvuBUS = new DichVuBUS(fMain.connectionString);
            LoaiDichVuBUS.LayDanhSachLoaiDichVuView_CBB(cbbTypeNameLoaiDV,true);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAddLoaiDichVuNew_Click(object sender, EventArgs e)
        {
            int typeCbb =Convert.ToInt32(cbbTypeNameLoaiDV.SelectedValue);
            string textCbbLoaiDV = cbbTypeNameLoaiDV.Text.Trim();
            string NameNew = txtNameDVNew.Text.Trim();
            if (NameNew != "" && textCbbLoaiDV != "" && txbGiaTienDichVu.Text.Trim() != "") 
            {
                if (typeCbb == 0 && cbbTypeNameLoaiDV.Text.Trim() != "") // THEM LOAI DV MOI 
                {
                    typeCbb = LoaiDichVuBUS.INSERTLoaiDichVu(textCbbLoaiDV);
                }
                else
                { 
                    MessageBoxHelper.ShowMessageBox("Thêm Dịch Vụ New That bai "); return;
                }
              int ketqua =   dichvuBUS.ThemDichVu(NameNew,Convert.ToDecimal(txbGiaTienDichVu.Text.Trim()), typeCbb);
                if (ketqua > 0)
                {
                    MessageBoxHelper.ShowMessageBox("Thêm Dịch Vụ Thành Công"); isSUCCESS = true; this.Close();
                }
                else
                {
                    MessageBoxHelper.ShowMessageBox("Vui lòng điền đầy đủ thông tin trước khi Thêm !");
                }
            }
            else
            {
                MessageBoxHelper.ShowMessageBox("Vui lòng điền đầy đủ thông tin trước khi Thêm !");
            } 
        }

        private void ptrBoxDeleteLoaiDV_Click(object sender, EventArgs e)
        {
            int typeCbb = Convert.ToInt32(cbbTypeNameLoaiDV.SelectedValue);
            string textCbbLoaiDV = cbbTypeNameLoaiDV.Text.Trim();
            if (textCbbLoaiDV == "" || typeCbb == 0)
            {
                MessageBoxHelper.ShowMessageBox("Vui lòng Chọn Loai Dịch Vụ khác để xóa");
                return;
            }
            if (MessageBoxHelper.Show($"Bạn thực sự muốn Xóa Loai Dịch Vụ [{textCbbLoaiDV}][{typeCbb}]\nĐồng nghĩa với việc Xóa Toàn Bộ những Dịch Vụ thuộc loại [{textCbbLoaiDV}][{typeCbb}] ?") == DialogResult.Yes) 
            {
                DataTable data = dichvuBUS.LayThongTinDichVuTheoMaLoaiDichVu(typeCbb);
                foreach (DataRow row in data.Rows)
                {
                    // Truy cập các cột cụ thể trong hàng
                    int maDichVu = Convert.ToInt32(row["MaDichVu"]);

                    if (!dichvuBUS.XoaDichVu(maDichVu))
                    {
                        
                    } 
                }
                
                if (LoaiDichVuBUS.XoaLoaiDichVu(typeCbb))
                {
                    MessageBoxHelper.ShowMessageBox($"Xóa thành công Dịch Vụ [{textCbbLoaiDV}][{typeCbb}] ");
                    LoaiDichVuBUS.LayDanhSachLoaiDichVuView_CBB(cbbTypeNameLoaiDV, true);
                }

            }
        }
    }
}
