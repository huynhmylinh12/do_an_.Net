using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace QuanLyBanVeXe
{
    class Data_QLTuyenXe
    {
        //Khai báo đối tượng kết nối
        SqlConnection conn = new SqlConnection("Data Source = DESKTOP-3CLCT2U\\SQLEXPRESS;Initial Catalog = QL_VEXEKHACH; Integrated Security = True");

        //1. tạo Dataset 
        DataSet ds_QLTX = new DataSet();
        SqlDataAdapter da_TuyenXen;


        public Data_QLTuyenXe()
        {
            loadTuyenXe();

        }

        public void loadTuyenXe()
        {
            //B1
            string CauLenh = "select * from TuyenXe";
            //b2
            da_TuyenXen = new SqlDataAdapter(CauLenh, conn);
            //b3
            da_TuyenXen.Fill(ds_QLTX, "TuyenXe");
            //b4 xét khóa chính
            DataColumn[] key = new DataColumn[1];
            key[0] = ds_QLTX.Tables["TuyenXe"].Columns[0];
            //set Khóa chính
            ds_QLTX.Tables["TuyenXe"].PrimaryKey = key;
        }

        public DataTable LoadDLTuyenXe()
        {
            return ds_QLTX.Tables["TuyenXe"];
        }


        public bool ThemTuyenXe(string pmaTuyen, string ptenTuyen, string pdiemDi, string pdiemDen, string pchiTiet, double pgiaVe)
        {
            try
            {
                //b1
                DataRow dr_row = ds_QLTX.Tables["TuyenXe"].NewRow();
                //b2 gán dữ liệu cho row
                dr_row["MATUYENXE"] = pmaTuyen;
                dr_row["TENTUYENXE"] = ptenTuyen;
                dr_row["DIEMDI"] = pdiemDi;
                dr_row["DIEMDEN"] = pdiemDen;
                dr_row["CHITIETTUYENXE"] = pchiTiet;
                dr_row["GIAVE"] = pgiaVe;
                

                //b3 Thêm vào bảng khoa tại dataset
                ds_QLTX.Tables["TuyenXe"].Rows.Add(dr_row);
                //b4 lưu vào csdl
                SqlDataAdapter da_TuyenXe_buil = new SqlDataAdapter("select * from TuyenXe", conn);
                SqlCommandBuilder builpTuyenXe = new SqlCommandBuilder(da_TuyenXe_buil);
                //update vào csdl
                da_TuyenXe_buil.Update(ds_QLTX, "TuyenXe");
                return true;
            }
            catch
            {
                return false;
            }
        }

        public int XoaTuyenXe(string pmaTX)
        {
            try
            {
                //b1
                if (ktra_KhoaNgoai_Ve(pmaTX) == false)
                {
                    DataRow dr_row = ds_QLTX.Tables["TuyenXe"].Rows.Find(pmaTX);
                    dr_row.Delete();
                    //b4 lưu vào csdl
                    string str = "select * from TuyenXe where MATUYENXE = " + pmaTX;
                    SqlDataAdapter da_TuyenXe = new SqlDataAdapter(str, conn);
                    SqlCommandBuilder builpTuyenXe = new SqlCommandBuilder(da_TuyenXe);
                    //update vào csdl 
                    da_TuyenXe.Update(ds_QLTX, "TuyenXe");
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

        public bool ktra_KhoaNgoai_Ve(string pMaTX)
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("select * from VE where MATUYENXE = " + pMaTX, conn);
            SqlDataReader re = cmd.ExecuteReader();

            while (re.Read())
            {
                return true;
                conn.Close();
            }
            conn.Close();
            return false;
        }




        public bool SuaTuyenXe(string pmaTuyen, string ptenTuyen, string pdiemDi, string pdiemDen, string pchiTiet, double pgiaVe)
        {
            try
            {
                DataRow rowUpdate = ds_QLTX.Tables["TuyenXe"].Rows.Find(pmaTuyen);
                rowUpdate["TENTUYENXE"] = ptenTuyen;
                rowUpdate["DIEMDI"] = pdiemDi;
                rowUpdate["DIEMDEN"] = pdiemDen;
                rowUpdate["CHITIETTUYENXE"] = pchiTiet;
                rowUpdate["GIAVE"] = pgiaVe;
                

                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.UpdateCommand = new SqlCommand(@"UPDATE TUYENXE SET TENTUYENXE = @TENTUYENXE, DIEMDI = @DIEMDI, DIEMDEN = @DIEMDEN, CHITIETTUYENXE = @CHITIETTUYENXE, 
                                            GIAVE = @GIAVE
                                             WHERE MATUYENXE = @MATUYENXE", conn);
                adapter.UpdateCommand.Parameters.Add("@TENTUYENXE", SqlDbType.NVarChar, 255, "TENTUYENXE");
                adapter.UpdateCommand.Parameters.Add("@DIEMDI", SqlDbType.NVarChar, 255, "DIEMDI");
                adapter.UpdateCommand.Parameters.Add("@DIEMDEN", SqlDbType.NVarChar, 255, "DIEMDEN");
                adapter.UpdateCommand.Parameters.Add("@CHITIETTUYENXE", SqlDbType.NVarChar, 255, "CHITIETTUYENXE");
                adapter.UpdateCommand.Parameters.Add("@GIAVE", SqlDbType.Money, 255, "GIAVE");
                

                var pr2 = adapter.UpdateCommand.Parameters.Add(
                    new SqlParameter("@MATUYENXE", SqlDbType.Int) { SourceColumn = "MATUYENXE" });
                pr2.SourceVersion = DataRowVersion.Original;
                adapter.Update(ds_QLTX, "TUYENXE");

                return true;

            }
            catch { return false; }
        }


    }
}
