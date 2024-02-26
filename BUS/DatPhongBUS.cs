using BUS.ClassHelper;
using DAO;
using DTO;
using GUI;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;

namespace BUS
{
    public class DatPhongBUS
    {
        private HoaDonBUS BillBUS;
        private ChiTietLuuTruBUS chiTietLuuTruBUS;
        private KhachHangBUS khachHangBUS;
        private DatPhongDAO datPhongDAO;
        private PhongBUS phongBus;

        DataTable ListPhongTrong = null;
    

        private string ConnectionString;
        public DatPhongBUS(string connectionString)
        {
            ConnectionString = connectionString;
               datPhongDAO = new DatPhongDAO(connectionString);
        }

        public List<DatPhongDTO> GetAllDatPhong()
        {
            DataTable datPhongData = datPhongDAO.GetAllDatPhong();
            List<DatPhongDTO> listDatPhong = new List<DatPhongDTO>();

            foreach (DataRow row in datPhongData.Rows)
            {
                DatPhongDTO datPhong = new DatPhongDTO
                {
                    MaDatPhong = Convert.ToInt32(row["MaDatPhong"]),
                    MaKhachHang = Convert.ToInt32(row["MaKhachHang"]),
                    MaPhong = Convert.ToInt32(row["MaPhong"]),
                    NgayNhanPhong = Convert.ToDateTime(row["NgayNhanPhong"]),
                    NgayTraPhong = Convert.ToDateTime(row["NgayTraPhong"]),
                    TinhTrang = row["TinhTrang"].ToString()
                };

                listDatPhong.Add(datPhong);
            }

            return listDatPhong;
        }

        public void PhongkhadungdeBook(DataGridView dtgv, DateTime thoiGianBatDau, DateTime thoiGianKetThuc)
        {
          
             ListPhongTrong = datPhongDAO.PhongkhadungdeBook(thoiGianBatDau,thoiGianKetThuc);
            // Tạo một 2 cột  mới 
            ListPhongTrong.Columns.Add("LoaiPhong", typeof(string));
            ListPhongTrong.Columns.Add("Img", typeof(Image));
            ListPhongTrong.Columns.Add("GiaTheoDem", typeof(string));
            int indexRow = ListPhongTrong.Rows.Count;

            for (int i = 0; i < indexRow; i++)
            {
                DataRow firstRow = ListPhongTrong.Rows[i];
                int maloaiphong = (int)firstRow["MaLoaiPhong"];
                var ChiTietLoaiPhong = datPhongDAO.GetLoaiPhongByMaLoaiPhong(maloaiphong);
                if (ChiTietLoaiPhong.Rows.Count > 0)
                {
                    // Thêm giá trị cho 2 cột mới 
                   
                    ListPhongTrong.Rows[i]["LoaiPhong"] = ChiTietLoaiPhong.Rows[0]["TenLoaiPhong"];
                    ListPhongTrong.Rows[i]["GiaTheoDem"] = Convert.ToDouble(ChiTietLoaiPhong.Rows[0]["GiaTheoDem"]).ToString("#,##0.00");
                    ListPhongTrong.Rows[i]["Img"] = Image.FromFile(@"img\plus_25px.png");

                }
            }
            dtgv.DataSource = ListPhongTrong;
        }

        public void deleteRowAndSapXep(DataGridView dtgv, int rowIndex, bool SapXep = false)
        {
            try
            { 
                // tien hanh xoa
                dtgv.Rows.RemoveAt(rowIndex);
                if (SapXep)
                {
                    // Cập nhật lại số thứ tự cho tất cả các dòng còn lại
                    for (int i = 0; i < dtgv.Rows.Count; i++)
                    {
                        dtgv.Rows[i].Cells[0].Value = (i + 1).ToString();
                    }
                }
            }
            catch (Exception)
            {

                
            }
        }

       
        public void SumRow(DataGridView dtgv, Label lbl_Tongtien)
        {
            try
            {

                decimal kq = 0;

                // Tính tổng giá trị cột "cGiaTheoDems" cho từng dòng
                foreach (DataGridViewRow row in dtgv.Rows)
                {
                    DateTime ngayCheckin = Common.ConvertToDateTime(row.Cells["cNgayBD"].Value);
                    DateTime ngayCheckout = Common.ConvertToDateTime(row.Cells["cNgayKT"].Value);
                    decimal giaPhongCoBan = Common.ConvertToDecimal(row.Cells["cGiaTheoDems"].Value);

                    kq += TinhGiaPhong(ngayCheckin, ngayCheckout, giaPhongCoBan);
                }

                // Cập nhật giá trị của lbl_Tongtien
                lbl_Tongtien.Text = kq.ToString("#,##0");
            }
            catch (Exception)
            {
                // Xử lý ngoại lệ nếu cần
            }
        }



