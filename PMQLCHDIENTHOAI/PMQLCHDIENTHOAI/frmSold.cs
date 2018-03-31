using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BUS;
using Excel = Microsoft.Office.Interop.Excel;
using PMQLCHDIENTHOAI.Data;
using LiveCharts.Wpf;

namespace PMQLCHDIENTHOAI
{
    public partial class frmSold : Form
    {
        public int flat = 0;
        public int tabindex = 1;
        public string IDTK = "";
        public frmSold()
        {
            InitializeComponent();
            
            openTabPage(tabindex);
        }

        public void blockMenu()
        {
            btnNewSolds.Enabled = TaiKhoanBUS.Instance.checkRoleUser(IDTK, Properties.Settings.Default.cnXemDSSold);
            btnAddNewSold.Enabled= TaiKhoanBUS.Instance.checkRoleUser(IDTK, Properties.Settings.Default.cnLapSold);
            btnNewSoldInfo.Enabled= TaiKhoanBUS.Instance.checkRoleUser(IDTK, Properties.Settings.Default.cnXemDetailSold);
            btnExcel.Enabled= TaiKhoanBUS.Instance.checkRoleUser(IDTK, Properties.Settings.Default.cnXuatSold);
            btnSaveSold_Info.Enabled = TaiKhoanBUS.Instance.checkRoleUser(IDTK, Properties.Settings.Default.cnCapNhatSold);
        }
        public void openTabPage(int tab)
        {
            tabSold.TabPages.Clear();
            switch (tab)
            {
                case 1://tab home
                    tabSold.TabPages.Add(tabPageHome);
                    break;
                case 2://tab add Sold
                    tabSold.TabPages.Add(tabPageAddSold);
                    datatableDeciveSold_Add.RowsDefaultCellStyle.BackColor = Color.White;
                    datatableDeciveSold_Add.AlternatingRowsDefaultCellStyle.BackColor = Color.LightBlue;
                    break;
                case 3://tab Solds
                    tabSold.TabPages.Add(tabPageSolds);
                    datatableSold_Solds.RowsDefaultCellStyle.BackColor = Color.White;
                    datatableSold_Solds.AlternatingRowsDefaultCellStyle.BackColor = Color.LightBlue;
                    datatableCTSold_Solds.RowsDefaultCellStyle.BackColor = Color.White;
                    datatableCTSold_Solds.AlternatingRowsDefaultCellStyle.BackColor = Color.LightBlue;
                    datebtnToSold_Solds.Value = datebtnFromSold_Solds.Value = DateTime.Now.Date;
                    break;
                case 4://tab Info Sold
                    tabSold.TabPages.Add(tabPageInfoSold);
                     datatableDeSold_info.RowsDefaultCellStyle.BackColor = Color.White;
                    datatableDeSold_info.AlternatingRowsDefaultCellStyle.BackColor = Color.LightBlue;
                    break;
                case 5://tab Search Solds
                    tabSold.TabPages.Add(tabPageSearchSolds);
                    break;
                case 6://tab Excel
                    tabSold.TabPages.Add(tabPageExcel);
                    dataTableSoldsExcel.RowsDefaultCellStyle.BackColor = Color.White;
                    dataTableSoldsExcel.AlternatingRowsDefaultCellStyle.BackColor = Color.LightBlue;
                    datatableout_Ex.RowsDefaultCellStyle.BackColor = Color.White;
                    datatableout_Ex.AlternatingRowsDefaultCellStyle.BackColor = Color.LightBlue;
                    break;
                case 7:
                    tabSold.TabPages.Add(tabpageChart);break;
                default://khong co quyen
                    break;
            }
        }

        //private void groupBox3_Enter(object sender, EventArgs e)
        //{

        //}

        #region Back
        private void btnAddBack_Click(object sender, EventArgs e)
        {
            openTabPage(1);

        }

        private void btnInfoBack_Click(object sender, EventArgs e)
        {
            openTabPage(1);
            refeshSoldSInfo();
            txtsearchSold_Info.text = "";
        }

        private void btnExcelBack_Click(object sender, EventArgs e)
        {
            openTabPage(1);
        }

        private void btnSearchBack_Click(object sender, EventArgs e)
        {
            openTabPage(1);
        }

        private void btnSoldsBack_Click(object sender, EventArgs e)
        {
            openTabPage(1);
            bindingSourceHDB.Filter = null;
            bindingSourceHDB.Sort = null;
            datatableSold_Solds.DataSource = null;
            datatableSold_Solds.Rows.Clear();
        }
        private void tabPayBack_Click(object sender, EventArgs e)
        {
            openTabPage(1);
        }
        #endregion Back

        #region Menu 
        

        private void btnAddNewSold_Click(object sender, EventArgs e)
        {
            openTabPage(2);
            //load ds khách hàng lên combobox
            HoaDonBanBus.Instance.loadDSKhachHangHDB(cbIDCustomerSold_Add);
            //load ds thiết bị lên combobox
            ThietBiBus.Instance.LoadTBIDNameHDB(cbIDDeviceSold_Add);
            //load ds phương thức giao hàng lên combobox
            HoaDonBanBus.Instance.loadDSPTGHHDB(drocbpHTGHCustomerSold_Add);
            //load ds trạng thái hdm lên combobox
            HoaDonBanBus.Instance.loadTinhTrangHDB(cbStatusSold_Add);
            //load ds lý do xuất hdm lên combobox
            HoaDonBanBus.Instance.loadLyDoHDB(cbLyDoSold_Add);
            phigiaohang();
            cbIDCustomerSold_Add.Enabled = false;
            refesh_sold_Add();
            txtDateSold_Add.Text = DateTime.Now.Date.ToString("dd/MM/yyyy");
            txtNumSold_Add.Value = 1; txtSumSold_Add.Text = "0";
        }

