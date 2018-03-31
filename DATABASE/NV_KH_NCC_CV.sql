
-----[dbo].[sp_Staff_Add]
-----[dbo].[sp_STAFF_Getlist]
-----[dbo].[sp_Staff_Getlist_ByKey]
-----[dbo].[[sp_POSITION_Getlist]]
----- [dbo].[sp_Add_CHUCVU] 
----- [dbo].[sp_Update_CHUCVU]
-----[dbo].[sp_Delete_CHUCVU] 
-----[dbo].[sp_GetList_CHUCVU]
-----[dbo].[sp_NCC_Add]
-----[dbo].[sp_NCC_Update]
-----[dbo].[sp_NCC_GetList_Bykey]
-----[dbo].[sp_NCC_GetList]
------[dbo].[sp_NCC_GetListName]
-----[dbo].[sp_NCC_Delete]


CREATE FUNCTION F_CREATE_ID_STAFF()
	RETURNS VARCHAR(6)
	AS
	BEGIN
		DECLARE @ID INT =0;
		if(select COUNT(IDNV) from NHANVIEN) >0
			begin
				SELECT @ID = MAX(IDNV) FROM NHANVIEN
			end
		
		SET @ID =@ID+1;
		IF(@ID <10)
			RETURN '00000'+ CONVERT(varchar(1), @ID);
		IF (@ID >= 10 AND @ID <100) 
			RETURN '0000'+ CONVERT(varchar(2), @ID);
		IF (@ID >=100 AND @ID <1000) 
			RETURN '000'+ CONVERT(varchar(3), @ID);
		IF (@ID >=1000 AND @ID <10000) 
			RETURN '00'+ CONVERT(varchar(4), @ID);
		IF (@ID >=10000 AND @ID <100000) 
			RETURN '0'+ CONVERT(varchar(5), @ID);
		IF (@ID >=100000 AND @ID <1000000) 
			RETURN  CONVERT(varchar(6), @ID);

			RETURN  CONVERT(varchar(6),'000000');
	END
	go

/****** Object:  StoredProcedure [dbo].[sp_Staff_Add]   nguyen thi yen nhi Script Date: 06/10/2017 13:07:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROCEDURE [dbo].[sp_Staff_Add] 
		@TENNV nVARCHAR(50) ='',
		@GIOITINH NVARCHAR(3)='',
		@NGAYSINH DATE ,
		@SDT VARCHAR(11)='',
		@CMND VARCHAR(12)='',
		@DIACHI NVARCHAR(100)='',
		@BACLUONG FLOAT=0,
		@PHUCAP FLOAT=0,
		@LUONG FLOAT=0,
		@CHUCVU INT=0,
		@TRANGTHAI BIT =0,
		@ErrCode int = 0 OUTPUT,
		@ErrMsg NVARCHAR(200) = '' OUTPUT
	AS
	BEGIN
		SELECT @ErrCode = 0, @ErrMsg = N'Thêm nhân viên mới thành công!'
	
		IF  EXISTS (SELECT 1 FROM NHANVIEN WHERE CMND=@CMND)
			BEGIN
				SELECT @ErrCode = 1, @ErrMsg = N'Nhân viên này đã tồn tại!'
				return
			END
		----CREATE ID AUTO FOR STAFF
		DECLARE @MANV VARCHAR(6) ='000000'
		SELECT  @MANV = dbo.F_CREATE_ID_STAFF()
		IF( CONVERT(INT,@MANV) >=1000000)
			BEGIN
				SELECT @ErrCode = 1, @ErrMsg = N'Dữ liệu chứa đã đầy'
				return
			END
		INSERT INTO NHANVIEN (MANV,MACV,TENNV,DIACHINV,SDTNV,GIOITINHNV,NGAYSINH,TRANGTHAINV,CMND,BACLUONG,PHUCAP,LUONG)
		 VALUES(@MANV,@CHUCVU,@TENNV,@DIACHI,@SDT,@GIOITINH,@NGAYSINH,@TRANGTHAI,@CMND,@BACLUONG,@PHUCAP,@LUONG)
	END

	
	
-----------------------------------------------------------------------------------------------------------------------------------

/****** Object:  StoredProcedure [dbo].[sp_STAFF_GetlistExcel] nguyen thi yen nhi   Script Date: 06/10/2017 13:16:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROCEDURE [dbo].[sp_STAFF_GetlistExcel]
	AS
			BEGIN
				SELECT ROW_NUMBER() OVER (ORDER BY (SELECT 1)) AS STT,MANV AS N'Mã',TENNV AS N'Tên nhân viên',				
				TENCV AS N'Chức vụ',
				case when TRANGTHAINV=1 then N'Hoạt động' else N'không hoạt động'end as N'Trạng thái'				
				FROM NHANVIEN, CHUCVU
				where NHANVIEN.MACV=CHUCVU.MACV
			end
---------------------------------------------------------------------
/****** Object:  StoredProcedure [dbo].[sp_STAFF_Getlist]  ******/
	SET ANSI_NULLS ON
		GO
		SET QUOTED_IDENTIFIER ON
		GO
		create PROCEDURE [dbo].[sp_STAFF_Getlist]
			AS
			BEGIN
				SELECT ROW_NUMBER() OVER (ORDER BY (SELECT 1)) AS STT,MANV AS N'Mã',TENNV AS N'Tên nhân viên',CHUCVU.TENCV AS N'Chức vụ',
				DIACHINV AS N'Địa chỉ',SDTNV AS N'số điện thoại',GIOITINHNV AS N'Giới tính',NGAYSINH AS N'Ngày sinh',
				case when TRANGTHAINV=1 then N'Hoạt động' else N'không hoạt động'end as N'Trạng thái', NGAYTAONV AS N'Ngày tạo',
				CMND AS N'Chứng minh nhân dân',BACLUONG AS N'Bậc lương', PHUCAP AS N'Phụ cấp', LUONG AS N'Lương', IDNV AS N'Mã giả'					
				FROM NHANVIEN, CHUCVU
				where NHANVIEN.MACV=CHUCVU.MACV
			end
				
	
