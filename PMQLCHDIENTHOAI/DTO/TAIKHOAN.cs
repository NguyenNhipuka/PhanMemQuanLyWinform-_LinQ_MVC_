using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DTO
{
    public class TAIKHOAN
    {
        #region Properties
        string MANV;
        string TENTK;
        Boolean TRANGTHAITK;
        string MATKHAU;     
        Boolean QUENMK;
        DateTime NGAYTAO;
        DateTime NGAYRESET;
        List<int> DSCN;
        public TAIKHOAN()
        {
        }


        public TAIKHOAN(DataRow datarow)
        {
            if (datarow == null) return;
            _manv = datarow["MANV"].ToString();
            _tentk = datarow["TENTK"].ToString();
            _trangthai =(Boolean) datarow["TRANGTHAITK"];
        }
        public TAIKHOAN(string manv, string tentk, bool trangthai, string matkhau, bool quenmk, DateTime ngaytao, DateTime ngayreset)
        {
            _manv = manv;
            _tentk = tentk;
            _trangthai = trangthai;
            _matkhau = matkhau;           
            _quenmk = quenmk;
            _ngaytao = ngaytao;
            _ngayreset = ngayreset;
            
        }
        public TAIKHOAN(string manv, string tentk, bool trangthai, string matkhau, bool quenmk, DateTime ngaytao, DateTime ngayreset,List<int> dscn)
        {
            _manv = manv;
            _tentk = tentk;
            _trangthai = trangthai;
            _matkhau = matkhau;
            _quenmk = quenmk;
            _ngaytao = ngaytao;
            _ngayreset = ngayreset;
            _dscn = dscn;
        }
        public TAIKHOAN(string manv, string tentk, bool trangthai, string matkhau)
        {
            _manv = manv;
            _tentk = tentk;
            _trangthai = trangthai;
            _matkhau = matkhau;
        }

        public TAIKHOAN(string manv, bool trangthai)
        {
            _manv = manv;
            _trangthai = trangthai;
        }
        #endregion Properties

        #region GET SET
        public string _manv { get => MANV; set => MANV = value; }
        public string _tentk { get => TENTK; set => TENTK = value; }
        public string _matkhau { get => MATKHAU; set => MATKHAU = value; }
        public bool _trangthai { get => TRANGTHAITK; set => TRANGTHAITK = value; }
        public bool _quenmk { get => QUENMK; set => QUENMK = value; }
        public DateTime _ngaytao { get => NGAYTAO; set => NGAYTAO = value; }
        public DateTime _ngayreset { get => NGAYRESET; set => NGAYRESET = value; }
        public List<int> _dscn { get => DSCN; set => DSCN = value; }

        public static implicit operator List<object>(TAIKHOAN v)
        {
            throw new NotImplementedException();
        }
        #endregion END GET SET
    }
}
