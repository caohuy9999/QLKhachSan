using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Xml.Linq;
using DAO; // Thêm namespace của DAO
using DTO;
using GUI;

namespace BUS
{
    public class LoaiDichVuBUS
    {
        private readonly LoaiDichVuDAO loaiDichVuDAO;

        private readonly DichVuBUS dichvuBus;

        public LoaiDichVuBUS(string connectionString)
        {
            loaiDichVuDAO = new LoaiDichVuDAO(connectionString);
            dichvuBus = new DichVuBUS(connectionString);
        }

        public List<LoaiDichVuDTO> LayDanhSachLoaiDichVu()
        {
            return loaiDichVuDAO.LayDanhSachLoaiDichVu();
        }

        public LoaiDichVuDTO LayLoaiDichVuTheoMa(int maLoaiDichVu)
        {
            return loaiDichVuDAO.LayLoaiDichVuTheoMa(maLoaiDichVu);
        }

        public bool ThemLoaiDichVu(LoaiDichVuDTO loaiDichVu)
        {
            return loaiDichVuDAO.ThemLoaiDichVu(loaiDichVu);
        }
       





        public bool CapNhatLoaiDichVu(LoaiDichVuDTO loaiDichVu)
        {
            return loaiDichVuDAO.CapNhatLoaiDichVu(loaiDichVu);
        }

        public bool XoaLoaiDichVu(int maLoaiDichVu)
        {
            return loaiDichVuDAO.XoaLoaiDichVu(maLoaiDichVu);
        }




        //---------------------------------------------------------------------\\

        public void LayDanhSachLoaiDichVuView_DTGV(DataGridView dtgv)
        {
            DataTable dataLoaiDichVu = loaiDichVuDAO.LayDanhSachLoaiDichVu_DataTable();

            // Đặt dữ liệu cho DataGridView
            //dtgv.Rows.Clear(); // Xóa bỏ các hàng hiện có (nếu có)
            dataLoaiDichVu.Columns.Add("ImgDelete", typeof(Image));
            dataLoaiDichVu.Columns.Add("ImgEdit", typeof(Image));
            dataLoaiDichVu.Columns.Add("TenDichVu", typeof(string));
            dataLoaiDichVu.Columns.Add("Gia", typeof(string));
            int indexRow = dataLoaiDichVu.Rows.Count;

            for (int i = 0; i < indexRow; i++)
            {
                dataLoaiDichVu.Rows[i]["ImgDelete"] = Image.FromFile(@"img\cancel_25px.png");
                dataLoaiDichVu.Rows[i]["ImgEdit"] = Image.FromFile(@"img\edit_25px.png");
                
              DataTable dtTable = dichvuBus.LayThongTinDichVuTheoMaLoaiDichVu((int)dataLoaiDichVu.Rows[i]["MaLoaiDichVu"], (int)dataLoaiDichVu.Rows[i]["MaDichVu"]);
                dataLoaiDichVu.Rows[i]["TenDichVu"] = dtTable.Rows[0]["TenDichVu"];
                dataLoaiDichVu.Rows[i]["Gia"] = dtTable.Rows[0]["Gia"];
            }
            dtgv.DataSource = dataLoaiDichVu;
        }

        public void LayDanhSachLoaiDichVuView_CBB(ComboBox cbb, bool addNameNew = false)
        {
            DataTable dataLoaiDichVu = loaiDichVuDAO.LayDanhSachLoaiDichVu_DataTable();
            DataRow newRow = dataLoaiDichVu.NewRow();
            if (addNameNew)
            {
                newRow["TenLoaiDichVu"] = ""; // Đặt giá trị cho cột "ColumnName1"
            }
            else
            {
                newRow["TenLoaiDichVu"] = "All"; // Đặt giá trị cho cột "ColumnName1"
            }
            newRow["MaLoaiDichVu"] = 0; // Đặt giá trị cho cột "ColumnName1"
            dataLoaiDichVu.Rows.InsertAt(newRow, 0);// Chèn hàng mới vào đầu DataTable ở vị trí 0

            cbb.DisplayMember = "TenLoaiDichVu"; // Hiển thị Tên Loại phòng trong ComboBox
            cbb.ValueMember = "MaLoaiDichVu"; // Giá trị thực sự (ID) của Loại phòng
            cbb.DataSource = dataLoaiDichVu;
        }

        public bool ThemNameLoaiDichVu(string Name)
        {
            LoaiDichVuDTO addLoaiDV = new LoaiDichVuDTO
            {
                TenLoaiDichVu = Name,
                MoTa = ""

            };
            return loaiDichVuDAO.ThemLoaiDichVu(addLoaiDV);
        }

        public int INSERTLoaiDichVu(string Name)
        {
            LoaiDichVuDTO addLoaiDV = new LoaiDichVuDTO
            {
                TenLoaiDichVu = Name,
                MoTa = ""

            };
            return loaiDichVuDAO.INSERTLoaiDichVu(addLoaiDV);
        }

        public bool UpdateLoaiDichVu(int MaLoaiDichVu, string TenLoaiDichVu, string MoTa)
        {
            LoaiDichVuDTO update = new LoaiDichVuDTO
            {
                MaLoaiDichVu = MaLoaiDichVu,
                TenLoaiDichVu = TenLoaiDichVu,
                MoTa = MoTa
            };
            return loaiDichVuDAO.CapNhatLoaiDichVu(update);
        }
    }
}
