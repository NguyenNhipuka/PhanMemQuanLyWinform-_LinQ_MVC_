
INSERT INTO KHACHHANG
VALUES(N'Khách thường',N'Không có','0000000000','email@gmail.com',0)
--INSERT NHOMNGUOIDUNG
INSERT INTO NHOMNGUOIDUNG VALUES(N'QUẢN TRỊ HỆ THỐNG',N'IT')
INSERT INTO NHOMNGUOIDUNG VALUES(N'QUẢN LÝ',N'CHỦ CỬA HÀNG')
INSERT INTO NHOMNGUOIDUNG VALUES(N'NHÂN VIÊN KINH DOANH',N'NHÂN VIÊN KINH DOANH')
INSERT INTO NHOMNGUOIDUNG VALUES(N'NHÂN VIÊN KHO',N'NHÂN VIÊN KHO')
SELECT * FROM NHOMNGUOIDUNG
--INSERT MANCHUCNANG
INSERT INTO MANCHUCNANG VALUES
(N'Thêm tài khoản',1),(N'Xóa tài khoản',1),(N'Phân quyền',1),(N'Reset mật khẩu',1),
(N'Thêm xóa sửa nhóm quyền',1),(N'Xem danh sách tài khoản',1),(N'Xem thông tin chi tiết tài khoản',1),
(N'Thêm nhân viên',1),(N'Xem danh sách nhân viên',1),(N'Xuất danh sách nhân viên',1),
(N'Xem thông tin nhân viên',1),(N'Cập nhật thông tin nhân viên',1),(N'Tìm kiếm nhân viên',1),(N'Xóa nhân viên',1),
(N'Thêm chức vụ',1),(N'Xem danh sách chức vụ',1),(N'Xem thông tin chức  vụ',1),(N'Cập nhật thông tin chức vụ',1),(N'Xóa chức vụ',1)
INSERT INTO MANCHUCNANG VALUES
(N'Tìm kiếm nhà cung cấp',1),(N'Xóa thông tin nhà cung cấp',1),(N'Thêm nhà cung cấp',1),(N'Xem thông tin nhà cung cấp',1),
(N'Xuất danh sách nhà cung cấp',1),(N'Cập nhật thông tin nhà cung cấp',1),
(N'Thêm loại hàng',1),(N'Sửa loại hàng',1),(N'Xóa loại hàng',1),(N'Xem loại hàng',1),
(N'Thêm đơn vị tính',1),(N'Sửa đơn vị tính',1),(N'Xóa đơn vị tính',1),(N'Xem đơn vị tính',1),
(N'Thêm danh sách ngân hàng',1),(N'Sửa thông tin ngân hàng',1),(N'Xóa thông tin ngân hàng',1),(N'Xem thông tin ngân hàng',1),
(N'Lập hóa đơn bán',1),(N'Cập nhật hóa đơn bán',1),(N'Xem danh sách  hóa đơn bán',1),
(N'Xem thông tin chi tiết  hóa đơn bán',1),(N'Xuất danh sách hóa đơn',1),(N'Cập nhật trình trạng hóa đơn',1),
(N'Lập phiếu chi',1),(N'Xem phiếu chi',1),
(N'Lập hóa đơn nhập',1),(N'Cập nhật  hóa đơn nhập',1),(N'Xem danh sách  hóa đơn nhập',1),
(N'Xem thông tin chi tiết  hóa đơn nhập',1),(N'Xuất danh sách hóa đơn nhập',1),(N'Cập nhật trình trạng hóa đơn nhập',1),
(N'Lập hóa đơn bảo hành',1),(N'Cập nhật  hóa đơn bảo hành',1),(N'Xem danh sách  hóa đơn bảo hành',1),
(N'Xem thông tin chi tiết  hóa đơn bảo hành',1),(N'Xuất danh sách hóa đơn bảo hành',1),
(N'Lập hóa đơn sửa chữa',1),(N'Cập nhật  hóa đơn sửa chữa',1),(N'Xem danh sách  hóa đơn sửa chữa',1),
(N'Xem thông tin chi tiết  hóa đơn sửa chữa',1),(N'Xuất danh sách hóa đơn sửa chữa',1),
(N'Thêm thông tin thiết bị',1),(N'Sửa thông tin thiết bị',1),(N'Xóa thông tin thiết bị',1),(N'Cập nhật  thông tin thiết bị',1),(N'Lập báo giá',1),
(N'Sửa tài khoản',1)
INSERT INTO MANCHUCNANG VALUES (N'Lập hóa phiếu bảo hành',1),(N'Xem danh sách phiếu bảo hành',1),(N'Xem phiếu bảo hành',1)
INSERT INTO MANCHUCNANG VALUES (N'Thêm khách hàng',1),(N'Xem danh sách khách hàng',1),(N'Xem thông tin khách hàng',1),(N'Cập nhật khách hàng',1),(N'Xóa khách hàng',1),(N'Xuất danh sách khách hàng',1)

