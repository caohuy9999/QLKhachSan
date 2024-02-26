using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BUS.ClassHelper;
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
                    GiaTheoDem = Convert.ToDecimal(row["GiaTheoDem"]),
                    SucChua = Convert.ToInt32(row["SucChua"]),
                    MieuTa = row["MieuTa"].ToString()
                };

                listLoaiPhong.Add(loaiPhong);
            }

            return listLoaiPhong;
        }
        public List<LoaiPhongDTO> GetAllLoaiPhongByTypeRoom(int typeRoom)
        {
            DataTable loaiPhongData = loaiPhongDAO.GetAllLoaiPhongByTypeRoom(typeRoom);
            List<LoaiPhongDTO> listLoaiPhong = new List<LoaiPhongDTO>();

            foreach (DataRow row in loaiPhongData.Rows)
            {
                LoaiPhongDTO loaiPhong = new LoaiPhongDTO
                {
                    MaLoaiPhong = Convert.ToInt32(row["MaLoaiPhong"]),
                    TenLoaiPhong = row["TenLoaiPhong"].ToString(),
                    GiaTheoDem = Convert.ToDecimal(row["GiaTheoDem"]),
                    SucChua = Convert.ToInt32(row["SucChua"]),
                    MieuTa = row["MieuTa"].ToString()
                };

                listLoaiPhong.Add(loaiPhong);
            }

            return listLoaiPhong;
        }

        public void GetLoaiPhong(ComboBox cbbTypeRoom)
        {
            DataTable loaiPhongData = loaiPhongDAO.GetAllLoaiPhong();
  
            DataRow newRow = loaiPhongData.NewRow();
            newRow["TenLoaiPhong"] = "All"; // Đặt giá trị cho cột "ColumnName1"
            newRow["MaLoaiPhong"] = 0; // Đặt giá trị cho cột "ColumnName2"
          
            loaiPhongData.Rows.InsertAt(newRow, 0);// Chèn hàng mới vào đầu DataTable ở vị trí 0

            cbbTypeRoom.DisplayMember = "TenLoaiPhong"; // Hiển thị Tên Loại phòng trong ComboBox
            cbbTypeRoom.ValueMember = "MaLoaiPhong"; // Giá trị thực sự (ID) của Loại phòng

            cbbTypeRoom.DataSource = loaiPhongData;
        }


        public void GetAllLoaiPhongForDataGridView(DataGridView dtgvTypeRoom,int typeRoom = 0)
        {
            DataTable loaiPhongData = new DataTable();
            if (typeRoom == 0)
            {
                loaiPhongData = loaiPhongDAO.GetAllLoaiPhong();
            }
            else
            {
                loaiPhongData = loaiPhongDAO.GetAllLoaiPhongByTypeRoom(typeRoom);
            }

            if (loaiPhongData.Rows.Count > 0)
            {
                loaiPhongData.Columns.Add("STT", typeof(int));
                loaiPhongData.Columns.Add("Delete", typeof(Image));
                loaiPhongData.Columns.Add("Edit", typeof(Image));
                int stt = 0;
                foreach (DataRow row in loaiPhongData.Rows)
                {
                    row["Delete"] = Image.FromFile(@"img\cancel_25px.png");
                    row["Edit"] = Image.FromFile(@"img\edit_25px.png");
                    stt++;
                    row["STT"] = stt;
                }
            }
          
            dtgvTypeRoom.DataSource = loaiPhongData;

        }



        public void InsertLoaiPhong(LoaiPhongDTO loaiPhong)
        {
            loaiPhongDAO.InsertLoaiPhong(loaiPhong.TenLoaiPhong,  loaiPhong.GiaTheoDem, loaiPhong.SucChua, loaiPhong.MieuTa);
        }

        public LoaiPhongDTO GetLoaiPhongByMaLoaiPhong(int maLoaiPhong)
        {
           var result = loaiPhongDAO.GetLoaiPhongByMaLoaiPhong(maLoaiPhong);
            if (result.Rows.Count > 0)
            {
                DataRow row = result.Rows[0];
                LoaiPhongDTO loaiPhong = new LoaiPhongDTO
                {
                    MaLoaiPhong = Convert.ToInt32(row["MaLoaiPhong"]),
                    TenLoaiPhong = row["TenLoaiPhong"].ToString(),
                    GiaTheoDem = Convert.ToDecimal(row["GiaTheoDem"]),
                  
                    SucChua = Convert.ToInt32(row["SucChua"]),
                    MieuTa = row["MieuTa"].ToString()
                };

                return loaiPhong;
            }

            return null; // Trả về null nếu không tìm thấy dữ liệu
        }



        public void GetRoomForDataGridView(DataGridView dtgvRoom,int maloaiphong,string typeSTT = "" , string KeywordSearch = "")
        {
            DataTable loaiPhongData = loaiPhongDAO.GetPhongByMaLoaiPhong(maloaiphong, typeSTT, KeywordSearch);

            // Xóa toàn bộ dữ liệu hiện tại trong DataGridView
            dtgvRoom.Rows.Clear();


            if (loaiPhongData.Rows.Count > 0)
            {
                int stt = 0;

                foreach (DataRow row in loaiPhongData.Rows)
                {
                    Image infoRoom = Image.FromFile(@"img\info_25px.png");
                 //   Image editRoom = Image.FromFile(@"img\edit_25px.png");
                    Image deleteRoom = Image.FromFile(@"img\cancel_25px.png");

                    stt++;
                    dtgvRoom.Rows.Add(
                        stt,
                        row["MaPhong"],
                        row["MaLoaiPhong"],
                        row["SoPhong"],
                        row["TinhTrang"],
                         infoRoom,
                        deleteRoom
                    );
                }

              
            }

        }


        public bool UpdateLoaiPhong(LoaiPhongDTO loaiPhong)
        {
            return loaiPhongDAO.UpdateLoaiPhong(loaiPhong);
        }

        public bool DeleteLoaiPhong(int maLoaiPhong)
        {
            return loaiPhongDAO.DeleteLoaiPhong(maLoaiPhong);
        }



        public bool EditPhong(string tenloaiphong, string giatheodem,string succhua, string mieuta, string maloaiphong)
        {
            try
            {
                // check
                if (string.IsNullOrEmpty(tenloaiphong) || string.IsNullOrEmpty(giatheodem) || string.IsNullOrEmpty(mieuta) || string.IsNullOrEmpty(maloaiphong) || string.IsNullOrEmpty(succhua))
                {
                    MessageBoxHelper.ShowMessageBox("Vui lòng nhập đầy đủ thông tin or Nhập sai định dạng!");
                    return false;
                }

                // Chuyển đổi giá trị
                decimal giaTheoDem;
                if (!decimal.TryParse(giatheodem, out giaTheoDem))
                {
                    MessageBoxHelper.ShowMessageBox("Giá theo đêm không hợp lệ!");
                    return false;
                }


                LoaiPhongDTO loaiPhong = new LoaiPhongDTO
                {
                    TenLoaiPhong = tenloaiphong,
                    GiaTheoDem = giaTheoDem,
                    SucChua = Common.ConvertToInt32(succhua),
                    MieuTa = mieuta,
                    MaLoaiPhong = Common.ConvertToInt32(maloaiphong)
                };

                
             
                return loaiPhongDAO.UpdateLoaiPhong(loaiPhong);
            }
            catch (Exception ex)
            {
                MessageBoxHelper.ShowMessageBox("Lỗi Update loaiPhong: " + ex.Message);
                return false;
            }
        }

        public bool AddPhong(string tenloaiphong, string giatheodem, string succhua, string mieuta)
        {
            try
            {
              
                if (string.IsNullOrEmpty(tenloaiphong) || string.IsNullOrEmpty(giatheodem) || string.IsNullOrEmpty(succhua) || string.IsNullOrEmpty(mieuta))
                {
                    MessageBoxHelper.ShowMessageBox("Vui lòng nhập đầy đủ thông tin!");
                    return false;
                }

             
                decimal giaTheoDem;
                int sucChua;
                if (!decimal.TryParse(giatheodem, out giaTheoDem) || !int.TryParse(succhua, out sucChua))
                {
                    MessageBoxHelper.ShowMessageBox("Dữ liệu không hợp lệ!");
                    return false;
                }

                // Tạo DTO để lưu trữ thông tin thêm mới
                LoaiPhongDTO loaiPhong = new LoaiPhongDTO
                {
                    TenLoaiPhong = tenloaiphong,
                    GiaTheoDem = giaTheoDem,
                    SucChua = sucChua,
                    MieuTa = mieuta
                };


                return  loaiPhongDAO.InsertLoaiPhong(loaiPhong) > 0;
            }
            catch (Exception ex)
            {
                // Xử lý ngoại lệ nếu có
                MessageBoxHelper.ShowMessageBox("Lỗi: " + ex.Message);
                return false;
            }
        }


    }
}
