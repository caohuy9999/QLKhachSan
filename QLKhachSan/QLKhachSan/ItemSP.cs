using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLKhachSan
{
    public partial class ItemSP : UserControl
    {
        public int MaPhong { get; set; }
        public int MaLoaiPhong { get; set; }

        public string _soPhong;
        public string soPhong {
            set {
                _soPhong = value;
                lbl_SoPhong.Text = value;
            }
            get { return this._soPhong; }
        }


        public string _loaiPhong;
        public string loaiPhong {
            set {
                _loaiPhong = value;
                lbLoaiPhong.Text = value;
            }
            get { return this._loaiPhong; }
        }


        public string _tinhTrang;
        public string tinhTrang {
            set {
                _tinhTrang = value;
                lbTinhTrang.Text = value;
            }
            get { return this._tinhTrang; }
        }
        public ItemSP()
        {
            InitializeComponent();
          //  btnAdd.IconChar = FontAwesome.Sharp.IconChar.Plus;
        }

        private void btnBookRoom_Click(object sender, EventArgs e)
        {

        }

        private void btnCheckOutRoom_Click(object sender, EventArgs e)
        {

        }

        private void btnDetailsRoom_Click(object sender, EventArgs e)
        {

        }

        private void btnChiTiet_Click(object sender, EventArgs e)
        {

        }

        private void btnBook_Click(object sender, EventArgs e)
        {

        }

        private void btnCheckOut_Click(object sender, EventArgs e)
        {

        }
    }
}
