 /*==============================================================*/
/*Tên đề tài: Quản lý cửa hang mua bán linh kiện va fthieets bị điện thoại*/
/* Nhóm 1: Nguyễn Thị Yến Nhi - 2001140169, Nguyễn Thị Thảo Quyên - 2001140208 */
/*==============================================================*/
/******* drop database DATABASE_DT ****8*/
CREATE DATABASE DATABASE_DT
/*==============================================================*/
/* Table: BANK                                                  */F
/*==============================================================*/

create table BANK (
   MANH                 int   IDENTITY (1,1)      not null,
   TENNH                nvarchar(30)          not null,
   constraint PK_BANK primary key nonclustered (MANH)
)
go
/*==============================================================*/
/* Table: CHUCVU                                                */
/*==============================================================*/
create table CHUCVU (
   MACV                 int   IDENTITY(1,1)       not null,
   TENCV                nvarchar(100)         not null,
   constraint PK_CHUCVU primary key nonclustered (MACV)
)
go
/*==============================================================*/
/* Table: LOAIHANG                                              */
/*==============================================================*/
create table LOAIHANG (
   MALOAI               INT   IDENTITY(1,1)       not null,
   TENLOAI              nvarchar(50)         not null,
   constraint PK_LOAIHANG primary key nonclustered (MALOAI)
)
go
/*==============================================================*/
/* Table: DVT                                                   */
/*==============================================================*/
create table DVT (
   MADVT                int   IDENTITY(1,1)       not null,
   TENDVT               nvarchar(20)         not null,
   constraint PK_DVT primary key nonclustered (MADVT)
)
go
/*==============================================================*/
/* Table: PTGIAOHANG                                            */
/*==============================================================*/
create table PTGIAOHANG (
   MAPTGH               int IDENTITY(1,1)         not null,
   TENPTGH              nvarchar(50)         not null,
   PHIPTGH              float                not null,
   constraint PK_PTGIAOHANG primary key nonclustered (MAPTGH)
)
go
/*==============================================================*/
/* Table: MANHINH                                               */
/*==============================================================*/
create table MANCHUCNANG (
   MACN                int IDENTITY(1,1)          not null,
   TENMANHINH           nvarchar(50)         not null,
   TRANGTHAI            bit              not null,
   constraint PK_MANHINH primary key nonclustered (MACN)
)
go
/*==============================================================*/
/* Table: HANG                                                  */
/*==============================================================*/
create table HANG (
   MAH                  char(10)             not null,
   MALOAI               INT                  not null,
   MADVT                int					 not null,
   MANCC				INT					not null,
   TENH                 nvarchar(100)         not null,
   SLH                  INT			        not null,
   DONGIAM              float                not null,
   DONGIAB              float                not null,
   TGBH                 int                  not null,
   NSX                  nvarchar(50)         not null,
   TINHTRANGH           tinyint                  not null,
   MOTAH				NVARCHAR(200)		not null,
   ID int IDENTITY(1,1)          not null,
   constraint PK_HANG primary key nonclustered (MAH)
)
go
/*==============================================================*/
/* Table: CT_HANG                                               */
/*==============================================================*/
create table CT_HANG (
   MAH                  char(10)             not null,
   TON_MAX              int                  not null,
   TON_MIN              int                  not null,
   KHUYENMAI_H          float                not null,
   constraint PK_CT_HANG primary key (MAH)
)
go
/*==============================================================*/
/* Table: NHANVIEN                                              */
/*==============================================================*/
create table NHANVIEN (
   MANV                 char(6)              not null,
   IDNV					INT IDENTITY (1,1) NOT NULL,
   MACV                 int                  not null,
   TENNV                nvarchar(60)         not null,
   DIACHINV             nvarchar(100)        not null,
   SDTNV                nvarchar(11)         not null,
   GIOITINHNV           nvarchar(3)          not null,
   NGAYSINH             datetime             not null,
   TRANGTHAINV          bit              not null,
   NGAYTAONV            datetime             not null,
   CMND                 varchar(12)         not null,
   BACLUONG             float                not null,
   PHUCAP               float                not null,
   LUONG                float                not null,
   constraint PK_NHANVIEN primary key nonclustered (MANV)
)
go

/*==============================================================*/
/* Table: NCC                                                   */
/*==============================================================*/
create table NCC (
   MANCC                INT IDENTITY(1,1)             not null,
   MANH                 int                  not null,
   TENNCC               nvarchar(100)        not null,
   SDTNCC               varchar(11)         not null,
   EMAILNCC             varchar(30)         not null,
   DIACHINCC            nvarchar(100)        not null,
   STKBANK              varchar(30)         not null,
   NGAYTAONCC           datetime             not null,
   TRANGTHAINCC         bit              not null,
   constraint PK_NCC primary key nonclustered (MANCC)
)
go

