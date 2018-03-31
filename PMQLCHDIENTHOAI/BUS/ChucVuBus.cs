using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAO.Implementation;
using DTO;
using System.Windows.Forms;
using System.Data;
using Bunifu.Framework.UI;
using PMQLCHDIENTHOAI;
namespace BUS
{
    public class ChucVuBus
    {
        private static ChucVuBus instance;

        public static ChucVuBus Instance
        {
            get
            {
                if (instance == null)
                    instance = new ChucVuBus();
                return instance;
            }
            private set
            {
                ChucVuBus.instance = value;
            }
        }

        public Boolean LoadPositionList(ComboBox cb)
        {
            DataTable table = ChucVuDAO.Instance.getPositionTable();
            if (table != null)
            {
                cb.DataSource = table;
                cb.DisplayMember = "TENCV";
                cb.ValueMember = "MACV";
            }

            return true;
        }

        public Boolean insertPosition(string tencv)
        {
            ChucVuDAO.Instance.InsertPosition(tencv, out int? ErrCode, out string ErrMsg);            
            CustomMessageBox.show("Kết quả", ErrMsg, ErrCode == 0);
            return ErrCode == 0;
        }
        public Boolean updatePosition(int macv,string tencv)
        {
            ChucVuDAO.Instance.updatePosition(macv,tencv, out int? ErrCode, out string ErrMsg);
            CustomMessageBox.show("Kết quả", ErrMsg, ErrCode == 0);
            return ErrCode == 0;
        }
        public Boolean deletePosition(int macv)
        {
            ChucVuDAO.Instance.deletePosition(macv, out int? ErrCode, out string ErrMsg);
            CustomMessageBox.show("Kết quả", ErrMsg, ErrCode == 0);
            return ErrCode == 0;
        }
    }
}
