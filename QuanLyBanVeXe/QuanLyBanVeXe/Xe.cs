using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormBanVeXeKhach
{
    class Xe
    {
        string maXe, loaiXe;
        int soghetang1, soghetang2;

        public Xe(string maXe, string loaiXe, int soghetang1, int soghetang2)
        {
             this.maXe = maXe;
             this.loaiXe = loaiXe;
             this.soghetang1 = soghetang1;
             this.soghetang2 = soghetang2;

        }

        public Xe()
        {

        }
        public string LoaiXe
        {
            get { return loaiXe; }
            set { loaiXe = value; }
        }

        public string MaXe
        {
            get { return maXe; }
            set { maXe = value; }
        }
        

        public int Soghetang2
        {
            get { return soghetang2; }
            set { soghetang2 = value; }
        }

        public int Soghetang1
        {
            get { return soghetang1; }
            set { soghetang1 = value; }
        }
        
    }
}
