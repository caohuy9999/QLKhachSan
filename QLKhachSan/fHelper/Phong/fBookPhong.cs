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
using System.Xml.Linq;
using BUS;
using BUS.ClassHelper;
using DTO;

namespace QLKhachSan
{
    public partial class fBookPhong : Form
    {
        
        private PhongBUS phongBUS;
        private DatPhongBUS datphong;
        private KhachHangBUS khachHangBUS;
        private int selectmaPhong;
        public bool bookPhongResult = false;
        public fBookPhong()
        {
            InitializeComponent();
        }
        public fBookPhong(int maPhong)
        {
            InitializeComponent();
            selectmaPhong = maPhong;
            this.BackColor = Color.DodgerBlue;
            this.Padding = new Padding(1, 1, 1, 1);
        }

        private void fBookPhong_Load(object sender, EventArgs e)
        {

            khachHangBUS = new KhachHangBUS(fMain.connectionString);
            datphong = new DatPhongBUS(fMain.connectionString);
            
            DateTime start = DateTimeDayStart.Value;
            DateTime stop = DateTimeDayStop.Value;
            datphong.PhongkhadungdeBook(dtgvListPhongTrong, start, stop);
            khachHangBUS.DoDuLieuArray(txtEmail2);
          

            foreach (DataGridViewRow row in dtgvListPhongTrong.Rows)
            {
                object cellValue = row.Cells["cMaPhong"].Value;
                if (cellValue != null && cellValue.ToString() == selectmaPhong.ToString())
                {
                    dtgvListPhongTrong.ClearSelection();  // Bôi đen ô trong hàng mong muốn
                    row.Cells["cMaPhong"].Selected = true;
                    dtgvListPhongTrong.CurrentCell = row.Cells["cSoPhong"]; // Đặt con trỏ chuột vào ô
                    break;
                }
            }
        }






        private void btnBookRoom_Click(object sender, EventArgs e)
        {
            
            bool RESULT = datphong.InsertDatPhong(dtgvListPhongSelect, txtName.Text, txtEmail2.Text.Trim(), txtNumberPhone.Text.Trim(), txtDiaChi.Text);
            if (RESULT)
            {
                if (MessageBoxHelper.Show("Book Phòng Thành Công Thoát Form") == DialogResult.Yes)
                {
                    this.Close();
                }
                bookPhongResult = true;
            }
        }

      

        static decimal TinhGiaPhong(DateTime ngayCheckin, DateTime ngayCheckout, decimal giaPhongCoBan = 1000000m)
        {
            decimal giaPhong = 0; // Giá phòng cơ bản là 1 triệu

            TimeSpan thoiGianLuuTru = ngayCheckout - ngayCheckin;
            int soNgay = (int)Math.Ceiling(thoiGianLuuTru.TotalDays); // Số ngày làm tròn lên

            if (soNgay <= 0)
            {
                
            }
            else
            {
                giaPhong += giaPhongCoBan * soNgay;
            }
            // Tính phụ thu dựa trên thời gian check-in
            if (ngayCheckin.TimeOfDay >= new TimeSpan(5, 0, 0) && ngayCheckin.TimeOfDay < new TimeSpan(9, 0, 0))
            {
                giaPhong += giaPhongCoBan * 0.5m; // Check in từ 5h - 9h: Phụ thu 50%
            }
            else if (ngayCheckin.TimeOfDay >= new TimeSpan(9, 0, 0) && ngayCheckin.TimeOfDay < new TimeSpan(14, 0, 0))
            {
                giaPhong += giaPhongCoBan * 0.3m; // Check in từ 9h - 14h: Phụ thu 30%
            }


            // Tính phụ thu dựa trên thời gian check-out
            if (ngayCheckout.TimeOfDay >= new TimeSpan(12, 0, 0) && ngayCheckout.TimeOfDay < new TimeSpan(15, 0, 0))
            {
                giaPhong += giaPhongCoBan * 0.3m; // Trả phòng từ 12h - 15h: Phụ thu 30%
            }
            else if (ngayCheckout.TimeOfDay >= new TimeSpan(15, 0, 0) && ngayCheckout.TimeOfDay < new TimeSpan(18, 0, 0))
            {
                giaPhong += giaPhongCoBan * 0.5m; // Trả phòng từ 15h - 18h: Phụ thu 50%
            }
            else if (ngayCheckout.TimeOfDay >= new TimeSpan(18, 0, 0))
            {
                giaPhong += giaPhongCoBan; // Trả phòng sau 18h: Phụ thu 100%
            }

            return giaPhong;
        }


        