        private void btnNewSolds_Click(object sender, EventArgs e)
        {
            openTabPage(3);
            HoaDonBanBus.Instance.loadDSHDB(datatableSold_Solds);
            bindingSourceHDB.Filter = null;
            bindingSourceHDB.Sort = null;
            bindingSourceHDB.DataSource = null;
            bindingSourceHDB.DataSource = datatableSold_Solds.DataSource;

        }

        private void btnNewSoldInfo_Click(object sender, EventArgs e)
        {
            refeshSoldSInfo();
            txtsearchSold_Info.text = string.Empty;
            openTabPage(4);
            
            HoaDonBanBus.Instance.loadTinhTrangHDB(dropStatusSold_Info);
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            openTabPage(6);
            HoaDonBanBus.Instance.loadDSHDB(dataTableSoldsExcel);
            bindingSourceHDB.Filter = bindingSourceHDB.Sort = null;
            bindingSourceHDB.DataSource = null;
            bindingSourceHDB.DataSource = dataTableSoldsExcel.DataSource;
            datatableout_Ex.DataSource = null;
            datatableout_Ex.Rows.Clear();
            foreach (DataGridViewColumn col in dataTableSoldsExcel.Columns)
                datatableout_Ex.Columns.Add(col.Name, col.HeaderText);
        }

        private void btnSearchNewSolds_Click(object sender, EventArgs e)
        {
            openTabPage(5);
            bindingSourceHDB.Filter = null;
            bindingSourceHDB.Sort = null;
        }
        #endregion

        private void btnAddPTGH_Add_Click(object sender, EventArgs e)
        {
            frmPTGH frmptgh = new frmPTGH();
            frmptgh.ShowDialog();
            HoaDonBanBus.Instance.loadDSPTGHHDB(drocbpHTGHCustomerSold_Add);
            if (drocbpHTGHCustomerSold_Add.Items.Count > 0)
            {

                drocbpHTGHCustomerSold_Add.SelectedIndex = 0;
            }          

            frmptgh.IDTK = IDTK;
            frmptgh.ShowDialog();
        }

        private void btnAddPTGH_Info_Click(object sender, EventArgs e)
        {
            frmPTGH frmptgh = new frmPTGH();
            frmptgh.ShowDialog();

        }

        private void frmSold_Load(object sender, EventArgs e)
        {
            blockMenu();
        }

        private void btnNewKH_Add_Click(object sender, EventArgs e)
        {

            if (txtNameCustomerSold_Add.Enabled)
            {  //khach hang cu
                cbIDCustomerSold_Add.Enabled = true;
                loadthongtinkhachhang();
                txtNameCustomerSold_Add.Enabled = txtPhoneCustomerSold_Add.Enabled = false;

            }
            else
            { //khach hang moi
                cbIDCustomerSold_Add.Enabled = false;
                txtNameCustomerSold_Add.Enabled = txtPhoneCustomerSold_Add.Enabled = true;
                txtNameCustomerSold_Add.Text = txtPhoneCustomerSold_Add.Text = txtAdressCustomerSold_Add.Text = string.Empty;
            }
            
        }

        private void cbIDDeviceSold_Add_SelectedValueChanged(object sender, EventArgs e)
        {
            if (cbIDDeviceSold_Add.SelectedItem != null)
            {
                try
                {
                    string matb = cbIDDeviceSold_Add.SelectedValue.ToString();
                    HoaDonBanBus.Instance.loadTBByKey(matb, txtTBTon_Add, txtPriceSold_Add, txtKMSold_Add);
                    txtNumSold_Add.Maximum = Convert.ToInt32(txtTBTon_Add.Text.ToString());
                    if (txtNumSold_Add.Maximum != 0) txtNumSold_Add.Value = 1;
                    int soluongban = Convert.ToInt32(txtNumSold_Add.Value);
                    txtGiaMoi_Add.Text =string.Format("{0:#,##0}", double.Parse(txtPriceSold_Add.Text.ToString()));
                    double giamua = double.Parse(txtGiaMoi_Add.Text.ToString());
                    if (string.IsNullOrEmpty(txtKMSold_Add.Text.ToString())){
                        txtKMSold_Add.Text = "0";
                    }
                    double khuyenmai = double.Parse(txtKMSold_Add.Text.ToString()) *giamua;
                    if (!string.IsNullOrEmpty(txtGiaMoi_Add.Text))
                    {
                        txtSumDSold_Add.Text = string.Format("{0:#,##0}", (soluongban * giamua - khuyenmai));

                    }
                }
                catch
                {
                    txtTBTon_Add.Text = txtGiaMoi_Add.Text = txtSumDSold_Add.Text = "0";
                }


            }
            else
            {
                txtGiaMoi_Add.Text = txtTBTon_Add.Text = txtGiaMoi_Add.Text = txtSumDSold_Add.Text = "0";

            }
        }

