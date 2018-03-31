using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DTO;
using System.Data.SqlClient;
using System.ComponentModel;
using System.Data;

namespace DAO.Implementation
{
    public class BaoHanh_SuaChuaDAO
    {
        private static BaoHanh_SuaChuaDAO instance;

        public BaoHanh_SuaChuaDAO()
        {
        }

        public static BaoHanh_SuaChuaDAO Instance
        {
            get
            {
                if (instance == null)
                    instance = new BaoHanh_SuaChuaDAO();
                return instance;
            }
        }
        private DataTable ConvertToDataTable<T>(IList<T> data)
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

        #region ==PHIẾU BẢO HÀNH
        
        //lấy ds  các thiết bị  TRONG HÓA ĐƠN BÁN VỚI SL ĐÃ LẬP BẢO HÀNH combobox
        public DataTable getAll_TB_HDB_PBH_MHD(string mahdb)
        {
            DataTable tb = new DataTable();
            using (QLCHDTDataContext db = new QLCHDTDataContext())
            {
                List<sp_TB_HDB_PBH_GetlistMAHBResult> ds = db.sp_TB_HDB_PBH_GetlistMAHB(mahdb).ToList();
                tb = ConvertToDataTable(ds);
            }
            return tb;
        }

        //lấy thông tin của của thiết bị theo mã hóa đơn và mã hàng
        public sp_TB_HDB_PBH_GetlistMAHDB_MHResult getAll_TB_HDB_PBH_MH(string mahd,string matb)
        {

            sp_TB_HDB_PBH_GetlistMAHDB_MHResult tb = new sp_TB_HDB_PBH_GetlistMAHDB_MHResult();
            tb = null;
            using (QLCHDTDataContext db = new QLCHDTDataContext())
            {
                List<sp_TB_HDB_PBH_GetlistMAHDB_MHResult> dstb = db.sp_TB_HDB_PBH_GetlistMAHDB_MH(mahd,matb).ToList();
                if (dstb.Count > 0) tb = dstb[0];
            }
            return tb;
        }

        //lấy ds của các thiết bị  số lượng trong hoa đơn bán đã  lập bảo hành lên gridview
        public DataTable getAll_TB_PBH(string mahdb)
        {
            DataTable tb = new DataTable();
            tb = null;
            using (QLCHDTDataContext db = new QLCHDTDataContext())
            {
                List<sp_TB_HDB_DaPBH_GetlistMAHBResult> ds = db.sp_TB_HDB_DaPBH_GetlistMAHB(mahdb).ToList();
                tb = ConvertToDataTable(ds);
            }
            return tb;
        }

        public void updatePBH()
        {
            using (QLCHDTDataContext db = new QLCHDTDataContext())
            {
                try
                {
                    db.sp_PHIEUBAOHANH_Update();
                    db.SubmitChanges();
                }
                catch { }
                 
            }
        }

        //lập phiếu bảo hành
        public Boolean addThietBi(PHIEUBAOHANH p, out int? ErrCode, out string ErrMsg)
        {
            ErrCode = 0;
            ErrMsg = "Thêm thành công";
            using (QLCHDTDataContext db = new QLCHDTDataContext())
            {
                db.sp_PHIEUBAOHANH_Add(p.MAHDB, p.MAH, p.MANV, p.SERIALBH, ref ErrCode, ref ErrMsg);
                db.SubmitChanges();
            }
            return ErrCode == 0;
        }

        //lấy ds phiếu bảo hành
        public DataTable getAllPBH()
        {
            DataTable tb = new DataTable();
            using (QLCHDTDataContext db = new QLCHDTDataContext())
            {
                List<sp_PHIEUBAOHANH_GetlistResult> ds = db.sp_PHIEUBAOHANH_Getlist().ToList();
                tb = ConvertToDataTable(ds);
            }
            return tb;
        }

        #endregion

        #region ===BẢO HÀNH
        //lấy thông tin của một phiếu bảo hành theo mã
        public sp_PHIEUBAOHANH_GetlistByIDResult getInfo_PBH_IDPBH(int ma_pbh)
        {
            sp_PHIEUBAOHANH_GetlistByIDResult tb = new sp_PHIEUBAOHANH_GetlistByIDResult();
            tb = null;
            using (QLCHDTDataContext db = new QLCHDTDataContext())
            {
                List<sp_PHIEUBAOHANH_GetlistByIDResult> dstb = db.sp_PHIEUBAOHANH_GetlistByID(ma_pbh).ToList();
                if (dstb.Count > 0) tb = dstb[0];
            }
            return tb;
        }

        //lấy ds  bảo hành
        public DataTable getAllBH()
        {
            DataTable tb = new DataTable();
            tb = null;
            using (QLCHDTDataContext db = new QLCHDTDataContext())
            {
                List<sp_BAOHANH_GetlistResult> ds = db.sp_BAOHANH_Getlist().ToList();
                tb = ConvertToDataTable(ds);
            }
            return tb;
        }
        //lấy ds  bảo hành theo mã phiếu bảo hành
        public DataTable getBH_ByIDPBH(int maphieu)
        {
            DataTable tb = new DataTable();
            using (QLCHDTDataContext db = new QLCHDTDataContext())
            {
                List<sp_BAOHANH_GetlistByIDPBHResult> ds = db.sp_BAOHANH_GetlistByIDPBH(maphieu).ToList();
                tb = ConvertToDataTable(ds);
            }
            return tb;
        }

