using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;

namespace QuanLyBanVeXe
{
    public partial class ctrQLVe : UserControl
    {
        Data_QLVe xl = new Data_QLVe();
        public ctrQLVe()
        {
            InitializeComponent();
        }

        private void ctrQLVe_Load(object sender, EventArgs e)
        {
            xl.LoadTuyenXe();
            DataTable tb = new DataTable();
            tb = xl.getTuyenXe();
            foreach (DataRow row in tb.Rows)
            {
                cboMaTuyen.Items.Add(row["MATUYENXE"]);
            }

            xl.loadXe();
            DataTable tb2 = new DataTable();
            tb2 = xl.LoadDLXe();
            foreach (DataRow row in tb2.Rows)
            {
                cboMaXe.Items.Add(row["MAXE"]);
            }

            cboSoGheTrong.Items.Add(22);
            cboSoGheTrong.Items.Add(44);
            cboSoGheTrong.Items.Add(36);


            dataGridView1.DataSource = xl.LoadDLVe();
        }

        

        private void btnCapNhat_Click(object sender, EventArgs e)
        {
            txtMaVe.Enabled = cboMaTuyen.Enabled = cboMaXe.Enabled = dateTimePicker1.Enabled = txtGio.Enabled = true;
            btnThem.Enabled = btnSua.Enabled = btnXoa.Enabled = btnHuy.Enabled = true;
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            btnThem.Enabled = btnSua.Enabled = btnXoa.Enabled = btnHuy.Enabled = false;
            txtMaVe.Enabled = cboMaTuyen.Enabled = cboMaXe.Enabled = dateTimePicker1.Enabled = txtGio.Enabled = false;

        }

        private void btnThem_Click(object sender, EventArgs e)
        {
           // txtMaVe.Enabled = cboMaTuyen.Enabled = cboMaXe.Enabled = txtNgayDi.Enabled = txtGio.Enabled = txtSoghetrong.Enabled = false;
            if (txtMaVe.Text.Length == 0 && cboMaTuyen.Text.Length == 0 && cboMaXe.Text.Length == 0 && dateTimePicker1.Text.Length == 0 && txtGio.Text.Length == 0)
            {
                MessageBox.Show("Vui lòng nhập đủ thông tin");
                return;
            }

            if (xl.ThemVe(txtMaVe.Text, cboMaTuyen.Text, cboMaXe.Text, DateTime.ParseExact(dateTimePicker1.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("MM/dd/yyyy", CultureInfo.InvariantCulture), txtGio.Text, int.Parse(cboSoGheTrong.SelectedItem.ToString())) == true)
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
                int re = xl.XoaVe(txtMaVe.Text);
                if (re == 1)
                {
                    MessageBox.Show("Thành Công");
                }
                else if (re == 0)
                {
                    MessageBox.Show("Thất bại");
                }
                else
                    MessageBox.Show("Vé tồn tại trong bảng Bán Vé");
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (xl.SuaVE(txtMaVe.Text, cboMaTuyen.Text, cboMaXe.Text, DateTime.ParseExact(dateTimePicker1.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("MM/dd/yyyy", CultureInfo.InvariantCulture), txtGio.Text, int.Parse(cboSoGheTrong.SelectedItem.ToString())) == true)
            {
                MessageBox.Show("Thành Công");
            }
            else
            {
                MessageBox.Show("Thất bại");
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtMaVe.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            txtGio.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();

            string ghe = dataGridView1.CurrentRow.Cells[5].Value.ToString();
            for (int i = 0; i < cboSoGheTrong.Items.Count; i++)
            {
                if (ghe.Equals(cboSoGheTrong.Items[i].ToString()))
                {
                    cboSoGheTrong.SelectedIndex = i;
                    break;
                }
            }

            string maxe = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            for (int i = 0; i < cboMaXe.Items.Count; i++)
            {
                if (maxe.Equals(cboMaXe.Items[i].ToString()))
                {
                    cboMaXe.SelectedIndex = i;
                    break;
                }
            }

            string matuyenxe = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            for (int i = 0; i < cboMaTuyen.Items.Count; i++)
            {
                if (matuyenxe.Equals(cboMaTuyen.Items[i].ToString()))
                {
                    cboMaTuyen.SelectedIndex = i;
                    break;
                }
            }
        }

        
    }
}
