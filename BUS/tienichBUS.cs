using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Windows.Forms;
using BUS.ClassHelper;
using DAO;
using DTO;
using QLKhachSan.fHelper.Phong;

namespace BUS
{
    public class tienichBUS
    {
        private tienichDAO tienNghiDAO;

        public tienichBUS(string connectionString)
        {
            tienNghiDAO = new tienichDAO(connectionString);
        }















        public void InsertTienNghi(TienIchDTO tienNghi)
        {
            tienNghiDAO.InsertTienIch(tienNghi);
        }

        public bool InsertTienNghi(string tentienich,string mieuta,string pathIMG,string soluong)
        {
            if (Common.ConvertToInt32(soluong) == 0)
            {
                MessageBoxHelper.ShowMessageBox("Sai dinh dạng vui lòng xem lại !"); return false;
            }
            TienIchDTO tienNghi = new TienIchDTO();
            tienNghi.TenTienIch = tentienich;
           tienNghi.MieuTa = mieuta;
            tienNghi.PathIMG = pathIMG;
            tienNghi.SoLuong = Common.ConvertToInt32(soluong);
           return tienNghiDAO.InsertTienIch(tienNghi);
        }







        public bool UpdateTienNghi(TienIchDTO tienNghi)
        {
           return tienNghiDAO.UpdateTienIch(tienNghi);
        }
        public bool UpdateTienNghi(string MaTienIch, string tentienich, string mieuta, string pathIMG,string soluong)
        {
            if (Common.ConvertToInt32(MaTienIch) == 0 || Common.ConvertToInt32(soluong) == 0)
            {
                MessageBoxHelper.ShowMessageBox("Sai dinh dạng vui lòng xem lại !"); return false;
            }
            TienIchDTO tienNghi = new TienIchDTO();
            tienNghi.MaTienIch = Common.ConvertToInt32(MaTienIch);
            tienNghi.TenTienIch = tentienich;
            tienNghi.MieuTa = mieuta;
            tienNghi.PathIMG = pathIMG;
            tienNghi.SoLuong = Common.ConvertToInt32(soluong);
            return tienNghiDAO.InsertTienIch(tienNghi);
        }



        public bool DeleteTienNghi(string maTienNghi)
        {
            int indexMaTienIch = Common.ConvertToInt32(maTienNghi);
           return tienNghiDAO.DeleteTienIch(indexMaTienIch);
        }

        public List<TienIchDTO> GetAllTienNghi(int typeRoom = 0 , string keyWord = "")
        {
           
            return tienNghiDAO.GetAllTienNghi(typeRoom, keyWord);
        }


        public void PopulateComboBox(ComboBox comboBox)
        {
            List<TienIchDTO> tienIchList = GetAllTienNghi();

            // Gán danh sách vào ComboBox
            comboBox.DataSource = tienIchList;
            comboBox.DisplayMember = "TenTienIch";  // Hiển thị tên tiện ích
            comboBox.ValueMember = "MaTienIch";  // Giữ giá trị của MaTienIch
            comboBox.SelectedIndex = 1;  // Chọn một item đầu tiên (nếu muốn)
        }



        public int GetSoLuongByTenTienIch(string tentienich)
        {
            if (tentienich != "")
            {
                return tienNghiDAO.GetSoLuongByTenTienIch(tentienich);
            }
            return 0;
        }









    }
}
