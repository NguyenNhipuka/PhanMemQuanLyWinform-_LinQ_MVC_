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

namespace PMQLCHDIENTHOAI
{
    public partial class frmBuy : Form
    {
        public int flat = 0;
        public int tabindex = 1;
        public string IDTK = "";
     
        public frmBuy()
        {
            InitializeComponent();
           
            openTabPage(tabindex);
            blockmenu();
        }

       public void blockmenu()
       {
            btnAddBuy.Enabled = TaiKhoanBUS.Instance.checkRoleUser(IDTK, Properties.Settings.Default.cnLapBuy);
            btnBuys.Enabled = TaiKhoanBUS.Instance.checkRoleUser(IDTK, Properties.Settings.Default.cnXemDSBuy);
            btnBuyInfo.Enabled = TaiKhoanBUS.Instance.checkRoleUser(IDTK, Properties.Settings.Default.cnXemDetailBuy);
            btnExcel.Enabled = TaiKhoanBUS.Instance.checkRoleUser(IDTK, Properties.Settings.Default.cnXuatBuy);
        }
        public void openTabPage(int tab)
        {
            tabBuy.TabPages.Clear();
            switch (tab)
            {
                case 1://tab home
                    tabBuy.TabPages.Add(tabPageHome);
                    break;
                case 2://tab add Buy
                    tabBuy.TabPages.Add(tabPageAddBuy);
                    break;
                case 3://tab Buys
                    tabBuy.TabPages.Add(tabPageBuys);
                    break;
                case 4://tab Info Buys
                    tabBuy.TabPages.Add(tabPageInfoBuy);
                    break;
                case 5://tab Search Buys
                    tabBuy.TabPages.Add(tabPageSearchBuys);
                    break;
                case 6://tab Excel
                    tabBuy.TabPages.Add(tabPageExcel);
                    break;
                case 7://tab phiếu chi
                    tabBuy.TabPages.Add(tabPagePhieuChiBuys);
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

        private void btnBuysBack_Click(object sender, EventArgs e)
        {
            openTabPage(1);
        }

        private void btnSearchBack_Click(object sender, EventArgs e)
        {
            openTabPage(1);
        }

        private void btnExcelBack_Click(object sender, EventArgs e)
        {
            openTabPage(1);
        }
        private void btnPhieuChiBack_Click(object sender, EventArgs e)
        {
            openTabPage(1);
        }
        private void btnAddBuy_Click(object sender, EventArgs e)
        {
           
            openTabPage(2);

            #region  combobox
            NCCBus.Instance.LoadNCCName(cbSupplierBuy_Add);
            if (cbSupplierBuy_Add.Items.Count > 0) cbSupplierBuy_Add.SelectedIndex = 0;
            // cbGTGT_Add
            DataTable t = new DataTable();
            t.Columns.Add("Ma");
            t.Columns.Add("GT");
            t.Rows.Add(5, 5);
            t.Rows.Add(10, 10);
            cbGTGT_Add.DataSource = t;
            cbGTGT_Add.DisplayMember = "GT";
            cbGTGT_Add.ValueMember = "Ma";
            DataTable tt = new DataTable();
            tt.Columns.Add("Matt");
            tt.Columns.Add("GTtt");
            tt.Rows.Add(0, "Chưa thanh toán");
            tt.Rows.Add(1, "Đã thanh toán");
            cbStatusPayBuy_Add.DataSource = tt;
            cbStatusPayBuy_Add.DisplayMember = "GTtt";
            cbStatusPayBuy_Add.ValueMember = "Matt";
            DataTable tthd = new DataTable();
            tthd.Columns.Add("Mah");
            tthd.Columns.Add("GTh");
            tthd.Rows.Add(0, "Đang xử lý");
            tthd.Rows.Add(1, "Đã nhập");
           // tthd.Rows.Add(2, "Đã hủy");
            cbStatusBuy_Add.DataSource = tthd;
            cbStatusBuy_Add.DisplayMember = "GTh";
            cbStatusBuy_Add.ValueMember = "Mah";
            #endregion

            if (cbSupplierBuy_Add.Items != null)
            {
                int mancc = Convert.ToInt32(cbSupplierBuy_Add.SelectedValue.ToString());
                ThietBiBus.Instance.LoadTBIDName(cbIDDeviceBuy_Add, mancc);
                try { cbIDDeviceBuy_Add.SelectedIndex = 0; } catch { }
            }
            btnSaveDeviceBuy_Add.Enabled = false;
            txtDateBuy_Add.Text = DateTime.Now.Date.ToString("dd/MM/yyyy");
            txtSumPayBillAdd.Text = txtSumBuy_Add.Text = "0";
        }

        private void btnBuys_Click(object sender, EventArgs e)
        {
            openTabPage(3);
            HoaDonMuaBus.Instance.loadDSHDM(datatableBuys_Buys);
            datatableBuys_Buys.RowsDefaultCellStyle.BackColor = Color.White;
            datatableBuys_Buys.AlternatingRowsDefaultCellStyle.BackColor = Color.LightBlue;
            bindingSourceDSHDM.Filter = null;
            bindingSourceDSHDM.Sort = null;
            bindingSourceDSHDM.DataSource = null;
            bindingSourceDSHDM.DataSource = datatableBuys_Buys.DataSource;
            sumGridAndSumRow(datatableBuys_Buys, lblSoluongBuys_Buys, lblTongBuys_Buys, 6);
            sumGridAndSumRow(datatableBuys_Buys, lblSoluongBuys_Buys, lblDaThanhToanBuys, 7);
            sumGridAndSumRow(datatableBuys_Buys, lblSoluongBuys_Buys, lblTongNoBuys, 8);
        }

        private void btnSearchBuys_Click(object sender, EventArgs e)
        {
            openTabPage(5);
        }
        private void btnPhieuChiBuys_Click(object sender, EventArgs e)
        {
            openTabPage(7);
            TabPhieuChi.SelectedIndex = 0;
            datatableBuysNo_Buys.RowsDefaultCellStyle.BackColor = Color.White;
            datatableBuysNo_Buys.AlternatingRowsDefaultCellStyle.BackColor = Color.LightBlue;
            HoaDonMuaBus.Instance.loadDSHDMNo(datatableBuysNo_Buys);
            bindingSource1.Filter = null;
            bindingSource1.Sort = null;
            bindingSource1.DataSource = datatableBuysNo_Buys.DataSource;
            if (datatableBuysNo_Buys.SelectedRows != null)
            {
                foreach (DataGridViewRow r in datatableBuysNo_Buys.SelectedRows)
                {
                    try
                    {
                        string mahd = r.Cells[1].Value.ToString();
                        HoaDonMuaBus.Instance.SotienConNo(mahd, lblTienNo);
                        txtIDPC.Text = mahd;
                    }
                    catch { lblTienNo.Text = "0"; txtIDPC.Text = string.Empty; }
                }

            }
        }
        private void btnBuyInfo_Click(object sender, EventArgs e)
        {
            openTabPage(4);
            DataTable tthd = new DataTable();
            tthd.Columns.Add("Mah");
            tthd.Columns.Add("GTh");
            tthd.Rows.Add(0, "Đang xử lý");
            tthd.Rows.Add(1, "Đã nhập");
            tthd.Rows.Add(2, "Đã hủy");
            dropStatusBuy_Info.DataSource = tthd;
            dropStatusBuy_Info.DisplayMember = "GTh";
            dropStatusBuy_Info.ValueMember = "Mah";
            dropStatusBuy_Info.SelectedValue=0;

        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            openTabPage(6);
            HoaDonMuaBus.Instance.loadDSHDM(dataTableDevicesExcel);
            bindingSourceHDMXuat.Filter = bindingSourceHDMXuat.Sort = null;
            bindingSourceHDMXuat.DataSource = null;
            bindingSourceHDMXuat.DataSource = dataTableDevicesExcel.DataSource;
            foreach (DataGridViewColumn col in dataTableDevicesExcel.Columns)
                datatableout_Ex.Columns.Add(col.Name, col.HeaderText);
        }

        private void cbIDDeviceBuy_Add_SelectedValueChanged(object sender, EventArgs e)
        {

            if (cbIDDeviceBuy_Add.SelectedItem != null)
            {               
                try
                {
                    string matb = cbIDDeviceBuy_Add.SelectedValue.ToString();
                    HoaDonMuaBus.Instance.loadTBByKeyHDB(matb, txtTBTon_Add, txtGia_Add);
                    int soluongnhap = Convert.ToInt32(txtSLuongDevice.Value);
                    double giamua = double.Parse(txtGiaMoi_Add.Text.ToString());
                    if (!string.IsNullOrEmpty(txtGia_Add.Text))
                    {
                        txtFreePriceBuy_Add.Text = (soluongnhap * giamua).ToString();
                        txtFreePriceBuy_Add.Text = string.Format("{0:#,##0.00}", double.Parse(txtFreePriceBuy_Add.Text));

                    }
                }
                catch {
                    txtTBTon_Add.Text = txtGia_Add.Text = txtFreePriceBuy_Add.Text= "0";
                }


            }
            else
            {
                txtGiaMoi_Add.Text= txtTBTon_Add.Text = txtGia_Add.Text = txtFreePriceBuy_Add.Text = "0";

            }
        }
        private void cbSupplierBuy_Add_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbSupplierBuy_Add.SelectedItem != null)
            {
                cbIDDeviceBuy_Add.DataSource = null;
                cbIDDeviceBuy_Add.Items.Clear();
                try
                {
                    int mancc = Convert.ToInt32(cbSupplierBuy_Add.SelectedValue.ToString());
                    ThietBiBus.Instance.LoadTBIDName(cbIDDeviceBuy_Add, mancc);
                    if(datatableDecivesBuy_Add.Rows.Count > 0)
                    {
                        datatableDecivesBuy_Add.DataSource = null;
                        datatableDecivesBuy_Add.Rows.Clear();
                        txtGiaMoi_Add.Text = txtTBTon_Add.Text = txtGia_Add.Text = txtFreePriceBuy_Add.Text = string.Empty;
                    }

                }
                catch { }
                return;
            }
           

        }

