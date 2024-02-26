using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BUS;

namespace QLKhachSan
{
    public partial class GUI : Form
    {
        public static string connectionString = "Data Source=DESKTOP-Q285043;Initial Catalog=SQLQLKhachSan;Integrated Security=True";
        private NhanVienBUS nhanVienBUS;
        private PhongBUS phongBUS;




        private Form activeFrom = null;

        internal bool TileExpand = true;
        internal bool optionLeftPhong = true;
        internal bool optionLeftDichVu = true;
        internal bool optionLeftTienNghi = true;

       

        internal string optionLeftString = "";
        public GUI()
        {
            InitializeComponent();
        }

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


        private void timerMain_Tick(object sender, EventArgs e)
        {
            switch (optionLeftString)
            {
                case "btnMenu":
                    if (TileExpand)
                    {
                        Sidebar.Width -= 10;
                        if (Sidebar.Width == 60)
                        {
                            TileExpand = false;
                            timerMain.Stop();
                        }
                    }
                    else
                    {
                        Sidebar.Width += 10;
                        if (Sidebar.Width == 200)
                        {
                            TileExpand = true;
                            timerMain.Stop();
                        }
                    }
                    break;

                case "btnOpenScriptPhong":
                    if (optionLeftPhong)
                    {
                        pnlControlPhong.Height += 20;
                        if (pnlControlPhong.Height == pnlControlPhong.MaximumSize.Height)
                        {
                            optionLeftPhong = false;
                            timerMain.Stop();
                        }
                    }
                    else
                    {
                        pnlControlPhong.Height -= 20;
                        if (pnlControlPhong.Height == pnlControlPhong.MinimumSize.Height)
                        {
                            optionLeftPhong = true;
                            timerMain.Stop();
                        }
                    }
                    break;

                case "btnOpenScriptDichVu":
                    if (optionLeftDichVu)
                    {
                        pnlControlDichVu.Height += 20;
                        if (pnlControlDichVu.Height == pnlControlDichVu.MaximumSize.Height)
                        {
                            optionLeftDichVu = false;
                            timerMain.Stop();
                        }
                    }
                    else
                    {
                        pnlControlDichVu.Height -= 20;
                        if (pnlControlDichVu.Height == pnlControlDichVu.MinimumSize.Height)
                        {
                            optionLeftDichVu = true;
                            timerMain.Stop();
                        }
                    }
                    break;

                case "btnOpenScriptTienNghi":
                    if (optionLeftTienNghi)
                    {
                        pnlControlTienNghi.Height += 20;
                        if (pnlControlTienNghi.Height == pnlControlTienNghi.MaximumSize.Height)
                        {
                            optionLeftTienNghi = false;
                            timerMain.Stop();
                        }
                    }
                    else
                    {
                        pnlControlTienNghi.Height -= 20;
                        if (pnlControlTienNghi.Height == pnlControlTienNghi.MinimumSize.Height)
                        {
                            optionLeftTienNghi = true;
                            timerMain.Stop();
                        }
                    }
                    break;

            }
        }

        private void btnMenu_Click(object sender, EventArgs e)
        {
            optionLeftString = "btnMenu";
            timerMain.Start();
        }
        private void btnOpenScriptPhong_Click(object sender, EventArgs e)
        {
            optionLeftString = "btnOpenScriptPhong";
            timerMain.Start();
        }
        private void btnOpenScriptDichVu_Click(object sender, EventArgs e)
        {
            optionLeftString = "btnOpenScriptDichVu"; timerMain.Start();
        }

        private void btnOpenScriptTienNghi_Click(object sender, EventArgs e)
        {
            optionLeftString = "btnOpenScriptTienNghi"; timerMain.Start();
        }



        private void GUI_Load(object sender, EventArgs e)
        {
            
            nhanVienBUS = new NhanVienBUS(connectionString);
            phongBUS = new PhongBUS(connectionString);
        }

        private void btnQlTienNghi_Click(object sender, EventArgs e)
        {

        }

        private void btnChitiettiennghi_Click(object sender, EventArgs e)
        {

        }

        private void btnQlDichVu_Click(object sender, EventArgs e)
        {

        }

        private void btnLoaiDichVu_Click(object sender, EventArgs e)
        {

        }

        private void btnDatPhong_Click(object sender, EventArgs e)
        {

        }

        private void btnQlPhong_Click(object sender, EventArgs e)
        {
            openChildForm(new fDatPhong());


        }

        private void btnLoaiPhong_Click(object sender, EventArgs e)
        {
            var nhanVienData = phongBUS.GetAllPhong();

        }

        private void btnHoaDon_Click(object sender, EventArgs e)
        {

        }

        private void btnQlNhanVien_Click(object sender, EventArgs e)
        {

        }

        private void btnQlTaikhoan_Click(object sender, EventArgs e)
        {


            DataTable nhanVienData = nhanVienBUS.GetAllNhanVien();
            openChildForm(new QlUser(nhanVienData));


        }

        private void openChildForm(Form childForm)
        {
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

        private void btnThongKe_Click(object sender, EventArgs e)
        {

        }

        
    }
}
