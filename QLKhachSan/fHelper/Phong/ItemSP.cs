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
using GUI;
using QLKhachSan.fHelper.fKhachHang;

namespace QLKhachSan
{
    public partial class ItemSP : UserControl
    {
        ChiTietHoaDonBUS chitiethoadonBUS = new ChiTietHoaDonBUS(fMain.connectionString);
        HoaDonBUS hoadonBUS = new HoaDonBUS(fMain.connectionString);
        PhongBUS phongBUS = new PhongBUS(fMain.connectionString);
        DatPhongBUS datphongBUS = new DatPhongBUS(fMain.connectionString);
     
        public event EventHandler Reload;
       
        public event EventHandler Detail;
        public event ItemValueChangedEventHandler itemValueChanged;
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
                if (!value.Contains("Trống"))
                {
                    btnBook.Visible = false;
                }
                if (value.Contains("Đang dọn dẹp"))
                {
                    btnCheckOut.Visible = false;
                }
                lbTinhTrang.Text = value;
            }
            get { return this._tinhTrang; }
        }

      
        public string _giaTheoDem;
        public string GiaTheoDem {
            set {
                _giaTheoDem = value;

                if (double.TryParse(value, out double giaTheoDemValue))
                    lbGiaTheoDem.Text = $"Giá theo Đêm: {giaTheoDemValue.ToString("#,##0")} VNĐ";
            }
            get { return this._giaTheoDem; }
        }

        public string _idHoaDon;
        public string idHoaDon {
            set {
                _idHoaDon = value;
                lbIdBill.Text = value;
            }
            get { return this.idHoaDon; }
        }



        public Color _roomcolor;
        public Color Roomcolor {
            set {
                _roomcolor = value;
                pnlMainItem.BackColor = value;
            }
            get { return this._roomcolor; }
        }




        public ItemSP()
        {
            InitializeComponent();
            //  btnAdd.IconChar = FontAwesome.Sharp.IconChar.Plus;
         //   datPhongBUS = new DatPhongBUS(GUI.connectionString);
        }

     
        private void btnChiTiet_Click(object sender, EventArgs e)
        {
            fChiTietLuuTru f = new fChiTietLuuTru(MaPhong,_idHoaDon);
            f.ShowDialog();
            if (f.ScriptResult)
            {
                Reload?.Invoke(this, EventArgs.Empty); // Gửi sự kiện Reload đến Form chứa UserControl
            }
        }

        private void btnBook_Click(object sender, EventArgs e)
        {
            fBookPhong f = new fBookPhong(MaPhong);
            f.ShowDialog();
            if (f.bookPhongResult)
            {
                Reload?.Invoke(this, EventArgs.Empty); // Gửi sự kiện Reload đến Form chứa UserControl
            }
        }

        private void btnCheckOut_Click(object sender, EventArgs e)
        {
           
            int _idHoaDo1n = Common.ConvertToInt32(_idHoaDon.Replace("ID Bill :", ""));
            if (_idHoaDo1n == 0)
            {
                MessageBoxHelper.ShowMessageBox("Error MaHoaDon"); return;
            }

            List<ChiTietHoaDonDTO> chiTietHoaDonList = chitiethoadonBUS.GetChiTietHoaDonByMaHoaDon(_idHoaDo1n);
            decimal tongTienDV = 0; // Khởi tạo biến tổng tiền dịch vụ
            foreach (ChiTietHoaDonDTO chiTietHoaDon in chiTietHoaDonList)
            {
                tongTienDV += chiTietHoaDon.ThanhTien;
            }

            HoaDonDTO HoaDon = hoadonBUS.LayHoaDonTheoMa(_idHoaDo1n);

            DateTime timehientai = DateTime.Now;


            Common.ShowForm(new fXuatHoaDon(_idHoaDo1n, MaPhong, timehientai));
            if (!fXuatHoaDon.isInHoaDonThanhCong)
            {
                return;
            }

            // tính lại tiền phòng với time hiện tại ? 


            bool isThanhToan = hoadonBUS.ThanhToan(_idHoaDo1n, tongTienDV, "Tiền Mặt", timehientai:timehientai);
            if (isThanhToan)
            {

                datphongBUS.UpdateDatPhongTinhTrangByMaHoaDon(_idHoaDo1n, "CheckOut Thành Công");



                if (isThanhToan && phongBUS.UpdateStatusPhong(MaPhong, "Đang don dẹp"))
                {
                    // truyển đổi trạng thái của bảng đặt phòng


                    MessageBoxHelper.ShowMessageBox("Thanh Toán Hóa Đơn Thành Công");
                    Reload?.Invoke(this, EventArgs.Empty); // Gửi sự kiện Reload đến Form chứa UserControl
                    return;
                }
                MessageBoxHelper.ShowMessageBox("Thanh Toán Hóa Đơn Thất bại");
            }
        }
           
             /// thay đổi cả chi tiết lưu chú nữa
            
        

        private void ItemSP_Load(object sender, EventArgs e)
        {
            
        }



        #region event View
        private void lbLoaiPhong_Click(object sender, EventArgs e)
        {
            ViewScript(sender);
        }

        private void lbGiaTheoDem_Click(object sender, EventArgs e)
        {
            ViewScript(sender);
        }

        private void lbTinhTrang_Click(object sender, EventArgs e)
        {
            ViewScript(sender);
        }

        private void lbl_SoPhong_Click(object sender, EventArgs e)
        {
            ViewScript(sender);
        }

        private void lbIdBill_Click(object sender, EventArgs e)
        {
            ViewScript(sender);
        }
        #endregion



        private void ViewScript(object sender)
        {
            string valum = this._idHoaDon.Replace("ID Bill :", "");
            int  sophong = Common.ConvertToInt32(this.soPhong.Replace("Số Phòng:", ""));
            int idBill = Common.ConvertToInt32(valum);
            ItemValueChangedEventArgs myArgs = new ItemValueChangedEventArgs(this.MaPhong, sophong, idBill);
            this.itemValueChanged(sender, myArgs);
        }





       
        public delegate void ItemValueChangedEventHandler(object sender, ItemValueChangedEventArgs e);
        public class ItemValueChangedEventArgs : EventArgs
        {
            private int maPhong;
            private int soPhong;
            private int idHoaDon;

            public ItemValueChangedEventArgs(int _maPhong,int _soPhong, int _idPhong)
            {
                this.maPhong = _maPhong;
                this.soPhong = _soPhong;
                this.idHoaDon = _idPhong;
            }
            public int MaPhong { get { return this.maPhong; } }
            public int SoPhong { get { return this.soPhong; } }
            public int IdHoaDon { get { return this.idHoaDon; } }
        }

    }
}