        private void txtSLuongDevice_ValueChanged(object sender, EventArgs e)
        {

            if (cbIDDeviceBuy_Add.SelectedItem != null)
            {

                try
                {
                    string matb = cbIDDeviceBuy_Add.SelectedValue.ToString();
                    HoaDonMuaBus.Instance.loadTBByKey(matb, txtTBTon_Add, txtGia_Add);
                    int soluongnhap = Convert.ToInt32(txtSLuongDevice.Value);
                    double giamua = double.Parse(txtGiaMoi_Add.Text.ToString());
                    if (!string.IsNullOrEmpty(txtGiaMoi_Add.Text))
                    {
                        txtFreePriceBuy_Add.Text = (soluongnhap * giamua).ToString();
                        txtFreePriceBuy_Add.Text = string.Format("{0:#,##0.00}", double.Parse(txtFreePriceBuy_Add.Text));

                    }
                }
                catch
                {
                    txtGiaMoi_Add.Text= txtTBTon_Add.Text = txtGia_Add.Text = txtFreePriceBuy_Add.Text = string.Empty;
                }


            }
            else
            {
                txtGiaMoi_Add.Text= txtTBTon_Add.Text = txtGia_Add.Text = txtFreePriceBuy_Add.Text = string.Empty;

            }
        }