/*==============================================================*/
/* Table: HDM                                                   */
/*==============================================================*/
create table HDM (
   MAHDM                char(10)             not null,
   MANV                 char(6)              not null,
   MANCC                INT              not null,
   NGAYTAOHDM           datetime             not null,
   HANNOHDM             datetime             not null,
   GTGTHDM				FLOAT				not null,
   TONGTIEN             float                not null,
   TINHTRANGHDM         tinyint                  not null,
   TINHTRANGTTOAN		bit					not null,
   ID int IDENTITY(1,1)          not null,

   constraint PK_HDM primary key nonclustered (MAHDM)
)
go
/*==============================================================*/
/* Table: CTHDM                                                 */
/*==============================================================*/
create table CTHDM (
   MAH                  char(10)             not null,
   MAHDM                char(10)             not null,
   DONGIAHM             float                not null,
   TGBAOHANH            int                  not null,
   SLUONGHM             int                  not null,
   THANHTIEN			FLOAT				not null,
    TRANGTHAI				tinyint				NOT NULL ,
   constraint PK_CTHDM primary key (MAH, MAHDM)
)
go
/*==============================================================*/
/* Table: PHIEUCHI                                                  */
/*==============================================================*/
create table PHIEUCHI (
   MAPCHDM        int      IDENTITY(1,1)  NOT NULL,
   DIENGIAI		 nvarchar(100)         not null,	
   NGAYCHI         datetime             not null,
	MAHDM 			CHAR(10)			not null,
	MANV 			CHAR(6) not null,
	TIEN FLOAT not null,
   constraint PK_PHIEUCHI primary key nonclustered (MAPCHDM)
)
go
/*==============================================================*/
/* Table: LYDOXUAT                                              */
/*==============================================================*/
create table LYDOXUAT (
   MALYDO               int  IDENTITY(1,1)        not null,
   TENLYDO              nvarchar(30)         not null,
   constraint PK_LYDOXUAT primary key nonclustered (MALYDO)
)
go
/*==============================================================*/
/* Table: HDB                                                   */
/*==============================================================*/
create table HDB (
   MAHDB                char(10)             not null,
   MALYDO               int                  not null,
   MAKH                 int            not null,
   MANV                 char(6)              not null,
   MAPTGH               int                  not null,
   TENKHB               nvarchar(50)         not null,
   DIACHIKHB            nvarchar(100)        not null,
   SDTKHB               varchar(11)         not null,
   NGAYBAN              datetime             not null,
   TRIGIAHDB            float                not null,
   TINHTRANGHDB        tinyint                  not null,
   PHIGIAOH             float                not null,
   THUTHEMHDB           float                not null,
   ID int IDENTITY(1,1)          not null,
      NOHDB     float                NOT null,
   constraint PK_HDB primary key nonclustered (MAHDB)
)
go

/*==============================================================*/
/* Table: PHIEUBAOHANH                                          */
/*==============================================================*/
create table PHIEUBAOHANH (
   IDPBH				INT IDENTITY  (1,1)      NOT NULL,
   MAHDB                char(10)             not null,
   MAH                  char(10)             not null,
   MANV                 char(6)              not null,
   NGAYLAPPBH           datetime             not null,
   SERIALBH             Varchar(30)         not null,
   TINHTRANGPBH         tinyint                  not null,
   constraint PK_PHIEUBAOHANH primary key nonclustered (IDPBH)
)
go
/*==============================================================*/
/* Table: BAOHANH                                          */
/*==============================================================*/
create table BAOHANH (
   IDBH				INT IDENTITY (1,1)       NOT NULL,
   IDPBH			int                  not null,
   NGUYENNHAN		NVARCHAR(200)		not null,
   MANV                 char(6)              not null,
   NGAYLAPBH           datetime             not null,
   NGAYGIAOBH           datetime             not null,
   TINHTRANGBH         tinyint                  not null,
   SDTNHAN VARCHAR(11) not null,
   constraint PK_BAOHANH primary key nonclustered (IDBH)
)
go
/*==============================================================*/
/* Table: CTHDB                                                 */
/*==============================================================*/

