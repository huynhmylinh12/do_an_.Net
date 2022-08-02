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
    public partial class ctrQLBanVe : UserControl
    {
        Data_QLBanVe dt = new Data_QLBanVe();
        DataTable tblVeTheoTuyenXe_NgayDi = new DataTable();
        int maVe;
        Button[] listBtnTang1;
        Button[] listBtnTang2;
        int slGhe_DaChon = 0;
        List<string> lstGhe_DaChon = new List<string>();
        #region method
        public ctrQLBanVe()
        {
            InitializeComponent();
        }

        private void loadComcoBox()
        {
            //Load tuyến xe
            dt.LoadTuyenXe();
            cobTuyenXe.DataSource = dt.getTuyenXe();
            cobTuyenXe.DisplayMember = "TENTUYENXE";
            cobTuyenXe.ValueMember = "MATUYENXE";

            //load trạng thái thanh toán
            cobTTThanhToan.Items.Add("Đã thanh toán");
            cobTTThanhToan.Items.Add("Chưa thanh toán");
        }

        private void loadGhe()
        {
            flpTang1.Controls.Clear();
            flpTang2.Controls.Clear();
            DataTable tblXe = dt.getXe_theoMa(cobXe.SelectedItem.ToString()); 
            int soluong = int.Parse(tblXe.Rows[0]["SOGHE"].ToString());
            int soGheTang1 = 0;
            int soGheTang2 = 0;
            List<string> viTriGhe_DaBan = new List<string>();
            string strGhe = dt.getlstViTriGhe_DaBan(maVe);
            viTriGhe_DaBan = xuLyViTriGhe(strGhe);

            if (soluong % 2 == 0)
            {
                soGheTang1 = soGheTang2 = soluong / 2;
            }
            else
            {
                soGheTang1 = soluong / 2 + 1;
                soGheTang2 = soluong / 2;
            }
            listBtnTang1 = new Button[soGheTang1];
            listBtnTang2 = new Button[soGheTang2];
            
            for (int i = 0; i < listBtnTang1.Length; i++)
            {
                string tenGhe1 = i + 1 + "A";

                Button btn1 = new Button() { Width = 60, Height = 60 };
                btn1.Name = "btn" + i + 1;
                btn1.Text = tenGhe1;
                btn1.BackColor = Color.RoyalBlue;
                foreach(string item in viTriGhe_DaBan)
                {
                    if (btn1.Text.Equals(item))
                    {
                        btn1.BackColor = Color.Violet;
                        btn1.Enabled = false;
                    }
                }
                btn1.Click += btn1_Click;
                btn1.Tag = i;
                listBtnTang1[i] = btn1;
                flpTang1.Controls.Add(btn1);
            }

            for (int i = 0; i < listBtnTang2.Length; i++)
            {
                string tenGhe2 = i + 1 + "B";

                Button btn2 = new Button() { Width = 60, Height = 60 };
                btn2.Name = "btn" + i + 1;
                btn2.Text = tenGhe2;
                btn2.BackColor = Color.RoyalBlue;
                foreach (string item in viTriGhe_DaBan)
                {
                    if (btn2.Text.Equals(item))
                    {
                        btn2.BackColor = Color.Violet;
                        btn2.Enabled = false;
                    }
                }
                btn2.Click += btn2_Click;
                btn2.Tag = i;
                listBtnTang2[i] = btn2;
                flpTang2.Controls.Add(btn2);
            }
        }

        private List<string> xuLyViTriGhe(string str)
        {
            List<string> lst = new List<string>();
            string ghe = "";
            for (int i = 0; i < str.Length; i++)
            {
                if (str[i] != ' ' && str[i] != ',')
                {
                    ghe += str[i];
                }
                else if(str[i] == ',')
                {
                    lst.Add(ghe);
                    ghe = "";
                }
                
            }
            return lst;
        }

        #endregion

        #region Envent
        private void ctrQLBanVe_Load(object sender, EventArgs e)
        {
            loadComcoBox();
        }

        int flag = 0;
        private void cobXe_SelectedIndexChanged(object sender, EventArgs e)
        {
            maVe = dt.getMaVe(cobTuyenXe.SelectedValue.ToString(), DateTime.ParseExact(dateTimePicker1.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("MM/dd/yyyy", CultureInfo.InvariantCulture), int.Parse(cobXe.SelectedItem.ToString()));
            txtSoGheTrong.Text = dt.getSoLuongGheTrong(maVe).ToString();
            loadGhe();
            slGhe_DaChon = 0;
            if (flag == 0)
            {
                string Gio = dt.getGio(maVe);
                for (int i = 0; i < cobGioKH.Items.Count; i++)
                {
                    if (cobGioKH.Items[i].ToString().Equals(Gio))
                    {
                        flag = 1;
                        cobGioKH.SelectedIndex = i;
                        break;
                    }
                }
            }
            else
                flag = 0;
        }

        private void cobGioKH_SelectedIndexChanged(object sender, EventArgs e)
        {
            maVe = dt.getMaVe(cobTuyenXe.SelectedValue.ToString(), DateTime.ParseExact(dateTimePicker1.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("MM/dd/yyyy", CultureInfo.InvariantCulture), cobGioKH.SelectedItem.ToString());
            DataTable tblXe = dt.getXe(maVe);
            if (flag == 0)
            {
                for (int i = 0; i < cobXe.Items.Count; i++)
                {
                    if (cobXe.Items[i].ToString().Equals(tblXe.Rows[0]["MAXE"].ToString()))
                    {
                        flag = 1;
                        cobXe.SelectedIndex = i;
                        break;
                    }
                }

            }
            else
                flag = 0;
            
        }

        private void btnKiemTra_Click(object sender, EventArgs e)
        {
            cobXe.Enabled = cobGioKH.Enabled = true;
            
            dt.LoadVeTheoTuyenXe_NgayDi(cobTuyenXe.SelectedValue.ToString(), DateTime.ParseExact(dateTimePicker1.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("MM/dd/yyyy", CultureInfo.InvariantCulture));
            tblVeTheoTuyenXe_NgayDi = dt.getVeTheoTuyenXe_NgayDi();
            foreach (DataRow row in tblVeTheoTuyenXe_NgayDi.Rows)
            {
                cobXe.Items.Add(row["MAXE"]);
                cobGioKH.Items.Add(row["GIO"]);
            }
            
        }


        void btn2_Click(object sender, EventArgs e)
        {
            string viTriGhe = listBtnTang2[(sender as Button).TabIndex].Text;
            if (listBtnTang2[(sender as Button).TabIndex].BackColor != Color.Green)
            {
                listBtnTang2[(sender as Button).TabIndex].BackColor = Color.Green;
                lstGhe_DaChon.Add(viTriGhe);
            }
            else
            {
                listBtnTang2[(sender as Button).TabIndex].BackColor = Color.RoyalBlue;
                lstGhe_DaChon.Remove(viTriGhe);
            }

            txtViTriGhe.Text = "";
            foreach (string item in lstGhe_DaChon)
            {
                txtViTriGhe.Text += item + ",";
            }
            txtSoLuong.Text = lstGhe_DaChon.Count.ToString();
        }

        void btn1_Click(object sender, EventArgs e)
        {
            string viTriGhe = listBtnTang1[(sender as Button).TabIndex].Text;
            if (listBtnTang1[(sender as Button).TabIndex].BackColor != Color.Green)
            {
                listBtnTang1[(sender as Button).TabIndex].BackColor = Color.Green;
                lstGhe_DaChon.Add(viTriGhe);
            }
            else
            {
                listBtnTang1[(sender as Button).TabIndex].BackColor = Color.RoyalBlue;
                lstGhe_DaChon.Remove(viTriGhe);
            }
            txtViTriGhe.Text = "";
            foreach (string item in lstGhe_DaChon)
            {
                txtViTriGhe.Text += item + ",";
            }
            txtSoLuong.Text = lstGhe_DaChon.Count.ToString();
        }

        private void btnDatVe_Click(object sender, EventArgs e)
        {
            if (cobGioKH.SelectedItem == null || txtTenKH.Text.Length == null || txtSDT.Text.Length == null || txtSoGheTrong.Text.Length == null || txtSoLuong.Text == null || cobTTThanhToan.SelectedItem == null)
            {
                MessageBox.Show("Mời bạn nhập đầy đủ thông tin để đặt vé");
                return;
            }
            string x = txtViTriGhe.Text.Remove(txtViTriGhe.Text.Length - 1, 1);
            string str = String.Format("Thông tin đặt vé:\nHọ tên khách: {0}\nSDT: {1}\nVị trí ghế: {2}\nNgày khởi hành: {3}\nGiờ khởi hành: {4}\nTuyến xe: {5}", txtTenKH.Text, txtSDT.Text, x, dateTimePicker1.Text, cobGioKH.SelectedItem.ToString(), cobTuyenXe.GetItemText(cobTuyenXe.SelectedItem));
            if (MessageBox.Show(str, "Imformation", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
            {
                DateTime dateTime = DateTime.UtcNow.Date;
                string ngayDat = dateTime.ToString("MM/dd/yyyy");
                if (dt.ThemBanVe(txtTenKH.Text, txtSDT.Text, txtViTriGhe.Text.Remove(txtViTriGhe.Text.Length - 1, 1), txtSoLuong.Text, cobTTThanhToan.SelectedItem.ToString(), txtGhiChu.Text, maVe, ngayDat, NhanVien.MaNV) == true)
                {
                    MessageBox.Show("Đặt vé thành công.");
                    loadGhe();
                    txtSoGheTrong.Text = dt.getSoLuongGheTrong(maVe).ToString();
                }
                else
                    MessageBox.Show("Đặt vé thất bại");
            }
        }

        private void btnXoaTuyenXe_Click(object sender, EventArgs e)
        {
            txtGhiChu.Text = txtSDT.Text = txtSoGheTrong.Text = txtSoLuong.Text = txtTenKH.Text = txtViTriGhe.Text = "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có muốn đóng quản lý vé không?", "Đóng quản lý vé", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
            {
                this.Parent.Controls.Clear();
            }
        }
        #endregion


        

        
    }
}