        public void add_RoomBySelected(DataGridView dtgv,string id , DateTime thoiGianBatDau, DateTime thoiGianKetThuc)
        {
            // datagridview 

            DataRow[] rows = ListPhongTrong.Select($"MaPhong = {id}");
            if (rows.Length > 0)
            {
                int rowIndex = dtgv.Rows.Add(); // Thêm một dòng mới vào DataGridView và lấy chỉ số hàng
                DatagridviewHelper.SetStatusDataGridView(dtgv, rowIndex, "cSTT", rowIndex+ 1);
                DatagridviewHelper.SetStatusDataGridView(dtgv, rowIndex, "cMaPhongs", rows[0]["MaPhong"]);
                DatagridviewHelper.SetStatusDataGridView(dtgv, rowIndex, "cMaLoaiPhongs", rows[0]["MaLoaiPhong"]);
                DatagridviewHelper.SetStatusDataGridView(dtgv, rowIndex, "cTinhTrangs", rows[0]["TinhTrang"]);
                DatagridviewHelper.SetStatusDataGridView(dtgv, rowIndex, "cLoaiPhongs", rows[0]["LoaiPhong"]);
                DatagridviewHelper.SetStatusDataGridView(dtgv, rowIndex, "cSoPhongs", rows[0]["SoPhong"]);
                DatagridviewHelper.SetStatusDataGridView(dtgv, rowIndex, "cSoNguoi", "1" );
                DatagridviewHelper.SetStatusDataGridView(dtgv, rowIndex, "cNgayBD", thoiGianBatDau);
                DatagridviewHelper.SetStatusDataGridView(dtgv, rowIndex, "cNgayKT", thoiGianKetThuc);
                DatagridviewHelper.SetStatusDataGridView(dtgv, rowIndex, "cGiaTheoDems", rows[0]["GiaTheoDem"]);
                DatagridviewHelper.SetStatusDataGridView(dtgv, rowIndex, "cImgDelete", Image.FromFile(@"img\cancel_25px.png"));
            }
        }

        public void add_SelectedByRoom(DataGridView dtgv, DataGridView dtgv2, int rowIndex)
        {
            // get lay du lieu
            string maphong =   DatagridviewHelper.GetStatusDataGridView(dtgv2, rowIndex, "cMaPhongs");
            string maloaiphong = DatagridviewHelper.GetStatusDataGridView(dtgv2, rowIndex, "cMaLoaiPhongs");
            string tinhtrang = DatagridviewHelper.GetStatusDataGridView(dtgv2, rowIndex, "cTinhTrangs");
            string loaiphong = DatagridviewHelper.GetStatusDataGridView(dtgv2, rowIndex, "cLoaiPhongs");
            string sophong = DatagridviewHelper.GetStatusDataGridView(dtgv2, rowIndex, "cSoPhongs");
            string giaPhong = DatagridviewHelper.GetStatusDataGridView(dtgv2, rowIndex, "cGiaTheoDems");

            // tạo 1 hàng mới trong Datatable

            DataRow newRow = ListPhongTrong.NewRow();
            newRow["MaPhong"] = maphong; // Đặt giá trị cho cột "ColumnName1"
            newRow["SoPhong"] = sophong; // Đặt giá trị cho cột "ColumnName2"
            newRow["MaLoaiPhong"] = maloaiphong; // Đặt giá trị cho cột "ColumnName1"
            newRow["TinhTrang"] = tinhtrang; // Đặt giá trị cho cột "ColumnName2"
            newRow["LoaiPhong"] = loaiphong; // Đặt giá trị cho cột "ColumnName1"
            newRow["GiaTheoDem"] = giaPhong; // Đặt giá trị cho cột "ColumnName2"
            newRow["Img"] = Image.FromFile(@"img\plus_25px.png"); // Đặt giá trị cho cột "ColumnName2"
            
            ListPhongTrong.Rows.Add(newRow);

            dtgv.DataSource = ListPhongTrong;
        }


