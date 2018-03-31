using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dapper;
using DTO;
using System.Data;
using System.Data.SqlClient;
using System.ComponentModel;

namespace DAO.Implementation
{
    public class TaiKhoanDAO
    {
        private static TaiKhoanDAO instance;

        public static TaiKhoanDAO Instance
        {
            get
            {
                if (instance == null)
                    instance = new TaiKhoanDAO();
                return instance;
            }
        }
        public TaiKhoanDAO()
        {
        }
        public TAIKHOAN Login(string username, string password, out int ErrCode, out string ErrMsg)
        {
            DataTable data = new DataTable();
            TAIKHOAN tk = new TAIKHOAN();
            int rowAffect = 0;
            using (SqlConnection connection = new SqlConnection(DataProvider.Instance.getConnectstring()))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("sp_User_Login", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@password", password);

                cmd.Parameters.Add("@ErrCode", SqlDbType.Int);
                cmd.Parameters["@ErrCode"].Direction = ParameterDirection.Output;

                cmd.Parameters.Add("@ErrMsg", SqlDbType.NVarChar, 200);
                cmd.Parameters["@ErrMsg"].Direction = ParameterDirection.Output;

                rowAffect = cmd.ExecuteNonQuery();
                ErrCode = 0;
                ErrCode = (int)cmd.Parameters["@ErrCode"].Value;
                ErrMsg = (string)cmd.Parameters["@ErrMsg"].Value;
                connection.Close();
            }
            if (rowAffect == 0)
            {
                foreach (DataRow item in data.Rows)
                {
                    tk = new TAIKHOAN(item);
                }

            }
            return tk;
        }


        //lay danh sach tai khoan
        public DataTable getAllAccountByKey(string tentk, string matk)
        {
            DataTable data = new DataTable();
            using (SqlConnection connection = new SqlConnection(DataProvider.Instance.getConnectstring()))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("sp_TaiKhoan_Getlist_ByName", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@MANV", matk);
                cmd.Parameters["@MANV"].SqlDbType = SqlDbType.Char;
                cmd.Parameters.AddWithValue("@TENNV", tentk);
                cmd.Parameters["@TENNV"].SqlDbType = SqlDbType.NVarChar;
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(data);
                connection.Close();
            }          
            return data;
        }
        //lay danh sach tai khoan
        public DataTable getAllAccount()
        {
            DataTable data = new DataTable();
            using (SqlConnection connection = new SqlConnection(DataProvider.Instance.getConnectstring()))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("sp_TaiKhoan_Getlist", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(data);
                connection.Close();
            }
            return data;
        }

