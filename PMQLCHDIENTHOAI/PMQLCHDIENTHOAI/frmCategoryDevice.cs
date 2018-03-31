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
    public partial class frmCategoryDevice : Form
    {
        public string IDTK="";
        int key_ThemLoai = Properties.Settings.Default.cnThemLoai;
        int key_SuaLoai = Properties.Settings.Default.cnSuaLoai;
        int key_XoaLoai = Properties.Settings.Default.cnXoaLoai;
        int key_XemLoai = Properties.Settings.Default.cnXemLoai;
        bool k_themloai, k_sualoai, k_xoaloai, k_xemloai = false;
        public frmCategoryDevice()
        {
            InitializeComponent();
            OpenFunction();
        }

        private void OpenFunction()
        {
            btnAddCate_Add.Enabled= k_themloai = TaiKhoanBUS.Instance.checkRoleUser(IDTK,key_ThemLoai);
            btnDeleteCate_Add.Enabled = k_xoaloai = TaiKhoanBUS.Instance.checkRoleUser(IDTK, key_XoaLoai);
            btnUpdateCate_Add.Enabled = k_sualoai = TaiKhoanBUS.Instance.checkRoleUser(IDTK, key_SuaLoai);
        }
        private void loadToData()
        {
            ThietBiBus.Instance.LoadLoaiTB(dataCate_Add);
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAddCate_Add_Click(object sender, EventArgs e)
        {
            if (txtNameCate_Add.Text.ToString().Length <= 0)
            {
                CustomAlert.Show("Vui lòng nhập tên loại thiết bị");
                return;
            }
            if(ThietBiBus.Instance.AddLoaiTB(txtNameCate_Add.Text.ToString()))
            {
                loadToData();
            }
        }

        private void dataCate_Add_CurrentCellChanged(object sender, EventArgs e)
        {
            //đã mở chức năng để cập nhật
            if (btnSaveCate_Add.Enabled && dataCate_Add.CurrentRow!= null)
            {
                txtIdCate_Add.Text = dataCate_Add.CurrentRow.Cells[1].Value.ToString();
                txtNameCate_Add.Text = dataCate_Add.CurrentRow.Cells[2].Value.ToString();
            }
            else
            {
                txtIdCate_Add.Text = "";
                txtNameCate_Add.Text = "";
            }
        }

        private void frmCategoryDevice_Load(object sender, EventArgs e)
        {
            btnSaveCate_Add.Enabled = false;
            if (IDTK.Equals(""))
            {
                this.Close();
            }
            OpenFunction();
            loadToData();
        }

        private void btnDeleteCate_Add_Click(object sender, EventArgs e)
        {
            if(dataCate_Add.SelectedRows != null)
            {
                int matb =Convert.ToInt32(dataCate_Add.SelectedRows[0].Cells[1].Value.ToString());
                DialogResult confirm = CustomDialog1.show("Thông báo xác nhận", "Bạn có muốn xóa loại thiế bị có mã là " + matb + " không ?", "Có", "Không");
               if (confirm == DialogResult.No)
                {
                    return;
                }
                if (ThietBiBus.Instance.DeleteLoaiTb(matb))
                {
                    loadToData();
                    txtIdCate_Add.Text = txtNameCate_Add.Text = null;
                }
               
            }
        }

        private void btnSaveCate_Add_Click(object sender, EventArgs e)
        {
            //mở chức năng thêm nếu có quyền
            if (txtNameCate_Add.Text.ToString().Length <= 0)
            {
                CustomAlert.Show("Vui lòng nhập tên loại thiết bị");
                return;
            }
            if(ThietBiBus.Instance.UpdateLoaiTB(Convert.ToInt32(txtIdCate_Add.Text.ToString()),txtNameCate_Add.Text.ToString()))
            {
                txtNameCate_Add.Text = txtIdCate_Add.Text = null;
                loadToData();
                btnAddCate_Add.Enabled = TaiKhoanBUS.Instance.checkRoleUser(IDTK, key_ThemLoai);
                btnUpdateCate_Add.Enabled = true;
            }

        }

        private void btnUpdateCate_Add_Click(object sender, EventArgs e)
        {
            txtNameCate_Add .Enabled = true;
            btnSaveCate_Add.Enabled = true;
            btnAddCate_Add.Enabled = false;
          
        }
    }
}
