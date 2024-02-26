using System;
using System.Data;
using System.Data.SqlClient;

namespace QLKhachSan
{
    public class DatabaseHelper
    {
        //// chuoi Ket Noi
        private string connectionString;

        public DatabaseHelper(string connectionString)
        {
            this.connectionString = connectionString;
        }




        // Thực hiện truy vấn và trả về dữ liệu dưới dạng DataTable
        public DataTable ExecuteQuery(string query, SqlParameter[] parameters = null)
        {
            // Tạo một kết nối mới đến cơ sở dữ liệu sử dụng chuỗi kết nối
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Mở kết nối đến cơ sở dữ liệu
                connection.Open();

                // Tạo một đối tượng SqlCommand để thực thi truy vấn
                SqlCommand command = new SqlCommand(query, connection);

                // Kiểm tra và thêm các tham số vào truy vấn nếu có
                if (parameters != null)
                {
                    command.Parameters.AddRange(parameters);
                }

                // Sử dụng SqlDataAdapter để lấy dữ liệu từ truy vấn
                using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                {
                    // Tạo một DataTable để chứa dữ liệu lấy từ cơ sở dữ liệu
                    DataTable dataTable = new DataTable();

                    // Đổ dữ liệu từ SqlDataAdapter vào DataTable
                    adapter.Fill(dataTable);

                    // Trả về DataTable chứa dữ liệu lấy từ cơ sở dữ liệu
                    return dataTable;
                }
            } // Khi khối using này kết thúc, kết nối sẽ tự động đóng
        }





        // Thực hiện truy vấn không trả về dữ liệu (INSERT, UPDATE, DELETE)
        public int ExecuteNonQuery(string query, SqlParameter[] parameters = null)
        {
            // Tạo một kết nối mới đến cơ sở dữ liệu sử dụng chuỗi kết nối
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Mở kết nối đến cơ sở dữ liệu
                connection.Open();

                // Tạo một đối tượng SqlCommand để thực thi truy vấn
                SqlCommand command = new SqlCommand(query, connection);

                // Kiểm tra và thêm các tham số vào truy vấn nếu có
                if (parameters != null)
                {
                    command.Parameters.AddRange(parameters);
                }

                // Thực thi truy vấn và trả về số dòng bị ảnh hưởng
                return command.ExecuteNonQuery();
            } // Khi khối using này kết thúc, kết nối sẽ tự động đóng
        }

        // Thực hiện truy vấn không trả về dữ liệu (INSERT, UPDATE, DELETE)
        public int ExecuteNonQueryGetMaxId(string query, SqlParameter[] parameters = null)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Tạo MaHD mới (giả sử nó là kiểu số nguyên)
                int newMaHD;
                // Lấy giá trị MaHD lớn nhất trong bảng HoaDon
                using (SqlCommand getMaxMaHD = new SqlCommand(query, connection)) // SELECT MAX(MaHD) FROM HoaDon
                {
                    object result = getMaxMaHD.ExecuteScalar();
                    // Nếu bảng trống, bắt đầu từ 1, ngược lại thì cộng thêm 1
                    newMaHD = (result is DBNull) ? 1 : Convert.ToInt32(result) + 1;
                }
                return newMaHD;
            }

        }


    }
}
