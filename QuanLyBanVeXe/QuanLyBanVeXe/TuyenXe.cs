using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormBanVeXeKhach
{
    class TuyenXe
    {
        string maTuyenXe, tenTuyenXe, diemDi, diemDen, chitiettuyen;
        double giave;

        public TuyenXe(string maTuyenXe, string tenTuyenXe, string diemDi, string diemDen, string chitiettuyen, double giave)
        {
            this.maTuyenXe = maTuyenXe;
            this.tenTuyenXe = tenTuyenXe;
            this.diemDi = diemDi;
            this.diemDen = diemDen;
            this.chitiettuyen = chitiettuyen;
            this.giave = giave;
        }
        public TuyenXe()
        {

        }
        public string Chitiettuyen
        {
            get { return chitiettuyen; }
            set { chitiettuyen = value; }
        }

        public string DiemDen
        {
            get { return diemDen; }
            set { diemDen = value; }
        }

        public string DiemDi
        {
            get { return diemDi; }
            set { diemDi = value; }
        }

        public string TenTuyenXe
        {
            get { return tenTuyenXe; }
            set { tenTuyenXe = value; }
        }

        public string MaTuyenXe
        {
            get { return maTuyenXe; }
            set { maTuyenXe = value; }
        }
       

        public double Giave
        {
            get { return giave; }
            set { giave = value; }
        }


    }
}