update MANCHUCNANG set TENMANHINH =N'Xóa phương thức giao hàng' where MACN=70
update MANCHUCNANG set TENMANHINH =N'Cập nhất phương thức giao hàng' where MACN=69
update MANCHUCNANG set TENMANHINH =N'Thêm phương thức giao hàng' where MACN=68

select * from MANCHUCNANG
--INSERT PHANQUYEN
insert into PHANQUYEN values(1,1),(2,1),(3,1),(4,1),(5,1),(6,1)
insert into PHANQUYEN values(7,1)
INSERT INTO PHANQUYEN VALUES
(1,2),(2,2),(3,2),(4,2),(5,2),(6,2),(7,2),(8,2),(9,2),
(10,2),(11,2),(12,2),(13,2),(14,2),(15,2),(16,2),(17,2),(18,2),(19,2),
(20,2),(21,2),(22,2),(23,2),(24,2),(25,2),(26,2),(27,2),(28,2),(29,2),
(30,2),(31,2),(32,2),(33,2),(34,2),(35,2),(36,2),(37,2),(38,2),(39,2),
(40,2),(41,2),(42,2),(43,2),(44,2),(45,2),(46,2),(47,2),(48,2),(49,2),
(50,2),(51,2),(52,2),(53,2),(54,2),(55,2),(56,2),(57,2),(58,2),(59,2),
(60,2),(61,2),(62,2),(63,2),(64,2),(65,2),(66,2),(67,2),(68,2),(69,2),
(70,2),(71,2),(72,2),(73,2),(74,2),(75,2),(76,2),(77,2),(78,2),(79,2)



INSERT INTO PHANQUYEN VALUES
(16,3),(17,3)

		-------------NHẬP LIỆU---------------
/*==============================================================*/
/* Table: BANK                                                  */
/*==============================================================*/
INSERT INTO BANK
VALUES(N'Không có'),
(N'NGÂN HÀNG ĐÔNG Á'),
(N'NGÂN HÀNG SÀI GÒN'),
(N'NGÂN HÀNG PHƯƠNG ĐÔNG'),
(N'NGÂN HÀNG BẮC Á'),
(N'NGÂN HÀNG VIỆT NAM THƯƠNG TÍN'),
(N'NGÂN HÀNG ĐẠI CHÚNG'),
(N'NGÂN HÀNG BẢO VIỆT'),
(N'NGÂN HÀNG QUỐC TẾ'),
(N'NGÂN HÀNG TIÊN PHONG')
SELECT*FROM BANK
/*==============================================================*/
/* Table: CHUCVU                                                */
/*==============================================================*/
INSERT INTO CHUCVU
VALUES(N'QUẢN LÝ'),(N'NHÂN VIÊN IT')
(N'NHÂN VIÊN KINH DOANH'),
(N'NHÂN VIÊN SỬA CHỮA'),
(N'NHÂN VIÊN THỦ KHO'),
(N'NHÂN VIÊN GIAO HÀNG')

