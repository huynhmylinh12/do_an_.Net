using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace QuanLyBanVeXe
{
    class Data_QLVeDaBan
    {
        SqlConnection sqlconn = new SqlConnection("Data Source=DESKTOP-3CLCT2U\\SQLEXPRESS; Initial Catalog= QL_VeXeKhach; User ID=sa; Password =123456");
        //1. Tạo dataset
        DataSet ds = new DataSet();
        SqlDataAdapter da;
        public Data_QLVeDaBan()
        {
            LoadBanVe();
            LoadVe();
            LoadVeDaBan();
        }


        // Tương tác bảng khoa
        public void LoadBanVe()
        {

            string CauLenh = "select * from BanVe";

            da = new SqlDataAdapter(CauLenh, sqlconn);

            da.Fill(ds, "BanVe");
            //B4 set khóa chính
            DataColumn[] key = new DataColumn[1];
            key[0] = ds.Tables["BanVe"].Columns[0];
            //set khóa chính
            ds.Tables["BanVe"].PrimaryKey = key;

        }

        public DataTable getBanVe()
        {
            return ds.Tables["BanVe"];
        }

        public void LoadVe()
        {

            string CauLenh = "select * from Ve";

            da = new SqlDataAdapter(CauLenh, sqlconn);

            da.Fill(ds, "Ve");
            //B4 set khóa chính
            DataColumn[] key = new DataColumn[1];
            key[0] = ds.Tables["Ve"].Columns[0];
            //set khóa chính
            ds.Tables["Ve"].PrimaryKey = key;

        }

        public DataTable getVe()
        {
            return ds.Tables["Ve"];
        }

        public void LoadVeDaBan()
        {
            string str = "select MABANVE, VE.MAVE, MANV, MATUYENXE, MAXE, TENKH, SDT, VITRIGHE, SOLUONG, NGAYDAT,GIO, NGAYDI, TRANGTHAITHANHTOAN, GHICHU from VE, BANVE WHERE VE.MAVE = BANVE.MAVE";
            da = new SqlDataAdapter(str, sqlconn);
            da.Fill(ds, "VeDaBan");
            DataColumn[] key = new DataColumn[1];
            key[0] = ds.Tables["VeDaBan"].Columns[0];
            //set khóa chính
            ds.Tables["VeDaBan"].PrimaryKey = key;

        }
        public DataTable getVeDaBan()
        {
            return ds.Tables["VeDaBan"];
        }

        public void LoadXe()
        {

            string CauLenh = "select * from XE";

            da = new SqlDataAdapter(CauLenh, sqlconn);

            da.Fill(ds, "Xe");
            //B4 set khóa chính
            DataColumn[] key = new DataColumn[1];
            key[0] = ds.Tables["Xe"].Columns[0];
            //set khóa chính
            ds.Tables["Xe"].PrimaryKey = key;

        }

        public DataTable getXe()
        {
            return ds.Tables["Xe"];
        }

        public void LoadTuyenXe()
        {

            string CauLenh = "select * from TuyenXe";

            da = new SqlDataAdapter(CauLenh, sqlconn);

            da.Fill(ds, "TuyenXe");
            //B4 set khóa chính
            DataColumn[] key = new DataColumn[1];
            key[0] = ds.Tables["TuyenXe"].Columns[0];
            //set khóa chính
            ds.Tables["TuyenXe"].PrimaryKey = key;

        }

        public DataTable getTuyenXe()
        {
            return ds.Tables["TuyenXe"];
        }

        public bool XoaVe(string pmaVe)
        {
            try
            {
                DataRow rowdel = ds.Tables["VeDaBan"].Rows.Find(pmaVe);
                rowdel.Delete();

                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.DeleteCommand = new SqlCommand(@"DELETE FROM BANVE WHERE MABANVE = @MABANVE", sqlconn);
                var pr1 = adapter.DeleteCommand.Parameters.Add(new SqlParameter("@MABANVE", SqlDbType.Int));
                pr1.SourceColumn = "MABANVE";
                pr1.SourceVersion = DataRowVersion.Original;

                adapter.Update(ds, "VeDaBan");
                return true;
            }
            catch (Exception e) { return false; }
        }

        public bool SuaVe(string pMaBanVe, string pMANV, string pTENKH, string pSDT, string pVITRIGHE, string pSOLUONG, string pNGAYDAT, string pTRANGTHAITHANHTOAN, string pGHICHU)
        {
            try 
            {
                DataRow rowUpdate = ds.Tables["VeDaBan"].Rows.Find(pMaBanVe);
                rowUpdate["MANV"] = pMANV;
                rowUpdate["TENKH"] = pTENKH;
                rowUpdate["SDT"] = pSDT;
                rowUpdate["VITRIGHE"] = pVITRIGHE;
                rowUpdate["SOLUONG"] = pSOLUONG;
                rowUpdate["NGAYDAT"] = pNGAYDAT;
                rowUpdate["TRANGTHAITHANHTOAN"] = pTRANGTHAITHANHTOAN;
                rowUpdate["GHICHU"] = pGHICHU;

                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.UpdateCommand = new SqlCommand(@"UPDATE BANVE SET TENKH=@TENKH, SDT = @SDT, VITRIGHE = @VITRIGHE, SOLUONG = @SOLUONG, 
                                            TRANGTHAITHANHTOAN = @TTTHANHTOAN, GHICHU = @GHICHU, NGAYDAT = @NGAYDAT, THANHTIEN = 0, MANV = @MANV
                                             WHERE MABANVE = @MABANVE", sqlconn);
                adapter.UpdateCommand.Parameters.Add("@TENKH", SqlDbType.NVarChar, 255, "TENKH");
                adapter.UpdateCommand.Parameters.Add("@SDT", SqlDbType.NVarChar, 255, "SDT");
                adapter.UpdateCommand.Parameters.Add("@VITRIGHE", SqlDbType.VarChar, 255, "VITRIGHE");
                adapter.UpdateCommand.Parameters.Add("@SOLUONG", SqlDbType.Int, 255, "SOLUONG");
                adapter.UpdateCommand.Parameters.Add("@TTTHANHTOAN", SqlDbType.NVarChar, 255, "TRANGTHAITHANHTOAN");
                adapter.UpdateCommand.Parameters.Add("@GHICHU", SqlDbType.NVarChar, 255, "GHICHU");
                adapter.UpdateCommand.Parameters.Add("@NGAYDAT", SqlDbType.Date, 255, "NGAYDAT");
                adapter.UpdateCommand.Parameters.Add("@MANV", SqlDbType.Int, 255, "MANV");
                var pr2 = adapter.UpdateCommand.Parameters.Add(
                    new SqlParameter("@MABANVE", SqlDbType.Int) { SourceColumn = "MABANVE" });
                pr2.SourceVersion = DataRowVersion.Original;
                adapter.Update(ds, "VeDaBan");
               
                return true;
            
            }
            catch { return false; }
        }
    }
}
