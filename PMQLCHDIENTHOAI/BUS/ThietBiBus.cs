using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DTO;
using DAO.Implementation;
using PMQLCHDIENTHOAI.Utilities;
using Bunifu.Framework.UI;
using MetroFramework.Controls;
using System.Data;
using System.Data.SqlClient;
using System.ComponentModel;
using PMQLCHDIENTHOAI;
namespace BUS
{
    public class ThietBiBus
    {
        private static ThietBiBus instance;

        public static ThietBiBus Instance
        {
            get
            {
                if (instance == null)
                    instance = new ThietBiBus();
                return instance;
            }
            private set
            {
                ThietBiBus.instance = value;
            }
        }


        //load tất cả thiết bị lên 
        public Boolean LoadDSTBE(DataGridView gv)
        {
            DataTable table = ThietBiDAO.Instance.getAllTBExcel();
            if (table != null)
            {
                gv.DataSource = null;
                gv.DataSource = table;
            }
            return true;
        }

        //load tất cả loại thiết bị lên combobox thêm hóa đơn theo mã nhà cung cấp
        public Boolean LoadTBIDName(MetroComboBox cb,int mancc)
        {
            DataTable table = ThietBiDAO.Instance.getAllTBIDName(mancc);
            if (table != null)
            {
                cb.DataSource = table;
                cb.DisplayMember = "TEN";
                cb.ValueMember = "MAH";
            }
            return true;
        }

        //load tất cả loại thiết bị lên combobox thêm hóa đơn bán
        public Boolean LoadTBIDNameHDB(MetroComboBox cb)
        {
            DataTable table = ThietBiDAO.Instance.getAllTBIDNameHDB();
            if (table != null)
            {
                cb.DataSource = table;
                cb.DisplayMember = "TEN";
                cb.ValueMember = "MAH";
                cb.SelectedIndex = 0;
            }
            return true;
        }
        //load ds thông tin tất cả các thiết bị
        public Boolean LoadDSTB(DataGridView gv)
        {
            DataTable t = ThietBiDAO.Instance.getAllTB();
            gv.DataSource = null;
            gv.Rows.Clear();
            gv.DataSource = t;
            return true;
        }

        //load ds thông tin tất cả các thiết bị
        public DataTable getAllDSTB_EX()
        {
            return ThietBiDAO.Instance.getAllTB();
        }
        public Boolean LoadTBByKey(string matb,BunifuMaterialTextbox mahang,BunifuMaterialTextbox ten, MetroComboBox loai,
            MetroComboBox dvt, BunifuMaterialTextbox nsx, MetroComboBox ncc,RichTextBox mota,
            NumericUpDown giaban, NumericUpDown giamua, NumericUpDown baohanh, NumericUpDown tonmax,
            NumericUpDown tonmin, NumericUpDown khuyenmai, NumericUpDown sluong, MetroComboBox tinhtrang)
        {
            sp_TB_Getlist_ByKeyResult tb = null;
                tb= ThietBiDAO.Instance.getTBByKey(matb);
            if(tb !=null)
            {
                mahang.Text = tb.MAH;
                ten.Text = tb.TENH;
                LoadLoaiTB(loai);
                loai.SelectedValue = Convert.ToInt32(tb.MALOAI);
                LoadDVTTB(dvt);
                dvt.SelectedValue = Convert.ToInt32(tb.MADVT);
                nsx.Text = tb.NSX;
                NCCBus.Instance.LoadNCCName(ncc);
                ncc.SelectedValue = Convert.ToInt32(tb.MANCC);
                mota.Text = tb.MOTAH;
                giaban.Value =Convert.ToInt32(tb.DONGIAB.ToString());
                giamua.Value = Convert.ToInt32(tb.DONGIAM.ToString());
                baohanh.Value = Convert.ToInt32(tb.TGBH);
                tonmax.Value = Convert.ToInt32(tb.TON_MAX);
                tonmin.Value = Convert.ToInt32(tb.TON_MIN);
                khuyenmai.Value = Convert.ToInt32(tb.KHUYENMAI_H *100);
                sluong.Value = Convert.ToInt32(tb.SLH);
                tinhtrang.SelectedIndex= Convert.ToInt32(tb.TINHTRANGH);

            }
            else
            {
                CustomMessageBox.show("Kết quả", "Thiết bị không tồn tại",false);
                return false;
            }
            return true;
        }
        public Boolean AddTBByKeyToHDM(string matb,DataGridView gv,double giamua,double thanhtien,int soluong)
        {
            sp_TB_Getlist_ByKeyHDMResult tb = ThietBiDAO.Instance.getTBByKeyToHDM(matb);
            if (tb != null)
            {              
                //--stt,mahang ,ten hang,s luong,loại,don vi tinh,gia mua,tgbh,thanhtien
                gv.Rows.Add(gv.RowCount,tb.MAH,tb.TENH,soluong,tb.TENLOAI,tb.TENDVT,giamua,tb.TGBH,thanhtien);
                return true;
            }
            return false;
        }


