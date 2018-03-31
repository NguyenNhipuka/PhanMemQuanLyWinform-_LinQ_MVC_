using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DTO;
using DAO.Implementation;
using System.Windows.Forms;
using Bunifu.Framework.UI;
using PMQLCHDIENTHOAI;
using MetroFramework.Forms;
using MetroFramework.Controls;

namespace BUS
{
    public class NhanVienBus
    {
        private static NhanVienBus instance;

        public static NhanVienBus Instance
        {
            get
            {
                if (instance == null)
                    instance = new NhanVienBus();
                return instance;
            }
            private set
            {
                NhanVienBus.instance = value;
            }
        }

        public Boolean LoadStaffList(DataGridView gv)
        {
            DataTable table = DAO.Implementation.NhanVienDAO.Instance.getStaffTable();

            gv.DataSource = null;
            gv.Rows.Clear();
            if(table != null)
            {
                //DataColumn dcAuto = new DataColumn();
                //dcAuto.AutoIncrement = true;
                //dcAuto.AutoIncrementSeed = 1;
                //dcAuto.ColumnName = "STT";
                //table.Columns.Add(dcAuto);
                gv.DataSource = table;
            }
           
            return true;
        }

        
        public Boolean LoadSomeStaffList(DataGridView gv)
        {
            DataTable table = DAO.Implementation.NhanVienDAO.Instance.getStaffTable();
            gv.DataSource = null;
            gv.Rows.Clear();
            if (table != null)
            {
                DataTable t = new DataTable();
                t = table.Copy();
                for (int i = t.Columns.Count - 1; i > 3; i--)
                {
                   t.Columns.RemoveAt(i);
                }
                gv.DataSource = t; 
                
            }
            return true;
        }


        public Boolean InsertStaff(int macv, string ten, string diachi,
            string sdt, string gioitinh, DateTime ngaysinh,
            string cmnd, double bacluong, double phucap, double luong, bool trangthai)
        {
            NHANVIEN nv = new NHANVIEN();
            nv.TENNV = ten;
            nv.MACV = macv;nv.DIACHINV = diachi;
            nv.GIOITINHNV = gioitinh; nv.NGAYSINH = ngaysinh; nv.SDTNV = sdt;
            nv.CMND = cmnd;nv.BACLUONG = bacluong; nv.PHUCAP = phucap;
            nv.LUONG = luong; nv.TRANGTHAINV = trangthai;

           NhanVienDAO.Instance.InsertStaff(nv, out int? ErrCode,out string ErrMsg);
            CustomMessageBox.show("Kết quả", ErrMsg, ErrCode==0);
            return ErrCode==0;

        }

        //TIM NHAN VIEN
        public Boolean searchNhanVien(string txtnv, BunifuMaterialTextbox txtmanv, BunifuMaterialTextbox txttennv,
            BunifuiOSSwitch swtrangthai, BunifuiOSSwitch swgioitinh,
            MetroDateTime datepicngaysinh, BunifuMaterialTextbox txtsdt, BunifuMaterialTextbox txtcmnd,
            BunifuMaterialTextbox txtdiachi, BunifuMaterialTextbox txtbacluong, BunifuMaterialTextbox txtphucap,
            BunifuMaterialTextbox txtluong, ComboBox cbchucvu, MetroDateTime datepicngaytao)
        {
            //NHANVIEN nv = new NHANVIEN();
            //nv = null;
            //nv = NhanVienDAO.Instance.getNhanVienByID(txtnv);
            sp_Staff_Getlist_ByIDResult nv = new sp_Staff_Getlist_ByIDResult();
            nv = null;
            nv = NhanVienDAO.Instance.getNhanVienID(txtnv);
            if (nv==null)
            {
                CustomMessageBox.show("Kết quả", "Tài khoản không tồn tại", false);
                return false;
            }
            txtmanv.Text = nv.MANV.ToString();
            txttennv.Text = nv.TENNV.ToString();
            
            swtrangthai.Value = nv.TRANGTHAINV ==true?true:false;
            swgioitinh.Value = false;
            if (nv.GIOITINHNV.Equals("NỮ"))
            {
                swgioitinh.Value = true;
            }       
                    
            try
            {
                datepicngaysinh.Value = DateTime.Parse(nv.NGAYSINH.ToString());
            }
            catch { }
            txtsdt.Text = nv.SDTNV;
            txtcmnd.Text = nv.CMND;
            txtdiachi.Text = nv.DIACHINV;
            txtbacluong.Text = nv.BACLUONG.ToString();
            txtphucap.Text = nv.PHUCAP.ToString();
            txtluong.Text = nv.LUONG.ToString();
            cbchucvu.SelectedValue = Convert.ToInt32(nv.MACV);
           
            try
            {
                datepicngaytao.Value = DateTime.Parse(nv.NGAYTAONV.ToString());
            }
            catch { }
            return nv != null;
        }
        //update nhân viên
        public Boolean UpdateStaff(string manv, int macvnv, string tennv, string diachinv, string sdtnv,
           string gtnv, DateTime ngaysinhnv, int trangthainv, DateTime ngaytaonv, string cmndnv,
           double bacluongnv, double phucapnv, double luongnv)
        {
            NHANVIEN nv = new NHANVIEN();
            nv.MANV = manv;
            nv.MACV = macvnv;
            nv.TENNV = tennv;
            nv.DIACHINV = diachinv;
            nv.SDTNV = sdtnv;
            nv.GIOITINHNV = gtnv;
            nv.NGAYSINH = ngaysinhnv;
            nv.TRANGTHAINV = Convert.ToBoolean(trangthainv);
            nv.NGAYTAONV = ngaytaonv;
            nv.CMND = cmndnv;
            nv.BACLUONG = bacluongnv;
            nv.PHUCAP = phucapnv;
            nv.LUONG = luongnv;
            Boolean result = NhanVienDAO.Instance.updeteStaff(nv, out int? ErrCode, out string ErrMsg);
            CustomMessageBox.show("Kết quả", ErrMsg, result);
            return result;
        }
        //xoa nhân viên
        public Boolean DeleteStaff(string manv)
        {
            Boolean result = NhanVienDAO.Instance.deleteStaff(manv, out int? ErrCode, out string ErrMsg);
            CustomMessageBox.show("Kết quả", ErrMsg, result);
            return result;
        }
        ////////////////////////
        //load nhân viên
        public Boolean LoadDSStaff(DataGridView gv)
        {
            DataTable t = NhanVienDAO.Instance.getAllStaff();
            gv.DataSource = null;
            gv.DataSource = t;
            return true;
        }
        //load nhân viên excel
        public Boolean LoadDSStaffExcel(DataGridView gv)
        {
            DataTable t = NhanVienDAO.Instance.getAllStaffExcel();
            gv.DataSource = null;
            gv.DataSource = t;
            return true;
        }


    }
}
