using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DTO;
using System.Data;
using System.Data.SqlClient;
using System.ComponentModel;

namespace DAO.Implementation
{
    public class NccDAO
    {
        private static NccDAO instance;

        public NccDAO()
        {
        }

        public static NccDAO Instance
        {
            get
            {
                if (instance == null)
                    instance = new NccDAO();
                return instance;
            }
        }

        //lay tên các danh sach nha cung cap
        public DataTable getAllSuppliersName()
        {
            DataTable data = new DataTable();
            using (SqlConnection connection = new SqlConnection(DataProvider.Instance.getConnectstring()))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("sp_NCC_GetListName", connection);
                cmd.CommandType = CommandType.StoredProcedure;             
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(data);
                connection.Close();
            }
            return data;
        }

        //lay nha cung cap theo mã
        public DataTable getSuppliers_ById(int id)
        {
            DataTable data = new DataTable();
            List<NHACUNGCAP> list = new List<NHACUNGCAP>();
            using (SqlConnection connection = new SqlConnection(DataProvider.Instance.getConnectstring()))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("sp_NCC_GetId", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(data);
                connection.Close();
            }
            return data;
        }

        //lấy ds thông tin của tất cả tất cả ncc theo tên
        public DataTable getAllNCC_name()
        {

            DataTable data = new DataTable();
            using (QLCHDTDataContext db = new QLCHDTDataContext())
            {
                List<sp_NCC_GetListNameResult> ds = db.sp_NCC_GetListName().ToList();
                data = ConvertToDataTable(ds);
            }
            return data;
        }

        //lấy ds thông tin của tất cả tất cả ncc cho hdm
        public DataTable getAllNCCToHDM()
        {

        //
            DataTable data = new DataTable();
            using (QLCHDTDataContext db = new QLCHDTDataContext())
            {
                var ds = from ncc in db.NCCs
                         where ncc.TRANGTHAINCC ==true
                         select new
                         { ncc.MANCC,ncc.TENNCC};
                data = ConvertToDataTable(ds.ToList());
            }
            return data;
        }
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
        //Lấy danh sách tất nhà cung cấp
        public DataTable getDSAllNCC()
        {
            DataTable data = new DataTable();
            using (SqlConnection connection = new SqlConnection(DataProvider.Instance.getConnectstring()))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("sp_NCC_Getlist", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(data);
                connection.Close();
            }
            return data;
        }
        //lấy danh sách tất cả nhà cung cấp
        public DataTable getAllNCC()
        {

            DataTable data = new DataTable();
            using (QLCHDTDataContext db = new QLCHDTDataContext())
            {
                List<sp_NCC_GetListResult> dsncc = db.sp_NCC_GetList().ToList();               
                data = ConvertToDataTable(dsncc);
            }
            return data;
        }
        //thêm nhà cung cấp
        public Boolean addNCC(NCC ncc, out int? ErrCode, out string ErrMsg)
        {
            ErrCode = 0;
            ErrMsg = "Thêm thành công";
            using (QLCHDTDataContext db = new QLCHDTDataContext())
            {
                db.sp_NCC_Add(ncc.TENNCC, ncc.EMAILNCC, ncc.SDTNCC, ncc.DIACHINCC, ncc.STKBANK,
                   ncc.MANH, ncc.TRANGTHAINCC, ref ErrCode, ref ErrMsg);
                db.SubmitChanges();
            }

            return ErrCode == 0;
        }     
        

