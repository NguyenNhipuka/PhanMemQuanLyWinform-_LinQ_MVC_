using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using BUS;
using System.Windows.Forms;
using Bunifu.Framework.UI;
using MetroFramework.Controls;
using MetroFramework.Forms;

namespace PMQLCHDIENTHOAI
{
    public partial class frmHome : Form
    {
        public delegate void sendMessage(string message);
        public event sendMessage sendMessageEvent;

        ///Mã màn hình
        int key_MenuAddAccount = Properties.Settings.Default.cnThemTaiKhoan;//key thêm tài khoản
        int key_MenuRole = Properties.Settings.Default.cnPhanQuyen;//key phân quyền
        int key_MenuRole_ad = Properties.Settings.Default.cnThemSuaXoaNhomQuyen;// key nhóm quyền
        int key_MenuResetPass = Properties.Settings.Default.cnReset;//key reset pass
        int key_MenuAccounts = Properties.Settings.Default.cnXemDSTaiKhoan;//key xem ds tài khoản
        int key_MenuAccountDetail = Properties.Settings.Default.cnXemThongTinTaiKhoan;//xem thông tin chi tiết tài khoản

        ///Mã chức năng
        int key_action_UpdateTaiKhoan = Properties.Settings.Default.cnCapNhatTK;///key cập nhật thông tin nhóm người dùng tài khoản và trạng thái
        int key_action_DeleteTaiKhoan = Properties.Settings.Default.cnXoaTaiKhoan;///key xóa tài khoản

        public string IdNV = "";
        public int tabindex = 1;
        private int flat = 0;
        public void block_Menu()
        {
            btnMenuAccounts.Enabled = TaiKhoanBUS.Instance.checkRoleUser(IdNV, key_MenuAccounts);
            btnMenuAddAccount.Enabled = TaiKhoanBUS.Instance.checkRoleUser(IdNV, key_MenuAddAccount);
            btnMenuDetailAccount.Enabled = TaiKhoanBUS.Instance.checkRoleUser(IdNV, key_MenuAccountDetail);
            btnMenuResetPass.Enabled = TaiKhoanBUS.Instance.checkRoleUser(IdNV, key_MenuResetPass);
            btnMenuRole.Enabled = TaiKhoanBUS.Instance.checkRoleUser(IdNV, key_MenuRole);
            
        }
        public frmHome()
        {
            InitializeComponent();
            block_Menu();        
            openTabPage(tabindex);
            
        }
        public void openTabPage(int tab)
        {
            tabHome.TabPages.Clear();
            switch (tab)
            {
                case 1://tab home
                    tabHome.TabPages.Add(tabPageHome);
                    break;
                case 2://tab add account
                    tabHome.TabPages.Add(tabPageAddAccount);
                    break;
                case 3://tab Role
                    tabHome.TabPages.Add(tabPageRole);
                    break;
                case 4://tab reset
                    tabHome.TabPages.Add(tabPageReset);
                    break;
                case 5://tab Accounts

                    tabHome.TabPages.Add(tabPageAccounts);
                    break;
                case 6://tab info account
                    tabHome.TabPages.Add(tabPageInfoAccount);
                    break;
                case 7://tab change pass
                    tabHome.TabPages.Add(tabPageChangePass);
                    break;
                default://khong co quyen
                    break;
            }
        }

        #region ++ open tab
        private void btnMenuAddAccount_Click(object sender, EventArgs e)
        {
            
            openTabPage(2);
            txtNameAccount_Add.Text = txtMk.Text = "";
            TaiKhoanBUS.Instance.LoadNhomNDCHECK(gvNhomND);
            TaiKhoanBUS.Instance.LoadNV_ChuaTK(cbMaNV);
            try
            {
                cbMaNV.SelectedIndex = 0;
                txtNameAccount_Add.Text = TaiKhoanBUS.Instance.getTenNVbyID(cbMaNV.SelectedValue.ToString());
            }
            catch { }
            if (cbMaNV.Items.Count == 0) { btnAddAccount_add.Enabled = false; } else btnAddAccount_add.Enabled = true;
        }

        private void btnMenuRole_Click(object sender, EventArgs e)
        {
            TaiKhoanBUS.Instance.LoadNhomND(dataRoles_Ro);
            openTabPage(3);
            btnSaveRole_Ro.Enabled = btnCancelRole_Ro.Enabled = false;
            
        }