        private void btnAddDeviceBuy_Add_Click(object sender, EventArgs e)
        {
            if (cbIDDeviceBuy_Add.SelectedItem != null)
            {
                string matb = cbIDDeviceBuy_Add.SelectedValue.ToString();
                if (datatableDecivesBuy_Add.Rows.Count > 0)
                {
                    try
                    {
                        var item = datatableDecivesBuy_Add.Rows.Cast<DataGridViewRow>().First(r => r.Cells[1].Value.ToString().Equals(matb));
                        //đã được thêm vào datagridview
                        if (item != null)
                        {
                            return;
                        }
                    }
                    catch { }
                    
                }
                
                int soluong = Convert.ToInt32(txtSLuongDevice.Value);
                double giam = 0;
                if (txtGiaMoi_Add.Text.ToString() != "")
                {
                    giam = double.Parse(txtGiaMoi_Add.Text.ToString());
                }
               
                double thanhtien = double.Parse(txtFreePriceBuy_Add.Text.ToString());
                if(ThietBiBus.Instance.AddTBByKeyToHDM(matb, datatableDecivesBuy_Add, giam, thanhtien, soluong))
                {
                    tinhtonghd();
                    CustomAlert.Show("Thành công");

                }
                else CustomAlert.Show("that bai");
                
            }
        }

        public void tinhtonghd()
        {
            double sum = 0;
            for(int i=0;i< datatableDecivesBuy_Add.RowCount; i++)
            {
                sum += double.Parse(datatableDecivesBuy_Add.Rows[i].Cells[8].Value.ToString());
            }
            //string.Format("{0:#,##0.00}", double.Parse(txtFreePriceBuy_Add.Text))
            txtSumBuy_Add.Text = string.Format("{0:#,##0.00}", sum.ToString());
            sum +=sum*Convert.ToDouble(cbGTGT_Add.SelectedValue)/100;
            txtSumPayBillAdd.Text =sum.ToString();

        }
        private void btnCopyGia_add_Click(object sender, EventArgs e)
        {
            if(!string.IsNullOrEmpty(txtGia_Add.Text))
            {
                double giacu = double.Parse(txtGia_Add.Text.ToString());
                txtGiaMoi_Add.Text = giacu.ToString();
            }
        }

