using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLKhachSan
{
    public partial class QlUser : Form
    {
        public QlUser()
        {
            InitializeComponent();
        }

        public QlUser(DataTable data)
        {
            InitializeComponent();
            if ( data == null)
            {
                data = new DataTable();
            }
            dtgv.DataSource = data;
        }

        private void QlUser_Load(object sender, EventArgs e)
        {

        }
    }
}
