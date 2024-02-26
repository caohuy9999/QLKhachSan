using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BUS;
using BUS.ClassHelper;
using DTO;
using QLKhachSan.fHelper.Phong;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace QLKhachSan.fHelper.fTienNghi
{
    public partial class fTienNghi : Form
    {
        int ValumPhong;
        tienichBUS TienNghiBUS;
        PhongBUS phongBUS;
        public fTienNghi(int valumPhong = 0)
        {
            InitializeComponent();
            TienNghiBUS = new tienichBUS(fLogin.KeystringSQL);
            phongBUS = new PhongBUS(fLogin.KeystringSQL);
            if (valumPhong != 0)
            {
                this.BackColor = Color.DodgerBlue;
                this.Padding = new Padding(1, 1, 1, 1);
            }
            
            ValumPhong = valumPhong;
            timer.Start();
        }

        private void fTienNghi_Load(object sender, EventArgs e)
        {
            cbbFilter();
         //   doDataView();
        }


        void cbbFilter()
        {
            phongBUS.GetAllSoPhong_NameCBB(cbbFilterRoom);
            cbbFilterRoom.SelectedValue = ValumPhong;
        }
        void doDataView(int typeRoom = 0 , string keyword = "")
        {
            flowLayoutPanel.Controls.Clear(); // Xóa tất cả các mục trong flowLayoutPanel
            PullDataView(flowLayoutPanel, typeRoom, keyword);


        }



        public void PullDataView(FlowLayoutPanel flowLayoutPanel, int typeRoom = 0, string keyWord = "")
        {
            flowLayoutPanel.Controls.Clear(); // Xóa tất cả các mục trong flowLayoutPanel

            var data = TienNghiBUS.GetAllTienNghi(typeRoom, keyWord);

            foreach (var item in data)
            {
                ItemTienNghi newItem = CreateItemTienNghi(item);
                flowLayoutPanel.Controls.Add(newItem);
            }
        }

        private ItemTienNghi CreateItemTienNghi(TienIchDTO item)
        {
            var newItem = new ItemTienNghi();
            newItem.itemValueChanged += Form_itemValueChanged;
            newItem.PathImg = Path.GetDirectoryName(Application.ExecutablePath) + item.PathIMG;

            if (Common.FileTonTai(newItem.PathImg))
            {
                newItem.LoadImageAsync();
            }

            newItem.TenTienNghi = item.TenTienIch;
            newItem.MieuTa = item.MieuTa;
            newItem.MaTienIch = item.MaTienIch;
            newItem.SoLuong = item.SoLuong;
            return newItem;
        }


        private void Form_itemValueChanged(object sender, ItemTienNghi.ItemValueChangedEventArgs e)
        {
            txbMaTienIch.Text = e.MaTienIch.ToString();
            txbNameTienich.Text = e.TenTienIch.ToString();
            txbMieuTa.Text = e.MieuTa.ToString();
            pathImg = e.PathImg;
            if (Common.FileTonTai(pathImg))
            {
                pictureBoxImgTienIch.Image = Image.FromFile(pathImg);
            }
            else
            {
                pictureBoxImgTienIch.Image = null;
            }
            isScript = false;
            timer.Start();

        }







        private void btnEnd_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cbbFilterRoom_SelectedIndexChanged(object sender, EventArgs e)
        {
            int typeRoom = Common.ConvertToInt32(cbbFilterRoom.SelectedValue);
            doDataView(typeRoom == 0 ? 0 : typeRoom, txtSearchPhongByKeyword.Text.Trim());
        }

        private void txtSearchPhongByKeyword_TextChange(object sender, EventArgs e)
        {
            doDataView((int)cbbFilterRoom.SelectedValue == 0 ? 0 : (int)cbbFilterRoom.SelectedValue, txtSearchPhongByKeyword.Text.Trim());
        }

        bool isScript = true;
        private void timer_Tick(object sender, EventArgs e)
        {
            if (isScript)
            {
               
                if (pnlMainRight.Width <= 0)
                {
                    timer.Stop(); isScript = false; return;
                }
                pnlMainRight.Width -= 10;
            }
            else
            {
               
                if (pnlMainRight.Width >= 170)
                {
                    timer.Stop(); isScript = true;return;
                }
                pnlMainRight.Width += 10;
            }
        }



        private void btnThem_Click(object sender, EventArgs e)
        {
            pnlShowScript.Visible = true;
                pnlScript.Visible = false;

                txbMaTienIch.Visible = false;
                lblMaTienIch.Visible = false;

                txbNameTienich.Text = "";
                txbSoLuong.Text = "";
                txbMieuTa.Text = "";
                pictureBoxImgTienIch.Image = null;
                pathImg = "";


                btnXacNhan.IdleBorderColor = Color.FromArgb(30, 144, 255);
                btnXacNhan.IdleFillColor = Color.FromArgb(30, 144, 255);

                btnXacNhan.Text = "Xác Nhận Thêm";
                btnXacNhan.Visible = true;
                isScript = false;
                timer.Start();
            
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {

            pnlShowScript.Visible = true;
            pnlScript.Visible = false;

            txbMaTienIch.Visible = true;
            lblMaTienIch.Visible = true;


            btnXacNhan.IdleBorderColor = Color.FromArgb(255, 192, 128);
                btnXacNhan.IdleFillColor = Color.FromArgb(255, 192, 128);

                btnXacNhan.Text = "Xác Nhận Sửa";
                btnXacNhan.Visible = true;
                isScript = false;
                timer.Start();
            
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            pnlShowScript.Visible = true;
            pnlScript.Visible = false;


            txbMaTienIch.Visible = true;
            lblMaTienIch.Visible = true;

            btnXacNhan.IdleBorderColor = Color.FromArgb(255, 128, 128);
                btnXacNhan.IdleFillColor = Color.FromArgb(255, 128, 128);

                btnXacNhan.Text = "Xác Nhận Xóa";
                btnXacNhan.Visible = true;
                isScript = false;
                timer.Start();
            
        }


        private void btnXacNhan_Click(object sender, EventArgs e)
        {
            bool isAct  = false;
            string content = "";
            switch (btnXacNhan.Text)
            {
                case "Xác Nhận Thêm":

                    if (MessageBoxHelper.Show("Bạn Muốn Thêm Tiện Nghi Mới?") == DialogResult.No)
                        goto end;

                     isAct = TienNghiBUS.InsertTienNghi(txbNameTienich.Text.Trim(), txbMieuTa.Text.Trim(), pathImg, txbSoLuong.Text.Trim());
                    content = $"Thêm Tiện ích mới {(isAct ? "Thành Công" : "Thất Bại")}";
                    break;


                case "Xác Nhận Sửa":
                    if (MessageBoxHelper.Show($"Bạn Muốn Update Tiện Nghi [{txbNameTienich.Text.Trim()}] ?") == DialogResult.No)
                        goto end;
                    isAct = TienNghiBUS.UpdateTienNghi(txbMaTienIch.Text.Trim(),txbNameTienich.Text.Trim(), txbMieuTa.Text.Trim(), pathImg,txbSoLuong.Text.Trim());
                    content = $"Sửa Tiện ích {(isAct ? "Thành Công" : "Thất Bại")}";
                    break;


                case "Xác Nhận Xóa":
                    if (MessageBoxHelper.Show($"Bạn Muốn Xóa Tiện Nghi [{txbNameTienich.Text.Trim()}] ra khỏi hệ thống?") == DialogResult.No)
                        goto end;
                    isAct = TienNghiBUS.DeleteTienNghi(txbMaTienIch.Text.Trim());
                    content = $"Xóa Tiện ích {(isAct ? "Thành Công" : "Thất Bại")}";
                    break;

            }
            MessageBoxHelper.Show(content);
            end:
            timer.Start();
            if (isAct)
            {
                doDataView((int)cbbFilterRoom.SelectedValue == 0 ? 0 : (int)cbbFilterRoom.SelectedValue, txtSearchPhongByKeyword.Text.Trim());
            }

            this.Invoke((MethodInvoker)delegate
            {
                pnlScript.Visible = true;
                btnXacNhan.Visible = false;
                pnlShowScript.Visible = false;
            });
           

        }
        private string pathImg = "";
        private void pictureBoxBtn_Click(object sender, EventArgs e)
        {
            pathImg = Common.SelectImage(); // Gọi hàm để chọn hình ảnh
            if (!string.IsNullOrEmpty(pathImg))
            {
                pictureBoxImgTienIch.Image = Image.FromFile(pathImg);
                string NameFile = Common.Path_NameFile(pathImg);
                if (Common.CopyFile(pathImg, Common.PathExE() + $"\\img\\TienNghi\\{NameFile}"))
                    pathImg = $"\\img\\TienNghi\\{NameFile}";
            }
        }

        private void prcboxBtnHiden_Click(object sender, EventArgs e)
        {
            isScript = true;
            timer.Start();
        }

        private void txbSoLuong_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                e.Handled = true; // Chặn ký tự không hợp lệ
        }

        private void lblShowScript_Click(object sender, EventArgs e)
        {
            this.Invoke((MethodInvoker)delegate {
                pnlScript.Visible = true;
                pnlShowScript.Visible = false;
            });
        }
    }
}
