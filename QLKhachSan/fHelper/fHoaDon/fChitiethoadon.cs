using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BUS;
using BUS.ClassHelper;

namespace QLKhachSan.fHelper.fHoaDon
{
    public partial class fChitiethoadon : Form
    {
        ChiTietHoaDonBUS ChiTietHoaDonBUS;
        private int Mahoadon;
        public fChitiethoadon(int mahoadon)
        {
            Mahoadon = mahoadon;
            InitializeComponent();
            ChiTietHoaDonBUS = new ChiTietHoaDonBUS(fLogin.KeystringSQL);
            this.BackColor = Color.DodgerBlue;
            this.Padding = new Padding(1, 1, 1, 1);
        }

        private void fChitiethoadon_Load(object sender, EventArgs e)
        {
            ChiTietHoaDonBUS.DisplayChiTietHoaDonOnDataGridView(Mahoadon, dtgv, lbTileName,true);
        }

        private void btnInBaoCao_Click(object sender, EventArgs e)
        {
            DataTable data = ChiTietHoaDonBUS.CustomCloneDataGridView(dtgv);
            string tile = $"In Báo Chi Tiết Hóa Đơn: {Mahoadon}" + DateTime.Now.ToString();
            ChiTietHoaDonBUS.ExportToExcel(data, tile, FileDialogHelper.SelectFolder("Chọn đường dẫn Lưu File Báo Cáo", Common.PathExE()));
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