        private void lbTileName_Click(object sender, EventArgs e)
        {

        }

        private void btnEnd_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dtgvListPhongTrong_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == dtgvListPhongTrong.Columns["cIMG"].Index)
            {
                DateTime start = DateTimeDayStart.Value;
                DateTime stop = DateTimeDayStop.Value;
                string GetID = dtgvListPhongTrong.Rows[e.RowIndex].Cells["cMaPhong"].Value.ToString();
                datphong.add_RoomBySelected(dtgvListPhongSelect,GetID, start, stop);
                datphong.deleteRowAndSapXep(dtgvListPhongTrong, e.RowIndex);
                datphong.SumRow(dtgvListPhongSelect, lbl_Tongtien);
            }
        }

        private void dtgvListPhongSelect_CellClick(object sender, DataGridViewCellEventArgs e) // xoa
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == dtgvListPhongSelect.Columns["cImgDelete"].Index)
            {
                datphong.add_SelectedByRoom(dtgvListPhongTrong, dtgvListPhongSelect, e.RowIndex);
                datphong.deleteRowAndSapXep(dtgvListPhongSelect, e.RowIndex,true);
                datphong.SumRow(dtgvListPhongSelect, lbl_Tongtien);
            }
        }

        private void txtNumberPhone_OnValueChanged(object sender, EventArgs e)
        {
            if (Common.IsNumber(txtNumberPhone.Text) || txtNumberPhone.Text == "")
            {
                int counttxtNumberPhone = txtNumberPhone.Text.Length;

                if (counttxtNumberPhone > 10)
                {
                    MessageBoxHelper.ShowMessageBox("Không được nhập quá 10 số", 1);
                    txtNumberPhone.Text = txtNumberPhone.Text.Substring(0, 10); // Giới hạn số lượng số 10 so :D
                }
                else
                {
                    if (counttxtNumberPhone != 0)
                    {
                        lbGhiChuSDT.Visible = true;
                    }
                    lbGhiChuSDT.Text = $"{counttxtNumberPhone}/10";
                }
            }
            else
            {
                MessageBoxHelper.ShowMessageBox("Chỉ nhập số", 1);
                txtNumberPhone.Text = "";
            }


        }

        private void txtNumberPhone_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; // Chặn ký tự không hợp lệ
            }
        }

        private void DateTimeDayStart_ValueChanged(object sender, EventArgs e)
        {
            DateTime start = DateTimeDayStart.Value;
            DateTime stop = DateTimeDayStop.Value;
            datphong.PhongkhadungdeBook(dtgvListPhongTrong, start, stop);
        }

        private void DateTimeDayStop_ValueChanged(object sender, EventArgs e)
        {
            DateTime start = DateTimeDayStart.Value;
            DateTime stop = DateTimeDayStop.Value;
            datphong.PhongkhadungdeBook(dtgvListPhongTrong, start, stop);
        }

        private void txtEmail2_TextChanged(object sender, EventArgs e)
        {
            if (txtEmail2.Text.Trim() !="")
            {
                khachHangBUS.DoDuLieuByKeySearch(txtEmail2.Text.Trim(), txtName, txtNumberPhone, txtDiaChi);
            }
            else
            {
                txtDiaChi.Text = "";
                txtNumberPhone.Text = "";
                txtName.Text = "";
            }
        }

      
    }
}
