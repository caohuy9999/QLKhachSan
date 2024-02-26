using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Forms;
using BUS;
using BUS.ClassHelper;
using DTO;
using GUI;
using static QLKhachSan.ItemSP;

namespace QLKhachSan
{
    public partial class fDatPhong : Form
    {
        public List<ItemSP> itemphone;
        public List<ItemSP> itemphonesFilter;
        private PhongBUS phongBUS;
        private LoaiPhongBUS loaiphongBUS;
        private ChiTietLuuTruBUS chitietluutruBUS;
        private ChiTietHoaDonBUS chitiethoadonBUS;
        private bool isInitializing = false;
        private int idHoaDon = 0;
        public fDatPhong()
        {
            InitializeComponent();
           
        }

        private void fDatPhong_Load(object sender, EventArgs e)
        {
            chitietluutruBUS = new ChiTietLuuTruBUS(fMain.connectionString);
            loaiphongBUS = new LoaiPhongBUS(fMain.connectionString);
            phongBUS = new PhongBUS(fMain.connectionString);
            chitiethoadonBUS = new ChiTietHoaDonBUS(fMain.connectionString);
            loadAllPhong();
            loaiphongBUS.GetLoaiPhong(cbbTypeRoom);
            phongBUS.FilterPhongByTinhTrangCBB(cbbFilterStatusRoom,-1);

            isInitializing = true; // Đánh dấu là đang khởi tạo
            timer.Start();
        }


        private void loadAllPhong(string keyword = "" , int cbbTypeRoom = 0 , string cbbStatusRoom = "")
        {
            // Xóa toàn bộ UserControl
            flowLayoutPanel.Controls.Clear();
            List<PhongDTO> data = null;
            data = phongBUS.GetAllPhong(keyword, cbbTypeRoom, cbbStatusRoom);
                       var list = new ItemSP[data.Count];
            int i = 0;

            itemphone = new List<ItemSP>();
            itemphonesFilter = new List<ItemSP>();
            foreach (var item in data)
            {
                list[i] = new ItemSP();
                list[i].Reload += btnLoad_Click; // Gán sự kiện Reload từ UserControl
                list[i].Detail += ptrBoxBtnHidenMenu_Click; // Gán sự kiện Reload từ UserControl
                list[i].itemValueChanged += Form_itemValueChanged;
                list[i].GiaTheoDem = item.GiaTheoDem;
                list[i].loaiPhong = item.TenLoaiPhong;
                list[i].soPhong = $"Số Phòng: {item.SoPhong}";
                list[i].tinhTrang = $"Tình Trạng: {item.TinhTrang}";
                list[i].MaPhong = item.MaPhong;
                list[i].Roomcolor = item.Roomcolor;
                list[i].idHoaDon = item.IdBill;
                //list[i].price = item.Gia;
                //list[i].LoadImageAsync();
              
                itemphonesFilter.Add(list[i]);


                i++;
            }
            flowLayoutPanel.Controls.Clear();
            flowLayoutPanel.Controls.AddRange(list);
        }


        private void btnLoad_Click(object sender, EventArgs e)
        {
            loadAllPhong();
        }


        private void ReloadUserControls(object sender, EventArgs e)
        {
            // Xóa toàn bộ UserControl
            flowLayoutPanel.Controls.Clear();
            var data = phongBUS.GetAllPhong();

            var list = new ItemSP[data.Count];
            int i = 0;

            itemphone = new List<ItemSP>();
            itemphonesFilter = new List<ItemSP>();
            foreach (var item in data)
            {
                list[i] = new ItemSP();
                

                var ChiTietLoaiPhong = loaiphongBUS.GetLoaiPhongByMaLoaiPhong(item.MaLoaiPhong);
                if (ChiTietLoaiPhong != null)
                {
                    list[i].GiaTheoDem = $"Giá theo Đêm: {ChiTietLoaiPhong.GiaTheoDem.ToString("#,##0")}đ";
                    list[i].loaiPhong = ChiTietLoaiPhong.TenLoaiPhong;
                }
                list[i].soPhong = $"Số Phòng: {item.SoPhong}";
                list[i].tinhTrang = $"Tình Trạng: {item.TinhTrang}";
                list[i].MaPhong = item.MaPhong;
                list[i].Roomcolor = item.Roomcolor;
                list[i].idHoaDon = item.IdBill;
                //list[i].price = item.Gia;
                //list[i].LoadImageAsync();
                itemphonesFilter.Add(list[i]);


                i++;
            }
            flowLayoutPanel.Controls.Clear();
            flowLayoutPanel.Controls.AddRange(list);
        }


