using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DTO
{
    public class NhanVienDTO
    {
        #region Properties
        public  int ID_NV;
        public  String MANV;
        public int MACV;
        public  String TENNV;
        public  String DIACHINV;
        public  String SDTNV;
        public  String GIOITINHNV;
        public  DateTime NGAYSINH;
        public  DateTime NGAYTAONV;
        public  String CMND;
        public  Double BACLUONG;
        public  Double PHUCAP;
        public  Double LUONG;
        public Boolean TRANGTHAINV;



        #endregion Properties

        public NhanVienDTO()
        {

        }
       
        public NhanVienDTO(DataRow row)
        {
            ID_NV = Int32.Parse(row[1].ToString());          
            MANV = row[0].ToString();
            MACV = Int32.Parse(row[2].ToString());
            TENNV = row[3].ToString(); ;
            DIACHINV = row[4].ToString();
            SDTNV = row[5].ToString();
            GIOITINHNV = row[6].ToString();
            NGAYSINH = DateTime.Parse(row[7].ToString());
            NGAYTAONV = DateTime.Parse(row[9].ToString());
            CMND = row[10].ToString();
            BACLUONG = double.Parse(row[11].ToString());
            PHUCAP = double.Parse(row[12].ToString());
            LUONG = double.Parse(row[13].ToString());
            TRANGTHAINV = Boolean.Parse(row[8].ToString());
        }

        public NhanVienDTO(int mACV, string tENNV, string dIACHINV, string sDTNV, string gIOITINHNV, DateTime nGAYSINH, string cMND, double bACLUONG, double pHUCAP, double lUONG, bool tRANGTHAINV)
        {
            MACV = mACV;
            TENNV = tENNV;
            DIACHINV = dIACHINV;
            SDTNV = sDTNV;
            GIOITINHNV = gIOITINHNV;
            NGAYSINH = nGAYSINH;
          
            CMND = cMND;
            BACLUONG = bACLUONG;
            PHUCAP = pHUCAP;
            LUONG = lUONG;
            TRANGTHAINV = tRANGTHAINV;
        }

        public NhanVienDTO(string mANV, int mACV, string tENNV, string dIACHINV, string sDTNV, string gIOITINHNV, DateTime nGAYSINH, DateTime nGAYTAONV, string cMND, double bACLUONG, double pHUCAP, double lUONG, bool tRANGTHAINV)
        {
            MANV = mANV;
            MACV = mACV;
            TENNV = tENNV;
            DIACHINV = dIACHINV;
            SDTNV = sDTNV;
            GIOITINHNV = gIOITINHNV;
            NGAYSINH = nGAYSINH;
            NGAYTAONV = nGAYTAONV;
            CMND = cMND;
            BACLUONG = bACLUONG;
            PHUCAP = pHUCAP;
            LUONG = lUONG;
            TRANGTHAINV = tRANGTHAINV;
        }

        public NhanVienDTO(int iD_NV, string mANV, int mACV, string tENNV, string dIACHINV, string sDTNV, string gIOITINHNV, DateTime nGAYSINH, DateTime nGAYTAONV, string cMND, double bACLUONG, double pHUCAP, double lUONG, bool tRANGTHAINV)
        {
            ID_NV = iD_NV;
            MANV = mANV;
            MACV = mACV;
            TENNV = tENNV;
            DIACHINV = dIACHINV;
            SDTNV = sDTNV;
            GIOITINHNV = gIOITINHNV;
            NGAYSINH = nGAYSINH;
            NGAYTAONV = nGAYTAONV;
            CMND = cMND;
            BACLUONG = bACLUONG;
            PHUCAP = pHUCAP;
            LUONG = lUONG;
            TRANGTHAINV = tRANGTHAINV;
        }
    }
}
