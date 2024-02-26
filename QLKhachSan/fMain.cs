using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BUS;
using BUS.ClassHelper;
using QLKhachSan.fHelper.fBieuDo;
using QLKhachSan.fHelper.fHoaDon;
using QLKhachSan.fHelper.fKhachHang;
using QLKhachSan.fHelper.fTienNghi;
using QLKhachSan.fHelper.Phong;
using QLKhachSan.fHelper.TienIchPhong;

namespace QLKhachSan
{
    public partial class fMain : Form
    {
        public static string connectionString = fLogin.KeystringSQL;
        private NhanVienBUS nhanVienBUS;
        private PhongBUS phongBUS;
        List<Image> images = new List<Image>();
        private int currentIndex = 0;
        private bool Ismenu = true;

        private Form activeFrom = null;

        internal bool TileExpand = true;
        internal bool optionLeftPhong = true;
        internal bool optionLeftDichVu = true;
        internal bool optionLeftTienNghi = true;
        private object lock_ShowFrom = new object();

        private void AutoBitmap_background()
        {
            for (int i = 0; i < 11; i++)
            {
                string path = $@"img\background\{i}.png";
                using (Bitmap bitmap = new Bitmap(path))
                {
                    images.Add(new Bitmap(bitmap));
                }
            }
        }
        public static string optionScriptString = "";
        public fMain()
        {
            InitializeComponent();
           // this.BackColor = Color.Blue;
            this.Padding = new Padding(0, 1, 0, 0);
            btnShowMenu.Padding = new Padding(0, 1, 0, 0);
            AutoBitmap_background();

            pnlMainLeft.Visible = false;
            btnShowMenu.Visible = false;
            btnMaximize.Visible = false;
            openChildFormLogin(new fLogin());

           
        }