        //kiem tra trang thai hoat dong cua tai khoan
        public Boolean check_Status(string manv)
        {
            string query = "sp_CheckStatus_User @Username";

            var result = DataProvider.Instance.ExecuteScalar(query, new object[] { manv });
            if (result == null) return false;//khoong hoat dong

            int x =Int32.Parse(result.ToString());
            return x==1;
        }
        //Lấy các quyền màn hình
        public List<int> getAllAccountRole( string manv = "")
        {
            DataTable data = new DataTable();
            List<int> list = new List<int>();
            using (SqlConnection connection = new SqlConnection(DataProvider.Instance.getConnectstring()))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("sp_LayQuyen", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@MANV", SqlDbType.Char,6).Value=manv;
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(data);
                connection.Close();
            }
            if (data != null)
            {
                foreach (DataRow item in data.Rows)
                {
                    list.Add(Convert.ToInt32(item[0]));
                }
            }
            return list;
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

        //Lấy các quyền màn hình
        public DataTable getAllNhomND()
        {
            DataTable data = new DataTable();
            List<sp_NhomND_GetlistResult> list = new List<sp_NhomND_GetlistResult>();
            using (QLCHDTDataContext db = new QLCHDTDataContext())
            {
                list = db.sp_NhomND_Getlist().ToList();
                data = ConvertToDataTable(list);
            }
           
            return data;
        }

        //Lấy các nhóm người dùng theo mã nhân viên
        public DataTable getAllNhomNDByMaNV(string idmanv)
        {
            DataTable data = new DataTable();
            using (QLCHDTDataContext db = new QLCHDTDataContext())
            {
                List<sp_NhomND_Getlist_ByMaNVResult> list = db.sp_NhomND_Getlist_ByMaNV(idmanv).ToList();
                data = ConvertToDataTable(list);
            }
            return data;
        }

        public Boolean addUser(TAIKHOANDN t, List<int> nhomquyen, out int? ErrCode, out string ErrMsg)
        {
            ErrCode =1; ErrMsg = "";
            using (QLCHDTDataContext db = new QLCHDTDataContext())
            {
                db.sp_Add_User(t.MANV, t.TENTK, t.MATKHAUTK, t.TRANGTHAITK, ref ErrCode, ref ErrMsg);
            }
            if (ErrCode==0)
                themUserVaoNhom(t.MANV,nhomquyen);
            return ErrCode == 0;
        }
     
        
        /// <summary>
        /// Reset pass cho tài khoản
        /// </summary>
        /// <param name="matk">MÃ tài khoản</param>
        /// <param name="mk">Mật khẩu mới</param>
        /// <returns></returns>
        /// 
        public Boolean resetPassUser(string matk,string mk)
        {
            int rowAffect = 0;
            using (SqlConnection connection = new SqlConnection(DataProvider.Instance.getConnectstring()))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("sp_User_ResetPassword", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@MANV", matk);
                cmd.Parameters["@MANV"].SqlDbType = SqlDbType.Char;
                cmd.Parameters.AddWithValue("@MATKHAUMOI", mk);
                cmd.Parameters["@MATKHAUMOI"].SqlDbType = SqlDbType.VarChar;
                rowAffect = cmd.ExecuteNonQuery();
                connection.Close();
            }
            // nếu mã code =0 và số dòng được insert khác không thì true(insert sucess)
            return (rowAffect != 0);
        }

        public bool themUserVaoNhom(string idUser, List<int> dsNhomQuyen)
        {                    
             using (QLCHDTDataContext db = new QLCHDTDataContext())
            {
                foreach (int i in dsNhomQuyen)
                {
                    db.sp_Add_NhomQuyen_Cho_User(idUser, i);
                }
                db.SubmitChanges();
            } 
            return true;
        }

        //xóa tài khoản user?????
        public bool deleteUser(string idUser,string idUserDelete, out int ErrCode, out string ErrMsg)

        {
            int rowAffect = 0;
            using (SqlConnection connection = new SqlConnection(DataProvider.Instance.getConnectstring()))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("sp_Delete_User", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@MANVDELE", idUserDelete);
                cmd.Parameters["@MANVDELE"].SqlDbType = SqlDbType.Char;
                cmd.Parameters.AddWithValue("@MANV", idUser);
                cmd.Parameters["@MANV"].SqlDbType = SqlDbType.Char;
                cmd.Parameters.Add("@ErrCode", SqlDbType.Int);
                cmd.Parameters["@ErrCode"].Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@ErrMsg", SqlDbType.NVarChar, 200);
                cmd.Parameters["@ErrMsg"].Direction = ParameterDirection.Output;
                rowAffect = cmd.ExecuteNonQuery();
                ErrCode = 0;
                ErrCode = Convert.ToInt32(cmd.Parameters["@ErrCode"].Value);
                ErrMsg = cmd.Parameters["@ErrMsg"].Value.ToString();
                connection.Close();
            }
            return true;
        }


        public DataTable getPhanQuyenChucNang(int idnhom)
        {
            DataTable data = new DataTable();
            using(QLCHDTDataContext db = new QLCHDTDataContext())
            {
                List<sp_PHANQUYEN_MANH_GetlistResult> list = db.sp_PHANQUYEN_MANH_Getlist(idnhom).ToList();
                data = ConvertToDataTable(list);
            }
            return data;
        }

        //lấy danh sách các quyền của nhân viên theo nhóm người dùng
        public DataTable getPhanQuyenNhanVien(string idmanv,int idnhom)
        {
            DataTable data = new DataTable();
            using(QLCHDTDataContext db = new QLCHDTDataContext())
            {
                List<sp_LayQuyen_NVResult> list = db.sp_LayQuyen_NV(idmanv, idnhom).ToList();
                data = ConvertToDataTable(list);
            }
            return data;
        }

        public Boolean addNhomND(NHOMND nhom, out int ErrCode, out string ErrMsg)
        {
            int rowAffect = 0;
            using (SqlConnection connection = new SqlConnection(DataProvider.Instance.getConnectstring()))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("sp_Create_NhomQuyen", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@TEMNHOM", nhom._tennhom);
                cmd.Parameters["@TEMNHOM"].SqlDbType = SqlDbType.NVarChar;
                cmd.Parameters.AddWithValue("@GHICHU", nhom._ghichu);
                cmd.Parameters["@GHICHU"].SqlDbType = SqlDbType.NVarChar;
                cmd.Parameters.Add("@ErrCode", SqlDbType.Int);
                cmd.Parameters["@ErrCode"].Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@ErrMsg", SqlDbType.NVarChar, 200);
                cmd.Parameters["@ErrMsg"].Direction = ParameterDirection.Output;
                rowAffect = cmd.ExecuteNonQuery();

                ErrCode = 0;
                ErrCode = Convert.ToInt32(cmd.Parameters["@ErrCode"].Value);
                ErrMsg = cmd.Parameters["@ErrMsg"].Value.ToString();
                connection.Close();
               
            }
         return true;
        }

        //public Boolean deleteNhomND(int idnhom, out int ErrCode, out string ErrMsg)
        //{
        //    int rowAffect = 0;
        //    using (SqlConnection connection = new SqlConnection(DataProvider.Instance.getConnectstring()))
        //    {
        //        connection.Open();
        //        SqlCommand cmd = new SqlCommand("sp_Delete_NhomQuyen", connection);
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        cmd.Parameters.AddWithValue("@MANHOM", idnhom);
        //        cmd.Parameters["@MANHOM"].SqlDbType = SqlDbType.Int;
        //        cmd.Parameters.Add("@ErrCode", SqlDbType.Int);
        //        cmd.Parameters["@ErrCode"].Direction = ParameterDirection.Output;
        //        cmd.Parameters.Add("@ErrMsg", SqlDbType.NVarChar, 200);
        //        cmd.Parameters["@ErrMsg"].Direction = ParameterDirection.Output;
        //        rowAffect = cmd.ExecuteNonQuery();
        //        ErrCode = 0;
        //        ErrCode = Convert.ToInt32(cmd.Parameters["@ErrCode"].Value);
        //        ErrMsg = cmd.Parameters["@ErrMsg"].Value.ToString();
        //        connection.Close();
        //    }
        //    return true;
        //}
        public Boolean deleteNhomND(string manv,int idnhom, out int ErrCode, out string ErrMsg)
        {
            ErrCode = 1;
            ErrMsg = "Xóa thành công";
           using(QLCHDTDataContext db=new QLCHDTDataContext())
            {
                List<NDNHOMND> list = (from n in db.NDNHOMNDs
                                            join  nv in db.NHANVIENs on
                                            n.MANV equals nv.MANV  where n.MANV == manv select  n ).ToList();
               foreach(NDNHOMND n in list)
                {
                    //bang cap thi k cho xoa
                    if(n.MANHOM ==idnhom)
                    {
                        ErrCode = 1;
                        ErrMsg = "Không thể xóa nhóm quyền đồng cấp";return ErrCode == 0;
                    }
                    
                }
                var nhom = (from n in db.NHOMNGUOIDUNGs where n.MANHOM == idnhom select n).FirstOrDefault();
                List<NDNHOMND> ds = (from nd in db.NDNHOMNDs where nd.MANHOM == idnhom select nd).ToList();
                List<PHANQUYEN>dspq = (from nd in db.PHANQUYENs where nd.MANHOM == idnhom select nd).ToList();
                foreach(PHANQUYEN p in dspq)
                {
                    db.PHANQUYENs.DeleteOnSubmit(p);db.SubmitChanges();
                }
                foreach (NDNHOMND n in ds)
                {
                    db.NDNHOMNDs.DeleteOnSubmit(n); db.SubmitChanges();
                }
                db.NHOMNGUOIDUNGs.DeleteOnSubmit(nhom);
                db.SubmitChanges();
            }
            return true;
        }

        public Boolean updateNhomND(NHOMNGUOIDUNG nhom,List<int> dsChucNang, List<Boolean>dscheck, out int? ErrCode, out string ErrMsg)
        {
           
              ErrCode = 1;
            ErrMsg = "Không thành công";
            using(QLCHDTDataContext db  =new QLCHDTDataContext())
            {
                db.sp_Update_NhomQuyen(nhom.MANHOM, nhom.TEMNHOM, nhom.GHICHU, ref ErrCode, ref ErrMsg);
                db.SubmitChanges();
            }
                if(ErrCode == 0)
                {
                     updatePhanQuyen(nhom.MANHOM, dsChucNang, dscheck);
                    return true;
                }
                
            
            return ErrCode==0;
        }

        public bool updatePhanQuyen(int manhom, List<int> dsChucNang,List<Boolean>dscheck)
        {
            using (QLCHDTDataContext db = new QLCHDTDataContext())
            {
                for (int i = 0; i < dsChucNang.Count; i++)
                {
                    db.sp_Update_PHANQUYEN(manhom, dsChucNang[i], dscheck[i]);
                    db.SubmitChanges();
                }
            }
               
            return true;
        }

        ///cập nhật nhóm quyền và trạng thái cho tài khoản user
        public Boolean updateNhomUser(TAIKHOAN tk, List<int> dsNhom, List<Boolean> dscheck, out int ErrCode, out string ErrMsg)
        {
            int rowAffect = 0;
            using (SqlConnection conn = new SqlConnection(DataProvider.Instance.getConnectstring()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("sp_Update_QuyenUser", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@MANV", tk._manv);
                cmd.Parameters["@MANV"].SqlDbType = SqlDbType.Char;
                cmd.Parameters.AddWithValue("@HOATDONG", tk._trangthai);
                cmd.Parameters["@HOATDONG"].SqlDbType = SqlDbType.Bit;
                cmd.Parameters.Add("@ErrCode", SqlDbType.Int);
                cmd.Parameters["@ErrCode"].Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@ErrMsg", SqlDbType.NVarChar, 200);
                cmd.Parameters["@ErrMsg"].Direction = ParameterDirection.Output;
                rowAffect = cmd.ExecuteNonQuery();
                ErrCode = 0;
                try
                {
                    ErrCode = Convert.ToInt32(cmd.Parameters["@ErrCode"].Value);
                   
                }
                catch { }
                ErrMsg = cmd.Parameters["@ErrMsg"].Value.ToString();
                if (rowAffect != 0)
                {
                    updateQuyenUser(tk._manv, dsNhom, dscheck, conn);
                }
                conn.Close();
            }
            return true;
        }
        /// hàm cập nhật nhóm quyền cho tài khoản user
        public bool updateQuyenUser(string manv, List<int> dsNhom, List<Boolean> dscheck, SqlConnection con)
        {

            for (int i = 0; i < dsNhom.Count; i++)
            {
                SqlCommand cmd = new SqlCommand("sp_Update_NhomQuyen_Cho_User", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@MANV", manv);
                cmd.Parameters["@MANV"].SqlDbType = SqlDbType.Char;
                cmd.Parameters.AddWithValue("@MANHOM", dsNhom[i]);
                cmd.Parameters["@MANHOM"].SqlDbType = SqlDbType.Int;
                cmd.Parameters.AddWithValue("@FLAT", Convert.ToBoolean(dscheck[i]));
                cmd.Parameters["@FLAT"].SqlDbType = SqlDbType.Bit;
                cmd.ExecuteNonQuery();
            }
            return true;
        }

        ///lay tat ca manv chua co tai khoan
        public DataTable getDSNhanVienChuaTK()
        {
            DataTable tb = new DataTable();
            tb = null;
            using(QLCHDTDataContext db = new QLCHDTDataContext())
            {
                List<sp_NHANVIEN_TK_GetlistResult> list = db.sp_NHANVIEN_TK_Getlist().ToList();
                tb = ConvertToDataTable(list);
            }
            return tb;
        }
    }
}
