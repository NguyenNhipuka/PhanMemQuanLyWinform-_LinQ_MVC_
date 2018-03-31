using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DTO;
using System.Data.SqlClient;
using System.Data;

namespace DAO.Implementation
{
    public class ChucVuDAO
    {
        private static ChucVuDAO instance;

        public ChucVuDAO()
        {
        }

        public static ChucVuDAO Instance
        {
            get
            {
                if (instance == null)
                    instance = new ChucVuDAO();
                return instance;
            }
        }

        //lay danh sach tất cả nhân viên 
        public DataTable getPositionTable()
        {
            DataTable data = new DataTable();
            List<ChucVuDTO> list = new List<ChucVuDTO>();
            using (SqlConnection connection = new SqlConnection(DataProvider.Instance.getConnectstring()))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("sp_POSITION_Getlist", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(data);
                connection.Close();
            }
            return data;
        }

        public Boolean InsertPosition(string tencv, out int? ErrCode, out string ErrMsg)
        {
            ErrCode = 1;
            ErrMsg = "Thêm không thành công";
            using (QLCHDTDataContext db = new QLCHDTDataContext())
            {
                db.sp_Add_CHUCVU(tencv, ref ErrCode, ref ErrMsg);
                db.SubmitChanges();
            }

            return ErrCode == 0;
        }
       
            public Boolean updatePosition(int macv,string tencv, out int? ErrCode, out string ErrMsg)
        {
            ErrCode = 1;
            ErrMsg = "Cập nhật không thành công";
            using (QLCHDTDataContext db = new QLCHDTDataContext())
            {
                db.sp_Update_CHUCVU(macv,tencv, ref ErrCode, ref ErrMsg);
                db.SubmitChanges();
            }

            return ErrCode == 0;
        }
        
           public Boolean deletePosition(int macv, out int? ErrCode, out string ErrMsg)
        {
            ErrCode = 1;
            ErrMsg = "Cập nhật không thành công";
            using (QLCHDTDataContext db = new QLCHDTDataContext())
            {
                List<NHANVIEN> dstim = new List<NHANVIEN>();
                dstim = null;
                dstim = (from n in db.NHANVIENs where n.MACV == macv select n).ToList();
                List<CHUCVU> ds = new List<CHUCVU>();
                ds = null;
                ds = (from n in db.CHUCVUs where n.MACV==macv select n).ToList();
                if (ds != null)
                {
                    if (dstim.Count >0)
                    {
                        ErrCode = 1;
                        ErrMsg = "Mã chức vụ này đang được sử dụng";
                    }
                    else
                    {
                        db.CHUCVUs.DeleteOnSubmit(ds[0]);
                        db.SubmitChanges();
                        ErrCode = 0;
                        ErrMsg = "Xóa chức vụ thành công";
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
    }
}