        private void txtKMSold_Add_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        private void txtGiaMoi_Add_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        private void txtPhoneCustomerSold_Add_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(txtPhoneCustomerSold_Add.Text.Length >= 11)
            {
                e.Handled = true;
            }
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtNumSold_Add_ValueChanged(object sender, EventArgs e)
        {
            valuesChange();
        }

        private void valuesChange()
        {
            if (cbIDDeviceSold_Add.SelectedItem != null)
            {

                try
                {
                    int soluongban = Convert.ToInt32(txtNumSold_Add.Value);
                    txtGiaMoi_Add.Text = txtPriceSold_Add.Text.ToString();

                    double giamua = double.Parse(txtGiaMoi_Add.Text.ToString());
                    if (string.IsNullOrEmpty(txtKMSold_Add.Text.ToString()))
                    {
                        txtKMSold_Add.Text = "0";
                    }
                    double khuyenmai = double.Parse(txtKMSold_Add.Text.ToString()) / 100;
                    if (!string.IsNullOrEmpty(txtGiaMoi_Add.Text))
                    {
                        txtSumDSold_Add.Text = (soluongban * giamua - khuyenmai).ToString();
                        txtSumDSold_Add.Text = string.Format("{0:#,##0.00}", double.Parse(txtSumDSold_Add.Text));

                    }
                }
                catch
                {
                    txtGiaMoi_Add.Text = txtTBTon_Add.Text = txtPriceSold_Add.Text = txtSumDSold_Add.Text = string.Empty;
                }


            }
            else
            {
                txtGiaMoi_Add.Text = txtTBTon_Add.Text = txtPriceSold_Add.Text = txtSumDSold_Add.Text = string.Empty;

            }
        }
        private void txtKMSold_Add_OnValueChanged(object sender, EventArgs e)
        {
            valuesChange();
        }

        private void txtGiaMoi_Add_OnValueChanged(object sender, EventArgs e)
        {
            valuesChange();
        }

        private void cbIDCustomerSold_Add_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadthongtinkhachhang();
        }
        private void loadthongtinkhachhang()
        {
            //!txtNameCustomerSold_Add.Enabled &&
            if (cbIDCustomerSold_Add.SelectedItem != null)
            {
                int makh = 0;
                try
                {
                    makh = Convert.ToInt32(cbIDCustomerSold_Add.SelectedValue.ToString());
                    HoaDonBanBus.Instance.loadThongTinKHHDB(makh,
                         txtNameCustomerSold_Add, txtAdressCustomerSold_Add, txtPhoneCustomerSold_Add);
                }
                catch
                {
                    txtNameCustomerSold_Add.Text = txtPhoneCustomerSold_Add.Text = txtAdressCustomerSold_Add.Text = string.Empty;
                }

            }
        }

        private void txtPhuthuCustomerSold_Add_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void btnAddSold_Add_Click(object sender, EventArgs e)
        {
            if (cbIDDeviceSold_Add.SelectedItem != null)
            {
                int slton = Convert.ToInt32(txtTBTon_Add.Text.ToString());
                if (slton == 0)
                {
                    CustomMessageBox1.show("Đã hết hàng");
                    return;
                }
                string matb = cbIDDeviceSold_Add.SelectedValue.ToString();
                if (datatableDeciveSold_Add.Rows.Count > 0)
                {
                    try
                    {
                        var item = datatableDeciveSold_Add.Rows.Cast<DataGridViewRow>().First(r => r.Cells[1].Value.ToString().Equals(matb));
                        //đã được thêm vào datagridview
                        if (item != null)
                        {
                            return;
                        }
                    }
                    catch { }

                }

                int soluong = Convert.ToInt32(txtNumSold_Add.Value);
                
                double giab = 0;
                if (txtGiaMoi_Add.Text.ToString() != "")
                {
                    giab = double.Parse(txtGiaMoi_Add.Text.ToString());
                }
                double kmai = double.Parse(txtKMSold_Add.Text.ToString());
                double thanhtien = double.Parse(txtSumDSold_Add.Text.ToString());
                if (ThietBiBus.Instance.AddTBByKeyToHDB(matb, datatableDeciveSold_Add, giab, kmai, soluong, thanhtien))
                {
                    tinhtonghd();

                }
            }

        }

        //tỉnh tổng của một cột trong gridview
        private double sumGridView(DataGridView gv,int indexCol)
        {
            double sum = 0;
            for (int i = 0; i < gv.RowCount; i++)
            {
                sum += double.Parse(gv.Rows[i].Cells[indexCol].Value.ToString());
            }
            return sum;
        }
        public void tinhtonghd()
        {
            double sum = sumGridView(datatableDeciveSold_Add,8);
            //string.Format("{0:#,##0.00}", double.Parse(txtFreePriceBuy_Add.Text))
            txtSumSold_Add.Text = string.Format("{0:#,##0}", sum);
            double phuthu = 0;
            if (txtPhuthuCustomerSold_Add.Text !=null)
                phuthu = double.Parse(txtPhuthuCustomerSold_Add.Text.ToString());
            double phivc = double.Parse(lblPhiVC.Text.ToString());
            sum +=phuthu+phivc;
            txtSumPayBillAdd.Text = string.Format("{0:#,##0}", sum);

        }

        private void drocbpHTGHCustomerSold_Add_SelectedIndexChanged(object sender, EventArgs e)
        {
            phigiaohang();
        }

