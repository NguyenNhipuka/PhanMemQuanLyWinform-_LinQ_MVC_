using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DTO
{
    public class NHACUNGCAP
    {
        #region Properties

        #endregion
        int ID_NCC;
        int MANH;
        int TENNCC;
        string SDTNCC;
        string EMAILNCC;
        string DIACHINCC;
        string STKBANK;
        DateTime NGAYTAO;
        Boolean TRANGTHAINCC;

        public NHACUNGCAP()
        {
        }

        public NHACUNGCAP(int manh, int ten, string sdt, string email, string diachi, string stkbank, bool trangthai)
        {
            _manh = manh;
            _ten = ten;
            _sdt = sdt;
            _email = email;
            _diachi = diachi;
            _stkbank = stkbank;
            _trangthai = trangthai;
        }

        public NHACUNGCAP(int id, int manh, int ten, string sdt, string email, string diachi, string stkbank, DateTime ngaytao, bool trangthai)
        {
            _id = id;
            _manh = manh;
            _ten = ten;
            _sdt = sdt;
            _email = email;
            _diachi = diachi;
            _stkbank = stkbank;
            _ngaytao = ngaytao;
            _trangthai = trangthai;
        }

        public int _id { get => ID_NCC; set => ID_NCC = value; }
        public int _manh { get => MANH; set => MANH = value; }
        public int _ten { get => TENNCC; set => TENNCC = value; }
        public string _sdt { get => SDTNCC; set => SDTNCC = value; }
        public string _email { get => EMAILNCC; set => EMAILNCC = value; }
        public string _diachi { get => DIACHINCC; set => DIACHINCC = value; }
        public string _stkbank { get => STKBANK; set => STKBANK = value; }
        public DateTime _ngaytao { get => NGAYTAO; set => NGAYTAO = value; }
        public bool _trangthai { get => TRANGTHAINCC; set => TRANGTHAINCC = value; }
    }
}
