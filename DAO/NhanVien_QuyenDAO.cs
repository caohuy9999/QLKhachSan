using DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO
{
    public class NhanVien_QuyenDAO
    {
        private string connectionString;
        DatabaseHelper dbHelper;
        public NhanVien_QuyenDAO(string connectionString)
        {
            this.connectionString = connectionString;
            dbHelper = new DatabaseHelper(connectionString);
        }

        public List<NhanVien_QuyenDTO> GetAllNhanVien_Quyen()
        {
            List<NhanVien_QuyenDTO> nhanVien_QuyenList = new List<NhanVien_QuyenDTO>();

            // Thực hiện truy vấn SQL để lấy dữ liệu từ cơ sở dữ liệu

            // ...

            return nhanVien_QuyenList;
        }

        public void InsertNhanVien_Quyen(NhanVien_QuyenDTO nhanVien_Quyen)
        {
            // Thực hiện truy vấn SQL để chèn dữ liệu vào cơ sở dữ liệu

            // ...
        }

        public int GetQuyenByMaNhanVien(int maNhanVien)
        {
            string query = "SELECT MaQuyen FROM NhanVien_Quyen WHERE MaNhanVien = @MaNhanVien";

            SqlParameter[] parameters = new SqlParameter[]
            {
        new SqlParameter("@MaNhanVien", maNhanVien)
            };

            object result = dbHelper.ExecuteScalar(query, parameters);

            if (result != null)
            {
                return Convert.ToInt32(result);
            }

            // Trả về giá trị mặc định hoặc xử lý nếu không có giá trị nào được trả về
            return -1; // Ví dụ: -1 là giá trị mặc định khi không tìm thấy giá trị
        }

      
    }

}
