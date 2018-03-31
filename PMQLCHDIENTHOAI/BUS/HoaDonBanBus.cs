using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Bunifu.Framework.UI;
using MetroFramework.Controls;
using PMQLCHDIENTHOAI;
using System.Data;
using DAO.Implementation;
using DTO;
using CrystalDecisions.CrystalReports.Engine;

namespace BUS
{
    public class HoaDonBanBus
    {
        private static HoaDonBanBus instance;

        public static HoaDonBanBus Instance
        {
            get
            {
                if (instance == null)
                    instance = new HoaDonBanBus();
                return instance;
            }
            private set
            {
                HoaDonBanBus.instance = value;
            }
        }

        ///load các khách hàng
        public Boolean loadDSKhachHangHDB(ComboBox cb)
        {
            DataTable table = HoaDonBanDAO.Instance.getAllKhachHangHDB();
            cb.DataSource = null;
            if(table == null)
            {
                return false;
            }
            cb.DataSource = table;
            cb.DisplayMember = "TEN";
            cb.ValueMember = "MAKH";
            cb.SelectedIndex = 0;
            return true;
        }

        ///load các phương thức giao hàng hdbans
        public Boolean loadDSPTGHHDB(ComboBox cb)
        {
            DataTable table = HoaDonBanDAO.Instance.getAllPhuongThucGiaoHangHDB();
            cb.DataSource = null;
            if (table == null)
            {
                return false;
            }
            cb.DataSource = table;
            cb.DisplayMember = "TEN";
            cb.ValueMember = "MAPTGH";
            cb.SelectedIndex = 0;
            return true;
        }

        ///load các loại tình trạng hdb
        public Boolean loadTinhTrangHDB(ComboBox cb)
        {
            DataTable table = new DataTable();
            table.Columns.Add("MATT");
            table.Columns.Add("TEN");
            table.Rows.Add(2, "Đang xử lý");
            table.Rows.Add(0, "Hủy");
            table.Rows.Add(1, "Đã giao");          
            table.Rows.Add(3, "Đang giao");
            cb.DataSource = table;
            cb.DisplayMember = "TEN";
            cb.ValueMember =  "MATT";
            cb.SelectedIndex = 0;
            return true;
        }

        ///load các loại tình trạng hdb
        public Boolean loadLyDoHDB(ComboBox cb)
        {
            DataTable table = HoaDonBanDAO.Instance.getAllLyDoHDB();
            cb.DataSource = null;
            if (table == null)
            {
                return false;
            }
            cb.DataSource = table;
            cb.DisplayMember = "TENLYDO";
            cb.ValueMember = "MALYDO";
            cb.SelectedIndex = 0;
            return true;
        }

        ///load thông tin của khách hàng theo mà lên hóa đơn bán
        public Boolean loadThongTinKHHDB(int makh,BunifuMaterialTextbox tenkh, BunifuMaterialTextbox diachikh,
            BunifuMaterialTextbox sodienthoai)
        {
            KHACHHANG kh = HoaDonBanDAO.Instance.getKhachHangInfoByKey(makh);
            if(kh == null)
            {
                diachikh.Text = sodienthoai.Text = tenkh.Text =string.Empty;
                return false;
            }
            tenkh.Text = kh.TENKH;
            sodienthoai.Text = kh.SDTKH;
            diachikh.Text = kh.DIACHIKH;
            return true;
        }

        public void phivanchuyen(int maptgiaohang,Label phigh)
        {
           
            PTGIAOHANG pt = HoaDonBanDAO.Instance.getPTGHByKeyHDB(maptgiaohang);
            if (pt != null)
            {
                phigh.Text = pt.PHIPTGH.ToString();
                return;
            }
            phigh.Text = "0";
        }

