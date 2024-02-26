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

namespace QLKhachSan
{
    public partial class fChiTietLuuTru : Form
    {

        private int selectmaPhong;
        private int idHoaDon;
        private PhongBUS phongBUS;
        private DatPhongBUS datphong;
        private KhachHangBUS khachHangBUS;
        private ChiTietLuuTruBUS chitietluutruBUS;
        private DichVuSuDungBUS dichvuduocsudungBUS;
        private ChiTietHoaDonBUS chitiethoadonBUS;
        private HoaDonBUS hoadonBUS;
        private DatPhongBUS datphongBUS;

        public bool ScriptResult = false;

        public event EventHandler Reload;

        public fChiTietLuuTru()
        {
            InitializeComponent();
        }

        public fChiTietLuuTru(int maPhong,string idBill)
        {
            InitializeComponent();
            selectmaPhong = maPhong;
            idHoaDon = Common.ConvertToInt32(idBill.Replace("ID Bill :", ""));
             chitietluutruBUS = new ChiTietLuuTruBUS(fMain.connectionString);
            phongBUS = new PhongBUS(fMain.connectionString);
            dichvuduocsudungBUS = new DichVuSuDungBUS(fMain.connectionString);
            chitiethoadonBUS = new ChiTietHoaDonBUS(fMain.connectionString);
            hoadonBUS = new HoaDonBUS(fMain.connectionString);
            datphongBUS = new DatPhongBUS(fMain.connectionString);


            phongBUS.FilterPhongByTinhTrangCBB(cbbStatusPhong, maPhong);
            phongBUS.GetInfoPhongByID(maPhong, lbTileName, cbbStatusPhong, lblPhongTrong, btnBookAndUpdate, btnAddDichVu); // ẩn hiện lbl , btn
            chitietluutruBUS.LayDanhSachChiTietLuuTrubyMaDatPhong(maPhong, lblNameKH, DateTimeStart, lblStopByNight, lblNumberNguoi); // view lên lbl
            chitiethoadonBUS.DisplayChiTietHoaDonOnDataGridView(idHoaDon, dtgv, lblTongTienDV);

        }














        private void btnAddDichVu_Click(object sender, EventArgs e)
        {
            fAddDichVu f = new fAddDichVu(idHoaDon);
            f.ShowDialog();
            
                if (f.isAddDV)
                {
                phongBUS.FilterPhongByTinhTrangCBB(cbbStatusPhong, selectmaPhong);
                phongBUS.GetInfoPhongByID(selectmaPhong, lbTileName, cbbStatusPhong, lblPhongTrong, btnBookAndUpdate, btnAddDichVu); // ẩn hiện lbl , btn
                chitietluutruBUS.LayDanhSachChiTietLuuTrubyMaDatPhong(selectmaPhong, lblNameKH, DateTimeStart, lblStopByNight, lblNumberNguoi); // view lên lbl
                chitiethoadonBUS.DisplayChiTietHoaDonOnDataGridView(idHoaDon, dtgv, lblTongTienDV);
                }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void lbTileName_Click(object sender, EventArgs e)
        {

        }

        private void metroComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void metroComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnCheckOut_Click(object sender, EventArgs e)
        {

        }

        private void btnBook_Click(object sender, EventArgs e)
        {

        }

        private void bunifuThinButton21_Click(object sender, EventArgs e)
        {

        }

        private void fChiTietLuuTru_Load(object sender, EventArgs e)
        {
            this.BackColor = Color.DodgerBlue;
            this.Padding = new Padding(1, 1, 1, 1);
        }

      
        private void btnBook_Click_1(object sender, EventArgs e)
        {
            btnBookAndUpdate.Visible = false;
            btnAddDichVu.Visible = true;
        }

        
      

        private void btnBookRoom_Click(object sender, EventArgs e)
        {

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnBookAndUpdate_Click(object sender, EventArgs e)
        {
            bool act = false;
            string Content = "";
            switch (btnBookAndUpdate.ButtonText)
            {
                case "Cập Nhật":
                    act = phongBUS.UpdateStatusPhong(selectmaPhong, cbbStatusPhong.Text.Trim());
                    Content =$"Cập nhật trạng thái phòng {(act ? "Thành Công" : "Thất Bại")}";
                    ScriptResult = act;
                    break;
                case "Nhận Phòng":



                    break;
                case "Thanh Toán":

                    int _idHoaDo1n = idHoaDon;
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

                    //HoaDonDTO HoaDon = hoadonBUS.LayHoaDonTheoMa(_idHoaDo1n);

                    // tính lại tiền phòng với time hiện tại ? 


                    act = hoadonBUS.ThanhToan(_idHoaDo1n, tongTienDV, "Tiền Mặt");
                    if (act)
                    {

                        datphongBUS.UpdateDatPhongTinhTrangByMaHoaDon(_idHoaDo1n, "CheckOut Thành Công");



                        if (act && phongBUS.UpdateStatusPhong(selectmaPhong, "Đang don dẹp"))
                        {
                            // truyển đổi trạng thái của bảng đặt phòng


                            MessageBoxHelper.ShowMessageBox("Thanh Toán Hóa Đơn Thành Công");
                            Reload?.Invoke(this, EventArgs.Empty); // Gửi sự kiện Reload đến Form chứa UserControl
                         
                        }
                        Content = $"Thanh Toán Hóa Đơn {(act ? "Thành Công" : "Thất Bại")}";
                    }


                    break;
                case "Book Phòng":

                    fBookPhong f = new fBookPhong(selectmaPhong);
                    f.ShowDialog();
                    act = f.bookPhongResult;
                    ScriptResult = f.bookPhongResult;
                    Content = $"Book Phòng {(act ? "Thành Công" : "Thất Bại")}";

                    break;
            }


            if (act && MessageBoxHelper.Show(Content + "\nBạn có muốn thoát của sổ?") == DialogResult.Yes)
            {
                this.Close();
            }
        }
    }
}