create table CTHDB (
   MAHDB                char(10)             not null,
   MAH                  char(10)             not null,
   SOLUONGB             int                  not null,
   DONGIAHDB            float                not null,
   KHUYENMAI            float                not null,
   TGBHBAN              int                  not null,
   THANHTIEN FLOAT NOT  null,
   constraint PK_CTHDB primary key (MAHDB, MAH)
)
go
/*==============================================================*/
/* Table: KHACHHANG                                             */
/*==============================================================*/
create table KHACHHANG (
   MAKH                 int    IDENTITY (1,1)        not null,
   TENKH                nvarchar(50)         not null,
   DIACHIKH             nvarchar(100)        not null,
   SDTKH                varchar(11)         not null,
   EMAILKH              varchar(30)         not null,
   TINHTRANGKH          int             not null,
   constraint PK_KHACHHANG primary key nonclustered (MAKH)
)
go

/*==============================================================*/
/* Table: SUACHUA                                               */
/*==============================================================*/
create table SUACHUA (
	MASC               char(10)             NOT null,
   MAKH                 INT             not null,
   MANV                 char(6)              not null,
   TENKHSC              nvarchar(50)         not null,
   SDTKHSC              varchar(11)         not null,
   NGAYNHANSC           datetime             not null,
   NGAYGIAOSC           datetime             not null,
   TONGCHIPHISC         float                not null,
   TINHTRANGSC          TINYINT                  not null,
    ID                 INT      IDENTITY (1,1)   not null,
)
go
alter TABLE SUACHUA ADD constraint PK_SUACHUA primary key (MASC)
/*==============================================================*/
/* Table: TAIKHOANDN                                            */
/*==============================================================*/
create table TAIKHOANDN (
   MANV                 char(6)              not null,
   TENTK                Nvarchar(60)         not null,
   MATKHAUTK            varchar(100)        not null,
   TRANGTHAITK         bit              not null,
   QUENMK               bit             not null,
   NGAYTAO				DATETIME			 not null,
   NGAYRESET			DATETIME			 not null,
   constraint PK_TAIKHOANDN primary key (MANV)
)
go
/*==============================================================*/
/* Table: NHOMNGUOIDUNG                                         */
/*==============================================================*/
create table NHOMNGUOIDUNG (
   MANHOM               int  IDENTITY (1,1)               not null,
   TEMNHOM              nvarchar(50)         not null,
   GHICHU               nvarchar(100)        not null,
   constraint PK_NHOMNGUOIDUNG primary key nonclustered (MANHOM)
)
go
/*==============================================================*/
/* Table: NDNHOMND                                              */
/*==============================================================*/
create table NDNHOMND (
   MANV                 char(6)              not  null,
   MANHOM               int                  not  null,
   GHICHU               nvarchar(100)        not null,
   constraint PK_NDNHOMND primary key (MANV, MANHOM)
)
go
/*==============================================================*/
/* Table: PHANQUYEN                                             */
/*==============================================================*/
create table PHANQUYEN (
   MACN          int                  not null,
   MANHOM               int                  not null,
   constraint PK_PHANQUYEN primary key (MACN, MANHOM)
)
go
/*==============================================================*/
/* Table: CTHDSC                                                */
/*==============================================================*/
create table CTHDSC (
   SOHIEU				int   IDENTITY (1,1)      not null,
   MASC                CHAR(10)				 NOT null,
   MAH                  char(10)             not null,
   TENTBSC              nvarchar(50)         not null,
   LOISC                nvarchar(200)        not null,
   MOTASC				nvarchar(200)        NOT  null,
   CHIPHISC             float                NOT null,
   TINHTRANGCTSC		bit              NOT null,	
   constraint PK_CTHDSC primary key nonclustered (SOHIEU)
)
go
/*==============================================================*/
/* Table: HANGLOI                                               */
/*==============================================================*/
create table HANGLOI (
   MAHL                 int   IDENTITY (1,1)      not null,
   MANV                 char(6)              not null,
   MAHDB                char(10)             not null,
   LANDOI               int                  not null,
   NGAYNHAPHANGLOI      datetime             not null,
   LYDONHAPHLOI         nvarchar(100)         not null,
   constraint PK_HANGLOI primary key nonclustered (MAHL)
)
go
/*==============================================================*/
/* Table: HOADONMOI                                             */
/*==============================================================*/
create table HOADONMOI (
   MAHDB                char(10)             not null,
   LANDOI               int                  not null,
   MALYDO               int                  not null,
   MANV                 char(6)              not null,
   GTGTDH               float                not null,
   TGBHDOIHANG          DATETIME                  not null,

   constraint PK_HOADONMOI primary key nonclustered (MAHDB, LANDOI)
)
go
/*==============================================================*/
/* Table: CTDOIHANG                                             */
/*==============================================================*/
create table CTDOIHANG (
   IDCTDOIHANG          INT IDENTITY(1,1)          NOT NULL,
   MAH                  char(10)             not null,
   MAHDB                char(10)             not null,
   LANDOI               int                  not null,
   TGBHHANGDOI          datetime             not null,
   SOLUONG              int                  not null,
   constraint PK_CTDOIHANG primary key (IDCTDOIHANG)
)
go

