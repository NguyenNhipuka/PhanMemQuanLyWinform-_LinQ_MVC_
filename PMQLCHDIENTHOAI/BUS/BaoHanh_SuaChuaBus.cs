using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DTO;
using DAO.Implementation;
using PMQLCHDIENTHOAI.Utilities;
using Bunifu.Framework.UI;
using MetroFramework.Controls;
using System.Data;
using System.Data.SqlClient;
using System.ComponentModel;
using PMQLCHDIENTHOAI;
using System.Windows.Forms;

namespace BUS
{
    public class BaoHanh_SuaChuaBus
    {
        private static BaoHanh_SuaChuaBus instance;

        public static BaoHanh_SuaChuaBus Instance
        {
            get
            {
                if (instance == null)
                    instance = new BaoHanh_SuaChuaBus();
                return instance;
            }
            private set
            {
                BaoHanh_SuaChuaBus.instance = value;
            }
        }


        //load tất cả loại thiết bị lên combobox thêm hóa đơn bán 
        public Boolean LoadTB_HDB(MetroComboBox cb, string mahd)
        {
            DataTable table = BaoHanh_SuaChuaDAO.Instance.getAll_TB_HDB_PBH_MHD(mahd);
            cb.DataSource = null;
            cb.Items.Clear();
            if (table.Rows.Count > 0)
            {
                try
                {
                    cb.DataSource = table;
                    cb.DisplayMember = table.Columns[2].ColumnName;
                    cb.ValueMember = table.Columns[1].ColumnName;
                    cb.SelectedIndex = 0;
                }
                catch { }
                return true;
            }
            else
            {
                CustomMessageBox.show("Thông báo", "Không tồn tại hóa đơn này", false);
                return false;
            }


        }


        //load tất cả loại thiết bị lên combobox thêm hóa đơn bán 
        public Boolean LoadTB_HDB_MH(MetroComboBox cb, string mahd, BunifuMaterialTextbox slhang,
           BunifuMaterialTextbox sldalap, BunifuMaterialTextbox ngayban,
           BunifuMaterialTextbox tgbh)
        {
            sp_TB_HDB_PBH_GetlistMAHDB_MHResult tb = BaoHanh_SuaChuaDAO.Instance.getAll_TB_HDB_PBH_MH(mahd, cb.SelectedValue.ToString());
            if (tb != null)
            {
                slhang.Text = tb.SOLUONGB.ToString();
                sldalap.Text = tb.SLL.ToString();
                ngayban.Text = tb.NGAYBAN.ToString();
                tgbh.Text = tb.TGBH.ToString();
                return true;

            }
            slhang.Text = sldalap.Text = "0";
            ngayban.Text = string.Empty;
            tgbh.Text = "0";
            return false;
        }

        //load ds thông tin tất cả các thiết bị dã bảo hành trong phiếu bảo hành theo hóa đơn bán
        public Boolean LoadDSTB(string mahd, DataGridView gv)
        {
            DataTable t = BaoHanh_SuaChuaDAO.Instance.getAll_TB_PBH(mahd);
            gv.DataSource = null;
            gv.Rows.Clear();
            gv.DataSource = t;
            return true;
        }
        //load ds phiếu bảo hành
        public Boolean LoadDSPBH(DataGridView gv)
        {
            DataTable t = BaoHanh_SuaChuaDAO.Instance.getAllPBH();
            gv.DataSource = null;
            gv.Rows.Clear();
            gv.DataSource = t;
            return t.Rows.Count > 0;
        }
        ////THEM PHIÊU BẢO HÀNH
        public Boolean addPBH(string mah, string mahd, string seri,
           string manv)
        {
            PHIEUBAOHANH p = new PHIEUBAOHANH();
            p.MAH = mah;
            p.MAHDB = mahd;
            p.MANV = manv;
            p.SERIALBH = seri;
            Boolean result = BaoHanh_SuaChuaDAO.Instance.addThietBi(p, out int? ErrCode, out string ErrMsg);
            CustomMessageBox.show("Kết quả", ErrMsg, result);
            return result;
        }

        ////===================================
        ///load thông tin của phiếu bảo hành cho hóa đơn bảo hành
        public Boolean LoadPBH_ID(int maphieu, BunifuMaterialTextbox ma, BunifuMaterialTextbox tentb,
            BunifuMaterialTextbox tinhtrang, BunifuMaterialTextbox serial, BunifuMaterialTextbox makh,
            BunifuMaterialTextbox tenkh, BunifuMaterialTextbox sdt, Label tinhtrangpbh)
        {
            sp_PHIEUBAOHANH_GetlistByIDResult t = BaoHanh_SuaChuaDAO.Instance.getInfo_PBH_IDPBH(maphieu);
            if (t != null)
            {
                ma.Text = maphieu.ToString();
                tentb.Text = t.TENH;
                tinhtrang.Text = t.Tình_trạng_bảo_hành;
                serial.Text = t.SERIALBH;
                makh.Text = t.MAKH.ToString();
                tenkh.Text = t.TENKHB;
                sdt.Text = t.SDTKHB;
                tinhtrangpbh.Text = t.TINHTRANGPBH.ToString();
                return true;

            }

            return false;
        }