        private void txtGia_Add_OnValueChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtGia_Add.Text))
            {
                double giacu = double.Parse(txtGia_Add.Text.ToString());
                txtGiaMoi_Add.Text = giacu.ToString();
            }
        }
        private void UpdateSTTGrid(DataGridView gridView)
        {
            for (int i = 0; i < gridView.RowCount; i++)
            {
                gridView.Rows[i].Cells[0].Value = i + 1;

            }
        }

        private void datatableDecivesBuy_Add_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            UpdateSTTGrid(datatableDecivesBuy_Add);
        }

        private void datatableDecivesBuy_Add_Sorted(object sender, EventArgs e)
        {
            UpdateSTTGrid(datatableDecivesBuy_Add);
        }

        private void cbSupplierBuy_Add_DropDown(object sender, EventArgs e)
        {

        }

        private void btnUpdateDeviceBuy_Add_Click(object sender, EventArgs e)
        {
            if(datatableDecivesBuy_Add.SelectedRows != null && datatableDecivesBuy_Add.RowCount >0)
            {
                try
                {
                    string matb = datatableDecivesBuy_Add.CurrentRow.Cells[1].Value.ToString();
                    cbIDDeviceBuy_Add.SelectedValue =matb.ToString();
                    HoaDonMuaBus.Instance.loadTBByKey(matb, txtTBTon_Add, txtGia_Add);
                    int soluongnhap = Convert.ToInt32(datatableDecivesBuy_Add.CurrentRow.Cells[3].Value.ToString());
                    double giamua = double.Parse(datatableDecivesBuy_Add.CurrentRow.Cells[6].Value.ToString());
                    if (!string.IsNullOrEmpty(txtGia_Add.Text))
                    {
                        txtFreePriceBuy_Add.Text = (soluongnhap * giamua).ToString();
                        txtFreePriceBuy_Add.Text = string.Format("{0:#,##0.00}", double.Parse(txtFreePriceBuy_Add.Text));

                    }
                    txtSLuongDevice.Value = soluongnhap;
                    //khoa chuc nang
                    btnRefeshBuy_Add.Enabled = btnSaveBuy_Add.Enabled= btnAddDeviceBuy_Add.Enabled= cbIDDeviceBuy_Add.Enabled= false;
                }
                catch { }
            }
            else
            {
                CustomAlert.Show("Chưa có thiết bị");
            }
        }

        private void btnDeleteDeciveBuy_Add_Click(object sender, EventArgs e)
        {
            if (datatableDecivesBuy_Add.CurrentRow != null)
            {
                try
                {
                    datatableDecivesBuy_Add.Rows.Remove(datatableDecivesBuy_Add.CurrentRow);
                    UpdateSTTGrid(datatableDecivesBuy_Add);
                    tinhtonghd();
                    CustomAlert.Show("Xóa thành công!");

                }
                catch { }
            }
            else CustomAlert.Show("Chưa chọn thiết bị");

        }

        private void btnRefeshBuy_Add_Click(object sender, EventArgs e)
        {
            DialogResult result = CustomDialog1.show("Xác nhận", "Xóa danh sách để lập mới ?", "Chấp nhận", "Trở lại");
            if (result==DialogResult.No)
            {
                return;//hủy thao tác
            }

            datatableDecivesBuy_Add.DataSource = null;
            datatableDecivesBuy_Add.Rows.Clear();
            txtGiaMoi_Add.Text = txtTBTon_Add.Text = txtGia_Add.Text = txtSumBuy_Add.Text= txtFreePriceBuy_Add.Text = string.Empty;
        }

        private void btnSaveDeviceBuy_Add_Click(object sender, EventArgs e)
        {
            //mở chuc nang
            btnUpdateDeviceBuy_Add.Enabled =btnRefeshBuy_Add.Enabled = btnSaveBuy_Add.Enabled
                = cbIDDeviceBuy_Add.Enabled = btnAddDeviceBuy_Add.Enabled = true;
            //thuc thi lưu cập nhật
            if (cbIDDeviceBuy_Add.SelectedItem != null)
            {
                    try
                    {
                    int soluong = Convert.ToInt32(txtSLuongDevice.Value);
                    double giam = double.Parse(txtGiaMoi_Add.Text.ToString());
                    double thanhtien = double.Parse(txtFreePriceBuy_Add.Text.ToString());
                    int index = datatableDecivesBuy_Add.CurrentRow.Index;
                    datatableDecivesBuy_Add.Rows[index].Cells[3].Value = soluong;
                    datatableDecivesBuy_Add.Rows[index].Cells[6].Value = giam;
                    datatableDecivesBuy_Add.Rows[index].Cells[7].Value = thanhtien;
                    tinhtonghd();
                    CustomAlert.Show("Cập nhật thành công");

                }
                    catch
                {
                    CustomAlert.Show("Cập nhật k thành công");
                }

            }
            //khoa chuc nang
            btnSaveDeviceBuy_Add.Enabled = false;
        }

        private void btnSaveBuy_Add_Click(object sender, EventArgs e)
        {
            if (datatableDecivesBuy_Add.Rows.Count <= 0) { CustomMessageBox1.show("Chưa có thiết bị trong hóa đơn");return; }
            if(cbSupplierBuy_Add.Items.Count ==0) { CustomMessageBox1.show("Chưa có nhà cung cấp trong hóa đơn"); return; }
            int mancc = Convert.ToInt32(cbSupplierBuy_Add.SelectedValue);
            int ttthanhtoan = Convert.ToInt32(cbStatusPayBuy_Add.SelectedValue);
            
            int tthoadon = Convert.ToInt32(cbStatusBuy_Add.SelectedValue);
            double gtgt = Convert.ToDouble(cbGTGT_Add.SelectedValue.ToString())/100;
            DateTime hanno = datePicHanNoBuy.Value.Date;
            double tongtien = Double.Parse(txtSumPayBillAdd.Text.ToString());
            if (ttthanhtoan == 1) hanno = new DateTime();
             Boolean result = false;
            result = HoaDonMuaBus.Instance.addHDM(out string mahd,IDTK, cbSupplierBuy_Add,
                cbStatusPayBuy_Add, cbStatusBuy_Add
                , cbGTGT_Add,
                datePicHanNoBuy, txtSumPayBillAdd,
                datatableDecivesBuy_Add);
            //them k thanh cong
            if (!result) return;

            DialogResult dialogResult = CustomDialog1.show("Thông báo", "Bạn có muốn xuất hóa đơn", "Có", "Không");
            if(dialogResult == DialogResult.OK)
             {
                CustomAlert.Show("Chuẩn bị xuất!");
                frmXuatHDM frm = new frmXuatHDM();
                frm.mahd = mahd;
                frm.ShowDialog();
             }
            datatableDecivesBuy_Add.DataSource = null;
            datatableDecivesBuy_Add.Rows.Clear();

        }

        private void cbStatusPayBuy_Add_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToInt32(cbStatusPayBuy_Add.SelectedValue) == 1)
                    datePicHanNoBuy.Enabled = false;
                else
                    datePicHanNoBuy.Enabled = true;
                
            }
            catch { }
            
        }

        private void cbGTGT_Add_SelectedValueChanged(object sender, EventArgs e)
        {
            string tong = txtSumPayBillAdd.Text.ToString();
            if (datatableDecivesBuy_Add.Rows.Count <=0)
            {
                return;
            }
            tinhtonghd();
        }

        private void btnSearchBuy_Info_Click(object sender, EventArgs e)
        {
            string mahd = txtsearchBuy_Info.text.ToString().ToUpper();
            if (string.IsNullOrEmpty(mahd))
            {
                CustomMessageBox1.show("Vui lòng nhập mã hóa đơn mua!");
                return;
            }
            if (mahd.Length >10)
            {
                CustomMessageBox1.show("Mã hóa đơn không đúng định dạng!");
                return;
            }
            Boolean result = HoaDonMuaBus.Instance.searchHDMByKey(mahd, txtnameSupplierBuy_Info,
                txtStatusPayBuy_Info, dropStatusBuy_Info, txtVATBuy_Info, txtIdStaffBuy_Info,
                txtSumPayBillBuy_Info, txtdateBuy_Info, txtSumBuy_Info,
                datatableDecivesBuy_Info, datatableDSPhieuChiBuy_Info);
            if (result == false)
            {
                txtidBuy_Info.Text = string.Empty;
                dropStatusBuy_Info.Enabled = true;
                 btnSaveBuy_info.Enabled=false;
                return;
            }
            btnSaveBuy_info.Enabled = dropStatusBuy_Info.Enabled;
            txtidBuy_Info.Text = mahd;

        }

        public void refeshDataHDMinfo()
        {
            txtnameSupplierBuy_Info.Text= txtStatusPayBuy_Info.Text
                = txtVATBuy_Info .Text= txtIdStaffBuy_Info.Text= string.Empty;
            datatableDSPhieuChiBuy_Info.DataSource= datatableDecivesBuy_Info.DataSource = null;
            datatableDecivesBuy_Info.Rows.Clear();
            datatableDSPhieuChiBuy_Info.Rows.Clear();
        }

        private void btnSaveBuy_info_Click(object sender, EventArgs e)
        {
            if(datatableDecivesBuy_Info.Rows.Count > 0)
            {
               string mahd = txtidBuy_Info.Text.ToString();
               Boolean result= HoaDonMuaBus.Instance.updateTinhTrangHDM(mahd, dropStatusBuy_Info);
                if(result == true)
                {
                    if (dropStatusBuy_Info.SelectedValue.ToString() != "0")
                        btnSaveBuy_info.Enabled = false;
                    else
                        btnSaveBuy_info.Enabled = true;
                }
            }
        }

        private void dropStatusBuy_Info_SelectedValueChanged(object sender, EventArgs e)
        {
            
        }

        private void datatableBuys_Buys_FilterStringChanged(object sender, EventArgs e)
        {
            bindingSourceDSHDM.Filter = datatableBuys_Buys.FilterString;
            UpdateSTTGrid(datatableBuys_Buys);
            sumGridAndSumRow(datatableBuys_Buys, lblSoluongBuys_Buys, lblTongBuys_Buys, 6);
            sumGridAndSumRow(datatableBuys_Buys, lblSoluongBuys_Buys, lblDaThanhToanBuys, 7);
            sumGridAndSumRow(datatableBuys_Buys, lblSoluongBuys_Buys, lblTongNoBuys, 8);
        }

        
        
        private void datatableBuys_Buys_SortStringChanged(object sender, EventArgs e)
        {
            bindingSourceDSHDM.Sort = datatableBuys_Buys.SortString;
            UpdateSTTGrid(datatableBuys_Buys);
            sumGridAndSumRow(datatableBuys_Buys, lblSoluongBuys_Buys, lblTongBuys_Buys, 6);
            sumGridAndSumRow(datatableBuys_Buys, lblSoluongBuys_Buys, lblDaThanhToanBuys, 7);
            sumGridAndSumRow(datatableBuys_Buys, lblSoluongBuys_Buys, lblTongNoBuys, 8);

        }

        private void datatableBuys_Buys_DataSourceChanged(object sender, EventArgs e)
        {
            sumGridAndSumRow(datatableBuys_Buys, lblSoluongBuys_Buys, lblTongBuys_Buys, 6);
            sumGridAndSumRow(datatableBuys_Buys, lblSoluongBuys_Buys, lblDaThanhToanBuys, 7);
            sumGridAndSumRow(datatableBuys_Buys, lblSoluongBuys_Buys, lblTongNoBuys, 8);

        }



        private void dataTableDevicesExcel_FilterStringChanged(object sender, EventArgs e)
        {
            bindingSourceHDMXuat.Filter = dataTableDevicesExcel.FilterString;
            UpdateSTTGrid(dataTableDevicesExcel);
           
        }

        private void dataTableDevicesExcel_SortStringChanged(object sender, EventArgs e)
        {
            bindingSourceHDMXuat.Sort = dataTableDevicesExcel.SortString;
            UpdateSTTGrid(dataTableDevicesExcel);
            
        }

        private void btnInEx_Excel_Click(object sender, EventArgs e)
        {

            if (dataTableDevicesExcel.SelectedRows != null)
            {
                foreach (DataGridViewRow row in dataTableDevicesExcel.SelectedRows)
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

        private void TabPhieuChi_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadDSPhieuChi();

        }

        private void loadDSPhieuChi()
        {

            if (TabPhieuChi.SelectedIndex == 0)
            {
                btnLapPhieuChi.Enabled= TaiKhoanBUS.Instance.checkRoleUser(IDTK, Properties.Settings.Default.cnLapChi);
                datatableBuysNo_Buys.RowsDefaultCellStyle.BackColor = Color.White;
                datatableBuysNo_Buys.AlternatingRowsDefaultCellStyle.BackColor = Color.LightBlue;
                HoaDonMuaBus.Instance.loadDSHDMNo(datatableBuysNo_Buys);
                bindingSource1.Filter = null;
                bindingSource1.Sort = null;
                bindingSource1.DataSource = datatableBuysNo_Buys.DataSource;
            }
            else
            {
                 if(!TaiKhoanBUS.Instance.checkRoleUser(IDTK, Properties.Settings.Default.cnXemChi))
                {
                    CustomMessageBox.show("Cảnh báo", "bạn không có quyền với chức năng này", false);
                    TabPhieuChi.SelectedIndex = 0;
                    return;
                }
                dataGridDSPhieuChi.RowsDefaultCellStyle.BackColor = Color.White;
                dataGridDSPhieuChi.AlternatingRowsDefaultCellStyle.BackColor = Color.LightBlue;
                HoaDonMuaBus.Instance.loadDSPCHDM(dataGridDSPhieuChi);
                if(dataGridDSPhieuChi.Rows.Count > 0)
                {
                    sumGridAndSumRow(dataGridDSPhieuChi, lbltongdongPC, lblSumPC, 5);
                }
                bindingSource1.Filter = null;
                bindingSource1.Sort = null;
                bindingSource1.DataSource = dataGridDSPhieuChi.DataSource;
            }
        }

        private void datatableBuysNo_Buys_FilterStringChanged(object sender, EventArgs e)
        {
            bindingSource1.Filter = datatableBuysNo_Buys.FilterString;
            UpdateSTTGrid(datatableBuysNo_Buys);
            try
            {
               
                string mahd = datatableBuysNo_Buys.SelectedRows[0].Cells[1].Value.ToString();
                HoaDonMuaBus.Instance.SotienConNo(mahd, lblTienNo);
            }
            catch { lblTienNo.Text = "0"; }
        }

        private void datatableBuysNo_Buys_SortStringChanged(object sender, EventArgs e)
        {
           
            try
            {
                bindingSource1.Sort = datatableBuysNo_Buys.SortString;
                UpdateSTTGrid(datatableBuysNo_Buys);
                string mahd = datatableBuysNo_Buys.SelectedRows[0].Cells[1].Value.ToString();
                HoaDonMuaBus.Instance.SotienConNo(mahd, lblTienNo);
            }
            catch { lblTienNo.Text = "0"; }
        }

        private void dataGridDSPhieuChi_FilterStringChanged(object sender, EventArgs e)
        {
            bindingSource1.Filter = dataGridDSPhieuChi.FilterString;
            UpdateSTTGrid(dataGridDSPhieuChi);
            sumGridAndSumRow(dataGridDSPhieuChi, lbltongdongPC, lblSumPC, 5);

        }

        private void dataGridDSPhieuChi_SortStringChanged(object sender, EventArgs e)
        {
            bindingSource1.Sort = dataGridDSPhieuChi.SortString;
            UpdateSTTGrid(dataGridDSPhieuChi);
            sumGridAndSumRow(dataGridDSPhieuChi, lbltongdongPC, lblSumPC, 5);
        }

        private void btnLapPhieuChi_Click(object sender, EventArgs e)
        {
            if (datatableBuysNo_Buys.SelectedRows != null)
            {
                if (txtSoTien.Text.Length <=0)
                {
                    CustomMessageBox1.show("Bạn chưa nhập số tiền");
                    return;
                }
                double tien = double.Parse(txtSoTien.Text.ToString());
                string diengiai = txtDienGiaiPC.Text.ToString();
                if(diengiai.Length >= 100)
                {
                    CustomMessageBox1.show("Diễn giải không quá 100 ký tự");
                    return;
                }
                if (tien <= 0)
                {
                    CustomMessageBox1.show("Số tiền phải lớn hơn 0");
                    return;
                }
                string mahdmua = datatableBuysNo_Buys.SelectedRows[0].Cells[1].Value.ToString();
                if(tien > double.Parse(lblTienNo.Text.ToString()))
                {
                    CustomMessageBox.show("Thông báo", "Số tiền trả vượt quá khoản nợ", false);
                    return;
                }
                HoaDonMuaBus.Instance.lapPhieuChi(mahdmua, diengiai, tien,IDTK);
                txtIDPC.Text = txtDienGiaiPC.Text = txtSoTien.Text = string.Empty;
                loadDSPhieuChi();
            }
        }

        private void datatableBuysNo_Buys_CurrentCellChanged(object sender, EventArgs e)
        {
            if (datatableBuysNo_Buys.Rows.Count == 0) return;
            

                foreach (DataGridViewRow r in datatableBuysNo_Buys.SelectedRows)
                {
                    try
                    {
                        string mahd = r.Cells[1].Value.ToString();
                        HoaDonMuaBus.Instance.SotienConNo(mahd, lblTienNo);
                        txtIDPC.Text = mahd;
                    }
                    catch { lblTienNo.Text = "0"; txtIDPC.Text = string.Empty; }
                }



            
        }

        private void dataGridDSPhieuChi_DataSourceChanged(object sender, EventArgs e)
        {
           
        }

        private void sumGridAndSumRow(DataGridView gv,Label lblRowCount,Label lblSum,int index)
        {
            if (gv.Rows.Count == 0)
            {
                lblRowCount.Text = lblSumPC.Text = "0";
                return;
            }
            lblRowCount.Text = gv.RowCount + "";
            try
            {

                Double total = gv.Rows.Cast<DataGridViewRow>().Sum(t => Convert.ToDouble(t.Cells[index].Value));
                lblSum.Text = string.Format("{0:n0}", total);

            }
            catch { lblSum.Text = "0"; }
        }

        private double sumGridAndSumRow(DataGridView gv, int index)
        {

            try
            {
                Double total = gv.Rows.Cast<DataGridViewRow>().Sum(t => Convert.ToDouble(t.Cells[index].Value));
                return total;
            }
            catch { return 0; }
        }

        private void btnPrintBuy_info_Click(object sender, EventArgs e)
        {
            string mahd = txtidBuy_Info.Text.ToString();
            if (string.IsNullOrEmpty(mahd)) return;
            frmXuatHDM frm = new frmXuatHDM();
            frm.mahd = mahd;
            frm.ShowDialog();
        }

       

        private void btnDetailSold_Solds_Click(object sender, EventArgs e)
        {
            if (datatableBuys_Buys.SelectedRows != null && datatableBuys_Buys.RowCount > 0)
            {
                lblmaHDBSolds.Text = datatableBuys_Buys.SelectedRows[0].Cells[1].Value.ToString();
                HoaDonMuaBus.Instance.loadDSCTHDM_ByMaHD(datatableBuys_Buys.SelectedRows[0].Cells[1].Value.ToString(), datatableCTBuys_Buys);
            }
        }

        private void btnPrintEx_Excel_Click(object sender, EventArgs e)
        {
            double tam_tongmua , tam_dathanhtoan , tam_tongno = 0 ;
            tam_tongmua = sumGridAndSumRow(datatableout_Ex, 6);
            tam_dathanhtoan = sumGridAndSumRow(datatableout_Ex, 7);
            tam_tongno=sumGridAndSumRow(datatableout_Ex, 8);
            MessageBox.Show(tam_tongmua + "_" + tam_tongno+ "_" + tam_dathanhtoan);
            if (datatableout_Ex.Rows.Count > 0)
            {
                exportExcel_Buys(datatableout_Ex, "DANH SÁCH PHIẾU NHẬP", tam_tongmua.ToString(), tam_tongno.ToString(), tam_dathanhtoan.ToString());
            }

        }
        private void btnPrint_Buys_Click(object sender, EventArgs e)
        {
            if(datatableBuys_Buys.Rows.Count > 0)
            {
               
                exportExcel_Buys(datatableBuys_Buys, "DANH SÁCH PHIẾU NHẬP", lblTongBuys_Buys.Text.ToString(), lblTongNoBuys.Text.ToString() ,lblDaThanhToanBuys.Text.ToString());
               
            }
        }
        private void exportExcel_Buys(DataGridView gvExport,string nameExcel,string tongtrigia,string tongno,string tongdathanhtoan)
        {
            CustomAlert.Show("Vui lòng đợi....");
            if (gvExport.Rows.Count == 0) return;
            object misValue = System.Reflection.Missing.Value;
            Excel.Application app = new Excel.Application();
            app.Visible = false;
            Excel.Workbook workbook = app.Workbooks.Add(Excel.XlWBATemplate.xlWBATWorksheet);
            Excel.Worksheet worksheet = (Excel.Worksheet)workbook.ActiveSheet;
            worksheet.Name = "DS";


            ///name
            worksheet.Cells[1, 1] = nameExcel;
            worksheet.Cells[1, 1].EntireRow.Font.Bold = true;
            Excel.Range range = worksheet.Range[worksheet.Cells[1, 1], worksheet.Cells[1, gvExport.Columns.Count]];
            range.Merge(true);
            range.Interior.ColorIndex = 36;
            range.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Red);
            range.Font.Size = 16;
            range.HorizontalAlignment = 3;
            range.VerticalAlignment = 3;
            ///header 

            for (int j = 0; j < gvExport.Columns.Count; j++)
            {

                worksheet.Cells[3, j + 1] = gvExport.Columns[j].HeaderText.ToString();

            }
            Excel.Range rangeHeader = worksheet.Range[worksheet.Cells[3, 1], worksheet.Cells[3, gvExport.Columns.Count]];
            rangeHeader.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Red);

            rangeHeader.Font.Size = 13;
            rangeHeader.Font.Bold = true;
            rangeHeader.Columns.ColumnWidth = 30;
            ///content

            for (int i = 0; i < gvExport.Rows.Count; i++)
            {
                for (int j = 0; j < gvExport.Columns.Count; j++)
                {
                    worksheet.Cells[i + 4, j + 1] = gvExport.Rows[i].Cells[j].Value.ToString();
                }
            }
            range.Merge(true);
            range.Interior.ColorIndex = 36;
            ///footer
            int indexfooter = gvExport.Rows.Count + 7;
            Excel.Range rangeFooter = worksheet.Range[worksheet.Cells[indexfooter, 7], worksheet.Cells[indexfooter + 5, 8]];
            rangeFooter.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Red);
            rangeFooter.Font.Size = 13;
            rangeFooter.Font.Bold = true;

            rangeFooter.Columns.ColumnWidth = 30;
            worksheet.Cells[indexfooter-1, 7] = "dong nham";
            worksheet.Cells[indexfooter, 7] = "Tổng trị giá";
            worksheet.Cells[indexfooter, 8] = double.Parse(tongtrigia);
            worksheet.Cells[indexfooter++, 7] = "Tổng đã thanh toán";
            worksheet.Cells[indexfooter, 8] = double.Parse(tongdathanhtoan);
            worksheet.Cells[indexfooter++, 7] = "Tổng còn nợ";
            worksheet.Cells[indexfooter, 8] = double.Parse(tongno);

            var savefileDialog = new SaveFileDialog();
            savefileDialog.FileName = "output_Buys_Excel";
            savefileDialog.Filter = "Excel files (*.xlsx)|*.xlsx|All files (*.*)|*.*";
            savefileDialog.DefaultExt = ".xlsx";
            if (savefileDialog.ShowDialog() == DialogResult.OK)
            {
                workbook.SaveAs(savefileDialog.FileName, misValue, misValue, misValue, misValue, misValue, Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
                CustomAlert.Show("Xuất thành công!");
            }
            else
            {
                CustomAlert.Show("Thao tác xuất bị hủy!");
            }

            workbook.Close(true, misValue, misValue);
            app.Quit();
        }
    }
}
