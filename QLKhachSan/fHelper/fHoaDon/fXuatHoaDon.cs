using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Xml.Linq;
using BUS;
using BUS.ClassHelper;
using DTO;
using GUI;
using iTextSharp.text;
using iTextSharp.text.pdf;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;


namespace QLKhachSan
{
    public partial class fXuatHoaDon : Form
    {
        public static bool isInHoaDonThanhCong = false;
        private ChiTietHoaDonBUS chitiethoadonBUS;
        private ChiTietLuuTruBUS chitietluutruBUS;
        private HoaDonBUS hoadonBUS;
        DateTime TimeHienTai;
        public fXuatHoaDon(int IDhoadon, int maphong,DateTime timehientai)
        {
            InitializeComponent();

            chitiethoadonBUS = new ChiTietHoaDonBUS(fLogin.KeystringSQL);
            chitietluutruBUS = new ChiTietLuuTruBUS(fLogin.KeystringSQL);
            hoadonBUS = new HoaDonBUS(fLogin.KeystringSQL);
            //// Thêm dữ liệu mẫu cho DataGridView
            //for (int i = 0; i < 11; i++) 
            //{
            //    dtgv.Rows.Add(i, "a", "b", "c", "d", "e");
            //}
            TimeHienTai = timehientai;
            dtgv.ClearSelection(); // Bỏ chọn tất cả các ô
           
            DatagridviewHelper.DataCenter(dtgv); DatagridviewHelper.TitleCenter(dtgv);
            hoadonBUS.XuatHoaDon(dtgv,IDhoadon,maphong, TimeHienTai, lblNameKH, lblSoDem, lblSoNguoi, lblNgayLapHoaDon, lblTongTien, lblSoHoaDon, lblNameNV, lblSoPhong);

        }

        private void PrintButton_Click(object sender, EventArgs e)
        {

            this.Invoke((MethodInvoker)delegate {
                dtgv.ClearSelection(); // Bỏ chọn tất cả các ô
                PrintButton.Visible = false;  // Ẩn nút in để tránh in chính nó
            });



            // Tạo một hình ảnh Bitmap để vẽ nội dung của Form
            Bitmap bmp = new Bitmap(this.Width, this.Height);
            this.DrawToBitmap(bmp, new System.Drawing.Rectangle(0, 0, this.Width, this.Height));

            // Đặt độ phân giải DPI cho Bitmap (vd: 300 DPI)
            //  bmp.SetResolution(200, 200);


            // In Bitmap ra giấy
            PrintDocument printDocument = new PrintDocument();
            printDocument.PrintPage += new PrintPageEventHandler((s, ev) => {
                ev.Graphics.DrawImage(bmp, new Point(0, 0));
            });

            PrintDialog printDialog = new PrintDialog();
            printDialog.Document = printDocument;

            if (printDialog.ShowDialog() == DialogResult.OK)
            {
                printDocument.Print();
            }

            // Hiển thị nút in lại sau khi in xong
            PrintButton.Visible = true;


            dtgv.ClearSelection(); // Bỏ chọn tất cả các ô
            
                if (MessageBoxHelper.Show("Đã in xong thoát của sổ") == DialogResult.Yes)
            {
                isInHoaDonThanhCong = true;
                this.Close();   
            }
            //PrintDocument printDocument = new PrintDocument();
            //printDocument.PrintPage += new PrintPageEventHandler(printDocument_PrintPage);

            //PrintDialog printDialog = new PrintDialog();
            //printDialog.Document = printDocument;

            //if (printDialog.ShowDialog() == DialogResult.OK)
            //{
            //    printDocument.Print();
            //}

        }
       
       

        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {
           
        }

        private void fXuatHoaDon_Load(object sender, EventArgs e)
        {
            this.BackColor = Color.DodgerBlue;
            this.Padding = new Padding(1, 1, 1, 1);
        }

        private void printDocument_PrintPage(object sender, PrintPageEventArgs e)
        {
            // Tạo một hình ảnh Bitmap để vẽ nội dung của DataGridView
            Bitmap bmp = new Bitmap(dtgv.Width, dtgv.Height);
            dtgv.DrawToBitmap(bmp, new System.Drawing.Rectangle(0, 0, dtgv.Width, dtgv.Height));

            // Đặt độ phân giải DPI cho Bitmap (vd: 300 DPI)
            bmp.SetResolution(300, 300);

            // In Bitmap ra giấy
            e.Graphics.DrawImage(bmp, new Point(0, 0));
        }
    }
}
