using System;
using System.Collections.Generic;
using DTO; // Đảm bảo rằng bạn đã import các lớp DTO cần thiết
using DAO;
using System.Windows.Forms;
using System.Data;

namespace BUS
{
    public class DichVuSuDungBUS
    {
        private DichVuSuDungDAO dichVuSuDungDAO;
        private PhongDAO phongDAO;
        private ChiTietLuuTruDAO chitietluutruDAO;
        private DichVuBUS dichvuBus;

        private string ConnectionString;
        public DichVuSuDungBUS(string connectionString)
        {
            ConnectionString = connectionString;
            dichVuSuDungDAO = new DichVuSuDungDAO(connectionString);

        }

        public List<DichVuSuDungDTO> LayDanhSachDichVuSuDung()
        {
            return dichVuSuDungDAO.LayDanhSachDichVuSuDung();
        }

        public void LayDanhSachDuocSuDungBySoPhong_DataGirdView(DataGridView dtgv,int maPhong)
        {
            phongDAO = new PhongDAO(ConnectionString);
            dichvuBus = new DichVuBUS(ConnectionString);
            chitietluutruDAO = new ChiTietLuuTruDAO(ConnectionString);

            int maDatPhong = phongDAO.LayMaDatPhongTheoMaPhongByDangSuDung(maPhong);
            DataTable data = chitietluutruDAO.LayDanhSachChiTietLuuTruTheoMaDatPhong(maDatPhong); // LẤY MaDatPhong trong hóa đơn hoặc ở bảng đặt phòng cx đc :D
            int MaDatPhong = (int)data.Rows[0]["MaDatPhong"];




            DataTable data2 = dichVuSuDungDAO.LayDanhSachDichVuSuDungByMaDatPhong(MaDatPhong);
            if (data2.Rows.Count < 0)
            {
                return;
            }
            DataTable DV = dichvuBus.LayThongTinDichVuTheoMa((int)data2.Rows[0]["MaDichVu"]);

            data2.Columns.Add("TenDichVu", typeof(string));
            data2.Columns.Add("DonGia", typeof(string));
            data2.Columns.Add("ThanhTien", typeof(string));

            //for (int i = 0; i < data2.Rows.Count; i++)
            //{
            //    decimal donGia = (decimal)DV.Rows[0]["Gia"];
            //    int soluong = (int)data2.Rows[i]



            //    data.Rows[i]["TenDichVu"] = DV.Rows[0]["TenDichVu"];
            //    data.Rows[i]["DonGia"] = DV.Rows[0]["Gia"];
            //    data.Rows[i]["ThanhTien"] = loaiDV.MoTa;
            //}
            dtgv.DataSource = data2;

        }

        public int ThemDichVuSuDung(DichVuSuDungDTO dichVuSuDung)
        {
            // Thực hiện kiểm tra logic nghiệp vụ trước khi thêm dịch vụ sử dụng (nếu cần)
            // Ví dụ: kiểm tra xem dịch vụ có sẵn trong cơ sở dữ liệu hay không

            // Sau đó, gọi DAO để thêm dịch vụ sử dụng vào cơ sở dữ liệu
            return dichVuSuDungDAO.ThemDichVuSuDung(dichVuSuDung);
        }

        public int CapNhatDichVuSuDung(DichVuSuDungDTO dichVuSuDung)
        {
            // Thực hiện kiểm tra logic nghiệp vụ trước khi cập nhật dịch vụ sử dụng (nếu cần)

            // Sau đó, gọi DAO để cập nhật thông tin dịch vụ sử dụng
            return dichVuSuDungDAO.CapNhatDichVuSuDung(dichVuSuDung);
        }

        public int XoaDichVuSuDung(int maSuDung)
        {
            // Thực hiện kiểm tra logic nghiệp vụ trước khi xóa dịch vụ sử dụng (nếu cần)

            // Sau đó, gọi DAO để xóa dịch vụ sử dụng khỏi cơ sở dữ liệu
            return dichVuSuDungDAO.XoaDichVuSuDung(maSuDung);
        }
    }
}
