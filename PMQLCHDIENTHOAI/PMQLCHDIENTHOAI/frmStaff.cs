using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BUS;
using MetroFramework.Forms;
using MetroFramework.Controls;

namespace PMQLCHDIENTHOAI
{
    public partial class frmStaff : Form
    {
        Size sizelarge = new Size(1286, 650);
        Point pointlarge = new Point(60, 55);

        Size sizesmall = new Size(1122, 650);
        Point pointsmall = new Point(227, 55);

        public int flat = 0;
        public int tabindex = 1;
        public string IDTK = "";
        public void Exitfrm()
        {
            this.Close();
        }
        public frmStaff()
        {
            InitializeComponent();
            openTabPage(tabindex);
            block_menu();
  
        }
        private void block_menu()
        {
            btnMenuAddStaff.Enabled = TaiKhoanBUS.Instance.checkRoleUser(IDTK, Properties.Settings.Default.cnThemNV);
            btnMenuReportExcel.Enabled = TaiKhoanBUS.Instance.checkRoleUser(IDTK, Properties.Settings.Default.cnXuatDSNV);
            btnMenuStaffs.Enabled = TaiKhoanBUS.Instance.checkRoleUser(IDTK, Properties.Settings.Default.cnXemDSNV);

        }
        public void openTabPage(int tab)
        {
            tabHome.TabPages.Clear();
            switch (tab)
            {
                case 1://tab home
                    tabHome.TabPages.Add(tabPageHome);
                    break;
                case 2://tab add Staff
                    tabHome.TabPages.Add(tabPageAddStaff);
                    break;
                case 3://tab Staffs
                    tabHome.TabPages.Add(tabPageStaffs);
                    break;
                case 4://tab Info Staff
                    tabHome.TabPages.Add(tabPageInfoStaff);
                    btnSearchNhanVien_Inf.Enabled = TaiKhoanBUS.Instance.checkRoleUser(IDTK, Properties.Settings.Default.cnXemTTNV);
                    btnUpdateStaff_InfoSup.Enabled = TaiKhoanBUS.Instance.checkRoleUser(IDTK, Properties.Settings.Default.cnCapNhatTTNV);
                    btnDeleteStaff_InfoSup.Enabled = TaiKhoanBUS.Instance.checkRoleUser(IDTK, Properties.Settings.Default.cnXoaNhanVien);

                    break;
                case 5://tab Search Staff
                    tabHome.TabPages.Add(tabPageSearchStaffs);
                    break;

                case 6://tab Excel
                    tabHome.TabPages.Add(tabPageExcel);
                    break;
                default://khong co quyen
                    break;
            }
        }
        public void miniatureMenu()
        {
            pnlstaffmain.Hide();
        }
        public void enlargeMenu()
        {

            pnlstaffmain.Show();
        }

        public void enlargeform()
        {
            this.Location = new Point();
            this.Size = sizelarge;
            
        }
      
 

        private void tabPageAccounts_Click(object sender, EventArgs e)
        {

        }

        private void btnAddBack_Click(object sender, EventArgs e)
        {
            openTabPage(1);
        }

        private void btnStaffsBack_Click(object sender, EventArgs e)
        {
            openTabPage(1);
        }

        private void btnMenuAddStaff_Click(object sender, EventArgs e)
        {
            openTabPage(2);
            datepicBirth_Add.Value = DateTime.Now.Date;
            ChucVuBus.Instance.LoadPositionList(dropCV_Add);
        }

        private void btnMenuStaffs_Click(object sender, EventArgs e)
        {
            openTabPage(3);
            datatableStaffs_Staffs.RowsDefaultCellStyle.BackColor = Color.White;
            datatableStaffs_Staffs.AlternatingRowsDefaultCellStyle.BackColor = Color.LightBlue;
            NhanVienBus.Instance.LoadDSStaff(datatableStaffs_Staffs);
            removeFilterAndSortBindingSource(bindingSource1, datatableStaffs_Staffs);

        }

        private void btnMenuSearchStaff_Click(object sender, EventArgs e)
        {
            openTabPage(5);
        }

        private void btnMenuStaffInfo_Click(object sender, EventArgs e)
        {
            openTabPage(4);           
            refesh_StaffInfo();
            ChucVuBus.Instance.LoadPositionList(cbChucVu_Inf);
        }

        private void btnMenuReportExcel_Click(object sender, EventArgs e)
        {
            openTabPage(6);           
            datagidStaffsExcel.RowsDefaultCellStyle.BackColor = Color.White;
            datagidStaffsExcel.AlternatingRowsDefaultCellStyle.BackColor = Color.LightBlue;
            NhanVienBus.Instance.LoadDSStaffExcel(datagidStaffsExcel);
            removeFilterAndSortBindingSource(bindingSource1, datagidStaffsExcel);
            datagivStaffsPrint.DataSource = null;
            datagivStaffsPrint.Rows.Clear();

        }
        //
        private void removeFilterAndSortBindingSource(BindingSource bin, DataGridView gv)
        {
            bin.Filter = bin.Sort = null;
            bin.DataSource = null;
            bin.DataSource = gv.DataSource;
        }
        //
        private void btnSearchBack_Click(object sender, EventArgs e)
        {
            openTabPage(1);
        }