        public bool InsertDatPhong(DataGridView dtgv, string nameKH , string emailKH, string sdtKH, string diachiKH)
        {

            bool isBook = false;
            khachHangBUS = new KhachHangBUS(ConnectionString);
            chiTietLuuTruBUS = new ChiTietLuuTruBUS(ConnectionString);
            BillBUS = new HoaDonBUS(ConnectionString);
            phongBus = new PhongBUS(ConnectionString);
            DateTime realtime = DateTime.Now;

            foreach (DataGridViewRow row in dtgv.Rows)
            {
                if (!row.IsNewRow) // Bỏ qua hàng mới, nếu có
                {


                  int maphong =  Convert.ToInt32(DatagridviewHelper.GetStatusDataGridView(dtgv, row.Index, "cMaPhongs"));
                    string maloaiphong = DatagridviewHelper.GetStatusDataGridView(dtgv, row.Index, "cMaLoaiPhongs");
                    string loaiphong = DatagridviewHelper.GetStatusDataGridView(dtgv, row.Index, "cLoaiPhongs");
                    string sophong = DatagridviewHelper.GetStatusDataGridView(dtgv, row.Index, "cSoPhongs");
                    int slnguoi = Convert.ToInt32(DatagridviewHelper.GetStatusDataGridView(dtgv, row.Index, "cSoNguoi"));
                    DateTime DayStart = Convert.ToDateTime(DatagridviewHelper.GetStatusDataGridView(dtgv, row.Index, "cNgayBD"));
                    DateTime DayStop = Convert.ToDateTime(DatagridviewHelper.GetStatusDataGridView(dtgv, row.Index, "cNgayKT"));
                    DateTime? thoiGianThucTeTraPhong = null; // Đặt thời gian thực tế trả phòng là null ban đầu
                    decimal giaTien = Convert.ToDecimal(DatagridviewHelper.GetStatusDataGridView(dtgv, row.Index, "cGiaTheoDems"));

                    string tinhtrang = "";
                   
                    if (DayStart < realtime)
                    {
                        DayStart = realtime;
                        tinhtrang = "Đang sử dụng";

                    }
                    else if (DayStart > realtime)
                    {
                        tinhtrang = "Đặt Phòng";
                    }

                    if (DayStop <= realtime)
                    {
                        MessageBoxHelper.ShowMessageBox("Đặt phòng thất bại , TimeStop phải lớn hơn TimeStart"); return true;
                    }







                    // thêm khách hàng :D thuê phòng
                    int madatphong = -1;
                    int idKhachHang = khachHangBUS.ThemKhachHang(nameKH, emailKH, sdtKH, diachiKH);
                    if (idKhachHang != -1)
                    {
                        DatPhongDTO datPhong = new DatPhongDTO
                        {
                            MaKhachHang = idKhachHang,
                            MaPhong = maphong,
                            NgayNhanPhong = DayStart,
                            NgayTraPhong = DayStop,
                            TinhTrang = tinhtrang
                        };
                        int  idMaDatPhong =  datPhongDAO.InsertDatPhong(datPhong); // vua insert vua lay ma dat
                        if (idMaDatPhong < 0)
                        {
                            MessageBoxHelper.ShowMessageBox("Đặt phòng thất bại , InsertDatPhong Error"); return true;
                        }
                        else
                        {
                            madatphong = idMaDatPhong;
                        }

                    }
                    else
                    {
                        return false;
                    }



                    // Tạo đối tượng ChiTietLuuTruDTO từ dữ liệu
                    ChiTietLuuTruDTO chiTietLuuTru = new ChiTietLuuTruDTO()
                    {
                        MaDatPhong = madatphong,
                        SoLuongNguoi = slnguoi,
                        ThoiGianVaoPhong = DayStart,
                        ThoiGianDuKienTraPhong = DayStop,
                        ThoiGianThucTeTraPhong = thoiGianThucTeTraPhong,
                        TinhTrang = tinhtrang
                    };
                    // Gọi phương thức để thêm chi tiết lưu trú mới
                    int maLuuTru = chiTietLuuTruBUS.ThemChiTietLuuTru(chiTietLuuTru); // cái này phải rêtun ra Int 
                    if (maLuuTru < 0)
                    {
                        MessageBox.Show("Thêm chi tiết lưu trú thất bại. Vui lòng kiểm tra lại thông tin."); return false;
                        // Thực hiện bất kỳ xử lý nào bạn cần sau khi thêm thành công
                    }



                    // tạo hóa đơn :D 
                    HoaDonDTO thongtinBill = new HoaDonDTO()
                    {
                        MaDatPhong = madatphong,
                        MaKhachHang = slnguoi,
                        MaNhanVien = 1,
                        NgayLapHoaDon = realtime,
                        TongTienPhong = TinhGiaPhong(DayStart, DayStop, giaTien),
                        TongTienDichVu = 0,
                        TongTien = 0,
                        TinhTrang = "Chưa thanh toán", 
                        DaThanhToan = false,
                    };
                    // Sau đó, thêm hóa đơn bằng cách gọi phương thức của HoaDonBUS
                   int mahoadon =  BillBUS.ThemHoaDon(thongtinBill);



                    // thay đổi phòng trống thành phòng đã đặt hoặc đã sử dụng
                    
                    if (phongBus.UpdateStatusPhong(maphong, tinhtrang))
                    {
                        isBook = true;
                        MessageBox.Show($"Id Bill :{mahoadon}\nTên Khách Hàng :{nameKH}\nLoại Phòng :{loaiphong}\nSố Phòng :{sophong}\nGiá theo đêm :{giaTien}\nTổng giá ước tính :{thongtinBill.TongTienPhong.ToString("#,##0")}","Thông Báo Book Phòng Thành Công");
                    } 
         }
            }
           return isBook;
        }


        public bool UpdateDatPhong(DatPhongDTO datPhong)
        {
            return datPhongDAO.UpdateDatPhong(datPhong);
        }

        public bool DeleteDatPhong(int maDatPhong)
        {
            return datPhongDAO.DeleteDatPhong(maDatPhong);
        }





        // Các phương thức khác cho việc cập nhật và xóa dữ liệu

        public decimal TinhGiaPhong(DateTime ngayCheckin, DateTime ngayCheckout, decimal giaPhongCoBan = 1000000m)
        {
            decimal giaPhong = 0; // Giá phòng cơ bản là 1 triệu

            TimeSpan thoiGianLuuTru = ngayCheckout - ngayCheckin;
            int soNgay = (int)Math.Ceiling(thoiGianLuuTru.TotalDays); // Số ngày làm tròn lên

            if (soNgay <= 0)
            {

            }
            else
            {
                giaPhong += giaPhongCoBan * soNgay;
            }
            // Tính phụ thu dựa trên thời gian check-in
            if (ngayCheckin.TimeOfDay >= new TimeSpan(5, 0, 0) && ngayCheckin.TimeOfDay < new TimeSpan(9, 0, 0))
            {
                giaPhong += giaPhongCoBan * 0.5m; // Check in từ 5h - 9h: Phụ thu 50%
            }
            else if (ngayCheckin.TimeOfDay >= new TimeSpan(9, 0, 0) && ngayCheckin.TimeOfDay < new TimeSpan(14, 0, 0))
            {
                giaPhong += giaPhongCoBan * 0.3m; // Check in từ 9h - 14h: Phụ thu 30%
            }


            // Tính phụ thu dựa trên thời gian check-out
            if (ngayCheckout.TimeOfDay >= new TimeSpan(12, 0, 0) && ngayCheckout.TimeOfDay < new TimeSpan(15, 0, 0))
            {
                giaPhong += giaPhongCoBan * 0.3m; // Trả phòng từ 12h - 15h: Phụ thu 30%
            }
            else if (ngayCheckout.TimeOfDay >= new TimeSpan(15, 0, 0) && ngayCheckout.TimeOfDay < new TimeSpan(18, 0, 0))
            {
                giaPhong += giaPhongCoBan * 0.5m; // Trả phòng từ 15h - 18h: Phụ thu 50%
            }
            else if (ngayCheckout.TimeOfDay >= new TimeSpan(18, 0, 0))
            {
                giaPhong += giaPhongCoBan; // Trả phòng sau 18h: Phụ thu 100%
            }

            return giaPhong;
        }


        public bool UpdateDatPhongTinhTrangByMaHoaDon(int maHoaDon, string tinhTrang)
        {
            return datPhongDAO.UpdateDatPhongTinhTrangByMaHoaDon(maHoaDon, tinhTrang);
        }



    }





}
