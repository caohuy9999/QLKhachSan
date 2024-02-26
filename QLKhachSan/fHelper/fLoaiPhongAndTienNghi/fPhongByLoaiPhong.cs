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
using GUI;
using QLKhachSan.fHelper.fLoaiPhongAndTienNghi;

namespace QLKhachSan.fHelper.Phong
{
    public partial class fPhongByLoaiPhong : Form
    {

        LoaiPhongBUS loaiphongBUS;
        PhongBUS phongBUS;
        private bool ActLoadFrom = false;
        public fPhongByLoaiPhong()
        {
            InitializeComponent();
            DatagridviewHelper.TitleCenter(dtgvTypeRoom);
          
        }

        private void fPhongByLoaiPhong_Load(object sender, EventArgs e)
        {
            loaiphongBUS = new LoaiPhongBUS(fLogin.KeystringSQL);
            phongBUS = new PhongBUS(fLogin.KeystringSQL);
            fload();
            phongBUS.FilterPhongByTinhTrangCBB(cbbStatusRoom, -1);
            ActLoadFrom = true;
        }


        private void fload()
        {
            loaiphongBUS.GetLoaiPhong(cbbTypeRoom);
            loaiphongBUS.GetAllLoaiPhongForDataGridView(dtgvTypeRoom);
           
        }





        private void cbbTypeRoom_SelectedIndexChanged(object sender, EventArgs e)
        {
           
          loaiphongBUS.GetAllLoaiPhongForDataGridView(dtgvTypeRoom, (int)cbbTypeRoom.SelectedValue);
            
        }

        private void dtgvTypeRoom_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            bool isAct = false;
            string content = "";
            int indexMaPhong =Common.ConvertToInt32(DatagridviewHelper.GetStatusDataGridView(dtgvTypeRoom, e.RowIndex, "cMaLoaiPhong"));
            lblPhong.Text = $"ID : {indexMaPhong}. "+ DatagridviewHelper.GetStatusDataGridView(dtgvTypeRoom, e.RowIndex, "cTenLoaiPhong");
            if (e.RowIndex >= 0 && e.ColumnIndex == dtgvTypeRoom.Columns["cEdit"].Index)
            {
                isAct = loaiphongBUS.EditPhong(
                    DatagridviewHelper.GetStatusDataGridView(dtgvTypeRoom, e.RowIndex, "cTenLoaiPhong"),
                     DatagridviewHelper.GetStatusDataGridView(dtgvTypeRoom, e.RowIndex, "cGiaTheoDem"),
                      DatagridviewHelper.GetStatusDataGridView(dtgvTypeRoom, e.RowIndex, "cSucChua"),
                       DatagridviewHelper.GetStatusDataGridView(dtgvTypeRoom, e.RowIndex, "cMieuTa"),
                        DatagridviewHelper.GetStatusDataGridView(dtgvTypeRoom, e.RowIndex, "cMaLoaiPhong")
                    );
                content = $"Cập nhật Loại Phòng {(isAct ? "Thành Công" : "Thất Bại")}";
            }

            if (e.RowIndex >= 0 && e.ColumnIndex == dtgvTypeRoom.Columns["cDelete"].Index)
            {
                isAct = loaiphongBUS.DeleteLoaiPhong(indexMaPhong);
                content = $"Xóa Loại Phòng {(isAct ? "Thành Công" : "Thất Bại")}";
            }
           
            if (isAct)
            {
                MessageBoxHelper.ShowMessageBox(content);
                fload();
            }

            
            loaiphongBUS.GetRoomForDataGridView(dtgvRoom, indexMaPhong);
        }

        private void btnAddTypeRoom_Click(object sender, EventArgs e)
        {
            Common.ShowForm(new fAddTypeRoom());
            if (fAddTypeRoom.isAdd)
            {
                fload();
            } 
        }

        private void dtgvRoom_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            int indexMaPhong = Common.ConvertToInt32(DatagridviewHelper.GetStatusDataGridView(dtgvRoom, e.RowIndex, "cMaPhongP"));

            if (e.RowIndex >= 0 && e.ColumnIndex == dtgvRoom.Columns["cDeleteRoom"].Index)
            {
                
            }
           
            if (e.RowIndex >= 0 && e.ColumnIndex == dtgvRoom.Columns["cTienIchRoom"].Index)
            {


                Common.ShowForm(new fTienNghi.fTienNghi(indexMaPhong));





            }

        }

        private void btnAddRoomNew_Click(object sender, EventArgs e)
        {
            if (lblPhong.Text == "Quản lý phòng")
            {
                MessageBoxHelper.ShowMessageBox("Vui Lòng Chọn Loại Phòng trước khi muốn thêm phòng mới!"); return;
            }

            if (MessageBoxHelper.Show($"Bạn Muốn Thêm Phòng mới ? Loại Phòng {lblPhong.Text.Trim()}") == DialogResult.Yes)
            {
                int MaLoaiPhong = Common.ConvertToInt32(lblPhong.Text.Trim().Split(':')[1].Split('.')[0]);
             bool isAddRoom =    phongBUS.InsertPhong(MaLoaiPhong);

                MessageBoxHelper.ShowMessageBox($"Thêm Phòng Mới {(isAddRoom ?"Thành Công" : "Thất Bại")}");
                if (isAddRoom)
                {
                    fload();
                }
            }
        }

        private void cbbStatusRoom_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!ActLoadFrom)
            {
                return;
            }
            if (lblPhong.Text == "Quản lý phòng")
            {
                MessageBoxHelper.ShowMessageBox("Vui Lòng Chọn Loại Phòng trước khi muốn thêm phòng mới!"); return;
            }
            int MaLoaiPhong = Common.ConvertToInt32(lblPhong.Text.Trim().Split(':')[1].Split('.')[0]);
            loaiphongBUS.GetRoomForDataGridView(dtgvRoom, MaLoaiPhong, cbbStatusRoom.Text.Trim()== "All" ? "" : cbbStatusRoom.Text.Trim(), txbKeywordSearch.Text.Trim());
        }

        private void txbKeywordSearch_TextChange(object sender, EventArgs e)
        {
            if (!ActLoadFrom)
                return;
            if (lblPhong.Text == "Quản lý phòng" )
            {
                MessageBoxHelper.ShowMessageBox("Vui Lòng Chọn Loại Phòng trước khi muốn thêm phòng mới!"); return;
            }
            int MaLoaiPhong = Common.ConvertToInt32(lblPhong.Text.Trim().Split(':')[1].Split('.')[0]);
            loaiphongBUS.GetRoomForDataGridView(dtgvRoom, MaLoaiPhong, cbbStatusRoom.Text.Trim() == "All" ? "" : cbbStatusRoom.Text.Trim(), txbKeywordSearch.Text.Trim());
        }

        private void txbKeywordSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; // Chặn ký tự không hợp lệ
            }
        }
    }
}