        private void btnMenuResetPass_Click(object sender, EventArgs e)
        {
            
            openTabPage(4);
            TaiKhoanBUS.Instance.LoadDSTaiKhoan(dataTableReset_Re);
            if(dataTableReset_Re.Rows.Count >0)
            {
                txtMaTk_re.Text = dataTableReset_Re.Rows[0].Cells[1].Value.ToString();
            }
        }

        private void btnMenuAccounts_Click(object sender, EventArgs e)
        {
            openTabPage(5);
            TaiKhoanBUS.Instance.LoadDSTaiKhoan(datatableAccount_Ac);
        }

        private void btnMenuDetailAccount_Click(object sender, EventArgs e)
        {
            openTabPage(6);
            for (int i = 0; i < pnlbtn_Inf.Controls.Count; i++)
            {
                if (pnlbtn_Inf.Controls[i].GetType() == typeof(BunifuFlatButton))
                {
                    pnlbtn_Inf.Controls[i].Enabled = false;
                }
            }

            txtsearchAccount_Inf.text = txtMaTK_Inf.Text= txtName_Inf.Text = txtCreated_Inf.Text=null;
            // load tất cả
            TaiKhoanBUS.Instance.LoadNhomNDByMaNV(dataRoles_Inf,"");
        }
        private void btnMenu_Click(object sender, EventArgs e)
        {
            openTabPage(1);
        }
        private void btnChangePass_Click(object sender, EventArgs e)
        {
            openTabPage(7);
            txtoldPass.Text = txtNewPass.Text = txtConfirmNewPass.Text = "";
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            string s = IdNV;
            IdNV = null;
            frmLogin frmlogin = new frmLogin();
            frmlogin.sendMessageEvent += fr_sendMessageEventLogin;
            frmlogin.ShowDialog();
            
            if (IdNV == null) { IdNV = s; }
            if (sendMessageEvent != null)
            {
                sendMessageEvent(IdNV);
                CustomAlert.Show("Chào mừng tài khoản " + IdNV);
            }
           
           
            block_Menu();

        }
        #endregion open tab

        #region++ event click back

        private void btnInfoBack_Click(object sender, EventArgs e)
        {
            openTabPage(1);
        }

        private void btnAddBck_Click(object sender, EventArgs e)
        {
            openTabPage(1);
        }

        private void btnResetBack_Click(object sender, EventArgs e)
        {
            openTabPage(1);
        }

        private void btnRoleBack_Click(object sender, EventArgs e)
        {
            openTabPage(1);
        }

        private void btnAccountsBack_Click(object sender, EventArgs e)
        {
            openTabPage(1);
        }

        private void btnInfoBack_Click_1(object sender, EventArgs e)
        {
            openTabPage(1);
        }
        #endregion event click back

        private void frmHome_Load(object sender, EventArgs e)
        {
            this.BringToFront();
        }

        private void btnChangePassBack_Click(object sender, EventArgs e)
        {
            openTabPage(1);
        }

        private void btnRefesh_ChangePass_Click(object sender, EventArgs e)
        {
            txtoldPass.Text = txtNewPass.Text = txtConfirmNewPass.Text = "";
        }


        //nhận tài khoản đăng nhập
        public void fr_sendMessageEventLogin(string messaege)
        {
            IdNV = messaege;
        }

        private void btnAddAccount_add_Click(object sender, EventArgs e)
        {
            string manv = cbMaNV.SelectedValue.ToString();
            string tentk = txtNameAccount_Add.Text.ToString();
            string mk = txtMk.Text.ToString();
            Boolean tt = swActive_add.Value;
            if(mk.Length < 8)
            {
                CustomMessageBox1.show("Mật khẩu lớn hơn bằng 8 ký tự");
                return;
            }
            
            List<int> dsNhomQuyen = new List<int>();
            foreach (DataGridViewRow r in gvNhomND.Rows)
            {

                DataGridViewCheckBoxCell cell = r.Cells[0] as DataGridViewCheckBoxCell;
                if (Convert.ToBoolean(cell.Value) == true)
                {
                    dsNhomQuyen.Add(Convert.ToInt32(r.Cells[1].Value));
                }

            }

            TaiKhoanBUS.Instance.InsertUser(manv, tentk, mk, tt, dsNhomQuyen);
            TaiKhoanBUS.Instance.LoadNhomNDCHECK(gvNhomND);
            TaiKhoanBUS.Instance.LoadNV_ChuaTK(cbMaNV);
            try
            {
                cbMaNV.SelectedIndex = 0;
                txtNameAccount_Add.Text = TaiKhoanBUS.Instance.getTenNVbyID(cbMaNV.SelectedValue.ToString());
            }
            catch { }
            if (cbMaNV.Items.Count == 0) { btnAddAccount_add.Enabled = false; } else btnAddAccount_add.Enabled = true;


        }

