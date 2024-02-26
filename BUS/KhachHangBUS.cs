using BUS.ClassHelper;
using DAO;
using DTO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Windows.Forms;
using Bunifu.Framework.UI;
using GUI;
using System.Drawing;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace BUS
{
    public class KhachHangBUS
    {
        private KhachHangDAO khachHangDAO;

        public KhachHangBUS(string connectionString)
        {
            khachHangDAO = new KhachHangDAO(connectionString);
        }

        public List<KhachHangDTO> LayDanhSachKhachHang()
        {
            return khachHangDAO.LayDanhSachKhachHang();
        }

        public DataTable LayDanhSachKhachHangDataTable()
        {
            return khachHangDAO.LayDanhSachKhachHangDataTable();
        }








        /// cái này bao gồm cả lấy id , sẽ check luôn khách cũ để lấy id ra 
        public int ThemKhachHang(string name,string email,string sdt,string diachi)
        {
            int id = -1;
            if (diachi == "" || email == "" || (!email.Contains("@")) || diachi == "")
            {
                MessageBoxHelper.ShowMessageBox("vui long nhap day du thong tin", 1); return id;
            }


          var a =  khachHangDAO.KiemTraVaLayKhachHang(email.ToLower(),name,sdt,diachi);

            //  id = khachHangDAO.MaKhachHang;
            id = a.MaKhachHang;
            return id;
        }


        
        public int ThemKhachHang(KhachHangDTO khachHang)
        {
            if (string.IsNullOrEmpty(khachHang.HoTen) || string.IsNullOrEmpty(khachHang.Email) || string.IsNullOrEmpty(khachHang.SoDienThoai) || string.IsNullOrEmpty(khachHang.DiaChi))
            {
                MessageBoxHelper.ShowMessageBox("Vui lòng nhập đầy đủ thông tin", 1);
                return 0;
            }

            int isThemKH = khachHangDAO.ThemKhachHang(khachHang);
            MessageBoxHelper.ShowMessageBox($"Thêm khách hàng {(isThemKH > 0 ? "Thành công" : "Thất bại, vui lòng thử lại sau!")}", 1);

            return isThemKH;
        }


        public bool SuaThongTinKhachHang(KhachHangDTO khachHang)
        {
            if (string.IsNullOrEmpty(khachHang.HoTen) || string.IsNullOrEmpty(khachHang.Email) || string.IsNullOrEmpty(khachHang.SoDienThoai) || string.IsNullOrEmpty(khachHang.DiaChi) || khachHang.MaKhachHang == 0)
            {
                MessageBoxHelper.ShowMessageBox("Vui lòng nhập đầy đủ thông tin", 1);
                return false;
            }
            bool isUpdate = khachHangDAO.SuaThongTinKhachHang(khachHang);
            MessageBoxHelper.ShowMessageBox($"Update khách hàng {(isUpdate ? "Thành công" : "Thất bại, vui lòng thử lại sau!")}", 1);
            return isUpdate;
        }

      


        public bool XoaKhachHang(int maKhachHang)
        {
            return khachHangDAO.XoaKhachHang(maKhachHang);
        }

       

        // Lấy thông tin của một khách hàng bằng mã khách hàng
        public KhachHangDTO LayThongTinKhachHang(int maKhachHang)
        {
            return khachHangDAO.LayThongTinKhachHang(maKhachHang);
        }

        public void DoDuLieuArray(TextBox txb)
        {
            
            // Tạo một AutoCompleteStringCollection để lưu trữ dữ liệu cho AutoComplete.
            AutoCompleteStringCollection autoCompleteData = new AutoCompleteStringCollection();

            // Thêm dữ liệu từ mảng vào AutoCompleteStringCollection.
            autoCompleteData.AddRange(khachHangDAO.LayDanhSachEmailKhachHang());

            // Gán AutoCompleteCustomSource của TextBox cho AutoCompleteStringCollection.
            txb.AutoCompleteCustomSource = autoCompleteData;

            // Bật chức năng AutoComplete.
            txb.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txb.AutoCompleteSource = AutoCompleteSource.CustomSource;

        }

        public void DoDuLieuByKeySearch(string keySearch, BunifuMaterialTextbox txbName, BunifuMaterialTextbox txbSDT, BunifuMaterialTextbox txbDiaChi)
        {
            KhachHangDTO KH = khachHangDAO.LayKhachHangTheoEmail(keySearch);
            if (KH != null)
            {
                txbName.Text = KH.HoTen;
                txbSDT.Text = KH.SoDienThoai;
                txbDiaChi.Text = KH.DiaChi;
            }
        }


        public void LoadDataToGridView(DataGridView dataGridView, string timkiemtheotukhoa = "")
        {
            DataTable data = null;

            if (timkiemtheotukhoa == "")
                data = LayDanhSachKhachHangDataTable();
            else
                 data = khachHangDAO.TimKiemKhachHangTheoTuKhoa(timkiemtheotukhoa);

                data.Columns.Add("STT", typeof(int));
            data.Columns.Add("Delete", typeof(Image));
            data.Columns.Add("Edit", typeof(Image));
            int stt = 0;

            foreach (DataRow row in data.Rows)
            {
                    row["Delete"] = Image.FromFile(@"img\cancel_25px.png");
                    row["Edit"] = Image.FromFile(@"img\edit_25px.png");
                stt++;
                row["STT"] = stt;
            }
            dataGridView.DataSource = data;
        }

    }
}