        //load ds bảo hành theo mã phiếu bảo hành
        public Boolean LoadDSBH_IDPBH(int maphieu, DataGridView gv)
        {
            DataTable t = BaoHanh_SuaChuaDAO.Instance.getBH_ByIDPBH(maphieu);
            gv.DataSource = null;
            gv.Rows.Clear();
            gv.DataSource = t;
            return t.Rows.Count > 0;
        }
        ///load thông tin của phiếu bảo hành cho hóa đơn bảo hành
        public void UpdateBH(int mabh, Byte tinhtrang, string nguyennhan, DateTime ngaygiao)
        {
            BAOHANH b = new BAOHANH();
            b.IDBH = mabh;
            b.TINHTRANGBH = tinhtrang;
            b.NGUYENNHAN = nguyennhan;
            b.NGAYGIAOBH = ngaygiao;
            BaoHanh_SuaChuaDAO.Instance.updateBaoHanh(b);
        }

        //lap bao hanh
        public Boolean addBaoHanh(out int mhd, int mapbh, string nguyennhan, string manv, DateTime ngaygiao, string sdt)
        {
            BAOHANH b = new BAOHANH();
            b.IDPBH = mapbh;
            b.NGUYENNHAN = nguyennhan;
            b.MANV = manv;
            b.NGAYGIAOBH = ngaygiao;
            b.SDTNHAN = sdt;
            mhd = 0;
            BaoHanh_SuaChuaDAO.Instance.addBH(b, out int? ErrCode, out string ErrMsg, out int mahdnew);
            CustomMessageBox.show("Kết quả", ErrMsg, ErrCode == 0);
            mhd = mahdnew;
            return ErrCode == 0;
        }

        //load ds  bảo hành
        public Boolean LoadDSBH(DataGridView gv)
        {
            DataTable t = BaoHanh_SuaChuaDAO.Instance.getAllBH();
            gv.DataSource = null;
            gv.Rows.Clear();
            gv.DataSource = t;
            return t.Rows.Count > 0;
        }
        ///load các loại tình trạng hdsc
        public Boolean loadTinhTrangHDSCThem(ComboBox cb)
        {
            DataTable table = new DataTable();
            table.Columns.Add("MATT");
            table.Columns.Add("TEN");
            table.Rows.Add(0, "Đang xử lý");
            table.Rows.Add(2, "Đã giao");
            cb.DataSource = table;
            cb.DisplayMember = "TEN";
            cb.ValueMember = "MATT";
            return true;
        }
        ///load các loại tình trạng hdsc
        public Boolean loadTinhTrangHDSCThong(ComboBox cb)
        {
            DataTable table = new DataTable();
            table.Columns.Add("MATT");
            table.Columns.Add("TEN");
            table.Rows.Add(0, "Đang xử lý");
            table.Rows.Add(1, "Đã sửa");
            table.Rows.Add(2, "Đã giao");
            cb.DataSource = table;
            cb.DisplayMember = "TEN";
            cb.ValueMember = "MATT";
            return true;
        }
        ///load các loại tình trạng hdsc
        public Boolean loadTinhTrangTBSC(ComboBox cb)
        {
            DataTable table = new DataTable();
            table.Columns.Add("MATT");
            table.Columns.Add("TEN");
            table.Rows.Add(0, "Đang xử lý");
            table.Rows.Add(1, "Đã sửa");
            cb.DataSource = table;
            cb.DisplayMember = "TEN";
            cb.ValueMember = "MATT";
            return true;
        }

        ///lập sữa chữa
        public bool addHDSC(out string mahdmoi, string manv, int makh, string tenkh, string sdt,
            DateTime ngaytra, double tongchiphi, byte trinhtrang, DataGridView gv)
        {
            SUACHUA s = new SUACHUA();

            s.MAKH = makh;
            s.MANV = manv;
            s.TENKHSC = tenkh;
            s.SDTKHSC = sdt;
            s.NGAYGIAOSC = ngaytra;
            s.TONGCHIPHISC = tongchiphi;
            s.TINHTRANGSC = trinhtrang;
            List<CTHDSC> ds = new List<CTHDSC>();
            CTHDSC c;
            foreach (DataGridViewRow r in gv.Rows)
            {
                c = new CTHDSC();
                c.MAH = r.Cells["Column2"].Value.ToString();
                c.TENTBSC = r.Cells["Column3"].Value.ToString();
                c.LOISC = r.Cells["Column5"].Value.ToString();
                c.MOTASC = r.Cells["Column4"].Value.ToString();
                c.CHIPHISC = Double.Parse(r.Cells["Column6"].Value.ToString());
                c.TINHTRANGCTSC = (r.Cells["Column7"].Value.ToString() == "0") ? false : true;
                ds.Add(c);
            }
            mahdmoi = "0000000000";
            BaoHanh_SuaChuaDAO.Instance.addSC(s, ds, out int? ErrCode, out string ErrMsg, out string mahd);
            if (ErrCode == 0)
            {
                gv.DataSource = null;
                gv.Rows.Clear();
            }
            mahdmoi = mahd;
            CustomMessageBox.show("Thông báo", ErrMsg, ErrCode == 0);
            return ErrCode == 0;
        }