        private void bunifuMaterialTextbox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                e.Handled = true; // Chặn ký tự không hợp lệ
        }


        private void cbbTypeRoom_SelectedIndexChanged(object sender, EventArgs e)
        {
           if (isInitializing)
                loadAllPhong(txtSearchPhongByKeyword.Text.Trim(), Common.ConvertToInt32(cbbTypeRoom.SelectedValue),cbbFilterStatusRoom.Text.Trim());
        }


        private void cbbFilterStatusRoom_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (isInitializing)
                loadAllPhong(txtSearchPhongByKeyword.Text.Trim(), Common.ConvertToInt32(cbbTypeRoom.SelectedValue), cbbFilterStatusRoom.Text.Trim());
        }
        private bool isScript = false;
        private void timer_Tick(object sender, EventArgs e)
        {
            if (!isScript)
            {
                if (pnlRightMain.Width <= 0)
                {
                    isScript = true;
                    timer.Stop();
                }
                pnlRightMain.Width -= 50;
            }
            else
            {
                if (pnlRightMain.Width >= 300)
                {
                    timer.Stop(); isScript = false;
                }
                pnlRightMain.Width += 50;
            }
            ptrBoxBtnHidenMenu.Visible = !isScript;
            ptrBoxBtnShowMenu.Visible = isScript;
        }

        private void ptrBoxBtnShowMenu_Click(object sender, EventArgs e)
        {
            timer.Start();
        }

        private void ptrBoxBtnHidenMenu_Click(object sender, EventArgs e)
        {





            timer.Start();

        }

        private void Form_itemValueChanged(object sender, ItemValueChangedEventArgs e)
        {
            if (e.IdHoaDon >0)
            {
                lbTileName.Text =$"Phòng {e.SoPhong}";
                DatagridviewHelper.DataCenter(dtgv); DatagridviewHelper.TitleCenter(dtgv);
                chitietluutruBUS.LayDanhSachChiTietLuuTrubyMaDatPhong(e.MaPhong, lblNameKH, null, null, null, DateTimeStarts: DateTimeStarts); // view lên lbl
                chitiethoadonBUS.DisplayChiTietHoaDonOnDataGridView(e.IdHoaDon, dtgv, lblTongTienDV);
            }
            else
            {
                lblTongTienDV.Text = "";
                lbTileName.Text = "";
                DateTimeStarts.Text = "";
                lblNameKH.Text = "";
                dtgv.Rows.Clear();
            }
            idHoaDon = e.IdHoaDon;
            if (isScript)
                timer.Start();
        }

        private void btnAddDichVu_Click(object sender, EventArgs e)
        {
            fAddDichVu f = new fAddDichVu(idHoaDon);
           
            f.ShowDialog();
        }

        private string textChange;
        private void txtSearchPhongByKeyword_TextChange(object sender, EventArgs e)
        {
            if (isInitializing && textChange != txtSearchPhongByKeyword.Text.Trim())
            {
                loadAllPhong(txtSearchPhongByKeyword.Text.Trim(), Common.ConvertToInt32(cbbTypeRoom.SelectedValue), cbbFilterStatusRoom.Text.Trim());
                textChange = txtSearchPhongByKeyword.Text.Trim();
            }    
              
        }
    }
}
