using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;


namespace QuanLyBanVeXe
{
    class Data_QLNhanVien
    {
        //Khai báo đối tượng kết nối
        SqlConnection conn = new SqlConnection("Data Source = DESKTOP-3CLCT2U\\SQLEXPRESS;Initial Catalog = QL_VEXEKHACH; Integrated Security = True");

        //1. tạo Dataset 
        DataSet ds_QLNV = new DataSet();
        SqlDataAdapter da_NhanVien;


        public Data_QLNhanVien()
        {
            loadNhanVien();

        }

        public void loadNhanVien()
        {
            //B1
            string CauLenh = "select * from NhanVien";
            //b2
            da_NhanVien = new SqlDataAdapter(CauLenh, conn);
            //b3
            da_NhanVien.Fill(ds_QLNV, "NhanVien");
            //b4 xét khóa chính
            DataColumn[] key = new DataColumn[1];
            key[0] = ds_QLNV.Tables["NhanVien"].Columns[0];
            //set Khóa chính
            ds_QLNV.Tables["NhanVien"].PrimaryKey = key;
        }

        public DataTable LoadDLNhanVien()
        {
            return ds_QLNV.Tables["NhanVien"];
        }

        public bool ThemNhanVien(string pmaNV, string ptenNV, string pphai, string pdiachi, string psdt, string ploaitaikhoang, string ptenDN, string pmatkhau)
        {
            try
            {
                //b1
                DataRow dr_row = ds_QLNV.Tables["NhanVien"].NewRow();
                //b2 gán dữ liệu cho row
                dr_row["MaNV"] = pmaNV;
                dr_row["TenNV"] = ptenNV;
                dr_row["Phai"] = pphai;
                dr_row["DiaChi"] = pdiachi;
                dr_row["SDT"] = psdt;
                dr_row["LoaiTaiKhoang"] = ploaitaikhoang;
                dr_row["TenDN"] = ptenDN;
                dr_row["MatKhau"] = pmatkhau;

                //b3 Thêm vào bảng khoa tại dataset
                ds_QLNV.Tables["NhanVien"].Rows.Add(dr_row);
                //b4 lưu vào csdl
                SqlDataAdapter da_NhanVien_buil = new SqlDataAdapter("select * from NhanVien", conn);
                SqlCommandBuilder builpNhanVien = new SqlCommandBuilder(da_NhanVien_buil);
                //update vào csdl
                da_NhanVien_buil.Update(ds_QLNV, "NhanVien");
                return true;
            }
            catch
            {
                return false;
            }
        }

        public int XoaNhanVien(string pmaNV)
        {
            try
            {
                //b1
                if (ktra_KhoaNgoai_BanVe(pmaNV) == false)
                {
                    DataRow dr_row = ds_QLNV.Tables["NhanVien"].Rows.Find(pmaNV);
                    dr_row.Delete();
                    //b4 lưu vào csdl
                    string str = "select * from NhanVien where MANV = " + pmaNV;
                    SqlDataAdapter da_NhanVien = new SqlDataAdapter(str, conn);
                    SqlCommandBuilder builpNhanVien = new SqlCommandBuilder(da_NhanVien);
                    //update vào csdl 
                    da_NhanVien.Update(ds_QLNV, "NhanVien");
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

        public bool ktra_KhoaNgoai_BanVe(string pMaNV)
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("select * from BANVE where MANV = " + pMaNV, conn);
            SqlDataReader re = cmd.ExecuteReader();

            while (re.Read())
            {
                return true;
                conn.Close();
            }
            conn.Close();
            return false;
        }




        public bool SuaNV(string pmaNV, string ptenNV, string pphai, string pdiachi, string psdt, string ploaitaikhoang, string ptenDN, string pmatkhau)
        {
            try
            {
                DataRow rowUpdate = ds_QLNV.Tables["NhanVien"].Rows.Find(pmaNV);
                rowUpdate["TenNV"] = ptenNV;
                rowUpdate["Phai"] = pphai;
                rowUpdate["DiaChi"] = pdiachi;
                rowUpdate["SDT"] = psdt;
                rowUpdate["LoaiTaiKhoang"] = ploaitaikhoang;
                rowUpdate["TenDN"] = ptenDN;
                rowUpdate["MatKhau"] = pmatkhau;

                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.UpdateCommand = new SqlCommand(@"UPDATE NHANVIEN SET TENNV=@TENNV, PHAI = @PHAI, DIACHI = @DIACHI, SDT = @SDT, 
                                            LOAITAIKHOANG = @LOAITAIKHOANG, TENDN = @TENDN, MATKHAU = @MATKHAU
                                             WHERE MANV = @MANV", conn);
                adapter.UpdateCommand.Parameters.Add("@TENNV", SqlDbType.NVarChar, 255, "TENNV");
                adapter.UpdateCommand.Parameters.Add("@PHAI", SqlDbType.NVarChar, 255, "PHAI");
                adapter.UpdateCommand.Parameters.Add("@DIACHI", SqlDbType.NVarChar, 255, "DIACHI");
                adapter.UpdateCommand.Parameters.Add("@SDT", SqlDbType.NVarChar, 255, "SDT");
                adapter.UpdateCommand.Parameters.Add("@LOAITAIKHOANG", SqlDbType.NVarChar, 255, "LOAITAIKHOANG");
                adapter.UpdateCommand.Parameters.Add("@TENDN", SqlDbType.NVarChar, 255, "TENDN");
                adapter.UpdateCommand.Parameters.Add("@MATKHAU", SqlDbType.NVarChar, 255, "MATKHAU");

                var pr2 = adapter.UpdateCommand.Parameters.Add(
                    new SqlParameter("@MANV", SqlDbType.Int) { SourceColumn = "MANV" });
                pr2.SourceVersion = DataRowVersion.Original;
                adapter.Update(ds_QLNV, "NhanVien");

                return true;

            }
            catch { return false; }
        }







    }
}
