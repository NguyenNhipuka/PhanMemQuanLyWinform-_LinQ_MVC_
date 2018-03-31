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
    public class NhanVienDAO
    {
        private static NhanVienDAO instance;

        public NhanVienDAO()
        {
        }

        public static NhanVienDAO Instance
        {
            get
            {
                if (instance == null)
                    instance = new NhanVienDAO();
                return instance;
            }
        }
        //lay danh sach các nhân viên theo tên hoặc theo mã
        public List<NhanVienDTO> getStaffListBykey(string tennv = "", string manv = "")
        {
            DataTable data = new DataTable();
            List<NhanVienDTO> list = new List<NhanVienDTO>();
            using (SqlConnection connection = new SqlConnection(DataProvider.Instance.getConnectstring()))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("sp_STAFF_Getlist", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@TENNV", SqlDbType.NVarChar, 60, tennv);
                cmd.Parameters.Add("@MANV", SqlDbType.Char, 6, manv);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(data);
                connection.Close();
            }
            if (data != null)
            {
                foreach (DataRow item in data.Rows)
                {
                    NhanVienDTO T = new NhanVienDTO(item);
                    
                    list.Add(T);
                }

            }
            return list;
        }
        //lay danh sach tất cả nhân viên 
        public List<NhanVienDTO> getStaffList()
        {
            DataTable data = new DataTable();
            List<NhanVienDTO> list = new List<NhanVienDTO>();
            using (SqlConnection connection = new SqlConnection(DataProvider.Instance.getConnectstring()))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("sp_STAFF_Getlist", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(data);
                connection.Close();
            }
            if (data != null)
            {
                foreach (DataRow item in data.Rows)
                {
                    NhanVienDTO T = new NhanVienDTO();
                    list.Add(T);
                }

            }
            return list;
        }


        //lay danh sach tất cả nhân viên 
        public DataTable getStaffTable()
        {
            DataTable data = new DataTable();
            List<NhanVienDTO> list = new List<NhanVienDTO>();
            using (SqlConnection connection = new SqlConnection(DataProvider.Instance.getConnectstring()))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("sp_STAFF_Getlist", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(data);
                connection.Close();
            }
            
            return data;
        }

        //lay danh sach nhan vien
 
        public NHANVIEN getNhanVienByID(string manv)
        {            
            NHANVIEN nv = new NHANVIEN();
            nv = null;
            using (QLCHDTDataContext db = new QLCHDTDataContext())
            {
                List<NHANVIEN> dsncc = (from n in db.NHANVIENs where n.MANV==manv select n).ToList();
                if (dsncc.Count > 0) nv = dsncc[0];
            }
            return nv;
        }
        //lay danh sach nhan vien

        public sp_Staff_Getlist_ByIDResult getNhanVienID(string manv)
        {
            sp_Staff_Getlist_ByIDResult nv = new sp_Staff_Getlist_ByIDResult();
            nv = null;
            using (QLCHDTDataContext db = new QLCHDTDataContext())
            {
                List<sp_Staff_Getlist_ByIDResult> dsncc = db.sp_Staff_Getlist_ByID(manv).ToList();
                if (dsncc.Count > 0) nv = dsncc[0];
            }
            return nv;
        }
        //thêm nhân viên mới
        public Boolean InsertStaff(NHANVIEN nv, out int? ErrCode, out string ErrMsg)
        {
            ErrCode = 1;
            ErrMsg = "Thêm không thành công";
            using (QLCHDTDataContext db = new QLCHDTDataContext())
            {
                db.sp_Staff_Add(nv.TENNV, nv.GIOITINHNV, nv.NGAYSINH, nv.SDTNV, nv.CMND, nv.DIACHINV, nv.BACLUONG, nv.PHUCAP, nv.LUONG,nv.MACV, nv.TRANGTHAINV, ref ErrCode, ref ErrMsg);
                db.SubmitChanges();
            }

            return ErrCode == 0;
        }
        //update nhân viên
        public Boolean updeteStaff(NHANVIEN nv, out int? ErrCode, out string ErrMsg)
        {
            ErrCode = 1;
            ErrMsg = "Không thành công";
            using (QLCHDTDataContext db = new QLCHDTDataContext())
            {
                try
                {
                    db.sp_Staff_Update(nv.MANV, nv.MACV, nv.TENNV, nv.DIACHINV,
                        nv.SDTNV, nv.GIOITINHNV, nv.NGAYSINH, nv.TRANGTHAINV, nv.NGAYTAONV,
                        nv.CMND, nv.BACLUONG, nv.PHUCAP, nv.LUONG, ref ErrCode, ref ErrMsg);                  
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
        //xóa nhân viên
        public Boolean deleteStaff(string manv, out int? ErrCode, out string ErrMsg)
        {
            ErrCode = 0;
            ErrMsg = "Thực hiện không thành công";
            using (QLCHDTDataContext db = new QLCHDTDataContext())
            {
                try
                {
                    db.sp_Staff_Delete(manv, ref ErrCode, ref ErrMsg);                   
                    db.SubmitChanges();
                }
                catch { }

            }
            return ErrCode == 0;

        }
        //////////////////////////
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
        //lấy danh sách tất cả danh sách nhân viên
        public DataTable getAllStaff()
        {

            DataTable data = new DataTable();
            using (QLCHDTDataContext db = new QLCHDTDataContext())
            {
                List<sp_STAFF_GetlistResult> dsnv = db.sp_STAFF_Getlist().ToList();
                data = ConvertToDataTable(dsnv);
            }
            return data;
        }
        //lấy danh sách tất cả nhân viên excel
        public DataTable getAllStaffExcel()
        {

            DataTable data = new DataTable();
            using (QLCHDTDataContext db = new QLCHDTDataContext())
            {
                List<sp_STAFF_GetlistExcelResult> dsnv = db.sp_STAFF_GetlistExcel().ToList();
                data = ConvertToDataTable(dsnv);
            }
            return data;
        }
    }
}
