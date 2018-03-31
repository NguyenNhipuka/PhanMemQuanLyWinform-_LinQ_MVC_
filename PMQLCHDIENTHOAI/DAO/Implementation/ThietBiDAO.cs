using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using DTO;
using System.Data.SqlClient;
using System.ComponentModel;
namespace DAO.Implementation
{
    public class ThietBiDAO
    {
        private static ThietBiDAO instance;

        public ThietBiDAO()
        {
        }

        public static ThietBiDAO Instance
        {
            get
            {
                if (instance == null)
                    instance = new ThietBiDAO();
                return instance;
            }
        }

        //Lấy danh sách tất cả loại thiết bị
        public DataTable getAllLoaiTB()
        {
            DataTable data = new DataTable();
            using (SqlConnection connection = new SqlConnection(DataProvider.Instance.getConnectstring()))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("sp_LOAI_Getlist", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(data);
                connection.Close();
            }
            return data;
        }

        //Lấy danh sách tất cả đơn vị tính của thiết bị
        public DataTable getAllDVTTB()
        {
            DataTable data = new DataTable();
            using (SqlConnection connection = new SqlConnection(DataProvider.Instance.getConnectstring()))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("sp_DVT_Getlist", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(data);
                connection.Close();
            }
            return data;
        }

        //lấy ds thông tin của tất cả tất cả các thiết bị
        public DataTable getAllTB()
        {
            
            DataTable data = new DataTable();
            using (QLCHDTDataContext db = new QLCHDTDataContext())
            {
                List<sp_TB_GetlistResult> dsTatCaTB = db.sp_TB_Getlist().ToList();
                data = ConvertToDataTable(dsTatCaTB);
            }
            return data;
        }


        //lấy ds thông tin của tất cả tất cả các thiết bị vơi mã thiết bị và tên tb theo mã nhà cung cấp
        public DataTable getAllTBIDName(int mancc)
        {

            DataTable data = new DataTable();
            using (QLCHDTDataContext db = new QLCHDTDataContext())
            {
                List<sp_TB_GetlistMAH_TENHResult> dsTatCaTB = db.sp_TB_GetlistMAH_TENH(mancc).ToList();
                data = ConvertToDataTable(dsTatCaTB);
            }
            return data;
        }
        //lấy ds thông tin của tất cả tất cả các thiết bị vơi mã thiết bị và tên tb
        public DataTable getAllTBIDNameHDB()
        {

            DataTable data = new DataTable();
            using (QLCHDTDataContext db = new QLCHDTDataContext())
            {
                List<sp_TB_GetlistMAH_TENH_HDBResult> dsTatCaTB = db.sp_TB_GetlistMAH_TENH_HDB().ToList();
                data = ConvertToDataTable(dsTatCaTB);
            }
            return data;
        }
        //lấy ds thông tin của tất cả tất cả các thiết bị
        public DataTable getAllTBExcel()
        {

            DataTable data = new DataTable();
            using (QLCHDTDataContext db = new QLCHDTDataContext())
            {
                var dstb= from tb in db.HANGs
                           join l in db.LOAIHANGs on tb.MALOAI equals l.MALOAI
                           select new
                           { tb.MAH,tb.TENH,tb.DONGIAB,tb.TGBH,tb.MOTAH};
                data = ConvertToDataTable(dstb.ToList());
            }
            return data;
        }

        //lấy ds thông tin của của thiết bị theo mã
        public sp_TB_Getlist_ByKeyResult getTBByKey(string matb)
        {

            sp_TB_Getlist_ByKeyResult tb = new sp_TB_Getlist_ByKeyResult();
            tb = null;
            using (QLCHDTDataContext db = new QLCHDTDataContext())
            {
                List<sp_TB_Getlist_ByKeyResult>dstb = db.sp_TB_Getlist_ByKey(matb).ToList();
                if (dstb.Count > 0) tb = dstb[0];
            }
            return tb;
        }

