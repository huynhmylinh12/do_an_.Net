using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyBanVeXe
{
    public partial class frmMain : Form
    {
        public string maNV {get; set;}
        public frmMain()
        {
            InitializeComponent();
            
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có muốn thoát chương trình không?", "Thoát chương trình", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.Close();
            }
        }

        public void showControl(Control control)
        {
            pnMain.Controls.Clear();
            control.Size = pnMain.Size;
            control.Dock = DockStyle.Fill;
            control.Focus();
            pnMain.Controls.Add(control);
        }

        private void btnQLVe_Click(object sender, EventArgs e)
        {
            ctrQLVeDaBan ctr = new ctrQLVeDaBan();
            showControl(ctr);
        }

        private void btnNhanVien_Click(object sender, EventArgs e)
        {
            ctrNhanVien ctr = new ctrNhanVien();
            showControl(ctr);
        }

        private void btnQLTuyenXe_Click(object sender, EventArgs e)
        {
            ctrQLTuyenXe ctr = new ctrQLTuyenXe();
            showControl(ctr);
        }

        private void btnQLXe_Click(object sender, EventArgs e)
        {
            ctrQLXe ctr = new ctrQLXe();
            showControl(ctr);
        }

        private void btnDangXuat_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Bạn có muốn thoát chương trình không?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {   
                frmDangNhap frm = new frmDangNhap();
                frm.Visible = true;
                this.Visible = false;
            }
        }

        private void btnBanVe_Click(object sender, EventArgs e)
        {
            ctrQLBanVe ctr = new ctrQLBanVe();
            showControl(ctr);
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            lbTaiKhoan.Text = NhanVien.TenNV;
            maNV = NhanVien.MaNV;
            if (NhanVien.Loaitaikhoang.Equals("Nhân viên"))
            {
                btnNhanVien.Enabled = btnQLTaoVe.Enabled = btnQLXe.Enabled = btnQLTuyenXe.Enabled = false;
            }
            timer1.Start();
        }

        private void btnThongKe_Click(object sender, EventArgs e)
        {
            frmThongKe frm = new frmThongKe();
            frm.Visible = true;
        }

        private void btnQLTaoVe_Click(object sender, EventArgs e)
        {
            ctrQLVe ctr = new ctrQLVe();
            showControl(ctr);
        }

        

        private void timer1_Tick(object sender, EventArgs e)
        {
            label1.Text = DateTime.Now.ToString("hh:mm:ss tt");
        }



        


        


    }
}
