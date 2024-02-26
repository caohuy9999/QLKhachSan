using DAO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            return nhanVienDAO.GetAllNhanVien();
        }

        public void InsertNhanVien(string tenDangNhap, string matKhau, string hoTen, string chucVu, decimal luong)
        {
            // Thực hiện kiểm tra tính hợp lệ của dữ liệu trước khi thêm vào cơ sở dữ liệu (nếu cần)
            // Nếu dữ liệu hợp lệ, bạn có thể gọi phương thức từ DAO để thêm vào cơ sở dữ liệu.
            nhanVienDAO.InsertNhanVien(tenDangNhap, matKhau, hoTen, chucVu, luong);
        }

        // Các phương thức khác cho việc cập nhật, xóa, và xử lý logic kinh doanh khác
    }
}
