using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BUS;
using MetroFramework.Controls;
using Bunifu.Framework.UI;
using Zen.Barcode;
using Excel = Microsoft.Office.Interop.Excel;
using PMQLCHDIENTHOAI.Data;
using LiveCharts.Wpf;

namespace PMQLCHDIENTHOAI
{
    public partial class frmDevice : Form
    {
        public int flat = 0;
        public int tabindex = 1;
        public string IDTK = "";
        int key_menu_ThemTB=Properties.Settings.Default.cnThemTB;
        int key_menu_ThongtinTB;
        int key_menu_XuatTB;
        int key_menu_BaogiaTB = Properties.Settings.Default.cnLapBG;
        int key_Them = Properties.Settings.Default.cnThemTB;
        int key_Sua = Properties.Settings.Default.cnCapNhatTB;
        int key_Xoa = Properties.Settings.Default.cnXoaTB;
        bool  k_sua, k_xoa = false;
        public frmDevice()
        {
            InitializeComponent();
            openTabPage(tabindex);
            btnAddDivices.Enabled = TaiKhoanBUS.Instance.checkRoleUser(IDTK, Properties.Settings.Default.cnThemTB);

        }
        public void block_menu()
        {
            btnAddDivices.Enabled = TaiKhoanBUS.Instance.checkRoleUser(IDTK, Properties.Settings.Default.cnThemTB);
            btnUpdateDevice_Info.Enabled = TaiKhoanBUS.Instance.checkRoleUser(IDTK, Properties.Settings.Default.cnCapNhatTB);
            btnDeleteDevice_Info.Enabled = TaiKhoanBUS.Instance.checkRoleUser(IDTK, Properties.Settings.Default.cnXoaTB);
        }
        public void openTabPage(int tab)
        {
            tabHome.TabPages.Clear();
            switch (tab)
            {
                case 1://tab home
                    tabHome.TabPages.Add(tabPageHome);
                    break;
                case 2://tab add Device
                    tabHome.TabPages.Add(tabPageAddDevice);
                    break;
                case 3://tab Devices
                    tabHome.TabPages.Add(tabPageDevices);
                    break;
                case 4://tab Info Device
                    tabHome.TabPages.Add(tabPageInfoDevice);
                    block_menu();
                    break;
                case 5://tab Search Devices
                    tabHome.TabPages.Add(tabPageSearchDevices);
                    break;

                case 6://tab Excel
                    tabHome.TabPages.Add(tabPageExcel);
                    break;
                case 7://tab Quote
                    tabHome.TabPages.Add(tabpageQuote);
                    break;
                default://khong co quyen
                    break;
            }
        }
        private void btnSearchBack_Click(object sender, EventArgs e)
        {
            openTabPage(1);

        }
        private void btnAddDivices_Click(object sender, EventArgs e)
        {
            openTabPage(2);
            loadComboboxAddDevice();
        }
        public void loadComboboxAddDevice()
        {
            ThietBiBus.Instance.LoadLoaiTB(dropCateDevice_Add);
            if (dropCateDevice_Add.Items.Count > 0)
            {
                dropCateDevice_Add.SelectedIndex = 0;
            }
            ThietBiBus.Instance.LoadDVTTB(dropUnitDevice_Add);
            if (dropUnitDevice_Add.Items.Count > 0)
            {
                dropUnitDevice_Add.SelectedIndex = 0;
            }
            NCCBus.Instance.LoadNCCName(dropSupplierDevice_Add);
            if (dropSupplierDevice_Add.Items.Count > 0)
            {
                dropSupplierDevice_Add.SelectedIndex = 0;
            }
            cbStatusDevice_Add.SelectedIndex = 0;
            
        }
        private void btnDivices_Click(object sender, EventArgs e)
        {
            openTabPage(3);
            dataTableDevices_Devices.RowsDefaultCellStyle.BackColor = Color.White;
            dataTableDevices_Devices.AlternatingRowsDefaultCellStyle.BackColor = Color.LightBlue;
            ThietBiBus.Instance.LoadDSTB(dataTableDevices_Devices);
            removeFilterAndSortBindingSource(bindingSource1, dataTableDevices_Devices);

        }