        public Boolean AddTBByKeyToHDB(string matb, DataGridView gv, double giab,double khuyenmai, int soluong,double thanhtien)
        {
            sp_TB_Getlist_ByKeyHDMResult tb = ThietBiDAO.Instance.getTBByKeyToHDM(matb);
            if (tb != null)
            {
                //--stt,mahang ,ten hang,s luong,loại,don vi tinh,gia mua,tgbh,thanhtien
                gv.Rows.Add(gv.RowCount+1, tb.MAH, tb.TENH, soluong, tb.TENDVT, giab, tb.TGBH, khuyenmai, thanhtien);
                return true;
            }
            return false;
        }

        public Boolean AddThieiBi(int MALOAI,int MADVT,int MANCC,string TENH,
            int SLH,double DONGIAM,double DONGIAB,int TGBH,
                   string NSX,int TINHTRANGH,string MOTAH, int TON_MAX,int TON_MIN,double KHUYENMAI_H )
        {
            HANG  h = new HANG();
            h.MALOAI = MALOAI;h.MADVT = MADVT;h.MANCC = MANCC;
            h.TENH = TENH;h.SLH = SLH;h.DONGIAM = DONGIAM; h.DONGIAB = DONGIAB;
            h.TGBH = TGBH; h.NSX = NSX; h.TINHTRANGH =Convert.ToByte(TINHTRANGH); h.MOTAH = MOTAH;
            CT_HANG ct = new CT_HANG();
            ct.TON_MAX = TON_MAX;ct.TON_MIN = TON_MIN;ct.KHUYENMAI_H=KHUYENMAI_H;
            ThietBiDAO.Instance.addThietBi(h, ct, out int? ErrCode,out string ErrMsg);
            CustomMessageBox.show("Kết quả", ErrMsg, ErrCode==0);
            return ErrCode == 0;
        }

        public Boolean DeleteThietBi(string matb)
        {
            Boolean result = ThietBiDAO.Instance.deleteThietBi(matb, out int? ErrCode, out string ErrMsg);
            CustomMessageBox.show("Kết quả", ErrMsg, result);
            return result;
        }

        public Boolean UpdateTB(string MATB,int MALOAI, int MADVT, int MANCC, string TENH,
            int SLH, double DONGIAM, double DONGIAB, int TGBH,
                   string NSX, int TINHTRANGH, string MOTAH, int TON_MAX, int TON_MIN, double KHUYENMAI_H)
        {
            HANG h = new HANG();
            h.MAH = MATB;
            h.MALOAI = MALOAI; h.MADVT = MADVT; h.MANCC = MANCC;
            h.TENH = TENH; h.SLH = SLH; h.DONGIAM = DONGIAM; h.DONGIAB = DONGIAB;
            h.TGBH = TGBH; h.NSX = NSX; h.TINHTRANGH = Convert.ToByte(TINHTRANGH); h.MOTAH = MOTAH;
            CT_HANG ct = new CT_HANG();
            ct.TON_MAX = TON_MAX; ct.TON_MIN = TON_MIN; ct.KHUYENMAI_H = KHUYENMAI_H;
            Boolean result = ThietBiDAO.Instance.updeteThietBi(h, ct, out int? ErrCode, out string ErrMsg);
            CustomMessageBox.show("Kết quả", ErrMsg, result);
            return result;
        }

        //load ds thông tin tất cả các thiết bị với số lượng đã bán theo thời gian,tình trạng
        public Boolean LoadDSTBSoLuongBanNgay(DateTime dfrom,DateTime dto,Byte tinhtrang,DataGridView gv)
        {
            DataTable t = ThietBiDAO.Instance.getTBSOLuongBan_Ngay(dfrom,dto,tinhtrang);
            gv.DataSource = null;
            gv.DataSource = t;
            return true;
        }


        //load ds thông tin tất cả các thiết bị với số lượng đã bán
        public Boolean LoadALLDSTBSoLuongBanNgay(DataGridView gv)
        {
            DataTable t = ThietBiDAO.Instance.getAllTBSOLuongBan_Ngay();
            gv.DataSource = null;
            gv.DataSource = t;
            return true;
        }