        private void btnExcelBack_Click(object sender, EventArgs e)
        {
            openTabPage(1);
        }

        private void btnAddStaff_add_Click(object sender, EventArgs e)
        {
            string tennv = txtTenStaff_Add.Text.ToString();
            if (string.IsNullOrEmpty(tennv))
            {
                CustomMessageBox1.show("Chưa nhập tên nhân viên");
                return;
            }

            string gioi = "Nam";
            if (swSex_add.Value == true) gioi = "Nữ";
            DateTime date = datepicBirth_Add.Value.Date;
            string sdt = txtPhone_Add.Text.ToString();
            if (string.IsNullOrEmpty(sdt) || sdt.Length < 10 || sdt.Length > 11)
            {
                CustomMessageBox1.show("Số điện thoại không đúng định dạng");
                return;
            }
            string cmnd = txtCMND_Add.Text.ToString();
            if (string.IsNullOrEmpty(cmnd) || cmnd.Length < 9)
            {
                CustomMessageBox1.show("Số chứng minh không đúng định dạng");
                return;
            }
            string dchi = txtAddress_Add.Text.ToString();
            if (string.IsNullOrEmpty(dchi))
            {
                CustomMessageBox1.show("Chưa nhập địa chỉ nhân viên");
                return;
            }
            double bac = double.Parse(txtBac_Add.Text.ToString());
            double phucap = double.Parse(txtPhuCap_Add.Text.ToString());
            double luong = double.Parse(txtLuong_Add.Text.ToString());
            int cv =int.Parse(dropCV_Add.SelectedValue.ToString());
            Boolean status = swStatus_AddStaff.Value;
            NhanVienBus.Instance.InsertStaff(cv, tennv, dchi, sdt, gioi, date, cmnd, bac, phucap, luong, status);

        }

        private void dropCV_Add_Click(object sender, EventArgs e)
        {
            
        }

        private void btnIn_Ex_Click(object sender, EventArgs e)
        {
            if (datagidStaffsExcel.CurrentRow != null)
            {
                for (int i = 0; i < datagidStaffsExcel.SelectedRows.Count; i++)
                {
                    string manv = datagidStaffsExcel.SelectedRows[i].Cells[1].Value.ToString();
                    string tennv = datagidStaffsExcel.SelectedRows[i].Cells[2].Value.ToString();
                    try
                    {
                        var item = datagivStaffsPrint.Rows.Cast<DataGridViewRow>().First(r => r.Cells[1].Value.ToString().Equals(manv));
                        if (item != null) continue;//da ton tai
                    }
                    catch { };
                    datagivStaffsPrint.Rows.Add(datagivStaffsPrint.RowCount + 1, manv,tennv, datagidStaffsExcel.Rows[i].Cells[3].Value.ToString());
                    datagivStaffsPrint.Refresh();
                    UpdateSTTGrid(datagivStaffsPrint);
                }
            }
            
        }
        private void UpdateSTTGrid(DataGridView gridView)
        {
            for (int i = 0; i < gridView.RowCount; i++)
            {
                gridView.Rows[i].Cells[0].Value = i + 1;

            }
        }
        private void btnOut_Click(object sender, EventArgs e)
        {
            if (datagivStaffsPrint.SelectedRows != null)
            {
                foreach (DataGridViewRow row in datagivStaffsPrint.SelectedRows)
                {
                    if (!row.IsNewRow)
                    {
                        datagivStaffsPrint.Rows.Remove(row);
                    }

                }
                UpdateGrid(datagivStaffsPrint);
                datagivStaffsPrint.Refresh();

            }
        }

        private void UpdateGrid(DataGridView gridView)
        {
            for (int i = 0; i < gridView.RowCount; i++)
            {
                gridView.Rows[i].Cells[0].Value = i + 1;

            }
        }

        private void datagivStaffsPrint_Sorted(object sender, EventArgs e)
        {
            UpdateGrid(datagivStaffsPrint);
        }

        private void datagidStaffsExcel_Sorted(object sender, EventArgs e)
        {
            UpdateGrid(datagidStaffsExcel);
        }

        private void btnRefeshExcel_Ex_Click(object sender, EventArgs e)
        {
            datagivStaffsPrint.DataSource = null;
            datagivStaffsPrint.Rows.Clear();
        }

