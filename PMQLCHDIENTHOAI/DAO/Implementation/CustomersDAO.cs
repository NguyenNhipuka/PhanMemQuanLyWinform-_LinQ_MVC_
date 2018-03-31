using DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;

namespace DAO.Implementation
{
    public class CustomersDAO
    {
        public static CustomersDAO instance;
        public CustomersDAO()
        {
        }

        public static CustomersDAO Instance
        {
            get
            {
                if (instance == null)
                    instance = new CustomersDAO();
                return instance;
            }
        }
        //convert
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
        //lấy ds thông tin của tất cả khách hàng
        public DataTable getAllKhachHang()
        {

            DataTable data = new DataTable();
            using (QLCHDTDataContext db = new QLCHDTDataContext())
            {
                List < sp_KHACHHANG_GetlistResult > dsallKH = db.sp_KHACHHANG_Getlist().ToList();
                data = ConvertToDataTable(dsallKH);                
            }
            return data;
        }
        
        //thêm khách hàng mới
        public Boolean addKhachHang(KHACHHANG kh, out int? ErrCode, out string ErrMsg)
        {
            ErrCode = 0;
            ErrMsg = "Thêm thành công";
            using (QLCHDTDataContext db = new QLCHDTDataContext())
            {
                db.sp_KHACHHANG_Add(kh.TENKH, kh.DIACHIKH, kh.SDTKH, kh.EMAILKH, kh.TINHTRANGKH, ref ErrCode, ref ErrMsg);
                db.SubmitChanges();
            }

            return ErrCode == 0;
        }
        //search thông tin của khách hàng theo mã
        public sp_KHACHHANG_GetList_BykeyResult getKhachHangByKey(int makh)
        {

            DataTable data = new DataTable();
            sp_KHACHHANG_GetList_BykeyResult kh = new sp_KHACHHANG_GetList_BykeyResult();
            kh = null;
            using (QLCHDTDataContext db = new QLCHDTDataContext())
            {
                List<sp_KHACHHANG_GetList_BykeyResult> ttkh = db.sp_KHACHHANG_GetList_Bykey(makh).ToList();
                if (ttkh.Count > 0) kh = ttkh[0];
            }
            return kh;
        }
        //xóa khách hàng
        public Boolean deleteKhachHang(int makh, out int? ErrCode, out string ErrMsg)
        {
            ErrCode = 1;
            ErrMsg = "Thực hiện không thành công";
            using (QLCHDTDataContext db = new QLCHDTDataContext())
            {

                List<HDB> dshdb = null;
                dshdb=( from kh in db.HDBs where kh.MAKH==makh select kh).ToList();
                List<SUACHUA> kh_suachua = null;
                kh_suachua=( from kh in db.SUACHUAs where kh.MAKH == makh select kh).ToList();
                List<KHACHHANG> ds = new List<KHACHHANG>();
                ds = null;
                ds = (from n in db.KHACHHANGs where n.MAKH == makh select n).ToList();
                if (ds != null)
                {
                    if (dshdb.Count >0 || kh_suachua.Count>0)
                    {
                        ErrCode = 1;
                        ErrMsg = "Khách hàng này đang được sử dụng";
                    }
                    else
                    {
                        db.KHACHHANGs.DeleteOnSubmit(ds[0]);
                        db.SubmitChanges();
                        ErrCode = 0;
                        ErrMsg = "Xóa khách hàng thành công";
                    }
                }
                else
                {
                    ErrCode = 1;
                    ErrMsg = "Xóa không thành công";
                }

            }
            return ErrCode == 0;
        }
        //update khách hàng
        public Boolean updeteKhachHang(KHACHHANG kh, out int? ErrCode, out string ErrMsg)
        {
            ErrCode = 1;
            ErrMsg = "Không thành công";
            using (QLCHDTDataContext db = new QLCHDTDataContext())
            {
                try
                {
                    db.sp_KHACHHANG_Update(kh.MAKH,kh.TENKH, kh.DIACHIKH, kh.SDTKH, 
                        kh.EMAILKH, kh.TINHTRANGKH, ref ErrCode, ref ErrMsg);                  
                    db.SubmitChanges();
                }
                catch
                {
                    ErrCode = 1;
                    ErrMsg = "Không thành công";
                }
            }
            return ErrCode == 0;
        }
        //lấy ds thông tin all Khách hàng 
        public DataTable getAllKHExcel()
        {
            DataTable data = new DataTable();
            using (QLCHDTDataContext db = new QLCHDTDataContext())
            {
                var dskh = from kh in db.KHACHHANGs
                           select new { kh.MAKH, kh.TENKH };           
                data = ConvertToDataTable(dskh.ToList());
            }
            return data;
        }
    }
}
