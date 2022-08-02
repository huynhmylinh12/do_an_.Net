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
    public partial class ctrQLVeDaBan : UserControl
    {
        Data_QLVeDaBan dt = new Data_QLVeDaBan();
        public ctrQLVeDaBan()
        {
            InitializeComponent();
        }

        private void loadGridview()
        {
            dataGridView1.DataSource = dt.getVeDaBan();
            
        }

        private void loadCombobox()
        {
            dt.LoadXe();
            cobMaXe.DataSource =  dt.getXe();
            cobMaXe.DisplayMember = "MAXE";
            cobMaXe.ValueMember = "MAXE";

            dt.LoadTuyenXe();
            cobMaTuyenXe.DataSource = dt.getTuyenXe();
            cobMaTuyenXe.DisplayMember = "MATUYENXE";
            cobMaTuyenXe.ValueMember = "MATUYENXE";

            cobTTThanhToan.Items.Add("Đã thanh toán");
            cobTTThanhToan.Items.Add("Chưa thanh toán");
            cobTTThanhToan.SelectedIndex = 0;
        }

        private void ctrQLVe_Load(object sender, EventArgs e)
        {
            loadGridview();
            loadCombobox();
            btnXoaVe.Enabled = btnLuu.Enabled = btnHuy.Enabled = false;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            btnXoaVe.Enabled = true;
            txtMaVe.Text = dataGridView1.Rows[e.RowIndex].Cells["colMaBanVe"].Value.ToString();
            txtMaLoaiVe.Text = dataGridView1.Rows[e.RowIndex].Cells["colMaVe"].Value.ToString();
            txtMaNV.Text = dataGridView1.Rows[e.RowIndex].Cells["colMaNV"].Value.ToString();
            txtTenKh.Text = dataGridView1.Rows[e.RowIndex].Cells["colTenKH"].Value.ToString();
            txtSDT.Text = dataGridView1.Rows[e.RowIndex].Cells["colSDT"].Value.ToString();
            txtViTriGhe.Text = dataGridView1.Rows[e.RowIndex].Cells["colViTriGhe"].Value.ToString();
            txtSoLuong.Text = dataGridView1.Rows[e.RowIndex].Cells["colSoLuong"].Value.ToString();
            txtNgayDat.Text = dataGridView1.Rows[e.RowIndex].Cells["colNgayDat"].Value.ToString();
            txtNgayKH.Text = dataGridView1.Rows[e.RowIndex].Cells["colNgayDi"].Value.ToString();
            txtGioKhoiHanh.Text = dataGridView1.Rows[e.RowIndex].Cells["colGioKH"].Value.ToString();
            txtGhiChu.Text = dataGridView1.Rows[e.RowIndex].Cells["colGhiChu"].Value.ToString();

            string maxe = dataGridView1.Rows[e.RowIndex].Cells["colMaXe"].Value.ToString();
            for (int i = 0; i < cobMaXe.Items.Count; i++)
            {
                string x = cobMaXe.GetItemText(cobMaXe.Items[i]);
                if (maxe.Equals(x))
                {
                    cobMaXe.SelectedIndex = i;
                    break;
                }
            }

            string matuyenxe = dataGridView1.Rows[e.RowIndex].Cells["colMaTuyenXe"].Value.ToString();
            for (int i = 0; i < cobMaXe.Items.Count; i++)
            {
                string x = cobMaTuyenXe.GetItemText(cobMaTuyenXe.Items[i]);
                if (matuyenxe.Equals(x))
                {
                    cobMaTuyenXe.SelectedIndex = i;
                    break;
                }
            }

            string tt = dataGridView1.Rows[e.RowIndex].Cells["colTTThanhToan"].Value.ToString();
            
            if (tt.Equals("Đã thanh toán"))
            {
                cobMaTuyenXe.SelectedIndex = 0;
            }
            else
                cobMaTuyenXe.SelectedIndex = 1;
            
            
        }

        private void btnCapNhat_Click(object sender, EventArgs e)
        {
            btnLuu.Enabled = btnHuy.Enabled = true;
            txtNgayDat.Enabled = txtSDT.Enabled = txtTenKh.Enabled = txtViTriGhe.Enabled = cobTTThanhToan.Enabled = true;
        }

        private void btnXoaVe_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(String.Format("Bạn có muốn xóa vé có mã vé {0} không?", txtMaVe.Text), "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
            {
                if (dt.XoaVe(txtMaVe.Text) == true)
                    MessageBox.Show("Xóa vé thành công.");
                else
                    MessageBox.Show("Xóa vé thất bại");
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (txtMaNV.Text.Length == 0 || txtTenKh.Text.Length == 0 || txtSDT.Text.Length == 0 || txtSoLuong.Text.Length == 0 || txtViTriGhe.Text.Length == 0 || txtNgayDat.Text.Length == 0 || txtMaNV.Text.Length == 0)
            {
                MessageBox.Show("Vui lòng nhập đủ thông tin");
                return;
            }
            if (MessageBox.Show(String.Format("Bạn có muốn sửa vé có mã vé {0} không?", txtMaVe.Text), "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
            {
                if (dt.SuaVe(txtMaVe.Text, txtMaNV.Text, txtTenKh.Text, txtSDT.Text, txtViTriGhe.Text, txtSoLuong.Text, txtNgayDat.Text, cobTTThanhToan.GetItemText(cobTTThanhToan.SelectedItem), txtGhiChu.Text) == true)
                    MessageBox.Show("Sửa vé thành công.");
                else
                    MessageBox.Show("Sửa vé thất bại");
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có muốn đóng quản lý vé không?", "Đóng quản lý vé", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
            {
                this.Parent.Controls.Clear();
            }
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            frmThongKe frm = new frmThongKe();
            frm.Visible = true;
        }

        
    }
}
