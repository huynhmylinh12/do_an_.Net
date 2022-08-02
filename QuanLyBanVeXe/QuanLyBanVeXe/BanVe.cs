using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormBanVeXeKhach
{
    class BanVe
    {
        int maBanVe, soLuong;
        string tenKH, sdt, vitrighe, trengthaithanhtoan, ghichu, maVe, ngaydat, maNV;
        double thanhtien;

        public BanVe(int maBanVe, int soLuong, string tenKH, string sdt, string vitrighe, string trengthaithanhtoan, string ghichu, string maVe, string ngaydat, string maNV, double thanhtien)
        {
            this.maBanVe = maBanVe;
            this.soLuong = soLuong;
            this.tenKH = tenKH;
            this.sdt = sdt;
            this.vitrighe = vitrighe;
            this.trengthaithanhtoan = trengthaithanhtoan;
            this.ghichu = ghichu;
            this.maVe = maVe;
            this.ngaydat = ngaydat;
            this.maNV = maNV;
            this.thanhtien = thanhtien;
        }

        public BanVe()
        {

        }
        public double Thanhtien
        {
            get { return thanhtien; }
            set { thanhtien = value; }
        }

        public int SoLuong
        {
            get { return soLuong; }
            set { soLuong = value; }
        }

        public int MaBanVe
        {
            get { return maBanVe; }
            set { maBanVe = value; }
        }

        public string MaNV
        {
            get { return maNV; }
            set { maNV = value; }
        }

        public string Ngaydat
        {
            get { return ngaydat; }
            set { ngaydat = value; }
        }

        public string MaVe
        {
            get { return maVe; }
            set { maVe = value; }
        }

        public string Ghichu
        {
            get { return ghichu; }
            set { ghichu = value; }
        }

        public string Trengthaithanhtoan
        {
            get { return trengthaithanhtoan; }
            set { trengthaithanhtoan = value; }
        }

        public string Vitrighe
        {
            get { return vitrighe; }
            set { vitrighe = value; }
        }

        public string Sdt
        {
            get { return sdt; }
            set { sdt = value; }
        }

        public string TenKH
        {
            get { return tenKH; }
            set { tenKH = value; }
        }
        

        
    }
}