        private void btnReset_add_Click(object sender, EventArgs e)
        {
           txtNameAccount_Add.Text = txtMk.Text = String.Empty;
        }

        private void btnCancelCreateAccount_add_Click(object sender, EventArgs e)
        {
            string x = gvNhomND.CurrentRow.Cells[0].Value.ToString() + gvNhomND.CurrentRow.Cells[1].Value.ToString();
        }

       

        private void gvNhomND_MouseClick(object sender, MouseEventArgs e)
        {
            if((bool)(gvNhomND.SelectedRows[0].Cells[0].Value) == true)
            {
                gvNhomND.SelectedRows[0].Cells[0].Value = false;
            }
            else
            {
                gvNhomND.SelectedRows[0].Cells[0].Value = true;
            }
        }

        private void dataRoles_Ro_CurrentCellChanged(object sender, EventArgs e)
        {
            try
            {
                if (dataRoles_Ro.SelectedRows != null)
                {
                    int id = Convert.ToInt32(dataRoles_Ro.CurrentRow.Cells[0].Value);
                    TaiKhoanBUS.Instance.LoadPHANQUYEN(dataFunction_Ro, id);
                }
            }
            catch { }
            
        }

        private void btnDeleteRole_Ro_Click(object sender, EventArgs e)
        {
            if (dataRoles_Ro.SelectedRows != null)
            {

                if(Convert.ToInt32(dataRoles_Ro.SelectedRows[0].Cells[0].Value.ToString()) == 2)
                {
                    CustomMessageBox1.show("Không thể xóa nhóm quyền này");return;
                }
                DialogResult dialog = CustomDialog1.show("Xác nhận", "Xóa và thu hồi nhóm quyền nay", "Có", "Không");
                if(dialog== DialogResult.No)
                {
                    return;
                }
                TaiKhoanBUS.Instance.deleteNhomNguoiDung(IdNV,Convert.ToInt32(dataRoles_Ro.SelectedRows[0].Cells[0].Value.ToString()));
                ///kiem tra lai quyen "Phan quyen" sau khi xoa
                if ( !TaiKhoanBUS.Instance.checkRoleUser(IdNV, key_MenuRole))
                {
                    ////---khong co quyen doi vois chuc nang phan quyen
                    openTabPage(1);
                    block_Menu();
                    return;
                }
                TaiKhoanBUS.Instance.LoadNhomND(dataRoles_Ro);
            }
            
        }

        private void dataFunction_Ro_MouseClick(object sender, MouseEventArgs e)
        {
            if ((bool)(dataFunction_Ro.SelectedRows[0].Cells[0].Value) == true)
            {
                dataFunction_Ro.SelectedRows[0].Cells[0].Value = false;
            }
            else
            {
                dataFunction_Ro.SelectedRows[0].Cells[0].Value = true;
            }
        }

        private void btnAddRole_Ro_Click(object sender, EventArgs e)
        {
            string tennhom = txtTenNhom_Ro.Text.ToString();
            string ghichu = txtGhiChu_Ro.Text.ToString();
            if(tennhom == null || ghichu==null)
            {
                MetroFramework.MetroMessageBox.Show(this, "Vui lòng điền đầy đủ thông tin", "Thông báo");
                return;
            }
            TaiKhoanBUS.Instance.insertNhomND(tennhom, ghichu);
            TaiKhoanBUS.Instance.LoadNhomND(dataRoles_Ro);
            txtGhiChu_Ro.Text = txtTenNhom_Ro.Text = "";
        }

        private void btnUpdateRole_Ro_Click(object sender, EventArgs e)
        {
            
            if(dataRoles_Ro.SelectedRows!= null)
            {
                txtTenNhom_Ro.Text = dataRoles_Ro.SelectedRows[0].Cells[1].Value.ToString();
                txtGhiChu_Ro.Text = dataRoles_Ro.SelectedRows[0].Cells[2].Value.ToString();
                btnSaveRole_Ro.Enabled=btnCancelRole_Ro.Enabled = true;
                btnAddRole_Ro.Enabled=btnDeleteRole_Ro.Enabled =dataRoles_Ro.Enabled=btnUpdateRole_Ro.Enabled= false;
            }
            
        }

