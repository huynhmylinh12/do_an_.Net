using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
namespace QuanLyBanVeXe
{
    class Data_QLXe
    {
        //Khai báo đối tượng kết nối
        SqlConnection conn = new SqlConnection("Data Source = DESKTOP-3CLCT2U\\SQLEXPRESS;Initial Catalog = QL_VEXEKHACH; Integrated Security = True");

        //1. tạo Dataset 
        DataSet ds_QLXe = new DataSet();
        SqlDataAdapter da_Xe;


        public Data_QLXe()
        {
            loadXe();

        }

        public void loadXe()
        {
            //B1
            string CauLenh = "select * from Xe";
            //b2
            da_Xe = new SqlDataAdapter(CauLenh, conn);
            //b3
            da_Xe.Fill(ds_QLXe, "Xe");
            //b4 xét khóa chính
            DataColumn[] key = new DataColumn[1];
            key[0] = ds_QLXe.Tables["Xe"].Columns[0];
            //set Khóa chính
            ds_QLXe.Tables["Xe"].PrimaryKey = key;
        }

        public DataTable LoadDLXe()
        {
            return ds_QLXe.Tables["Xe"];
        }

        public bool ThemXe(string pmaXe, string ploaiXe, int psoghe)
        {
            try
            {
                //b1
                DataRow dr_row = ds_QLXe.Tables["Xe"].NewRow();
                //b2 gán dữ liệu cho row
                dr_row["MaXe"] = pmaXe;
                dr_row["LoaiXe"] = ploaiXe;
                dr_row["SoGhe"] = psoghe;


                //b3 Thêm vào bảng Xe tại dataset
                ds_QLXe.Tables["Xe"].Rows.Add(dr_row);
                //b4 lưu vào csdl
                SqlDataAdapter da_Xe_buil = new SqlDataAdapter("select * from Xe", conn);
                SqlCommandBuilder builXe = new SqlCommandBuilder(da_Xe_buil);
                //update vào csdl 
                da_Xe_buil.Update(ds_QLXe, "Xe");
                return true;
            }
            catch
            {
                return false;
            }
        }


        public int XoaXe(string pmaXe)
        {
            try
            {
                //b1
                if (ktra_KhoaNgoai_Ve(pmaXe) == false)
                {
                    DataRow dr_row = ds_QLXe.Tables["Xe"].Rows.Find(pmaXe);
                    dr_row.Delete();
                    //b4 lưu vào csdl
                    string str = "select * from Xe where MAXE = " + pmaXe;
                    SqlDataAdapter da_Xe = new SqlDataAdapter(str, conn);
                    SqlCommandBuilder builpXe = new SqlCommandBuilder(da_Xe);
                    //update vào csdl 
                    da_Xe.Update(ds_QLXe, "Xe");
                    return 1;
                }
                else
                    return -1;
            }
            catch
            {
                return 0;
            }
        }

        public bool ktra_KhoaNgoai_Ve(string pMaXe)
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("select * from Ve where MAXE = " + pMaXe, conn);
            SqlDataReader re = cmd.ExecuteReader();

            while (re.Read())
            {
                return true;
                conn.Close();
            }
            conn.Close();
            return false;
        }

        public bool LuuXe(string pmaXe, string ploaiXe, int psoghe)
        {
            try
            {
                //b1
                DataRow dr_row = ds_QLXe.Tables["Xe"].Rows.Find(pmaXe);
                //b2 gán dữ liệu cho row

                dr_row["LoaiXe"] = ploaiXe;
                dr_row["SoGhe"] = psoghe;



                //b3 Thêm vào bảng tại dataset
                ds_QLXe.Tables["Xe"].Rows.Add(dr_row);
                //b4 lưu vào csdl
                SqlDataAdapter da_Xe_buil = new SqlDataAdapter("select * from Xe", conn);
                SqlCommandBuilder builpXe = new SqlCommandBuilder(da_Xe_buil);
                //update vào csdl 
                da_Xe_buil.Update(ds_QLXe, "Xe");
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool SuaXe(string pmaXe, string ploaiXe, int psoghe)
        {
            try
            {
                DataRow rowUpdate = ds_QLXe.Tables["Xe"].Rows.Find(pmaXe);
                rowUpdate["LOAIXE"] = ploaiXe;
                rowUpdate["SOGHE"] = psoghe;
               
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.UpdateCommand = new SqlCommand(@"UPDATE XE SET LOAIXE = @LOAIXE, SOGHE = @SOGHE
                                             WHERE MAXE = @MAXE", conn);
                adapter.UpdateCommand.Parameters.Add("@LOAIXE", SqlDbType.NVarChar, 255, "LOAIXE");
                adapter.UpdateCommand.Parameters.Add("@SOGHE", SqlDbType.Int, 255, "SOGHE");
                

                var pr2 = adapter.UpdateCommand.Parameters.Add(
                    new SqlParameter("@MAXE", SqlDbType.Int) { SourceColumn = "MAXE" });
                pr2.SourceVersion = DataRowVersion.Original;
                adapter.Update(ds_QLXe, "XE");

                return true;

            }
            catch { return false; }
        }
    }
}
