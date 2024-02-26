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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace QLKhachSan
{
    public partial class fLogin : Form
    {
        public event EventHandler LoginSuccess;

        public static string HoTenNV;
        public static int MaNhanVien;
        public static string TenQuyen;
        public static string KeystringSQL = "Data Source=DESKTOP-Q285043;Initial Catalog=SQLQLKhachSan02;Integrated Security=True";
        private NhanVienBUS nhanvienBUS;
        private NhanVien_QuyenBUS nhanvienquyen;
        private QuyenBUS quyen;
        public fLogin()
        {
            InitializeComponent();
            nhanvienBUS = new NhanVienBUS(KeystringSQL);
            nhanvienquyen = new NhanVien_QuyenBUS(KeystringSQL);
            quyen = new QuyenBUS(KeystringSQL);
            ckbHidenPassWord_OnChange(null,null);
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (txbUser.Text.Trim() == "" ||txbPassword.Text.Trim() == "")
            {
                MessageBoxHelper.ShowMessageBox("Vui lòng nhập đủ dữ liệu!"); // Đăng nhập thất bại
                return;
            }
            if (!IsLoginInformationValid())
            {
                MessageBoxHelper.ShowMessageBox("Tài khoản hoặc mật khẩu không đúng!"); // Đăng nhập thất bại
            }
        }


        bool IsLoginInformationValid()
        {
            DataTable result = nhanvienBUS.Login(txbUser.Text.Trim(), txbPassword.Text.Trim());
            if (result.Rows.Count > 0)
            {
                
                MaNhanVien = (int)result.Rows[0]["MaNhanVien"];
                HoTenNV = result.Rows[0]["HoTen"].ToString();
                TenQuyen = quyen.GetTenQuyenByMaQuyen(nhanvienquyen.GetQuyenByMaNhanVien(MaNhanVien));

                MessageBoxHelper.ShowMessageBox($"Đăng nhập thành công! \nXin chào {TenQuyen} : {HoTenNV} : {MaNhanVien}");
                fMain.optionScriptString = "Flogin";
                LoginSuccess?.Invoke(this, EventArgs.Empty);
                //this.Hide();
                //fMain f = new fMain();
                //f.ShowInTaskbar = true;
                //f.ShowDialog();
                return true;
            }
            return false;
        }



        private void btnEnd_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void fLogin_Load(object sender, EventArgs e)
        {
            txbUser.Focus();
        }

     


        private void ckbHidenPassWord_OnChange(object sender, EventArgs e)
        {
        //    txbPassword.UseSystemPasswordChar = !ckbHidenPassWord.Checked;
        }

      
        private void txbUser_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Tab)
            {
                // Ngăn chặn chuyển focus tự động
                e.Handled = true;
                txbPassword.Focus();
            }
        }
    }
}