----------------------------------------------------------------------------------------------------------------------------------------

/****** Object:  StoredProcedure [dbo].[sp_Staff_Getlist_ByKey]    Script Date: 06/10/2017 13:16:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_Staff_Getlist_ByKey]
		@TENNV NVARCHAR(60) = '',
		@MANV CHAR(6)=''
	AS
	BEGIN
		IF(@TENNV !='' and @MANV ='')
		BEGIN
			SELECT *
			FROM NHANVIEN
			WHERE @TENNV LIKE '%' + @TENNV + '%'	
			RETURN
		END
			SELECT *
			FROM NHANVIEN
			WHERE  @MANV=MANV		
	END
	go

	/****** Object:  StoredProcedure [dbo].[sp_Staff_Getlist_ByKey]    Script Date: 06/10/2017 13:16:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_Staff_Getlist_ByID]
		@MANV CHAR(6)='000000'
	AS
	BEGIN
			SELECT *
			FROM NHANVIEN
			WHERE  @MANV=MANV		
	END
	go

-------------------------------------------------------------------------------------------

/****** Object:  StoredProcedure [dbo].[[sp_POSITION_Getlist]]    Script Date: 06/10/2017 13:16:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_POSITION_Getlist]
	AS
	BEGIN
		SELECT *
		FROM CHUCVU	
	END
/****** Object:  StoredProcedure [dbo].[sp_Staff_Update]   Quyen   ******/

 SET ANSI_NULLS ON
	GO
	SET QUOTED_IDENTIFIER ON
	GO
	create PROCEDURE [dbo].[sp_Staff_Update] 	

		   @MANV                 char(6),		  
		   @MACV                 int,
		   @TENNV                nvarchar(60),
		   @DIACHINV             nvarchar(100),
		   @SDTNV                nvarchar(11),
		   @GIOITINHNV           nvarchar(3),
		   @NGAYSINH             datetime,
		   @TRANGTHAINV          bit,
		   @NGAYTAONV            datetime,
		   @CMND                 varchar(12),
		   @BACLUONG             float,
		   @PHUCAP               float ,
		   @LUONG                float,
		   @ErrCode int = 0 OUTPUT,
		   @ErrMsg NVARCHAR(100) = '' OUTPUT
	AS
	BEGIN
		IF EXISTS (SELECT 1 FROM NHANVIEN WHERE UPPER(TENNV) = UPPER(@TENNV)AND UPPER(MACV) = UPPER(@MACV) AND MANV!=@MANV)
		BEGIN
			SELECT @ErrCode = 1, @ErrMsg = N'Đã tồn tại tên nhân viên này: '+@TENNV + N' này'
			RETURN
		END
		UPDATE NHANVIEN
		SET MACV=@MACV, TENNV=@TENNV, DIACHINV=@DIACHINV, SDTNV=@SDTNV,GIOITINHNV=@GIOITINHNV,
		NGAYSINH=@NGAYSINH, TRANGTHAINV=@TRANGTHAINV, NGAYTAONV=@NGAYTAONV, CMND=@CMND, BACLUONG=@BACLUONG,
		PHUCAP=@PHUCAP, LUONG=@LUONG
		WHERE MANV=@MANV 
		
		SELECT @ErrCode = 0, @ErrMsg = N'Cập nhật thành công nhân viên: '+@TENNV+'('+@MANV+')'
	END
	GO
