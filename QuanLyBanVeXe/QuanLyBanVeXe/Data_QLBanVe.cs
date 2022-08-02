using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace QuanLyBanVeXe
{
    class Data_QLBanVe
    {
        SqlConnection sqlconn = new SqlConnection("Data Source=DESKTOP-3CLCT2U\\SQLEXPRESS; Initial Catalog= QL_VeXeKhach; User ID=sa; Password =123456");

        SqlDataAdapter da;
        DataSet ds = new DataSet();
        int maBanVe = 0;
        public Data_QLBanVe()
        {
            LoadBanVe();
        }

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
            maBanVe = int.Parse(ds.Tables["BanVe"].Rows[ds.Tables["BanVe"].Rows.Count - 1]["MABANVE"].ToString());
        }

        public DataTable getBanVe()
        {
            return ds.Tables["BanVe"];
        }

        //Ve Theo TuyenXe_NgayDi
        public void LoadVeTheoTuyenXe_NgayDi(string pMaTuyenXe, string pNgayDi)
        {
            SqlCommand cmd = new SqlCommand("SET DATEFORMAT DMY", sqlconn);
            sqlconn.Open();
            cmd.ExecuteNonQuery();
            sqlconn.Close();
            string CauLenh = "select * from VE where MATUYENXE = " + pMaTuyenXe + " and NGAYDI = N'"+ pNgayDi +"'";

            da = new SqlDataAdapter(CauLenh, sqlconn);

            da.Fill(ds, "Ve_TuyenXe_ngayDi");
            //B4 set khóa chính
            DataColumn[] key = new DataColumn[1];
            key[0] = ds.Tables["Ve_TuyenXe_ngayDi"].Columns[0];
            //set khóa chính
            ds.Tables["Ve_TuyenXe_ngayDi"].PrimaryKey = key;
        }

        public DataTable getVeTheoTuyenXe_NgayDi()
        {
            return ds.Tables["Ve_TuyenXe_ngayDi"];
        }

        //Ve Theo TuyenXe_NgayDi_Gio
        public void LoadVeTheoTuyenXe_NgayDi_Gio(string pMaTuyenXe, string pNgayDi, string pGio)
        {
            string CauLenh = "select * from VE where MATUYENXE = " + pMaTuyenXe + "and NGAYDI = N'" + pNgayDi + "' and GIO = '" + pGio+"'";

            da = new SqlDataAdapter(CauLenh, sqlconn);

            da.Fill(ds, "Ve_TuyenXe_ngayDi_Gio");
            //B4 set khóa chính
            DataColumn[] key = new DataColumn[1];
            key[0] = ds.Tables["Ve_TuyenXe_ngayDi_Gio"].Columns[0];
            //set khóa chính
            ds.Tables["Ve_TuyenXe_ngayDi_Gio"].PrimaryKey = key;
        }

        public DataTable getVeTheoTuyenXe_NgayDi_Gio()
        {
            return ds.Tables["Ve_TuyenXe_ngayDi_Gio"];
        }

        

        public void LoadTuyenXe()
        {

            string CauLenh = "select * from TUYENXE";

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

        public int getSoLuongGheTrong(int pMaVe)
        {
            try
            { 
                string CauLenh = "select SOGHETRONG from VE where MAVE = " + pMaVe;

                SqlCommand cmd = new SqlCommand(CauLenh,sqlconn);
                sqlconn.Open();
                int sl =  int.Parse(cmd.ExecuteScalar().ToString());
                sqlconn.Close();
                return sl;
            }
            catch (Exception e) { return 0; }
        }

        public int getMaVe(string pMaTuyenXe, string pNgayDi, int pMaXe)
        {
            try
            {
                string CauLenh = "select MAVE from VE where MATUYENXE = " + pMaTuyenXe + " and NGAYDI = N'" + pNgayDi + "' and MAXE = "+ pMaXe;

                SqlCommand cmd = new SqlCommand(CauLenh, sqlconn);
                sqlconn.Open();
                int ma = int.Parse(cmd.ExecuteScalar().ToString());
                sqlconn.Close();
                return ma;
            }
            catch (Exception e) { return 0; }
        }

        public int getMaVe(string pMaTuyenXe, string pNgayDi, string pGio)
        {
            try
            {
                string CauLenh = "select MAVE from VE where MATUYENXE = " + pMaTuyenXe + " and NGAYDI = N'"+pNgayDi+"' and GIO = '" + pGio + "'";

                DataTable tbl = new DataTable();
                da = new SqlDataAdapter(CauLenh, sqlconn);
                da.Fill(tbl);
                int ma = int.Parse(tbl.Rows[0][0].ToString());
                
                return ma;
            }
            catch (Exception e) { Console.Write(e.Message); return 0; }
        }

        public DataTable getXe(int pMaVe)
        {
            try
            {
                string CauLenh = "select * from VE, XE where VE.MAXE = XE.MAXE AND MAVE = "+pMaVe;

                DataTable tbl = new DataTable("Xe");
                da = new SqlDataAdapter(CauLenh,sqlconn);
                da.Fill(tbl);
                return tbl;
            }
            catch (Exception e) { return null; }
        }

        public string getGio(int pMaVe)
        {
            try
            {
                string CauLenh = "select GIO from VE where MAVE = " + pMaVe;

                SqlCommand cmd = new SqlCommand(CauLenh, sqlconn);
                sqlconn.Open();
                string gio = cmd.ExecuteScalar().ToString();
                sqlconn.Close();
                return gio;
            }
            catch (Exception e) { return null; }
        }

        public DataTable getVe_TheoTuyenXe_NgayDi_Xe(string pMaTuyenXe, string pNgayDi, string pMaXe)
        {
            try
            {
                string CauLenh = "select * from VE where MATUYENXE = " + pMaTuyenXe + " and NGAYDI = N'" + pNgayDi + "' and MAXE = " + pMaXe;

                da = new SqlDataAdapter(CauLenh, sqlconn);
                DataTable tbl = new DataTable();
                da.Fill(tbl);

                return tbl;
            }
            catch (Exception e) { return null; }
        }

        public DataTable getXe_theoMa(string pMa)
        {
            string CauLenh = "select * from XE where MAXE = "+ pMa;

            da = new SqlDataAdapter(CauLenh, sqlconn);
            DataTable tbl = new DataTable();
            da.Fill(tbl);
            
            return tbl;
        }

        public string getlstViTriGhe_DaBan(int pMaVe)
        {
            string arr = "";
            string CauLenh = "select VITRIGHE from BanVe where MAVE = " + pMaVe;

            da = new SqlDataAdapter(CauLenh, sqlconn);
            DataTable tbl = new DataTable();
            da.Fill(tbl);

            foreach (DataRow row in tbl.Rows)
            {
                arr += row["VITRIGHE"].ToString();
                arr += ",";
            }
            return arr;
        }

        public DataTable getBanVe_TheoNgayDat(string pNgay)
        {
            //MABANVE, TENKH, SDT, TRANGTHAITHANHTOAN, SOLUONG, THANHTIEN, NGAYDAT, MANV
            string CauLenh = "select * from BanVe where NGAYDAT = N'" + pNgay + "'";

            da = new SqlDataAdapter(CauLenh, sqlconn);
            da.Fill(ds, "BanVe_TheoNgayDat");

            return ds.Tables["BanVe_TheoNgayDat"];
        }

        public string getGiaVe_TheoMaTuyen(int pMaTuyen)
        {
            string CauLenh = "select GIAVE from TUYENXE where MATUYENXE = " + pMaTuyen;
            SqlCommand cmd = new SqlCommand(CauLenh, sqlconn);
            sqlconn.Open();
            string gia = cmd.ExecuteScalar().ToString();
            sqlconn.Close();
            return gia;
        }
        public bool ThemBanVe(string ptenKH, string pSDT, string pViTriGhe, string pSoLuong, string pTTThanhToan, string pGhiChu, int pMaVe, string pNgayDat, string pMaNV)
        {
            try
            {
                //b1
                DataRow dr_row = ds.Tables["BanVe"].NewRow();
                //b2 gán dữ liệu cho row
                dr_row["MABANVE"] = maBanVe + 1;
                dr_row["TENKH"] = ptenKH;
                dr_row["SDT"] = pSDT;
                dr_row["VITRIGHE"] = pViTriGhe;
                dr_row["SOLUONG"] = pSoLuong;
                dr_row["TRANGTHAITHANHTOAN"] = pTTThanhToan;
                dr_row["GHICHU"] = pGhiChu;
                dr_row["MAVE"] = pMaVe;
                dr_row["NGAYDAT"] = pNgayDat;
                dr_row["THANHTIEN"] = 0;
                dr_row["MANV"] = pMaNV;
                //b3 Thêm vào bảng khoa tại dataset
                ds.Tables["BanVe"].Rows.Add(dr_row);

                //b4 lưu vào csdl
                SqlDataAdapter da_build = new SqlDataAdapter("select * from BanVe", sqlconn);
                SqlCommandBuilder builpNhanVien = new SqlCommandBuilder(da_build);
                //update vào csdl
                da_build.Update(ds, "BanVe");
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