        //lấy ds của các thiết bị theo tình trạng với số lượng trong hóa đơn bán theo ngày
        public DataTable getTBSOLuongBan_Ngay(DateTime dfrom,DateTime dto,Byte ngoaitru_tinhtrang)
        {
            DataTable tb = new DataTable();
            using (QLCHDTDataContext db = new QLCHDTDataContext())
            {
                List<sp_TB_Getlist_SoLuongBanNgayResult> ds = db.sp_TB_Getlist_SoLuongBanNgay(dfrom,dto,ngoaitru_tinhtrang).ToList();
                tb = ConvertToDataTable(ds);
            }
            return tb;
        }


        //lấy ds của các thiết bị với số lượng trong hóa đơn bán
        public DataTable getAllTBSOLuongBan_Ngay()
        {
            DataTable tb = new DataTable();
            using (QLCHDTDataContext db = new QLCHDTDataContext())
            {
                List<sp_TB_Getlist_AllSoLuongBanNgayResult> ds = db.sp_TB_Getlist_AllSoLuongBanNgay().ToList();
                tb = ConvertToDataTable(ds);
            }
            return tb;
        }

        //lấy ds của các thiết bị theo tình trạng với số lượng trong hoa đơn mua theo ngày
        public DataTable getTBSOLuongMua_Ngay(DateTime dfrom, DateTime dto, Byte ngoaitru_tinhtrang)
        {
            DataTable tb = new DataTable();
            using (QLCHDTDataContext db = new QLCHDTDataContext())
            {
                List<sp_TB_Getlist_SoLuongMuaNgayResult> ds = db.sp_TB_Getlist_SoLuongMuaNgay(dfrom, dto, ngoaitru_tinhtrang).ToList();
                tb = ConvertToDataTable(ds);
            }
            return tb;
        }

        //lấy ds của các thiết bị  số lượng trong hoa đơn mua 
        public DataTable getAllTBSOLuongMua_Ngay()
        {
            DataTable tb = new DataTable();
            using (QLCHDTDataContext db = new QLCHDTDataContext())
            {
                List<sp_TB_Getlist_AllSoLuongMuaNgayResult> ds = db.sp_TB_Getlist_AllSoLuongMuaNgay().ToList();
                tb = ConvertToDataTable(ds);
            }
            return tb;
        }

        //lấy ds của các thiết bị sắp hết hàng
        public DataTable getTBSapHet()
        {
            DataTable tb = new DataTable();
            using (QLCHDTDataContext db = new QLCHDTDataContext())
            {
                List<sp_TB_Getlist_SapHetResult> ds = db.sp_TB_Getlist_SapHet().ToList();
                tb = ConvertToDataTable(ds);
            }
            return tb;
        }