        //thêm tên ngân hàng
        public Boolean addBank(string tenbank, out int? ErrCode, out string ErrMsg)
        {
            ErrCode = 0;
            ErrMsg = "Thêm thành công";
            using (QLCHDTDataContext db = new QLCHDTDataContext())
            {
                db.sp_BANK_Add(tenbank, ref ErrCode, ref ErrMsg);
                db.SubmitChanges();
            }
            return ErrCode == 0;
        }
        //XÓA NGÂN HÀNG
        public Boolean deleteBank(int manh, out int? ErrCode, out string ErrMsg)
        {
            ErrCode = 1;
            ErrMsg = "Thực hiện không thành công";
            using (QLCHDTDataContext db = new QLCHDTDataContext())
            {

                var nh = from n in db.NCCs where n.MANH==manh select n;
                List<BANK> ds = new List<BANK>();
                ds = null;
                ds = (from n in db.BANKs where n.MANH == manh select n).ToList();
                if (ds != null)
                {
                    if (nh != null)
                    {
                        ErrCode = 1;
                        ErrMsg = "Mã ngân hàng này đang được sử dụng";
                    }
                    else
                    {
                        db.BANKs.DeleteOnSubmit(ds[0]);
                        db.SubmitChanges();
                        ErrCode = 0;
                        ErrMsg = "Xóa ngân hàng thành công";
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
        //Lấy danh sách tất cả ngân hàng
        public DataTable getDSAllBank()
        {
            DataTable data = new DataTable();
            using (SqlConnection connection = new SqlConnection(DataProvider.Instance.getConnectstring()))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("sp_Bank_Getlist", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(data);
                connection.Close();
            }
            return data;
        }
        //lấy ds thông tin của nhà cung cấp theo mã
        public sp_NCC_GetList_BykeyResult getNCCByKey(int mancc)
        {

            DataTable data = new DataTable();
            sp_NCC_GetList_BykeyResult ncc = new sp_NCC_GetList_BykeyResult();
            using (QLCHDTDataContext db = new QLCHDTDataContext())
            {
                List<sp_NCC_GetList_BykeyResult> dsncc = db.sp_NCC_GetList_Bykey(mancc).ToList();
                if (dsncc.Count > 0) ncc = dsncc[0];
            }
            return ncc;
        }
        //update ngân hàng
        public Boolean updateBank(int manh, string tennh, out int? ErrCode, out string ErrMsg)
        {
            ErrCode = 0;
            ErrMsg = "";
            using (QLCHDTDataContext db = new QLCHDTDataContext())
            {                
                db.sp_BANK_Update(manh, tennh, ref ErrCode, ref ErrMsg);
                db.SubmitChanges();
            }
            return ErrCode == 0;
        }
        //update nhà cung cấp 
        public Boolean updateNCC(NCC ncc, out int? ErrCode, out string ErrMsg)
        {
            ErrCode = 1;
            ErrMsg = "Không thành công";
            using (QLCHDTDataContext db = new QLCHDTDataContext())                
            {
                try
                {
                    db.sp_NCC_Update(ncc.MANCC, ncc.TENNCC, ncc.EMAILNCC, ncc.SDTNCC, ncc.STKBANK,
                   ncc.DIACHINCC, ncc.MANH, ncc.TRANGTHAINCC, ref ErrCode, ref ErrMsg);
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
        //xóa nhà cung cấp
        public Boolean deleteNCC(int mancc, out int? ErrCode, out string ErrMsg)
        {
            ErrCode = 0;
            ErrMsg = "Thực hiện thành công";
            using (QLCHDTDataContext db = new QLCHDTDataContext())
            {
               
                var ncc = from nc in db.HDMs where nc.MANCC == mancc select nc;
                List<NCC> ds = new List<NCC>();
                ds = null;
                ds = (from n in db.NCCs where n.MANCC == mancc select n).ToList();
                if (ds != null)
                {
                    if (ncc != null)
                    {
                        ErrCode = 1;
                        ErrMsg = "Mã nhà cung cấp này đang được sử dụng";
                    }
                    else
                    {
                        db.NCCs.DeleteOnSubmit(ds[0]);
                        db.SubmitChanges();
                        ErrCode = 0;
                        ErrMsg = "Xóa nhà cung cấp thành công";
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
        
        //lấy danh sách tất cả NCC excel
        public DataTable getAllNCCExcel()
        {

            DataTable data = new DataTable();
            using (QLCHDTDataContext db = new QLCHDTDataContext())
            {
                List<sp_NCC_GetListExcelResult> dsncc = db.sp_NCC_GetListExcel().ToList();
                data = ConvertToDataTable(dsncc);
            }
            return data;
        }
    }
}