      private void phigiaohang()
        {
            if (drocbpHTGHCustomerSold_Add.SelectedItem != null)
            {
                int mapt = 2;
                try
                {
                    mapt = Convert.ToInt32(drocbpHTGHCustomerSold_Add.SelectedValue.ToString());
                    HoaDonBanBus.Instance.phivanchuyen(mapt, lblPhiVC);
                }
                catch
                {
                    lblPhiVC.Text = "0";
                }

            }
        }
        private void btnSaveSold_Add_Click(object sender, EventArgs e)
        {
            if (datatableDeciveSold_Add.Rows.Count <= 0) { CustomMessageBox1.show("Chưa có thiết bị trong hóa đơn"); return; }
            int makh = 1;
            if (!txtNameCustomerSold_Add.Enabled)
            {
                makh = Convert.ToInt32(cbIDCustomerSold_Add.SelectedValue);
            }
            if (cbStatusSold_Add.SelectedValue.ToString().Equals("0"))
            {
                CustomMessageBox1.show("Hóa đơn không thể lập");
                return;
            }
            string tenkh = txtNameCustomerSold_Add.Text.ToString();
            string sdt = txtPhoneCustomerSold_Add.Text.ToString();
            string diachi = txtAdressCustomerSold_Add.Text.ToString();
            if (sdt.Length <9 || sdt.Length >11)
            {
                CustomMessageBox.show("Thông báo", "Số điện thoại không hợp lệ", false);
                return;
            }

            if (string.IsNullOrEmpty(tenkh) || string.IsNullOrEmpty(sdt) || string.IsNullOrEmpty(diachi))
            {
                CustomMessageBox.show("Thông báo", "Vui lòng điền đầy đủ thông tin khách hàng", false);
                return;
            }
            Boolean result = false;

            result = HoaDonBanBus.Instance.addHDB(out string mhdb,IDTK, makh, tenkh, sdt, diachi, txtPhuthuCustomerSold_Add, double.Parse(lblPhiVC.Text),
                drocbpHTGHCustomerSold_Add, cbStatusSold_Add, cbLyDoSold_Add,
                txtSumPayBillAdd, datatableDeciveSold_Add, txtThanhToanSold_Add);

            //them k thanh cong
            if (!result) return;

            DialogResult dialogResult = CustomDialog1.show("Thông báo", "Bạn có muốn xuất hóa đơn", "Có", "Không");
            if (dialogResult == DialogResult.OK)
            {
                
                frmXuatHDB frm = new frmXuatHDB();
                frm.mahdb = mhdb;
                frm.key = 0;
                frm.ShowDialog();
            }
            refesh_sold_Add();
            
        }
        private void refesh_sold_Add()
        {
            txtNameCustomerSold_Add.Text = txtPhoneCustomerSold_Add.Text = txtAdressCustomerSold_Add.Text = string.Empty;
            txtPhuthuCustomerSold_Add.Text = txtSumPayBillAdd.Text= txtThanhToanSold_Add.Text= "0";
            datatableDeciveSold_Add.DataSource = null;
            datatableDeciveSold_Add.Rows.Clear();
        }
        private void UpdateSTTGrid(DataGridView gridView)
        {
            for (int i = 0; i < gridView.RowCount; i++)
            {
                gridView.Rows[i].Cells[0].Value = i + 1;

            }
        }
        private void btnDeleteSold_Add_Click(object sender, EventArgs e)
        {
            if (datatableDeciveSold_Add.CurrentRow != null)
            {
                try
                {
                    datatableDeciveSold_Add.Rows.Remove(datatableDeciveSold_Add.CurrentRow);
                    UpdateSTTGrid(datatableDeciveSold_Add);
                    tinhtonghd();
                    CustomAlert.Show("Xóa thành công!");

                }
                catch { }
            }
            else CustomAlert.Show("Chưa chọn thiết bị");
        }

        private void btnRefeshSold_Add_Click(object sender, EventArgs e)
        {
            if (datatableDeciveSold_Add.RowCount <= 0) return;
            DialogResult result = CustomDialog1.show("Xác nhận", "Xóa danh sách để lập mới ?", "Chấp nhận", "Trở lại");
            if (result == DialogResult.No)
            {
                return;//hủy thao tác
            }

            datatableDeciveSold_Add.DataSource = null;
            datatableDeciveSold_Add.Rows.Clear();
            tinhtonghd();
        }

        private void txtPhuthuCustomerSold_Add_OnValueChanged(object sender, EventArgs e)
        {
            if (txtPhuthuCustomerSold_Add.Text == string.Empty)
                txtPhuthuCustomerSold_Add.Text = "0";
            if (datatableDeciveSold_Add.RowCount <= 0) return;
            double tong = double.Parse(txtSumPayBillAdd.Text) + double.Parse(txtPhuthuCustomerSold_Add.Text);
            txtSumPayBillAdd.Text = tong.ToString();
        }

        private void btnUpdateDeviceSold_Add_Click(object sender, EventArgs e)
        {
            if (datatableDeciveSold_Add.SelectedRows != null && datatableDeciveSold_Add.RowCount >0)
            {
                try
                {
                    string matb = datatableDeciveSold_Add.CurrentRow.Cells[1].Value.ToString();
                    cbIDDeviceSold_Add.SelectedValue = matb.ToString();
                    
                    int soluong = Convert.ToInt32(datatableDeciveSold_Add.CurrentRow.Cells[3].Value.ToString());
                    

                    txtNumSold_Add.Value = soluong;
                    txtGiaMoi_Add.Text = datatableDeciveSold_Add.CurrentRow.Cells[6].Value.ToString();
                    btnSaveUpdateSold_Add.Enabled = true;
                    //khoa chuc nang
                    btnUpdateDeviceSold_Add.Enabled = btnRefeshSold_Add.Enabled = btnAddSold_Add.Enabled = btnSaveSold_Add.Enabled = false;
                }
                catch { }
            }
            else
            {
                CustomAlert.Show("Chưa có thiết bị");
            }
        }

