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

namespace QLKhachSan
{
    public partial class fDatPhong : Form
    {
        public List<ItemSP> itemphone;
        public List<ItemSP> itemphonesFilter;
        private PhongBUS phongBUS;
        public fDatPhong()
        {
            InitializeComponent();
        }

        private void fDatPhong_Load(object sender, EventArgs e)
        {
            phongBUS = new PhongBUS(GUI.connectionString);
           var data = phongBUS.GetAllPhong();

            var list = new ItemSP[data.Count];
            int i = 0;

            itemphone = new List<ItemSP>();
            itemphonesFilter = new List<ItemSP>();
            foreach (var item in data)
            {
                list[i] = new ItemSP();
                //list[i].itemValueChanged += Form1_itemValueChanged;
                //list[i].uri_monan = @"img\Menu\" + item.url;
                list[i].soPhong = "Số Phòng:"+ item.SoPhong;
                list[i].loaiPhong = "";
                list[i].tinhTrang = "Tình Trạng:" + item.TinhTrang;
                //list[i].price = item.Gia;
                //list[i].LoadImageAsync();
                itemphonesFilter.Add(list[i]);
                i++;
            }
            flowLayoutPanel.Controls.AddRange(list);
        }
    }
}