/*==============================================================*/
/* Table: LOAIHANG                                              */
/*==============================================================*/
INSERT INTO LOAIHANG
VALUES(N'IPAP'),
(N'SMARTPHONE'),
(N'HEADPHONE'),
(N'PIN SẠC DỰ PHÒNG'),
(N'CÁP SẠC'),
(N'THẺ NHỚ'),
(N'ỐP LƯNG ĐIỆN THOẠI'),
(N'GẬY TỰ SƯỚNG')
SELECT*FROM LOAIHANG
/*==============================================================*/
/* Table: DVT                                                   */
/*==============================================================*/
INSERT INTO DVT
VALUES(N'CÁI'),
(N'HỘP'),
(N'CHIẾC'),
(N'CẶP')
SELECT * FROM DVT
/*==============================================================*/
/* Table: PTGIAOHANG                                            */
/*==============================================================*/
INSERT INTO PTGIAOHANG
VALUES(N'BƯU ĐIỆN',40000),
(N'CHUYỂN PHÁT NHANH',60000),
(N'Trực tiếp',0)
SELECT*FROM PTGIAOHANG
/*==============================================================*/
/* Table: NHANVIEN                                              */
/*==============================================================*/
SET DATEFORMAT DMY
INSERT INTO NHANVIEN
VALUES('000001',1,N'NGUYỄN THỊ AN THANH',N'TÂN PHÚ','0946358034',N'NỮ','30/08/1996',1,'30/08/2000','123456789012',0,0,0)
SET DATEFORMAT DMY
INSERT INTO NHANVIEN VALUES('000002',1,N'NGUYỄN THỊ YẾN NHI',N'TÂN PHÚ','09012345678',N'NỮ','30/08/1996',1,'30/08/2000','123456789012',5,2,10000000)
INSERT INTO NHANVIEN
VALUES
('000003',2,N'NGUYỄN THỊ THẢO QUYÊN',N'TÂN PHÚ','01234567891',N'NỮ','01/04/1996',1,'30/09/2000','098765432109',4,1.5,7000000),
('000004',4,N'ĐỖ NGỌC ANH',N'PHÚ NHUẬN','09012345670',N'NỮ','25/05/1996',0,'01/02/2001','123456789000',2,1,6000000),
('000005',5,N'TRƯƠNG NAM THÀNH',N'QUẬN 2','09012345678',N'NAM','01/08/1996',0,'10/10/2002','123456789099',1,0.5,3000000)
select * from NHANVIEN
/*==============================================================*/
/* Table: TAIKHOANDN                                             */
/*==============================================================*/

---INSERT TAIKHOANDN -- 123
set dateformat DMY
INSERT INTO TAIKHOANDN VALUES('000001',N'Nguyễn Thị An Thanh','202cb962ac59075b964b07152d234b70',1,0,'30/08/2000','30/08/2000')
INSERT INTO TAIKHOANDN VALUES('000002',N'Nguyễn Thị Yến Nhi','202cb962ac59075b964b07152d234b70',1,0,'30/09/2000','30/09/2000')

/*==============================================================*/
/* Table: NDNHOMND                                            */
/*==============================================================*/
--INSERT NDNHOMND
INSERT INTO NDNHOMND VALUES
('000001',1,N'Admin hệ thống'),
('000002',2,N'Chủ cửa hàng'),
('000003',3,N'Nhân viên')
select * from NDNHOMND
/*==============================================================*/
/* Table: NCC                                                   */
/*==============================================================*/