        private void btnSaveUpdateSold_Add_Click(object sender, EventArgs e)
        {
            //mở chuc nang
            btnUpdateDeviceSold_Add.Enabled = btnRefeshSold_Add.Enabled
                = btnAddSold_Add.Enabled = btnSaveSold_Add.Enabled = true;

            //thuc thi lưu cập nhật
            if (cbIDDeviceSold_Add.SelectedItem != null)
            {
                try
                {
                    tinhtonghd();
                    int index = datatableDeciveSold_Add.CurrentRow.Index;
                    datatableDeciveSold_Add.Rows[index].Cells[3].Value = txtNumSold_Add.Value;
                    datatableDeciveSold_Add.Rows[index].Cells[5].Value =double.Parse(txtGiaMoi_Add.Text.ToString());
                    datatableDeciveSold_Add.Rows[index].Cells[8].Value =double.Parse(txtSumDSold_Add.Text.ToString());                  
                    CustomAlert.Show("Cập nhật thành công");
                }
                catch
                {
                    CustomAlert.Show("Cập nhật k thành công");
                }

            }
            //khoa chuc nang
            btnSaveUpdateSold_Add.Enabled = false;
        }

        private void txtPriceSold_Add_OnValueChanged(object sender, EventArgs e)
        {
            txtPriceSold_Add.Text=string.Format("{0:#,##0.00}", txtPriceSold_Add.Text.ToString());
        }

        private void txtSumPayBillAdd_OnValueChanged(object sender, EventArgs e)
        {
            txtThanhToanSold_Add.Text = string.Format("{0:#,##0.00}", txtSumPayBillAdd.Text.ToString());
        }

        private void txtThanhToanSold_Add_OnValueChanged(object sender, EventArgs e)
        {
            if (txtThanhToanSold_Add.Text == string.Empty)
                txtThanhToanSold_Add.Text = "0";
                try
                {

                    if (double.Parse(txtThanhToanSold_Add.Text) > double.Parse(txtSumPayBillAdd.Text))
                    {
                        CustomMessageBox1.show("Tiền thanh toán không vượt tổng tiền phải thanh toán");
                        txtThanhToanSold_Add.Text = string.Format("{0:#,##0.00}", double.Parse(txtSumPayBillAdd.Text));
                    }
                    else
                    {
                        txtThanhToanSold_Add.Text = string.Format("{0:#,##0.00}", double.Parse(txtThanhToanSold_Add.Text));

                    }                    
                }
                catch { }

   
        }

        private void datatableSold_Solds_SortStringChanged(object sender, EventArgs e)
        {
            bindingSourceHDB.Sort = datatableSold_Solds.SortString;
            UpdateSTTGrid(datatableSold_Solds);
            tinhDSHDB();
        }

        private void datatableSold_Solds_FilterStringChanged(object sender, EventArgs e)
        {
            bindingSourceHDB.Filter = datatableSold_Solds.FilterString;
            UpdateSTTGrid(datatableSold_Solds);
            tinhDSHDB();
        }

        private void btnDetailSold_Solds_Click(object sender, EventArgs e)
        {
            if(datatableSold_Solds.SelectedRows != null && datatableSold_Solds.RowCount >0)
            {
                lblmaHDBSolds.Text = datatableSold_Solds.SelectedRows[0].Cells[1].Value.ToString();
                HoaDonBanBus.Instance.loadDSCTHDBByMADHB(datatableSold_Solds.SelectedRows[0].Cells[1].Value.ToString(), datatableCTSold_Solds);
            }
           
        }

        private void btnAllSold_Solds_Click(object sender, EventArgs e)
        {
            HoaDonBanBus.Instance.loadDSHDB(datatableSold_Solds);
            bindingSourceHDB.Filter = null;
            bindingSourceHDB.Sort = null;
            bindingSourceHDB.DataSource = null;
            bindingSourceHDB.DataSource = datatableSold_Solds.DataSource;
        }
        private void btnNgaySold_Solds_Click(object sender, EventArgs e)
        {
            //
            if (datebtnFromSold_Solds.Value > datebtnToSold_Solds.Value)
            {
                CustomMessageBox1.show("Giá trị thời gian ngược");
                return;
            }
            HoaDonBanBus.Instance.loadDSHDBNgay(datebtnFromSold_Solds, datebtnToSold_Solds, datatableSold_Solds);
            bindingSourceHDB.Filter = null;
            bindingSourceHDB.Sort = null;
            bindingSourceHDB.DataSource = null;
            
            bindingSourceHDB.DataSource = datatableSold_Solds.DataSource;
        }
        private void btnNOSold_Solds_Click(object sender, EventArgs e)
        {
            HoaDonBanBus.Instance.loadDSHDBNo(datatableSold_Solds);
            bindingSourceHDB.Filter = null;
            bindingSourceHDB.Sort = null;
            bindingSourceHDB.DataSource = null;
            bindingSourceHDB.DataSource = datatableSold_Solds.DataSource;
        }
        private void btnThanhToanSold_Solds_Click(object sender, EventArgs e)
        {
            HoaDonBanBus.Instance.loadDSHDBThanhToan(datatableSold_Solds);
            bindingSourceHDB.Filter = null;
            bindingSourceHDB.Sort = null;
            bindingSourceHDB.DataSource = null;
            bindingSourceHDB.DataSource = datatableSold_Solds.DataSource;
        }
        private void datatableSold_Solds_DataSourceChanged(object sender, EventArgs e)
        {
            tinhDSHDB();
        }