        ///them thiet bi b=vao hoa don sua chua
        public Boolean AddTBByKeyToHDSC(string matb, DataGridView gv, string mota, string loi, int tinhtrang, double chiphi)
        {
            sp_TB_Getlist_ByKeyHDMResult tb = ThietBiDAO.Instance.getTBByKeyToHDM(matb);
            if (tb != null)
            {

                gv.Rows.Add(gv.RowCount + 1, 0, tb.MAH, tb.TENH, mota, loi, chiphi, tinhtrang, tinhtrang, "S000000000");
                return true;
            }
            return false;
        }


        ///cập nhật sửa chữa
        public Boolean updateSC(string masc, string tenkh, string sdt,
            DateTime ngaytra, double tongchiphi, byte trinhtrangDSC)
        {
            SUACHUA s = new SUACHUA();
            s.MASC = masc;
            s.TENKHSC = tenkh;
            s.SDTKHSC = sdt;
            s.NGAYGIAOSC = ngaytra;
            s.TONGCHIPHISC = tongchiphi;
            s.TINHTRANGSC = trinhtrangDSC;

            BaoHanh_SuaChuaDAO.Instance.UpdateSC(s, out int? ErrCode, out string ErrMsg);
            CustomMessageBox.show("Thông báo", ErrMsg, ErrCode == 0);
            return ErrCode == 0;
        }

        public Boolean updateTBSC(int sophieu, string mota, string loi,
           double chiphi, Boolean tinhtrangTB)
        {

            CTHDSC c = new CTHDSC();

            c.SOHIEU = sophieu;
            c.MOTASC = mota;
            c.LOISC = loi;
            c.CHIPHISC = chiphi;
            c.TINHTRANGCTSC = tinhtrangTB;
            BaoHanh_SuaChuaDAO.Instance.UpdateTBSC(c);
            CustomMessageBox.show("Thông báo", "Cập nhật thành công", true);
            return true;
        }


        public void loadHDSC_ByID(string masc, BunifuMaterialTextbox ten, BunifuMaterialTextbox sdt, ComboBox cbkh, ComboBox tinhtrangsc, MetroDateTime ngaynhan, MetroDateTime tra,
            BunifuMaterialTextbox tongchiphi, DataGridView gv)
        {
            sp_SUACHUA_GetlistByIDResult tbsc = BaoHanh_SuaChuaDAO.Instance.getHDSCByID(masc);
            if (tbsc == null)
            {
                CustomMessageBox.show("Thông báo", "Không tồn tại hóa đơn này", false);
                return;
            }
            if (tbsc.Mã_khách_hàng != 0)
                cbkh.SelectedValue = tbsc.Mã_khách_hàng.ToString();
            else cbkh.Enabled = false;
            ten.Text = tbsc.Tên_khách_hàng;
            sdt.Text = tbsc.SDT;
            tongchiphi.Text = tbsc.Tổng_chi_phí.ToString();
            tinhtrangsc.SelectedValue = tbsc.Tình_trạng;
            if (tbsc.Tình_trạng == 2) { tinhtrangsc.Enabled = false; }
            else tinhtrangsc.Enabled = true;
            tra.Value = DateTime.Parse(tbsc.Ngày_giao.ToString());

            List<sp_CTHDSC_GetlistByIDResult> ds = BaoHanh_SuaChuaDAO.Instance.getCTSCID(masc);
            ngaynhan.Value = DateTime.Parse(tbsc.Ngày_nhận.ToString());
            gv.DataSource = null;
            gv.Rows.Clear();
            foreach (sp_CTHDSC_GetlistByIDResult tb in ds)
            {
                gv.Rows.Add(tb.STT, tb.Số_hiệu, tb.Mã_hàng, tb.Tên_hàng, tb.Mô_tả, tb.Lỗi, tb.Chi_phí, tb.Tình_trạng, tb.TT, tb.Mã_sửa_chữa);

            }

        }

        //load ds  sua chua
        public Boolean LoadDSSC(DataGridView gv)
        {
            DataTable t = BaoHanh_SuaChuaDAO.Instance.getAllSC();
            gv.DataSource = null;
            gv.Rows.Clear();
            gv.DataSource = t;
            return t.Rows.Count > 0;
        }

        //load ds chi tiet sua chua theo ma sua chua
        public Boolean LoadDSCTSC(string masc,DataGridView gv)
        {
            DataTable t = BaoHanh_SuaChuaDAO.Instance.getAllCTSC(masc);
            gv.DataSource = null;
            gv.Rows.Clear();
            gv.DataSource = t;
            return t.Rows.Count > 0;
        }


        public void update_PBH()
        {
            BaoHanh_SuaChuaDAO.Instance.updatePBH();
        }
    }
        
}