SET DATEFORMAT DMY
INSERT INTO NCC
VALUES(1,N'THẾ GIỚI DI ĐỘNG','09876543210','TGĐ@GMAIL.COM',N'150 TÂN KỲ TÂN QUÝ, P. TÂN SƠN NHÌ, TÂN PHÚ - TP.HCM','678905432101','12/09/2000',1),
(2,N'LAZADA','01234567890','LZD@GMAIL.COM',N'QUẬN 4 - TP.HCM','234567890123','02/03/2001',1),
(3,N'CELLPHONE','09876543210','CELLPHOONE@GMAIL.COM',N'4B CỘNG HÒA - TP.HCM','123456789098','15/09/2001',1),
(4,N'TECH ONE','09987654321','TECHOONE@GMAIL.COM',N'113 THÁI HÒA - ĐỐNG ĐA- HÀ NỘI','22334455661177','20/09/2001',1),
(5,N'THÀNH NHÂN','01687543256','TN@GMAIL.COM',N'QUẬN 12 - TP.HCM','876590542311','22/09/2001',1),
(6,N'THÀNH ĐẠT','01214567890','TĐ@GMAIL.COM',N'THỦ DẦU MỘT- BÌNH DƯƠNG','090987654321','01/10/2001',1)
SELECT*FROM NCC
/*==============================================================*/
/* Table: HANG                                                  */
/*==============================================================*/
GO
INSERT INTO HANG (MAH,MALOAI,MADVT,MANCC,TENH,SLH,DONGIAM,DONGIAB,TGBH,NSX,TINHTRANGH,MOTAH)
VALUES('SP00000001',2,1,1,N'SAMSUM GALAXY S6 EDGE',10,4950000,4990000,06,'2015',1,N'MÀN HÌNH 5.7 INH, RAM 4GB, ROM 32GB, CAMERA 16MP/8MP'),
('SP00000002',2,1,1,N'SAMSUM GALAXY S6 EDGE ĐEN',5,4950000,4990000,06,'2015',1,N'MÀN HÌNH 5.7 INH, RAM 4GB, ROM 32GB, CAMERA 16MP/8MP'),
('SP00000003',2,1,1,N'SAMSUM GALAXY S6 EDGE XÁM',7,4950000,4990000,06,'2015',1,N'MÀN HÌNH 5.7 INH, RAM 4GB, ROM 32GB, CAMERA 16MP/8MP'),
('SP00000004',2,1,2,N'SAMSUM GALAXY S6 EDGE TRẮNG',4,4950000,4990000,06,'2015',1,N'MÀN HÌNH 5.7 INH, RAM 4GB, ROM 32GB, CAMERA 16MP/8MP'),
('SP00000005',2,1,2,N'APPLE IPHONE 6S VÀNG HỒNG',20,13400000,13490000,12,'2014',1,N'MÀN HÌNH 4.7 INH, RAM 2GB, ROM 64GB, CAMERA 12MP/5MP'),
('SP00000006',2,1,2,N'APPLE IPHONE 6S VÀNG ĐỒNG',10,14400000,14490000,12,'2014',1,N'MÀN HÌNH 4.7 INH, RAM 2GB, ROM 64GB, CAMERA 12MP/5MP'),
('SP00000007',2,1,2,N'APPLE IPHONE 7 PLUS ĐEN',5,23890000,23990000,12,'2016',1,N'MÀN HÌNH 5.5 INH, RAM 3GB, ROM 128 GB, CAMERA 12MP/7MP'),
('SP00000008',2,1,3,N'APPLE IPHONE 7 PLUS ĐỎ',2,23890000,23990000,12,'2016',1,N'MÀN HÌNH 5.5 INH, RAM 3GB, ROM 128 GB, CAMERA 12MP/7MP'),
('SP00000009',2,1,3,N'APPLE IPHONE 8',2,26000000,26290000,12,'2017',1,N'MÀN HÌNH 5.5 INH, RAM 3GB, ROM 64 GB, CAMERA 12MP/7MP'),
('SP00000010',2,1,3,N'SAMSUM GALAXY NOTE 8',1,22300000,22490000,12,'2017',1,N'MÀN HÌNH 6.3 INH, RAM 6GB, ROM 64 GB, CAMERA 12MP/8MP'),
('SP00000011',2,1,4,N'SAMSUM GALAXY NOTE 5',2,9900000,9990000,12,'2015',1,N'MÀN HÌNH 5.7 INH, RAM 4GB, ROM 64 GB, CAMERA 16MP/5MP'),
('SP00000012',2,1,4,N'OPPO F3',2,1050000,10690000,12,'2017',1,N'MÀN HÌNH 6INH FULL HD, RAM 4GB, ROM 64GB, CAMERA 10MP/8MP'),
('SP00000013',2,1,4,N'VIVO Y55S',2,3900000,3990000,12,'2017',1,N'MÀN HÌNH 5.2 INH, RAM 4GB, ROM 64GB, CAMERA 13MP/5MP'),
('SP00000014',2,1,4,N'VIVO V5S',2,9900000,9990000,12,'2017',1,N'MÀN HÌNH 5.5 INH, RAM 4GB, ROM 64GB, CAMERA 20MP/13MP'),
('SP00000015',2,1,4,N'VIVO Y53',2,3300000,3390000,12,'2017',1,N'MÀN HÌNH 5 INH, RAM 2GB, ROM 16GB, CAMERA 8MP/5MP')
SELECT*FROM HANG
/*==============================================================*/
/* Table: CT_HANG                                               */
/*==============================================================*/
INSERT INTO CT_HANG
VALUES('SP00000001',15,2,0.1),
('SP00000002',50,2,0),
('SP00000003',10,2,0),
('SP00000004',5,2,0),
('SP00000005',30,2,0),
('SP00000006',5,2,0.5),
('SP00000007',6,2,0.5),
('SP00000008',10,2,0),
('SP00000009',30,2,0),
('SP00000010',100,2,0),
('SP00000011',21,2,0),
('SP00000012',23,2,0),
('SP00000013',12,2,0),
('SP00000014',11,2,0.1),
('SP00000015',10,2,0)
SELECT*FROM CT_HANG

