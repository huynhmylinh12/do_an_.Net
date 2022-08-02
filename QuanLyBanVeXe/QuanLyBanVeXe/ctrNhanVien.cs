using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyBanVeXe
{
    public partial class ctrNhanVien : UserControl
    {
        Data_QLNhanVien xl = new Data_QLNhanVien();
        public ctrNhanVien()
        {
            InitializeComponent();
        }

        private void ctrNhanVien_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = xl.LoadDLNhanVien();
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            txtMaNV.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            txtHoTen.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            txtGioiTinh.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            txtDiaChi.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            txtSDT.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            txtLoaiNV.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
            txtTenDN.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();
            txtMatKhau.Text = dataGridView1.CurrentRow.Cells[7].Value.ToString();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            txtMaNV.Enabled = txtHoTen.Enabled = txtGioiTinh.Enabled = txtDiaChi.Enabled = txtSDT.Enabled = txtLoaiNV.Enabled = txtTenDN.Enabled = txtMatKhau.Enabled = false;
            //kiểm tra rỗng

            if (txtMaNV.Text.Length == 0 && txtHoTen.Text.Length == 0 && txtSDT.Text.Length == 0 && txtTenDN.Text.Length == 0 && txtMatKhau.Text.Length == 0 && txtLoaiNV.Text.Length == 0 && txtDiaChi.Text.Length == 0 && txtGioiTinh.Text.Length == 0)
            {
                MessageBox.Show("Vui lòng nhập đủ thông tin");
                return;
            }

            if (xl.ThemNhanVien(txtMaNV.Text, txtHoTen.Text, txtGioiTinh.Text, txtDiaChi.Text, txtSDT.Text, txtLoaiNV.Text, txtTenDN.Text, txtMatKhau.Text) == true)
            {
                MessageBox.Show("Thành Công");
            }
            else
            {
                MessageBox.Show("Thất bại");
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn muốn xóa", "Thông Báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
            {
                int re = xl.XoaNhanVien(txtMaNV.Text);
                if (re == 1)
                {
                    MessageBox.Show("Thành Công");
                }
                else if(re == 0)
                {
                    MessageBox.Show("Thất bại");
                }
                else
                    MessageBox.Show("Nhân Viên tồn tại trong bảng Bán Vé");
            }
        }

        private void btnCapNhat_Click(object sender, EventArgs e)
        {
            txtMaNV.Enabled = txtHoTen.Enabled = txtGioiTinh.Enabled = txtDiaChi.Enabled = txtSDT.Enabled = txtLoaiNV.Enabled = txtTenDN.Enabled = txtMatKhau.Enabled = true;
            btnThem.Enabled = btnXoa.Enabled = btnSua.Enabled = btnHuy.Enabled = true;

        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (xl.SuaNV(txtMaNV.Text, txtHoTen.Text, txtGioiTinh.Text, txtDiaChi.Text, txtSDT.Text, txtLoaiNV.Text, txtTenDN.Text, txtMatKhau.Text) == true)
            {
                MessageBox.Show("Thành Công");
            }
            else
            {
                MessageBox.Show("Thất bại");
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            txtMaNV.Enabled = txtHoTen.Enabled = txtGioiTinh.Enabled = txtDiaChi.Enabled = txtSDT.Enabled = txtLoaiNV.Enabled = txtTenDN.Enabled = txtMatKhau.Enabled = false;
            btnThem.Enabled = btnXoa.Enabled = btnSua.Enabled = btnHuy.Enabled = false;
        }

        
        

        
       
       
        
    }
}
