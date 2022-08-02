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
    public partial class ctrQLXe : UserControl
    {
        Data_QLXe xl = new Data_QLXe();
        public ctrQLXe()
        {
            InitializeComponent();
        }


        private void ctrQLXe_Load(object sender, EventArgs e)
        {   
            cboSoGhe.Items.Add(22);
            cboSoGhe.Items.Add(44);
            cboSoGhe.Items.Add(36);
            dataGridView1.DataSource = xl.LoadDLXe();
            
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            txtMa.Enabled = txtLoaiXe.Enabled = cboSoGhe.Enabled = false;
            txtMa.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            txtLoaiXe.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            int soghe = int.Parse(dataGridView1.CurrentRow.Cells[2].Value.ToString());
            if (soghe == 22)
                cboSoGhe.SelectedIndex = 0;
            else if (soghe == 44)
                cboSoGhe.SelectedIndex = 1;
            else
                cboSoGhe.SelectedIndex = 2;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
           // txtMa.Enabled = txtLoaiXe.Enabled = cboSoGhe.Enabled = true;
            //kiểm tra rỗng

            if (txtMa.Text.Length == 0 && txtLoaiXe.Text.Length == 0 && cboSoGhe.Text.Length == 0)
            {
                MessageBox.Show("Vui lòng nhập đủ thông tin");
                return;
            }

            if (xl.ThemXe(txtMa.Text, txtLoaiXe.Text, int.Parse(cboSoGhe.Text)) == true)
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
                int re = xl.XoaXe(txtMa.Text);
                if (re == 1)
                {
                    MessageBox.Show("Thành Công");
                }
                else if (re == 0)
                {
                    MessageBox.Show("Thất bại");
                }
                else
                    MessageBox.Show("Xe tồn tại trong bảng Vé");
            }
        }

        private void btnCapNhat_Click(object sender, EventArgs e)
        {
            txtMa.Enabled = txtLoaiXe.Enabled = cboSoGhe.Enabled = true;
            btnThem.Enabled = btnXoa.Enabled = btnSua.Enabled = btnHuy.Enabled = true;
            
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (xl.SuaXe(txtMa.Text, txtLoaiXe.Text, int.Parse(cboSoGhe.Text)) == true)
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
            txtMa.Enabled = txtLoaiXe.Enabled = cboSoGhe.Enabled = false;
            btnThem.Enabled = btnXoa.Enabled = btnSua.Enabled = btnHuy.Enabled = false;
        }
    }
}
