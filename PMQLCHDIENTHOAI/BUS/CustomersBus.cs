using Bunifu.Framework.UI;
using DAO.Implementation;
using DTO;
using PMQLCHDIENTHOAI;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BUS
{
    public class CustomersBus
    {
        private static CustomersBus instance;

        public static CustomersBus Instance
        {
            get
            {
                if (instance == null)
                    instance = new CustomersBus();
                return instance;
            }
            private set
            {
                CustomersBus.instance = value;
            }
        }
       
        //load ds thông tin tất cả các khách hàng
        public Boolean LoadDSKH(DataGridView gv)
        {
            DataTable t = CustomersDAO.Instance.getAllKhachHang();
            gv.DataSource = null;
            gv.DataSource = t;
            return true;
        }
        //thêm khách hàng
        public Boolean AddKhachHang(string tenkh, string diachikh, string sdtkh, string emailkh, int tinhtrangkh)
        {
            KHACHHANG kh = new KHACHHANG();
            kh.TENKH = tenkh;
            kh.DIACHIKH = diachikh;
            kh.SDTKH = sdtkh;
            kh.EMAILKH = emailkh;
            kh.TINHTRANGKH = tinhtrangkh;
             CustomersDAO.Instance.addKhachHang(kh, out int? ErrCode, out string ErrMsg);
            CustomMessageBox.show("Kết quả", ErrMsg, ErrCode==0);
            return ErrCode == 0;
        }
        //tìm kiếm khách hàng theo mã khách hàng 
        public Boolean SearchKhachHangKey(int ma, BunifuMaterialTextbox makh, BunifuMaterialTextbox tenkh
            , BunifuMaterialTextbox sdtkh, BunifuMaterialTextbox emailkh,
            BunifuMaterialTextbox diachikh, BunifuiOSSwitch tinhtrangkh,
            BunifuMaterialTextbox solangd, BunifuMaterialTextbox tongtien)
        {
            sp_KHACHHANG_GetList_BykeyResult kh = new sp_KHACHHANG_GetList_BykeyResult();
            kh = null;
            kh= CustomersDAO.Instance.getKhachHangByKey(ma);
            if (kh != null)
            {
                kh.MAKH = ma;
                makh.Text = kh.MAKH.ToString();
                tenkh.Text = kh.TENKH;
                sdtkh.Text = kh.SDTKH;
                emailkh.Text = kh.EMAILKH;
                diachikh.Text = kh.DIACHIKH;
                tinhtrangkh.Value = Convert.ToBoolean(kh.TINHTRANGKH);                                            
                solangd.Text = kh.SOLAN.ToString();
                tongtien.Text = kh.TONG.ToString();
                return true;   
            }
            return false;
        }
        //xóa khách hàng
        public Boolean DeleteKhachHang(int makh)
        {
            Boolean result = CustomersDAO.Instance.deleteKhachHang(makh, out int? ErrCode, out string ErrMsg);
            CustomMessageBox.show("Kết quả", ErrMsg, result);
            return result;
        }
        //update khách hàng
        public Boolean UpdateKhachHang(int makh, string tenkh, string diachikh, string sdtkh,
           string emailkh, int tinhtrangkh)
        {
            KHACHHANG kh = new KHACHHANG();
            kh.MAKH = makh;
            kh.TENKH = tenkh;
            kh.DIACHIKH = diachikh;
            kh.SDTKH = sdtkh;
            kh.EMAILKH = emailkh;
            kh.TINHTRANGKH = Convert.ToInt32(tinhtrangkh);           
            Boolean result =CustomersDAO.Instance.updeteKhachHang(kh, out int? ErrCode, out string ErrMsg);
            CustomMessageBox.show("Kết quả", ErrMsg, result);
            return result;
        }
        //load tất cả Khách hàng lên Excel 
        public Boolean LoadDSKHExcel(DataGridView gv)
        {
            DataTable table = CustomersDAO.Instance.getAllKHExcel();
            if (table != null)
            {
                gv.DataSource = null;
                gv.DataSource = table;
            }
            return true;
        }
    }
}
