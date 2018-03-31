using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BUS;

namespace PMQLCHDIENTHOAI
{
    public partial class frmPosition : Form
    {
        public frmPosition()
        {
            InitializeComponent();
        }

        private void frmPosition_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'dataSet.CHUCVU' table. You can move, or remove it, as needed.
            this.cHUCVUTableAdapter.Fill(this.dataSet.CHUCVU);
        }

        private void btnAddCV_Add_Click(object sender, EventArgs e)
        {
            string tencv = txtNameCV_Add.Text.ToString();
            if (string.IsNullOrEmpty(tencv))
            {
                CustomMessageBox1.show("Chưa nhập tên chức vụ.");return;
            }
            if (ChucVuBus.Instance.insertPosition(tencv))
            {
                txtNameCV_Add.Text = string.Empty;
                this.cHUCVUTableAdapter.Fill(this.dataSet.CHUCVU);

            }
            this.cHUCVUTableAdapter.Fill(this.dataSet.CHUCVU);

        }

        private void btnDeleteCV_Add_Click(object sender, EventArgs e)
        {
            try
            {
                int macv = Convert.ToInt32(cHUCVUDataGridView.SelectedRows[0].Cells[0].Value.ToString());
                if (ChucVuBus.Instance.deletePosition(macv))
                {
                    txtNameCV_Add.Text =txtIdCV_Add.Text= string.Empty;
                    this.cHUCVUTableAdapter.Fill(this.dataSet.CHUCVU);
                }
            }
            catch
            {
                CustomMessageBox1.show("Chưa chọn chức vụ");
            }
        }

        private void btnSaveCV_Add_Click(object sender, EventArgs e)
        {
            try
            {
                int macv = Convert.ToInt32(txtIdCV_Add.Text.ToString());
                string tencv = txtNameCV_Add.Text.ToString();
                if (string.IsNullOrEmpty(tencv))
                {
                    CustomMessageBox1.show("Chưa nhập tên chức vụ."); return;
                }
                if (ChucVuBus.Instance.updatePosition(macv,tencv))
                {
                    txtNameCV_Add.Text = txtIdCV_Add.Text = string.Empty;
                    this.cHUCVUTableAdapter.Fill(this.dataSet.CHUCVU);
                    btnSaveCV_Add.Enabled = false;
                    btnAddCV_Add.Enabled = true;
                    btnUpdateCV_Add.Enabled = true;
                    txtNameCV_Add.Enabled = false;
                }
                this.cHUCVUTableAdapter.Fill(this.dataSet.CHUCVU);
            }
            catch
            {
                CustomMessageBox1.show("Chưa chọn chức vụ");
            }
        }

        private void btnUpdateCV_Add_Click(object sender, EventArgs e)
        {
            btnSaveCV_Add.Enabled = true;
            btnAddCV_Add.Enabled = false;
            if(cHUCVUDataGridView.SelectedRows!= null)
            {
                txtIdCV_Add.Text = cHUCVUDataGridView.SelectedRows[0].Cells[0].Value.ToString();
                txtNameCV_Add.Text = cHUCVUDataGridView.SelectedRows[0].Cells[1].Value.ToString();
                txtNameCV_Add.Enabled = true;
            }
            else
            {
                btnSaveCV_Add.Enabled = false;
                btnAddCV_Add.Enabled = true;
                btnUpdateCV_Add.Enabled = true;
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
