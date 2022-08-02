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
    public partial class ctrQLTuyenXe : UserControl
    {
        Data_QLTuyenXe xl = new Data_QLTuyenXe();
        public ctrQLTuyenXe()
        {
            InitializeComponent();
        }

        private void ctrQLTuyenXe_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = xl.LoadDLTuyenXe();
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            txtMaTuyen.Enabled = txtTenTuyen.Enabled = txtDiemDi.Enabled = txtDiemDen.Enabled = txtChiTiet.Enabled = txtGiaVe.Enabled = false;

            txtMaTuyen.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            txtTenTuyen.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            txtDiemDi.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            txtDiemDen.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            txtChiTiet.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            txtGiaVe.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
            
        }

        private void btnThemTuyenXe_Click(object sender, EventArgs e)
        {
            //kiểm tra rỗng

            if (txtMaTuyen.Text.Length == 0 && txtTenTuyen.Text.Length == 0 && txtDiemDi.Text.Length == 0 && txtDiemDen.Text.Length == 0 && txtChiTiet.Text.Length == 0 && txtGiaVe.Text.Length == 0)
            {
                MessageBox.Show("Vui lòng nhập đủ thông tin");
                return;
            }

            if (xl.ThemTuyenXe(txtMaTuyen.Text, txtTenTuyen.Text, txtDiemDi.Text, txtDiemDen.Text, txtChiTiet.Text, double.Parse(txtGiaVe.Text)) == true)
            {
                MessageBox.Show("Thành Công");
            }
            else
            {
                MessageBox.Show("Thất bại");
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (xl.SuaTuyenXe(txtMaTuyen.Text, txtTenTuyen.Text, txtDiemDi.Text, txtDiemDen.Text, txtChiTiet.Text, double.Parse(txtGiaVe.Text)) == true)
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
            txtMaTuyen.Enabled = txtTenTuyen.Enabled = txtDiemDi.Enabled = txtDiemDen.Enabled = txtChiTiet.Enabled = txtGiaVe.Enabled = false;
            btnThemTuyenXe.Enabled = btnXoaTuyenXe.Enabled = btnSua.Enabled = btnHuy.Enabled = false;
        }

        private void btnXoaTuyenXe_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn muốn xóa", "Thông Báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
            {
                int re = xl.XoaTuyenXe(txtMaTuyen.Text);
                if (re == 1)
                {
                    MessageBox.Show("Thành Công");
                }
                else if (re == 0)
                {
                    MessageBox.Show("Thất bại");
                }
                else
                    MessageBox.Show("Tuyến Xe tồn tại trong bảng Vé");
            }
        }

        private void btnCapNhat_Click(object sender, EventArgs e)
        {
            txtMaTuyen.Enabled = txtTenTuyen.Enabled = txtDiemDi.Enabled = txtDiemDen.Enabled = txtChiTiet.Enabled = txtGiaVe.Enabled = true;
            btnThemTuyenXe.Enabled = btnXoaTuyenXe.Enabled = btnSua.Enabled = btnHuy.Enabled = true;
        }
    }
}