/*==============================================================*/
/* Table: HDM                                                   */
/*==============================================================*/

SET DATEFORMAT DMY
INSERT INTO HDM
VALUES('M000000001','000002',1,'12/09/2015','12/09/2015',0.1,7260000,0,1),
('M000000002','000002',2,'01/02/2016','13/09/2015',0.1,8580000,1,0),
('M000000003','000002',3,'12/03/2016','15/03/2016',0.1,54450000,0,1),
('M000000004','000002',4,'14/04/2016','12/09/2016',0.1,49060000,1,0),
('M000000005','000002',5,'21/05/2016','12/08/2016',0.1,23100000,0,1)
SELECT*FROM HDM
/*==============================================================*/
/* Table: CTHDM                                                 */
/*==============================================================*/
INSERT INTO CTHDM
VALUES('SP00000015','M000000001',3300000,12,2,6600000,0),
('SP00000013','M000000002',3900000,12,2,7800000,1),
('SP00000001','M000000003',4950000,06,10,49500000,0),
('SP00000010','M000000004',22300000,12,2,44600000,1),
('SP00000012','M000000005',10500000,12,2,21000000,0)
SELECT*FROM CTHDM
/*==============================================================*/
/* Table: PHIEUCHI                                              */
/*==============================================================*/
SET DATEFORMAT DMY
INSERT INTO PHIEUCHI (DIENGIAI,NGAYCHI,MAHDM,MANV,TIEN)
VALUES('','12/09/2015','M000000001','000002',7260000),
(N'','15/03/2016','M000000003','000002',54450000),
(N'','12/08/2016','M000000005','000002',23100000)
SELECT*FROM PHIEUCHI
/*==============================================================*/
/* Table: LYDOXUAT                                              */
/*==============================================================*/
INSERT INTO LYDOXUAT
VALUES(N'Bán hàng mới'),
(N'BÁN hàng xả kho')
/*==============================================================*/
/* Table: KHACHHANG                                             */
/*==============================================================*/
INSERT INTO KHACHHANG
VALUES
(N'ĐỖ LINH ĐAN',N'QUẬN 1 - TP.HCM','09999999999','DANLINH@GMAIL.COM',1),
(N'TRẦN MỸ LINH',N'QUẬN 2 - TP.HCM','01678543290','LINHMY@GMAIL.COM',1),
(N'CAO XUÂN KHÁNH',N'CHỢ LÁCH - BẾN TRE','01234567890','KHANHXUAN@GMAIL.COM',1),
(N'TRANG LÂM TỐ QUYÊN',N'QUẬN 12 - TP.HCM','01232124567','QUYENTO@GMAIL.COM',0),
(N'NGUYỄN MINH MẨN',N'QUẬN PHÚ NHUẬN - TP.HCM','09000000009','MANMINH@GMAIL.COM',0)

