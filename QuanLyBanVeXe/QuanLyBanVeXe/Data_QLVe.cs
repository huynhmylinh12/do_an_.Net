using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace QuanLyBanVeXe
{
    class Data_QLVe
    {
        //Khai báo đối tượng kết nối
        SqlConnection conn = new SqlConnection("Data Source = DESKTOP-3CLCT2U\\SQLEXPRESS;Initial Catalog = QL_VEXEKHACH; Integrated Security = True");

        //1. tạo Dataset 
        DataSet ds_QLVe = new DataSet();
        SqlDataAdapter da_Ve;


        public Data_QLVe()
        {
            loadVe();

        }

        public void loadVe()
        {
            //B1
            string CauLenh = "select * from VE";
            //b2
            da_Ve = new SqlDataAdapter(CauLenh, conn);
            //b3
            da_Ve.Fill(ds_QLVe, "VE");
            //b4 xét khóa chính
            DataColumn[] key = new DataColumn[1];
            key[0] = ds_QLVe.Tables["VE"].Columns[0];
            //set Khóa chính
            ds_QLVe.Tables["VE"].PrimaryKey = key;
        }

        public DataTable LoadDLVe()
        {
            return ds_QLVe.Tables["VE"];
        }

        public void LoadTuyenXe()
        {

            string CauLenh = "select * from TUYENXE";

            da_Ve = new SqlDataAdapter(CauLenh, conn);

            da_Ve.Fill(ds_QLVe, "TuyenXe");
            //B4 set khóa chính
            DataColumn[] key = new DataColumn[1];
            key[0] = ds_QLVe.Tables["TuyenXe"].Columns[0];
            //set khóa chính
            ds_QLVe.Tables["TuyenXe"].PrimaryKey = key;

        }

        public DataTable getTuyenXe()
        {
            return ds_QLVe.Tables["TuyenXe"];
        }

        public void loadXe()
        {
            //B1
            string CauLenh = "select * from Xe";
            //b2
            da_Ve = new SqlDataAdapter(CauLenh, conn);
            //b3
            da_Ve.Fill(ds_QLVe, "Xe");
            //b4 xét khóa chính
            DataColumn[] key = new DataColumn[1];
            key[0] = ds_QLVe.Tables["Xe"].Columns[0];
            //set Khóa chính
            ds_QLVe.Tables["Xe"].PrimaryKey = key;
        }

        public DataTable LoadDLXe()
        {
            return ds_QLVe.Tables["Xe"];
        }

        public bool ThemVe(string pmaVe, string pmaTuyenXe, string pmaXe, string pngayDi, string pGio, int psoGheTrong)
        {
            try
            {
                //b1
                DataRow dr_row = ds_QLVe.Tables["VE"].NewRow();
                //b2 gán dữ liệu cho row
                dr_row["MAVE"] = pmaVe;
                dr_row["MATUYENXE"] = pmaTuyenXe;
                dr_row["MAXE"] = pmaXe;
                dr_row["NGAYDI"] = pngayDi;
                dr_row["GIO"] = pGio;
                dr_row["SOGHETRONG"] = psoGheTrong;


                //b3 Thêm vào bảng Xe tại dataset
                ds_QLVe.Tables["VE"].Rows.Add(dr_row);
                //b4 lưu vào csdl
                SqlDataAdapter da_Ve_buil = new SqlDataAdapter("select * from VE", conn);
                SqlCommandBuilder builVe = new SqlCommandBuilder(da_Ve_buil);
                //update vào csdl 
                da_Ve_buil.Update(ds_QLVe, "VE");
                return true;
            }
            catch
            {
                return false;
            }
        }


        public int XoaVe(string pmaVE)
        {
            try
            {
                //b1
                if (ktra_KhoaNgoai_BanVe(pmaVE) == false)
                {
                    DataRow dr_row = ds_QLVe.Tables["VE"].Rows.Find(pmaVE);
                    dr_row.Delete();
                    //b4 lưu vào csdl
                    string str = "select * from VE where MAVE = " + pmaVE;
                    SqlDataAdapter da_VE = new SqlDataAdapter(str, conn);
                    SqlCommandBuilder builpVE = new SqlCommandBuilder(da_VE);
                    //update vào csdl 
                    da_VE.Update(ds_QLVe, "VE");
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

        public bool ktra_KhoaNgoai_BanVe(string pMaVE)
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("select * from BANVE where MAVE = " + pMaVE, conn);
            SqlDataReader re = cmd.ExecuteReader();

            while (re.Read())
            {
                return true;
                conn.Close();
            }
            conn.Close();
            return false;
        }

        public bool SuaVE(string pmaVe, string pmaTuyenXe, string pmaXe, string pngayDi, string pGio, int psoGheTrong)
        {
            try
            {
                DataRow rowUpdate = ds_QLVe.Tables["VE"].Rows.Find(pmaVe);
                rowUpdate["MATUYENXE"] = pmaTuyenXe;
                rowUpdate["MAXE"] = pmaXe;
                rowUpdate["NGAYDI"] = pngayDi;
                rowUpdate["GIO"] = pGio;

                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.UpdateCommand = new SqlCommand(@"UPDATE VE SET MATUYENXE = @MATUYENXE, MAXE = @MAXE, NGAYDI = @NGAYDI, GIO = @GIO
                                                        WHERE MAVE = @MAVE", conn);
                adapter.UpdateCommand.Parameters.Add("@MATUYENXE", SqlDbType.Int, 255, "MATUYENXE");
                adapter.UpdateCommand.Parameters.Add("@MAXE", SqlDbType.Int, 255, "MAXE");
                adapter.UpdateCommand.Parameters.Add("@NGAYDI", SqlDbType.Date, 255, "NGAYDI");
                adapter.UpdateCommand.Parameters.Add("@GIO", SqlDbType.Time, 255, "GIO");
                
                var pr2 = adapter.UpdateCommand.Parameters.Add(
                    new SqlParameter("@MAVE", SqlDbType.Int) { SourceColumn = "MAVE" });
                pr2.SourceVersion = DataRowVersion.Original;
                adapter.Update(ds_QLVe, "VE");

                return true;

            }
            catch(Exception e) { Console.Write(e.Message); return false; }
        }


    }
}
