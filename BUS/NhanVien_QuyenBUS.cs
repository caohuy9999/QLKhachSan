using DAO;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUS
{
    public class NhanVien_QuyenBUS
    {
        private NhanVien_QuyenDAO nhanVien_QuyenDAO;

        public NhanVien_QuyenBUS(string connectionString)
        {
            this.nhanVien_QuyenDAO = new NhanVien_QuyenDAO(connectionString);
        }

        public List<NhanVien_QuyenDTO> GetAllNhanVien_Quyen()
        {
            return nhanVien_QuyenDAO.GetAllNhanVien_Quyen();
        }

        public void InsertNhanVien_Quyen(NhanVien_QuyenDTO nhanVien_Quyen)
        {
            // Kiểm tra logic kinh doanh nếu cần

            nhanVien_QuyenDAO.InsertNhanVien_Quyen(nhanVien_Quyen);
        }

        public int GetQuyenByMaNhanVien(int  MaNhanVien)
        {
            return nhanVien_QuyenDAO.GetQuyenByMaNhanVien(MaNhanVien);
        }
    }

}
