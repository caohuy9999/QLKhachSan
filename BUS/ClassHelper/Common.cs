using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BUS.ClassHelper
{
    public class Common
    {
        public static bool IsNumber(string pValue)
        {
            if (pValue == "")
            {
                return false;
            }
            for (int i = 0; i < pValue.Length; i++)
            {
                if (!char.IsDigit(pValue[i]))
                {
                    return false;
                }
            }
            return true;
        }

        public static bool IsValidEmail(string email)
        {
            // Sử dụng biểu thức chính quy để kiểm tra địa chỉ email
            string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            Regex regex = new Regex(pattern);

            return regex.IsMatch(email);
        }

        public static string ConvertToVietnameseText(decimal number)
        {
            string[] units = { "", "nghìn", "triệu", "tỷ" };
            string[] words = { "không", "một", "hai", "ba", "bốn", "năm", "sáu", "bảy", "tám", "chín" };

            string result = "";

            int unitIndex = 0;

            while (number > 0)
            {
                decimal chunk = number % 1000;
                number = Math.Floor(number / 1000);

                if (chunk > 0)
                {
                    string chunkText = "";

                    int ones = (int)(chunk % 10);
                    int tens = (int)((chunk % 100) / 10);
                    int hundreds = (int)(chunk / 100);

                    if (hundreds > 0)
                    {
                        chunkText += words[hundreds] + " trăm";
                        if (tens > 0 || ones > 0)
                            chunkText += " ";
                    }

                    if (tens > 1)
                    {
                        chunkText += words[tens] + " mươi";
                        if (ones > 0)
                            chunkText += " ";
                    }
                    else if (tens == 1)
                    {
                        chunkText += "mười";
                        if (ones > 0)
                            chunkText += " ";
                    }

                    if (ones > 0 && tens != 1)
                        chunkText += words[ones];

                    if (unitIndex > 0)
                        chunkText += " " + units[unitIndex];

                    result = chunkText + " " + result;
                }

                unitIndex++;
            }

            return result.Trim();
        }

        public static void ShowForm(Form form_0)
        {
            try
            {
                form_0.ShowInTaskbar = false;
                form_0.ShowDialog();
            }
            catch
            {
            }
        }

      

     
        public static string SelectImage(string title = "Chọn Hình Ảnh", string typeFile = "Image Files (*.jpg, *.png, *.gif, *.bmp)|*.jpg;*.png;*.gif;*.bmp|All files (*.*)|*.*")
        {
            string result = "";
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = typeFile;
                openFileDialog.InitialDirectory = PathExE();
                openFileDialog.Title = title;
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    result = openFileDialog.FileName;
                }
            }
            return result;
        }

        public static bool CopyFile(string path_Goc, string path_Dich)
        {
            if (File.Exists(path_Goc))
            {
                try
                {
                    File.Copy(path_Goc, path_Dich, true);
                    if (File.Exists(path_Dich))
                    {
                        return true; // Sao chép thành công
                    }

                }
                catch (Exception ex)
                {
                    if (File.Exists(path_Dich))
                    {
                        return true; // Sao chép thành công
                    }
                    Console.WriteLine("Lỗi khi sao chép tệp: " + ex.Message);
                }
            }
            else
            {
                Console.WriteLine("Tệp nguồn không tồn tại.");
            }
            return false; // Sao chép thất bại
        }

        public static string PathExE()
        {
            return Path.GetDirectoryName(Application.ExecutablePath);
        }

        public static string Path_NameFile(string pathFile, bool WithoutExtension = true)
        {
            if (WithoutExtension)
            {
                return Path.GetFileName(pathFile);
            }
            return Path.GetFileNameWithoutExtension(pathFile);
        }

        public static bool FileTonTai(string path)
        {
            try
            {
                if (File.Exists(path))
                {
                    return true; //  File ton tai
                }

            }
            catch (Exception)
            {
            }

            return false; // File k ton tai
        }

        public static int ConvertToInt32(object value)
        {
            int result = 0;
            if (value != null)
            {
                int.TryParse(value.ToString(), out result);
            }
            return result;
        }

        public static double ConvertToDouble(object value)
        {
            double result = 0.0;
            if (value != null)
            {
                double.TryParse(value.ToString(), out result);
            }
            return result;
        }

        public static string ConvertToString(object value)
        {
            return value?.ToString() ?? string.Empty;
        }

        public static bool ConvertToBoolean(object value)
        {
            bool result = false;
            if (value != null)
            {
                bool.TryParse(value.ToString(), out result);
            }
            return result;
        }

        public static char ConvertToChar(object value)
        {
            char result = '\0';
            if (value != null)
            {
                char.TryParse(value.ToString(), out result);
            }
            return result;
        }

        public static DateTime ConvertToDateTime(object value)
        {
            DateTime result = DateTime.MinValue;
            if (value != null)
            {
                DateTime.TryParse(value.ToString(), out result);
            }
            return result;
        }

        public static byte ConvertToByte(object value)
        {
            byte result = 0;
            if (value != null)
            {
                byte.TryParse(value.ToString(), out result);
            }
            return result;
        }

        public static long ConvertToLong(object value)
        {
            long result = 0;
            if (value != null)
            {
                long.TryParse(value.ToString(), out result);
            }
            return result;
        }

        public static decimal ConvertToDecimal(object value)
        {
            decimal result = 0;
            if (value != null)
            {
                decimal.TryParse(value.ToString(), out result);
            }
            return result;
        }

        public static short ConvertToShort(object value)
        {
            short result = 0;
            if (value != null)
            {
                short.TryParse(value.ToString(), out result);
            }
            return result;
        }

        public static ushort ConvertToUShort(object value)
        {
            ushort result = 0;
            if (value != null)
            {
                ushort.TryParse(value.ToString(), out result);
            }
            return result;
        }

        public static sbyte ConvertToSByte(object value)
        {
            sbyte result = 0;
            if (value != null)
            {
                sbyte.TryParse(value.ToString(), out result);
            }
            return result;
        }

        public static uint ConvertToUInt(object value)
        {
            uint result = 0;
            if (value != null)
            {
                uint.TryParse(value.ToString(), out result);
            }
            return result;
        }

        public static ulong ConvertToULong(object value)
        {
            ulong result = 0;
            if (value != null)
            {
                ulong.TryParse(value.ToString(), out result);
            }
            return result;
        }

        public static float ConvertToFloat(object value)
        {
            float result = 0.0f;
            if (value != null)
            {
                float.TryParse(value.ToString(), out result);
            }
            return result;
        }

        public static bool ConvertToBool(object value)
        {
            bool result = false;
            if (value != null)
            {
                bool.TryParse(value.ToString(), out result);
            }
            return result;
        }


        public static List<string> RemoveEmptyItems(List<string> List)
        {
            List<string> list = new List<string>();
            string text = "";
            for (int i = 0; i < List.Count; i++)
            {
                text = List[i].Trim();
                if (text != "")
                {
                    list.Add(text);
                }
            }
            return list;
        }


        public static decimal TinhGiaPhong(DateTime ngayCheckin, DateTime ngayCheckout, decimal giaPhongCoBan = 1000000m)
        {
            decimal giaPhong = 0; // Giá phòng cơ bản là 1 triệu

            TimeSpan thoiGianLuuTru = ngayCheckout - ngayCheckin;
            int soNgay = (int)Math.Ceiling(thoiGianLuuTru.TotalDays); // Số ngày làm tròn lên

            if (soNgay <= 0)
            {

            }
            else
            {
                giaPhong += giaPhongCoBan * soNgay;
            }
            // Tính phụ thu dựa trên thời gian check-in
            if (ngayCheckin.TimeOfDay >= new TimeSpan(5, 0, 0) && ngayCheckin.TimeOfDay < new TimeSpan(9, 0, 0))
            {
                giaPhong += giaPhongCoBan * 0.5m; // Check in từ 5h - 9h: Phụ thu 50%
            }
            else if (ngayCheckin.TimeOfDay >= new TimeSpan(9, 0, 0) && ngayCheckin.TimeOfDay < new TimeSpan(14, 0, 0))
            {
                giaPhong += giaPhongCoBan * 0.3m; // Check in từ 9h - 14h: Phụ thu 30%
            }


            // Tính phụ thu dựa trên thời gian check-out
            if (ngayCheckout.TimeOfDay >= new TimeSpan(12, 0, 0) && ngayCheckout.TimeOfDay < new TimeSpan(15, 0, 0))
            {
                giaPhong += giaPhongCoBan * 0.3m; // Trả phòng từ 12h - 15h: Phụ thu 30%
            }
            else if (ngayCheckout.TimeOfDay >= new TimeSpan(15, 0, 0) && ngayCheckout.TimeOfDay < new TimeSpan(18, 0, 0))
            {
                giaPhong += giaPhongCoBan * 0.5m; // Trả phòng từ 15h - 18h: Phụ thu 50%
            }
            else if (ngayCheckout.TimeOfDay >= new TimeSpan(18, 0, 0))
            {
                giaPhong += giaPhongCoBan; // Trả phòng sau 18h: Phụ thu 100%
            }

            return giaPhong;
        }

    }
}
