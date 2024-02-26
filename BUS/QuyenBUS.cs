using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows.Forms;
using DAO; 
using DTO; 

namespace BUS
{
    public class QuyenBUS
    {
        private QuyenDAO quyenDAO;
        DatabaseHelper dbHelper;
        public QuyenBUS(string connectionString)
        {
            quyenDAO = new QuyenDAO(connectionString);
            dbHelper = new DatabaseHelper(connectionString);
        }

        public List<QuyenDTO> GetAllQuyen()
        {
            return quyenDAO.GetAllQuyen();
        }

        public int AddQuyen(QuyenDTO quyen)
        {
            return quyenDAO.AddQuyen(quyen);
        }

        public bool UpdateQuyen(QuyenDTO quyen)
        {
            return quyenDAO.UpdateQuyen(quyen);
        }

        public bool DeleteQuyen(int maQuyen)
        {
            return quyenDAO.DeleteQuyen(maQuyen);
        }


        public void DumpDataComBoBox(ComboBox cbb)
        {
            List<QuyenDTO> list = GetAllQuyen();
            cbb.DataSource = list;
            cbb.DisplayMember = "TenQuyen"; 
            cbb.ValueMember = "MaQuyen"; 
        }

        public string GetTenQuyenByMaQuyen(int maQuyen)
        {
            string query = "SELECT TenQuyen FROM Quyen WHERE MaQuyen = @MaQuyen";

            SqlParameter[] parameters = new SqlParameter[]
            {
        new SqlParameter("@MaQuyen", maQuyen)
            };

            object result = dbHelper.ExecuteScalar(query, parameters);

            if (result != null)
            {
                return result.ToString();
            }
            return ""; 
        }



    }
}