        private void btnSearchDivices_Click(object sender, EventArgs e)
        {
            openTabPage(5);
            #region load du lieu

            //load ds trạng thái hdbán lên combobox
            DataTable table = new DataTable();
            table.Columns.Add("MATT");
            table.Columns.Add("TEN");
            table.Rows.Add(4, "Tất cả");
            table.Rows.Add(0, "Hủy");
            table.Rows.Add(1, "Đã giao");
            table.Rows.Add(2, "Đang xử lý");
            table.Rows.Add(3, "Đang giao");          
            cbStatusSold_Se.DataSource = table;
            cbStatusSold_Se.DisplayMember = "TEN";
            cbStatusSold_Se.ValueMember = "MATT";
           // cbStatusSold_Se.SelectedValue = 4;
            //load ds trạng thái hdm lên combobox
            DataTable tthd = new DataTable();
            tthd.Columns.Add("Mah");
            tthd.Columns.Add("GTh");
            tthd.Rows.Add(3, "Tất cả");
            tthd.Rows.Add(0, "Đang xử lý");
            tthd.Rows.Add(1, "Đã nhập");
            tthd.Rows.Add(2, "Đã hủy");
            
            cbStatusBuy_Se.DataSource = tthd;
            cbStatusBuy_Se.DisplayMember = "GTh";
            cbStatusBuy_Se.ValueMember = "Mah";
            //cbStatusSold_Se.SelectedValue = 3;
            #endregion

            dataTableFliter_De.RowsDefaultCellStyle.BackColor = Color.White;
            dataTableFliter_De.AlternatingRowsDefaultCellStyle.BackColor = Color.LightBlue;
            lblTenDSTim.Text = " Danh sách tìm kiếm";

        }

        private void btnDiviceInfo_Click(object sender, EventArgs e)
        {
            openTabPage(4);
            btnUpdateDevice_Info.Enabled = k_sua;
            btnDeleteDevice_Info.Enabled = k_xoa;
            btnSaveDevice_Info.Enabled = false;
            refesh_Info();
        }

        private void btnReportExcel_Click(object sender, EventArgs e)
        {
            openTabPage(7);
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            openTabPage(6);
            ThietBiBus.Instance.LoadDSTBE(dataTableDevicesExcel);
            removeFilterAndSortBindingSource(bindingSource1, dataTableDevicesExcel);
            datatableoutDevicesExcel.DataSource = null;
            datatableoutDevicesExcel.Rows.Clear();
        }

        private void btnAddBack_Click(object sender, EventArgs e)
        {
            refeshAddDevice();
            openTabPage(1);
        }

        private void btnDevicesBack_Click(object sender, EventArgs e)
        {
            openTabPage(1);
           

        }

        private void btnQuoteBack_Click(object sender, EventArgs e)
        {
            openTabPage(1);
        }

        private void btnExcelBack_Click(object sender, EventArgs e)
        {
            openTabPage(1);
        }

        private void btnInfoBack_Click(object sender, EventArgs e)
        {
            openTabPage(1);
        }

        private void btnAddCate_Add_Click(object sender, EventArgs e)
        {
            frmCategoryDevice frmCategory = new frmCategoryDevice();
            frmCategory.IDTK = IDTK;
            frmCategory.ShowDialog();
            ThietBiBus.Instance.LoadLoaiTB(dropCateDevice_Add);
            if (dropCateDevice_Add.Items.Count > 0)
            {
                dropCateDevice_Add.SelectedIndex = 0;
            }

        }

        private void btnAddUnit_Add_Click(object sender, EventArgs e)
        {
            frmUnit frmunit = new frmUnit();
            frmunit.IDTK = IDTK;
            frmunit.ShowDialog();
            ThietBiBus.Instance.LoadDVTTB(dropUnitDevice_Add);
            if (dropUnitDevice_Add.Items.Count > 0)
            {
                dropUnitDevice_Add.SelectedIndex = 0;
            }

        }

        private void btnUpdateCate_Info_Click(object sender, EventArgs e)
        {
            frmCategoryDevice frmCategory = new frmCategoryDevice();
            frmCategory.ShowDialog();
        }

        private void btnUpdateUnit_Info_Click(object sender, EventArgs e)
        {
            frmUnit frmunit = new frmUnit();
            frmunit.ShowDialog();
        }

        #region ++ load loại thiết bị lên combobox
        private void dropCateDevice_Add_Click(object sender, EventArgs e)
        {
        }

        private void dropUnitDevice_Add_Click(object sender, EventArgs e)
        {
        }

        private void dropSupplierDevice_Add_Click(object sender, EventArgs e)
        {
        }
        #endregion