        //load ds thông tin tất cả các thiết bị với số lượng đã nhập theo thời gian,tình trạng
        public Boolean LoadDSTBSoLuongMuaNgay(DateTime dfrom, DateTime dto, Byte tinhtrang, DataGridView gv)
        {
            DataTable t = ThietBiDAO.Instance.getTBSOLuongMua_Ngay(dfrom, dto, tinhtrang);
            gv.DataSource = null;
            gv.DataSource = t;
            return true;
        }

        //load ds thông tin tất cả các thiết bị với số lượng đã nhập 
        public Boolean LoadALLDSTBSoLuongMuaNgay(DataGridView gv)
        {
            DataTable t = ThietBiDAO.Instance.getAllTBSOLuongMua_Ngay();
            gv.DataSource = null;
            gv.DataSource = t;
            return true;
        }
        //load ds thông tin tất cả các thiết bị : sắp hết hàng
        public Boolean LoadDSTBSapHet( DataGridView gv)
        {
            DataTable t = ThietBiDAO.Instance.getTBSapHet();
            gv.DataSource = null;
            gv.DataSource = t;
            return true;
        }


        #region ++ ĐƠN VỊ TÍNH
        //load tất cả đơN vị tính  lên combobox
        public Boolean LoadDVTTB(MetroComboBox cb)
        {
            DataTable table = ThietBiDAO.Instance.getAllDVTTB();
            if (table != null)
            {
                cb.DataSource = table;
                cb.DisplayMember = "Tên đơn vị tính";
                cb.ValueMember = "Mã đơn vị tính";
            }
            return true;
        }


        //load tất cả đơN vị tính   thiết bị lên combobox
        public Boolean LoaddDVTTB(DataGridView gv)
        {
            DataTable table = ThietBiDAO.Instance.getAllDVTTB();
            if (table != null)
            {
                gv.DataSource = null;
                gv.DataSource = table;
            }
            return true;
        }


        public Boolean AddDVTTB(string tendonvi)
        {
            Boolean result = ThietBiDAO.Instance.addDVTThietBi(tendonvi, out int? ErrCode, out string ErrMsg);
            CustomMessageBox.show("Kết quả", ErrMsg, result);
            return result;
        }

        public Boolean UpdateDVTTB(int madonvi, string tendonvi)
        {
            Boolean result = ThietBiDAO.Instance.updateDVTThietBi(madonvi, tendonvi, out int? ErrCode, out string ErrMsg);
            CustomMessageBox.show("Kết quả", ErrMsg, result);
            return result;
        }

        public Boolean DeleteDVTTB(int madonvi)
        {
            Boolean result = ThietBiDAO.Instance.deleteDVTThietBi(madonvi, out int? ErrCode, out string ErrMsg);
            CustomMessageBox.show("Kết quả", ErrMsg, result);
            return result;
        }
        #endregion ++ ĐƠN VỊ TÍNH

        #region ++ LOẠI THIẾT BỊ
        //load tất cả loại thiết bị lên combobox
        public Boolean LoadLoaiTB(MetroComboBox cb)
        {
            DataTable table = ThietBiDAO.Instance.getAllLoaiTB();
            if (table != null)
            {
                cb.DataSource = table;
                cb.DisplayMember = "Tên loại";
                cb.ValueMember = "Mã loại";
            }
            return true;
        }

        //load tất cả loại thiết bị lên combobox
        public Boolean LoadLoaiTB(DataGridView gv)
        {
            DataTable table = ThietBiDAO.Instance.getAllLoaiTB();
            if (table != null)
            {
                gv.DataSource = null;
                gv.DataSource = table;
            }
            return true;
        }


        public Boolean AddLoaiTB(string tenloai)
        {
            Boolean result = ThietBiDAO.Instance.addLoaiThietBi(tenloai, out int? ErrCode, out string ErrMsg);
            CustomMessageBox.show("Kết quả", ErrMsg, result);
            return result;
        }

        public Boolean UpdateLoaiTB(int maloai, string tenloai)
        {
            Boolean result = ThietBiDAO.Instance.updateLoaiThietBi(maloai, tenloai, out int? ErrCode, out string ErrMsg);
            CustomMessageBox.show("Kết quả", ErrMsg, result);
            return result;
        }

        public Boolean DeleteLoaiTb(int maloai)
        {
            Boolean result = ThietBiDAO.Instance.deleteLoaiThietBi(maloai, out int? ErrCode, out string ErrMsg);
            CustomMessageBox.show("Kết quả", ErrMsg, result);
            return result;
        }
        #endregion ++ LOẠI THIẾT BỊ
    }
}
