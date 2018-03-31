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
    public partial class frmProFix : Form
    {
        public int flat = 0;
        public int tabindex = 1;
        public string IDTK = "";
        public frmProFix()
        {
            InitializeComponent();
            openTabPage(tabindex);
        }
        private void block_menu()
        {
            //phieu bao hanh
            Boolean them = TaiKhoanBUS.Instance.checkRoleUser(IDTK, Properties.Settings.Default.cnLapPBH);
            Boolean xem = TaiKhoanBUS.Instance.checkRoleUser(IDTK, Properties.Settings.Default.cnXemPBH);

            if (them || xem)
            {
                btnAddPro.Enabled = true;
            }
            else btnAddPro.Enabled = false;
            btnPros.Enabled = TaiKhoanBUS.Instance.checkRoleUser(IDTK, Properties.Settings.Default.cnXemDSPBH);
            ///bao hanh
            them = TaiKhoanBUS.Instance.checkRoleUser(IDTK, Properties.Settings.Default.cnLapBH);
            xem = TaiKhoanBUS.Instance.checkRoleUser(IDTK, Properties.Settings.Default.cnXemDetailBH);
            if (them || xem)
            {
                btnAddUPro.Enabled = true;
            }
            else btnAddUPro.Enabled = false;
            btnUPros.Enabled = TaiKhoanBUS.Instance.checkRoleUser(IDTK, Properties.Settings.Default.cnXemDSBH);
            //sua chua
            btnAddFix.Enabled = TaiKhoanBUS.Instance.checkRoleUser(IDTK, Properties.Settings.Default.cnLapSC);
            btnFixs.Enabled = TaiKhoanBUS.Instance.checkRoleUser(IDTK, Properties.Settings.Default.cnXemDSSC);



           
           


        }
        public void openTabPage(int tab)
        {
            tabProFix.TabPages.Clear();
            switch (tab)
            {
                case 1://tab home
                    tabProFix.TabPages.Add(tabPageHome);
                    break;
                case 2://tab add - update Pro
                    tabProFix.TabPages.Add(tabPageAddPro);
                    break;
                case 3://tab Pros
                    tabProFix.TabPages.Add(tabPagePros);
                    break;
                case 4://tab add - update UPro
                    tabProFix.TabPages.Add(tabPageAddUPro);
                    break;
                case 5://tab UPros
                    tabProFix.TabPages.Add(tabPageUPros);
                     break;
                case 6://tab add - update Fix
                    tabProFix.TabPages.Add(tabPageAddFix);
                    break;
                case 7://tab Fixs
                    tabProFix.TabPages.Add(tabPageFixs);
                    break;
                default://khong co quyen
                    break;
            }
        }
        private void btnAddFixBack_Click(object sender, EventArgs e)
        {
            openTabPage(1);
        }

        private void btnAddProBack_Click(object sender, EventArgs e)
        {
            openTabPage(1);
        }

        private void btnSearchProsBack_Click(object sender, EventArgs e)
        {
            openTabPage(1);
        }

        private void btnFixsBack_Click(object sender, EventArgs e)
        {
            openTabPage(1);
        }

        private void btnUProsBack_Click(object sender, EventArgs e)
        {
            openTabPage(1);
           
        }

        private void btnAddPro_Click(object sender, EventArgs e)
        {
            openTabPage(2);
            datatableDecivePro_Add.RowsDefaultCellStyle.BackColor = Color.White;
            datatableDecivePro_Add.AlternatingRowsDefaultCellStyle.BackColor = Color.LightBlue;

            #region ===load tình trạng phiếu bảo hành
            DataTable table = new DataTable();
            table.Columns.Add("MATT");
            table.Columns.Add("TEN");
            table.Rows.Add(1, "Còn hạn bảo hành");
            table.Rows.Add(2, "Hết hạn bảo hành");
            table.Rows.Add(0, "Đã hủy"); 
            dropStatusProAdd.DataSource = table;
            dropStatusProAdd.DisplayMember = "TEN";
            dropStatusProAdd.ValueMember = "MATT";
            dropStatusProAdd.Enabled = false;
            #endregion
            refesh_Add_Update_Pro();
            txtsearchSold_Id.text = string.Empty;
            //khoa chuc nang
            btnFindPro_Add.Enabled = TaiKhoanBUS.Instance.checkRoleUser(IDTK, Properties.Settings.Default.cnXemPBH);
            btnAddPro_Add.Enabled= TaiKhoanBUS.Instance.checkRoleUser(IDTK, Properties.Settings.Default.cnLapPBH);
            
        }
        private void UpdateSTTGrid(DataGridView gridView)
        {
            for (int i = 0; i < gridView.RowCount; i++)
            {
                gridView.Rows[i].Cells[0].Value = i + 1;

            }
        }
        #region ==========REFESH
        ///REFESH TAB LẬP VÀ SỬA PHIẾU BẢO HÀNH
        private void refesh_Add_Update_Pro()
        {
           
           txtIdSold_ProAdd.Text = txtNumInProAdd.Text =
            txtNumOutProAdd.Text = txtTimeSoldPro_Add.Text = txtNumproPro_Add.Text =
            txtSeriPro_Add.Text = string.Empty;
            dropDeciveSold_ProAdd.DataSource = datatableDecivePro_Add.DataSource=null;
            datatableDecivePro_Add.Rows.Clear();
        }
        ///REFESH TAB LẬP VÀ SỬA  BẢO HÀNH
        private void refesh_Add_Update_UPro()
        {
              txtIdProUPro_Add.Text=txtidDeviceUPro_Add.Text =
                txtStaPro_Add.Text = rtxtCauseUPro_Add.Text=
            txtSeriDeciUPro_Add.Text = txtidCustomer_Add.Text =
              txtNameCustomerUpro_Add.Text = txtPhoneCustomerUpro_Add.Text = string.Empty;
            lbltinhtrang.Text = "0";
            datatableUPro_Add.DataSource  = null;
            datatableUPro_Add.Rows.Clear();
        }
        private void  refesh_Add_Update_Fix()
        {
           txtPhoneAdd_Fix.Text = txtNameCustomerAdd_Fix.Text = rtxtDescipAddAdd_Fix.Text= rtxtErrDecAddAdd_Fix.Text=string.Empty;
            datepicFromAdd_Fix.Value = datepicToAdd_Fix.Value =DateTime.Parse(DateTime.Now.ToShortDateString());
            txtPriceFIxDeAddAdd_Fix.Text = "0";
            datatableDeciveAddAdd_Fix.DataSource = null;
            datatableDeciveAddAdd_Fix.Rows.Clear();
        }
        #endregion

        private void btnAddUPro_Click(object sender, EventArgs e)
        {
            openTabPage(4);
            #region ===load tình trạng phiếu bảo hành
            DataTable table = new DataTable();
            table.Columns.Add("MATT");
            table.Columns.Add("TEN");
            table.Rows.Add(2, "Đang bảo hành");
            table.Rows.Add(1, "Đã trả");
            table.Rows.Add(0, "Đã hủy");
            dropStatusUPro_Add.DataSource = table;
            dropStatusUPro_Add.DisplayMember = "TEN";
            dropStatusUPro_Add.ValueMember = "MATT";
            dropStatusUPro_Add.Enabled = false;
            refesh_Add_Update_UPro();
            #endregion

            //khoa chuc nang
            btnSearchPBHUPro_Add.Enabled = TaiKhoanBUS.Instance.checkRoleUser(IDTK, Properties.Settings.Default.cnXemDetailBH);

        }

        private void btnAddFix_Click(object sender, EventArgs e)
        {
            openTabPage(6);
            //load ds khách hàng lên combobox
            HoaDonBanBus.Instance.loadDSKhachHangHDB(cbIDCustomerAdd_Fix);
            //load ds thiết bị lên combobox
            ThietBiBus.Instance.LoadTBIDNameHDB(cbIDDeviceAdd_Fix);
            //load ds tinhf trang sua chua tung thiet bi len combobox
            BaoHanh_SuaChuaBus.Instance.loadTinhTrangTBSC(dropstatusAdd_Fix);
            //load ds tinh trang phieu sua chua  len combobox
            BaoHanh_SuaChuaBus.Instance.loadTinhTrangHDSCThem(cbStatusFix_Add);

            datepicFromAdd_Fix.Value = datepicToAdd_Fix.Value= DateTime.Parse(DateTime.Now.ToShortDateString());
            refesh_Add_Update_Fix();
            btnUpdateInfo_Fix.Visible = false;
            btnAddAdd_Fix.Enabled = btnSaveAddAdd_Fix.Enabled = btnUpdateAddAdd_Fix.Enabled = btnDeleteAddAdd_Fix.Enabled = true;
            //khoa chuc nang
            btnAddSC_Add.Enabled = TaiKhoanBUS.Instance.checkRoleUser(IDTK, Properties.Settings.Default.cnLapSC);
            btnAddPro.Enabled = TaiKhoanBUS.Instance.checkRoleUser(IDTK, Properties.Settings.Default.cnXuatSC);
            lblIDSC.Text = "0";
        }

        private void btnPros_Click(object sender, EventArgs e)
        {
            openTabPage(3);
            datatablePros_Pros.RowsDefaultCellStyle.BackColor = Color.White;
            datatablePros_Pros.AlternatingRowsDefaultCellStyle.BackColor = Color.LightBlue;
            BaoHanh_SuaChuaBus.Instance.LoadDSPBH(datatablePros_Pros);
            removeFilterAndSortBindingSource(bindingSourcePBH, datatablePros_Pros);

        }

        private void btnUPros_Click(object sender, EventArgs e)
        {
            openTabPage(5);
            datatableUPros_UPros.RowsDefaultCellStyle.BackColor = Color.White;
            datatableUPros_UPros.AlternatingRowsDefaultCellStyle.BackColor = Color.LightBlue;
            BaoHanh_SuaChuaBus.Instance.LoadDSBH(datatableUPros_UPros);
            removeFilterAndSortBindingSource(bindingSourcePBH, datatableUPros_UPros);
        }

        private void btnFixs_Click(object sender, EventArgs e)
        {
            openTabPage(7);
            datatableFixs_Fixs.RowsDefaultCellStyle.BackColor = Color.White;
            datatableFixs_Fixs.AlternatingRowsDefaultCellStyle.BackColor = Color.LightBlue;
            BaoHanh_SuaChuaBus.Instance.LoadDSSC(datatableFixs_Fixs);
            removeFilterAndSortBindingSource(bindingSourcePBH, datatableFixs_Fixs);
            gvCTSC_ByID_Fixs.RowsDefaultCellStyle.BackColor = Color.White;
            gvCTSC_ByID_Fixs.AlternatingRowsDefaultCellStyle.BackColor = Color.LightBlue;
            gvCTSC_ByID_Fixs.DataSource = null;
            gvCTSC_ByID_Fixs.Rows.Clear();
            lblIDSC_FIXs.Text = "";
        }

        private void btnUProBack_Click(object sender, EventArgs e)
        {
            openTabPage(1);
        }

       

       

        #region ====thực thi tìm
        private void btnFindPro_Add_Click(object sender, EventArgs e)
        {
            Boolean result = false;
                if (string.IsNullOrEmpty(txtsearchSold_Id.text.ToString())){
                    CustomMessageBox1.show("Bạn chưa nhập mã hóa đơn");return;}
                result = BaoHanh_SuaChuaBus.Instance.LoadTB_HDB(dropDeciveSold_ProAdd, txtsearchSold_Id.text.ToString().ToUpper());

            if (!result) refesh_Add_Update_Pro();
            else
            {
                BaoHanh_SuaChuaBus.Instance.LoadDSTB(txtsearchSold_Id.text.ToString(), datatableDecivePro_Add);
                removeFilterAndSortBindingSource(bindingSourcePBH, datatableDecivePro_Add);
                txtIdSold_ProAdd.Text = txtsearchSold_Id.text.ToString().ToUpper();
            }
        }
        #endregion

        private void dropDeciveSold_ProAdd_DataSourceChanged(object sender, EventArgs e)
        {
            if(dropDeciveSold_ProAdd.Items.Count > 0)
            {
                try
                {
                    dropDeciveSold_ProAdd.SelectedIndex = 0;
                }
                catch { }
                
            }
        }

        private void dropDeciveSold_ProAdd_SelectedValueChanged(object sender, EventArgs e)
        {
            if (dropDeciveSold_ProAdd.Items.Count <= 0 || string.IsNullOrEmpty(txtIdSold_ProAdd.Text.ToString()))
                return;
            try
            {
                BaoHanh_SuaChuaBus.Instance.LoadTB_HDB_MH(dropDeciveSold_ProAdd, txtIdSold_ProAdd.Text.ToString(),
               txtNumInProAdd, txtNumOutProAdd, txtTimeSoldPro_Add, txtNumproPro_Add);
                if (txtNumInProAdd.Text.Equals( txtNumOutProAdd.Text))
                {
                    txtSeriPro_Add.Enabled = btnAddPro_Add.Enabled =  false;
                }
                else
                {
                    txtSeriPro_Add.Enabled = btnAddPro_Add.Enabled = true;
                }
            }
            catch { }
        }

        private void btnAddPro_Add_Click(object sender, EventArgs e)
        {
            if (dropDeciveSold_ProAdd.Items.Count <= 0) return;
            if(txtNumInProAdd.Text.Length <= 0)
            {
                CustomMessageBox1.show("Vui lòng chọn thiết bị");
                return;
            }
            string seri = txtSeriPro_Add.Text.ToString();
            if (string.IsNullOrEmpty(seri))
            {
                CustomMessageBox1.show("Vui lòng nhập số Serial");
                return;
            }
            Boolean result = false;
            result = BaoHanh_SuaChuaBus.Instance.addPBH(dropDeciveSold_ProAdd.SelectedValue.ToString(),
                txtIdSold_ProAdd.Text.ToString(),seri,IDTK);
            if (result)
            {
                DialogResult dialogResult = CustomDialog1.show("Thông báo", "Bạn có muốn xuất hóa đơn", "Có", "Không");
                if (dialogResult == DialogResult.OK)
                {

                    frmXuatSuaChua frm = new frmXuatSuaChua();
                    frm.mahd = seri;
                    frm.key = 2;
                    frm.ShowDialog();
                }
                BaoHanh_SuaChuaBus.Instance.LoadDSTB(txtIdSold_ProAdd.Text.ToString(), datatableDecivePro_Add);
                removeFilterAndSortBindingSource(bindingSourcePBH, datatableDecivePro_Add);
                try
                {

                }
                catch {
                    int sl = Int32.Parse(txtNumOutProAdd.Text.ToString());
                    sl--;
                    txtNumOutProAdd.Text = sl.ToString();
                }
                txtSeriPro_Add.Text = string.Empty;
            }              
        }
        private void removeFilterAndSortBindingSource(BindingSource bin, DataGridView gv)
        {
            bin.Filter = bin.Sort = null;
            bin.DataSource = null;
            bin.DataSource = gv.DataSource;
        }

        private void datatableDecivePro_Add_SortStringChanged(object sender, EventArgs e)
        {
            bindingSourcePBH.Sort = datatableDecivePro_Add.SortString;
            UpdateSTTGrid(datatableDecivePro_Add);
        }

        private void datatableDecivePro_Add_FilterStringChanged(object sender, EventArgs e)
        {
            bindingSourcePBH.Filter = datatableDecivePro_Add.FilterString;
            UpdateSTTGrid(datatableDecivePro_Add);

        }

        private void btnRefeshPro_Add_Click(object sender, EventArgs e)
        {
            refesh_Add_Update_Pro();
        }

        private void txtNumInProAdd_OnValueChanged(object sender, EventArgs e)
        {

           
            
        }

        private void txtNumOutProAdd_OnValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtNumInProAdd.Text.ToString()))
                {
                    int slm = Int32.Parse(txtNumOutProAdd.Text.ToString());
                    int slbh = Int32.Parse(txtNumInProAdd.Text.ToString());
                    if (slm == slbh)
                        btnAddPro_Add.Enabled = false;
                    else
                        btnAddPro_Add.Enabled = true;
                }

            }
            catch { }
        }

        private void datatablePros_Pros_SortStringChanged(object sender, EventArgs e)
        {
            bindingSourcePBH.Sort = datatablePros_Pros.SortString;

        }

        private void datatablePros_Pros_FilterStringChanged(object sender, EventArgs e)
        {
           bindingSourcePBH.Filter = datatablePros_Pros.FilterString;

        }

        private void datatableFixs_Fixs_FilterStringChanged(object sender, EventArgs e)
        {
            bindingSourcePBH.Filter = datatableFixs_Fixs.FilterString;
        }

        private void datatableFixs_Fixs_SortStringChanged(object sender, EventArgs e)
        {
            bindingSourcePBH.Sort = datatableFixs_Fixs.SortString;
        }

        private void datatableUPros_UPros_FilterStringChanged(object sender, EventArgs e)
        {
            bindingSourcePBH.Filter = datatableUPros_UPros.FilterString;
        }

        private void datatableUPros_UPros_SortStringChanged(object sender, EventArgs e)
        {
            bindingSourcePBH.Sort = datatableUPros_UPros.SortString;
        }

        private void btnSearchPBHUPro_Add_Click(object sender, EventArgs e)
        {
            if (!btnUpdateUPro_Add.Enabled) return;
            Boolean result = false;
            if (string.IsNullOrEmpty(txtIdUPro_Add.text))
            {
                CustomMessageBox1.show("Bạn chưa nhập mã hóa đơn"); return;
            }
            try
            {
                Convert.ToInt32(txtIdUPro_Add.text.ToString());
            }
            catch
            {
                CustomMessageBox1.show("Mã phiếu bảo hành không tồn tại"); return;
            }
            dropStatusUPro_Add.Enabled = true;
            result = BaoHanh_SuaChuaBus.Instance.LoadPBH_ID(Convert.ToInt32(txtIdUPro_Add.text.ToString()),
              txtIdProUPro_Add, txtidDeviceUPro_Add, txtStaPro_Add, txtSeriDeciUPro_Add, txtidCustomer_Add,
              txtNameCustomerUpro_Add, txtPhoneCustomerUpro_Add, lbltinhtrang);

            if (!result) refesh_Add_Update_UPro();
            else
            {

                //try {
                    int maphieu = 0;
                    maphieu = Convert.ToInt32(txtIdProUPro_Add.Text.ToString());
                    BaoHanh_SuaChuaBus.Instance.LoadDSBH_IDPBH(maphieu, datatableUPro_Add);
                //} catch { }
                
                removeFilterAndSortBindingSource(bindingSourcePBH, datatableUPro_Add);
            }
        }

        private void btnAddUPro_Add_Click(object sender, EventArgs e)
        {
            if (dropStatusUPro_Add.SelectedValue.ToString() == "0")
            {
                CustomMessageBox1.show("Bảo hành này chưa được lập");
                return;
            }
             if (lbltinhtrang.Text.ToString() == "0")
            {
                CustomMessageBox1.show("Phiếu bảo hành đã hủy");
                return;
            }
            if (lbltinhtrang.Text.ToString() == "2")
            {
                CustomMessageBox1.show("Phiếu bảo hành đã hết hạn");
                return;
            }
            int mapbh = Int32.Parse(txtIdProUPro_Add.Text.ToString());
            string nguyennhan = rtxtCauseUPro_Add.Text.ToString();
            if (string.IsNullOrEmpty(nguyennhan))
            {
                CustomMessageBox1.show("Chưa nhập nguyên nhân");
                return;
            }
            string manv = IDTK;
            DateTime ngaygiao = DateTime.Parse(datebtnFromDecs.Value.ToString());
            if (ngaygiao <DateTime.Now.Date)
            {
                CustomMessageBox1.show("Ngày giao không nhỏ hơn ngày hiện tại"); ;
                return;
            }
            string sdt = txtPhoneCustomerUpro_Add.Text.ToString();
            if (string.IsNullOrEmpty(sdt) || sdt.Length <10 || sdt.Length>11)
            {
                CustomMessageBox1.show("Số điện thoại không đúng định dạng");
                return;
            }
            Boolean result = false;
            try
            {
                result = BaoHanh_SuaChuaBus.Instance.addBaoHanh(out int mhd, mapbh, nguyennhan, manv, ngaygiao, sdt);
                if (result)
                {
                    string maphieubaohanh = txtIdUPro_Add.text.ToString();
                    DialogResult dialogResult = CustomDialog1.show("Thông báo", "Bạn có muốn xuất hóa đơn", "Có", "Không");
                    if (dialogResult == DialogResult.OK)
                    {
                            MessageBox.Show("ma" + mhd);
                        frmXuatSuaChua frm = new frmXuatSuaChua();
                        frm.mabh = mhd;
                        frm.key = 1;
                        frm.ShowDialog();
                    }
               
                    try
                    {
                        txtIdUPro_Add.text = maphieubaohanh;
                        btnSearchPBHUPro_Add_Click(sender, e);
                    }
                    catch { }
                }
                refesh_Add_Update_UPro();

            }
            catch { }
        }

        private void btnUpdateUPro_Add_Click(object sender, EventArgs e)
        {
            if(datatableUPro_Add.CurrentRow != null)
            {
                txtIdProUPro_Add.Text = datatableUPro_Add.SelectedRows[0].Cells[1].Value.ToString();
                txtidDeviceUPro_Add.Text = datatableUPro_Add.SelectedRows[0].Cells[4].Value.ToString();
                lbltinhtrang.Text = datatableUPro_Add.SelectedRows[0].Cells[10].Value.ToString();
                if(lbltinhtrang.Text.ToString().Equals("0"))
                {
                    txtStaPro_Add.Text = "Đã hủy";

                }
                else if (lbltinhtrang.Text.ToString().Equals("1")) txtStaPro_Add.Text = "Đã giao";
                else txtStaPro_Add.Text = "Đang bảo hành";
                txtSeriDeciUPro_Add.Text = datatableUPro_Add.SelectedRows[0].Cells[5].Value.ToString(); ;
                txtidCustomer_Add.Text = datatableUPro_Add.SelectedRows[0].Cells[7].Value.ToString();
                txtNameCustomerUpro_Add.Text = datatableUPro_Add.SelectedRows[0].Cells[8].Value.ToString();
                txtPhoneCustomerUpro_Add.Text = datatableUPro_Add.SelectedRows[0].Cells[9].Value.ToString();
                rtxtCauseUPro_Add.Text = datatableUPro_Add.SelectedRows[0].Cells[6].Value.ToString();

                dropStatusUPro_Add.SelectedValue = datatableUPro_Add.SelectedRows[0].Cells[15].Value.ToString();
                datatableUPro_Add.Enabled = false;
                if (datatableUPro_Add.SelectedRows[0].Cells[15].Value.ToString() != "2")
                {
                    dropStatusUPro_Add.Enabled = btnSaveUPro_Add.Enabled = false; datatableUPro_Add.Enabled = true;
                     btnAddUPro_Add.Enabled = btnUpdateUPro_Add.Enabled = true;
                    txtIdUPro_Add.Enabled = true; 
                }
                else
                {
                    btnAddUPro_Add.Enabled = false;
                    dropStatusUPro_Add.Enabled = true;
                    btnSaveUPro_Add.Enabled = true;
                    btnUpdateUPro_Add.Enabled = txtIdUPro_Add.Enabled =false; datatableUPro_Add.Enabled = false;
                }
               
                datebtnFromDecs.Value = DateTime.Parse(datatableUPro_Add.SelectedRows[0].Cells[14].Value.ToString());
               
                
            }
            
            
        }

        private void btnSaveUPro_Add_Click(object sender, EventArgs e)
        {
            dropStatusUPro_Add.Enabled= btnSaveUPro_Add.Enabled = false;
            txtIdUPro_Add.Enabled = btnAddUPro_Add.Enabled = btnUpdateUPro_Add.Enabled = true;
            int mabh = Convert.ToInt32(txtIdProUPro_Add.Text.ToString());
            DateTime ngaygiao = datebtnFromDecs.Value;
            for (int i=0;i< datatableUPro_Add.RowCount; i++)
            {
                if(datatableUPro_Add.Rows[0].Cells[1].Value.ToString() == txtIdProUPro_Add.Text.ToString())
                {
                    if(DateTime.Parse(datatableUPro_Add.SelectedRows[0].Cells[14].Value.ToString()) ==ngaygiao)
                    {
                        break;
                    }
                    if(DateTime.Parse(datatableUPro_Add.SelectedRows[0].Cells[14].Value.ToString()) < DateTime.Now.Date)
                    {
                        CustomMessageBox.show("Thông báo", "Ngày phải lớn hơn ngày hiện tại", false);
                        return;
                    }
                }
            }
            string nguyennhan = rtxtCauseUPro_Add.Text.ToString();
            if (string.IsNullOrEmpty(nguyennhan))
            {
                CustomMessageBox1.show("Chưa nhập nguyên nhân lỗi");
                return;
            }
            Byte tinhtrang = Byte.Parse(dropStatusUPro_Add.SelectedValue.ToString());
            if(tinhtrang == 1)
            {
                ngaygiao = DateTime.Now.Date;
                datebtnFromDecs.Value = ngaygiao;
            }
            try
            {

                    BaoHanh_SuaChuaBus.Instance.UpdateBH(mabh, tinhtrang, nguyennhan, ngaygiao);
                    CustomMessageBox.show("Thông báo", "Cập nhật thành công", true);

                //try {
                int maphieu = 0;
                maphieu = Convert.ToInt32(txtIdProUPro_Add.Text.ToString());
                BaoHanh_SuaChuaBus.Instance.LoadDSBH_IDPBH(maphieu, datatableUPro_Add);
                //} catch { }

                removeFilterAndSortBindingSource(bindingSourcePBH, datatableUPro_Add);
                btnAddUPro_Add.Enabled = true;
                datatableUPro_Add.Enabled = true;
                dropStatusUPro_Add.Enabled = true;
            }
            catch
            {
                CustomMessageBox.show("Thông báo", "Cập nhật không thành công", false);
            }

        }

        private void btnNewKH_Add_Click(object sender, EventArgs e)
        {
            if (!cbIDCustomerAdd_Fix.Enabled)
            {  //khach hang cu

                cbIDCustomerAdd_Fix.Enabled = true;
                loadthongtinkhachhang();
                txtNameCustomerAdd_Fix.Enabled = txtPhoneAdd_Fix.Enabled = false;

            }
            else
            { //khach hang moi
                cbIDCustomerAdd_Fix.Enabled = false;
                txtNameCustomerAdd_Fix.Enabled = txtPhoneAdd_Fix.Enabled = true;
            }
        }
        private void loadthongtinkhachhang()
        {
            //!txtNameCustomerSold_Add.Enabled &&
            if (cbIDCustomerAdd_Fix.SelectedItem != null)
            {
                int makh = 0;
                try
                {
                    makh = Convert.ToInt32(cbIDCustomerAdd_Fix.SelectedValue.ToString());
                    HoaDonBanBus.Instance.loadThongTinKHHDB(makh,
                         txtNameCustomerAdd_Fix, txtAdressCustomerFix_Add, txtPhoneAdd_Fix);
                }
                catch
                {
                    txtNameCustomerAdd_Fix.Text= txtAdressCustomerFix_Add.Text= txtPhoneAdd_Fix.Text = string.Empty;
                }

            }
        }

        private void cbIDCustomerAdd_Fix_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadthongtinkhachhang();
        }

        private void txtPriceFIxAddAdd_Fix_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

        }

        private void btnAddAdd_Fix_Click(object sender, EventArgs e)
        {
            if (cbIDDeviceAdd_Fix.Items.Count == 0) return;
            string loi = rtxtErrDecAddAdd_Fix.Text.ToString();
            if (string.IsNullOrEmpty(loi))
            {
                CustomMessageBox1.show("Chưa nhập lỗi của thiết bị");return;
            }
            string mota = rtxtDescipAddAdd_Fix.Text.ToString();
            string matb = cbIDDeviceAdd_Fix.SelectedValue.ToString();
            double chiphi = double.Parse(txtPriceFIxDeAddAdd_Fix.Text.ToString());
            int trangthaitbsc = Int32.Parse(dropstatusAdd_Fix.SelectedValue.ToString());
           
                if (BaoHanh_SuaChuaBus.Instance.AddTBByKeyToHDSC(matb, datatableDeciveAddAdd_Fix,mota,loi, trangthaitbsc, chiphi))
                {
                    tinhtonghd();
                rtxtDescipAddAdd_Fix.Text = rtxtErrDecAddAdd_Fix.Text = string.Empty;

                }
                UpdateSTTGrid(datatableDeciveAddAdd_Fix);
        }
        //tỉnh tổng của một cột trong gridview
        private double sumGridView(DataGridView gv, string nameCol)
        {
            double sum = 0;
            for (int i = 0; i < gv.RowCount; i++)
            {
                sum += double.Parse(gv.Rows[i].Cells[nameCol].Value.ToString());
            }
            return sum;
        }
        public void tinhtonghd()
        {//Column6
            
            double sum = sumGridView(datatableDeciveAddAdd_Fix, "Column6");
            //string.Format("{0:#,##0.00}", double.Parse(txtFreePriceBuy_Add.Text))
            txtPriceFIxAddAdd_Fix.Text = string.Format("{0:#,##0.00}", sum.ToString()); 
        }
        private void btnSaveAddAdd_Fix_Click(object sender, EventArgs e)
        {
            ///in thong tin hoa don sua chua
            if (!btnNewKH_Add.Enabled && datatableDeciveAddAdd_Fix.Rows.Count>0)
            {
                string mahdscin = lblIDSC.Text.ToString();
                if (mahdscin == "0") {  return; }
                if (!string.IsNullOrEmpty(mahdscin))
                {
                    frmXuatSuaChua frm = new frmXuatSuaChua();
                    frm.key = 0;
                    frm.mahd = mahdscin;
                    frm.ShowDialog();
                }
                //in hoa don sua chua
               
                return;
            }
            if (datatableDeciveAddAdd_Fix.RowCount > 0 && cbIDDeviceAdd_Fix.Items.Count > 0)
            {
                int makh = 1;
                if (cbIDCustomerAdd_Fix.Enabled)
                {
                    //khach hang than thiet
                    makh = Int32.Parse(cbIDCustomerAdd_Fix.SelectedValue.ToString());
                }
                string tenkh = txtNameCustomerAdd_Fix.Text.ToString();
                if (string.IsNullOrEmpty(tenkh))
                {
                    CustomMessageBox1.show("Tên khách hàng không được bỏ trống"); return;
                }
                string sdt = txtPhoneAdd_Fix.Text.ToString();
                if (string.IsNullOrEmpty(sdt))
                {
                    CustomMessageBox1.show("Số điện thoại  không được bỏ trống"); return;
                }

                string diachi = txtAdressCustomerFix_Add.Text.ToString();
                Byte tinhtranghdsc = Byte.Parse(cbStatusFix_Add.SelectedValue.ToString());
                double chiphi = double.Parse(txtPriceFIxAddAdd_Fix.Text.ToString());
                DateTime giao = DateTime.Parse(datepicToAdd_Fix.Value.ToString());
                if (DateTime.Compare(giao, DateTime.Now.Date) < 0)
                {
                    CustomMessageBox1.show("Ngày giao không nhỏ hơn ngày hiện tại");
                    return;
                }
                Boolean result = false;
                //kiem tra tinh trang
                if(tinhtranghdsc ==1 || tinhtranghdsc == 2)
                {
                    try
                    {
                        var item = datatableDeciveAddAdd_Fix.Rows.Cast<DataGridViewRow>().First(r => r.Cells["Column7"].Value.ToString().Equals("0"));
                        if (item != null)
                        {
                            CustomMessageBox1.show("Có thiết bị chưa được sửa");
                            return;//da ton tai 
                        }
                    }

                    catch { };
                }
                    
                result = BaoHanh_SuaChuaBus.Instance.addHDSC(out string mhdb,IDTK, makh, tenkh, sdt, giao, chiphi, tinhtranghdsc, datatableDeciveAddAdd_Fix);
                if (!result) return;
                DialogResult dialogResult = CustomDialog1.show("Thông báo", "Bạn có muốn xuất hóa đơn", "Có", "Không");
                if (dialogResult == DialogResult.OK)
                {

                    frmXuatSuaChua frm = new frmXuatSuaChua();
                    frm.mahd = mhdb;
                    frm.key = 0;
                    frm.ShowDialog();
                }
                rtxtDescipAddAdd_Fix.Text = rtxtErrDecAddAdd_Fix.Text = string.Empty;
                txtNumproPro_Add.Text = "0";
                txtPriceFIxDeAddAdd_Fix.Text = "0"; 
            }
            txtPriceFIxDeAddAdd_Fix.Text = "0";
        }

        private void txtPriceFIxDeAddAdd_Fix_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void btnSearchIDSC_Add_Click(object sender, EventArgs e)
        {
            //dong chuc nang them va luu du lieu
            if (txtIDAdd_Fix.text.ToString().Length == 0)
            {
                CustomMessageBox1.show("Chưa nhập mã phiếu sữa chữa");
                return;
            }

            //tim hd sua chua
            cbStatusFix_Add.Visible = true;
            btnSaveUpdateAdd_Fix.Visible = cbStatusFix_Add.Enabled = true;
            gropAdd.Visible = cbIDDeviceAdd_Fix.Enabled = false;
            refesh_Add_Update_Fix();
            BaoHanh_SuaChuaBus.Instance.loadHDSC_ByID(txtIDAdd_Fix.text.ToString(),txtNameCustomerAdd_Fix, txtPhoneAdd_Fix, cbIDCustomerAdd_Fix,
             cbStatusFix_Add, datepicFromAdd_Fix, datepicToAdd_Fix, txtPriceFIxAddAdd_Fix, datatableDeciveAddAdd_Fix);
           
            if(cbStatusFix_Add.SelectedValue.ToString() == "2")
            {
                btnUpdateInfo_Fix.Visible = btnSaveUpdateAdd_Fix.Visible= false;
            }
            else
            {
                btnUpdateInfo_Fix.Visible = true;
            }
            if(datatableDeciveAddAdd_Fix.Rows.Count > 0)
            {
                lblIDSC.Text = txtIDAdd_Fix.text.ToString();
            }
            else { lblIDSC.Text = "0"; }
            lblFlat.Text = "0";
           dropstatusAdd_Fix.Enabled = cbStatusFix_Add.Enabled;
            cbStatusFix_Add.Visible = true;
            btnUpdateInfo_Fix.Enabled = btnSaveUpdateAdd_Fix.Enabled = TaiKhoanBUS.Instance.checkRoleUser(IDTK, Properties.Settings.Default.cnCapNhatSC);

        }

        private void btnDeleteAddAdd_Fix_Click(object sender, EventArgs e)
        {
            if(datatableDeciveAddAdd_Fix.SelectedRows != null)
            {
                try
                {
                    datatableDeciveAddAdd_Fix.Rows.Remove(datatableDeciveAddAdd_Fix.CurrentRow);
                    UpdateSTTGrid(datatableDeciveAddAdd_Fix);
                    tinhtonghd();
                    rtxtDescipAddAdd_Fix.Text = rtxtErrDecAddAdd_Fix.Text = string.Empty;
                }
                catch { }
            }
        }

        private void btnUpdateAddAdd_Fix_Click(object sender, EventArgs e)
        {
            if(datatableDeciveAddAdd_Fix.Rows.Count>0)
            {

                cbIDDeviceAdd_Fix.SelectedValue = datatableDeciveAddAdd_Fix.SelectedRows[0].Cells[2].Value.ToString();
                rtxtDescipAddAdd_Fix.Text = datatableDeciveAddAdd_Fix.SelectedRows[0].Cells[4].Value.ToString();
                rtxtErrDecAddAdd_Fix.Text = datatableDeciveAddAdd_Fix.SelectedRows[0].Cells[5].Value.ToString();
                dropstatusAdd_Fix.SelectedValue= datatableDeciveAddAdd_Fix.SelectedRows[0].Cells["Column7"].Value.ToString();
                txtPriceFIxDeAddAdd_Fix.Text = datatableDeciveAddAdd_Fix.SelectedRows[0].Cells["Column6"].Value.ToString();
                cbIDDeviceAdd_Fix.Enabled=btnDeleteAddAdd_Fix.Enabled =
                btnAddAdd_Fix.Enabled= btnSaveAddAdd_Fix.Enabled= datatableDeciveAddAdd_Fix.Enabled= false;
                btnSaveuUpdateAdd_Fix.Enabled = true;
                btnUpdateAddAdd_Fix.Enabled = false;
            }
        }

        private void txtPriceFIxDeAddAdd_Fix_OnValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtPriceFIxDeAddAdd_Fix.Text == string.Empty)
                {
                    txtPriceFIxDeAddAdd_Fix.Text = "0";
                }
            }
            catch { }
            
        }

        private void btnSaveuUpdateAdd_Fix_Click(object sender, EventArgs e)
        {
            cbIDDeviceAdd_Fix.Enabled=btnDeleteAddAdd_Fix.Enabled = btnAddAdd_Fix.Enabled = btnSaveAddAdd_Fix.Enabled = true;
            //cập nhật cthdsc
            if (datatableDeciveAddAdd_Fix.RowCount > 0 && cbIDDeviceAdd_Fix.Items.Count > 0)
            {
                datatableDeciveAddAdd_Fix.SelectedRows[0].Cells["Column2"].Value= cbIDDeviceAdd_Fix.SelectedValue.ToString();                
               datatableDeciveAddAdd_Fix.SelectedRows[0].Cells["Column4"].Value =rtxtDescipAddAdd_Fix.Text. ToString();
                datatableDeciveAddAdd_Fix.SelectedRows[0].Cells["Column5"].Value = rtxtErrDecAddAdd_Fix.Text.ToString();
                datatableDeciveAddAdd_Fix.SelectedRows[0].Cells["Column7"].Value = dropstatusAdd_Fix.SelectedValue.ToString();
                datatableDeciveAddAdd_Fix.SelectedRows[0].Cells["Column6"].Value= txtPriceFIxDeAddAdd_Fix.Text.ToString();
                btnUpdateAddAdd_Fix.Enabled = btnDeleteAddAdd_Fix.Enabled= btnAddAdd_Fix.Enabled = btnSaveAddAdd_Fix .Enabled= true;
                btnSaveuUpdateAdd_Fix.Enabled = false;
                datatableDeciveAddAdd_Fix.Enabled = true;
                tinhtonghd();
            }
        }

        private void txtIDAdd_Fix_OnTextChange(object sender, EventArgs e)
        {
            try
            {
                txtIDAdd_Fix.text = txtIDAdd_Fix.text.ToString().ToUpper();

            }
            catch { }
        }

        private void datatableDeciveAddAdd_Fix_CurrentCellChanged(object sender, EventArgs e)
        {
            if (datatableDeciveAddAdd_Fix.RowCount > 0 && btnSC_Add_Add.Enabled)
            {
                try
                {

                    lblFlat.Text = datatableDeciveAddAdd_Fix.SelectedRows[0].Cells["Column2"].Value.ToString();
                    cbIDDeviceAdd_Fix.SelectedValue = datatableDeciveAddAdd_Fix.SelectedRows[0].Cells["Column2"].Value.ToString();
                    rtxtDescipAddAdd_Fix.Text = datatableDeciveAddAdd_Fix.SelectedRows[0].Cells["Column4"].Value.ToString();
                    rtxtErrDecAddAdd_Fix.Text = datatableDeciveAddAdd_Fix.SelectedRows[0].Cells["Column5"].Value.ToString();
                    int tinhtrang = 0;
                    if (Boolean.Parse(datatableDeciveAddAdd_Fix.SelectedRows[0].Cells["Column9"].Value.ToString()))
                        tinhtrang = 1;
                    dropstatusAdd_Fix.SelectedValue = tinhtrang;
                    txtPriceFIxDeAddAdd_Fix.Text = datatableDeciveAddAdd_Fix.SelectedRows[0].Cells["Column6"].Value.ToString();
                    btnDeleteAddAdd_Fix.Enabled = btnAddAdd_Fix.Enabled = btnSaveAddAdd_Fix.Enabled = false;
                    if (cbStatusFix_Add.SelectedValue.ToString() == "2")
                    {
                        dropstatusAdd_Fix.Enabled = false;
                    }
                    else dropstatusAdd_Fix.Enabled = true;
                }
                catch { }


            }
            else lblFlat.Text = "0";
        }

        private void btnAddSC_Add_Click(object sender, EventArgs e)
        {
            refesh_Add_Update_Fix();
            //load ds khách hàng lên combobox
            HoaDonBanBus.Instance.loadDSKhachHangHDB(cbIDCustomerAdd_Fix);
            //them hoa don
            datatableDeciveAddAdd_Fix.Columns[1].Visible = false;
            txtIDAdd_Fix.Enabled = false;
            btnAddAdd_Fix.Enabled = btnSaveAddAdd_Fix.Enabled= btnUpdateAddAdd_Fix.Enabled= btnDeleteAddAdd_Fix.Enabled= true;
            btnSC_Add_Add.Enabled= false;
            gropAdd.Visible = cbStatusFix_Add.Visible= true;
            btnSaveUpdateAdd_Fix.Visible = btnUpdateInfo_Fix.Visible =  false;
            txtPhoneAdd_Fix.Enabled = txtNameCustomerAdd_Fix.Enabled = cbStatusFix_Add.Enabled=btnNewKH_Add.Enabled = true;

            //load ds tinh trang phieu sua chua  len combobox
            BaoHanh_SuaChuaBus.Instance.loadTinhTrangHDSCThem(cbStatusFix_Add);
        }

        private void btnInfoSC_Add_Click(object sender, EventArgs e)
        {
            btnSC_Add_Add.Enabled = TaiKhoanBUS.Instance.checkRoleUser(IDTK, Properties.Settings.Default.cnXemSC);

            refesh_Add_Update_Fix();
            //xem thong tin
            datatableDeciveAddAdd_Fix.Columns[1].Visible = cbStatusFix_Add.Visible= true;
            txtIDAdd_Fix.Enabled = btnSC_Add_Add.Enabled=true;
            btnSaveUpdateAdd_Fix.Visible = btnUpdateInfo_Fix.Visible= true;
            txtPhoneAdd_Fix.Enabled=txtNameCustomerAdd_Fix.Enabled= btnNewKH_Add.Enabled = false;
            gropAdd.Visible = false;
            //load ds tinh trang phieu sua chua  len combobox
            BaoHanh_SuaChuaBus.Instance.loadTinhTrangHDSCThong(cbStatusFix_Add);
        }

        private void IOB_Fix_Click(object sender, EventArgs e)
        {

        }

       

        private void btnSaveUpdateAdd_Fix_Click_1(object sender, EventArgs e)
        {

        }

        private void cbStatusFix_Add_DropDown(object sender, EventArgs e)
        {
           
            
        }

        private void btnSaveUpdateAdd_Fix_Click(object sender, EventArgs e)
        {
            if (datatableDeciveAddAdd_Fix.RowCount > 0 && cbIDDeviceAdd_Fix.Items.Count > 0 && lblFlat.Text!="0")
            {

                Boolean result = false;

                int sophieu = Convert.ToInt32(datatableDeciveAddAdd_Fix.SelectedRows[0].Cells["Column8"].Value.ToString());
                string matb = datatableDeciveAddAdd_Fix.SelectedRows[0].Cells["Column2"].Value.ToString();
                string mota = rtxtDescipAddAdd_Fix.Text.ToString();
                string loi = rtxtErrDecAddAdd_Fix.Text.ToString();
                bool trangthaitb = (dropstatusAdd_Fix.SelectedValue.ToString() == "0") ? false : true;
                double chiphi = double.Parse(txtPriceFIxDeAddAdd_Fix.Text);
                result = BaoHanh_SuaChuaBus.Instance.updateTBSC( sophieu,mota, loi, chiphi, trangthaitb);
                if (!result) return;
                rtxtDescipAddAdd_Fix.Text = rtxtErrDecAddAdd_Fix.Text = string.Empty;
                txtPriceFIxDeAddAdd_Fix.Text = "0";
                BaoHanh_SuaChuaBus.Instance.loadHDSC_ByID(txtIDAdd_Fix.text.ToString(), txtNameCustomerAdd_Fix, txtPhoneAdd_Fix, cbIDCustomerAdd_Fix,
                  cbStatusFix_Add, datepicFromAdd_Fix, datepicToAdd_Fix, txtPriceFIxAddAdd_Fix, datatableDeciveAddAdd_Fix);
                Byte tinhtranghdsc = Byte.Parse(cbStatusFix_Add.SelectedValue.ToString());
                if (tinhtranghdsc ==2)
                {
                    btnSaveUpdateAdd_Fix.Visible = false;
                    
                }
                else
                {
                    btnSaveUpdateAdd_Fix.Visible = true;
                }
                try
                {
                    for (int i=0;i< datatableDeciveAddAdd_Fix.RowCount; i++)
                {
                    if(!Boolean.Parse(datatableDeciveAddAdd_Fix.Rows[i].Cells["Column9"].Value.ToString()))
                    {
                        btnUpdateInfo_Fix.Visible = false;
                        btnSaveUpdateAdd_Fix.Visible = true;
                        break;
                    }            
                }

                }
                catch { };
            }
            else
            {
                CustomMessageBox1.show("Chưa có thiết bị để cập nhât");
            }

        }

        private void btnUpdateInfo_Fix_Click(object sender, EventArgs e)
        {
            string tenkh = txtNameCustomerAdd_Fix.Text.ToString();
            if (string.IsNullOrEmpty(tenkh))
            {
                CustomMessageBox1.show("Tên khách hàng không được bỏ trống"); return;
            }
            string sdt = txtPhoneAdd_Fix.Text.ToString();
            if (string.IsNullOrEmpty(sdt))
            {
                CustomMessageBox1.show("Số điện thoại  không được bỏ trống"); return;
            }
            tinhtonghd();
            double tongchiphi = double.Parse(txtPriceFIxAddAdd_Fix.Text.ToString());
            DateTime nhan = DateTime.Parse(datepicFromAdd_Fix.Value.ToString());
            DateTime giao = DateTime.Parse(datepicToAdd_Fix.Value.ToString()).Date;
            if (DateTime.Compare(giao, DateTime.Now.Date) < 0)
            {
                CustomMessageBox1.show("Ngày giao không nhỏ hơn ngày hiện tại");
                return;
            }
            Boolean result = false;
            Byte tinhtranghdsc = Byte.Parse(cbStatusFix_Add.SelectedValue.ToString());
            string masc = datatableDeciveAddAdd_Fix.SelectedRows[0].Cells["Column10"].Value.ToString();
            result = BaoHanh_SuaChuaBus.Instance.updateSC(masc,tenkh, sdt, giao, tongchiphi, tinhtranghdsc);
            BaoHanh_SuaChuaBus.Instance.loadHDSC_ByID(masc, txtNameCustomerAdd_Fix, txtPhoneAdd_Fix, cbIDCustomerAdd_Fix,
                 cbStatusFix_Add, datepicFromAdd_Fix, datepicToAdd_Fix, txtPriceFIxAddAdd_Fix, datatableDeciveAddAdd_Fix);
             tinhtranghdsc = Byte.Parse(cbStatusFix_Add.SelectedValue.ToString());
            if (tinhtranghdsc == 2)
            {
                btnSaveUpdateAdd_Fix.Visible = btnUpdateInfo_Fix.Visible= false;
            }else
            {
                btnSaveUpdateAdd_Fix.Visible = btnUpdateInfo_Fix.Visible = true;
            }



        }

        private void tabPageHome_Click(object sender, EventArgs e)
        {

        }

        private void btnXemCTSC_Fixs_Click(object sender, EventArgs e)
        {
            lblIDSC_FIXs.Text = "";
            if (datatableFixs_Fixs.RowCount > 0 && datatableFixs_Fixs.SelectedRows != null)
            {
                lblIDSC_FIXs.Text= datatableFixs_Fixs.SelectedRows[0].Cells[1].Value.ToString();

                BaoHanh_SuaChuaBus.Instance.LoadDSCTSC(lblIDSC_FIXs.Text.ToString(), gvCTSC_ByID_Fixs);
            }

        }

        private void frmProFix_Load(object sender, EventArgs e)
        {
            block_menu();
            BaoHanh_SuaChuaBus.Instance.update_PBH();
        }

        private void btnPrinUPro_Add_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(txtIdProUPro_Add.Text.ToString()))
            {
                CustomMessageBox1.show("Chưa chọn hóa đơn bảo hành để in");return;
            }
            int mabh = Convert.ToInt32(txtIdProUPro_Add.Text.ToString());

            frmXuatSuaChua frm = new frmXuatSuaChua();
            frm.mabh = mabh;
            frm.key = 1;
            frm.ShowDialog();
        }

        private void btnPriPro_Add_Click(object sender, EventArgs e)
        {
            string seri = "";
            if (datatableDecivePro_Add.SelectedRows == null) 
            {
                CustomMessageBox1.show("Chưa chọn phiếu bảo hành để in"); return;
            }

            frmXuatSuaChua frm = new frmXuatSuaChua();
            frm.mahd = datatableDecivePro_Add.SelectedRows[0].Cells[6].Value.ToString();
            frm.key = 2;
            frm.ShowDialog();
        }

        private void datatableDecivePro_Add_CurrentCellChanged(object sender, EventArgs e)
        {

        }
    }
}
