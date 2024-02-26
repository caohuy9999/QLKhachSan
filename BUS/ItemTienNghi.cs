using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLKhachSan.fHelper.Phong
{
   
    public partial class ItemTienNghi : UserControl
    {
      

        public string _tenTienNghi;
        public string TenTienNghi 
        {
            set { this._tenTienNghi = value; lblNameTienNghi.Text = value.ToString(); }
            get { return _tenTienNghi; }
        }

        public string _pathImg;
        public string PathImg
        {
            set
            {
                this._pathImg = value;
            }
            get { return _pathImg; }
        }


        public string _mieuta;
        public string MieuTa
        {
            set
            {
                this._mieuta = value;
            }
            get { return _mieuta; }
        }




        public int _maTienIch;
        public int MaTienIch 
            {
            set
            {
                this._maTienIch = value;
            }
            get { return _maTienIch; }
        }


        public int _soLuong;
        public int SoLuong
        {
            set
            {
                this._soLuong = value;
                if (value <= 5)
                {
                    lblTonKho.BackColor = Color.FromArgb(192, 0, 0);
                }
                lblTonKho.Text = value.ToString();
            }
            get { return _soLuong; }
        }



        public event ItemValueChangedEventHandler itemValueChanged;

        public ItemTienNghi()
        {
            InitializeComponent();
          
        }


        public Task<Image> LoadImageFromFileAsync(string uri)
        {
            return Task.Run(() => {
                return Image.FromFile(uri);
            });
        }
        public async void LoadImageAsync()
        {
            pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            var image = await LoadImageFromFileAsync(this.PathImg);
            pictureBox.Image = image;


        }

        private void pictureBox_Click(object sender, EventArgs e)
        {
            // Gọi hàm itemValueChanged và truyền tham số
            ItemValueChangedEventArgs myArgs = new ItemValueChangedEventArgs(this.MaTienIch, this.TenTienNghi, this.MieuTa, this.PathImg,this.SoLuong);
            this.itemValueChanged(sender, myArgs);
        }

        public delegate void ItemValueChangedEventHandler(object sender, ItemValueChangedEventArgs e);

        public class ItemValueChangedEventArgs : EventArgs
        {
            private int maTienIch;
            private string tenTienIch;
            private string mieuTa;
            private string pathImg;
            private int soluong;

            public ItemValueChangedEventArgs(int _maTienIch, string _tenTienIch, string _mieuTa, string _pathImg,int _soluong)
            {
                this.maTienIch = _maTienIch;
                this.tenTienIch = _tenTienIch;
                this.mieuTa = _mieuTa;
                this.pathImg = _pathImg;
                this.soluong = _soluong;
            }

            public int MaTienIch { get { return this.maTienIch; } }
            public string TenTienIch { get { return this.tenTienIch; } }
            public string MieuTa { get { return this.mieuTa; } }
            public string PathImg { get { return this.pathImg; } }
            public int SoLuong { get { return this.soluong; } }
        }


    }
}
