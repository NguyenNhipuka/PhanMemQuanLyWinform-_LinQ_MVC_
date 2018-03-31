using BUS;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace PMQLCHDIENTHOAI
{
    public partial class frmCustomers : Form
    {
        public int flat = 0;
        public int tabindex = 1;
        public string IDTK = "";
        public frmCustomers()
        {
            InitializeComponent();
            openTabPage(tabindex);
        }
        public void Exitfrm()
        {
            this.Close();
        }
        public void block_menu()
        {
            btnAddCustomer.Enabled = TaiKhoanBUS.Instance.checkRoleUser(IDTK, Properties.Settings.Default.cnThemKH);
            btnCustomers.Enabled = TaiKhoanBUS.Instance.checkRoleUser(IDTK, Properties.Settings.Default.cnDSKH);
            btnReportExcel.Enabled = TaiKhoanBUS.Instance.checkRoleUser(IDTK, Properties.Settings.Default.cnXuatKh);

        }
        public void openTabPage(int tab)
        {  
            tabHome.TabPages.Clear();
            switch (tab)
            {
                case 1://tab home
                    tabHome.TabPages.Add(tabPageHome);
                    break;
                case 2://tab add Customer
                    tabHome.TabPages.Add(tabPageAddCustomer);
                    break;
                case 3://tab Customers
                                tabHome.TabPages.Add(tabPageCustomers);
                    break;
                case 4://tab Info Customer
                                tabHome.TabPages.Add(tabPageInfoCustomer);
                    btnSearchCustomer_InfoCus.Enabled = TaiKhoanBUS.Instance.checkRoleUser(IDTK, Properties.Settings.Default.cnTTKH);
                    btnUpdateCustomers_Info.Enabled = TaiKhoanBUS.Instance.checkRoleUser(IDTK, Properties.Settings.Default.cnCapNhatKH);
                    btnDeleteCustomers_Info.Enabled = TaiKhoanBUS.Instance.checkRoleUser(IDTK, Properties.Settings.Default.cnXoaKH);
                    break;
                case 5://tab Search Customers
                   

                    tabHome.TabPages.Add(tabPageSearchCustomers);
                    break;

                case 6://tab Excel Customers
                                tabHome.TabPages.Add(tabPageExcel);
                    break;
                default://khong co quyen
                    break;
            }
        
        }
        private void btnAddBack_Click(object sender, EventArgs e)
        {
            openTabPage(1);
        }

        private void btnInfoBack_Click(object sender, EventArgs e)
        {
            openTabPage(1);
        }

        private void btnSearchBack_Click(object sender, EventArgs e)
        {
            openTabPage(1);
        }

        private void btnCustomersBack_Click(object sender, EventArgs e)
        {
            openTabPage(1);
        }
        private void btnExcelBack_Click(object sender, EventArgs e)
        {
            openTabPage(1);
        }
        private void btnAddCustomer_Click(object sender, EventArgs e)
        {
            openTabPage(2);
            refresh();

        }

        private void btnCustomers_Click(object sender, EventArgs e)
        {
            openTabPage(3);
            datatableCustomers_Customers.RowsDefaultCellStyle.BackColor = Color.White;
            datatableCustomers_Customers.AlternatingRowsDefaultCellStyle.BackColor = Color.LightBlue;
            CustomersBus.Instance.LoadDSKH(datatableCustomers_Customers);
            removeFilterAndSortBindingSource(bindingSource1, datatableCustomers_Customers);

        }

        private void btnSearchCustomers_Click(object sender, EventArgs e)
        {
            openTabPage(5);
        }

        private void btnCustomerInfo_Click(object sender, EventArgs e)
        {
            openTabPage(4);
            refesh_CustomerInfo();
        }
        private void refesh_CustomerInfo()
        {
            txtIdCustomer_InfoCus.Text = txtNameCustomer_InfoCus.Text =
                txtPhoneCustomer_InfoCus.Text = txtEmaiCustomer_InfoCus.Text =
                txtAddressCustomers_InfoSup.Text = string.Empty;
            txtLoopCustomer_InfoCus.Text = txtSumCustomer_InfoCus.Text = "0";
        }

        private void btnReportExcel_Click(object sender, EventArgs e)
        {
            openTabPage(6);
            datagivCustomersExcelPrint.RowsDefaultCellStyle.BackColor = Color.White;
            datagivCustomersExcelPrint.AlternatingRowsDefaultCellStyle.BackColor = Color.LightBlue;
            CustomersBus.Instance.LoadDSKH(datagidCustomersExcel);
            removeFilterAndSortBindingSource(bindingSource1, datagidCustomersExcel);
            
        }

        private void btnAddCusto_Add_Click(object sender, EventArgs e)
        {
            string emailkh = txtEmailCusto_Add.Text.ToString();
            string match = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
                  @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
                  @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
            Regex regex = new Regex(match);
            if (!regex.IsMatch(emailkh))
            {
                CustomMessageBox.show("Thông báo", "Email không hợp lệ", false);
                return;
            }
            int ttkh = (swKhachHang_Inf.Value == true) ? 1 : 0;
            string tenkh = txtNameCusto_Add.Text.ToString();
            if (string.IsNullOrEmpty(tenkh))
            {
                CustomMessageBox1.show("Chưa nhập tên khách hàng");
                return;
            }
            string sdtkh = txtPhoneCusto_Add.Text.ToString();
            if (string.IsNullOrEmpty(sdtkh) || sdtkh.Length <10 ||sdtkh.Length >11)
            {
                CustomMessageBox1.show("Số điện thoại không đúng định dạng");
                return;
            }
            string diachikh = txtAddressCusto_Add.Text.ToString();
            if (string.IsNullOrEmpty(diachikh))
            {
                CustomMessageBox1.show("Chưa nhập địa chỉ khách hàng");
                return;
            }
            
            CustomersBus.Instance.AddKhachHang(tenkh, diachikh, sdtkh, emailkh, ttkh);
        }

        private void btnSearchCustomer_InfoCus_Click(object sender, EventArgs e)
        {
            int makh = 0;
            try
            {
               makh= int.Parse(txtsearchCustomer_InfoCus.text.ToString());
            }
            catch
            {
                CustomMessageBox1.show("Mã khách hàng không đúng định dạng");return;
            }
            
            Boolean result = CustomersBus.Instance.SearchKhachHangKey(makh, txtIdCustomer_InfoCus,txtNameCustomer_InfoCus,
                txtPhoneCustomer_InfoCus,txtEmaiCustomer_InfoCus, txtAddressCustomers_InfoSup, 
                swStatusCustomer_InfoCus,txtLoopCustomer_InfoCus, txtSumCustomer_InfoCus);
            if (!result)
            {
                refesh_CustomerInfo();
            }
        }

        private void btnUpdateCustomers_Info_Click(object sender, EventArgs e)
        {
            btnSaveCustomers_Info.Enabled = true;
            btnUpdateCustomers_Info.Enabled = false;
                txtNameCustomer_InfoCus.Enabled =
                txtPhoneCustomer_InfoCus.Enabled = txtEmaiCustomer_InfoCus.Enabled =
                txtAddressCustomers_InfoSup.Enabled = swStatusCustomer_InfoCus.Enabled = true;
        }

        private void btnDeleteCustomers_Info_Click(object sender, EventArgs e)
        {

            int makh = 1;
            try
            {
                makh= int.Parse(txtIdCustomer_InfoCus.Text.ToString());
            }
            catch
            {
                CustomMessageBox1.show("Mã khách hàng không đúng định dạng"); return;
            }
            if(makh == 1)
            {
                CustomMessageBox1.show("Mã khách hàng này không thể xóa"); return;
            }
            DialogResult confirm = CustomDialog1.show("Thông báo xác nhận", "Bạn có muốn xóa khách hàng có mã là " + makh + " không ?", "Có", "Không");
            if (confirm == DialogResult.No)
            {
                return;
            }
            if (!string.IsNullOrEmpty(makh.ToString()))
            {
                if (CustomersBus.Instance.DeleteKhachHang(makh))
                {
                    txtIdCustomer_InfoCus.Text = txtNameCustomer_InfoCus.Text =
                    txtPhoneCustomer_InfoCus.Text = txtEmaiCustomer_InfoCus.Text =
                    txtAddressCustomers_InfoSup.Text = string.Empty;
                    txtLoopCustomer_InfoCus.Text = txtSumCustomer_InfoCus.Text = "0";
                }
            }
        }
        private void btnSaveCustomers_Info_Click(object sender, EventArgs e)
        {
            string tenkh = txtNameCustomer_InfoCus.Text.ToString();
            if (string.IsNullOrEmpty(tenkh))
            {
                CustomMessageBox1.show("Chưa nhập tên khách hàng"); return;
            }
            int makh = Convert.ToInt32(txtIdCustomer_InfoCus.Text.ToString());           
            string diachikh = txtAddressCustomers_InfoSup.Text.ToString();
            if (string.IsNullOrEmpty(tenkh))
            {
                CustomMessageBox1.show("Chưa nhập địa chỉ khách hàng"); return;
            }
            string sdtkh = txtPhoneCustomer_InfoCus.Text.ToString();
            if (string.IsNullOrEmpty(sdtkh) || sdtkh.Length < 10 || sdtkh.Length > 11)
            {
                CustomMessageBox1.show("Số điện thoại không đúng định dạng");
                return;
            }
            string emailkh = txtEmaiCustomer_InfoCus.Text.ToString();
            string match = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
                  @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
                  @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
            Regex regex = new Regex(match);
            if (!regex.IsMatch(emailkh))
            {
                CustomMessageBox1.show( "Email không hợp lệ");
                return;
            }
            int ttkh = Convert.ToInt32(swStatusCustomer_InfoCus.Value);            
            Boolean result = CustomersBus.Instance.UpdateKhachHang(makh,tenkh,diachikh,sdtkh,emailkh,ttkh);
            //upadte fail
            if (!result)
            {
                return;
            }
            //upadte success
            btnSaveCustomers_Info.Enabled = false;
            btnUpdateCustomers_Info.Enabled = true;
            txtIdCustomer_InfoCus.Enabled = txtNameCustomer_InfoCus.Enabled =
            txtPhoneCustomer_InfoCus.Enabled = txtEmaiCustomer_InfoCus.Enabled =
            txtAddressCustomers_InfoSup.Enabled = swStatusCustomer_InfoCus.Enabled =
            txtLoopCustomer_InfoCus.Enabled = txtSumCustomer_InfoCus.Enabled = false;

        }
        private void btnResetCusto_Add_Click(object sender, EventArgs e)
        {
            refresh();
        }
        public void refresh()
        {
            txtNameCusto_Add.Text = string.Empty;
            txtPhoneCusto_Add.Text = string.Empty;
            txtEmailCusto_Add.Text = string.Empty;
            txtAddressCusto_Add.Text = string.Empty;
        }
        private void txtPhoneCusto_Add_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
                e.Handled = true;
        }
        
        //
        private void removeFilterAndSortBindingSource(BindingSource bin, DataGridView gv)
        {
            bin.Filter = bin.Sort = null;
            bin.DataSource = null;
            bin.DataSource = gv.DataSource;
        }
        //
        
        private void datatableCustomers_Customers_SortStringChanged(object sender, EventArgs e)
        {
            bindingSource1.Sort = datatableCustomers_Customers.SortString;
            UpdateSTTGrid(datatableCustomers_Customers);
        }

        private void datatableCustomers_Customers_FilterStringChanged(object sender, EventArgs e)
        {
            bindingSource1.Filter = datatableCustomers_Customers.FilterString;
            UpdateSTTGrid(datatableCustomers_Customers);
        }
        private void UpdateSTTGrid(DataGridView gridView)
        {
            for (int i = 0; i < gridView.RowCount; i++)
            {
                gridView.Rows[i].Cells[0].Value = i + 1;

            }
        }

        private void btnIn_CustomersExcel_Click(object sender, EventArgs e)
        {
            if (datagidCustomersExcel.CurrentRow != null)
            {
                for (int i = 0; i < datagidCustomersExcel.SelectedRows.Count; i++)
                {
                    string makh = datagidCustomersExcel.SelectedRows[i].Cells[1].Value.ToString();
                    string tenkh = datagidCustomersExcel.SelectedRows[i].Cells[2].Value.ToString();
                    try
                    {
                        var item = datagivCustomersExcelPrint.Rows.Cast<DataGridViewRow>().First(r => r.Cells[1].Value.ToString().Equals(makh));
                        if (item != null) continue;//da ton tai
                    }
                    catch { };
                    datagivCustomersExcelPrint.Rows.Add(datagivCustomersExcelPrint.RowCount + 1, makh, tenkh, datagidCustomersExcel.CurrentRow.Cells[3].Value.ToString());
                    datagivCustomersExcelPrint.Refresh();
                    UpdateSTTGrid(datagivCustomersExcelPrint);
                }
                
            }
        }

        private void btnOutCustomersExcel_Click(object sender, EventArgs e)
        {
            if (datagivCustomersExcelPrint.SelectedRows != null)
            {

                foreach (DataGridViewRow row in datagivCustomersExcelPrint.SelectedRows)
                {
                    if (!row.IsNewRow)
                    {
                        datagivCustomersExcelPrint.Rows.Remove(row);
                    }

                }
               
                UpdateSTTGrid(datagivCustomersExcelPrint);
                datagivCustomersExcelPrint.Refresh();
            }
        }

        private void btnRefeshExcel_Ex_Click(object sender, EventArgs e)
        {
            datagivCustomersExcelPrint.DataSource = null;
            datagivCustomersExcelPrint.Rows.Clear();
        }

        private void datagidCustomersExcel_SortStringChanged(object sender, EventArgs e)
        {
            bindingSource1.Sort = datagidCustomersExcel.SortString;
            UpdateSTTGrid(datagidCustomersExcel);
        }

        private void datagidCustomersExcel_FilterStringChanged(object sender, EventArgs e)
        {

            bindingSource1.Filter = datagidCustomersExcel.FilterString;
            UpdateSTTGrid(datagidCustomersExcel);
        }
        //
        private void RemoveFilterAndSortBindingSource(BindingSource bin, DataGridView gv)
        {
            bin.Filter = bin.Sort = null;
            bin.DataSource = null;
            bin.DataSource = gv.DataSource;
        }

        private void txtLoopCustomer_InfoCus_OnValueChanged(object sender, EventArgs e)
        {
            try
            {
                double.Parse(txtLoopCustomer_InfoCus.Text.ToString());
            }
            catch
            {
                txtLoopCustomer_InfoCus.Text = "0";
            }
        }

        private void txtSumCustomer_InfoCus_OnValueChanged(object sender, EventArgs e)
        {
            try
            {
                double.Parse(txtSumCustomer_InfoCus.Text.ToString());
            }
            catch
            {
                txtSumCustomer_InfoCus.Text = "0";
            }
        }

        private void frmCustomers_Load(object sender, EventArgs e)
        {
            block_menu();
        }
        //
    }
}