        private void btnSaveRole_Ro_Click(object sender, EventArgs e)
        {
            string tennhom = txtTenNhom_Ro.Text.ToString();
            string ghichu = txtGhiChu_Ro.Text.ToString();
            if (dataRoles_Ro.CurrentRow == null) return;
            int manhom = Convert.ToInt32(dataRoles_Ro.SelectedRows[0].Cells[0].Value.ToString());
            if (tennhom == null || ghichu == null)
            {
                MetroFramework.MetroMessageBox.Show(this, "Vui lòng điền đầy đủ thông tin", "Thông báo");
                return;
            }
            List<int> dsChucNang = new List<int>();
            List<Boolean> dsCheck = new List<bool>();
            foreach (DataGridViewRow r in dataFunction_Ro.Rows)
            {

                DataGridViewCheckBoxCell cell = r.Cells[0] as DataGridViewCheckBoxCell;
                Boolean check = Convert.ToBoolean(cell.Value);
                dsCheck.Add(check);
                dsChucNang.Add(Convert.ToInt32(r.Cells[1].Value));
            }
            if(dsChucNang.Count >0 && dsCheck.Count > 0)
            {
                TaiKhoanBUS.Instance.updateNhomNguoiDung(manhom, tennhom, ghichu, dsChucNang, dsCheck);
                CustomAlert.Show("Vui lòng đợi!");
                TaiKhoanBUS.Instance.LoadNhomND(dataRoles_Ro);
            }
            btnSaveRole_Ro.Enabled=btnCancelRole_Ro.Enabled = false;
            btnUpdateRole_Ro.Enabled= btnAddRole_Ro.Enabled = btnDeleteRole_Ro.Enabled=dataRoles_Ro.Enabled = true;
            txtGhiChu_Ro.Text = txtTenNhom_Ro.Text = "";
        }

        private void btnCancelRole_Ro_Click(object sender, EventArgs e)
        {
            btnSaveRole_Ro.Enabled = btnCancelRole_Ro.Enabled = false;
            btnAddRole_Ro.Enabled = btnDeleteRole_Ro.Enabled = dataRoles_Ro.Enabled =btnUpdateRole_Ro.Enabled= true;
            txtGhiChu_Ro.Text = txtTenNhom_Ro.Text = "";
            dataRoles_Ro_CurrentCellChanged(sender, e);

        }


        private void txtMaTK_Inf_OnValueChanged(object sender, EventArgs e)
        {
            string manv = txtMaTK_Inf.Text.ToString();
            if (manv.Length <= 0)
            {
                for (int i = 0; i < pnlbtn_Inf.Controls.Count; i++)
                {
                    if (pnlbtn_Inf.Controls[i].GetType() == typeof(BunifuFlatButton))
                    {
                        pnlbtn_Inf.Controls[i].Enabled = false;
                    }
                }
                return;
            }
            else
            {
                //mở chức năng xóa, update nếu người dùng có quyền
                btnUpdateRole_Inf.Enabled= TaiKhoanBUS.Instance.checkRoleUser(IdNV, key_action_UpdateTaiKhoan);

                btnDelateRole_Inf.Enabled = TaiKhoanBUS.Instance.checkRoleUser(IdNV, key_action_DeleteTaiKhoan);
            }               
            // load tất cả
            TaiKhoanBUS.Instance.LoadNhomNDByMaNV(dataRoles_Inf, manv);
        }

        private void dataRoles_Inf_MouseClick(object sender, MouseEventArgs e)
        {
            //đã bậc chức năng update
           
            if (!btnUpdateRole_Inf.Enabled && dataRoles_Inf.Rows.Count>0)
            {
                if(dataRoles_Inf.SelectedRows[0].Cells[1].Value.ToString() == "1")
                {
                    return;
                }
                if ((bool)(dataRoles_Inf.SelectedRows[0].Cells[0].Value) == true)
                {
                    dataRoles_Inf.SelectedRows[0].Cells[0].Value = false;
                }
                else
                {
                    dataRoles_Inf.SelectedRows[0].Cells[0].Value = true;
                }
            }
            
        }