/****** Object:  StoredProcedure [dbo].[sp_Staff_Delete]     nguyen thi thao quyen   ******/
	SET ANSI_NULLS ON
	GO
	SET QUOTED_IDENTIFIER ON
	GO

	CREATE PROCEDURE [dbo].[sp_Staff_Delete]
	@MANV CHAR(6),
	@ErrCode int = 0 OUTPUT,
	@ErrMsg NVARCHAR(200) = '' OUTPUT
	AS
	BEGIN
		IF EXISTS (SELECT 1 FROM TAIKHOANDN WHERE MANV=@MANV)
		BEGIN
			SELECT @ErrCode = 1, @ErrMsg = N'Nhân viên này đang được sử dụng: '
			RETURN
		END		
		DELETE FROM NHANVIEN
		WHERE MANV=@MANV
		SELECT @ErrMsg =N'Xóa thành công nhân viên có mã: '+@MANV	
	END
	GO
-----------------------------------------------------------------------------------------
/****** Object:  StoredProcedure [dbo].[sp_Add_CHUCVU]   nguyen thi thao quyen  ******/
	SET ANSI_NULLS ON
	GO
	SET QUOTED_IDENTIFIER ON
	GO

	CREATE PROCEDURE [dbo].[sp_Add_CHUCVU]
	@TENCV NVARCHAR(100),
	@ErrCode int = 0 OUTPUT,
	@ErrMsg NVARCHAR(200) = '' OUTPUT
	AS
	BEGIN
		IF EXISTS (SELECT 1 FROM CHUCVU WHERE UPPER(TENCV) = UPPER(@TENCV))
		BEGIN
			SELECT @ErrCode = 1, @ErrMsg = N'Đã tồn tại tên chức vụ: '+@TENCV +' này'
			RETURN
		END
		INSERT INTO CHUCVU VALUES(@TENCV)
		SELECT  @ErrMsg = N'Tạo thành công chức vụ: '+@TENCV
	END
	GO
	/****** Object:  StoredProcedure [dbo].[sp_Update_CHUCVU]     nguyen thi thao quyen   ******/
	SET ANSI_NULLS ON
	GO
	SET QUOTED_IDENTIFIER ON
	GO

	create PROCEDURE [dbo].[sp_Update_CHUCVU]
	@MACV INT,
	@TENCV NVARCHAR(100),
	@ErrCode int = 0 OUTPUT,
	@ErrMsg NVARCHAR(200) = '' OUTPUT
	AS
	BEGIN
		IF NOT EXISTS (SELECT * FROM CHUCVU WHERE MACV=@MACV)
		BEGIN
			SELECT @ErrCode = 1, @ErrMsg = N'Không tồn tại tên chức vụ này'
			RETURN
		END
		
		UPDATE CHUCVU
		SET TENCV= @TENCV WHERE MACV=@MACV
		SELECT  @ErrMsg = N'Cập nhật thành công chức vụ'
	END
	GO

	/****** Object:  StoredProcedure [dbo].[sp_GetList_CHUCVU]     nguyen thi thao quyen   ******/
	SET ANSI_NULLS ON
	GO
	SET QUOTED_IDENTIFIER ON
	GO

	CREATE PROCEDURE [dbo].[sp_GetList_CHUCVU]
	@MACV CHAR(6) ='',
	@ErrCode int = 0 OUTPUT,
	@ErrMsg NVARCHAR(200) = '' OUTPUT
	AS
	BEGIN
		SELECT * FROM CHUCVU 		
	END
	GO
	

	USE [DATABASE_DT]
