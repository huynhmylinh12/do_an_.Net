using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyBanVeXe
{
    public class NhanVien
    {
        static string maNV, tenNV, phai, diachi, sdt, loaitaikhoang, tenDN, matkhau;
        static double luong;

        
        public NhanVien()
        {
            
        }
        public NhanVien(string maNV, string tenNV, string phai, string diachi, string sdt, string loaitaikhoang, string tenDN, string matkhau, double luong)
        {
            MaNV = maNV;
            TenNV = tenNV;
            Phai = phai;
            Diachi = diachi;
            Sdt = sdt;
            Loaitaikhoang = loaitaikhoang;
            TenDN = tenDN;
            Matkhau = matkhau;
            Luong = luong;

        }

        public static string MaNV
        {
            get { return maNV; }
            set { maNV = value; }
        }
        public static double Luong
        {
            get { return luong; }
            set { luong = value; }
        }
        public static string Matkhau
        {
            get { return matkhau; }
            set { matkhau = value; }
        }

        public static string TenDN
        {
            get { return tenDN; }
            set { tenDN = value; }
        }

        public static string Loaitaikhoang
        {
            get { return loaitaikhoang; }
            set { loaitaikhoang = value; }
        }

        public static string Sdt
        {
            get { return sdt; }
            set { sdt = value; }
        }

        public static string Diachi
        {
            get { return diachi; }
            set { diachi = value; }
        }

        public static string Phai
        {
            get { return phai; }
            set { phai = value; }
        }

        public static string TenNV
        {
            get { return tenNV; }
            set { tenNV = value; }
        }
    }
}