        private void bindingSourceHDB_DataSourceChanged(object sender, EventArgs e)
        {
            tinhDSHDB();
        }

        private void tinhDSHDB()
        {
            lblSoLuongSolds.Text = datatableSold_Solds.RowCount.ToString();
            double tonghd = sumGridView(datatableSold_Solds, 9);
            double dathanhtoan = sumGridView(datatableSold_Solds, 10);
            lblTongHDSolds.Text = string.Format("{0:#,##0}",tonghd);
            lblDaThanhToanSolds.Text = string.Format("{0:#,##0}", dathanhtoan);
            lblTongPhiGiaoSolds.Text = string.Format("{0:#,##0}", sumGridView(datatableSold_Solds, 7));
            lblTongNoSolds.Text = string.Format("{0:#,##0}", tonghd-dathanhtoan);
            lblTongPhuThuSolds.Text = string.Format("{0:#,##0}", sumGridView(datatableSold_Solds, 8));
        }
        private void bunifuCustomLabel38_Click(object sender, EventArgs e)
        {

        }

        private void btnSearchSold_Info_Click(object sender, EventArgs e)
        {
            string mahd = txtsearchSold_Info.text.ToString().ToUpper();
            if (string.IsNullOrEmpty(mahd))
            {
                CustomMessageBox1.show("Vui lòng nhập mã hóa đơn bán!");
                return;
            }
            if (mahd.Length > 10)
            {
                CustomMessageBox1.show("Không tồn tại mã hóa đơn này"); refeshSoldSInfo();
                return;
            }
            Boolean result = HoaDonBanBus.Instance.searchHDBByKey(mahd, txtIdCusSold_Info, txtNameCusSold_Info, txtphoneCusSold_Info,
                txtHTGHSold_Info, txtPhiGHSold_Info, txtAdressCusSold_Info, txtPhuThuSold_Info,
                txtLyDoSold_Info, txtNoSold_Info, datatableDeSold_info, txtdateSold_info, txtSumPayBillSold_info, txtSumSold_info, dropStatusSold_Info);
            IdSold_Info.Text = mahd;
            if (result == false)
            {
                refeshSoldSInfo();
            }
        }

        private void refeshSoldSInfo()
        {
           
                txtIdCusSold_Info.Text = txtNameCusSold_Info.Text = txtphoneCusSold_Info.Text =
                txtHTGHSold_Info.Text = txtAdressCusSold_Info.Text =
                txtLyDoSold_Info.Text = txtdateSold_info.Text = gr.Text =
                txtSumPayBillSold_info.Text = IdSold_Info.Text = string.Empty;
                txtPhuThuSold_Info.Text = txtPhiGHSold_Info.Text = txtNoSold_Info.Text = txtSumSold_info.Text = "0";
                datatableDeSold_info.DataSource = null;
                datatableDeSold_info.Rows.Clear();
                dropStatusSold_Info.Enabled = true;


        }
    

        private void btnSaveSold_Info_Click(object sender, EventArgs e)
        {
            if(!dropStatusSold_Info.Enabled)
            {
                CustomMessageBox.show("Thông báo", "Hóa đơn đã giao hoặc hủy.Không thể sửa", false);
                return;
            }

            if (dropStatusSold_Info.Enabled && datatableDeSold_info.RowCount >0)
            {
                string mahd = IdSold_Info.Text.ToString();
                Byte tinhtrang = Byte.Parse(dropStatusSold_Info.SelectedValue.ToString());
                Boolean result = HoaDonBanBus.Instance.updateHDB(mahd, tinhtrang,double.Parse(txtThanhToanSold_Info.Text.ToString()));
                if (result == true)
                {
                    if (tinhtrang == 0 || tinhtrang == 1)
                    {
                        dropStatusSold_Info.Enabled  = false;

                    }
                    else
                    {
                        dropStatusSold_Info.Enabled = true;
                    }
                    
                }
            }
        }

        private void txtThanhToanSold_Info_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        private void datatableDeSold_info_DataSourceChanged(object sender, EventArgs e)
        {
            if(datatableDeSold_info.RowCount > 0)
            {
                txtNumDeSold_Info.Text = datatableDeSold_info.RowCount.ToString();
            }
            else
                txtNumDeSold_Info.Text = "0";

        }

        private void txtThanhToanSold_Info_OnValueChanged(object sender, EventArgs e)
        {
            if (txtThanhToanSold_Info.Text == string.Empty)
                txtThanhToanSold_Info.Text = "0";
            try
                {

                    if (double.Parse(txtThanhToanSold_Info.Text) > double.Parse(txtNoSold_Info.Text))
                    {
                        CustomMessageBox1.show("Tiền thanh toán không vượt số còn nợ");
                        txtThanhToanSold_Info.Text = string.Format("{0:#,##0.00}", double.Parse(txtNoSold_Info.Text));
                    }
                    else
                    {
                        txtThanhToanSold_Info.Text = string.Format("{0:#,##0.00}", double.Parse(txtThanhToanSold_Info.Text));

                    }
                }
                catch { }

          
        }