        /// xóa tài khoản
        private void btnDelateRole_Inf_Click(object sender, EventArgs e)
        {
            string manvdelete = txtMaTK_Inf.Text.ToString();
            Boolean result = false;
            if (txtMaTK_Inf.Text.ToString() == string.Empty) return;
            DialogResult dialogResult = DialogResult.No;
            dialogResult=CustomDialog1.show("Xác nhận","Bạn có muốn xóa tài khoản : " + txtMaTK_Inf.Text.ToString(),"Có","Không");
            if (dialogResult == DialogResult.No) return;
            if (txtMaTK_Inf.Text == IdNV) { CustomMessageBox1.show("Không thể xóa"); return; }
            if (IdNV.Length>0 && manvdelete.Length>0)
            {
                if(IdNV == manvdelete)
                {
                    CustomMessageBox1.show("Không thể xóa");return;
                }
                result= TaiKhoanBUS.Instance.deleteUser(IdNV, manvdelete);
            }
            if (result)
            {
                txtMaTK_Inf.Text = txtName_Inf.Text = txtCreated_Inf.Text = string.Empty;               
            }
            return;
        }
        //mở tính năng update tên tài khoản và update nhóm quyền cho tài khoản
        private void btnUpdateRole_Inf_Click(object sender, EventArgs e)
        {
            if (txtMaTK_Inf.Text.ToString() ==string.Empty) return;
            if (txtMaTK_Inf.Text == IdNV) { CustomMessageBox1.show("Không thể cập nhật"); return;}
            btnCancelRole_Inf.Enabled = btnSaveRole_Inf.Enabled= swStatus_Inf.Enabled = true;
            btnUpdateRole_Inf.Enabled = btnDelateRole_Inf.Enabled= false;
        }
        //save thay đổi tên tài khoản và update nhóm quyền cho tài khoản
        private void btnSaveRole_Inf_Click(object sender, EventArgs e)
        {
            string manv = txtMaTK_Inf.Text.ToString();
            if (manv.Length <= 0) return;
            Boolean trangthai = swStatus_Inf.Value;
            List<int> dsNhomQuyen = new List<int>();
            List<Boolean> dsCheck = new List<bool>();
            foreach (DataGridViewRow r in dataRoles_Inf.Rows)
            {
                DataGridViewCheckBoxCell cell = r.Cells[0] as DataGridViewCheckBoxCell;
                Boolean check = Convert.ToBoolean(cell.Value);
                dsCheck.Add(check);
                dsNhomQuyen.Add(Convert.ToInt32(r.Cells[1].Value));               
            }
            if (dsNhomQuyen.Count > 0 && dsCheck.Count > 0)
            {
                TaiKhoanBUS.Instance.updateNhomChoUser(manv, trangthai, dsNhomQuyen, dsCheck);
            }
            btnCancelRole_Inf.Enabled = btnSaveRole_Inf.Enabled= swStatus_Inf.Enabled = false;
            btnUpdateRole_Inf.Enabled = btnDelateRole_Inf.Enabled = true;
            
        }
        //hủy thao tác cập nhật tên tài khoản và update nhóm quyền cho tài khoản
        private void btnCancelRole_Inf_Click(object sender, EventArgs e)
        {
            //mở chức năng xóa, update
            btnUpdateRole_Inf.Enabled = btnDelateRole_Inf.Enabled = true;
            string manv = txtMaTK_Inf.Text.ToString();
            // load tất cả
            TaiKhoanBUS.Instance.LoadNhomNDByMaNV(dataRoles_Inf,manv );
        }

        private void btnSearchAccount_Inf_Click(object sender, EventArgs e)
        {
            string key = txtsearchAccount_Inf.text.ToString();
            
            if (key.Length > 0)
            { 

                TaiKhoanBUS.Instance.searchUser(key, txtMaTK_Inf, txtName_Inf, txtCreated_Inf, swStatus_Inf);
            }
        }