        #region TileTop
        private void pnlTileTop_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);
        private void btnClose_Click(object sender, EventArgs e)
        {
            try
            {
                timerbackground.Stop();
                Environment.Exit(0);
                Application.Exit();
            }
            catch
            {
                Close();
            }
        }
        private void btnMaximize_Click(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Maximized)
            {
                this.WindowState = FormWindowState.Normal;
                return;
            }
            Rectangle workingArea = Screen.FromHandle(this.Handle).WorkingArea;
            workingArea.Location = new Point(0, 0);
            this.MaximumSize = workingArea.Size;
            this.WindowState = FormWindowState.Maximized;
        }
        private void btnMinimize_Click(object sender, EventArgs e)
        {
            base.WindowState = FormWindowState.Minimized;
        }
        protected override void WndProc(ref Message m)
        {
            const int RESIZE_HANDLE_SIZE = 10;
            switch (m.Msg)
            {
                case 0x0084/*NCHITTEST*/ :
                    base.WndProc(ref m);

                    if ((int)m.Result == 0x01/*HTCLIENT*/)
                    {
                        Point screenPoint = new Point(m.LParam.ToInt32());
                        Point clientPoint = this.PointToClient(screenPoint);
                        if (clientPoint.Y <= RESIZE_HANDLE_SIZE)
                        {
                            if (clientPoint.X <= RESIZE_HANDLE_SIZE)
                                m.Result = (IntPtr)13/*HTTOPLEFT*/ ;
                            else if (clientPoint.X < (Size.Width - RESIZE_HANDLE_SIZE))
                                m.Result = (IntPtr)12/*HTTOP*/ ;
                            else
                                m.Result = (IntPtr)14/*HTTOPRIGHT*/ ;
                        }
                        else if (clientPoint.Y <= (Size.Height - RESIZE_HANDLE_SIZE))
                        {
                            if (clientPoint.X <= RESIZE_HANDLE_SIZE)
                                m.Result = (IntPtr)10/*HTLEFT*/ ;
                            else if (clientPoint.X < (Size.Width - RESIZE_HANDLE_SIZE))
                                m.Result = (IntPtr)2/*HTCAPTION*/ ;
                            else
                                m.Result = (IntPtr)11/*HTRIGHT*/ ;
                        }
                        else
                        {
                            if (clientPoint.X <= RESIZE_HANDLE_SIZE)
                                m.Result = (IntPtr)16/*HTBOTTOMLEFT*/ ;
                            else if (clientPoint.X < (Size.Width - RESIZE_HANDLE_SIZE))
                                m.Result = (IntPtr)15/*HTBOTTOM*/ ;
                            else
                                m.Result = (IntPtr)17/*HTBOTTOMRIGHT*/ ;
                        }
                    }
                    return;
            }
            base.WndProc(ref m);
        }
        #endregion
        private void openChildFormLogin(Form childForm)
        {
            timerbackground.Stop();
            if (activeFrom != null)
                activeFrom.Close();
            activeFrom = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
         //  childForm.Dock = DockStyle.Right;

            // Đăng ký xử lý sự kiện LoginSuccess từ FormLogin
            if (childForm is fLogin loginForm)
            {
                loginForm.LoginSuccess += LoginForm_LoginSuccess;
            }
            pnMain.Controls.Add(childForm);
            childForm.BringToFront();
            childForm.Show();
        }
        private void LoginForm_LoginSuccess(object sender, EventArgs e)
        {
            
            timer.Start();
            pictureBoxMain.Image = images[1];
            timerbackground.Start();
            //fLogin loginForm = (fLogin)sender;
            // loginForm.Close();
            // Hiển thị nội dung chính trong FormMain
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            switch (optionScriptString)
            {
                case "Flogin":
                    
                        activeFrom.Width -= 50;
                        if (activeFrom.Width <= 0)
                        {
                            if (fLogin.HoTenNV != null || fLogin.TenQuyen != null || fLogin.MaNhanVien != 0)
                            {
                                pnlMainLeft.Visible = true;
                            btnShowMenu.Visible = true;
                                btnMaximize.Visible = true;
                                // timerbackground.Start();
                            }
                            timer.Stop();
                            activeFrom.Close();
                        }
                    break;
                case "pnlMainLeft":
                    if (Ismenu)
                    {
                        pnlMainLeft.Width -= 100;
                        if (pnlMainLeft.Width <= 0)
                        {
                            
                            timer.Stop(); Ismenu = false;
                        }
                    }
                    else
                    {
                        pnlMainLeft.Width += 100;
                        if (pnlMainLeft.Width >= 200)
                        {
                            btnHome.Focus();
                            timer.Stop(); Ismenu = true;
                        }
                    }
                   
                    break;
            }
           
        }


        private void GUI_Load(object sender, EventArgs e)
        {
            
            nhanVienBUS = new NhanVienBUS(connectionString);
            phongBUS = new PhongBUS(connectionString);

            string pathImg = nhanVienBUS.GetPathAvatar(fLogin.MaNhanVien);
            if (Common.FileTonTai(Common.PathExE() + pathImg))
                ptbAvatar.Image = Image.FromFile(Common.PathExE() + pathImg);

          
        }

      

     

        private void openChildForm(Form childForm)
        {
            try
            {
                lock (lock_ShowFrom)
                {
                    timerbackground.Stop();
                    if (activeFrom != null)
                        activeFrom.Close();
                    activeFrom = childForm;
                    childForm.TopLevel = false;
                    childForm.FormBorderStyle = FormBorderStyle.None;
                    childForm.Dock = DockStyle.Fill;

                    pnMain.Controls.Add(childForm);
                    childForm.BringToFront();
                    childForm.Show();

                }
               
            }
            catch (Exception)
            {

            }
        }

       
        private void pictureBoxMain_Click(object sender, EventArgs e)
        {
            timerbackground.Stop();
            timerbackground_Tick(null,null);
            timerbackground.Start();
        }


        private void btnShowMenu_Click(object sender, EventArgs e)
        {
            if (pnlMainLeft.Visible == false)
            {
                pnlMainLeft.Visible = true;
            }
            else if (pnlMainLeft.Visible == true)
            {
                pnlMainLeft.Visible = false;
              
            }
        }

        private void timerbackground_Tick(object sender, EventArgs e)
        {
            if (images.Count > 0)
            {
                pictureBoxMain.Image = images[currentIndex];
                 // Hiển thị hình ảnh tiếp theo
                currentIndex = (currentIndex + 1) % images.Count;
            }
        }

        private void btnHome_Click(object sender, EventArgs e)
        {

            pictureBoxMain.Image = images[1];
            btnShowMenu_Click(sender, e);
            if (activeFrom != null)
            {
                activeFrom.Close();
                timerbackground.Start();
            }
        }

        private void btnLogOut_Click(object sender, EventArgs e)
        {
            if (MessageBoxHelper.Show($"Bạn chắc chắn muốn đăng xuất khỏi User {fLogin.TenQuyen} : {fLogin.HoTenNV}") == DialogResult.No)
            {
                return;
            }
            timerbackground.Stop();
            pictureBoxMain.Image = null;
            pnlMainLeft.Visible = false;
            btnShowMenu.Visible = false;
            btnMaximize.Visible = false;
            openChildFormLogin(new fLogin());
            fLogin.HoTenNV = null; fLogin.TenQuyen = null; fLogin.MaNhanVien = 0;
           
            //Hide();
            //fLogin fActive2 = new fLogin();
            //fActive2.ShowInTaskbar = true;
            //fActive2.ShowDialog();
        }

        private void btnRoom_Click(object sender, EventArgs e)
        {
            pictureBoxMain.Image = null;
            btnShowMenu_Click(sender, e);
            lock (lock_ShowFrom)
            {
                openChildForm(new fDatPhong());
            }
        }

        private void btnUser_Click(object sender, EventArgs e)
        {
            pictureBoxMain.Image = null;
            btnShowMenu_Click(sender, e);
            openChildForm(new fQlUser());
        }

        private void btnService_Click(object sender, EventArgs e)
        {
            pictureBoxMain.Image = null;
            btnShowMenu_Click(sender, e);
            openChildForm(new fQuanLyDichVu(1));
        }

       
        private void btnClient_Click(object sender, EventArgs e)
        {
            pictureBoxMain.Image = null;
            btnShowMenu_Click(sender, e);
            openChildForm(new fKhachHang());
        }

        private void btnBill_Click(object sender, EventArgs e)
        {
            pictureBoxMain.Image = null;
            optionScriptString = "pnlMainLeft";
            btnShowMenu_Click(sender, e);
            openChildForm(new fLichSuHoaDon());
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            pictureBoxMain.Image = null;
            btnShowMenu_Click(sender, e);
            openChildForm(new fBieuDo());
        }

       
        private void btnRoomManager_Click(object sender, EventArgs e)
        {
            pictureBoxMain.Image = null;
            btnShowMenu_Click(sender, e);
            openChildForm(new fPhongByLoaiPhong());
        }

        private void btnRoomAmenities_Click(object sender, EventArgs e)
        {
            pictureBoxMain.Image = null;
            btnShowMenu_Click(sender, e);
            openChildForm(new fTienNghi());
        }

        private void btnVouchers_Click(object sender, EventArgs e)
        {
            pictureBoxMain.Image = null;
            btnShowMenu_Click(sender, e);
            openChildForm(new fTienNghi());
        }

        private void btnAmenitiesRoom_Click(object sender, EventArgs e)
        {
            pictureBoxMain.Image = null;
            btnShowMenu_Click(sender, e);
            openChildForm(new fTienIchPhong());
        }

   

        private void btnVouchers_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Tab && pnlMainLeft.Visible)
            {
                e.IsInputKey = true;
                btnLogOut.Focus();
            }
        }
    }
}
