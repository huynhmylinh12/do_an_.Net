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
    public partial class frmDangNhap : Form
    {
        public string tenNV { get; set; }
        public string maNV { get; set; }
        Data_DangNhap dt = new Data_DangNhap();
        public frmDangNhap()
        {
            InitializeComponent();
            
        }

        

        private void frmDangNhap_Load(object sender, EventArgs e)
        {
            
        }

        private void btnThoat_MouseMove(object sender, MouseEventArgs e)
        {
            btnThoat.BackColor = Color.Red;
            btnThoat.ForeColor = Color.White;
        }

        private void btnThoat_MouseLeave(object sender, EventArgs e)
        {
            btnThoat.BackColor = SystemColors.ButtonFace;
            btnThoat.ForeColor = Color.Black;
        }

        private void btnDangNhap_MouseMove(object sender, MouseEventArgs e)
        {
            btnDangNhap.BackColor = Color.Aqua;
        }

        private void btnDangNhap_MouseLeave(object sender, EventArgs e)
        {
            btnDangNhap.BackColor = SystemColors.ButtonHighlight;
        }

        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            if (dt.ktra_DangNhap(txtTaiKhoan.Text, txtMatKhau.Text))
            {
                //Lưu nhân viên
                DataRow nv = dt.getNhanVien_DaDangNhap(txtTaiKhoan.Text, txtMatKhau.Text);
                NhanVien.MaNV = nv["MANV"].ToString();
                NhanVien.TenNV = nv["TENNV"].ToString();
                NhanVien.Loaitaikhoang = nv["LOAITAIKHOANG"].ToString();
                //Mở frm Main
                frmMain frm = new frmMain();
                frm.Visible = true;
                this.Visible = false;
            }
            else
            {
                MessageBox.Show("Sai mật khẩu hoặc tên đăng nhập. Mời bạn nhập lại.");
            }
        }
    }
}
