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

namespace QLKhachSan.fHelper.TienIchPhong
{
    public partial class fScriptTienIchPhong : Form
    {
        PhongBUS phongBUS;
        private tienichBUS tienichBUS = null;
        private PhongTienIchDTO TienichphongDTO = null;
        private PhongTienIchBUS phongtienichBUS;
        public static bool isAtc = false;
        public fScriptTienIchPhong(PhongTienIchDTO tienichphongDTO,string sophong)
        {
            InitializeComponent();
            TienichphongDTO = tienichphongDTO;
            // SỬA
            txbMaPhongTienIch.Visible = true;
            lblMaPhongTienIch.Visible = true;
            btnAddAndUpdate.ButtonText = "Update";
            lblNameTop.Text = $"Update Tiện Nghi Phòng: {sophong}";
        }


        public fScriptTienIchPhong()
        {
            InitializeComponent();
          
        }
        private void btnEnd_Click(object sender, EventArgs e)
        {
            this.Close();
        }





        private void btnAddAndUpdate_Click(object sender, EventArgs e)
        {

            if (CountSoLuongTienNghi.Value == 0)
            {
                MessageBoxHelper.ShowMessageBox("SỐ LƯỢNG KHÔNG ĐƯỢC = 0"); return;
            }


            if (btnAddAndUpdate.ButtonText== "Update")
            {
                 isAtc = phongtienichBUS.SuaPhongTienIch(Common.ConvertToInt32(txbMaPhongTienIch.Text.Trim()), Common.ConvertToInt32(cbbFilterRoom.SelectedValue), Common.ConvertToInt32(cbbTienNghi.SelectedValue), txbTrangThai.Text.Trim(), Common.ConvertToInt32(CountSoLuongTienNghi.Value));
                if (isAtc)
                {
                    this.Close();
                }
            }
            else if(btnAddAndUpdate.ButtonText == "Add")
            {
                isAtc = phongtienichBUS.ThemPhongTienIch(Common.ConvertToInt32(cbbFilterRoom.SelectedValue), Common.ConvertToInt32(cbbTienNghi.SelectedValue), txbTrangThai.Text.Trim(), Common.ConvertToInt32(CountSoLuongTienNghi.Value));
                if (isAtc)
                {
                   this.Close();
                }
            }

        }

        private void fScriptTienIchPhong_Load(object sender, EventArgs e)
        {
            this.BackColor = Color.DodgerBlue;
            this.Padding = new Padding(1, 1, 1, 1);
            phongtienichBUS = new PhongTienIchBUS(fLogin.KeystringSQL);
            phongBUS = new PhongBUS(fLogin.KeystringSQL);
            tienichBUS = new tienichBUS(fLogin.KeystringSQL);

            phongBUS.GetAllSoPhong_NameCBB(cbbFilterRoom,false);

            tienichBUS.PopulateComboBox(cbbTienNghi);

            if (TienichphongDTO != null)
            {
                CountSoLuongTienNghi.Minimum = TienichphongDTO.SoLuong;

                txbMaPhongTienIch.Text = TienichphongDTO.MaTienIch.ToString();
                cbbFilterRoom.SelectedValue = TienichphongDTO.MaPhong;
                CountSoLuongTienNghi.Value = TienichphongDTO.SoLuong;
                cbbTienNghi.SelectedValue = TienichphongDTO.MaTienIch;
                txbTrangThai.Text = TienichphongDTO.TrangThai;

               

            }

        }

        private void cbbTienNghi_SelectedIndexChanged(object sender, EventArgs e)
        {
            CountSoLuongTienNghi.Maximum = tienichBUS.GetSoLuongByTenTienIch(cbbTienNghi.Text.Trim());
        }
    }
}
