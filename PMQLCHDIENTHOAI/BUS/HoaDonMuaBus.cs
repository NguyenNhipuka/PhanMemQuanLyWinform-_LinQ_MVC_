using DAO.Implementation;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Bunifu.Framework.UI;
using MetroFramework.Controls;
using PMQLCHDIENTHOAI;
using System.Data;

namespace BUS
{
    public class HoaDonMuaBus
    {
        private static HoaDonMuaBus instance;

        public static HoaDonMuaBus Instance
        {
            get
            {
                if (instance == null)
                    instance = new HoaDonMuaBus();
                return instance;
            }
            private set
            {
                HoaDonMuaBus.instance = value;
            }
        }


        ///thêm thiết bị vào hóa đơn
        ///thêm hóa đơn
        ///*
        ///*/
        public Boolean addHDM(out string mahdmoi,string manv, ComboBox cbmancc, ComboBox cbtinhtrangtt, ComboBox cbtthoadon,
        ComboBox cbgtgt, BunifuDatepicker phanno, BunifuMaterialTextbox txttongtien, DataGridView data)
        {
            int mancc = Convert.ToInt32(cbmancc.SelectedValue.ToString());
            int ttthanhtoan = Convert.ToInt32(cbtinhtrangtt.SelectedValue.ToString());

            int tthoadon = Convert.ToInt32(cbtthoadon.SelectedValue.ToString());
            double gtgt = Convert.ToDouble(cbgtgt.SelectedValue.ToString()) / 100;
            DateTime hanno = phanno.Value.Date;
            double tongtien = Double.Parse(txttongtien.Text.ToString());
            HDM h = new HDM();
            h.MANCC = mancc;
            h.MANV = manv;
            h.HANNOHDM = hanno;
            h.GTGTHDM = gtgt;
            h.TINHTRANGHDM = Convert.ToByte(tthoadon);
            h.TINHTRANGTTOAN = (ttthanhtoan == 1) ? true : false;
            h.TONGTIEN = tongtien;
            List<CTHDM> ds = new List<CTHDM>();
            CTHDM c;
            string tam = "";
            for (int i = 0; i < data.Rows.Count; i++)
            {
                c = new CTHDM();
                c.MAH = data.Rows[i].Cells[1].Value.ToString();
                c.SLUONGHM = Convert.ToInt32(data.Rows[i].Cells[3].Value.ToString());
                c.TGBAOHANH = Convert.ToInt32(data.Rows[i].Cells[7].Value.ToString());
                c.THANHTIEN = double.Parse(data.Rows[i].Cells[8].Value.ToString());
                c.DONGIAHM = double.Parse(data.Rows[i].Cells[6].Value.ToString());
                c.TRANGTHAI = Convert.ToByte(ttthanhtoan);
                tam += "_" + c.MAH;
                ds.Add(c);
            }
            mahdmoi = "0000000000";
            Boolean result = HoaDonMua.Instance.themHDM(h, ds, out int? ErrCode, out string ErrMsg, out string mahdm);
            mahdmoi = mahdm;
            CustomMessageBox.show("Kết quả" +mahdmoi, ErrMsg, result);
            return result;
        }
        ///cập nhật hóa đơn
        ///load thiet bị theo mã lên form thêm hóa đơn mua
        public Boolean loadTBByKey(string matb,BunifuMaterialTextbox tonkho, BunifuMaterialTextbox gia)
        {
            sp_TB_Getlist_ByKeyResult tb = ThietBiDAO.Instance.getTBByKey(matb);
            if(tb == null) return false;
            tonkho.Text = tb.SLH.ToString();
            gia.Text = tb.DONGIAM.ToString();
            return true;
        }
        ///load thiet bị theo mã lên form thêm hóa đơn bán
        public Boolean loadTBByKeyHDB(string matb, BunifuMaterialTextbox tonkho, BunifuMaterialTextbox gia)
        {
            sp_TB_Getlist_ByKeyResult tb = ThietBiDAO.Instance.getTBByKey(matb);
            if (tb == null) return false;
            tonkho.Text = tb.SLH.ToString();
            gia.Text = tb.DONGIAB.ToString();
            return true;
        }

        ///search hóa đơn mua theo mã

