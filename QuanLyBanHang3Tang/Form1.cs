using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Data.SqlClient;
using QuanLyBanHang3Tang.BS_layer;

namespace QuanLyBanHang3Tang
{
    public partial class Form1 : Form
    {
        DataTable dtThanhPho = null;

        bool Them;
        string err;
        BLThanhPho dbTP = new BLThanhPho();

        public Form1()
        {
            InitializeComponent();
        }
        
        void LoadData()
        {
            try
            {                
                dgvTHANHPHO.DataSource = dbTP.LayThanhPho();

                dgvTHANHPHO.AutoResizeColumns();

                this.txtThanhPho.ResetText();
                this.txtTenThanhPho.ResetText();

                this.btnLuu.Enabled = false;
                this.btnHuy.Enabled = false;
                this.panel1.Enabled = false;

                this.btnSua.Enabled = true;
                this.btnThem.Enabled = true;
                this.btnXoa.Enabled = true;
                this.btnTroVe.Enabled = true;

                dgvTHANHPHO_CellClick(null, null);
            }
            catch (SqlException)
            {
                MessageBox.Show("Không lấy được nội dung trong table THANHPHO. Lỗi rồi!!!");
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void btnReload_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            Them = true;

            this.txtThanhPho.ResetText();
            this.txtTenThanhPho.ResetText();

            this.btnLuu.Enabled = true;
            this.btnHuy.Enabled = true;
            this.panel1.Enabled = true;

            this.btnThem.Enabled = false;
            this.btnXoa.Enabled = false;
            this.btnSua.Enabled = false;
            this.btnTroVe.Enabled = false;

            this.txtThanhPho.Focus();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            Them = false;

            this.panel1.Enabled = true;
            dgvTHANHPHO_CellClick(null, null);

            this.btnLuu.Enabled = true;
            this.btnHuy.Enabled = true;
            this.panel1.Enabled = true;

            this.btnThem.Enabled = false;
            this.btnSua.Enabled = false;
            this.btnXoa.Enabled = false;
            this.btnTroVe.Enabled = false;

            this.txtThanhPho.Enabled = false;
            this.txtTenThanhPho.Focus();
        }

        private void dgvTHANHPHO_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int r = dgvTHANHPHO.CurrentCell.RowIndex;

            this.txtThanhPho.Text = dgvTHANHPHO.Rows[r].Cells[0].Value.ToString();
            this.txtTenThanhPho.Text = dgvTHANHPHO.Rows[r].Cells[1].Value.ToString();
        }

        private void btnTroVe_Click(object sender, EventArgs e)
        {

            DialogResult traloi;

            traloi = MessageBox.Show("Chắc không?", "Trả lời",
                MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

            if (traloi == DialogResult.OK)
                this.Close();
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {

            this.txtThanhPho.ResetText();
            this.txtTenThanhPho.ResetText();

            this.btnThem.Enabled = true;
            this.btnSua.Enabled = true;
            this.btnXoa.Enabled = true;
            this.btnTroVe.Enabled = true;

            this.btnLuu.Enabled = false;
            this.btnHuy.Enabled = false;
            this.panel1.Enabled = false;

            dgvTHANHPHO_CellClick(null, null);
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {


            if (Them)
            {
                try
                {
                    BLThanhPho blTP = new BLThanhPho();
                    blTP.ThemThanhPho(this.txtThanhPho.Text, this.txtTenThanhPho.Text, ref err);

                    LoadData();

                    MessageBox.Show("Đã thêm xong!");
                }
                catch (SqlException)
                {
                    MessageBox.Show("Không thêm được. Lỗi rồi!");
                }
            }
            else
            {
                BLThanhPho blTP = new BLThanhPho();
                blTP.CapNhatThanhPho(this.txtThanhPho.Text, this.txtTenThanhPho.Text, ref err);

                LoadData();

                MessageBox.Show("Đã sửa xong!");
            } 
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                int r = dgvTHANHPHO.CurrentCell.RowIndex;

                string strTHANHPHO = dgvTHANHPHO.Rows[r].Cells[0].Value.ToString();
                DialogResult traloi;
                traloi = MessageBox.Show("Chắc xóa mẫu tin này không?", "Trả lời",
                    MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (traloi == DialogResult.OK)
                {
                    dbTP.XoaThanhPho(ref err, strTHANHPHO);

                    LoadData();

                    MessageBox.Show("Đã xóa xong!");
                }  
                else
                {
                    MessageBox.Show("Không thực hiện việc xóa mẫu tin!");
                } 
            }
            catch (SqlException)
            {
                MessageBox.Show("Không xóa được. Lỗi rồi!");
            }
        }
    }
}
