using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BUS.ClassHelper
{
    public class MessageBoxHelper
    {
        public static void ShowMessageBox(object content, int int_0 = 1)
        {
            MessageBoxIcon icon = MessageBoxIcon.None;
            switch (int_0)
            {
                case 1:
                    icon = MessageBoxIcon.Asterisk;
                    break;
                case 2:
                    icon = MessageBoxIcon.Hand;
                    break;
                case 3:
                    icon = MessageBoxIcon.Exclamation;
                    break;
                case 4:
                    icon = MessageBoxIcon.Exclamation;
                    break;
            }
            MessageBox.Show(content.ToString(), "HuySoftware", MessageBoxButtons.OK, icon);
        }

        public static DialogResult Show(string string_0)
        {
            return MessageBox.Show(string_0, "HuySoftware", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        }
    }
}