        ///lap hóa đơn bán
        public Boolean addHDB(out string mahdbmoi,string manv,int makh,string tenkh,string sdt,string diachinhan,
            BunifuMaterialTextbox phuthu,double phivc, ComboBox cbhtgiaohang, ComboBox cbtthoadon,ComboBox lydoxuat
               ,BunifuMaterialTextbox txttongtien, DataGridView data, BunifuMaterialTextbox nohdb)
        {
            
            int htgiaohang = Convert.ToInt32(cbhtgiaohang.SelectedValue.ToString());
            Byte tthoadon = Convert.ToByte(cbtthoadon.SelectedValue.ToString());
            int lydo = Convert.ToInt32(lydoxuat.SelectedValue.ToString());
            double tongtien = Double.Parse(txttongtien.Text.ToString());
            HDB h = new HDB();
            h.MAKH = makh;
            h.MANV = manv;
            h.TENKHB = tenkh;
            h.SDTKHB = sdt;
            h.DIACHIKHB = diachinhan;
            h.MALYDO = lydo;
            h.MAPTGH = htgiaohang;
            h.TRIGIAHDB = tongtien;
            h.TINHTRANGHDB = tthoadon;
            h.PHIGIAOH = phivc;
            h.THUTHEMHDB = double.Parse(phuthu.Text.ToString());
            h.NOHDB = double.Parse(nohdb.Text.ToString());

            
            List<CTHDB> ds = new List<CTHDB>();
            CTHDB c;
            for (int i = 0; i < data.Rows.Count; i++)
            {
                c = new CTHDB();
                c.MAH = data.Rows[i].Cells[1].Value.ToString();
                c.SOLUONGB = Convert.ToInt32(data.Rows[i].Cells[3].Value.ToString());
                c.TGBHBAN = Convert.ToInt32(data.Rows[i].Cells[6].Value.ToString());
                c.THANHTIEN = double.Parse(data.Rows[i].Cells[8].Value.ToString());
                c.DONGIAHDB = double.Parse(data.Rows[i].Cells[5].Value.ToString());
                c.KHUYENMAI= double.Parse(data.Rows[i].Cells[7].Value.ToString());                
                ds.Add(c);
            }
            mahdbmoi = "0000000000";
            
            Boolean result = HoaDonBanDAO.Instance.themHDB(h, ds, out int? ErrCode, out string ErrMsg,out string mahdb);
            mahdbmoi = mahdb;
             if(result)
                CustomMessageBox.show("Hóa đơn bán mới "+mahdb, ErrMsg, result);
             else
                CustomMessageBox.show("Kết quả ", ErrMsg, result);
            return result;
        }


        public Boolean updateHDB(string mahd,byte tinhtrang,double thanhtoan)
        {
            Boolean result = HoaDonBanDAO.Instance.capNhatHDB(mahd, tinhtrang, thanhtoan, out int? ErrCode, out string ErrMsg);
            if (result)
            {
                CustomMessageBox.show("Thông báo", ErrMsg, result);                
            }
            return result;
        }
        ///load thiet bị theo mã lên form thêm hóa đơn mua
        public Boolean loadTBByKey(string matb, BunifuMaterialTextbox tonkho, BunifuMaterialTextbox gia, BunifuMaterialTextbox khuyenmai)
        {
            sp_TB_Getlist_ByKeyResult tb = ThietBiDAO.Instance.getTBByKey(matb);
            if (tb == null) return false;
            tonkho.Text = tb.SLH.ToString();
            gia.Text = tb.DONGIAM.ToString();
            khuyenmai.Text = tb.KHUYENMAI_H.ToString();
            return true;
        }
        //thêm phương thức giao hàng
        public Boolean AddPTGH(string tenPTGH, double phi)
        {
            Boolean result = HoaDonBanDAO.Instance.addPTGH(tenPTGH, phi, out int? ErrCode, out string ErrMsg);
            CustomMessageBox.show("Kết quả", ErrMsg, result);
            return result;
        }
        ///hiên danh sách hóa đơn bán
        public Boolean loadDSHDB(DataGridView gv)
        {
            DataTable table = HoaDonBanDAO.Instance.getAllHDB();
            gv.DataSource = null;
            gv.Rows.Clear();
            if (table != null)
            {
               
                gv.DataSource = table;
                return true;
            }
            else
            {
                CustomMessageBox.show("Thông báo", "Chưa có hóa đơn bán nào", false);
                {
                    return false;
                }
            }
        }
        //load tất cả PTHG lên datagridview
        public Boolean LoadPTGH(DataGridView gv)
        {
            DataTable table = HoaDonBanDAO.Instance.getDSAllPTGH();
            gv.DataSource = null;

            gv.Rows.Clear();
            if (table != null)
            {
                gv.DataSource = table;
            }  
            
            return true;
        }
      