        private void btnSearchNhanVien_Inf_Click(object sender, EventArgs e)
        {
            string key = txtsearchNhanVien_Inf.text.ToString();

            if (string.IsNullOrEmpty(key))
            {
                CustomMessageBox1.show("Chưa nhập mã nhân viên");return;
                
            }
            Boolean result = false;
            result=NhanVienBus.Instance.searchNhanVien(key, txtMaNV_Inf, txtName_Inf, swStatus_Inf, swGTinh_Inf, datepicNGSinh_Inf, txtSDT_Inf, txtCMND_Inf, txtDChi_Inf, txtBLuong_Inf,
               txtPhuCap_Inf, txtLuong_Inf, cbChucVu_Inf, DatepicNgayTao_Inf);
            swGTinh_Inf.Enabled = swStatus_Inf.Enabled = false;
            if (!result)
            {
                refesh_StaffInfo();
                return;
            }
            btnUpdateStaff_InfoSup.Enabled = true;
        }
        private void refesh_StaffInfo()
        {
            txtMaNV_Inf.Text= txtName_Inf.Text = string.Empty;
            swStatus_Inf.Value= swGTinh_Inf.Value = false;
            datepicNGSinh_Inf.Value = DatepicNgayTao_Inf.Value = DateTime.Now.Date;
            txtSDT_Inf.Text = txtCMND_Inf.Text = txtDChi_Inf.Text = string.Empty;
            txtBLuong_Inf.Text = txtPhuCap_Inf.Text = txtLuong_Inf.Text = "0";
        }
        private void txtPhone_Add_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        private void txtCMND_Add_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        private void txtBac_Add_KeyPress(object sender, KeyPressEventArgs e)
        {
            char decimalchar = '.';
            if (Char.IsDigit(e.KeyChar) || Char.IsControl(e.KeyChar))
            {

            }else if(e.KeyChar==decimalchar & Text.IndexOf(decimalchar.ToString())==-1)
                {

            }else {
                e.Handled = true;
            }

        }

        private void btnUpdateStaff_InfoSup_Click(object sender, EventArgs e)
        {
            btnSaveStaff_InfoSup.Enabled = true;
            btnUpdateStaff_InfoSup.Enabled = false;
            txtMaNV_Inf.Enabled = false;
            txtName_Inf.Enabled = swGTinh_Inf.Enabled =
            datepicNGSinh_Inf.Enabled = txtSDT_Inf.Enabled = swStatus_Inf.Enabled = 
            txtDChi_Inf.Enabled = txtCMND_Inf.Enabled = txtBLuong_Inf.Enabled = 
            txtPhuCap_Inf.Enabled = txtLuong_Inf.Enabled = cbChucVu_Inf.Enabled = DatepicNgayTao_Inf.Enabled = true;
        }

        private void btnInfoBack_Click(object sender, EventArgs e)
        {
            openTabPage(1);
        }

        private void btnSaveStaff_InfoSup_Click(object sender, EventArgs e)
        {
            string tennv = txtName_Inf.Text.ToString();
            if (string.IsNullOrEmpty(tennv))
            {
                CustomAlert.Show("Chưa nhập tên nhân viên"); return;
            }
            string manv = txtMaNV_Inf.Text.ToString();
            int macv = Convert.ToInt32(cbChucVu_Inf.SelectedValue);
            string diachinv = txtDChi_Inf.Text.ToString();
            if (string.IsNullOrEmpty(diachinv))
            {
                CustomAlert.Show("Chưa nhập địa chỉ của nhân viên"); return;
            }
            string sdtnv = txtSDT_Inf.Text.ToString();
            if (string.IsNullOrEmpty(sdtnv) || sdtnv.Length <10 || sdtnv.Length < 11)
            {
                CustomAlert.Show("Số điện thoại không đúng định dạng"); return;
            }
            string gtnv = "Nam";
            if (swGTinh_Inf.Value == true) gtnv = "Nữ";            
            DateTime ngaysinhnv = datepicNGSinh_Inf.Value;
            int ttnv = Convert.ToInt32(swStatus_Inf.Value);
            DateTime ngaytaonv = DatepicNgayTao_Inf.Value;
            string cmnvnv = txtCMND_Inf.Text.ToString();
            if (string.IsNullOrEmpty(cmnvnv) || cmnvnv.Length < 9)
            {
                CustomAlert.Show("Chứng minh nhân dân không đúng định dạng"); return;
            }
            double bacluongnv = Convert.ToDouble(txtBLuong_Inf.Text.ToString());
            double phucaapnv = Convert.ToDouble(txtPhuCap_Inf.Text.ToString());
            double luongnv = Convert.ToDouble(txtLuong_Inf.Text.ToString());
            Boolean result = NhanVienBus.Instance.UpdateStaff(manv,macv,tennv,diachinv,sdtnv,gtnv,
                ngaysinhnv, ttnv, ngaytaonv, cmnvnv, bacluongnv, phucaapnv, luongnv);
            //upadte fail
            if (!result)
            {
                return;
            }
            //update success
            btnUpdateStaff_InfoSup.Enabled = true;
            btnSaveStaff_InfoSup.Enabled = false;
            txtMaNV_Inf.Enabled = txtDChi_Inf.Enabled = txtLuong_Inf.Enabled =
            txtName_Inf.Enabled = txtPhuCap_Inf.Enabled = txtCMND_Inf.Enabled =
            txtSDT_Inf.Enabled = txtBLuong_Inf.Enabled = cbChucVu_Inf.Enabled = swStatus_Inf.Enabled=
            DatepicNgayTao_Inf.Enabled = datepicNGSinh_Inf.Enabled = swGTinh_Inf.Enabled = false;
        }