/*==============================================================*/
/* KHOA NGOAI                                          */
/*==============================================================*/

alter table HANG
   add constraint FK_HANG_RELATIONS_DVT foreign key (MADVT)
      references DVT (MADVT)


alter table HANG
   add constraint FK_HANG_RELATIONS_LOAIHANG foreign key (MALOAI)
      references LOAIHANG (MALOAI)
go

alter table HANG
   add constraint FK_HANG_RELATIONS_NCC foreign key (MANCC)
      references NCC(MANCC)
go
----------------------------------------------------------------
alter table NCC
   add constraint FK_NCC_RELATIONS_BANK foreign key (MANH)
      references BANK (MANH)
go
---------------------------------------------------------------
alter table HDM
   add constraint FK_HDM_RELATIONS_NHANVIEN foreign key (MANV)
      references NHANVIEN (MANV)
go

alter table HDM
   add constraint FK_HDM_RELATIONS_NCC foreign key (MANCC)
      references NCC (MANCC)
go
--------------------------------------------------------------
alter table HDB
   add constraint FK_HDB_RELATIONS_KHACHHAN foreign key (MAKH)
      references KHACHHANG (MAKH)
go

alter table HDB
   add constraint FK_HDB_RELATIONS_NHANVIEN foreign key (MANV)
      references NHANVIEN (MANV)
go

alter table HDB
   add constraint FK_HDB_RELATIONS_PTGIAOHA foreign key (MAPTGH)
      references PTGIAOHANG (MAPTGH)
go

alter table HDB
   add constraint FK_HDB_RELATIONS_LYDOXUAT foreign key (MALYDO)
      references LYDOXUAT (MALYDO)
go
alter table CTHDB
   add constraint FK_CTHDB_RELATIONS_HANG foreign key (MAH)
      references HANG (MAH)
go
alter table CTHDB
   add constraint FK_CTHDB_RELATIONS_HDB foreign key (MAHDB)
      references HDB (MAHDB)	  
go
---------------------------------------------------------------
alter table CTHDM
   add constraint FK_CTHDM_RELATIONS_HDM foreign key (MAHDM)
      references HDM (MAHDM)
go

alter table CTHDM
   add constraint FK_CTHDM_RELATIONS_HANG foreign key (MAH)
      references HANG (MAH)
go
---------------------------------------------------------------
alter table PHIEUCHI
   add constraint FK_PHIEUCHI_RELATIONS_HDM foreign key (MAHDM)
      references HDM (MAHDM)
go

alter table PHIEUCHI
   add constraint FK_PHIEUCHI_RELATIONS_NHANVIEN foreign key (MANV)
      references NHANVIEN(MANV)
go
-----------------------------------------------------------------
alter table HANGLOI
   add constraint FK_HANGLOI_RELATIONS_NHANVIEN foreign key (MANV)
      references NHANVIEN (MANV)
go

alter table HANGLOI
   add constraint FK_HANGLOI_RELATIONS_HOADONMO foreign key (MAHDB, LANDOI)
      references HOADONMOI (MAHDB, LANDOI)
go
-------------------------------------------------------------------
alter table NHANVIEN
   add constraint FK_CV_RELATIONS_NHANVIEN foreign key (MACV)
      references CHUCVU(MACV)
go
-------------------------------------------------------------------

---------------------------------------------------------------------
alter table TAIKHOANDN
   add constraint FK_TAIKHOAN_RELATIONS_NHANVIEN foreign key (MANV)
      references NHANVIEN (MANV)
go
----------------------------------------------------------------------
alter table SUACHUA
   add constraint FK_SUACHUA_RELATIONS_KHACHHAN foreign key (MAKH)
      references KHACHHANG (MAKH)
go

alter table SUACHUA
   add constraint FK_SUACHUA_RELATIONS_NHANVIEN foreign key (MANV)
      references NHANVIEN (MANV)
go
------------------------------------------------------------------
alter table PHIEUBAOHANH
   add constraint FK_PHIEUBAO_RELATIONS_NHANVIEN foreign key (MANV)
      references NHANVIEN (MANV)