        private void btnAddDevice_Add_Click(object sender, EventArgs e)
        {
            if (dropCateDevice_Add.Items.Count <= 0)
            {
                CustomAlert.Show("Chưa có loại thiết bị"); return;
            }
            if (dropUnitDevice_Add.Items.Count <= 0)
            {
                CustomAlert.Show("Chưa có đơn vị tính cho thiết bị");return;
            }
            if (dropSupplierDevice_Add.Items.Count <= 0)
            {
                CustomAlert.Show(" Chưa có đơn vị tính cho thiết bị");return;
            }
            string tentb = txtNameDevice_Add.Text.ToString();
            if (tentb.Length <= 0)
            {
                CustomAlert.Show("Chưa nhập tên thiết bị"); return;
            }
            int maloai =Convert.ToInt32(dropCateDevice_Add.SelectedValue) ;
            int madvt = Convert.ToInt32(dropUnitDevice_Add.SelectedValue);
            int mancc= Convert.ToInt32(dropSupplierDevice_Add.SelectedValue);
            int sl = 0;
            double dongiamua =Convert.ToDouble(txtPriceOutDevice_Add.Value);
            double dongiaban=Convert.ToDouble(txtPriceInDevice_Add.Value);
            int tgbh = Convert.ToInt32(txtTimeDevice_Add.Value);
            string nsx = txtNSX.Text.ToString();
            int tinhtrang = Convert.ToInt32(cbStatusDevice_Add.SelectedIndex.ToString());
            string mota = rtxtDescribeDevice_Add.Text.ToString();
            if(string.IsNullOrEmpty(mota))
            {
                mota = "Không có mô tả";
            }
            int tonmax=Convert.ToInt32(txtExistMaxDevice_Add.Value);
            int tommin=Convert.ToInt32(txtExistMinDevice_Add.Value);
            double khuyenmai = Convert.ToDouble(txtPriceSaleDevice_Add.Value)/100.0;
            Boolean result = ThietBiBus.Instance.AddThieiBi(maloai, madvt, mancc, tentb, sl, dongiamua, dongiaban, tgbh,
            nsx, tinhtrang, mota, tonmax, tommin, khuyenmai);
            if (result)
            {
                //refesh
                refeshAddDevice();
            }

        }
        public void refeshAddDevice()
        {
            rtxtDescribeDevice_Add.Text = txtNameDevice_Add.Text = string.Empty;
            txtPriceOutDevice_Add.Value = txtPriceInDevice_Add.Value = 0;
                txtTimeDevice_Add.Value=0;
            txtExistMaxDevice_Add.Value = txtExistMinDevice_Add.Value = txtPriceSaleDevice_Add.Value = 0;
        }

        private void btnResetDevice_Add_Click(object sender, EventArgs e)
        {
            refeshAddDevice();
        }

        private void btnSearchDevice_Info_Click(object sender, EventArgs e)
        {
            string matb = txtsearchDevice_Info.text.ToString();
            
            if (string.IsNullOrEmpty(matb)){
                refesh_Info();
                CustomAlert.Show("Mời nhập mã thiết bị!");
                return;
            }
            if (matb.Length>10)
            {
                refesh_Info();
                CustomMessageBox.show("Kết quả", "Thiết bị không tồn tại", false);
                return;
            }
            Boolean result= ThietBiBus.Instance.LoadTBByKey(matb,txtIdDevice_Info, txtnameDevice_Info, cbLoaiDevice_Info,
                cbDVTDevice_Info, txtNSXDevice_Info, cbNCCDevice_Info,
                txtQuoteDevice_Info, txtGiaDevice_Info, txtGiaMDevice_Info, txtBHDevice_Info,
                txtMaxDevice_Info, txtMinDevice_Info, txtKMDevice_Info, txtSLuongDevice_Info,
                cbTinhTrangDevice_Info);
            if (!result) refesh_Info();
            else
            {
                Code39BarcodeDraw barcodeDraw = BarcodeDrawFactory.Code39WithChecksum;
                piccode.Image = barcodeDraw.Draw(txtIdDevice_Info.Text.ToString(), 11);
            }
        }

