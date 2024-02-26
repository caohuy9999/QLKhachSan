using Bunifu.Framework.UI;
using DAO;
using DTO;
using GUI;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace BUS
{
    public class PhongBUS
    {
        private PhongDAO phongDAO;

        private LoaiPhongBUS loaiphongBUS;
        private string ConnectionString;

        public PhongBUS(string connectionString)
        {
            ConnectionString = connectionString;
            phongDAO = new PhongDAO(ConnectionString);
        }

        public List<PhongDTO> GetAllPhong(string keyword = "", int cbbTypeRoom = 0, string cbbStatusRoom = "")
        {
            DataTable phongData = phongDAO.GetAllPhong(keyword, cbbTypeRoom, cbbStatusRoom);
            List<PhongDTO> listPhong = new List<PhongDTO>();

            foreach (DataRow row in phongData.Rows)
            {
                PhongDTO phong = new PhongDTO();
                phong.MaPhong = Convert.ToInt32(row["MaPhong"]);
                phong.SoPhong = Convert.ToInt32(row["SoPhong"]);
                phong.MaLoaiPhong = Convert.ToInt32(row["MaLoaiPhong"]);
                phong.TinhTrang = row["TinhTrang"].ToString();
                phong.Roomcolor = colorRoom(row["TinhTrang"].ToString()); // gắn màu cho phòng theo tình trạng :D
                phong.IdBill = row["MaHoaDon"].ToString() == "" ? "" : $"ID Bill :{row["MaHoaDon"]}";
                phong.TenLoaiPhong = row["TenLoaiPhong"].ToString();
                phong.GiaTheoDem = row["GiaTheoDem"].ToString();

                listPhong.Add(phong);
            }
            return listPhong;
        }

        public List<PhongDTO> LayDanhSachPhongTrong()
        {
            DataTable ListPhongTrong = phongDAO.LayDanhSachPhongTrong();
            List<PhongDTO> danhSachPhongTrong = new List<PhongDTO>();
            foreach (DataRow row in ListPhongTrong.Rows)
            {
                PhongDTO phong = new PhongDTO
                {
                    MaPhong = Convert.ToInt32(row["MaPhong"]),
                    SoPhong = Convert.ToInt32(row["SoPhong"]),
                    MaLoaiPhong = Convert.ToInt32(row["MaLoaiPhong"]),
                    TinhTrang = row["TinhTrang"].ToString(),
                    // Các thuộc tính khác
                };

                danhSachPhongTrong.Add(phong);
            }
            return danhSachPhongTrong;
        }



        public void PhongkhadungdeBook(DataGridView dtgv)
        {
            loaiphongBUS = new LoaiPhongBUS(ConnectionString);


            DataTable ListPhongTrong = phongDAO.LayDanhSachPhongTrong();
            System.Drawing.Image image = System.Drawing.Image.FromFile(@"img\plus_25px.png");
            dtgv.DataSource = ListPhongTrong;

            for (int i = 0; i < ListPhongTrong.Rows.Count; i++)
            {
              DataRow firstRow = ListPhongTrong.Rows[i];
                var ChiTietLoaiPhong = loaiphongBUS.GetLoaiPhongByMaLoaiPhong((int)firstRow["MaLoaiPhong"]);
                if (ChiTietLoaiPhong != null)
                {
                    DatagridviewHelper.SetStatusDataGridView(dtgv, i, "cLoaiPhong", ChiTietLoaiPhong.TenLoaiPhong);
                }
                //DatagridviewHelper.SetStatusDataGridView(dtgv, i, "cMaPhong", firstRow["MaPhong"]);
                //DatagridviewHelper.SetStatusDataGridView(dtgv, i, "cSoPhong", firstRow["SoPhong"]);
                //DatagridviewHelper.SetStatusDataGridView(dtgv, i, "cMaLoaiPhong", firstRow["MaLoaiPhong"]);
                DatagridviewHelper.SetStatusDataGridView(dtgv, i, "cIMG", image);
             

            }
            dtgv.DataSource = ListPhongTrong;
        }

    


        public bool InsertPhong(int maloaiphong)
        {
           
            PhongDTO phong = new PhongDTO
            {
                MaLoaiPhong = maloaiphong,
              
                TinhTrang = "Trống"
            };
           return phongDAO.InsertPhong(phong);
        }



        public bool UpdateStatusPhong(int maPhong, string tinhTrangMoi)
        {
            return phongDAO.UpdateStatusPhong(maPhong, tinhTrangMoi);
        }


        public void GetInfoPhongByID(int maPhong)
        {
            DataTable Infophong = phongDAO.GetPhongByID(maPhong);

            List<PhongDTO> danhSachPhongTrong = new List<PhongDTO>();
            foreach (DataRow row in Infophong.Rows)
            {
                PhongDTO phong = new PhongDTO
                {
                    MaPhong = Convert.ToInt32(row["MaPhong"]),
                    SoPhong = Convert.ToInt32(row["SoPhong"]),
                    MaLoaiPhong = Convert.ToInt32(row["MaLoaiPhong"]),
                    TinhTrang = row["TinhTrang"].ToString(),
                    // Các thuộc tính khác
                };

                danhSachPhongTrong.Add(phong);
            }
        }

        public void GetInfoPhongByID(int maPhong,Label lbTileName, ComboBox cbb, Label lblPhongTrong, BunifuThinButton2 btnBook, BunifuThinButton2 btnAddDichVu)
        {
            DataTable Infophong = phongDAO.GetPhongByID(maPhong);
            foreach (DataRow row in Infophong.Rows)
            {
                lbTileName.Text = $"Phòng : {row["SoPhong"].ToString()}";
                cbb.SelectedItem = $"Phòng : {row["TinhTrang"]}";

                string aaa = row["TinhTrang"].ToString();
                switch (row["TinhTrang"])
                {
                    case "Đang don dẹp":
                        btnAddDichVu.Visible = false;
                        lblPhongTrong.Visible = true;
                        btnBook.ButtonText = "Cập Nhật";
                        break;
                    case "Đang sử dụng":
                        lblPhongTrong.Visible = false;
                        btnAddDichVu.Visible = true;
                        btnBook.ButtonText = "Thanh Toán";
                        break;
                    case "Đã đặt":
                        lblPhongTrong.Visible = false;
                        btnAddDichVu.Visible = false;
                        btnBook.ButtonText = "Nhận Phòng";
                        break;
                    case "Trống":
                        btnAddDichVu.Visible = false;
                        lblPhongTrong.Visible = true;
                        btnBook.ButtonText = "Book Phòng";
                        break;

                }
                
            }
        }



        #region cách lấy dữ liệu cột trong DataTable rồi return ra 
        //public DataTable FilterPhongByTinhTrang(string tinhTrang)
        //{
        //    DataTable phongData = phongDAO.GetAllPhong();

        //    DataView dataView = new DataView(phongData);
        //    dataView.RowFilter = "TinhTrang = '" + tinhTrang + "'";

        //    return dataView.ToTable();
        //}
        #endregion


        public void FilterPhongByTinhTrangCBB(ComboBox cbbTinhTrang,int maphong = 0)
        {
            // Lấy DataTable chứa tất cả thông tin của phòng
            DataTable allPhongData = phongDAO.GetStatus_Room();
            cbbTinhTrang.Items.Clear();
            if (maphong == -1)
            {
                DataRow newRow = allPhongData.NewRow();
                newRow["TinhTrang"] = "All"; // Đặt giá trị cho cột "ColumnName1"
                allPhongData.Rows.InsertAt(newRow, 0);// Chèn hàng mới vào đầu DataTable ở vị trí 0
            }



            foreach (DataRow row in allPhongData.Rows)
            {
                cbbTinhTrang.Items.Add(row["TinhTrang"].ToString());
            }


            if (maphong > 0)
            {
                DataTable selectItem = phongDAO.GetStatus_Room(maphong);
                cbbTinhTrang.SelectedItem = selectItem.Rows[0]["TinhTrang"];
            }
            else if (maphong == -1)
            {
                cbbTinhTrang.SelectedItem = "All";
            }


            // Gán danh sách tình trạng không trùng nhau làm nguồn dữ liệu cho ComboBox
          //  cbbTinhTrang.DataSource = uniqueTinhTrangList;
        }



        public Color colorRoom(string tinhtrangphong)
        {
            Color color = new Color();
            switch (tinhtrangphong)
            {
                case "Đang sử dụng":
                    color = Color.FromArgb(192, 255, 192); // mau lam
                    break;

                case "Đã đặt":
                    color = Color.FromArgb(255, 240, 245); // mau xanh
                    break;
                case "Trống":
                    color = Color.FromArgb(224, 224, 224); // Xám 
                    break;
                case "Đang don dẹp":
                    color = Color.FromArgb(230, 230, 250); // tím 
                    break;
                default:
                    color = Color.FromArgb(192, 255, 192); // mau trang mac dinh
                    break;
            }

            return color;
        }

        
        public void GetAllSoPhong_NameCBB(ComboBox CBB,bool isAll = true)
        {
            DataTable allPhongData = phongDAO.GetAllSoPhong_AS_SoPhong_NVARCHAR();
            // Kiểm tra xem cột "SoPhongMoi" đã tồn tại chưa
            if (!allPhongData.Columns.Contains("SoPhongMoi"))
            {
                allPhongData.Columns.Add("SoPhongMoi", typeof(string));
                foreach (DataRow row in allPhongData.Rows)
                {
                    row["SoPhongMoi"] = "Số Phòng " + row["SoPhong"];
                }
            }

            if (isAll)
            {
                DataRow newRow = allPhongData.NewRow();
                newRow["MaPhong"] = 0;
                newRow["SoPhongMoi"] = "All";
                allPhongData.Rows.InsertAt(newRow, 0);
            }
          
            CBB.DataSource = allPhongData;
            CBB.DisplayMember = "SoPhongMoi";
            CBB.ValueMember = "MaPhong";

        }




    }
}
