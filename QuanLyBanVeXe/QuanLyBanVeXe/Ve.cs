using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormBanVeXeKhach
{
    class Ve
    {
        string maVe, maTuyenXe, maXe, ngayDi, gio;

        public Ve(string maVe, string maTuyenXe, string maXe, string ngayDi, string gio)
        {
            this.maVe = maVe;
            this.maTuyenXe = maTuyenXe;
            this.maXe = maXe;
            this.ngayDi = ngayDi;
            this.gio = gio;
        }

        public Ve()
        {

        }

        public string Gio
        {
            get { return gio; }
            set { gio = value; }
        }

        public string NgayDi
        {
            get { return ngayDi; }
            set { ngayDi = value; }
        }

        public string MaXe
        {
            get { return maXe; }
            set { maXe = value; }
        }

        public string MaTuyenXe
        {
            get { return maTuyenXe; }
            set { maTuyenXe = value; }
        }

        public string MaVe
        {
            get { return maVe; }
            set { maVe = value; }
        }
        
    }
}