        //dalete PTGH
        public Boolean DeletePTGH(int maptgh)
        {
            Boolean result = HoaDonBanDAO.Instance.deletePTGH(maptgh, out int? ErrCode, out string ErrMsg);
            CustomMessageBox.show("Kết quả", ErrMsg, result);
            return result;
        }
        //update nhà cung cấp
        public Boolean UpdatePTGH(int maptgh, string tenptgh, double phiptgh)
        {
            PTGIAOHANG p = new PTGIAOHANG();
            p.MAPTGH = maptgh;
            p.TENPTGH = tenptgh;
            p.PHIPTGH = phiptgh;            
            Boolean result = HoaDonBanDAO.Instance.updatePTGH(p, out int? ErrCode, out string ErrMsg);
            CustomMessageBox.show("Kết quả", ErrMsg, result);
            return result;
        }
        ///hiên danh sách hóa đơn bán đã thanh toán hết
        public Boolean loadDSHDBThanhToan(DataGridView gv)
        {
            DataTable table = HoaDonBanDAO.Instance.getAllHDBThanhToan();
            gv.DataSource = null;
            gv.Rows.Clear();
            if (table == null)
            {
                CustomMessageBox.show("Thông báo", "Không có hóa đơn bán nào", false);
                return false;
            }
            gv.DataSource = table;
            return true;
        }
        ///hiên danh sách hóa đơn bán theo ngày
        public Boolean loadDSHDBNgay(MetroDateTime dfrom, MetroDateTime dto,DataGridView gv)
        {
            DataTable table = HoaDonBanDAO.Instance.getAllHDBTheoNgay(dfrom.Value, dto.Value);
            gv.DataSource = null;
            gv.Rows.Clear();
            if (table == null)
            {
                CustomMessageBox.show("Thông báo", "Không có hóa đơn bán nào", false);
                return false;
            }
            gv.DataSource = table;
            return true;
        }

        ///hiên danh sách chi tiết hóa đơn bán
        public Boolean loadDSCTHDBByMADHB(string mahdb,DataGridView gv)
        {
            DataTable table = HoaDonBanDAO.Instance.getAllCTHDBByKeyMAHDB(mahdb);
            gv.DataSource = null;
            gv.Rows.Clear();
            if (table == null)
            {
                return false;
            }
            gv.DataSource = table;
            return true;
        }

        ///hiên danh sách hóa đơn bán
        public Boolean loadDSHDBNo(DataGridView gv)
        {
            DataTable table = HoaDonBanDAO.Instance.getAllHDBNo();
            gv.DataSource = null;
            gv.Rows.Clear();
            if (table == null)
            {
                CustomMessageBox.show("Thông báo", "Chưa có hóa đơn bán nào", false);
                return false;
            }
            gv.DataSource = table;
            return true;
        }

                ///search hóa đơn bán theo mã

        public Boolean searchHDBByKey(string mahd, BunifuMaterialTextbox makh, BunifuMaterialTextbox tenkh, BunifuMaterialTextbox sdt,
            BunifuMaterialTextbox hinhthucgiao, BunifuMaterialTextbox phi, BunifuMaterialTextbox diachi, BunifuMaterialTextbox phuthu,
            BunifuMaterialTextbox lydo, BunifuMaterialTextbox conno, DataGridView gv, BunifuMaterialTextbox ngay, BunifuMaterialTextbox tongtien,
            BunifuMaterialTextbox tong,ComboBox tinhtrang)
        {
            sp_HDB_GetlistInfoByMHDBResult hd = HoaDonBanDAO.Instance.getHDMByKey(mahd);
            if (hd == null)
            {
                CustomMessageBox.show("Kết quả", "Không tồn tại hóa đơn này", false);
                return false;
            }
                tenkh.Text = hd.Tên_khách_hàng;
                makh.Text = hd.Mã_khách_hàng.ToString();
                sdt.Text = hd.Số_điện_thoại;
                hinhthucgiao.Text = hd.Phương_thức_giao;
                phi.Text = string.Format("{0:n0}",hd.Phí_giao);
                diachi.Text = hd.Địa_chỉ;
                phuthu.Text = string.Format("{0:n0}", hd.Phụ_thu);
                lydo.Text = hd.Lý_do_xuất;
                conno.Text = string.Format("{0:n0}", hd.Trị_giá_hóa_đơn - hd.Đã_thanh_toán);
                ngay.Text = hd.Ngày_tạo.ToString();
                tongtien.Text = string.Format("{0:n0}", hd.Trị_giá_hóa_đơn);
                tinhtrang.SelectedValue = hd.Tình_trạng_hóa_đơn_bán;
  
                tinhtrang.Enabled = (hd.Tình_trạng_hóa_đơn_bán >1);

                DataTable tb = HoaDonBanDAO.Instance.getAllCTHDBByKeyMAHDB(hd.Mã);
            gv.DataSource = null;
            gv.Rows.Clear();
            if (tb == null) return true;
               
                gv.DataSource = tb;
                var sum = tb.AsEnumerable().Sum(x => x.Field<double>(7));
                tong.Text = string.Format("{0:n0}", sum);
           

            return true;

        }
       
       
    }
}
