using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DTO;
using DAO.Implementation;
using Bunifu.Framework.UI;
using MetroFramework.Forms;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;
using MetroFramework.Controls;
using DAO;
using PMQLCHDIENTHOAI;
using System.ComponentModel;

namespace BUS
{
    public class NCCBus
    {
        private static NCCBus instance;

        public static NCCBus Instance
        {
            get
            {
                if (instance == null)
                    instance = new NCCBus();
                return instance;
            }
            private set
            {
                NCCBus.instance = value;
            }
        }

       
        //load tất cả nhà cung cấp lên combobox to hdm
        public Boolean LoadNCCNameToHDM(MetroComboBox cb)
        {
            DataTable table = NccDAO.Instance.getAllNCCToHDM();
            if (table != null)
            {
                cb.DataSource = table;
                cb.DisplayMember = "TENNCC";
                cb.ValueMember = "MANCC";
            }
            return true;
        }
        //load tất cả nhà cung cấp lên combobox
        public Boolean LoadNCCName(MetroComboBox cb)
        {
            DataTable table = NccDAO.Instance.getAllNCC_name();
            if (table != null)
            {
                cb.DataSource = table;
                cb.DisplayMember = "TENNCC";
                cb.ValueMember = "MANCC";
            }
            return true;
        }
        //load nhà cung cấp
        public Boolean LoadDSNCC(DataGridView gv)
        {
            DataTable t = NccDAO.Instance.getAllNCC();
            gv.DataSource = null;
            gv.DataSource = t;
            return true;
        }

        //thêm nhà cung cấp
        public Boolean AddNCC(string tenncc, string email, string sdt, string stk, string diachi, int manh, bool tinhtrang)
        {
            NCC t = new NCC();
            t.TENNCC = tenncc;
            t.EMAILNCC = email;
            t.SDTNCC = sdt;
            t.STKBANK = stk;
            t.DIACHINCC = diachi;
            t.MANH = manh;
            t.TRANGTHAINCC = tinhtrang;
            Boolean result = NccDAO.Instance.addNCC(t, out int? ErrCode, out string ErrMsg);
            CustomMessageBox.show("Kết quả", ErrMsg, result);
            return result;
        }
        //load tất cả ngân hàng lên combobox
        public Boolean LoadBank(ComboBox cb)
        {
            DataTable table = NccDAO.Instance.getDSAllBank();
            if (table != null)
            {
                cb.DataSource = table;
                cb.DisplayMember = "TENNH";
                cb.ValueMember = "MANH";
            }
            return true;            
        }

        //load tất cả ngân hàng lên datagridview
        public Boolean LoadBank(DataGridView gv)
        {
            DataTable table = NccDAO.Instance.getDSAllBank();
            if (table != null)
            {
                gv.DataSource = null;
                //gv.Rows.Clear();
                gv.DataSource = table;

            }
            return true;
        }