GO
/****** Object:  StoredProcedure [dbo].[sp_NCC_Add]   nguyen thi yen nhi Script Date: 06/10/2017 13:07:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_NCC_Add] 
		@TENCC NVARCHAR(50) ='',
		@EMAILNCC NVARCHAR(30)='',
		@SDTNCC VARCHAR(11)='',
		@STKNH VARCHAR(13)='',
		@DIACHINCC NVARCHAR(100)='',
		@MANH INT=NULL,
		@TRANGTHAINCC BIT =0,
		@ErrCode int = 0 OUTPUT,
		@ErrMsg NVARCHAR(200) = '' OUTPUT
	AS
	BEGIN
		SELECT @ErrCode = 0, @ErrMsg = N'Thêm nhà cung cấp mới thành công!'
	
		IF  EXISTS (SELECT 1 FROM NCC WHERE TENNCC=@TENCC)
			BEGIN
				SELECT @ErrCode = 1, @ErrMsg = N'Nhà cung cấp này đã tồn tại!'
				return
			END
			
		INSERT INTO NCC(MANH,TENNCC,EMAILNCC,DIACHINCC,SDTNCC,STKBANK,NGAYTAONCC,TRANGTHAINCC)
		VALUES(@MANH,@TENCC,@EMAILNCC,@DIACHINCC,@SDTNCC,@STKNH,GETDATE(),@TRANGTHAINCC)
	END
/****** Object:  StoredProcedure [dbo].[sp_NCC_Update]   nguyen thi yen nhi Script Date: 06/10/2017 13:07:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROCEDURE [dbo].[sp_NCC_Update]
		@MANCC INT,
		@TENCC NVARCHAR(50),
		@EMAILNCC NVARCHAR(30),
		@SDTNCC VARCHAR(11),
		@STKNH VARCHAR(13),
		@DIACHINCC NVARCHAR(100),
		@MANH INT,
		@TRANGTHAINCC BIT =0,
		@ErrCode int = 0 OUTPUT,
		@ErrMsg NVARCHAR(200) = '' OUTPUT
	AS
	BEGIN
		SELECT @ErrCode = 0, @ErrMsg = N'Cập nhật nhà cung cấp thành công!'
	
		IF NOT  EXISTS (SELECT 1 FROM NCC WHERE @MANCC =MANCC)
			BEGIN
				SELECT @ErrCode = 1, @ErrMsg = N'Nhà cung cấp này không tồn tại!'
				return
			END
		UPDATE NCC
		SET TENNCC=@TENCC,SDTNCC=@SDTNCC,EMAILNCC=@EMAILNCC,DIACHINCC=@DIACHINCC,
		STKBANK=@STKNH,TRANGTHAINCC=@TRANGTHAINCC
		WHERE MANCC=@MANCC
	END

	/****** Object:  StoredProcedure [dbo].[sp_NCC_GetListInfo_Bykey]]   nguyen thi yen nhi Script Date: 06/10/2017 13:07:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROCEDURE [dbo].[sp_NCC_GetListInfo_Bykey]
		@MANCC INT=0,
		@TENCC NVARCHAR(50) =''
	AS
	BEGIN
	
		IF(@MANCC = 0 AND @TENCC !='')
			BEGIN
				SELECT *
				FROM NCC
				WHERE @TENCC LIKE '%' + @TENCC + '%'	
				RETURN
			END
		ELSE
			BEGIN
				SELECT  MANCC AS N'Mã',TENNCC AS N'Tên nhà cung cấp',SDTNCC AS N'Số điện thoại',DIACHINCC AS N'Địa chỉ',
				EMAILNCC AS N'Email',STKBANK AS N'Số tài khoản',B.TENNH AS N'Tên ngân hàng',
				TRANGTHAINCC AS N'Trạng thái',NGAYTAONCC AS N'Ngày tạo'
				FROM NCC N,BANK B
				WHERE  MANCC=@MANCC	
			END
		
	END
		/****** Object:  StoredProcedure [dbo].[sp_NCC_GetList_Bykey]  cript Date: 06/10/2017 13:07:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_NCC_GetList_Bykey]
		@MANCC INT=0
	AS
	BEGIN
		DECLARE @SOLAN INT=0
		DECLARE @TONGTIEN FLOAT=0;
		select @SOLAN =COUNT(MANCC), @TONGTIEN= SUM(TONGTIEN) from HDM where MANCC=@MANCC
		
				SELECT *,@SOLAN AS SOLAN ,@TONGTIEN AS TONG
				FROM NCC
				WHERE  MANCC=@MANCC	
			
		
	END
		/****** Object:  StoredProcedure [dbo].[sp_NCC_GetList]   nguyen thi yen nhi Script Date: 06/10/2017 13:07:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROCEDURE [dbo].[sp_NCC_GetList]
	AS
	BEGIN

		SELECT ROW_NUMBER() OVER (ORDER BY (SELECT 1)) AS STT
		, MANCC AS N'Mã',TENNCC AS N'Tên nhà cung cấp',SDTNCC AS N'Số điện thoại',DIACHINCC AS N'Địa chỉ',
		EMAILNCC AS N'Email',STKBANK AS N'Số tài khoản',B.TENNH AS N'Tên ngân hàng',
		TRANGTHAINCC AS N'Trạng thái',NGAYTAONCC AS N'Ngày tạo'
		FROM NCC N,BANK B
		WHERE N.MANH=B.MANH		
	END


		/****** Object:  StoredProcedure [dbo].[sp_NCC_GetListName]   nguyen thi yen nhi Script Date: 06/10/2017 13:07:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROCEDURE [dbo].[sp_NCC_GetListName]
	AS
	BEGIN

		SELECT MANCC,TENNCC 
		FROM NCC N	
	END
/****** Object:  StoredProcedure [dbo].[sp_NCC_Delete]   nguyen thi yen nhi Script Date: 06/10/2017 13:07:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROCEDURE [dbo].[sp_NCC_Delete]
	@MANCC INT,
	@ErrCode int = 0 OUTPUT,
	@ErrMsg NVARCHAR(200) = '' OUTPUT
	AS
	BEGIN
		SET NOCOUNT ON
		IF EXISTS (SELECT * FROM HDM WHERE MANCC=@MANCC)
		BEGIN
			SELECT @ErrCode = 1, @ErrMsg = N'Mã nhà cung cấp này đang được sử dụng: '
			RETURN
		END
		DELETE FROM NCC
		WHERE MANCC=@MANCC
		SELECT @ErrCode = 0,@ErrMsg =N'Xóa thành nhà cung cấp có mã: '+@MANCC
	END

/****** Object:  StoredProcedure [dbo].[sp_NCC_GetListExcel]    Script Date: 5/11/2017 11:42:03 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROCEDURE [dbo].[sp_NCC_GetListExcel]
	AS
	BEGIN
		SELECT ROW_NUMBER() OVER (ORDER BY (SELECT 1)) AS STT, MANCC AS N'Mã',TENNCC AS N'Tên nhà cung cấp',
		DIACHINCC AS N'Địa chỉ',	
		case when  TRANGTHAINCC=1 then N'Hoạt động' else N'không hoạt động'end as N'Trạng thái'			
		FROM NCC 		
	END
-----------------------------------------------------------------------------------------------------------------------------------

/****** Object:  StoredProcedure [dbo].[sp_KHACHHANG_Getlist] NGUYEN THI THAO QUYEN   Script Date: 22/10/2017 10:48 ******/
--SET ANSI_NULLS ON
--GO
--SET QUOTED_IDENTIFIER ON
--GO
--CREATE PROCEDURE [dbo].[sp_KHACHHANG_Getlist]
--	AS
--	BEGIN
--		SELECT ROW_NUMBER() OVER (ORDER BY (SELECT 1)) AS STT,*
--		FROM KHACHHANG	
--	END