SELECT*FROM KHACHHANG
/*==============================================================*/
/* Table: HDB                                                   */
/*==============================================================*/
INSERT INTO HDB (MAHDB,MALYDO,MAKH,MANV,MAPTGH,TENKHB,DIACHIKHB,SDTKHB,NGAYBAN,TINHTRANGHDB,PHIGIAOH,THUTHEMHDB,TRIGIAHDB,NOHDB)
VALUES		('B000000001',1,1,'000002',1,N'ĐỖ LINH ĐAN',N'QUẬN 1 - TP.HCM','09999999999','05/01/2016',2,40000,40000,5070000,0),
			('B000000002',1,2,'000002',1,N'TRẦN MỸ LINH',N'QUẬN 2 - TP.HCM','01678543290','03/02/2016',2,40000,0,10030000,0),
			('B000000003',1,1,'000002',1,N'TRẦN MỸ LINH',N'QUẬN 2 - TP.HCM','01678543290','03/02/2016',2,40000,0,240000,0)

SELECT*FROM HDB
/*==============================================================*/
/* Table: CTHDB                                                 */
/*==============================================================*/
INSERT INTO CTHDB
VALUES('B000000001','SP00000002',1,4990000,0,6,4990000),
('B000000002','SP00000003',1,9990000,0,6,9990000),
('B000000003','SP00000008',1,23990000,0,12,200000)
SELECT * FROM CTHDB
/*==============================================================*/
/* Table: PHIEUBAOHANH                                          */
/*==============================================================*/
INSERT INTO PHIEUBAOHANH
VALUES('B000000001','SP00000002','000002','05/01/2016','123456789012',1),
('B000000002','SP00000003','000002','03/02/2016','098765432100',1),
('B000000003','SP00000008','000001','03/02/2016','123456789010',1)

SELECT*FROM PHIEUBAOHANH
/*==============================================================*/
/* Table: BAOHANH                                          */
/*==============================================================*/
INSERT INTO BAOHANH (IDPBH,NGUYENNHAN,MANV,NGAYLAPBH,NGAYGIAOBH,TINHTRANGBH,SDTNHAN)
VALUES(1,N'LỖI PHẦN MỀM','000003','05/05/2016','05/06/2016',0,'0364595031'),
(2,N'LỖI SẢN XUẤT','000003','23/06/2016','23/07/2016',1,'0695325130'),
(3,N'LỖI GIA CÔNG','000003','21/07/2016','22/07/2016',1,'0964350315')
SELECT * FROM BAOHANH
/*==============================================================*/
/* Table: SUACHUA                                               */
/*==============================================================*/
Set Dateformat DMY
INSERT INTO SUACHUA (MASC,MAKH,MANV,TENKHSC,SDTKHSC,NGAYNHANSC,NGAYGIAOSC,TONGCHIPHISC,TINHTRANGSC)
VALUES('S000000001',1,'000003',N'TRẦN KIẾN MINH','09870000098','22/01/2016','05/02/2016',300000,1),
('S000000002',2,'000003',N'LÂM TINH NHI','09770000098','02/02/2016','22/02/2016',20000,1),
('S000000003',3,'000003',N'TẦN NGUYỆT NHI','09970000098','13/02/2016','15/03/2016',100000,1),
('S000000004',4,'000003',N'PHONG ĐẰNG','09870007098','27/03/2017','28/11/2017',250000,0)
select * from SUACHUA

/*==============================================================*/
/* Table: CTHDSC                                                */
/*==============================================================*/
INSERT INTO CTHDSC (MASC,MAH,TENTBSC,LOISC,MOTASC,CHIPHISC,TINHTRANGCTSC)
VALUES('S000000001','SP00000001',N'SAMSUM GALAXY S6 EDGE',N'LỖI MÀN HÌNH',N'MÀN HÌNH VỠ',300000,1),
('S000000002','SP00000005',N'APPLE IPHONE 6S VÀNG HỒNG',N'LỖI CHỨC NĂNG',N'GỌI KHÔNG ĐƯỢC',20000,1),
('S000000003','SP00000010',N'SAMSUM GALAXY NOTE 8',N'LỖI PHẦN MỀM',N'MÁY BỊ ĐỨNG',100000,1),
('S000000004','SP00000015',N'VIVO Y53',N'LỖI CAMERA',N'CAMERA BỊ MỜ',250000,0)
select * from CTHDSC
