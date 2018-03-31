------[dbo].[sp_PHIEUCHI_Add]
------[dbo].[sp_PHIEUCHI_Update] 
------[dbo].[sp_PHIEUCHI_Getlist]
------[dbo].[sp_PHIEUCHI_Getlist_ByKey]
------[dbo].[sp_PHIEUCHI_Delete] 
------[dbo].[sp_PHIEUCHI_GetlistInfo_ByMaHDM]

------[dbo].[sp_[sp_CTHDM_Add]
------FUNCTION F_CREATE_ID_HDM()
------[dbo].[sp_HDM_Add]
------[dbo].[sp_HDM_Update] 
------[dbo].[sp_HDM_Update_TinhTrangHDB]
------[dbo].[sp_HDM_Getlist]
------[dbo].[sp_HDM_Getlist_Info]
------[dbo].[sp_HDM_Getlist_ByKey]
------[dbo].[sp_HDM_Getlist_TTThanhToan]
------[dbo].[sp_HDM_Update_HDB]
------[dbo].[[[[sp_HDM_GetlistNgay]]]

------[dbo].[sp_HDB_Add]
------[dbo].sp_HDB_Update_HDB
------[dbo].[sp_HDB_Getlist]
------[dbo].[sp_HDB_GetlistThanhToan]
------[dbo].[sp_HDB_GetlistNO]
------[dbo].[sp_HDB_GetlistInfoByMHDB]
------[dbo].[sp_HDB_Getlist_Info]
------[dbo].[sp_HDB_Getlist_ByKey]
------[dbo].[sp_HDB_Getlist_TTThanhToan]
------[dbo].[sp_HDB_Update_TinhTrangHDB]


------[dbo].[sp_CTHDB_Add]
------[dbo].[sp_CTHDB_Update] 
------[dbo].[sp_CTHDB_Getlist]
------[dbo].[sp_CTHDB_Getlist_Info]
------[dbo].[sp_CTHDB_Getlist_ByKey]
------[dbo].[sp_CTHDB_Getlist_TTThanhToan]


------[dbo].[sp_PTGH_Add]
------[dbo].[sp_PTGH_Update] 
------[dbo].[sp_PTGH_Getlist]
------[dbo].[sp_PTGH_Getlist_Info]
------[dbo].[sp_PTGH_Getlist_ByKey]
------[dbo].[sp_PTGH_Getlist_TTThanhToan]
------
------
------
------
------
------
------------------------------------------------------------------------------------------------------------
	/****** Object:  StoredProcedure [dbo].[sp_PHIEUCHI_Add]     ******/
	SET ANSI_NULLS ON
	GO
	SET QUOTED_IDENTIFIER ON
	GO
	create PROCEDURE [dbo].[sp_PHIEUCHI_Add] 
			@DIENGIAI NVARCHAR(100) ='Thanh toán',			
			@MAHDM CHAR(10),
			@MANV CHAR(6),
			@TIEN FLOAT,		
			@ErrMsg NVARCHAR(200) = '' OUTPUT
		AS
		BEGIN	
			INSERT INTO PHIEUCHI(DIENGIAI,NGAYCHI,MAHDM,MANV,TIEN) VALUES(@DIENGIAI,getdate(), @MAHDM, @MANV,@TIEN)

			DECLARE @DATHANHTOAN float =0;
			DECLARE @TongTienHDM float =0;
			SELECT @DATHANHTOAN=SUM(TIEN) FROM PHIEUCHI WHERE MAHDM =@MAHDM
			SELECT @TongTienHDM =TONGTIEN FROM HDM WHERE MAHDM=@MAHDM
			IF(@TongTienHDM = @DATHANHTOAN)
				BEGIN
					UPDATE HDM SET TINHTRANGTTOAN =1 WHERE MAHDM=@MAHDM
				END
				set @DATHANHTOAN = @TongTienHDM - @DATHANHTOAN;		
			SELECT   @ErrMsg =N'Hóa đơn mua : ' + @MAHDM + N' còn nợ :  '+CONVERT(varchar(20),@DATHANHTOAN)
		END
		GO
	------------------------------------------------------------------------------------------
	/****** Object:  StoredProcedure [dbo].[sp_PHIEUCHI_Update]     ******/
	SET ANSI_NULLS ON
	GO
	SET QUOTED_IDENTIFIER ON
	GO
	CREATE PROCEDURE [dbo].[sp_PHIEUCHI_Update]
			@MAPCHDM INT,
			@DIENGIAI NVARCHAR(100) ='',			
			@NGAYCHI DATETIME,
			@MAHDM CHAR(6),
			@MANV CHAR(6),			
			@ErrCode int = 0 OUTPUT,
			@ErrMsg NVARCHAR(200) = '' OUTPUT
	AS
	BEGIN
		IF NOT EXISTS (SELECT 1 FROM PHIEUCHI WHERE UPPER(DIENGIAI) = UPPER(@DIENGIAI))
		BEGIN
			SELECT @ErrCode = 1, @ErrMsg = N'Không tồn tại tên PHIẾU CHI: '+@DIENGIAI +' này'
			RETURN
		END
		Declare @DIENGIAI_CU nvarchar(100)
		SELECT @DIENGIAI_CU = @DIENGIAI FROM PHIEUCHI WHERE MAPCHDM = @MAPCHDM
		UPDATE PHIEUCHI
		SET DIENGIAI= @DIENGIAI
		SELECT  @ErrMsg = N'Cập nhật thành công PHIẾU CHI: '+@DIENGIAI_CU +' thành '+@DIENGIAI
	END
	GO		
	---------------------------------------------------------------------------------------------------------------
	/****** Object:  StoredProcedure [dbo].[sp_PHIEUCHI_Getlist]    ******/
	SET ANSI_NULLS ON
	GO
	SET QUOTED_IDENTIFIER ON
	GO
	alter PROCEDURE [dbo].[sp_PHIEUCHI_Getlist]
		AS
		BEGIN
			SELECT ROW_NUMBER() OVER (ORDER BY (SELECT 1)) AS STT,MAPCHDM AS N'Mã phiếu chi',MAHDM AS N'Mã hóa đơn mua',
			p.MANV AS N'Mã nhân viên',TENNV as N'Tên nhân viên' ,TIEN AS N'Tiền',CONVERT(date,NGAYCHI) AS N'Ngày chi',DIENGIAI AS N'Diễn giải'
			FROM PHIEUCHI p,NHANVIEN N
			WHERE p.MANV = N.MANV
		end
	go
	/****** Object:  StoredProcedure [dbo].[sp_PHIEUCHI_Getlist_ByKey]   ******/
	SET ANSI_NULLS ON
	GO
	SET QUOTED_IDENTIFIER ON
	GO
	CREATE PROCEDURE [dbo].[sp_PHIEUCHI_Getlist_ByKey]
			@DIENGIAI NVARCHAR(100) = '',
			@MAPCHDM INT
		AS
		BEGIN
			IF(@DIENGIAI !='' AND @MAPCHDM='')
			BEGIN
				SELECT *
				FROM PHIEUCHI
				WHERE @DIENGIAI LIKE '%' + @DIENGIAI + '%'	
				RETURN
			END
				SELECT *
				FROM PHIEUCHI
				WHERE  @MAPCHDM=MAPCHDM		
		END	
	GO
	---------------------------------------------------------------

	/****** Object:  StoredProcedure [dbo].[sp_[sp_CTHDM_Add]  NGUYỄN THỊ YẾN NHI    ******/
	SET ANSI_NULLS ON
	GO
	SET QUOTED_IDENTIFIER ON
	GO
	CREATE PROCEDURE [dbo].[sp_CTHDM_Add]
		@MAH                  char(10),
		@MAHDM                char(10),
		@DONGIAHM             float ,
		@TGBAOHANH            int ,
		@SLUONGHM				INT,
		@THANHTIEN			FLOAT,
		@TRANGTHAI tinyint =0
	AS
	BEGIN
		INSERT INTO CTHDM VALUES(@MAH,@MAHDM,@DONGIAHM,@TGBAOHANH,@SLUONGHM,@THANHTIEN,@TRANGTHAI)
		IF(@TRANGTHAI=0) RETURN
		UPDATE HANG SET SLH=SLH+@SLUONGHM WHERE MAH=@MAH
	END
	GO
	
	/****** Object:  StoredProcedure [dbo].[sp_CTHDM_GetlistInfo] nguyen thi yen nhi   ******/
	SET ANSI_NULLS ON
	GO
	SET QUOTED_IDENTIFIER ON
	GO
	CREATE PROCEDURE [dbo].sp_CTHDM_GetlistInfo_ByMaHDM
		@MAHDM CHAR(10)
		AS
		BEGIN
			SELECT ROW_NUMBER() OVER (ORDER BY (SELECT 1)) AS STT,
			MAH AS N'Mã hàng',
			DONGIAHM AS N'Đơn giá',TGBAOHANH AS N'Thời gian bảo hành',SLUONGHM AS N'Số lượng nhập',THANHTIEN AS N'Thành tiền'
			FROM CTHDM
			where MAHDM=@MAHDM
		END
		GO	
	 /****** Object:  StoredProcedure FUNCTION F_CREATE_ID_HDM() NGUYEN THI YEN NHI    ******/
 
CREATE FUNCTION F_CREATE_ID_HDM()
	RETURNS CHAR(10)
	AS
	BEGIN
		DECLARE @ID INT =0;
		if(select COUNT(ID) from HDM) >0
			begin
				SELECT @ID = MAX(ID) FROM HDM
			end
		else set @ID  =0		
		SET @ID =@ID+1;
		IF(@ID <10)
			RETURN CONCAT('M00000000', @ID);
		IF (@ID <100)
			RETURN CONCAT('M0000000', @ID);
		IF (@ID<1000) 
			RETURN CONCAT('M000000', @ID);
		IF (@ID <10000) 
			RETURN CONCAT('M00000',@ID);
		IF (@ID <100000) 
			RETURN CONCAT('M0000',@ID);
		IF (@ID <1000000) 
			RETURN CONCAT('M000',@ID);
		IF (@ID<10000000) 
			RETURN CONCAT('M00',@ID);
		IF (@ID <100000000) 
			RETURN CONCAT('M0',@ID);
			RETURN CONCAT('0000000000',@ID);
	END
	GO
/****** Object:  StoredProcedure [dbo].[sp_HDM_Add]  NGUYỄN THỊ YẾN NHI    ******/
	SET ANSI_NULLS ON
	GO
	SET QUOTED_IDENTIFIER ON
	GO
	CREATE PROCEDURE [dbo].[sp_HDM_Add]
	   @MANV                 char(6),
	   @MANCC                INT,
	   @HANNOHDM             datetime ,
	   @GTGTHDM				FLOAT=0.05,
	   @TONGTIEN             float=0,
	   @TINHTRANGHDM         tinyint=0,
	   @TINHTRANGTTOAN		bit=0,
		@ErrCode int = 0 OUTPUT,
		@ErrMsg NVARCHAR(100) = '' OUTPUT,
		@MAHDNEW CHAR(10) = '0000000000' OUTPUT
	AS
	BEGIN
		
		----CREATE ID AUTO FOR HANG
		DECLARE @MAHDM CHAR(10) ='0000000000'
		SELECT  @MAHDM = DBO.F_CREATE_ID_HDM()
		IF( CONVERT(char,@MAHDM) = '0000000000')
			BEGIN
				SELECT @ErrCode = 1, @ErrMsg = N'Dữ liệu chứa đã đầy'
				return
			END

		INSERT INTO HDM (MAHDM,MANV,MANCC,HANNOHDM,GTGTHDM,TONGTIEN,TINHTRANGHDM,TINHTRANGTTOAN)
		VALUES (@MAHDM,@MANV,@MANCC,@HANNOHDM,@GTGTHDM,@TONGTIEN,@TINHTRANGHDM,@TINHTRANGTTOAN)
		SELECT @ErrCode = 0, @ErrMsg = N'Cất hóa đơn thành công',@MAHDNEW=@MAHDM
	END
	GO



/****** Object:  StoredProcedure [dbo].[sp_HDM_Update_TinhTrangHDM] NGUYỄN THỊ YẾN NHI    ******/
	SET ANSI_NULLS ON
	GO
	SET QUOTED_IDENTIFIER ON
	GO
	CREATE PROCEDURE [dbo].[sp_HDM_Update_TinhTrangHDM]
	   @MAHDM                 char(10),
	   @TINHTRANGHDM         tinyint=0,
		@ErrCode int = 0 OUTPUT,
		@ErrMsg NVARCHAR(100) = '' OUTPUT
		
	AS
	BEGIN
		UPDATE HDM
		SET TINHTRANGHDM=@TINHTRANGHDM
		WHERE MAHDM=@MAHDM
		SELECT @ErrCode = 0, @ErrMsg = N'Cập nhật trình trạng hóa đơn thành công'
	END
	GO



/****** Object:  StoredProcedure [dbo].[sp_HDM_Update_TinhTrangTTHDM] NGUYỄN THỊ YẾN NHI    ******/
	SET ANSI_NULLS ON
	GO
	SET QUOTED_IDENTIFIER ON
	GO
	create PROCEDURE [dbo].sp_HDM_Update_TinhTrangTTHDM
	   @MAHDM                 char(6),
	   @HANNOHDM             datetime=null ,
	   @TINHTRANGHDM         tinyint=0,
	   @TINHTRANGTTOAN		bit=1,--da thanh toan
		@ErrCode int = 0 OUTPUT,
		@ErrMsg NVARCHAR(100) = '' OUTPUT
		
	AS
	BEGIN
		
		DECLARE @TTHDM INT =0;
		SELECT @TTHDM= TINHTRANGHDM FROM HDM WHERE MAHDM=@MAHDM
		---NẾU hóa đơn đã nhập rồi thì k cho sửa trình trạng hóa đơn lại
		if(@TTHDM =1 AND @TINHTRANGHDM <>@TTHDM)
		BEGIN
			 SELECT @ErrCode = 1, @ErrMsg = N'Hóa đơn này đã được xử lý.'+'\n'+N' Không thể sửa lại trình trạng hóa đơn này'
			 return
		END
		declare @hanno date
		select @hanno=HANNOHDM from HDM where MAHDM=@MAHDM
		if(DATEDIFF(day,@hanno,@HANNOHDM)<0)
			begin
				 SELECT @ErrCode = 1, @ErrMsg = N'Hạn nợ mới phải sau hạn nợ cũ'
					return
			end

		UPDATE HDM
		SET TINHTRANGHDM=@TINHTRANGHDM,@TINHTRANGTTOAN=@TINHTRANGHDM,HANNOHDM=@HANNOHDM
		WHERE MAHDM=@MAHDM
		SELECT @ErrCode = 0, @ErrMsg = N'Cập nhật hóa đơn thành công'
	END
	GO
/****** Object:  StoredProcedure [dbo].[sp_HDM_Getlist]    ******/
	SET ANSI_NULLS ON
	GO
	SET QUOTED_IDENTIFIER ON
	GO
	alter PROCEDURE [dbo].[sp_HDM_Getlist]
		AS
		BEGIN
			SELECT ROW_NUMBER() OVER (ORDER BY (SELECT 1)) AS STT,MAHDM AS N'Mã',TENNCC AS N'Nhà cung cấp',
			TENNV AS N'Nhân viên phụ trách',CONVERT(date,NGAYTAOHDM) AS N'Ngày tạo',GTGTHDM AS N'Thuế',
			 TONGTIEN  AS N'Tổng tiền'
			,case when TINHTRANGTTOAN=1 then N'Đã thanh toán' else N'Nợ' end AS N'Tình trạng thanh toán',
			CONVERT(date,HANNOHDM) AS N'Hạn thanh toán',
			case when TINHTRANGHDM=1 then N'Đã xử lý' when TINHTRANGHDM=2 then N'Đã hủy'else N'Chưa xử lý' end  AS N'Tình trạng hóa đơn mua'
			FROM HDM H,NHANVIEN N ,NCC C
			WHERE H.MANV=N.MANV AND H.MANCC=C.MANCC
		END	
	GO

	/****** Object:  StoredProcedure [dbo].[sp_HDM_GetlistDetail]    ******/
	SET ANSI_NULLS ON
	GO
	SET QUOTED_IDENTIFIER ON
	GO
	create PROCEDURE [dbo].sp_HDM_GetlistDetail
		AS
		BEGIN
			SELECT ROW_NUMBER() OVER (ORDER BY (SELECT 1)) AS STT,MAHDM AS N'Mã',TENNCC AS N'Nhà cung cấp',
			TENNV AS N'Nhân viên phụ trách',CONVERT(date,NGAYTAOHDM) AS N'Ngày tạo',GTGTHDM AS N'Thuế',
			 TONGTIEN  AS N'Tổng tiền',(select ISNULL(sum(PHIEUCHI.TIEN),0) from PHIEUCHI where PHIEUCHI.MAHDM=H.MAHDM) AS N'Đã thanh toán',
			 ((select TONGTIEN from HDM where HDM.MAHDM=H.MAHDM)-(select ISNULL(sum(PHIEUCHI.TIEN),0) from PHIEUCHI where PHIEUCHI.MAHDM=H.MAHDM)) as N'Còn nợ'
			,case when TINHTRANGTTOAN=1 then N'Đã thanh toán' else N'Nợ' end AS N'Tình trạng thanh toán',
			CONVERT(date,HANNOHDM) AS N'Hạn thanh toán',
			case when TINHTRANGHDM=1 then N'Đã xử lý' when TINHTRANGHDM=2 then N'Đã hủy'else N'Chưa xử lý' end  AS N'Tình trạng hóa đơn mua'
			FROM HDM H,NHANVIEN N ,NCC C
			WHERE H.MANV=N.MANV AND H.MANCC=C.MANCC
		END	
	GO
	/****** Object:  StoredProcedure [dbo].[sp_HDM_Getlist_TTThanhToan]  nguyen thi yen nhi  ******/
	SET ANSI_NULLS ON
	GO
	SET QUOTED_IDENTIFIER ON
	GO
	CREATE PROCEDURE [dbo].sp_HDM_Getlist_TTThanhToan
	@TinhTrang bit =0
		AS
		BEGIN
			SELECT ROW_NUMBER() OVER (ORDER BY (SELECT 1)) AS STT,MAHDM AS N'Mã',TENNCC AS N'Nhà cung cấp',
			TENNV AS N'Nhân viên phụ trách',CONVERT(date,NGAYTAOHDM) AS N'Ngày tạo',GTGTHDM AS N'Thuế',TONGTIEN AS N'Tổng tiền'
			,case when TINHTRANGTTOAN=1 then N'Đã thanh toán' else N'Nợ' end AS N'Tình trạng thanh toán',
			CONVERT(date,HANNOHDM) AS N'Hạn thanh toán',
			case when TINHTRANGHDM=1 then N'Đã xử lý' when TINHTRANGHDM=2 then N'Đã hủy'else N'Chưa xử lý' end  AS N'Tình trạng hóa đơn mua'
			FROM HDM H,NHANVIEN N ,NCC C
			WHERE H.MANV=N.MANV AND H.MANCC=C.MANCC and TINHTRANGTTOAN= @TinhTrang
		END	
		GO
/****** Object:  StoredProcedure [dbo].[sp_HDM_GetlistInfo] nguyen thi yen nhi   ******/
	SET ANSI_NULLS ON
	GO
	SET QUOTED_IDENTIFIER ON
	GO
	alter PROCEDURE [dbo].sp_HDM_GetlistInfo_ByMaHDM
	@MAHDM CHAR(10)
		AS
		BEGIN
			
			SELECT ROW_NUMBER() OVER (ORDER BY (SELECT 1)) AS STT,H.MAHDM AS N'Mã',TENNCC AS N'Nhà cung cấp',
			TENNV AS N'Nhân viên phụ trách',CONVERT(date,NGAYTAOHDM) AS N'Ngày tạo',GTGTHDM AS N'Thuế',TONGTIEN AS N'Tổng tiền'
			,case when TINHTRANGTTOAN=1 then N'Đã thanh toán' else N'Nợ' end  AS N'Tình trạng thanh toán',
			CONVERT(date,HANNOHDM) AS N'Hạn thanh toán',
			 TINHTRANGHDM AS N'Tình trạng hóa đơn mua'
			FROM HDM H,NHANVIEN N ,NCC C 
			where H.MANV=N.MANV AND H.MANCC=C.MANCC AND MAHDM=@MAHDM
		END	
		GO
/****** Object:  StoredProcedure [dbo].[sp_HDM_GetlistInfo] nguyen thi yen nhi   ******/
	SET ANSI_NULLS ON
	GO
	SET QUOTED_IDENTIFIER ON
	GO
	create PROCEDURE [dbo].sp_HDM_Getlist_ByMaHDM_Export
	@MAHDM CHAR(10)
		AS
		BEGIN
			DECLARE @DATRA INT =-1;
			SELECT @DATRA= SUM(TIEN) FROM PHIEUCHI WHERE MAHDM=@MAHDM
			
			SET  @DATRA =ISNULL(@DATRA,0)
			SELECT H.MAHDM,TENNCC,
			TENNV,NGAYTAOHDM,GTGTHDM,TONGTIEN
			,case when TINHTRANGTTOAN=1 then N'Đã thanh toán' else N'Còn nợ' end  AS N'TTTT',
			CONVERT(date,HANNOHDM) AS N'HANTT',
			 case when TINHTRANGHDM=1 then N'Đã xử lý' when TINHTRANGHDM=2 then N'Đã hủy'else N'Chưa xử lý' end  AS N'TINHTRANGHD',
			 @DATRA as ' DATRA',TENH,CT.MAH,TENDVT,CT.SLUONGHM,CT.DONGIAHM,CT.THANHTIEN
			FROM HDM H,NHANVIEN N ,NCC C ,CTHDM CT,HANG HH,DVT DV
			where H.MANV=N.MANV AND H.MANCC=C.MANCC AND CT.MAHDM=H.MAHDM AND CT.MAH=HH.MAH AND DV.MADVT=HH.MADVT
			 AND H.MAHDM=@MAHDM 
		END
		GO

		exec sp_HDM_Getlist_ByMaHDM_Export 'M000000004'
		/****** Object:  StoredProcedure [dbo].[[[[sp_HDM_GetlistNgay]]]]    ******/
	SET ANSI_NULLS ON
	GO
	SET QUOTED_IDENTIFIER ON
	GO
	alter PROCEDURE [dbo].[sp_HDM_GetlistNgay]
	@FromDate DATE = NULL ,
	@ToDate DATE = NULL
		AS
		BEGIN 
			SELECT ROW_NUMBER() OVER (ORDER BY (SELECT 1)) AS STT,H.MAHDM AS N'Mã',TENNCC AS N'Nhà cung cấp',
			TENNV AS N'Nhân viên phụ trách',CONVERT(date,NGAYTAOHDM )AS N'Ngày tạo',GTGTHDM AS N'Thuế',TONGTIEN AS N'Tổng tiền'
			,case when TINHTRANGTTOAN=1 then N'Đã thanh toán' else N'Nợ' end  AS N'Tình trạng thanh toán',
			CONVERT(date,HANNOHDM )AS N'Hạn thanh toán',
			 TINHTRANGHDM AS N'Tình trạng hóa đơn mua'
			FROM HDM H,NHANVIEN N ,NCC C 
			where H.MANV=N.MANV AND H.MANCC=C.MANCC AND CAST(H.NGAYTAOHDM AS DATE) BETWEEN @FromDate AND @ToDate
		END	
GO
/****** Object:  StoredProcedure [dbo].[sp_PHIEUCHI_GetlistInfo_ByMaHDM] nguyen thi yen nhi   ******/
	SET ANSI_NULLS ON
	GO
	SET QUOTED_IDENTIFIER ON
	GO
	CREATE PROCEDURE [dbo].[sp_PHIEUCHI_GetlistInfo_ByMaHDM]
		@MAHDM CHAR(10)
		AS
		BEGIN
			SELECT ROW_NUMBER() OVER (ORDER BY (SELECT 1)) AS STT,
			MAPCHDM AS N'Mã phiếu chi',MAHDM AS N'Mã hóa đơn mua',
			CONVERT(date,NGAYCHI) AS N'Ngày chi',TIEN AS N'Tiền chi',MANV AS N'Mã nhân viên ',
			DIENGIAI AS N'Diễn giải'
			FROM PHIEUCHI
			where MAHDM=@MAHDM
		END	

		GO

		/****** Object:  StoredProcedure [dbo].[sp_PHIEUCHI_Sum_ByMaHDM] nguyen thi yen nhi   ******/
	CREATE FUNCTION F_PHIEUCHI_Sum_ByMaHDM(@MAHDM CHAR(10))
	RETURNS float
	AS
	BEGIN
		Declare @SUM float =0 
		SELECT @SUM =sum(PHIEUCHI.TIEN)
			FROM PHIEUCHI
			where MAHDM=@MAHDM
			return ISNULL(@SUM,0)
	END
	GO

	
------------------------------------------------------------------------HDB

 /****** Object:  StoredProcedure FUNCTION F_CREATE_ID_HDM() NGUYEN THI YEN NHI    ******/

CREATE FUNCTION F_CREATE_ID_HDB()
	RETURNS CHAR(10)
	AS
	BEGIN
		DECLARE @ID INT =0;
		if(select COUNT(ID) from HDB) >0
			begin					
			SELECT @ID = MAX(ID) FROM HDB

			end
		else set @ID  =0
		SET @ID =@ID+1;
		IF(@ID <10)
			RETURN CONCAT('B00000000', @ID);
		IF (@ID <100)
			RETURN CONCAT('B0000000', @ID);
		IF (@ID<1000) 
			RETURN CONCAT('B000000', @ID);
		IF (@ID <10000) 
			RETURN CONCAT('B00000',@ID);
		IF (@ID <100000) 
			RETURN CONCAT('B0000',@ID);
		IF (@ID <1000000) 
			RETURN CONCAT('B000',@ID);
		IF (@ID<10000000) 
			RETURN CONCAT('B00',@ID);
		IF (@ID <100000000) 
			RETURN CONCAT('B0',@ID);
			RETURN CONCAT('0000000000',@ID);
	END
	GO
/****** Object:  StoredProcedure [dbo].[sp_HDB_Add]  NGUYỄN THỊ YẾN NHI    ******/
	SET ANSI_NULLS ON
	GO
	SET QUOTED_IDENTIFIER ON
	GO
	CREATE PROCEDURE [dbo].[sp_HDB_Add]
	   @MANV                 char(6),
	   @MAKH                INT,
	   @TONGTIEN             float=0,
	   @TINHTRANGHDB         tinyint=0,
	   @PHIGIAO			FLOAT=0,
	   @MALYDO		INT =1,
	   @MAPTGH INT=1,
	   @SDT VARCHAR(11),
	   @DIACHI NVARCHAR(100),
	   @TENKH NVARCHAR(50),
	   @PHUTHU FLOAT =0,
	   @NO					FLOAT=0,
		@ErrCode int = 0 OUTPUT,
		@ErrMsg NVARCHAR(100) = '' OUTPUT,
		@MAHDNEW CHAR(10) = '0000000000' OUTPUT
	AS
	BEGIN
		
		----CREATE ID AUTO FOR HANG
		DECLARE @MAHDB CHAR(10) ='0000000000'
		SELECT  @MAHDB = DBO.F_CREATE_ID_HDB()
		IF( CONVERT(char,@MAHDB) = '0000000000')
			BEGIN
				SELECT @ErrCode = 1, @ErrMsg = N'Dữ liệu chứa đã đầy'
				return
			END
		INSERT INTO HDB (MAHDB,MALYDO,MAKH,MANV,MAPTGH,TENKHB,DIACHIKHB,SDTKHB,NGAYBAN,TRIGIAHDB,TINHTRANGHDB,PHIGIAOH,THUTHEMHDB,NOHDB)
		VALUES (@MAHDB,@MALYDO,@MAKH,@MANV,@MAPTGH,@TENKH,@DIACHI,@SDT,GETDATE(),@TONGTIEN,@TINHTRANGHDB,@PHIGIAO,@PHUTHU,@NO)
		SELECT @ErrCode = 0, @ErrMsg = N'Cất hóa đơn thành công',@MAHDNEW= @MAHDB
	END
	GO

------[dbo].[sp_HDB_Update] 


/****** Object:  StoredProcedure [dbo].[sp_HDB_Update_HDB] NGUYỄN THỊ YẾN NHI    ******/
	SET ANSI_NULLS ON
	GO
	SET QUOTED_IDENTIFIER ON
	GO
	CREATE PROCEDURE [dbo].[sp_HDB_Update_HDB]
	   @MAHDB                 char(10),
	   @TINHTRANGHDB         tinyint=0,
	    @THANHTOAN         FLOAT=0,
		@ErrCode int = 0 OUTPUT,
		@ErrMsg NVARCHAR(100) = '' OUTPUT
		
	AS
	BEGIN
		UPDATE HDB
		SET TINHTRANGHDB=@TINHTRANGHDB,NOHDB += @THANHTOAN
		WHERE MAHDB=@MAHDB
		SELECT @ErrCode = 0, @ErrMsg = N'Cập nhật trình trạng hóa đơn thành công'
	END
	GO


------[dbo].[sp_HDB_Getlist]
/****** Object:  StoredProcedure [dbo].[[sp_HDB_Getlist]]    ******/
	SET ANSI_NULLS ON
	GO
	SET QUOTED_IDENTIFIER ON
	GO
	alter PROCEDURE [dbo].[sp_HDB_Getlist]
		AS
		BEGIN 
			SELECT ROW_NUMBER() OVER (ORDER BY (SELECT 1)) AS STT,MAHDB AS N'Mã',TENKHB AS N'Nhà cung cấp',
			TENNV AS N'Nhân viên phụ trách',DIACHIKHB AS N'Địa chỉ',SDTKHB AS N'Số điện thoại',TENPTGH AS N'Phương thức giao',PHIGIAOH AS N'Phí giao',THUTHEMHDB AS N'Phụ thu'
			,TRIGIAHDB AS N'Trị giá hóa đơn',NOHDB AS N'Đã thanh toán',CONVERT(date,NGAYBAN) AS N'Ngày tạo',			
			case when TINHTRANGHDB=1 then N'Đã giao' when TINHTRANGHDB=2 then N'Đang xử lý'when TINHTRANGHDB=3 then N'Đang giao'  else N'Đã hủy' end  AS N'Tình trạng hóa đơn bán'
			,TENLYDO AS N'Lý do xuất'
			FROM HDB H,NHANVIEN N ,PTGIAOHANG P,LYDOXUAT L
			WHERE H.MANV=N.MANV AND H.MAPTGH = P.MAPTGH AND H.MALYDO=L.MALYDO
		END	
		GO
		/****** Object:  StoredProcedure [dbo].[[[sp_HDB_GetlistThanhToan]]]    ******/
	SET ANSI_NULLS ON
	GO
	SET QUOTED_IDENTIFIER ON
	GO
	alter PROCEDURE [dbo].[sp_HDB_GetlistThanhToan]
		AS
		BEGIN 
			SELECT ROW_NUMBER() OVER (ORDER BY (SELECT 1)) AS STT,MAHDB AS N'Mã',TENKHB AS N'Tên khách hàng',
			TENNV AS N'Nhân viên phụ trách',DIACHIKHB AS N'Địa chỉ',SDTKHB AS N'Số điện thoại',TENPTGH AS N'Phương thức giao',PHIGIAOH AS N'Phí giao',THUTHEMHDB AS N'Phụ thu'
			,TRIGIAHDB AS N'Trị giá hóa đơn',NOHDB AS N'Đã thanh toán',CONVERT(date,NGAYBAN) AS N'Ngày tạo',			
			case when TINHTRANGHDB=1 then N'Đã giao' when TINHTRANGHDB=2 then N'Đang xử lý'when TINHTRANGHDB=3 then N'Đang giao'  else N'Đã hủy' end  AS N'Tình trạng hóa đơn bán'
			,TENLYDO AS N'Lý do xuất'
			FROM HDB H,NHANVIEN N ,PTGIAOHANG P,LYDOXUAT L
			WHERE H.MANV=N.MANV AND H.MAPTGH = P.MAPTGH AND H.MALYDO=L.MALYDO AND NOHDB = TRIGIAHDB
		END	
		GO
			/****** Object:  StoredProcedure [dbo].[[[[sp_HDB_GetlistNgay]]]]    ******/
	SET ANSI_NULLS ON
	GO
	SET QUOTED_IDENTIFIER ON
	GO
	alter  PROCEDURE [dbo].[sp_HDB_GetlistNgay]
	@FromDate DATE = NULL ,
	@ToDate DATE = NULL
		AS
		BEGIN 
			set dateformat DMY
			SELECT ROW_NUMBER() OVER (ORDER BY (SELECT 1)) AS STT,MAHDB AS N'Mã',TENKHB AS N'Tên khách hàng',
			TENNV AS N'Nhân viên phụ trách',DIACHIKHB AS N'Địa chỉ',SDTKHB AS N'Số điện thoại',TENPTGH AS N'Phương thức giao',PHIGIAOH AS N'Phí giao',THUTHEMHDB AS N'Phụ thu'
			,TRIGIAHDB AS N'Trị giá hóa đơn',NOHDB AS N'Đã thanh toán',CONVERT(date,NGAYBAN )AS N'Ngày tạo',			
			case when TINHTRANGHDB=1 then N'Đã giao' when TINHTRANGHDB=2 then N'Đang xử lý'when TINHTRANGHDB=3 then N'Đang giao'  else N'Đã hủy' end  AS N'Tình trạng hóa đơn bán'
			,TENLYDO AS N'Lý do xuất'
			FROM HDB H,NHANVIEN N ,PTGIAOHANG P,LYDOXUAT L
			WHERE H.MANV=N.MANV AND H.MAPTGH = P.MAPTGH AND H.MALYDO=L.MALYDO AND CAST(h.NGAYBAN AS DATE) BETWEEN @FromDate AND @ToDate
		END	
		GO
		

/****** Object:  StoredProcedure [dbo].[[sp_HDB_GetlistNO]]    ******/
	SET ANSI_NULLS ON
	GO
	SET QUOTED_IDENTIFIER ON
	GO
	alter PROCEDURE [dbo].[sp_HDB_GetlistNO]
		AS
		BEGIN 
			SELECT ROW_NUMBER() OVER (ORDER BY (SELECT 1)) AS STT,MAHDB AS N'Mã',TENKHB AS N'Tên khách hàng',
			TENNV AS N'Nhân viên phụ trách',DIACHIKHB AS N'Địa chỉ',SDTKHB AS N'Số điện thoại',TENPTGH AS N'Phương thức giao',PHIGIAOH AS N'Phí giao',THUTHEMHDB AS N'Phụ thu'
			,TRIGIAHDB AS N'Trị giá hóa đơn',NOHDB AS N'Đã thanh toán',CONVERT(date,NGAYBAN) AS N'Ngày tạo',			
			case when TINHTRANGHDB=1 then N'Đã giao' when TINHTRANGHDB=2 then N'Đang xử lý'when TINHTRANGHDB=3 then N'Đang giao'  else N'Đã hủy' end  AS N'Tình trạng hóa đơn bán'
			,TENLYDO AS N'Lý do xuất'
			FROM HDB H,NHANVIEN N ,PTGIAOHANG P,LYDOXUAT L
			WHERE H.MANV=N.MANV AND H.MAPTGH = P.MAPTGH AND H.MALYDO=L.MALYDO AND NOHDB < TRIGIAHDB
		END	
		GO
------[dbo].[sp_HDB_Getlist_Info]
/****** Object:  StoredProcedure [dbo].[sp_HDB_GetlistInfoByMHDB] nguyen thi yen nhi   ******/
	SET ANSI_NULLS ON
	GO
	SET QUOTED_IDENTIFIER ON
	GO
	alter PROCEDURE [dbo].sp_HDB_GetlistInfoByMHDB
	@MAHDB CHAR(10)
		AS
		BEGIN
			
			SELECT ROW_NUMBER() OVER (ORDER BY (SELECT 1)) AS STT,MAHDB AS N'Mã',h.MAKH AS N'Mã khách hàng',TENKHB AS N'Tên khách hàng',
			TENNV AS N'Nhân viên phụ trách',DIACHIKHB AS N'Địa chỉ',SDTKHB AS N'Số điện thoại',TENPTGH AS N'Phương thức giao',PHIGIAOH AS N'Phí giao',THUTHEMHDB AS N'Phụ thu'
			,TRIGIAHDB AS N'Trị giá hóa đơn',NOHDB AS N'Đã thanh toán',CONVERT(date,NGAYBAN) AS N'Ngày tạo',			
			TINHTRANGHDB AS N'Tình trạng hóa đơn bán'
			,TENLYDO AS N'Lý do xuất'
			FROM HDB H,NHANVIEN N ,PTGIAOHANG P,LYDOXUAT L
			WHERE H.MAHDB = @MAHDB and H.MANV=N.MANV AND H.MAPTGH = P.MAPTGH AND H.MALYDO=L.MALYDO 
		END	
		GO
		
------[dbo].[sp_HDB_Getlist_ByKey]
/****** Object:  StoredProcedure [dbo].[sp_HDB_Getlist_ByKey] nguyen thi yen nhi   ******/
	SET ANSI_NULLS ON
	GO
	SET QUOTED_IDENTIFIER ON
	GO
	alter PROCEDURE [dbo].[sp_HDB_Getlist_ByKey]
	@MAHDB CHAR(10)
		AS
		BEGIN
			
			SELECT ROW_NUMBER() OVER (ORDER BY (SELECT 1)) AS STT,H.MAHDB,h.MAKH,TENKHB,
			TENNV,DIACHIKHB,SDTKHB,PHIGIAOH,THUTHEMHDB
			,TRIGIAHDB,NOHDB,NGAYBAN,c.MAH,c.SOLUONGB,c.DONGIAHDB,c.KHUYENMAI,c.TGBHBAN,c.THANHTIEN		
			FROM HDB H,NHANVIEN N ,CTHDB c, HANG hh
			WHERE H.MAHDB = @MAHDB and H.MANV=N.MANV and H.MAHDB=c.MAHDB and c.MAH=hh.MAH
		END	
		GO
		
------[dbo].[sp_HDB_Getlist_ChartYear]
/****** Object:  StoredProcedure [dbo].[sp_HDB_Getlist_ChartYear] nguyen thi yen nhi   ******/
	SET ANSI_NULLS ON
	GO
	SET QUOTED_IDENTIFIER ON
	GO
	CREATE PROCEDURE [dbo].sp_HDB_Getlist_ChartYear
	@YEAR INT ='2017'
		AS
		BEGIN
			select MONTH(NGAYBAN) as N'THANG',SUM(TRIGIAHDB) AS 'TONG' from HDB
			where YEAR(NGAYBAN)=@YEAR AND TINHTRANGHDB != 0
			GROUP BY MONTH(NGAYBAN)
			ORDER BY MONTH(NGAYBAN)
		END	

------[dbo].[sp_HDB_Update_TinhTrangHDB]


------[dbo].[sp_CTHDB_Add]
/****** Object:  StoredProcedure [dbo].[sp_[[sp_CTHDB_Add]]  NGUYỄN THỊ YẾN NHI    ******/
	SET ANSI_NULLS ON
	GO
	SET QUOTED_IDENTIFIER ON
	GO
	CREATE PROCEDURE [dbo].[sp_CTHDB_Add]
		@MAH                  char(10),
		@MAHDB                char(10),
		@DONGIAHB             float=0 ,
		@TGBAOHANH            int=0 ,
		@SLUONGHM				INT=1,
		@KHUYENMAI				FLOAT=0,
		@THANHTIEN			FLOAT=0,
		@TRANGTHAI tinyint=2
	AS
	BEGIN
		INSERT INTO CTHDB VALUES(@MAHDB,@MAH,@SLUONGHM,@DONGIAHB,@KHUYENMAI,@TGBAOHANH,@THANHTIEN)
		---1: da giao, 3-dang giao,0 huy,2 dang xu ly
		IF(@TRANGTHAI !=1 AND @TRANGTHAI !=3) RETURN
		UPDATE HANG SET SLH=SLH-@SLUONGHM where MAH=@MAH
		UPDATE HANG SET TINHTRANGH=(CASE WHEN SLH=0 THEN 0 ELSE 1 END) 
		WHERE MAH=@MAH
	END
	GO

------[dbo].[sp_CTHDB_Update] 
------[dbo].[sp_CTHDB_Getlist]
	SET ANSI_NULLS ON
	GO
	SET QUOTED_IDENTIFIER ON
	GO
	create PROCEDURE [dbo].[sp_CTHDB_Getlist]
		AS
		BEGIN 
		
		SELECT ROW_NUMBER() OVER (ORDER BY (SELECT 1)) AS STT,c.MAH AS N'Mã hàng',TENH AS N'Tên hàng',SOLUONGB AS N'Số lượng',DONGIAHDB AS N'Đơn giá',
		KHUYENMAI AS N'Khuyến mãi',TGBHBAN AS N'Thời gian bảo hành',THANHTIEN AS N'Thành tiền'
		FROM CTHDB c,HANG h
		where c.MAH=h.MAH
		END
GO
------[dbo].[sp_CTHDB_Getlist_Info]
------[dbo].[[sp_CTHDB_Getlist_ByKeyHDB]]
SET ANSI_NULLS ON
	GO
	SET QUOTED_IDENTIFIER ON
	GO
	create PROCEDURE [dbo].[sp_CTHDB_Getlist_ByKeyHDB]
	@MAHDB CHAR(10)
		AS
		BEGIN 
		SELECT ROW_NUMBER() OVER (ORDER BY (SELECT 1)) AS STT,c.MAH AS N'Mã hàng',TENH AS N'Tên hàng',SOLUONGB AS N'Số lượng',DONGIAHDB AS N'Đơn giá',
		KHUYENMAI AS N'Khuyến mãi',TGBHBAN AS N'Thời gian bảo hành',THANHTIEN AS N'Thành tiền'
		FROM CTHDB c,HANG h
		where c.MAH=h.MAH AND MAHDB = @MAHDB
		END
		GO
------[dbo].[sp_CTHDB_Getlist_TTThanhToan]

---------------------------------------------------------------------------------
------[dbo].[sp_PTGH_Add]
------[dbo].[sp_PTGH_Update] 
/****** Object:  StoredProcedure [dbo].[sp_PTGH_Getlist_Info] NGUYEN THI YEN NHI   ******/
	SET ANSI_NULLS ON
	GO
	SET QUOTED_IDENTIFIER ON
	GO
	create PROCEDURE [dbo].[sp_PTGH_Getlist_Info]
		AS
		BEGIN
			SELECT MAPTGH ,CONCAT(MAPTGH,'---',TENPTGH,'--',PHIPTGH) AS TEN
			FROM PTGIAOHANG
			ORDER BY TEN ASC
			
		END	
	go