go

alter table PHIEUBAOHANH
   add constraint FK_PHIEUBAO_RELATIONS_CTHDB foreign key (MAHDB, MAH)
      references CTHDB (MAHDB, MAH)
go
------------------------------------------------------------------
alter table BAOHANH
   add constraint FK_BAOHANH_RELATIONS_NHANVIEN foreign key (MANV)
      references NHANVIEN (MANV)
go
------------------------------------------------------------------
alter table BAOHANH
   add constraint FK_BAOHANH_RELATIONS_PHIEUBAOHANH foreign key (IDPBH)
      references PHIEUBAOHANH  (IDPBH)
go
----------------------------------------------------------------
alter table CTHDSC
   add constraint FK_CTHDSC_RELATIONS_SUACHUA foreign key (MASC)
      references SUACHUA (MASC)
go
----------------------------------------------------------------
alter table CTHDSC
   add constraint FK_CTHDSC_RELATIONS_HANG foreign key (MAH)
      references HANG (MAH)
go
---------------------------------------------------------------
alter table NDNHOMND
   add constraint FK_NDNHOMND_RELATIONS_TAIKHOAN foreign key (MANV)
      references TAIKHOANDN (MANV)
go

alter table NDNHOMND
   add constraint FK_NDNHOMND_RELATIONS_NHOMNGUOI foreign key (MANHOM)
      references NHOMNGUOIDUNG (MANHOM)
go
-------------------------------------------------------------
alter table HOADONMOI
   add constraint FK_HOADONMO_RELATIONS_HDB foreign key (MAHDB)
      references HDB (MAHDB)
go

alter table HOADONMOI
   add constraint FK_HOADONMO_RELATIONS_LYDOXUAT foreign key (MALYDO)
      references LYDOXUAT (MALYDO)
go

alter table HOADONMOI
   add constraint FK_HOADONMO_RELATIONS_NHANVIEN foreign key (MANV)
      references NHANVIEN (MANV)
go
-----------------------------------------------------------------
alter table PHANQUYEN
   add constraint FK_PHANQUYE_RELATIONS_MANCHUCNANG foreign key (MACN)
      references MANCHUCNANG (MACN)
go

alter table PHANQUYEN
   add constraint FK_PHANQUYEN_RELATIONS_NHOMNGUOIDUNG foreign key (MANHOM)
      references NHOMNGUOIDUNG (MANHOM)
go
---------------------------------------------------------------
--alter table CTDOIHANG
--   add constraint FK_CTDOIHANG_RELATIONS_HANG foreign key (MAH)
--      references HANG (MAH)
--go

--alter table CTDOIHANG
--   add constraint FK_CTDOIHAN_RELATIONS_HOADONMO foreign key (MAHDB, LANDOI)
--      references HOADONMOI (MAHDB, LANDOI)
--go


------------------------NGAY HIEN TAI-----------------------------------------
set DateFormat DMY
ALTER TABLE NHANVIEN
ADD CONSTRAINT DF_NGAYTAONV DEFAULT GETDATE() FOR NGAYTAONV
-----------------------------------------------------------------
ALTER TABLE NCC
ADD CONSTRAINT DF_NGAYTAONCC DEFAULT GETDATE() FOR NGAYTAONCC
-----------------------------------------------------------------
ALTER TABLE HDM
ADD CONSTRAINT DF_NGAYTAOHDM DEFAULT GETDATE() FOR NGAYTAOHDM
-----------------------------------------------------------------
ALTER TABLE HDB
ADD CONSTRAINT DF_NGAYBAN DEFAULT GETDATE() FOR NGAYBAN
-----------------------------------------------------------------
ALTER TABLE PHIEUBAOHANH
ADD CONSTRAINT DF_NGAYLAPPBH DEFAULT GETDATE() FOR NGAYLAPPBH
-----------------------------------------------------------------
ALTER TABLE SUACHUA
ADD CONSTRAINT DF_NGAYNHANSC DEFAULT GETDATE() FOR NGAYNHANSC
-----------------------------------------------------------------
ALTER TABLE TAIKHOANDN
ADD CONSTRAINT DF_NGAYTAO DEFAULT GETDATE() FOR NGAYTAO
-----------------------------------------------------------------
ALTER TABLE HANGLOI
ADD CONSTRAINT DF_NGAYNHAPHANGLOI DEFAULT GETDATE() FOR NGAYNHAPHANGLOI


--------------------------------------------------------------
--INSERT
--------------------------------------------------------------


--------------------------------------------------------------------------