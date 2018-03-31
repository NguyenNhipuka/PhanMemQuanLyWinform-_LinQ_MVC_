using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DTO;
using DAO.Implementation;
using PMQLCHDIENTHOAI.Utilities;
using Bunifu.Framework.UI;
using MetroFramework.Forms;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;
using PMQLCHDIENTHOAI;

namespace BUS
{
    public class TaiKhoanBUS
    {
        private static TaiKhoanBUS instance;

        public static TaiKhoanBUS Instance
        {
            get
            {
                if (instance == null)
                    instance = new TaiKhoanBUS();
                return instance;
            }
            private set
            {
                TaiKhoanBUS.instance = value;
            }
        }

        public TAIKHOAN get_user(string manv,string pass)
        {
            //var taikhoandao = new TaiKhoanDAO();
            //var taikhoan = taikhoandao.Login(manv, Common.HashMd5(pass));
            //var check_trangthai = taikhoandao.CheckLogin(manv, Common.HashMd5(pass));
            //if (taikhoan != null)
            //{
            //    return taikhoan;
            //}
            return null;
        }

        //kiểm tra đăng nhập
        public Boolean check_user(string manv, string pass, out int ErrCo, out string ErrMess)
        {
            var taikhoandao = new TaiKhoanDAO();
            ErrCo = 0;
            ErrMess = "Đăng nhập thành công";
            var taikhoan = taikhoandao.Login(manv, Common.HashMd5(pass), out ErrCo, out ErrMess);
            
            if (taikhoan != null && ErrCo == 0)
            {
                return true;
            }
            CustomMessageBox.show("Kết quả", ErrMess, ErrCo == 0);
            return false;
        }

        public Boolean checkRoleUser(string manv,int idQuyen)
        {
            List<int> roles = new List<int>();
            bool results = false;
            roles = TaiKhoanDAO.Instance.getAllAccountRole(manv);
            try
            {
                results = roles.Contains(idQuyen);
               // IEnumerable<int> results = roles.Where(s => s == idQuyen);
            }
            catch {
            }

            return results;
        }
        
        //lấy nhóm người dùng cho chức năng phân quyền
        public Boolean LoadNhomNDCHECK(DataGridView gv)
        {
            DataTable table = TaiKhoanDAO.Instance.getAllNhomND();
            if (table == null) return false;
            DataColumn col = new DataColumn();
            col.ColumnName = "CHECK";
            col.DataType = typeof(Boolean);
            col.DefaultValue = false;           
            table.Columns.Add(col);
            table.Columns[3].SetOrdinal(0);
            gv.DataSource = table;
            gv.Columns[1].HeaderText = "Mã nhóm";
            gv.Columns[2].HeaderText = "Tên nhóm";
            gv.Columns[0].HeaderText = "Chọn";
            gv.Columns[3].HeaderText = "Ghi chú";
           
            return true;
        }

        public Boolean LoadNhomND(DataGridView gv)
        {
            DataTable table = TaiKhoanDAO.Instance.getAllNhomND();
            if (table == null) return false;
            DataColumn col = new DataColumn();
            gv.DataSource = table;
           
            return true;
        }

        public Boolean LoadNV_ChuaTK(ComboBox cb)
        {
            DataTable table = TaiKhoanDAO.Instance.getDSNhanVienChuaTK();
            if (table == null) return false;
            cb.DataSource = table;
            cb.DisplayMember = "MANV";
            cb.ValueMember = "MANV";
            return true;
        }

        // load nhóm người dùng với thuộc tính check box