        private void btnDeleteStaff_InfoSup_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtMaNV_Inf.Text.ToString()))
            {
                if (NhanVienBus.Instance.DeleteStaff(txtMaNV_Inf.Text.ToString()))
                {

                    txtsearchNhanVien_Inf.text = txtMaNV_Inf.Text =
                    txtName_Inf.Text= txtSDT_Inf.Text= txtDChi_Inf.Text=
                    txtCMND_Inf.Text= txtBLuong_Inf.Text= txtPhuCap_Inf.Text=
                    txtLuong_Inf.Text=string.Empty;
                    swGTinh_Inf.Value = true;
                    cbChucVu_Inf.DataSource = null;

                }

            }
        }

        private void btnReset_add_Click(object sender, EventArgs e)
        {
            txtsearchNhanVien_Inf.text = txtMaNV_Inf.Text =
            txtName_Inf.Text = txtSDT_Inf.Text = txtDChi_Inf.Text =
            txtCMND_Inf.Text = txtBLuong_Inf.Text = txtPhuCap_Inf.Text =
            txtLuong_Inf.Text = string.Empty;
            swGTinh_Inf.Value = true;
            cbChucVu_Inf.DataSource = null;
        }

        private void txtPhuCap_Add_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        private void txtLuong_Add_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        private void txtSDT_Inf_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        private void txtCMND_Inf_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        private void txtBLuong_Inf_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        private void txtPhuCap_Inf_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        private void txtLuong_Inf_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        private void datagidStaffsExcel_FilterStringChanged(object sender, EventArgs e)
        {
            bindingSource1.Filter = datagidStaffsExcel.FilterString;
            UpdateSTTGrid(datagidStaffsExcel);
        }

        private void datagidStaffsExcel_SortStringChanged(object sender, EventArgs e)
        {
            bindingSource1.Sort = datagidStaffsExcel.SortString;
            UpdateSTTGrid(datagidStaffsExcel);
        }

        private void datatableStaffs_Staffs_SortStringChanged(object sender, EventArgs e)
        {
            bindingSource1.Sort = datatableStaffs_Staffs.SortString;
            UpdateSTTGrid(datatableStaffs_Staffs);
        }

        private void datatableStaffs_Staffs_FilterStringChanged(object sender, EventArgs e)
        {
            bindingSource1.Filter = datatableStaffs_Staffs.FilterString;
            UpdateSTTGrid(datatableStaffs_Staffs);
        }

        private void txtLuong_Inf_OnValueChanged(object sender, EventArgs e)
        {
            try
            {
                double.Parse(txtLuong_Inf.Text.ToString());
            }
            catch {
                txtLuong_Inf.Text = "0";
            }
        }

        private void txtPhuCap_Inf_OnValueChanged(object sender, EventArgs e)
        {

            try
            {
                double.Parse(txtPhuCap_Inf.Text.ToString());
            }
            catch
            {
                txtPhuCap_Inf.Text = "0";
            }
        }

        private void txtBLuong_Inf_OnValueChanged(object sender, EventArgs e)
        {

            try
            {
                double.Parse(txtBLuong_Inf.Text.ToString());
            }
            catch
            {
                txtBLuong_Inf.Text = "0";
            }
        }

        private void txtBac_Add_OnValueChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtBac_Add.Text.ToString()))
            {
                txtBac_Add.Text = "0";
            }
        }

        private void txtPhuCap_Add_OnValueChanged(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(txtPhuCap_Add.Text.ToString()))
            {
                txtBac_Add.Text = "0";
            }
        }

        private void txtLuong_Add_OnValueChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtLuong_Add.Text.ToString()))
            {
                txtBac_Add.Text = "0";
            }
        }

        private void btnChucVu_Click(object sender, EventArgs e)
        {
            frmPosition frm = new frmPosition();
            frm.ShowDialog();
        }

        private void frmStaff_Load(object sender, EventArgs e)
        {
            block_menu();
        }
    }
}