        private void refesh_Info()
        {
            txtIdDevice_Info.Text = txtnameDevice_Info.Text =
               txtNSXDevice_Info.Text = txtQuoteDevice_Info.Text= string.Empty;
            txtGiaDevice_Info.Value = txtGiaMDevice_Info.Value = txtBHDevice_Info.Value =
            txtMaxDevice_Info.Value = txtMinDevice_Info.Value =
            txtKMDevice_Info.Value = txtSLuongDevice_Info.Value = 0;
            piccode.Image = null;
        }
        private void btnSaveDevice_Info_Click(object sender, EventArgs e)
        {
            string tentb = txtnameDevice_Info.Text.ToString();
            if (tentb.Length <= 0)
            {
                CustomAlert.Show("Chưa nhập tên thiết bị"); return;
            }
            string  matb = txtIdDevice_Info.Text.ToString();
            int maloai = Convert.ToInt32(cbLoaiDevice_Info.SelectedValue);
            int madvt = Convert.ToInt32(cbDVTDevice_Info.SelectedValue);
            int mancc = Convert.ToInt32(cbNCCDevice_Info.SelectedValue);
            int sl = Convert.ToInt32(txtSLuongDevice_Info.Value.ToString());
            double dongiamua = Convert.ToDouble(txtGiaMDevice_Info.Value);
            double dongiaban = Convert.ToDouble(txtGiaDevice_Info.Value);
            if(dongiamua > dongiaban)
            {
                if(CustomDialog1.show("Cảnh báo","Tiếp tục với đơn giá bán nhỏ hơn giá mua?", "Có", "Không")==DialogResult.No)
                {
                    return;
                }
            }
            int tgbh = Convert.ToInt32(txtBHDevice_Info.Value);
            string nsx = txtNSXDevice_Info.Text.ToString();
            int tinhtrang = Convert.ToInt32(cbTinhTrangDevice_Info.SelectedIndex.ToString());
            string mota = txtQuoteDevice_Info.Text.ToString();
            if (string.IsNullOrEmpty(mota))
            {
                mota = "Không có mô tả";
            }
            int tonmax = Convert.ToInt32(txtMaxDevice_Info.Value);
            int tonmin = Convert.ToInt32(txtMinDevice_Info.Value);
            if(tonmax < tonmin)
            {
                CustomMessageBox1.show("Số lượng tồn tối đa phải lớn hơn tồn tối thiểu");
                return;
            }
            double khuyenmai = Convert.ToDouble(txtKMDevice_Info.Value) / 100.0;
            Boolean result = ThietBiBus.Instance.UpdateTB(matb,maloai, madvt, mancc, tentb, sl, dongiamua, dongiaban, tgbh,
            nsx, tinhtrang, mota, tonmax, tonmin, khuyenmai);
            //caap nhat k thanh cong
            if (!result)
            {
                return;
            }
            //caap nhat  thanh cong
            btnUpdateDevice_Info.Enabled = true;
            btnSaveDevice_Info.Enabled = false;
            txtnameDevice_Info.Enabled = cbLoaiDevice_Info.Enabled =
                cbDVTDevice_Info.Enabled = txtNSXDevice_Info.Enabled = cbNCCDevice_Info.Enabled =
                txtQuoteDevice_Info.Enabled = txtGiaDevice_Info.Enabled = txtGiaMDevice_Info.Enabled = txtBHDevice_Info.Enabled =
                txtMaxDevice_Info.Enabled = txtMinDevice_Info.Enabled = txtKMDevice_Info.Enabled = txtSLuongDevice_Info.Enabled =
                cbTinhTrangDevice_Info.Enabled = false;
        }

