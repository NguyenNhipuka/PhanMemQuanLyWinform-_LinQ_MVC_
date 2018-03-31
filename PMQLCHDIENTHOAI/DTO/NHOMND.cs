using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DTO
{
    public class NHOMND
    {
        int MANHOM;
        String TENNHOM;
        String GHICHU;

        public NHOMND()
        {
        }

        public NHOMND(int manhom, string tennhom, string ghichu)
        {
            _manhom = manhom;
            _tennhom = tennhom;
            _ghichu = ghichu;
        }

        public int _manhom { get => MANHOM; set => MANHOM = value; }
        public string _tennhom { get => TENNHOM; set => TENNHOM = value; }
        public string _ghichu { get => GHICHU; set => GHICHU = value; }

        
    }
}