        //lấy nhóm người dùng cho chức năng cập nhật nhóm người dùng cho nhân viên
        public Boolean LoadNhomNDByMaNV(DataGridView gv,string manv)
        {
            DataTable table = TaiKhoanDAO.Instance.getAllNhomNDByMaNV(manv);
            if (table == null) return false;
            DataColumn col = new DataColumn();
            col.ColumnName = "CHECK";
            col.DataType = typeof(Boolean);
            col.DefaultValue = false;
            table.Columns.Add(col);
            for (int i = 0; i < table.Rows.Count; i++)
            {
                try
                {
                    Convert.ToInt32(table.Rows[i][2].ToString());
                    table.Rows[i][3] = true;
                }
                catch { table.Rows[i][3] = false; }
            }
            table.Columns[3].SetOrdinal(0);
            gv.DataSource = null;
            gv.Rows.Clear();
            gv.DataSource = table;

            try
            {
                gv.Columns[gv.Columns.Count - 1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            }
            catch { }
            return true;
        }

        //
        public Boolean updateNhomChoUser(string manv,Boolean trangthai, List<int> DSNhom, List<Boolean> DSCheck)
        {
            TaiKhoanDAO.Instance.updateNhomUser(new TAIKHOAN(manv,trangthai),DSNhom,DSCheck, out int ErrCode, out string ErrMsg);

            CustomMessageBox.show("Kết quả", ErrMsg, ErrCode == 0);

            return ErrCode == 0;
        }
        // load nhóm người dùng với thuộc tính check box
        public Boolean LoadPHANQUYENNhanVien(DataGridView gv, string idnv,int idnhom)
        {
            try
            {
                gv.DataSource = null;
                gv.Rows.Clear();
                DataTable table = TaiKhoanDAO.Instance.getPhanQuyenNhanVien(idnv,idnhom);
                if (table == null) return false;
                gv.DataSource = table;
                gv.Columns[gv.Columns.Count - 1].Visible = false;
                gv.Columns[gv.Columns.Count - 1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
            catch
            {
                return false;
            }
            return true;
        }

        public Boolean LoadPHANQUYEN(DataGridView gv,int idNhom)
        {
            try {
                gv.DataSource = null;
                gv.Rows.Clear();
                DataTable table = TaiKhoanDAO.Instance.getPhanQuyenChucNang(idNhom);
                if (table == null) return false;
                DataColumn col = new DataColumn();
                col.ColumnName = "CHECK";
                col.DataType = typeof(Boolean);
                col.DefaultValue = false;
                table.Columns.Add(col);
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    try
                    {
                        Convert.ToInt32(table.Rows[i][2].ToString());
                        table.Rows[i][3] = true;
                    }
                    catch { table.Rows[i][3] = false; }
                }
                table.Columns[3].SetOrdinal(0);
                gv.DataSource = table;
                
            } catch {
            };
            
            
            return true;
        }

        public Boolean LoadDSTaiKhoan(DataGridView gv)
        {
            try
            {
                DataTable table = TaiKhoanDAO.Instance.getAllAccount();
                if (table == null) return false;
                gv.DataSource = table;
                if (table == null) return false;
                gv.Columns[gv.Columns.Count - 1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
            catch { }
            return true;
        }

        public string getTenNVbyID(string manv)
        {
           NHANVIEN nv= NhanVienDAO.Instance.getNhanVienByID(manv);
            return nv.TENNV;
        }
        public void InsertUser(string manv,string tennv,string matkhau,bool hoatdong,List<int>dsNhomQuyen)
        {
            TAIKHOANDN t = new TAIKHOANDN();
            t.MANV = manv;
            t.TENTK = tennv;t.TRANGTHAITK = hoatdong;t.MATKHAUTK = Common.HashMd5(matkhau);
            TaiKhoanDAO.Instance.addUser(t, dsNhomQuyen, out int? ErrCode, out string ErrMsg);
            CustomMessageBox.show("Kết quả", ErrMsg, ErrCode==0);
        }

        public void searchUser(string matk,BunifuMaterialTextbox txttk, BunifuMaterialTextbox txttentk, BunifuMaterialTextbox txtngaytao, BunifuiOSSwitch swtrangthai)
        {
            DataTable table = TaiKhoanDAO.Instance.getAllAccountByKey("", matk);
            if(table ==null || table.Rows.Count<=0)
            {
                MessageBox.Show("Tài khoản không tồn tại");
                return;
            }
            txttk.Text = table.Rows[0][0].ToString();
            txttentk.Text = table.Rows[0][1].ToString();
            swtrangthai.Value = Convert.ToBoolean(table.Rows[0][2].ToString());
            txtngaytao.Text = table.Rows[0][3].ToString();
        }

        /// <summary>
        /// Xóa tài khoản
        /// </summary>
        /// <param name="manv">mã tài khoản thực hiện thao tác xóa</param>
        /// <param name="manvdelete">mã tài khoản bị xóa</param>
        public Boolean deleteUser(string manv,string manvdelete)
        {
            TaiKhoanDAO.Instance.deleteUser(manv, manvdelete, out int ErrCode, out string ErrMsg);
            CustomMessageBox.show("Kết quả", ErrMsg, ErrCode == 0);
            return ErrCode==0;
        }

        public Boolean resetPass(string matk,string matkhau)
        {
            if(TaiKhoanDAO.Instance.resetPassUser(matk, Common.HashMd5(matkhau)))
            {
                CustomMessageBox.show("Kết quả", "Cập nhật mật khẩu thành công", true);
                return true;
            }
            return false;
        }

        public Boolean UpdatePass(string matk, string matkhaucu,string matkhaumoi)
        {
            TAIKHOAN t = new TAIKHOAN();
            t = TaiKhoanDAO.Instance.Login(matk, Common.HashMd5(matkhaucu), out int ErrCo, out string ErrMess);
            if (ErrCo != 0 || t==null)
            {
                CustomMessageBox.show("Kết quả", ErrMess, ErrCo == 0);
                return false;
            }
            if (TaiKhoanDAO.Instance.resetPassUser(matk, Common.HashMd5(matkhaumoi)))
            {
                CustomMessageBox.show("Kết quả", "Cập nhật mật khẩu thành công", true);
                return true;
            }
            return false;
        }

        public void insertNhomND(string tennhom,string ghichu)
        {
            TaiKhoanDAO.Instance.addNhomND(new NHOMND(0, tennhom, ghichu), out int ErrCode, out string ErrMsg);
            CustomMessageBox.show("Kết quả", ErrMsg, ErrCode == 0);

        }

        public Boolean updateNhomNguoiDung(int manhom, string tennhom,string ghichu,List<int>DSChucNang,List<Boolean>DSCheck)
        {
            NHOMNGUOIDUNG n = new NHOMNGUOIDUNG();
            n.MANHOM = manhom;n.TEMNHOM = tennhom;n.GHICHU = ghichu;
            TaiKhoanDAO.Instance.updateNhomND(n, DSChucNang, DSCheck, out int? ErrCode, out string ErrMsg);
            CustomMessageBox.show("Kết quả", ErrMsg, ErrCode == 0);

            return ErrCode == 0;
        }

        public Boolean deleteNhomNguoiDung(string manv,int manhom)
        {
            TaiKhoanDAO.Instance.deleteNhomND(manv,manhom, out int ErrCode, out string ErrMsg);
            CustomMessageBox.show("Kết quả", ErrMsg, ErrCode == 0);
            return ErrCode == 0;
        }
    }
}
