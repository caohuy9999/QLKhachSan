using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BUS.ClassHelper
{
    public class FileDialogHelper
    {
        public static string OpenFile(string title = "Chọn Tệp", string filter = "Tất cả các tệp|*.*", bool multiselect = false)
        {
            string selectedFilePath = string.Empty;

            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Title = title;
                openFileDialog.Filter = filter;
                openFileDialog.Multiselect = multiselect;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    selectedFilePath = multiselect ? string.Join(", ", openFileDialog.FileNames) : openFileDialog.FileName;
                }
            }

            return selectedFilePath;
        }

        public static string SaveFile(string title = "Lưu Tệp", string filter = "Tất cả các tệp|*.*")
        {
            string savedFilePath = string.Empty;

            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Title = title;
                saveFileDialog.Filter = filter;

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    savedFilePath = saveFileDialog.FileName;
                }
            }

            return savedFilePath;
        }

        public static string SelectFolder(string description = "Chọn Thư Mục", string initialPath = "")
        {
            string selectedFolderPath = string.Empty;

            using (FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog())
            {
                folderBrowserDialog.Description = description;
                folderBrowserDialog.SelectedPath = initialPath; // Đặt đường dẫn bắt đầu

                if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
                {
                    selectedFolderPath = folderBrowserDialog.SelectedPath;
                }
            }

            return selectedFolderPath;
        }
    }
}