        //public Boolean LoadBank(DataGridView gv)
        //{
        //    DataTable t = NccDAO.Instance.getDSAllBank();
        //    gv.DataSource = null;
        //    gv.DataSource = t;
        //    return true;
        //}
        //thêm ngân hàng
        public Boolean AddBank(string tennh)
        {
            Boolean result = NccDAO.Instance.addBank(tennh, out int? ErrCode, out string ErrMsg);
            CustomMessageBox.show("Kết quả", ErrMsg, result);
            return result;
        }
        //
        public DataTable ConvertToDataTable<T>(IList<T> data)
        {
            PropertyDescriptorCollection properties =
               TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            foreach (PropertyDescriptor prop in properties)
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            foreach (T item in data)
            {
                DataRow row = table.NewRow();
                foreach (PropertyDescriptor prop in properties)
                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                table.Rows.Add(row);
            }
            return table;

        }
        //xóa ngân hàng
        public Boolean DeleteBank(int manh)
        {
            Boolean result = NccDAO.Instance.deleteBank(manh, out int? ErrCode, out string ErrMsg);
            CustomMessageBox.show("Kết quả", ErrMsg, result);
            return result;
        }
        //tìm kiếm ncc theo mã nhà cung cấp 
        public Boolean SearchNCCKey(int macc, BunifuMaterialTextbox mancc,BunifuMaterialTextbox tenncc
            , BunifuMaterialTextbox sdtncc,
            ComboBox manhncc, BunifuMaterialTextbox stkncc, BunifuMaterialTextbox emailncc, BunifuMaterialTextbox diachincc,
            MetroDateTime ngaytaoncc, BunifuiOSSwitch tinhtrangncc,
            BunifuMaterialTextbox solangd, BunifuMaterialTextbox tongtien)
        {
            sp_NCC_GetList_BykeyResult ncc = NccDAO.Instance.getNCCByKey(macc);
            
            if (ncc != null)
            {
               
                mancc.Text = ncc.MANCC.ToString();
                tenncc.Text = ncc.TENNCC;
                emailncc.Text = ncc.EMAILNCC;
                sdtncc.Text = ncc.SDTNCC;
                stkncc.Text = ncc.STKBANK;
                diachincc.Text = ncc.DIACHINCC;
                LoadBank(manhncc);
                manhncc.SelectedValue = Convert.ToInt32(ncc.MANH);
                solangd.Text = ncc.SOLAN.ToString();
                tongtien.Text = ncc.TONG.ToString();

                try
                {
                   ngaytaoncc.Value = DateTime.Parse(ncc.NGAYTAONCC.ToString());
                }
                catch { }

                if (ncc.TRANGTHAINCC == false)
                {
                    tinhtrangncc.Value = false;
                }
                else tinhtrangncc.Value = true;

                return true;
            }
            mancc.Text = tenncc.Text = emailncc.Text = sdtncc.Text= stkncc.Text = diachincc.Text = "";
            LoadBank(manhncc);
            solangd.Text ="0";
            tongtien.Text = "0";
            CustomMessageBox.show("Thông báo", "Không tồn tại nhà cung cấp này", false);
            return false;
        }
        //update ngân hàng
        public Boolean UpdateBank(int manh, string tennh)
        {
            Boolean result = NccDAO.Instance.updateBank(manh, tennh, out int? ErrCode, out string ErrMsg);
            CustomMessageBox.show("Kết quả", ErrMsg, result);
            return result;
        }
        //update nhà cung cấp
        public Boolean UpdateNCC(int mancc, string tenncc, string emailncc, string sdtncc, string stkncc,
            string diachincc, int manhncc, int tinhtrangncc)
        {
            NCC ncc = new NCC();
            ncc.MANCC = mancc;
            ncc.TENNCC = tenncc;
            ncc.EMAILNCC = emailncc;
            ncc.SDTNCC = sdtncc;
            ncc.STKBANK = stkncc;
            ncc.DIACHINCC = diachincc;
            ncc.MANH = manhncc;           
            ncc.TRANGTHAINCC = Convert.ToBoolean(tinhtrangncc);         
            Boolean result = NccDAO.Instance.updateNCC(ncc, out int? ErrCode, out string ErrMsg);
            CustomMessageBox.show("Kết quả", ErrMsg, result);
            return result;
        }
        //xóa nhà cung cấp
        public Boolean DeleteNCC(int mancc)
        {
            Boolean result = NccDAO.Instance.deleteNCC(mancc, out int? ErrCode, out string ErrMsg);
            CustomMessageBox.show("Kết quả", ErrMsg, result);
            return result;
        }
        //load tất cả NCC lên Excel 
        //public Boolean LoadDSNCCExcel(DataGridView gv)
        //{
        //    DataTable table = NccDAO.Instance.getAllNCCExcel();
        //    if (table != null)
        //    {
        //        gv.DataSource = null;
        //        gv.DataSource = table;
        //    }
        //    return true;
        //}
        //load ncc excel
        public Boolean LoadDSNCCExcel(DataGridView gv)
        {
            DataTable t = NccDAO.Instance.getAllNCCExcel();
            gv.DataSource = null;
            gv.DataSource = t;
            return true;
        }
    }
}
