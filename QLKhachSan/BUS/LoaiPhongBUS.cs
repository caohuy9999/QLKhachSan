using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAO;
using DTO;

namespace BUS
{
    public class LoaiPhongBUS
    {
        private LoaiPhongDAO loaiPhongDAO;

        public LoaiPhongBUS(string connectionString)
        {
            loaiPhongDAO = new LoaiPhongDAO(connectionString);
        }

        public List<LoaiPhongDTO> GetAllLoaiPhong()
        {
            DataTable loaiPhongData = loaiPhongDAO.GetAllLoaiPhong();
            List<LoaiPhongDTO> listLoaiPhong = new List<LoaiPhongDTO>();

            foreach (DataRow row in loaiPhongData.Rows)
            {
                LoaiPhongDTO loaiPhong = new LoaiPhongDTO
                {
                    MaLoaiPhong = Convert.ToInt32(row["MaLoaiPhong"]),
                    TenLoaiPhong = row["TenLoaiPhong"].ToString(),
                    GiaTheoGio = Convert.ToDecimal(row["GiaTheoGio"]),
                    GiaTheoNgay = Convert.ToDecimal(row["GiaTheoNgay"]),
                    SucChua = Convert.ToInt32(row["SucChua"]),
                    MieuTa = row["MieuTa"].ToString()
                };

                listLoaiPhong.Add(loaiPhong);
            }

            return listLoaiPhong;
        }

        public void InsertLoaiPhong(LoaiPhongDTO loaiPhong)
        {
            loaiPhongDAO.InsertLoaiPhong(loaiPhong.TenLoaiPhong, loaiPhong.GiaTheoGio, loaiPhong.GiaTheoNgay, loaiPhong.SucChua, loaiPhong.MieuTa);
        }

        // Các phương thức khác cho việc cập nhật và xóa dữ liệu
    }
}