        //lấy thông tin của một phiếu bảo hành theo mã
        public sp_BAOHANH_GetlistByIDBHResult getInfo_BH_IDBH(int ma_bh)
        {
            sp_BAOHANH_GetlistByIDBHResult tb = new sp_BAOHANH_GetlistByIDBHResult();
            tb = null;
            using (QLCHDTDataContext db = new QLCHDTDataContext())
            {
                List<sp_BAOHANH_GetlistByIDBHResult> dstb = db.sp_BAOHANH_GetlistByIDBH(ma_bh).ToList();
                if (dstb.Count > 0) tb = dstb[0];
            }
            return tb;
        }

        /// update bảo hành
        public void updateBaoHanh(BAOHANH h)
        {
           
            using (QLCHDTDataContext db = new QLCHDTDataContext())
            {
               
                    db.sp_BAOHANH_UpdateByID(h.IDBH,h.NGUYENNHAN,h.NGAYGIAOBH,h.TINHTRANGBH);
                    db.SubmitChanges();
            }
        }

        //lập  bảo hành
        public Boolean addBH(BAOHANH p, out int? ErrCode, out string ErrMsg,out int mahd)
        {
            ErrCode = 0;
            ErrMsg = "Thêm thành công";
            mahd = 0;
            using (QLCHDTDataContext db = new QLCHDTDataContext())
            {
                db.sp_BAOHANH_Add(p.IDPBH,p.NGUYENNHAN,p.MANV,p.NGAYGIAOBH,p.SDTNHAN, ref ErrCode, ref ErrMsg);
                db.SubmitChanges();
               
                BAOHANH c = new BAOHANH();
                List<BAOHANH> ds =new List<BAOHANH>();ds = null;
                ds =(from b in db.BAOHANHs where p.IDPBH == p.IDPBH && p.NGAYGIAOBH == b.NGAYGIAOBH select b).ToList();
                if (ds.Count > 0) c = ds[0];
                mahd = c.IDBH;
            }
            return ErrCode == 0;
        }


        //lấy ds  sửa chưa
        public DataTable getAllSC()
        {
            DataTable tb = new DataTable();
            tb = null;
            using (QLCHDTDataContext db = new QLCHDTDataContext())
            {
                List<sp_SUACHUA_GetlistResult> ds = db.sp_SUACHUA_Getlist().ToList();
                tb = ConvertToDataTable(ds);
            }
            return tb;
        }

        //lấy ds chi tiết  sửa chưa
        public DataTable getAllCTSC(string masc)
        {
            DataTable tb = new DataTable();
            tb = null;
            using (QLCHDTDataContext db = new QLCHDTDataContext())
            {
                List<sp_CTHDSC_GetlistByIDInfoResult> ds = db.sp_CTHDSC_GetlistByIDInfo(masc).ToList();
                tb = ConvertToDataTable(ds);
            }
            return tb;
        }
        //lập  SỮA CHỮA
        public Boolean addSC(SUACHUA s,List<CTHDSC>ds, out int? ErrCode, out string ErrMsg, out string masc)
        {
            ErrCode = 1;
            ErrMsg = "Thêm không thành công";
            masc="0000000000";
            using (QLCHDTDataContext db = new QLCHDTDataContext())
            {
                db.sp_SUACHUA_Add(s.MAKH, s.MANV, s.TENKHSC, s.SDTKHSC, s.NGAYGIAOSC, s.TONGCHIPHISC,
                    s.TINHTRANGSC, ref ErrCode, ref ErrMsg, ref masc);
                db.SubmitChanges();
                if (masc == "0000000000") return false;
                foreach (CTHDSC c in ds)                   
                {
                    c.MASC = masc;
                    db.sp_CTHDSC_Add(c.MASC, c.MAH, c.TENTBSC, c.LOISC, c.MOTASC, c.CHIPHISC, c.TINHTRANGCTSC);
                }
                db.SubmitChanges();
            }
            return ErrCode == 0;
        }

        //tim hoa don sua chua theo ma
        public sp_SUACHUA_GetlistByIDResult getHDSCByID(string masc)
        {
           sp_SUACHUA_GetlistByIDResult tb = new sp_SUACHUA_GetlistByIDResult();
            tb = null;
            using (QLCHDTDataContext db = new QLCHDTDataContext())
            {
                List<sp_SUACHUA_GetlistByIDResult> ds = db.sp_SUACHUA_GetlistByID(masc).ToList();
                if (ds.Count > 0) tb = ds[0];
            }
            return tb;
        }

        ///lay thong TIN ctHDSC theo
        public List<sp_CTHDSC_GetlistByIDResult> getCTSCID(string maphieu)
        {
            List<sp_CTHDSC_GetlistByIDResult> ds = null;
            using (QLCHDTDataContext db = new QLCHDTDataContext())
            {
                 ds = db.sp_CTHDSC_GetlistByID(maphieu).ToList();
            }
            return ds;
        }

        //update sửa chữa
        public Boolean UpdateSC(SUACHUA s, out int? ErrCode, out string ErrMsg)
        {
            ErrCode = 1;
            ErrMsg = "Cập nhật không thành công";
            using (QLCHDTDataContext db = new QLCHDTDataContext())
            {
                db.sp_SUACHUA_Update(s.MASC, s.TENKHSC, s.SDTKHSC, s.NGAYGIAOSC, s.TONGCHIPHISC, s.TINHTRANGSC, ref ErrCode, ref ErrMsg);
                db.SubmitChanges();               
               
            }
            return ErrCode == 0;
        }
        public Boolean UpdateTBSC(CTHDSC c)
        {
           
            using (QLCHDTDataContext db = new QLCHDTDataContext())
            {
                    db.sp_CTHDSC_Update(c.SOHIEU, c.LOISC, c.MOTASC, c.CHIPHISC, c.TINHTRANGCTSC);
                    db.SubmitChanges();

            }
            return true;
        }
        #endregion
    }
}
