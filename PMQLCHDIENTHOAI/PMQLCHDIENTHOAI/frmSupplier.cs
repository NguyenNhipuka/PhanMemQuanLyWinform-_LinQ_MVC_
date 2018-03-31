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
    public partial class frmSupplier : Form
    {
        public int flat = 0;
        public int tabindex = 1;
        public String IDTK = "";
        public frmSupplier()
        {
            InitializeComponent();
            openTabPage(tabindex);
        }
        public void block_menu()
        {
            btnAddSupplier.Enabled = TaiKhoanBUS.Instance.checkRoleUser(IDTK, Properties.Settings.Default.cnThemNCC);
            btnSuppliers.Enabled = TaiKhoanBUS.Instance.checkRoleUser(IDTK, Properties.Settings.Default.cnDSNCC);
            btnReportExcel.Enabled = TaiKhoanBUS.Instance.checkRoleUser(IDTK, Properties.Settings.Default.cnXuatNCC);

        }
        public void openTabPage(int tab)
        {
            tabHome.TabPages.Clear();
            switch (tab)
            {
                case 1://tab home
                    tabHome.TabPages.Add(tabPageHome);
                    break;
                case 2://tab add Supplier
                    tabHome.TabPages.Add(tabPageAddSupplier);
                    break;
                case 3://tab Suppliers
                    tabHome.TabPages.Add(tabPageSuppliers);
                
                    break;
                case 4://tab Info Supplier
                    tabHome.TabPages.Add(tabPageInfoSupplier);
                    btnUpdateSupplier_InfoSup.Enabled = TaiKhoanBUS.Instance.checkRoleUser(IDTK, Properties.Settings.Default.cnCapNhatNCC);
                    btnSearchSupplier_InfoSupf.Enabled = TaiKhoanBUS.Instance.checkRoleUser(IDTK, Properties.Settings.Default.cnXemTTNV);
                    btnDelete_InfoSup.Enabled = TaiKhoanBUS.Instance.checkRoleUser(IDTK, Properties.Settings.Default.cnXoaNCC);

                    break;
                case 5://tab Search Suppliers
                    tabHome.TabPages.Add(tabPageSearchSupplier);
                    break;

                case 6://tab Excel Suppliers
                    tabHome.TabPages.Add(tabPageExcel);
                    break;
                default://khong co quyen
                    break;
            }
        }
        private void btnInfoBack_Click(object sender, EventArgs e)
        {
            openTabPage(1);
        }

        private void btnAddBack_Click(object sender, EventArgs e)
        {
            openTabPage(1);
        }

        private void btnSearchBack_Click(object sender, EventArgs e)
        {
            openTabPage(1);
        }

        private void btnSuppliersBack_Click(object sender, EventArgs e)
        {
            openTabPage(1);
        }

        private void btnExcelBack_Click(object sender, EventArgs e)
        {
            openTabPage(1);
        }

        private void btnAddSupplier_Click(object sender, EventArgs e)
        {
            openTabPage(2);
            NCCBus.Instance.LoadBank(drpBankSupplier_Add);
        }

        private void btnSuppliers_Click(object sender, EventArgs e)
        {
            openTabPage(3);
            datatableNccs_Nccs.RowsDefaultCellStyle.BackColor = Color.White;
            datatableNccs_Nccs.AlternatingRowsDefaultCellStyle.BackColor = Color.LightBlue;
            NCCBus.Instance.LoadDSNCC(datatableNccs_Nccs);
            removeFilterAndSortBindingSource(bindingSource1, datatableNccs_Nccs);

        }
        //
        private void removeFilterAndSortBindingSource(BindingSource bin, DataGridView gv)
        {
            bin.Filter = bin.Sort = null;
            bin.DataSource = null;
            bin.DataSource = gv.DataSource;
        }
        //
        private void btnSearchSuppliers_Click(object sender, EventArgs e)
        {
            openTabPage(5);
        }
        private void refesh_SupInfo()
        {

        }
        private void btnSupplierInfo_Click(object sender, EventArgs e)
        {
            openTabPage(4);
        }

        private void btnReportExcel_Click(object sender, EventArgs e)
        {
            openTabPage(6);
            //NCCBus.Instance.LoadDSNCCExcel(datagidSuppliersExcel);
            datagivSuppliersExcelPrint.RowsDefaultCellStyle.BackColor = Color.White;
            datagivSuppliersExcelPrint.AlternatingRowsDefaultCellStyle.BackColor = Color.LightBlue;
            NCCBus.Instance.LoadDSNCCExcel(datagidSuppliersExcel);
            removeFilterAndSortBindingSource(bindingSource1, datagidSuppliersExcel);
            datagivSuppliersExcelPrint.DataSource = null;
            datagivSuppliersExcelPrint.Rows.Clear();
        }

        private void btnUpdateBank_Add_Click(object sender, EventArgs e)
        {
            frmBank frmbank = new frmBank();            
            frmbank.ShowDialog();
            NCCBus.Instance.LoadBank(drpBankSupplier_Add);
            if (drpBankSupplier_Add.Items.Count > 0)
            {
                
                drpBankSupplier_Add.SelectedIndex = 0;
            }
           

        }

        private void btnUpdateBank_InfoSup_Click(object sender, EventArgs e)
        {
            frmBank frmbank = new frmBank();
            frmbank.ShowDialog();
        }

        private void pnlpageAddSupplier_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnAddSupplier_Add_Click(object sender, EventArgs e)
        {
            string emailncc = txtailSupplier_Add.Text.ToString();
            string match = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
                  @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
                  @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";            
            Regex regex = new Regex(match);
            if (!regex.IsMatch(emailncc))
            {
                CustomMessageBox.show("Thông báo","Email không hợp lệ",false);
                return;
            }

            bool ttncc = swTinhtrang_Inf.Value;
            int manhncc = int.Parse(drpBankSupplier_Add.SelectedValue.ToString());
            string stkncc = txtNumAccSupplier_Add.Text.ToString();
            string tenncc = txtNameSupplier_Add.Text.ToString();
            string sdtncc = txtPhoneSupplier_Add.Text.ToString();            
            string dichincc = txtAddressSupplier_Add.Text.ToString();

            if(string.IsNullOrEmpty(tenncc)||string.IsNullOrEmpty(emailncc)||string.IsNullOrEmpty(dichincc)||string.IsNullOrEmpty(sdtncc))
            {
                CustomMessageBox2.show("Thông báo", "Bắt buộc nhập (*) ", false);
                return;
            }
            if (manhncc == 1)
            {
                txtNumAccSupplier_Add.Text = "0000000000000000";
                stkncc = "0000000000000000";
            }
            else if (stkncc.Length != 16)
            {
                CustomMessageBox1.show("Số tài khoản phải 16 ký tự");
                return;
            }
            NCCBus.Instance.AddNCC(tenncc, emailncc,sdtncc, dichincc, stkncc, manhncc, ttncc);

        }

        private void btnSearchSupplier_InfoSupf_Click(object sender, EventArgs e)
        {
            int mancc = 0;
          
            try
            {
                mancc = int.Parse(txtsearchSupplier_InfoSup.text.ToString());
            }
            catch
            {
               CustomMessageBox1.show("Mã nhà cung cấp không hợp lệ"); return;
            }
            Boolean result = NCCBus.Instance.SearchNCCKey(mancc, txtIdSupplier_InfoSup, txtNameSupplier_InfoSup,
                txtPhoneSupplier_InfoSup, metroBankSupplier_InfoSup, txtNumBankSupplier_InfoSup,
                txtEmailSupplier_InfoSup, txtAddressSupplier_InfoSup, DatepicNgayTao_Inf, swStatusSupplier_InfoSup,
                txtLoopSupplier_InfoSup, txtSumSupplier_InfoSup);

        }

        private void txtPhoneSupplier_Add_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        private void txtNumAccSupplier_Add_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        private void txtailSupplier_Add_Validating(object sender, CancelEventArgs e)
        {
            
        }

        private void btnUpdateSupplier_InfoSup_Click(object sender, EventArgs e)
        {
            btnSave_InfoSup.Enabled = true;
            btnUpdateSupplier_InfoSup.Enabled = false;
            txtNameSupplier_InfoSup.Enabled = txtPhoneSupplier_InfoSup.Enabled = metroBankSupplier_InfoSup.Enabled =
                txtNumBankSupplier_InfoSup.Enabled = txtEmailSupplier_InfoSup.Enabled =
                txtAddressSupplier_InfoSup.Enabled =
                swStatusSupplier_InfoSup.Enabled = true;

        }

        private void pnlInfoAccount_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnSave_InfoSup_Click(object sender, EventArgs e)
        {
            string tenncc = txtNameSupplier_InfoSup.Text.ToString();
            if (string.IsNullOrEmpty(tenncc))
            {
                CustomMessageBox1.show("Chưa nhập tên nhà cung cấp"); return;
            }
            int mancc = Convert.ToInt32(txtIdSupplier_InfoSup.Text.ToString());           
            string emailncc = txtEmailSupplier_InfoSup.Text.ToString();
            string match = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
                 @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
                 @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
            Regex regex = new Regex(match);
            if (!regex.IsMatch(emailncc))
            {
                CustomMessageBox1.show("Email không hợp lệ");
                return;
            }
            int manhncc = Convert.ToInt32(metroBankSupplier_InfoSup.SelectedValue);
            string stkncc = txtNumBankSupplier_InfoSup.Text.ToString();
            if (manhncc == 1)
            {
                txtNumBankSupplier_InfoSup.Text= "0000000000000000";
                 stkncc = "0000000000000000";
            }
            else if(stkncc.Length != 16)
            {
                CustomMessageBox1.show("Số tài khoản phải 12 ký tự");
                return;
            }
            string sdtncc = txtPhoneSupplier_InfoSup.Text.ToString();
            if (sdtncc.Length <8 || sdtncc.Length >11)
            {
                CustomMessageBox1.show("Số điện thoại không đúng định dạng"); return;
            }
          
            string diachincc = txtAddressSupplier_InfoSup.Text.ToString();
            if (string.IsNullOrEmpty(diachincc))
            {
                CustomMessageBox1.show("Chưa nhập địa chỉ nhà cung cấp"); return;
            }
                    
            int tinhtrangncc = Convert.ToInt32(swStatusSupplier_InfoSup.Value);
            Boolean result = NCCBus.Instance.UpdateNCC(mancc,tenncc,emailncc,
                sdtncc, stkncc, diachincc,manhncc, tinhtrangncc);
            //update fail
            if (!result)
            {
                return;
            }
            //update success
            btnSave_InfoSup.Enabled = false ;
            btnUpdateSupplier_InfoSup.Enabled = true;
            txtNameSupplier_InfoSup.Enabled = txtPhoneSupplier_InfoSup.Enabled = metroBankSupplier_InfoSup.Enabled =
                txtNumBankSupplier_InfoSup.Enabled = txtEmailSupplier_InfoSup.Enabled =
                txtAddressSupplier_InfoSup.Enabled = DatepicNgayTao_Inf.Enabled =
                swStatusSupplier_InfoSup.Enabled = false;

        }

        private void btnDelete_InfoSup_Click(object sender, EventArgs e)
        {
            int mancc = int.Parse(txtIdSupplier_InfoSup.Text.ToString());
            DialogResult confirm = CustomDialog1.show("Thông báo xác nhận", "Bạn có muốn xóa nhà cung cấp có mã là " + mancc + " không ?", "Có", "Không");
            if (confirm == DialogResult.No)
            {
                return;
            }
            if(!string.IsNullOrEmpty(mancc.ToString()))
            {
                if (NCCBus.Instance.DeleteNCC(mancc))
                {
                    txtsearchSupplier_InfoSup.text = txtsearchSupplier_InfoSup.Text = string.Empty;
                    refresh();
                }
            }
            
        }

        private void btnResetSupplier_Add_Click(object sender, EventArgs e)
        {
            txtsearchSupplier_InfoSup.text = txtsearchSupplier_InfoSup.Text = string.Empty;
            refresh();
           
        }
        public void refresh()
        {
            txtIdSupplier_InfoSup.Text = string.Empty;
            txtNameSupplier_InfoSup.Text = string.Empty;
            txtPhoneSupplier_InfoSup.Text = string.Empty;
            metroBankSupplier_InfoSup.DataSource = null;
            txtNumBankSupplier_InfoSup.Text = string.Empty;
            txtEmailSupplier_InfoSup.Text = string.Empty;
            txtAddressSupplier_InfoSup.Text = string.Empty;
            txtLoopSupplier_InfoSup.Text = string.Empty;
            txtSumSupplier_InfoSup.Text = string.Empty;
            swStatusSupplier_InfoSup.Enabled = false;
        }

        private void btnIn_SuppliersExcel_Click(object sender, EventArgs e)
        {
            if (datagidSuppliersExcel.CurrentRow != null)
            {
                for (int i = 0; i < datagidSuppliersExcel.SelectedRows.Count; i++)
                {
                    string mancc = datagidSuppliersExcel.SelectedRows[i].Cells[1].Value.ToString();
                    string tenncc = datagidSuppliersExcel.SelectedRows[i].Cells[2].Value.ToString();
                    try
                    {
                        var item = datagivSuppliersExcelPrint.Rows.Cast<DataGridViewRow>().First(r => r.Cells[1].Value.ToString().Equals(mancc));
                        if (item != null) continue;// đã tồn tại
                    }
                    catch { };

                    datagivSuppliersExcelPrint.Rows.Add(datagivSuppliersExcelPrint.RowCount+1, mancc, tenncc, datagidSuppliersExcel.CurrentRow.Cells[3].Value.ToString());
                    datagivSuppliersExcelPrint.Refresh();
                    UpdateSTTGrid(datagivSuppliersExcelPrint);
                }
            }
        }

        private void btnOutSuppliersExcel_Click(object sender, EventArgs e)
        {
            if (datagivSuppliersExcelPrint.CurrentRow != null)
            {
                foreach (DataGridViewRow row in datagivSuppliersExcelPrint.SelectedRows)
                {
                    if (!row.IsNewRow)
                    {
                        datagivSuppliersExcelPrint.Rows.Remove(row);
                    }
                    
                }
               
                UpdateSTTGrid(datagivSuppliersExcelPrint);
                datagivSuppliersExcelPrint.Refresh();
            }

        }
        private void UpdateSTTGrid(DataGridView gridView)
        {
            for (int i = 0; i < gridView.RowCount; i++)
            {
                gridView.Rows[i].Cells[0].Value = i + 1;

            }
        }

        private void btnRefeshExcel_Ex_Click(object sender, EventArgs e)
        {
            datagivSuppliersExcelPrint.DataSource = null;
            datagivSuppliersExcelPrint.Rows.Clear();
        }

        private void datatableNccs_Nccs_FilterStringChanged(object sender, EventArgs e)
        {
            bindingSource1.Filter = datatableNccs_Nccs.FilterString;
            UpdateSTTGrid(datatableNccs_Nccs);
        }

        private void datatableNccs_Nccs_SortStringChanged(object sender, EventArgs e)
        {
            bindingSource1.Sort = datatableNccs_Nccs.SortString;
            UpdateSTTGrid(datatableNccs_Nccs);
        }

        private void datagidSuppliersExcel_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void datagidSuppliersExcel_FilterStringChanged(object sender, EventArgs e)
        {
            bindingSource1.Filter = datagidSuppliersExcel.FilterString;
            UpdateSTTGrid(datagidSuppliersExcel);
        }

        private void datagidSuppliersExcel_SortStringChanged(object sender, EventArgs e)
        {
            bindingSource1.Sort = datagidSuppliersExcel.SortString;
            UpdateSTTGrid(datagidSuppliersExcel);
        }
        //
        private void RemoveFilterAndSortBindingSource(BindingSource bin, DataGridView gv)
        {
            bin.Filter = bin.Sort = null;
            bin.DataSource = null;
            bin.DataSource = gv.DataSource;
        }

        private void txtPhoneSupplier_InfoSup_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        private void txtNumBankSupplier_InfoSup_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        private void frmSupplier_Load(object sender, EventArgs e)
        {
            block_menu();
        }

        private void btnPrint_Ex_Click(object sender, EventArgs e)
        {

        }
        //
    }
}