        public Boolean searchHDMByKey(string mahdm,BunifuMaterialTextbox tenncc, BunifuMaterialTextbox ttthanhtoan,
            ComboBox trangthaihd, BunifuMaterialTextbox thue, BunifuMaterialTextbox nhanvien, BunifuMaterialTextbox tongtientt,
            BunifuMaterialTextbox ngaylap, BunifuMaterialTextbox tong, DataGridView gvcthd, DataGridView gvphieuchi)
        {
            sp_HDM_GetlistInfo_ByMaHDMResult hdm = HoaDonMua.Instance.getHDMByKey(mahdm);
            if(hdm == null)
            {
                CustomMessageBox.show("Kết quả","Không tồn tại hóa đơn này" , false);
                tenncc.Text = tongtientt.Text = nhanvien.Text=
                ttthanhtoan.Text = thue.Text = string.Empty;
                tong.Text = tongtientt.Text = "0";
                gvcthd.DataSource = null;
                gvcthd.Rows.Clear();
                gvphieuchi.DataSource = null;
                gvphieuchi.Rows.Clear();
                return false;
            }
            tenncc.Text = hdm.Nhà_cung_cấp;
            ttthanhtoan.Text = hdm.Tình_trạng_thanh_toán;
            trangthaihd.SelectedValue = hdm.Tình_trạng_hóa_đơn_mua;
    
            trangthaihd.Enabled = hdm.Tình_trạng_hóa_đơn_mua == 0;

            thue.Text = hdm.Thuế.ToString();
            nhanvien.Text = hdm.Nhân_viên_phụ_trách;
            tongtientt.Text = string.Format("{0:#,##0.00}", hdm.Tổng_tiền.ToString());
            ngaylap.Text = hdm.Ngày_tạo.ToString();
            DataTable tablecthd = HoaDonMua.Instance.getCTHDMByKey(mahdm);
            gvcthd.DataSource = null;
            gvcthd.Rows.Clear();
            gvcthd.DataSource = tablecthd;
            var sum = tablecthd.AsEnumerable().Sum(x => x.Field<double>(5));
            tong.Text = sum.ToString();
            DataTable tablePhieuChi = HoaDonMua.Instance.getPCHDMByKey(mahdm);
            gvphieuchi.DataSource = null;
            gvphieuchi.Rows.Clear();
            if (tablePhieuChi != null)
            {
                gvphieuchi.DataSource = tablePhieuChi;
            }
            return true;
        }

        //cập nhật trình trạng HDM
        public Boolean updateTinhTrangHDM(string mahdm, ComboBox trangthaihd)
        {
            Byte trinhtrang = Convert.ToByte(trangthaihd.SelectedValue.ToString());
            Boolean result= HoaDonMua.Instance.capNhatTTHDM(mahdm, trinhtrang, out int ? ErrCode, out string ErrMsg);

            if(result ==true && trinhtrang !=0)
            {
                trangthaihd.Enabled = false;
            }
            else
            {
                trangthaihd.Enabled = true;
            }
            CustomMessageBox.show("Kết quả", ErrMsg, result);
            return result;
        }

        //hiện danh sách tất cả các hóa đơn mua
        public Boolean loadDSHDMDe(DataGridView gv)
        {
            DataTable table = HoaDonMua.Instance.getAllHDM();
            gv.DataSource = null;
            gv.Rows.Clear();
            if (table == null)
            {
                CustomMessageBox.show("Thông báo", "Chưa có hóa đơn mua nào", false);
                return false;
            }
            gv.DataSource = table;
            return true;
        }
        ///hiên danh sách hóa đơn để xuất

        public Boolean loadDSHDM(DataGridView gv)
        {
            DataTable table = HoaDonMua.Instance.getAllHDM();
            gv.DataSource = null;
            gv.Rows.Clear();
            if (table == null)
            {
                CustomMessageBox.show("Thông báo", "Chưa có hóa đơn mua nào", false);
                return false;
            }
            gv.DataSource = table;
            return true;
        }


        ///hiên danh sách hóa đơn nợ

        public Boolean loadDSHDMNo(DataGridView gv)
        {
            DataTable table = HoaDonMua.Instance.getAllHDMNo(false);
            gv.DataSource = null;
            gv.Rows.Clear();
            if (table == null)
            {
                CustomMessageBox.show("Thông báo", "Chưa có hóa đơn mua nào còn nợ", false);
                return false;
            }
            gv.DataSource = table;
            return true;
        }

        ///hiên danh sách các phiếu chi

        public Boolean loadDSPCHDM(DataGridView gv)
        {
            DataTable table = HoaDonMua.Instance.getPCHDM();
            gv.DataSource = null;
            gv.Rows.Clear();
            if (table == null)
            {
                CustomMessageBox.show("Thông báo", "Chưa có phiếu chi nào", false);
                return false;
            }
            gv.DataSource = table;
            return true;
        }

        public Boolean loadDSCTHDM_ByMaHD(string mahdmua,DataGridView gv)
        {
            DataTable table = HoaDonMua.Instance.getCTHDMByKey(mahdmua);
            gv.DataSource = null;
            gv.Rows.Clear();
            if (table == null)
            {
                return false;
            }
            gv.DataSource = table;
            return true;
        }

        public void SotienConNo(string mahd,Label sotienconno)
        {
            double tongtiendatra = HoaDonMua.Instance.getTongTienDaThanhToan(mahd);
            double tongtienhoadon = HoaDonMua.Instance.getTongTienHDM(mahd);
            sotienconno.Text = string.Format("{0:n0}",tongtienhoadon - tongtiendatra).ToString();
        }

        //lập phiếu chi
        public Boolean lapPhieuChi(string mahd, string diengiai, double sotien,string manv)
        {
            Boolean result = false;
            PHIEUCHI p = new PHIEUCHI();
            p.DIENGIAI = diengiai;
            p.MAHDM = mahd;
            p.TIEN = sotien;
            p.MANV = manv;
            string Msg = HoaDonMua.Instance.lapPhieuChi(p);
            CustomMessageBox.show("Thông báo", Msg, true);
            return result;
        }
    }
}
