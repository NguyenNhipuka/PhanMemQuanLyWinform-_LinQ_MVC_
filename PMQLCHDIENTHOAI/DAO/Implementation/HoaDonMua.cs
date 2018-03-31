using DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;

namespace DAO.Implementation
{
    public class HoaDonMua
    {
        private static HoaDonMua instance;

        public HoaDonMua()
        {
        }

        public static HoaDonMua Instance
        {
            get
            {
                if (instance == null)
                    instance = new HoaDonMua();
                return instance;
            }
        }

        ///Thêm hóa đơn mua
        public bool themHDM(HDM h,List<CTHDM>ds,out int? ErrCode, out string ErrMsg, out string mahd)
        {
            ErrCode = 1;
            ErrMsg = "Thêm không thành công";
            mahd = "";
            using (QLCHDTDataContext db = new QLCHDTDataContext())
            {

                db.sp_HDM_Add(h.MANV, h.MANCC, h.HANNOHDM, h.GTGTHDM, h.TONGTIEN, h.TINHTRANGHDM, h.TINHTRANGTTOAN, ref ErrCode, ref ErrMsg,ref mahd);
                db.SubmitChanges();
                if(ErrCode == 0 && mahd !="")
                {
                  
                    for(int i = 0; i < ds.Count; i++)
                    {
                        CTHDM c = new CTHDM();
                        c = ds[i];
                        db.sp_CTHDM_Add(c.MAH, mahd, c.DONGIAHM, c.TGBAOHANH, c.SLUONGHM, c.THANHTIEN,c.TRANGTHAI);
                        db.SubmitChanges();
                    }
                    if(h.TINHTRANGTTOAN == true)
                    {
                        PHIEUCHI p = new PHIEUCHI();
                        p.MAHDM = mahd;p.DIENGIAI = "Thanh toán cho hóa đơn " + mahd;
                        p.MANV = h.MANV;p.TIEN = h.TONGTIEN;
                        p.NGAYCHI = DateTime.Now.Date;
                        lapPhieuChi(p);
                    }
                }
            }
            return ErrCode == 0;
        }
        ///Hiện danh sách hóa đơn mua excel
        public DataTable getAllHDM()
        {

            DataTable data = new DataTable();
            using (QLCHDTDataContext db = new QLCHDTDataContext())
            {
                // List<sp_HDM_GetlistResult> ds = db.sp_HDM_Getlist().ToList();
                List<sp_HDM_GetlistDetailResult> ds = db.sp_HDM_GetlistDetail().ToList();
                data = ConvertToDataTable(ds);
            }
            return data;
        }
        ///Hiện danh sách hóa đơn mua
      
