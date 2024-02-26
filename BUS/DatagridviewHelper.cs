using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUI
{
  public  class DatagridviewHelper
    {
		public static void SetStatusDataGridViewWithWait(DataGridView dgv, int row, string colName, int timeWait = 0, string status = "Đơ\u0323i {time} giây...")
		{
			try
			{
				int time_start = Environment.TickCount;
				while ((Environment.TickCount - time_start) / 1000 - timeWait < 0)
				{
					dgv.Invoke((MethodInvoker)delegate
					{
						dgv.Rows[row].Cells[colName].Value = status.Replace("{time}", (timeWait - (Environment.TickCount - time_start) / 1000).ToString());
					});
					Thread.Sleep(500);
				}
			}
			catch
			{
			}
		}

		public static void SetStatusDataGridViewWithWait(DataGridView dgv, int row, string colName, int timeWait = 0, int timeStart = 0, string status = "Đơ\u0323i {time} giây...")
		{
			try
			{
				int time_start = Environment.TickCount;
				while ((Environment.TickCount - time_start) / 1000 - timeWait < 0)
				{
					dgv.Invoke((MethodInvoker)delegate
					{
						dgv.Rows[row].Cells[colName].Value = status.Replace("{time}", (timeStart - (Environment.TickCount - time_start) / 1000).ToString());
					});
					Thread.Sleep(500);
				}
			}
			catch
			{
			}
		}

		public static string GetStatusDataGridView(DataGridView dgv, int row, int col)
		{
			string output = "";
			try
			{
				if (dgv.Rows[row].Cells[col].Value != null)
				{
					try
					{
						output = dgv.Rows[row].Cells[col].Value.ToString();
					}
					catch
					{
						dgv.Invoke((MethodInvoker)delegate
						{
							output = dgv.Rows[row].Cells[col].Value.ToString();
						});
					}
				}
			}
			catch
			{
			}
			return output;
		}

		public static string GetStatusDataGridView(DataGridView dgv, int row, string colName)
		{
			string output = "";
			try
			{
				if (dgv.Rows[row].Cells[colName].Value != null)
				{
					try
					{
						output = dgv.Rows[row].Cells[colName].Value.ToString();
					}
					catch
					{
						dgv.Invoke((MethodInvoker)delegate
						{
							output = dgv.Rows[row].Cells[colName].Value.ToString();
						});
					}
				}
			}
			catch
			{
			}
			return output;
		}

		public static void SetStatusDataGridView(DataGridView dgv, int row, int col, object status)
		{
			try
			{
				try
				{
					dgv.Invoke((MethodInvoker)delegate
					{
						dgv.Rows[row].Cells[col].Value = status;
					});
				}
				catch
				{
					dgv.Rows[row].Cells[col].Value = status;
				}
			}
			catch
			{
			}
		}

		public static void SetStatusDataGridView(DataGridView dgv, int row, string colName, object status)
		{
			try
			{
				try
				{
					dgv.Invoke((MethodInvoker)delegate
					{
						dgv.Rows[row].Cells[colName].Value = status;
					});
				}
				catch
				{
					dgv.Rows[row].Cells[colName].Value = status;
				}
			}
			catch
			{
			}
		}

        public static void TitleCenter(DataGridView dgv)
        {
            try
            {
                foreach (DataGridViewColumn column in dgv.Columns)
                {
                    column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }
            }
            catch
            {
            }
        }

		public static void DataCenter(DataGridView dgv, bool DownLine = false)
        {
            try
            {
                foreach (DataGridViewColumn col in dgv.Columns)
                {
                    col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter; // Căn giữa
					if (DownLine)
                        col.DefaultCellStyle.WrapMode = DataGridViewTriState.True; // Cho phép xuống dòng
                   
                }
                // Cho một cột cụ thể (ví dụ: cột có index là 0)
                //dataGridView1.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                //dataGridView1.Columns[0].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            }
            catch
            {
            }
        }
    }
}