        private void txtThanhToanSold_Add_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        private void dataTableSoldsExcel_FilterStringChanged(object sender, EventArgs e)
        {
            bindingSourceHDB.Filter = dataTableSoldsExcel.FilterString;
            UpdateSTTGrid(dataTableSoldsExcel);
            
        }

        private void dataTableSoldsExcel_SortStringChanged(object sender, EventArgs e)
        {
            bindingSourceHDB.Sort = dataTableSoldsExcel.SortString;
            UpdateSTTGrid(dataTableSoldsExcel);
        }

        private void btnInEx_Excel_Click(object sender, EventArgs e)
        {
            if (dataTableSoldsExcel.SelectedRows != null)
                {
                    foreach (DataGridViewRow row in dataTableSoldsExcel.SelectedRows)
                    {
                        string mahd = row.Cells[1].Value.ToString();
                        try
                        {
                            var item = datatableout_Ex.Rows.Cast<DataGridViewRow>().First(r => r.Cells[1].Value.ToString().Equals(mahd));
                            if (item != null) continue;//da ton tai
                        }
                        catch { };
                        var newRow = (DataGridViewRow)row.Clone();

                        foreach (DataGridViewCell cell in row.Cells)
                            newRow.Cells[cell.ColumnIndex].Value = cell.Value;

                        datatableout_Ex.Rows.Add(newRow);

                        datatableout_Ex.Refresh();
                    }

                    UpdateSTTGrid(datatableout_Ex);
                }
            
        }

        private void btnOutEx_Excel_Click(object sender, EventArgs e)
        {
            if (datatableout_Ex.CurrentRow != null)
            {
                
                foreach (DataGridViewRow row in datatableout_Ex.SelectedRows)
                {
                    if (!row.IsNewRow)
                    {
                        datatableout_Ex.Rows.Remove(row);
                    }

                }
                UpdateSTTGrid(datatableout_Ex);
                datatableout_Ex.Refresh();
            }
        }

        private void btnResetEx_Excel_Click(object sender, EventArgs e)
        {
            datatableout_Ex.DataSource = null;
            datatableout_Ex.Rows.Clear();
        }

        private void btnPrintSold_Info_Click(object sender, EventArgs e)
        {
            string mahd = IdSold_Info.Text.ToString();
            if (string.IsNullOrEmpty(mahd)) { return; }

            frmXuatHDB frm = new frmXuatHDB();
            frm.mahdb = mahd;
            frm.ShowDialog();
        }

        private void btnPrintEx_Excel_Click(object sender, EventArgs e)
        {
            if (datatableout_Ex.Rows.Count == 0) return;
            object misValue = System.Reflection.Missing.Value;
            Excel.Application app = new Excel.Application();
            app.Visible = false;
            Excel.Workbook workbook = app.Workbooks.Add(Excel.XlWBATemplate.xlWBATWorksheet);
            Excel.Worksheet worksheet = (Excel.Worksheet)workbook.ActiveSheet;
            worksheet.Name = "DS";

            ///name
            worksheet.Cells[1, 1] = "Danh sách hóa đơn bán";
            worksheet.Cells[1, 1].EntireRow.Font.Bold = true;
            Excel.Range range = worksheet.Range[worksheet.Cells[1, 1], worksheet.Cells[1, datatableout_Ex.Columns.Count]];
            range.Merge(true);
            range.Interior.ColorIndex = 36;
            range.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Red);
            range.Font.Size = 16;
            range.HorizontalAlignment = 3;
            range.VerticalAlignment = 3;
            ///header 

