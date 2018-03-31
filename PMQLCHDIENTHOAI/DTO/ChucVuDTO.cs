using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DTO
{
    public class ChucVuDTO
    {
        int MACV;
        String TENCV;

        public ChucVuDTO(int macv, string tencv)
        {
            _macv = macv;
            _tencv = tencv;
        }
        public ChucVuDTO( string tencv)
        {
            _macv = 0;
            _tencv = tencv;
        }
        public int _macv { get => MACV; set => MACV = value; }
        public string _tencv { get => TENCV; set => TENCV = value; }
      
    }
}
