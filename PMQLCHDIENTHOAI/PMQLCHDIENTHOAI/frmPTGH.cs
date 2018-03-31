using BUS;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PMQLCHDIENTHOAI
{
    public partial class frmPTGH : Form
    {
        public string IDTK = "";
        public frmPTGH()
        {
            InitializeComponent();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void loadToData()
        {
            HoaDonBanBus.Instance.LoadPTGH(dataTransit_Add);
           
        }
        private void btnAdddTransit_Add_Click(object sender, EventArgs e)
        {
            if (txtNameTransit_Add.Text.ToString().Length <= 0)
            {
                CustomAlert.Show("Vui lòng nhập tên PTGH");
                return;
            }

            if (HoaDonBanBus.Instance.AddPTGH(txtNameTransit_Add.Text.ToString(), double.Parse(txtPriceTransit_Add.Text.ToString())))
            {
                loadToData();
            }
        }

        private void btnDeletedTransit_Add_Click(object sender, EventArgs e)
        {
            if (dataTransit_Add.SelectedRows != null)
            {
                int maptgh = Convert.ToInt32(dataTransit_Add.SelectedRows[0].Cells[1].Value.ToString());
                DialogResult confirm = CustomDialog1.show("Thông báo xác nhận", "Bạn có muốn xóa PTGH có mã là " + maptgh + " không ?", "Có", "Không");
                if (confirm == DialogResult.No)
                {
                    return;
                }
                if (HoaDonBanBus.Instance.DeletePTGH(maptgh))
                {
                    loadToData();
                }

            }
        }

        private void btnSaveTransit_Add_Click(object sender, EventArgs e)
        {
            string tenptgh = txtNameTransit_Add.Text.ToString();
            int maptgh =Convert.ToInt32(txtIdTransit_Add.Text.ToString());
            double phiptgh = Convert.ToDouble(txtPriceTransit_Add.Text.ToString());
            Boolean result = HoaDonBanBus.Instance.UpdatePTGH(maptgh, tenptgh, phiptgh); 
           
            //update fail
            if (!result)
            {
                return;
            }
            //update success
            loadToData();
            btnSaveTransit_Add.Enabled = false;
            btnUpdateTransit_Add.Enabled = true;
            txtIdTransit_Add.Enabled = txtNameTransit_Add.Enabled = txtPriceTransit_Add.Enabled = false;
        }

        private void btnUpdateTransit_Add_Click(object sender, EventArgs e)
        {
            btnSaveTransit_Add.Enabled = true;
            btnUpdateTransit_Add.Enabled = false;
            txtIdTransit_Add.Enabled = false;
            txtNameTransit_Add.Enabled = txtPriceTransit_Add.Enabled = true;
            if (dataTransit_Add.SelectedRows != null)
            {
                txtIdTransit_Add.Text = dataTransit_Add.CurrentRow.Cells[1].Value.ToString();
                txtNameTransit_Add.Text = dataTransit_Add.CurrentRow.Cells[2].Value.ToString();
                txtPriceTransit_Add.Text = dataTransit_Add.CurrentRow.Cells[3].Value.ToString();

            }
            else
            {
                txtNameTransit_Add.Text = "";
                txtIdTransit_Add.Text = "";
                txtPriceTransit_Add.Text = "";
            }
        }

        private void frmPTGH_Load(object sender, EventArgs e)
        {
            loadToData();
            dataTransit_Add.RowsDefaultCellStyle.BackColor = Color.White;
            dataTransit_Add.AlternatingRowsDefaultCellStyle.BackColor = Color.LightBlue;
        }

        private void txtPriceTransit_Add_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        private void txtPriceTransit_Add_OnValueChanged(object sender, EventArgs e)
        {
            if (txtPriceTransit_Add.Text == string.Empty)
                txtPriceTransit_Add.Text = "0";          
           
        }

      
        private void blockMenu()
        {
            btnAdddTransit_Add.Enabled = TaiKhoanBUS.Instance.checkRoleUser(IDTK, Properties.Settings.Default.cnThemPTGH);
            btnDeletedTransit_Add.Enabled = TaiKhoanBUS.Instance.checkRoleUser(IDTK, Properties.Settings.Default.cnXoaPTGH);
            btnUpdateTransit_Add.Enabled = TaiKhoanBUS.Instance.checkRoleUser(IDTK, Properties.Settings.Default.cnCapNhatPTGH);
        }

        private void frmPTGH_Load_1(object sender, EventArgs e)
        {
            blockMenu();
        }
    }
}