        //lấy ds thông tin của của thiết bị theo mã
        public sp_TB_Getlist_ByKeyHDMResult getTBByKeyToHDM(string matb)
        {

            DataTable data = new DataTable();
            sp_TB_Getlist_ByKeyHDMResult tb = new sp_TB_Getlist_ByKeyHDMResult();
            using (QLCHDTDataContext db = new QLCHDTDataContext())
            {
                List<sp_TB_Getlist_ByKeyHDMResult> dstb = db.sp_TB_Getlist_ByKeyHDM(matb).ToList();
                if (dstb.Count > 0) tb = dstb[0];
            }
            return tb;
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


        public Boolean addThietBi(HANG h,CT_HANG c, out int? ErrCode,out string ErrMsg) 
        {
            ErrCode = 0;
            ErrMsg = "Thêm thành công";
            using (QLCHDTDataContext db = new QLCHDTDataContext())
            {
                db.sp_TB_Add(h.MALOAI, h.MADVT, h.MANCC, h.TENH, h.SLH, h.DONGIAM, h.DONGIAB, h.TGBH,
                    h.NSX, h.TINHTRANGH, h.MOTAH, c.TON_MAX, c.TON_MIN,
                    c.KHUYENMAI_H,ref ErrCode ,ref ErrMsg);
                db.SubmitChanges();
            }

            return ErrCode==0;
        }

        //thêm loại thiết bị
        public Boolean addLoaiThietBi(string tenloai, out int? ErrCode, out string ErrMsg)
        {
            ErrCode = 0;
            ErrMsg = "Thêm thành công";
            using (QLCHDTDataContext db = new QLCHDTDataContext())
            {
                db.sp_Add_LOAI(tenloai,ref ErrCode, ref ErrMsg);
                db.SubmitChanges();
            }
            return ErrCode == 0;
        }

        //cập nhật loại thiết bị
        public Boolean updateLoaiThietBi(int maloai,string tenloai, out int? ErrCode, out string ErrMsg)
        {
            ErrCode = 0;
            ErrMsg = "";
            using (QLCHDTDataContext db = new QLCHDTDataContext())
            {
                db.sp_Update_LOAI(maloai,tenloai, ref ErrCode, ref ErrMsg);
                db.SubmitChanges();
            }
            return ErrCode == 0;
        }

        //xóa loại thiết bị
        public Boolean deleteLoaiThietBi(int maloai,out int? ErrCode, out string ErrMsg)
        {
            ErrCode = 0;
            ErrMsg = "Thực hiện không thành công";
            using(QLCHDTDataContext db = new QLCHDTDataContext())
            {
                try
                {
                    db.sp_Delete_LOAI(maloai, ref ErrCode, ref ErrMsg);
                    db.SubmitChanges();
                }
                catch { }
                
            }
            return ErrCode == 0;
        }

        //thêm đơn vị tính thiết bị
        public Boolean addDVTThietBi(string tendonvi, out int? ErrCode, out string ErrMsg)
        {
            ErrCode = 0;
            ErrMsg = "Thêm thành công";
            using (QLCHDTDataContext db = new QLCHDTDataContext())
            {
                db.sp_Add_DVT(tendonvi, ref ErrCode, ref ErrMsg);
                db.SubmitChanges();
            }
            return ErrCode == 0;
        }

        //cập nhật đơn vị tính thiết bị
        public Boolean updateDVTThietBi(int madonvi, string tendonvi, out int? ErrCode, out string ErrMsg)
        {
            ErrCode = 0;
            ErrMsg = "";
            using (QLCHDTDataContext db = new QLCHDTDataContext())
            {
                db.sp_Update_DVT(madonvi, tendonvi, ref ErrCode, ref ErrMsg);
                db.SubmitChanges();
            }
            return ErrCode == 0;
        }

        //xóa đơn vị tính thiết bị
        public Boolean deleteDVTThietBi(int madonvi, out int? ErrCode, out string ErrMsg)
        {
            ErrCode = 0;
            ErrMsg = "Thực hiện không thành công";
            using (QLCHDTDataContext db = new QLCHDTDataContext())
            {
                try { 
                     db.sp_Delete_DVT(madonvi, ref ErrCode, ref ErrMsg);
                    db.SubmitChanges();
                } catch { }
               
            }
            return ErrCode == 0;
        }

        //xóa thiết bị
        public Boolean deleteThietBi(string matb, out int? ErrCode, out string ErrMsg)
        {
            ErrCode = 0;
            ErrMsg = "Thực hiện không thành công";
            using (QLCHDTDataContext db = new QLCHDTDataContext())
            {
                try
                {
                    db.sp_TB_Delete(matb, ref ErrCode, ref ErrMsg);
                    db.SubmitChanges();
                }
                catch { }

            }
            return ErrCode == 0;
        }


        //update thiet bi
        public Boolean updeteThietBi(HANG h, CT_HANG c, out int? ErrCode, out string ErrMsg)
        {
            ErrCode = 1;
            ErrMsg = "Không thành công";
            using (QLCHDTDataContext db = new QLCHDTDataContext())
            {
                try
                {
                    db.sp_TB_Update(h.MAH, h.MALOAI, h.MADVT, h.MANCC, h.TENH, h.SLH, h.DONGIAM, h.DONGIAB, h.TGBH,
                    h.NSX, h.TINHTRANGH, h.MOTAH, c.TON_MAX, c.TON_MIN,
                    c.KHUYENMAI_H, ref ErrCode, ref ErrMsg);

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

    }
}