--
/****** Object:  StoredProcedure [dbo].[sp_KHACHHANG_Getlist] NGUYEN THI THAO QUYEN   Script Date: 22/10/2017 10:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROCEDURE [dbo].[sp_KHACHHANG_Getlist]
	AS
	BEGIN
		SELECT ROW_NUMBER() OVER (ORDER BY (SELECT 1)) AS STT, MAKH AS N'Mã',TENKH AS N'Tên khách hàng',DIACHIKH AS N'Địa chỉ', 
		SDTKH AS N'Số điện thoại', EMAILKH  AS N'Email', 
				case when  TINHTRANGKH=1 then N'Hoạt động' else N'không hoạt động'end as N'Trạng thái'							
				FROM KHACHHANG	
				where 	MAKH <>1		
	END
	go
--------------------------------------------------
/****** Object:  StoredProcedure [dbo].[sp_KHACHHANG_Add]   nguyen thi thao quyen Script Date: 22/10/2017 11:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROCEDURE [dbo].[sp_KHACHHANG_Add] 

		@TENKH                nvarchar(50)='',
		@DIACHIKH             nvarchar(100)='',
		@SDTKH                varchar(11)='',
		@EMAILKH              varchar(30)='',
		@TINHTRANGKH		INT=NULL,
		@ErrCode int = 0 OUTPUT,
		@ErrMsg NVARCHAR(200) = '' OUTPUT
	AS
	BEGIN
		SELECT @ErrCode = 0, @ErrMsg = N'Thêm khách hàng mới thành công!'
	
		IF  EXISTS (SELECT 1 FROM KHACHHANG WHERE UPPER(EMAILKH)= UPPER(@EMAILKH))
			BEGIN
				SELECT @ErrCode = 1, @ErrMsg = N'Email của  khách hàng này đã tồn tại!'
				return
			END
			
		INSERT INTO KHACHHANG(TENKH,DIACHIKH,SDTKH, EMAILKH,TINHTRANGKH)
		VALUES(@TENKH,@DIACHIKH,@SDTKH,@EMAILKH,LOWER(@TINHTRANGKH))
	END	
/****** Object:  StoredProcedure [dbo].[sp_KHACHHANG_GetList_Bykey] ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROCEDURE [dbo].[sp_KHACHHANG_GetList_Bykey]
		@MAKH INT=0
	AS
	BEGIN
		DECLARE @SOLAN INT=0
		DECLARE @TONGTIEN FLOAT=0;
		select @SOLAN =COUNT(MAKH), @TONGTIEN= SUM(TRIGIAHDB) from HDB where MAKH=@MAKH
		
				SELECT *,@SOLAN AS SOLAN ,@TONGTIEN AS TONG
				FROM KHACHHANG
				WHERE  MAKH=@MAKH		
		
	END
/****** Object:  StoredProcedure [dbo].[sp_KHACHHANG_Delete]   ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_KHACHHANG_Delete]
	@MAKH INT,
	@ErrCode int = 0 OUTPUT,
	@ErrMsg NVARCHAR(200) = '' OUTPUT
	AS
	BEGIN
		SET NOCOUNT ON
		IF EXISTS (SELECT 1 FROM HDB WHERE MAKH=@MAKH)
		BEGIN
			SELECT @ErrCode = 1, @ErrMsg = N'Mã khách hàng này đang được sử dụng: '
			RETURN
		END
		DELETE FROM KHACHHANG
		WHERE MAKH=@MAKH
		SELECT @ErrCode = 0,@ErrMsg =N'Xóa thành công khách hàng có mã: '+@MAKH
	END
/****** Object:  StoredProcedure [dbo].[sp_KHACHHANG_Update]   Quyen   ******/

 SET ANSI_NULLS ON
	GO
	SET QUOTED_IDENTIFIER ON
	GO
	create PROCEDURE [dbo].[sp_KHACHHANG_Update] 
		    @MAKH INT,
			@TENKH        nvarchar(50),
			@DIACHIKH     nvarchar(100),
			@SDTKH        varchar(11),
			@EMAILKH      varchar(30),
			@TINHTRANGKH  INT,		
		    @ErrCode int = 0 OUTPUT,
		    @ErrMsg NVARCHAR(100) = '' OUTPUT
	AS
	BEGIN
		SELECT @ErrCode = 0, @ErrMsg = N'Cập nhật khách hàng thành công!'	
		IF NOT  EXISTS (SELECT 1 FROM KHACHHANG WHERE @MAKH=MAKH)
			BEGIN
				SELECT @ErrCode = 1, @ErrMsg = N'Khách hàng này không tồn tại!'
				return
			END
		IF  EXISTS (SELECT * FROM KHACHHANG WHERE @MAKH !=MAKH and EMAILKH=@EMAILKH)
			BEGIN
				SELECT @ErrCode = 1, @ErrMsg = N'Email khách hàng này đã tồn tại!'
				return
			END
		UPDATE KHACHHANG
		SET TENKH=@TENKH, DIACHIKH=@DIACHIKH, SDTKH=@SDTKH, EMAILKH=@EMAILKH, TINHTRANGKH=@TINHTRANGKH
		WHERE MAKH=@MAKH		
	END
	GO