            for (int j = 0; j < datatableout_Ex.Columns.Count; j++)
            {

                worksheet.Cells[3, j + 1] = datatableout_Ex.Columns[j].HeaderText.ToString();

            }
            Excel.Range rangeHeader = worksheet.Range[worksheet.Cells[3, 1], worksheet.Cells[3, datatableout_Ex.Columns.Count]];
            rangeHeader.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Red);
            rangeHeader.Font.Size = 13;
            rangeHeader.Font.Bold = true;
            ///content

            for (int i = 0; i < datatableout_Ex.Rows.Count; i++)
            {
                for (int j = 0; j < datatableout_Ex.Columns.Count; j++)
                {
                    worksheet.Cells[i + 4, j + 1] = datatableout_Ex.Rows[i].Cells[j].Value.ToString();
                }
            }
            worksheet.Columns.ColumnWidth = 30;

            var savefileDialog = new SaveFileDialog();
            savefileDialog.FileName = "output_Excel";
            savefileDialog.Filter = "Excel files (*.xlsx)|*.xlsx|All files (*.*)|*.*";
            savefileDialog.DefaultExt = ".xlsx";
            if (savefileDialog.ShowDialog() == DialogResult.OK)
            {
                workbook.SaveAs(savefileDialog.FileName, misValue, misValue, misValue, misValue, misValue, Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);

            }

            workbook.Close(true, misValue, misValue);
            app.Quit();

        }

        private void btnPrintSold_Solds_Click(object sender, EventArgs e)
        {
            if (datatableSold_Solds.Rows.Count == 0) return;
            object misValue = System.Reflection.Missing.Value;
            Excel.Application app = new Excel.Application();
            app.Visible = false;
            Excel.Workbook workbook = app.Workbooks.Add(Excel.XlWBATemplate.xlWBATWorksheet);
            Excel.Worksheet worksheet = (Excel.Worksheet)workbook.ActiveSheet;
            worksheet.Name = "DS";

           
            ///name
            worksheet.Cells[1, 1] = "Danh sách hóa đơn bán";
            worksheet.Cells[1, 1].EntireRow.Font.Bold = true;
            Excel.Range range = worksheet.Range[worksheet.Cells[1, 1], worksheet.Cells[1, datatableSold_Solds.Columns.Count]];
            range.Merge(true);
            range.Interior.ColorIndex = 36;
            range.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Red);
            range.Font.Size = 16;
            range.HorizontalAlignment = 3;
            range.VerticalAlignment = 3;
            ///header 

            for (int j = 0; j < datatableSold_Solds.Columns.Count; j++)
            {

                worksheet.Cells[3, j + 1] = datatableSold_Solds.Columns[j].HeaderText.ToString();

            }
            Excel.Range rangeHeader = worksheet.Range[worksheet.Cells[3, 1], worksheet.Cells[3, datatableSold_Solds.Columns.Count]];
            rangeHeader.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Red);
            
            rangeHeader.Font.Size = 13;
            rangeHeader.Font.Bold = true;
            rangeHeader.Columns.ColumnWidth = 30;
            ///content

            for (int i = 0; i < datatableSold_Solds.Rows.Count; i++)
            {
                for (int j = 0; j < datatableSold_Solds.Columns.Count; j++)
                {
                    worksheet.Cells[i + 4, j + 1] = datatableSold_Solds.Rows[i].Cells[j].Value.ToString();
                }
            }
            range.Merge(true);
            range.Interior.ColorIndex = 36;
            ///footer
            int indexfooter = datatableSold_Solds.Rows.Count + 6;
            Excel.Range rangeFooter = worksheet.Range[worksheet.Cells[indexfooter, 7], worksheet.Cells[indexfooter+5, 8]];
            rangeFooter.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Red);
            rangeFooter.Font.Size = 13;
            rangeFooter.Font.Bold = true;
            
           
            worksheet.Cells[indexfooter, 7] = "Tổng";
            worksheet.Cells[indexfooter++, 7] = "Tổng phí giao";
            worksheet.Cells[indexfooter, 8] = double.Parse(lblTongPhiGiaoSolds.Text.ToString());
            worksheet.Cells[indexfooter++, 7] = "Tổng phụ thu";
            worksheet.Cells[indexfooter, 8] = double.Parse(lblTongPhuThuSolds.Text.ToString());
            worksheet.Cells[indexfooter++, 7] = "Tổng trị giá hóa đơn";
            worksheet.Cells[indexfooter, 8] = double.Parse(lblTongHDSolds.Text.ToString());
            worksheet.Cells[indexfooter++, 7] = "Tổng đã thanh toán";
            worksheet.Cells[indexfooter, 8] = double.Parse(lblDaThanhToanSolds.Text.ToString());

            var savefileDialog = new SaveFileDialog();
            savefileDialog.FileName = "output_Excel";
            savefileDialog.Filter = "Excel files (*.xlsx)|*.xlsx|All files (*.*)|*.*";
            savefileDialog.DefaultExt = ".xlsx";
            if (savefileDialog.ShowDialog() == DialogResult.OK)
            {
                workbook.SaveAs(savefileDialog.FileName, misValue, misValue, misValue, misValue, misValue, Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);

            }

            workbook.Close(true, misValue, misValue);
            app.Quit();
        }

        private void datatableSold_Solds_CurrentCellChanged(object sender, EventArgs e)
        {

        }
        private void chart(int year)
        {
            cartesianChart1.Series.Clear();
            cartesianChart1.AxisX.Clear();
            cartesianChart1.AxisY.Clear();
            using (DATADataContext db = new DATADataContext())
            {
                var  info= db.sp_HDB_Getlist_ChartYear(year);

                ColumnSeries col = new ColumnSeries()
                {
                    DataLabels = true,
                    Values = new LiveCharts.ChartValues<double>(),
                    LabelPoint = point => point.Y.ToString()
                    
                };
                Axis axis = new Axis() { Separator = new Separator() { Step = 1, IsEnabled = false } };
                axis.Labels = new List<string>();
                axis.Title = "Thông kê bán theo tháng của năm " + year;
                
               
                foreach(var x in info)
                {
                    col.Values.Add(x.TONG.Value);
                    col.Values.Add(Convert.ToDouble(x.THANG.Value));
                }
                cartesianChart1.Series.Add(col);
                cartesianChart1.AxisX.Add(axis);
            
                cartesianChart1.AxisY.Add(new Axis{ LabelFormatter =value =>value.ToString() , Separator =new Separator()});
            }
        }

        private void btnDrawChart_Click(object sender, EventArgs e)
        {
            int nam = 2017;
            try
            {
                nam = Convert.ToInt32(txtyear.Value);
            }
            catch { nam = 2017; }

            chart(nam);
        }

        private void btnPayMent_Click(object sender, EventArgs e)
        {
            openTabPage(7);
        }

        private void tabpageChart_Click(object sender, EventArgs e)
        {

        }

        private void btnChartBack_Click(object sender, EventArgs e)
        {
            openTabPage(1);
        }
    }
}
