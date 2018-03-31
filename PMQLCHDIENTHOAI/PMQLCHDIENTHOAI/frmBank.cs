using BUS;
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
    public partial class frmBank : Form
    {
        internal static string IDTK;

        public frmBank()
        {
            InitializeComponent();
            btnSaveBank_Add.Enabled = false;
        }
        
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        //
        private void loadToData()
        {
            NCCBus.Instance.LoadBank(dataBank_Add);        
           
        }

        private void btnAddBank_Add_Click(object sender, EventArgs e)
        {
          
            if (txtNameBank_Add.Text.ToString().Length <= 0)
            {
                CustomAlert.Show("Vui lòng nhập tên ngân hàng");
                return;
            }
            if (NCCBus.Instance.AddBank(txtNameBank_Add.Text.ToString()))
            {
                loadToData();
            }
        }

        private void btnDeleteBank_Add_Click(object sender, EventArgs e)
        {
            if (dataBank_Add.SelectedRows != null)
            {
                int manh = Convert.ToInt32(dataBank_Add.SelectedRows[0].Cells[1].Value.ToString());
                DialogResult confirm = CustomDialog1.show("Thông báo xác nhận", "Bạn có muốn xóa ngân hàng có mã là " + manh + " không ?", "Có", "Không");
                if (confirm == DialogResult.No)
                {
                    return;
                }
                if (NCCBus.Instance.DeleteBank(manh))
                {
                    loadToData();
                }

            }
        }

        private void btnSaveBank_Add_Click(object sender, EventArgs e)
        {
            
            if (txtNameBank_Add.Text.ToString().Length <= 0)
            {
                CustomAlert.Show("Vui lòng nhập tên ngân hàng");
                return;
            }
            if (NCCBus.Instance.UpdateBank(Convert.ToInt32(txtIdBank_Add.Text.ToString()), txtNameBank_Add.Text.ToString()))
            {
                    loadToData();
                    btnUpdateBank_Add.Enabled = true;
            }
            btnSaveBank_Add.Enabled = false;         
           

        }

        private void btnUpdateBank_Add_Click(object sender, EventArgs e)
        {
            btnSaveBank_Add.Enabled = true;
            btnUpdateBank_Add.Enabled = false;
            if(dataBank_Add.SelectedRows!=null)
            {
                txtIdBank_Add.Text = dataBank_Add.CurrentRow.Cells[1].Value.ToString();
                txtNameBank_Add.Text = dataBank_Add.CurrentRow.Cells[2].Value.ToString();

            }
            else
            {
                txtIdBank_Add.Text = "";
                txtNameBank_Add.Text = "";
            }            
        }

        private void frmBank_Load(object sender, EventArgs e)
        {
            loadToData();
        }
    }
}
