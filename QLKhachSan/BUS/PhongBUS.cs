using DAO;
using DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUS
{
    public class PhongBUS
    {
        private PhongDAO phongDAO;

        public PhongBUS(string connectionString)
        {
            phongDAO = new PhongDAO(connectionString);
        }

        public List<PhongDTO> GetAllPhong()
        {
            DataTable phongData = phongDAO.GetAllPhong();
            List<PhongDTO> listPhong = new List<PhongDTO>();

            foreach (DataRow row in phongData.Rows)
            {
                PhongDTO phong = new PhongDTO
                {
                    MaPhong = Convert.ToInt32(row["MaPhong"]),
                    SoPhong = row["SoPhong"].ToString(),
                    MaLoaiPhong = Convert.ToInt32(row["MaLoaiPhong"]),
                    TinhTrang = row["TinhTrang"].ToString()
                };

                listPhong.Add(phong);
            }

            return listPhong;
        }

        public void InsertPhong(PhongDTO phong)
        {
            phongDAO.InsertPhong(phong);
        }

        // Các phương thức khác cho việc cập nhật và xóa dữ liệu
    }
}