        ///Mở chức năng reset pass
        private void btnReset_Re_Click(object sender, EventArgs e)
        {
            txtpassreset_re.Enabled = txtpassresetCo_re.Enabled = btnSave_Re.Enabled= btnCancel_Re.Enabled= true;
            btnReset_Re.Enabled = dataTableReset_Re.Enabled= false;
        }
        ///hủy thao tác reset pass
        private void btnCancel_Re_Click(object sender, EventArgs e)
        {
            txtpassreset_re.Enabled = txtpassresetCo_re.Enabled = btnSave_Re.Enabled = btnCancel_Re.Enabled = false;
            btnReset_Re.Enabled =dataTableReset_Re.Enabled= true;
            txtpassresetCo_re.Text = txtpassreset_re.Text = null;
        }
        ///Luu thông tin reset pass theo mã nhân viên
        private void btnSave_Re_Click(object sender, EventArgs e)
        {
            string matk = txtMaTk_re.Text.ToString();
            string mk1 = txtpassreset_re.Text.ToString();
            string mk2 = txtpassresetCo_re.Text.ToString();
            if (!mk1.Equals(mk2))
            {
                CustomMessageBox1.show("Mật khẩu không trùng khớp");
                return;
            }
            if(mk1.Length < 8)
            {
                CustomMessageBox1.show("Mật khẩu phải lớn hơn bằng 8 ký tự");
                return;
            }
            //update
            if(!TaiKhoanBUS.Instance.resetPass(matk, mk1))
            {
                return;
            }
            //load lại trang
            TaiKhoanBUS.Instance.LoadDSTaiKhoan(dataTableReset_Re);
            if (dataTableReset_Re.Rows.Count == 0)
            {
                return;
            }
           
            txtMaTk_re.Text = dataTableReset_Re.Rows[0].Cells[1].Value.ToString();
            //gán default
            txtpassreset_re.Enabled = txtpassresetCo_re.Enabled = btnSave_Re.Enabled = btnCancel_Re.Enabled = false;
            btnReset_Re.Enabled = dataTableReset_Re.Enabled= true;
            txtpassresetCo_re.Text = txtpassreset_re.Text = null;
        }

        private void dataTableReset_Re_MouseClick(object sender, MouseEventArgs e)
        {
            //đã bậc chức năng update
            if (dataTableReset_Re.Rows.Count>0)
            {
                txtMaTk_re.Text = dataTableReset_Re.SelectedRows[0].Cells[1].Value.ToString();
            }
        }
        private void UpdateGrid(DataGridView gridView)
        {
            for (int i = 0; i < gridView.RowCount; i++)
            {
                gridView.Rows[i].Cells[0].Value = i + 1;

            }
        }

        private void gvNhomND_Sorted(object sender, EventArgs e)
        {
            UpdateGrid(gvNhomND);
        }

        private void dataRoles_Ro_Sorted(object sender, EventArgs e)
        {
            UpdateGrid(dataRoles_Ro);
        }

        private void dataTableReset_Re_Sorted(object sender, EventArgs e)
        {
            UpdateGrid(dataTableReset_Re);
        }

        private void datatableAccount_Ac_Sorted(object sender, EventArgs e)
        {
            UpdateGrid(datatableAccount_Ac);
        }

        private void btnUpdatePass_Click(object sender, EventArgs e)
        {

            string mkcu = txtoldPass.Text;
            string mkmoi1 = txtNewPass.Text;
            string mkmoi2 = txtConfirmNewPass.Text;
            if(mkcu.Length <=0 || mkmoi1.Length<=0 || mkmoi2.Length <= 0)
            {
                CustomMessageBox1.show("Vui lòng điền đầy đủ thông tin");
                return;
            }
            if (!mkmoi1.Equals(mkmoi2))
            {
                CustomMessageBox1.show("Mật khẩu không trùng khớp");
                return;
            }
            if(TaiKhoanBUS.Instance.UpdatePass(IdNV,mkcu, mkmoi1))
            {
                CustomMessageBox1.show("Vui lòng đăng nhập lại");
                txtoldPass.Text = txtNewPass.Text = txtConfirmNewPass.Text = "";
            }

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

   

        private void rdcheckAll_CheckedChanged(object sender, EventArgs e)
        {
            if (dataFunction_Ro.Rows.Count > 0)
            {
                foreach (DataGridViewRow r in dataFunction_Ro.Rows)
                {

                    DataGridViewCheckBoxCell cell = r.Cells[0] as DataGridViewCheckBoxCell;
                    cell.Value = Convert.ToBoolean(true);

                }
            }
        }

        private void rdNonCheck_CheckedChanged(object sender, EventArgs e)
        {
            if (dataFunction_Ro.Rows.Count > 0)
            {
                foreach (DataGridViewRow r in dataFunction_Ro.Rows)
                {

                    DataGridViewCheckBoxCell cell = r.Cells[0] as DataGridViewCheckBoxCell;
                    cell.Value = Convert.ToBoolean(false);

                }
            }
        }

        private void cbMaNV_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                txtNameAccount_Add.Text = TaiKhoanBUS.Instance.getTenNVbyID(cbMaNV.SelectedValue.ToString());
            }
            catch { txtNameAccount_Add.Text = string.Empty; }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