        private void btnDeleteDevice_Info_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtIdDevice_Info.Text.ToString()))
            {
                if (ThietBiBus.Instance.DeleteThietBi(txtIdDevice_Info.Text.ToString()))
                {
                    refesh_Info();

                }

            }
          
        }
       
        private void dataTableDevices_Devices_FilterStringChanged(object sender, EventArgs e)
        {
            bindingSource1.Filter = dataTableDevices_Devices.FilterString;
        }

        private void dataTableDevices_Devices_SortStringChanged(object sender, EventArgs e)
        {
            bindingSource1.Sort = dataTableDevices_Devices.SortString;
        }

        private void btnInEx_Excel_Click(object sender, EventArgs e)
        {
            if (dataTableDevicesExcel.CurrentRow != null)
            {
                for (int i = 0;i < dataTableDevicesExcel.SelectedRows.Count; i++)
                {
                    string matb = dataTableDevicesExcel.SelectedRows[i].Cells[0].Value.ToString();
                    try
                    {
                        var item = datatableoutDevicesExcel.Rows.Cast<DataGridViewRow>().First(r => r.Cells[1].Value.ToString().Equals(matb));
                        if (item != null) continue;//da ton tai
                    }
                    catch { };

                    datatableoutDevicesExcel.Rows.Add(datatableoutDevicesExcel.RowCount + 1, matb,
                        dataTableDevicesExcel.SelectedRows[i].Cells[1].Value.ToString(),
                        dataTableDevicesExcel.SelectedRows[i].Cells[2].Value.ToString(), dataTableDevicesExcel.SelectedRows[i].Cells[3].Value.ToString(), dataTableDevicesExcel.SelectedRows[i].Cells[4].Value.ToString());

                    datatableoutDevicesExcel.Refresh();
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

        private void datatableoutDevicesExcel_Sorted(object sender, EventArgs e)
        {
            UpdateSTTGrid(datatableoutDevicesExcel);
        }

        private void btnOutEx_Excel_Click(object sender, EventArgs e)
        {
            if (datatableoutDevicesExcel.SelectedRows != null)
            {              
                foreach (DataGridViewRow row in datatableoutDevicesExcel.SelectedRows)
                {
                    if (!row.IsNewRow)
                    {
                        datatableoutDevicesExcel.Rows.Remove(row);
                    }

                }
                UpdateSTTGrid(datatableoutDevicesExcel);
                datatableoutDevicesExcel.Refresh();
            }
            
        }

        private void frmDevice_Load(object sender, EventArgs e)
        {
            btnAddDivices.Enabled = TaiKhoanBUS.Instance.checkRoleUser(IDTK, key_menu_ThemTB);
           // btnReportExcel.Enabled = TaiKhoanBUS.Instance.checkRoleUser(IDTK, key_menu_BaogiaTB);
             btnExcel.Enabled = TaiKhoanBUS.Instance.checkRoleUser(IDTK, key_menu_BaogiaTB);
            k_sua = TaiKhoanBUS.Instance.checkRoleUser(IDTK, key_Sua);

            k_xoa = TaiKhoanBUS.Instance.checkRoleUser(IDTK, key_Xoa);
        }

        private void btnPrintEx_Excel_Click(object sender, EventArgs e)
        {
            if (datatableoutDevicesExcel.Rows.Count == 0) return;
            object misValue = System.Reflection.Missing.Value;
            Excel.Application app = new Excel.Application();
            app.Visible = false;
            Excel.Workbook workbook = app.Workbooks.Add(Excel.XlWBATemplate.xlWBATWorksheet);
            Excel.Worksheet worksheet = (Excel.Worksheet)workbook.ActiveSheet;
            worksheet.Name = "DS";

            ///name
            worksheet.Cells[1, 1] = "Bảng báo giá sản phẩm";
            worksheet.Cells[1, 1].EntireRow.Font.Bold = true;
            Excel.Range range = worksheet.Range[worksheet.Cells[1, 1], worksheet.Cells[1, datatableoutDevicesExcel.Columns.Count]];
            range.Merge(true);
            range.Interior.ColorIndex = 36;
            range.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Red);
            range.Font.Size = 16;
            range.HorizontalAlignment = 3;
            range.VerticalAlignment = 3;
            ///header 

            for (int j = 0; j < datatableoutDevicesExcel.Columns.Count; j++)
                {

                 worksheet.Cells[3, j+1] = datatableoutDevicesExcel.Columns[j].HeaderText.ToString();
                }
  
            ///content

            for (int i = 0; i < datatableoutDevicesExcel.Rows.Count; i++)
            {
                for (int j = 0; j < datatableoutDevicesExcel.Columns.Count; j++)
                {
                    worksheet.Cells[i + 4, j + 1] = datatableoutDevicesExcel.Rows[i].Cells[j].Value.ToString();
                }
            }
            worksheet.Columns.ColumnWidth = 30;

            var savefileDialog = new SaveFileDialog();
            savefileDialog.FileName = "output_Excel";
            savefileDialog.Filter = "Excel files (*.xlsx)|*.xlsx|All files (*.*)|*.*";
            savefileDialog.DefaultExt = ".xlsx";
            if(savefileDialog.ShowDialog() ==DialogResult.OK)
            {
                workbook.SaveAs(savefileDialog.FileName, misValue, misValue, misValue, misValue, misValue, Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);

            }
           
            workbook.Close(true, misValue, misValue);
            app.Quit();

        }

        
        private void btnHelpEx_Excel_Click(object sender, EventArgs e)
        {
            
        }

        private void btnAddSupplier_Add_Click(object sender, EventArgs e)
        {
            CustomAlert.Show("Vui lòng mở danh mục nhà cung cấp");
        }

        private void btnInDevices_Quote_Click(object sender, EventArgs e)
        {

        }

        private void btnOutDevices_Quote_Click(object sender, EventArgs e)
        {

        }

        private void txtMaxDevice_Info_Validating(object sender, CancelEventArgs e)
        {

        }

        private void btnResetEx_Excel_Click(object sender, EventArgs e)
        {
            datatableoutDevicesExcel.DataSource = null;
            datatableoutDevicesExcel.Rows.Clear();
        }

        private void btnSaphet_Fliter_De_Click(object sender, EventArgs e)
        {
            lblTenDSTim.Text = " Danh sách hàng sắp hết";

          
            ThietBiBus.Instance.LoadDSTBSapHet(dataTableFliter_De);
            removeFilterAndSortBindingSource(bindingSource1, dataTableFliter_De);

        }

        private void btnHangBanFliter_De_Click(object sender, EventArgs e)
        {
            lblTenDSTim.Text = " Danh sách số lượng từng hàng đã bán";
            if (datebtnFromDecs.Value > datebtnToDecs.Value)
            {
                CustomMessageBox1.show("Giá trị thời gian ngược");
                return;
            }
            Byte tt = 0;
            if (cbStatusSold_Se.SelectedItem != null) tt = Convert.ToByte(cbStatusSold_Se.SelectedValue.ToString());
            ThietBiBus.Instance.LoadDSTBSoLuongBanNgay(datebtnFromDecs.Value, datebtnToDecs.Value, tt, dataTableFliter_De);
            removeFilterAndSortBindingSource(bindingSource1, dataTableFliter_De);

        }

        private void btnTBNhap_Fliter_De_Click(object sender, EventArgs e)
        {
            lblTenDSTim.Text = " Danh sách số lượng từng hàng đã nhập ";
            if (datebtnFromDecs.Value > datebtnToDecs.Value)
            {
                CustomMessageBox1.show("Giá trị thời gian ngược");
                return;
            }
            Byte tt = 0;
            if (cbStatusBuy_Se.SelectedItem != null) tt = Convert.ToByte(cbStatusBuy_Se.SelectedValue.ToString());
            ThietBiBus.Instance.LoadDSTBSoLuongMuaNgay(datebtnFromDecs.Value, datebtnToDecs.Value, tt, dataTableFliter_De);
            removeFilterAndSortBindingSource(bindingSource1, dataTableFliter_De);

        }

        private void btnHanbanChayFliter_De_Click(object sender, EventArgs e)
        {

        }

        private void dataTableFliter_De_SortStringChanged(object sender, EventArgs e)
        {
            bindingSource1.Sort = dataTableFliter_De.SortString;
            UpdateSTTGrid(dataTableFliter_De);
        }
       
        private void dataTableFliter_De_FilterStringChanged(object sender, EventArgs e)
        {
            bindingSource1.Filter = dataTableFliter_De.FilterString;
            UpdateSTTGrid(dataTableFliter_De);
        }

        private void btnAllHangBanFliter_De_Click(object sender, EventArgs e)
        {
            lblTenDSTim.Text = " Danh sách số lượng từng hàng đã bán (Tất cả)";

            ThietBiBus.Instance.LoadALLDSTBSoLuongBanNgay( dataTableFliter_De);
            removeFilterAndSortBindingSource(bindingSource1, dataTableFliter_De);
        }

        private void  removeFilterAndSortBindingSource(BindingSource bin ,DataGridView gv)
        {
            bin.Filter = bin.Sort = null;
            bin.DataSource = null;
            bin.DataSource = gv.DataSource;
        }
        private void btnAllHangMuaFliter_De_Click(object sender, EventArgs e)
        {
            lblTenDSTim.Text = " Danh sách số lượng từng hàng đã nhập (Tất cả)";
            ThietBiBus.Instance.LoadALLDSTBSoLuongMuaNgay(dataTableFliter_De);
            removeFilterAndSortBindingSource(bindingSource1, dataTableFliter_De);

        }

        private void txtSLuongDevice_Info_ValueChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtSLuongDevice_Info.Value.ToString()))
            {
                txtSLuongDevice_Info.Value = 0;
                return;
            }
            try
            {
                int.Parse(txtSLuongDevice_Info.Value.ToString());
            }
            catch {
                txtSLuongDevice_Info.Value = 0;
            }
        }

        private void txtKMDevice_Info_ValueChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtKMDevice_Info.Value.ToString()))
            {
                txtKMDevice_Info.Value = 0;
                return;
            }
            try
            {
                Double.Parse(txtKMDevice_Info.Value.ToString());
            }
            catch
            {
                txtKMDevice_Info.Value = 0;
            }
        }

        private void txtMinDevice_Info_ValueChanged(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(txtMinDevice_Info.Value.ToString()))
            {
                txtMinDevice_Info.Value = 0;
                return;
            }
            try
            {
                int.Parse(txtMinDevice_Info.Value.ToString());
            }
            catch
            {
                txtMinDevice_Info.Value = 0;
            }
        }

        private void txtMaxDevice_Info_ValueChanged(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(txtMaxDevice_Info.Value.ToString()))
            {
                txtMaxDevice_Info.Value = 0;
                return;
            }
            try
            {
                int.Parse(txtMaxDevice_Info.Value.ToString());
            }
            catch
            {
                txtMaxDevice_Info.Value = 0;
            }
        }

        private void txtBHDevice_Info_ValueChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtBHDevice_Info.Value.ToString()))
            {
                txtBHDevice_Info.Value = 0;
                return;
            }
            try
            {
                int.Parse(txtBHDevice_Info.Value.ToString());
            }
            catch
            {
                txtBHDevice_Info.Value = 0;
            }
        }

        private void txtGiaMDevice_Info_ValueChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtGiaMDevice_Info.Value.ToString()))
            {
                txtGiaMDevice_Info.Value = 0;
                return;
            }
            try
            {
                Double.Parse(txtGiaMDevice_Info.Value.ToString());
            }
            catch
            {
                txtGiaMDevice_Info.Value = 0;
            }
        }

        private void txtGiaDevice_Info_ValueChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtGiaDevice_Info.Value.ToString()))
            {
                txtGiaDevice_Info.Value = 0;
                return;
            }
            try
            {
                Double.Parse(txtGiaDevice_Info.Value.ToString());
            }
            catch
            {
                txtGiaDevice_Info.Value = 0;
            }
        }

        private void txtPriceOutDevice_Add_ValueChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtPriceOutDevice_Add.Value.ToString()))
            {
                txtPriceOutDevice_Add.Value = 0;
                return;
            }
            try
            {
                int.Parse(txtPriceOutDevice_Add.Value.ToString());
            }
            catch
            {
                txtPriceOutDevice_Add.Value = 0;
            }
        }

        private void txtPriceInDevice_Add_ValueChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtPriceInDevice_Add.Value.ToString()))
            {
                txtPriceInDevice_Add.Value = 0;
                return;
            }
            try
            {
                int.Parse(txtPriceInDevice_Add.Value.ToString());
            }
            catch
            {
                txtPriceInDevice_Add.Value = 0;
            }
        }

        private void txtTimeDevice_Add_ValueChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtTimeDevice_Add.Value.ToString()))
            {
                txtTimeDevice_Add.Value = 0;
                return;
            }
            try
            {
                int.Parse(txtTimeDevice_Add.Value.ToString());
            }
            catch
            {
                txtTimeDevice_Add.Value = 0;
            }
        }

        private void txtExistMaxDevice_Add_ValueChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtExistMaxDevice_Add.Value.ToString()))
            {
                txtExistMaxDevice_Add.Value = 0;
                return;
            }
            try
            {
                int.Parse(txtExistMaxDevice_Add.Value.ToString());
            }
            catch
            {
                txtExistMaxDevice_Add.Value = 0;
            }
        }

        private void txtExistMinDevice_Add_ValueChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtExistMinDevice_Add.Value.ToString()))
            {
                txtExistMinDevice_Add.Value = 0;
                return;
            }
            try
            {
                int.Parse(txtExistMinDevice_Add.Value.ToString());
            }
            catch
            {
                txtExistMinDevice_Add.Value = 0;
            }
        }

        private void txtPriceSaleDevice_Add_ValueChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtPriceSaleDevice_Add.Value.ToString()))
            {
                txtGiaDevice_Info.Value = 0;
                return;
            }
            try
            {
                Double.Parse(txtPriceSaleDevice_Add.Value.ToString());
            }
            catch
            {
                txtPriceSaleDevice_Add.Value = 0;
            }
        }

        private void tabPageHome_Click(object sender, EventArgs e)
        {

        }



        private void btnPrintDevices_Quote_Click(object sender, EventArgs e)
        {

           
        }

        private void btnHelpDevices_Quote_Click(object sender, EventArgs e)
        {

        }

        private void btnPrintDes_Des_Click(object sender, EventArgs e)
        {
            if (dataTableDevices_Devices.Rows.Count == 0) return;
            object misValue = System.Reflection.Missing.Value;
            Excel.Application app = new Excel.Application();
            app.Visible = false;
            Excel.Workbook workbook = app.Workbooks.Add(Excel.XlWBATemplate.xlWBATWorksheet);
            Excel.Worksheet worksheet = (Excel.Worksheet)workbook.ActiveSheet;
            worksheet.Name = "DS";


            ///name
            worksheet.Cells[1, 1] = "Danh sách thiết bị";
            worksheet.Cells[1, 1].EntireRow.Font.Bold = true;
            Excel.Range range = worksheet.Range[worksheet.Cells[1, 1], worksheet.Cells[1, dataTableDevices_Devices.Columns.Count]];
            range.Merge(true);
            range.Interior.ColorIndex = 36;
            range.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Red);
            range.Font.Size = 16;
            range.HorizontalAlignment = 3;
            range.VerticalAlignment = 3;
            ///header 

            for (int j = 0; j < dataTableDevices_Devices.Columns.Count; j++)
            {

                worksheet.Cells[3, j + 1] = dataTableDevices_Devices.Columns[j].HeaderText.ToString();

            }
            Excel.Range rangeHeader = worksheet.Range[worksheet.Cells[3, 1], worksheet.Cells[3, dataTableDevices_Devices.Columns.Count]];
            rangeHeader.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Red);

            rangeHeader.Font.Size = 13;
            rangeHeader.Font.Bold = true;
            ///content

            for (int i = 0; i < dataTableDevices_Devices.Rows.Count; i++)
            {
                for (int j = 0; j < dataTableDevices_Devices.Columns.Count; j++)
                {
                    worksheet.Cells[i + 4, j + 1] = dataTableDevices_Devices.Rows[i].Cells[j].Value.ToString();
                }
            }
            range.Merge(true);
            range.Interior.ColorIndex = 36;

            #region ///footer
            //int indexfooter = dataTableDevices_Devices.Rows.Count + 6;
            //Excel.Range rangeFooter = worksheet.Range[worksheet.Cells[indexfooter, 7], worksheet.Cells[indexfooter + 5, 8]];
            //rangeFooter.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Red);
            //rangeFooter.Font.Size = 13;
            //rangeFooter.Font.Bold = true;

            //rangeFooter.Columns.ColumnWidth = 30;
            //worksheet.Cells[indexfooter, 7] = "Tổng";
            //worksheet.Cells[indexfooter++, 7] = "Tổng phí giao";
            //worksheet.Cells[indexfooter, 8] = double.Parse(lblTongPhiGiaoSolds.Text.ToString());
            //worksheet.Cells[indexfooter++, 7] = "Tổng phụ thu";
            //worksheet.Cells[indexfooter, 8] = double.Parse(lblTongPhuThuSolds.Text.ToString());
            //worksheet.Cells[indexfooter++, 7] = "Tổng trị giá hóa đơn";
            //worksheet.Cells[indexfooter, 8] = double.Parse(lblTongHDSolds.Text.ToString());
            //worksheet.Cells[indexfooter++, 7] = "Tổng đã thanh toán";
            //worksheet.Cells[indexfooter, 8] = double.Parse(lblDaThanhToanSolds.Text.ToString());
            #endregion

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

        private void btnBarcodeEx_Excel_Click(object sender, EventArgs e)
        {

        }

        private void btnPrintBarcode_Click(object sender, EventArgs e)
        {
            frmBarCode frm = new frmBarCode();
            frm.ShowDialog();
        }

        private void btnChart_Click(object sender, EventArgs e)
        {

        }
        private void chart()
        {
            cartesianChart1.Series.Clear();
            cartesianChart1.AxisX.Clear();
            cartesianChart1.AxisY.Clear();
            using (DATADataContext db = new DATADataContext())
            {
                var info = db.sp_TB_Getlist_AllSoLuongBanNgay();

                ColumnSeries col = new ColumnSeries()
                {
                    DataLabels = true,
                    Values = new LiveCharts.ChartValues<int>(),
                    
                    
                    LabelPoint = point => point.Y.ToString(),
                    
                     

                };
                Axis axis = new Axis() { Separator = new Separator() { Step = 1, IsEnabled = false } };
                axis.Labels = new List<string>();
                axis.Title = "Thông kê bán hàng bán ";


                foreach (var x in info)
                {
                   // col.Values.Add(x.MAH.ToString());
                    col.Values.Add(x.SL);
                }
                cartesianChart1.Series.Add(col);
                cartesianChart1.AxisX.Add(axis);

                cartesianChart1.AxisY.Add(new Axis { LabelFormatter = value => value.ToString(), Separator = new Separator() });
            }
        }

        private void btnChart_Click_1(object sender, EventArgs e)
        {
            chart();
        }

        private void btnUpdateDevice_Info_Click(object sender, EventArgs e)
        {
            if (txtIdDevice_Info.Text.Length <= 0) return;
            btnSaveDevice_Info.Enabled = true;
            txtnameDevice_Info.Enabled= cbLoaiDevice_Info.Enabled =
                cbDVTDevice_Info.Enabled = txtNSXDevice_Info.Enabled = cbNCCDevice_Info.Enabled =
                txtQuoteDevice_Info.Enabled = txtGiaDevice_Info.Enabled = txtGiaMDevice_Info.Enabled = txtBHDevice_Info.Enabled =
                txtMaxDevice_Info.Enabled = txtMinDevice_Info.Enabled = txtKMDevice_Info.Enabled = 
                cbTinhTrangDevice_Info.Enabled = true;

        }
    }
}