        ///Lấy danh sách hóa đơn mua nợ
        public DataTable getAllHDMNo(Boolean tinhtrang)
        {

            DataTable data = new DataTable();
            using (QLCHDTDataContext db = new QLCHDTDataContext())
            {
                List<sp_HDM_Getlist_TTThanhToanResult> ds = db.sp_HDM_Getlist_TTThanhToan(tinhtrang).ToList();
                data = ConvertToDataTable(ds);
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

        ///Hiện danh sách thông tin cua một hóa đơn theo mã cho form thông tin hóa đơn
        public sp_HDM_GetlistInfo_ByMaHDMResult getHDMByKey(string mahdmua)
        {

            sp_HDM_GetlistInfo_ByMaHDMResult tb = new sp_HDM_GetlistInfo_ByMaHDMResult();
            tb = null;
            using (QLCHDTDataContext db = new QLCHDTDataContext())
            {
                List<sp_HDM_GetlistInfo_ByMaHDMResult> ds = db.sp_HDM_GetlistInfo_ByMaHDM(mahdmua).ToList();
                if (ds.Count > 0) tb = ds[0];
            }
            return tb;
        }

        ///Hiện danh sách thông tin cua các PHIẾU chi  theo mã hóa đơn
        public DataTable getPCHDMByKey(string mahdmua)
        {

            DataTable data = new DataTable();
            using (QLCHDTDataContext db = new QLCHDTDataContext())
            {
                List<sp_PHIEUCHI_GetlistInfo_ByMaHDMResult> ds = db.sp_PHIEUCHI_GetlistInfo_ByMaHDM(mahdmua).ToList();
                data = ConvertToDataTable(ds);
            }
            return data;
        }

        ///Hiện danh sách thông tin cua các PHIẾU chi 
        public DataTable getPCHDM()
        {

            DataTable data = new DataTable();
            using (QLCHDTDataContext db = new QLCHDTDataContext())
            {
                List<sp_PHIEUCHI_GetlistResult> ds = db.sp_PHIEUCHI_Getlist().ToList();
                data = ConvertToDataTable(ds);
            }
            return data;
        }

        ///Lấy tổng tiền đã thanh toán theo hóa đơn
        public double getTongTienDaThanhToan(string mahd)
        {

            double tongtien = 0;
            using (QLCHDTDataContext db = new QLCHDTDataContext())
            {
                double? s = db.F_PHIEUCHI_Sum_ByMaHDM(mahd);
              
               //double? s = db.PHIEUCHIs.Where(t => t.MAHDM == mahd).Sum(t => t.TIEN);
                tongtien = s ?? 0;
               // var sum = (from n in db.PHIEUCHIs where n.MAHDM==mahd select n.TIEN).Sum();
               
                
            }

            return tongtien;
        }

        ///Lấy tổng tiền đã thanh toán theo hóa đơn
        public double getTongTienHDM(string mahd)
        {

            double tongtien = 0;
            using (QLCHDTDataContext db = new QLCHDTDataContext())
            {
                double? tong = (from hd in db.HDMs
                            where hd.MAHDM == mahd
                            select hd.TONGTIEN).Sum();

                tongtien = tong ?? 0;
            }
            return tongtien;
        }
        ///Hiện danh sách thông tin cua các chi tiết hóa đơn  theo mã hóa đơn
        public DataTable getCTHDMByKey(string mahdmua)
        {

            DataTable data = new DataTable();
            using (QLCHDTDataContext db = new QLCHDTDataContext())
            {
                List<sp_CTHDM_GetlistInfo_ByMaHDMResult> ds = db.sp_CTHDM_GetlistInfo_ByMaHDM(mahdmua).ToList();
                data = ConvertToDataTable(ds);
            }
            return data;
        }
        ///Cập nhật trình trạng hóa đơn mua
        public bool capNhatTTHDM(string mahdmua,Byte trinhtranghdm, out int? ErrCode, out string ErrMsg)
        {
            ErrCode = 1;
            ErrMsg = "Cập nhật không thành công";
            using (QLCHDTDataContext db = new QLCHDTDataContext())
            {
                db.sp_HDM_Update_TinhTrangHDM(mahdmua, trinhtranghdm, ref ErrCode, ref ErrMsg);
                db.SubmitChanges();
                if (ErrCode == 0 && trinhtranghdm ==1)
                {
                    var ds = from ct in db.CTHDMs where ct.MAHDM == mahdmua select ct;
                    foreach(CTHDM c in ds)
                    {                     
                            var hang = from h in db.HANGs where h.MAH == c.MAH select h;
                            foreach (HANG h in hang)
                            {
                                h.SLH += c.SLUONGHM;
                                c.TRANGTHAI = 1;
                                
                            }
                            db.SubmitChanges();                      
                        
                    }
                }
                else if(ErrCode == 0 && trinhtranghdm == 2)
                {
                    var ds = from ct in db.CTHDMs where ct.MAHDM == mahdmua select ct;
                    foreach (CTHDM c in ds)
                    {
                        c.TRANGTHAI = 2;
                        db.SubmitChanges();

                    }
                }
            }
            return ErrCode == 0;
        }

        //lập phiếu chi
        public string lapPhieuChi(PHIEUCHI p)
        {
            string ErrMsg = "";

            using (QLCHDTDataContext db = new QLCHDTDataContext())
            {
                db.sp_PHIEUCHI_Add(p.DIENGIAI, p.MAHDM, p.MANV, p.TIEN, ref ErrMsg);
            }
            return ErrMsg;
        }


    }
}
