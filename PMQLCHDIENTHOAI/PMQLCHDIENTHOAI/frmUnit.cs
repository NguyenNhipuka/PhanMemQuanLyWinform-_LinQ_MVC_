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
    public partial class frmUnit : Form
    {
        public string IDTK = "";
        int key_ThemDVT = Properties.Settings.Default.cnThemDVT;
        int key_SuaDVT = Properties.Settings.Default.cnSuaDVT;
        int key_XoaDVT = Properties.Settings.Default.cnXoaDVT;
        int key_XemDVT = Properties.Settings.Default.cnXemDVt;
        bool k_them, k_sua, k_xoa, k_xem = false;
        public frmUnit()
        {
            InitializeComponent();
            btnSavedUnit_Add.Enabled = false;
        }
        private void OpenFunction()
        {
            btnAddUnit_Add.Enabled = k_them = TaiKhoanBUS.Instance.checkRoleUser(IDTK, key_ThemDVT);
            btnDeletedUnit_Add.Enabled = k_xoa = TaiKhoanBUS.Instance.checkRoleUser(IDTK, key_XoaDVT);
            btnUpdatedUnit_Add.Enabled = k_sua = TaiKhoanBUS.Instance.checkRoleUser(IDTK, key_SuaDVT);
        }
        private void loadToData()
        {
            ThietBiBus.Instance.LoaddDVTTB(datadUnit_Add);
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAdddUnit_Add_Click(object sender, EventArgs e)
        {

            if (txtNamedUnit_Add.Text.ToString().Length <= 0)
            {
                CustomAlert.Show("Vui lòng nhập tên đơn vị tính ");
                return;
            }
            ThietBiBus.Instance.AddDVTTB(txtNamedUnit_Add.Text.ToString());
        }

        private void datadUnit_Add_CurrentCellChanged(object sender, EventArgs e)
        {
            //đã mở chức năng để cập nhật
            if (btnSavedUnit_Add.Enabled && datadUnit_Add.CurrentRow != null)
            {
                txtIdUnit_Add.Text = datadUnit_Add.CurrentRow.Cells[1].Value.ToString();
                txtNamedUnit_Add.Text = datadUnit_Add.CurrentRow.Cells[2].Value.ToString();
            }
            else
            {
                txtIdUnit_Add.Text = "";
                txtNamedUnit_Add.Text = "";
            }
        }

        private void frmUnit_Load(object sender, EventArgs e)
        {
            btnSavedUnit_Add.Enabled = false;
            if (IDTK.Equals(""))
            {
                this.Close();
            }
            OpenFunction();
            loadToData();
        }

        private void btnDeletedUnit_Add_Click(object sender, EventArgs e)
        {

            if (datadUnit_Add.SelectedRows != null)
            {
                int madv = Convert.ToInt32(datadUnit_Add.SelectedRows[0].Cells[1].Value.ToString());
                if (ThietBiBus.Instance.DeleteDVTTB(madv))
                {
                    loadToData();
                    txtIdUnit_Add.Text = txtNamedUnit_Add.Text = null;
                }
            }
        }

        private void btnSavedUnit_Add_Click(object sender, EventArgs e)
        {
            //mở chức năng thêm nếu có quyền
            if (txtNamedUnit_Add.Text.ToString().Length <= 0)
            {
                CustomAlert.Show("Vui lòng nhập tên đơn vị tính ");
                return;
            }
            if (ThietBiBus.Instance.UpdateDVTTB(Convert.ToInt32(txtIdUnit_Add.Text.ToString()), txtNamedUnit_Add.Text.ToString()))
            {
                txtIdUnit_Add.Text = txtNamedUnit_Add.Text = null;
                loadToData();
                btnSavedUnit_Add.Enabled = TaiKhoanBUS.Instance.checkRoleUser(IDTK, key_ThemDVT);

            }
        }

        private void btnUpdatedUnit_Add_Click(object sender, EventArgs e)
        {
            txtIdUnit_Add.Enabled = true;
            btnSavedUnit_Add.Enabled = true;
            btnAddUnit_Add.Enabled = false;
        }
    }
}
