using BUS.ClassHelper;
using DAO;
using DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace BUS
{
    public class NhanVienBUS
    {
        private NhanVienDAO nhanVienDAO;

        public NhanVienBUS(string connectionString)
        {
            nhanVienDAO = new NhanVienDAO(connectionString);
        }

        public DataTable GetAllNhanVien()
        {
            return nhanVienDAO.GetAllNhanVienDataTable();
        }

        public DataTable Login(string user, string passwork)
        {
            string username = user.Trim();
            string password = passwork.Trim();
            DataTable result = new DataTable();
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                return result;
            }
            return nhanVienDAO.isLoginDAO(username, password);
        }

        public bool InsertNhanVien(string tenDangNhap, string matKhau, string hoTen, string chucVu, decimal luong,string pathAvatar)
        {
            if (string.IsNullOrWhiteSpace(tenDangNhap) || string.IsNullOrWhiteSpace(matKhau) || string.IsNullOrWhiteSpace(hoTen) || string.IsNullOrWhiteSpace(chucVu))
            {
                MessageBoxHelper.ShowMessageBox("Vui lòng nhập đầy đủ thông tin!");return false;
            }

            if (nhanVienDAO.IsUsernameExists(tenDangNhap))
            {
                MessageBoxHelper.ShowMessageBox("Tên đăng nhâp đã được sử dụng"); return false;
            }

            if (MessageBoxHelper.Show("Bạn có muốn tạo tài khoản mới?") == DialogResult.No)
                return false;

            bool inserted = nhanVienDAO.InsertNhanVien(tenDangNhap, matKhau, hoTen, chucVu, luong, pathAvatar) > 0;
            MessageBoxHelper.ShowMessageBox($"Tạo tài khoản mới {(inserted ? "thành công" : "Thất bại, thử lại sau")}!");
            return inserted;
        }

        public void LoadDataToGridView(DataGridView dataGridView,string timkiemtheotukhoa = "")
        {
            DataTable data = null;

            if (timkiemtheotukhoa == "")
                data = GetAllNhanVien();
            else
                data = TimKiemNhanVienTheoTuKhoa(timkiemtheotukhoa);

            data.Columns.Add("STT", typeof(int));
            data.Columns.Add("ImgAvatar", typeof(Image));
            int stt = 0;

            foreach (DataRow row in data.Rows)
            {
                string pathIMG = Common.PathExE() + row["PathAvatar"].ToString();
                if (Common.FileTonTai(pathIMG))
                {
                    row["ImgAvatar"] = Image.FromFile(pathIMG);
                }
                stt++;
                row["STT"] = stt;
            }
            dataGridView.DataSource = data;
        }

        public bool Update(int maNhanVien, string tenDangNhap, string matKhau, string hoTen,string chucVu, string pathImg)
        {
            
            if (string.IsNullOrWhiteSpace(tenDangNhap) || string.IsNullOrWhiteSpace(matKhau) || string.IsNullOrWhiteSpace(hoTen) || string.IsNullOrWhiteSpace(chucVu))
            {
                MessageBoxHelper.ShowMessageBox("Vui lòng nhập đầy đủ thông tin!"); return false;
            }

            NhanVienDTO nvDTO = new NhanVienDTO
                 {
                MaNhanVien = maNhanVien,
                     TenDangNhap = tenDangNhap,
                     MatKhau = matKhau,
                     HoTen = hoTen,
                     ChucVu = chucVu,
                     PathAvatar = pathImg
                 };

            bool isUpdate = nhanVienDAO.UpdateNhanVien(nvDTO);
            MessageBoxHelper.ShowMessageBox($"Update tài khoản {(isUpdate ? "thành công" : "Thất bại, thử lại sau")}!");
            return isUpdate;
        }

        public bool XoaNhanVien(int maNhanVien)
        {
            if (maNhanVien == 0)
            {
                MessageBoxHelper.ShowMessageBox("Vui lòng nhập đầy đủ thông tin!"); return false;
            }
           bool isDelete = nhanVienDAO.DeleteNhanVien(maNhanVien);
            MessageBoxHelper.ShowMessageBox($"Xóa tài khoản {(isDelete ? "thành công" : "Thất bại, thử lại sau")}!");
            return isDelete;
        }

        public DataTable TimKiemNhanVienTheoTuKhoa(string tuKhoa)
        {
            return nhanVienDAO.TimKiemNhanVienTheoTuKhoa(tuKhoa);
        }

        public string GetPathAvatar(int maTaiKhoan)
        {
            return nhanVienDAO.GetPathAvatarForNhienVien(maTaiKhoan);
        }

        public void dumpComboboxData(System.Windows.Forms.ComboBox cbb)
        {
            DataTable dataTable = GetAllNhanVien();
            if (dataTable.Rows.Count > 0)
            {
                DataRow newRow = dataTable.NewRow();
                newRow["HoTen"] = "All"; // Đặt giá trị cho cột "ColumnName1"
                newRow["MaNhanVien"] = 0; // Đặt giá trị cho cột "ColumnName2"
                dataTable.Rows.InsertAt(newRow, 0);// Chèn hàng mới vào đầu DataTable ở vị trí 0
                cbb.DisplayMember = "HoTen"; // Hiển thị Tên Loại phòng trong ComboBox
                cbb.ValueMember = "MaNhanVien"; // Giá trị thực sự (ID) của Loại phòng
                cbb.DataSource = dataTable;
            }
        }

    }
}
