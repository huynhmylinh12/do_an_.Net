using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace QuanLyBanVeXe
{
    class Data_DangNhap
    {
        SqlConnection sqlconn = new SqlConnection("Data Source=DESKTOP-3CLCT2U\\SQLEXPRESS; Initial Catalog= QL_VeXeKhach; User ID=sa; Password =123456");
        
        DataSet ds = new DataSet();
        SqlDataAdapter da;
        public Data_DangNhap()
        {
            LoadNhanVien();
        }


        // Tương tác bảng khoa
        public void LoadNhanVien()
        {

            string CauLenh = "select * from NHANVIEN";

            da = new SqlDataAdapter(CauLenh, sqlconn);

            da.Fill(ds, "NhanVien");
            //B4 set khóa chính
            DataColumn[] key = new DataColumn[1];
            key[0] = ds.Tables["NhanVien"].Columns[0];
            //set khóa chính
            ds.Tables["NhanVien"].PrimaryKey = key;

        }

        public DataTable getNhanVien()
        {
            return ds.Tables["NhanVien"];
        }

        public bool ktra_DangNhap(string pTenDN, string pMatKhau)
        {
            foreach (DataRow row in ds.Tables["NhanVien"].Rows)
            {
                if (pTenDN.Equals(row["TENDN"].ToString()) && pMatKhau.Equals(row["MATKHAU"].ToString()))
                {
                    return true;
                }
            }
            return false;
        }

        public DataRow getNhanVien_DaDangNhap(string pTenDN, string pMatKhau)
        {
            foreach (DataRow row in ds.Tables["NhanVien"].Rows)
            {
                if (pTenDN.Equals(row["TENDN"].ToString()) && pMatKhau.Equals(row["MATKHAU"].ToString()))
                {
                    return row;
                }
            }
            return null;
        }
    }
}
