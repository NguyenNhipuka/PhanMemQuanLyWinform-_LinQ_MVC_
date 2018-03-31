using DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace DAO.Implementation
{
    public class HoaDonBanDAO
    {
        private static HoaDonBanDAO instance;

        public HoaDonBanDAO()
        {
        }

        public static HoaDonBanDAO Instance
        {
            get
            {
                if (instance == null)
                    instance = new HoaDonBanDAO();
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

        ///lấy danh sách khách hàng lên combobox
        public DataTable getAllKhachHangHDB()
        {

            DataTable data = new DataTable();
            using (QLCHDTDataContext db = new QLCHDTDataContext())
            {
                List<sp_KH_GetlistMAKH_TENKH_HDBResult> ds = db.sp_KH_GetlistMAKH_TENKH_HDB().ToList();
                data = ConvertToDataTable(ds);
            }
            return data;
        }

        ///lấy danh sách khách hàng lên combobox
        public DataTable getAllLyDoHDB()
        {

            DataTable data = new DataTable();
            using (QLCHDTDataContext db = new QLCHDTDataContext())
            {
                List<LYDOXUAT> ds = (from l in db.LYDOXUATs select l).ToList();
                data = ConvertToDataTable(ds);
            }
            return data;
        }

        ///lấy thông tin khách hàng theo mã cho hóa đơn bán
        public KHACHHANG getKhachHangInfoByKey(int makh)
        {
            KHACHHANG kh = new KHACHHANG();
            kh = null;
            using (QLCHDTDataContext db = new QLCHDTDataContext())
            {
                List<KHACHHANG> ds = (from k in db.KHACHHANGs where k.MAKH == makh select k).ToList();
                if (ds.Count > 0) kh = ds[0];
            }
            return kh;
        }

        ///lấy phí vận chuyển theo mã
        public PTGIAOHANG getPTGHByKeyHDB(int mapt)
        {
            PTGIAOHANG pt = new PTGIAOHANG();
            pt = null;
            using (QLCHDTDataContext db = new QLCHDTDataContext())
            {
                List<PTGIAOHANG> ds = (from p in db.PTGIAOHANGs where p.MAPTGH==mapt select p).ToList();
                if (ds.Count > 0) pt = ds[0];
            }
            return pt;
        }
        ///lấy danh sách PHƯƠNG THỨC GIAO HÀNG  lên combobox
        public DataTable getAllPhuongThucGiaoHangHDB()
        {

            DataTable data = new DataTable();
            using (QLCHDTDataContext db = new QLCHDTDataContext())
            {
                List<sp_PTGH_Getlist_InfoResult> ds = db.sp_PTGH_Getlist_Info().ToList();
                data = ConvertToDataTable(ds);
            }
            return data;
        }

        ///Thêm hóa đơn BÁN
        public bool themHDB(HDB h, List<CTHDB> ds, out int? ErrCode, out string ErrMsg,out string mahd)
        {
            ErrCode = 1;
            ErrMsg = "Thêm không thành công";
             mahd = "0000000000";
            using (QLCHDTDataContext db = new QLCHDTDataContext())
            {

                db.sp_HDB_Add(h.MANV,h.MAKH,h.TRIGIAHDB,h.TINHTRANGHDB,h.PHIGIAOH,
                    h.MALYDO,h.MAPTGH,h.SDTKHB,h.DIACHIKHB,h.TENKHB,h.THUTHEMHDB,h.NOHDB, ref ErrCode, ref ErrMsg, ref mahd);
                db.SubmitChanges();
                if (ErrCode == 0 && mahd != "0000000000")
                {
                    CTHDB c;
                    for (int i = 0; i < ds.Count; i++)
                    {
                         c = new CTHDB();
                        c = ds[i];
                        db.sp_CTHDB_Add(c.MAH, mahd, c.DONGIAHDB, c.TGBHBAN, c.SOLUONGB, c.KHUYENMAI, c.THANHTIEN, h.TINHTRANGHDB);
                        db.SubmitChanges();
                    }
                }
            }
            return ErrCode == 0;
        }
        //Lấy danh sách tất cả PTGH
        public DataTable getDSAllPTGH()
        {
            DataTable data = new DataTable();
            using (SqlConnection connection = new SqlConnection(DataProvider.Instance.getConnectstring()))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("sp_PTGIAOHANG_Getlist", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(data);
                connection.Close();
            }
            return data;
        }

        ///lấy danh sách hóa đơn bán
        public DataTable getAllHDB()
        {

            DataTable data = new DataTable();
            using (QLCHDTDataContext db = new QLCHDTDataContext())
            {
                List<sp_HDB_GetlistResult> ds = db.sp_HDB_Getlist().ToList();
                data = ConvertToDataTable(ds);
            }
            return data;
        }

        //thêm phương thức giao hàng
        public Boolean addPTGH(string tenPTGH, double phi, out int? ErrCode, out string ErrMsg)
        {
            ErrCode = 0;
            ErrMsg = "Thêm thành công";
            try
            {
                using (QLCHDTDataContext db = new QLCHDTDataContext())
                {
                    db.sp_PTGIAOHANG_Add(tenPTGH, phi, ref ErrCode, ref ErrMsg);
                    db.SubmitChanges();
                }
            }
            catch { }
            
            return ErrCode == 0;
        }
        //delete PTGH
        public Boolean deletePTGH(int maptgh, out int? ErrCode, out string ErrMsg)
        {
            ErrCode = 0;
            ErrMsg = "Thực hiện thành công";
            using (QLCHDTDataContext db = new QLCHDTDataContext())
            {
                try
                {
                    db.sp_PTGIAOHANG_Delete(maptgh, ref ErrCode, ref ErrMsg);
                    db.SubmitChanges();
                }
                catch { }

            }
            return ErrCode == 0;
        }
        //update PTGH 
        public Boolean updatePTGH(PTGIAOHANG  ptgh, out int? ErrCode, out string ErrMsg)
        {
            ErrCode = 1;
            ErrMsg = "Không thành công";            
                using (QLCHDTDataContext db = new QLCHDTDataContext())
                {
                    try

                    {
                        db.sp_PTGIAOHANG_Update(ptgh.MAPTGH, ptgh.TENPTGH, ptgh.PHIPTGH, ref ErrCode, ref ErrMsg);
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
        ///lấy danh sách hóa đơn bán đã thanh toán hết
        public DataTable getAllHDBThanhToan()
        {

            DataTable data = new DataTable();
            using (QLCHDTDataContext db = new QLCHDTDataContext())
            {
                List<sp_HDB_GetlistThanhToanResult> ds =db.sp_HDB_GetlistThanhToan().ToList();
                data = ConvertToDataTable(ds);
            }
            return data;
        }

        ///lấy danh sách hóa đơn bán theo ngày
        public DataTable getAllHDBTheoNgay(DateTime fromDate,DateTime toDate)
        {
            DataTable data = new DataTable();
            using (QLCHDTDataContext db = new QLCHDTDataContext())
            {
                List<sp_HDB_GetlistNgayResult> ds = db.sp_HDB_GetlistNgay(fromDate, toDate).ToList();
                data = ConvertToDataTable(ds);
            }
            return data;
        }


        ///lấy danh sách chi tiết  hóa đơn bán
        public DataTable getAllCTHDBByKeyMAHDB(string mahdb)
        {

            DataTable data = new DataTable();
            using (QLCHDTDataContext db = new QLCHDTDataContext())
            {
                List<sp_CTHDB_Getlist_ByKeyHDBResult> ds = db.sp_CTHDB_Getlist_ByKeyHDB(mahdb).ToList();
                data = ConvertToDataTable(ds);
            }
            return data;
        }

        ///lấy danh sách hóa đơn bán còn nợ
        public DataTable getAllHDBNo()
        {

            DataTable data = new DataTable();
            using (QLCHDTDataContext db = new QLCHDTDataContext())
            {
                List<sp_HDB_GetlistNOResult> ds = db.sp_HDB_GetlistNO().ToList();
                data = ConvertToDataTable(ds);
            }
            return data;
        }

        ///Hiện danh sách thông tin cua một hóa đơn theo mã cho form thông tin hóa đơn
        public sp_HDB_GetlistInfoByMHDBResult getHDMByKey(string mahd)
        {

            sp_HDB_GetlistInfoByMHDBResult tb = new sp_HDB_GetlistInfoByMHDBResult();
            tb = null;
            using (QLCHDTDataContext db = new QLCHDTDataContext())
            {
                List<sp_HDB_GetlistInfoByMHDBResult> ds = db.sp_HDB_GetlistInfoByMHDB(mahd).ToList();
                if (ds.Count > 0) tb = ds[0];
            }
            return tb;
        }

        ///Cập nhật  hóa đơn bán
        public bool capNhatHDB(string mahd, Byte trinhtranghdm,double thanhtoan, out int? ErrCode, out string ErrMsg)
        {
            ErrCode = 1;
            ErrMsg = "Cập nhật không thành công";
            using (QLCHDTDataContext db = new QLCHDTDataContext())
            {
                Byte tinhtrangbandau = 0;
                 var t = (from h in db.HDBs where h.MAHDB == mahd select h.TINHTRANGHDB).Take(1);
                if (t == null)
                {
                   
                    return false;
                }
                    tinhtrangbandau = Byte.Parse(t.FirstOrDefault().ToString());
                db.sp_HDB_Update_HDB(mahd, trinhtranghdm, thanhtoan, ref ErrCode, ref ErrMsg);
                db.SubmitChanges();
                //cẬP NHẬT TINH TRẠNG HÓA ĐƠN BÁN
                if (ErrCode == 0 && tinhtrangbandau != trinhtranghdm)
                {
                    var ds = from ct in db.CTHDBs where ct.MAHDB == mahd select ct;

                    if (tinhtrangbandau == 2 )
                    {
                        //tru so luong hang theo sthdb
                        //2 ->3 :tru sl hang
                        //2->1:tru sl hang
                        if (trinhtranghdm == 1 || trinhtranghdm == 3)
                        {
                            foreach (CTHDB c in ds)
                            {
                                var hang = from h in db.HANGs where h.MAH == c.MAH select h;
                                foreach (HANG h in hang)
                                {
                                    h.SLH -= c.SOLUONGB;
                                    if(h.SLH == 0)
                                    {
                                        h.TINHTRANGH = 0;
                                    }
                                    db.SubmitChanges();
                                }


                            }
                        }
                    }
                    ////3->0:congsl,3->2: cong lại
                    else if ((tinhtrangbandau == 3 && trinhtranghdm ==0) || (tinhtrangbandau == 3 && trinhtranghdm == 2))
                    {
                        //lay lai so luong hang
                        foreach (CTHDB c in ds)
                        {
                            var hang = from h in db.HANGs where h.MAH == c.MAH select h;
                            foreach (HANG h in hang)
                            {
                                h.SLH += c.SOLUONGB;
                                if (h.SLH > 0)
                                {
                                    h.TINHTRANGH = 1;
                                }
                                db.SubmitChanges();
                            }


                        }
                    }

                }
            }             
            return ErrCode == 0;
        }

    }
}
