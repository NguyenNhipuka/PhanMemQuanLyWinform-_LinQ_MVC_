 ----[dbo].[sp_Add_DVT]
 ---[dbo].[sp_Update_DVT] 
 ---[dbo].[sp_DVT_Getlist]
 ---[dbo].[sp_DVT_Getlist_ByKey]
 ---[dbo].[sp_Delete_DVT]
   
 ----[dbo].[sp_Add_LOAI]
 ---[dbo].[sp_Update_LOAI] 
 ---[dbo].[sp_LOAI_Getlist]
 ---[dbo].[sp_LOAI_Getlist_ByKey]
 ---[dbo].[sp_Delete_LOAI] 

  ----[dbo].[sp_CTTB_Add]
 ----[dbo].[sp_TB_Add]
 ---[dbo].[sp_TB_Update] 
 ---[dbo].[sp_TB_Getlist]
 ---[dbo].[sp_TB_Getlist_ByKey]
 ---[dbo].[sp_TB_Delete]  

 -----[dbo].[[[sp_TB_Getlist_AllSoLuongBanNgay]]]
----- [dbo].[[[sp_TB_Getlist_SoLuongBanNgay]]]
-------- [dbo].[[[sp_TB_Getlist_SoLuongMuaNgay]]]
------- [dbo].[[[sp_TB_Getlist_AllSoLuongMuaNgay]]]
--------[dbo].[[sp_TB_Getlist_SapHet]
	CREATE view THONGTINTB as
		SELECT ROW_NUMBER() OVER (ORDER BY (SELECT 1)) AS STT,H.MAH as 'MAH',
			TENH ,TENLOAI ,TENDVT,SLH ,DONGIAM,
			DONGIAB ,KHUYENMAI_H ,NSX ,TGBH ,TINHTRANGH,
			MOTAH,TENNCC,TON_MAX ,TON_MIN 
			
			FROM HANG H,LOAIHANG L,DVT D,CT_HANG C,NCC N
			where H.MALOAI=L.MALOAI AND D.MADVT=H.MADVT AND H.MAH=C.MAH AND H.MANCC=N.MANCC ;
go
	CREATE VIEW DSHOADONDABAN AS
	SELECT C.MAHDB,C.MAH,C.SOLUONGB FROM HDB B,CTHDB C WHERE C.MAHDB = B.MAHDB AND TINHTRANGHDB =1 OR TINHTRANGHDB =3;

 /****** Object:  StoredProcedure FUNCTION F_CREATE_ID_HANG() NGUYEN THI YEN NHI    ******/
CREATE FUNCTION F_CREATE_ID_HANG()
	RETURNS CHAR(10)
	AS
	BEGIN
		DECLARE @ID INT =0;
		if(select COUNT(ID) from HANG) >0
			begin
				SELECT @ID = MAX(ID) FROM HANG
			end
		SET @ID =@ID+1;
		IF(@ID <10)
			RETURN CONCAT('SP0000000', @ID);
		IF (@ID <100)
			RETURN CONCAT('SP000000', @ID);
		IF (@ID<1000) 
			RETURN CONCAT('SP00000', @ID);
		IF (@ID <10000) 
			RETURN CONCAT('SP0000',@ID);
		IF (@ID <100000) 
			RETURN CONCAT('SP000',@ID);
		IF (@ID <1000000) 
			RETURN CONCAT('SP00',@ID);
		IF (@ID<10000000) 
			RETURN CONCAT('SP0',@ID);
		IF (@ID <100000000) 
			RETURN CONCAT('SP',@ID);
			RETURN CONCAT('0000000000',@ID);
	END
	GO
 /****** Object:  StoredProcedure [dbo].[sp_Add_DVT]     ******/
	SET ANSI_NULLS ON
	GO
	SET QUOTED_IDENTIFIER ON
	GO
	CREATE PROCEDURE [dbo].[sp_Add_DVT] 
	@TENDVT NVARCHAR(20),
	@ErrCode int = 0 OUTPUT,
	@ErrMsg NVARCHAR(100) = '' OUTPUT
	AS
	BEGIN
		IF EXISTS (SELECT 1 FROM DVT WHERE UPPER(TENDVT) = UPPER(@TENDVT))
		BEGIN
			SELECT @ErrCode = 1, @ErrMsg = N'Đã tồn tại tên đơn vị tính: '+@TENDVT + N' này'
			RETURN
		END
		INSERT INTO DVT VALUES(@TENDVT)
		SELECT @ErrCode = 0, @ErrMsg = N'Thêm thành công đơn vị tính: '+@TENDVT
	END
	GO
	------------------------------------------------------------------------------------------
	/****** Object:  StoredProcedure [dbo].[sp_Update_DVT]     ******/
	SET ANSI_NULLS ON
	GO
	SET QUOTED_IDENTIFIER ON
	GO
	CREATE PROCEDURE [dbo].[sp_Update_DVT]
	@MADVT INT,
	@TENDVT NVARCHAR(20),
	@ErrCode int = 0 OUTPUT,
	@ErrMsg NVARCHAR(100) = '' OUTPUT
	AS
	BEGIN
		IF NOT EXISTS (SELECT 1 FROM DVT WHERE UPPER(TENDVT) = UPPER(@TENDVT) and MADVT=@MADVT)
		BEGIN
		Declare @TENDVT_CU nvarchar(30)
		SELECT @TENDVT_CU = TENDVT FROM DVT WHERE MADVT = @MADVT
		UPDATE DVT
		SET TENDVT= @TENDVT
		where MADVT=@MADVT
		SELECT  @ErrCode = 0,@ErrMsg = N'Cập nhật thành công đơn vị tính: '+@TENDVT_CU + N' thành '+@TENDVT
			RETURN
		END
		SELECT @ErrCode = 1, @ErrMsg = N'Tên đơn vị tính đã tồn tại!: '

	END
	GO		
	---------------------------------------------------------------------------------------------------------------
	/****** Object:  StoredProcedure [dbo].[sp_DVT_Getlist]    ******/
	SET ANSI_NULLS ON
	GO
	SET QUOTED_IDENTIFIER ON
	GO
	CREATE PROCEDURE [dbo].[sp_DVT_Getlist]
		AS
		BEGIN
			SELECT ROW_NUMBER() OVER (ORDER BY (SELECT 1)) AS STT,MADVT AS N'Mã đơn vị tính',TENDVT AS N'Tên đơn vị tính'
			FROM DVT	
		END	
	/****** Object:  StoredProcedure [dbo].[sp_DVT_Getlist_ByKey]   ******/
	SET ANSI_NULLS ON
	GO
	SET QUOTED_IDENTIFIER ON
	GO
	CREATE PROCEDURE [dbo].[sp_DVT_Getlist_ByKey]
			@TENDVT NVARCHAR(20) = '',
			@MADVT INT
		AS
		BEGIN
			IF(@TENDVT !='' and @MADVT ='')
			BEGIN
				SELECT *
				FROM DVT
				WHERE @TENDVT LIKE '%' + @TENDVT + '%'	
				RETURN
			END
				SELECT MADVT AS N'Mã đơn vị tính',TENDVT AS N'Tên đơn vị tính'
				FROM DVT
				WHERE  @MADVT=MADVT		
		END
	--------------------------------------------------------------------------------------------------------
	/****** Object:  StoredProcedure [dbo].[sp_Delete_DVT]     ******/
	SET ANSI_NULLS ON
	GO
	SET QUOTED_IDENTIFIER ON
	GO
	CREATE PROCEDURE [dbo].[sp_Delete_DVT]
	@MADVT INT,
	@ErrCode int = 0 OUTPUT,
	@ErrMsg NVARCHAR(200) = '' OUTPUT
	AS 
	BEGIN
		SET NOCOUNT ON
		IF EXISTS (SELECT 1 FROM HANG WHERE MADVT = @MADVT)
		BEGIN
			SELECT @ErrCode = 1, @ErrMsg = N'Đơn vị tính này đang được sử dụng: '
			RETURN 
		END
		DELETE FROM DVT
		WHERE MADVT=@MADVT
		SELECT @ErrCode = 0,@ErrMsg =N'Xóa thành công đơn vị tính có mã: '+@MADVT	
		
	END
	GO


	/****** Object:  StoredProcedure [dbo].[sp_Add_LOAI]   NGUYEN THI YEN NHI  ******/
	SET ANSI_NULLS ON
	GO
	SET QUOTED_IDENTIFIER ON
	GO
	CREATE PROCEDURE [dbo].[sp_Add_LOAI] 
	@TENLOAI NVARCHAR(50),
	@ErrCode int = 0 OUTPUT,
	@ErrMsg NVARCHAR(100) = '' OUTPUT
	AS
	BEGIN
		SET NOCOUNT ON
		IF EXISTS (SELECT 1 FROM LOAIHANG WHERE UPPER(TENLOAI) = UPPER(@TENLOAI))
		BEGIN
			SELECT @ErrCode = 1, @ErrMsg = N'Đã tồn tại tên loại: '+@TENLOAI + N' này'
			RETURN
		END
		INSERT INTO LOAIHANG VALUES(@TENLOAI)
		SELECT  @ErrMsg = N'Thêm thành công loại: '+@TENLOAI
	END
	GO
	------------------------------------------------------------------------------------------
	/****** Object:  StoredProcedure [dbo].[sp_Update_LOAI] NGUYEN THI YEN NHI    ******/
	SET ANSI_NULLS ON
	GO
	SET QUOTED_IDENTIFIER ON
	GO
	CREATE PROCEDURE [dbo].[sp_Update_LOAI]
	@MALOAI INT,
	@TENLOAI NVARCHAR(50),
	@ErrCode int = 0 OUTPUT,
	@ErrMsg NVARCHAR(100) = '' OUTPUT
	AS
	IF 1=0 BEGIN
			SET FMTONLY OFF
			END
	BEGIN
		IF NOT EXISTS (SELECT 1 FROM LOAIHANG WHERE UPPER(TENLOAI) = UPPER(@TENLOAI) AND MALOAI !=@MALOAI)
		BEGIN
		Declare @TEN_CU nvarchar(30)
		SELECT @TEN_CU = TENLOAI FROM LOAIHANG WHERE MALOAI=@MALOAI
		UPDATE LOAIHANG
		SET TENLOAI = @TENLOAI
		Where MALOAI=@MALOAI
		SELECT  @ErrMsg = N'Cập nhật thành công	loại '+@TEN_CU +N' thành '+@TENLOAI
			RETURN
		END
		SELECT @ErrCode = 1, @ErrMsg = N'Tên đơn vị tính đã tồn tại!: '

	END
	GO		
	---------------------------------------------------------------------------------------------------------------
	/****** Object:  StoredProcedure [dbo].[sp_LOAI_Getlist] NGUYEN THI YEN NHI   ******/
	SET ANSI_NULLS ON
	GO
	SET QUOTED_IDENTIFIER ON
	GO
	CREATE PROCEDURE [dbo].[sp_LOAI_Getlist]
		AS
		BEGIN
			SELECT ROW_NUMBER() OVER (ORDER BY (SELECT 1)) AS STT,MALOAI AS N'Mã loại',TENLOAI AS N'Tên loại'
			FROM LOAIHANG	
		END	
	/****** Object:  StoredProcedure [dbo].[sp_LOAI_Getlist_ByKey] NGUYEN THI YEN NHI  ******/
	SET ANSI_NULLS ON
	GO
	SET QUOTED_IDENTIFIER ON
	GO
	CREATE PROCEDURE [dbo].[sp_LOAI_Getlist_ByKey]
			@TENLOAI NVARCHAR(50) = '',
			@MALOAI INT
		AS
		BEGIN
			IF(@TENLOAI !='' and @MALOAI ='')
			BEGIN
				SELECT *
				FROM LOAIHANG
				WHERE @TENLOAI LIKE '%' + @TENLOAI + '%'	
				RETURN
			END
				SELECT MALOAI AS N'Mã loại',TENLOAI AS N'Tên loại'
				FROM LOAIHANG
				WHERE  @MALOAI=MALOAI	
		END
	--------------------------------------------------------------------------------------------------------
	/****** Object:  StoredProcedure [dbo].[sp_Delete_LOAI]    NGUYEN THI YEN NHI ******/
	SET ANSI_NULLS ON
	GO
	SET QUOTED_IDENTIFIER ON
	GO
	CREATE PROCEDURE [dbo].[sp_Delete_LOAI]
	@MALOAI INT,
	@ErrCode int = 0 OUTPUT,
	@ErrMsg NVARCHAR(200) = '' OUTPUT
	AS
	BEGIN
	SET NOCOUNT ON
		IF EXISTS (SELECT 1 FROM HANG WHERE MALOAI=@MALOAI)
		BEGIN
			SELECT @ErrCode = 1, @ErrMsg = N'Đơn vị tính này đang được sử dụng: '
			RETURN
		END
		DELETE LOAIHANG
		WHERE MALOAI=@MALOAI
		
		SELECT @ErrCode = 0, @ErrMsg = CONCAT(N'Xóa thành công loại hàng có mã: ',@MALOAI);
	END
	GO
----select * from HANG
/****** Object:  StoredProcedure [dbo].[sp_TB_Add]  NGUYỄN THỊ YẾN NHI    ******/
	SET ANSI_NULLS ON
	GO
	SET QUOTED_IDENTIFIER ON
	GO
	create PROCEDURE [dbo].[sp_TB_Add] 
	   @MALOAI INT,
	   @MADVT int,
	   @MANCC int,
	   @TENH  nvarchar(100),
	   @SLH   INT	,
	   @DONGIAM  float,
	   @DONGIAB  float,
	   @TGBH    int,
	   @NSX   nvarchar(50),
	   @TINHTRANGH  tinyint ,
	   @MOTAH	NVARCHAR(200),
	   @MAX INT=0,
		 @MIN INT=0,
		 @KHUYENMAI float=0,
		@ErrCode int = 0 OUTPUT,
		@ErrMsg NVARCHAR(100) = '' OUTPUT
	AS
	BEGIN
		IF EXISTS (SELECT 1 FROM HANG WHERE UPPER(TENH) = UPPER(@TENH) AND UPPER(MALOAI) = UPPER(@MALOAI))
		BEGIN
			SELECT @ErrCode = 1, @ErrMsg = N'Đã tồn tại tên hàng này: '+@TENH + N' này'
			RETURN
		END
		----CREATE ID AUTO FOR HANG
		DECLARE @MAH VARCHAR(10) ='0000000000'
		SELECT  @MAH = DBO.F_CREATE_ID_HANG()
		IF( CONVERT(char,@MAH) = '0000000000')
			BEGIN
				SELECT @ErrCode = 1, @ErrMsg = N'Dữ liệu chứa đã đầy'
				return
			END
		INSERT INTO HANG(MAH,MALOAI,MADVT,MANCC,TENH,SLH,DONGIAM,DONGIAB,TGBH,NSX,TINHTRANGH,MOTAH)
		VALUES(@MAH,@MALOAI,@MADVT,@MANCC,@TENH,@SLH,@DONGIAM,@DONGIAB,@TGBH,@NSX,@TINHTRANGH,@MOTAH)
		INSERT CT_HANG VALUES(@MAH,@MAX,@MIN,@KHUYENMAI)
		SELECT @ErrCode = 0, @ErrMsg = N'Thêm thành công mặt hàng mới'
		return
	END
	GO
 ---[dbo].[sp_TB_Update] 
 /****** Object:  StoredProcedure [dbo].[sp_TB_Update]   NGUYỄN THỊ YẾN NHI    ******/

 SET ANSI_NULLS ON
	GO
	SET QUOTED_IDENTIFIER ON
	GO
	CREATE PROCEDURE [dbo].[sp_TB_Update] 
		@MAH CHAR(10),
	   @MALOAI INT,
	   @MADVT int,
	   @MANCC int,
	   @TENH  nvarchar(100),
	   @SLH   INT	,
	   @DONGIAM  float,
	   @DONGIAB  float,
	   @TGBH    int,
	   @NSX   nvarchar(50),
	   @TINHTRANGH  tinyint ,
	   @MOTAH	NVARCHAR(200),
	    @MAX INT=0,
		 @MIN INT=0,
		 @KHUYENMAI float=0,
		@ErrCode int = 0 OUTPUT,
		@ErrMsg NVARCHAR(100) = '' OUTPUT
	AS
	BEGIN
		IF EXISTS (SELECT 1 FROM HANG WHERE UPPER(TENH) = UPPER(@TENH) AND UPPER(MALOAI) = UPPER(@MALOAI) AND MAH !=@MAH)
		BEGIN
			SELECT @ErrCode = 1, @ErrMsg = N'Đã tồn tại tên hàng này: '+@TENH + N' này'
			RETURN
		END
		UPDATE HANG
		SET MALOAI=@MALOAI ,MADVT=@MADVT,MANCC=@MANCC
		,TENH=@TENH ,SLH=@SLH,DONGIAM=@DONGIAM,
		DONGIAB=@DONGIAB,
		TGBH=@TGBH,NSX=@NSX,TINHTRANGH=@TINHTRANGH,MOTAH=@MOTAH
		WHERE MAH=@MAH
		
		UPDATE CT_HANG
		SET TON_MAX=@MAX,TON_MIN=@MIN,KHUYENMAI_H=@KHUYENMAI
		WHERE MAH=@MAH
		SELECT @ErrCode = 0, @ErrMsg = N'Cập nhật thành công mặt hàng: '+@TENH +'('+@MAH+')'
	END
	GO
 ---[dbo].[sp_TB_Getlist]
 /****** Object:  StoredProcedure [dbo].[sp_TB_Getlist]   ******/
	SET ANSI_NULLS ON
	GO
	SET QUOTED_IDENTIFIER ON
	GO
	CREATE PROCEDURE [dbo].[sp_TB_Getlist]
		AS
		BEGIN
			SELECT ROW_NUMBER() OVER (ORDER BY (SELECT 1)) AS STT,H.MAH AS N'Mã',
			TENH AS N'Tên',TENLOAI AS N'Loại',TENDVT AS N'Đơn vị tính',SLH AS N'Số lượng',DONGIAM AS N'Giá nhập',
			DONGIAB AS N'Giá bán',KHUYENMAI_H AS N'Khuyến mãi',NSX AS N'Nhà sản xuất',TGBH AS N'Bảo hành (tháng)',TINHTRANGH AS N'Trình trạng',
			MOTAH AS N'Mô tả',TENNCC AS N'Tên nhà cung cấp',TON_MAX AS N'Tồn max',TON_MIN AS N'Tồn min'
			FROM HANG H,LOAIHANG L,DVT D,CT_HANG C,NCC N
			where H.MALOAI=L.MALOAI AND D.MADVT=H.MADVT AND H.MAH=C.MAH AND H.MANCC=N.MANCC
		END	
 ---[dbo].[sp_TB_Getlist_ByKey]
  ---[dbo].[sp_TB_Getlist_ByKey]
 /****** Object:  StoredProcedure [dbo].[sp_TB_Getlist_ByKey]   ******/
	SET ANSI_NULLS ON
	GO
	SET QUOTED_IDENTIFIER ON
	GO
	CREATE PROCEDURE [dbo].sp_TB_Getlist_ByKey
	@MAH CHAR(10)
		AS
		BEGIN
			SELECT ROW_NUMBER() OVER (ORDER BY (SELECT 1)) AS STT,H.MAH,
			TENH,MALOAI ,MADVT ,SLH ,DONGIAM ,
			DONGIAB ,KHUYENMAI_H,NSX ,TGBH ,TINHTRANGH,
			MOTAH ,MANCC ,TON_MAX,TON_MIN
			FROM HANG H,CT_HANG c
			where @MAH=h.MAH and c.MAH=H.MAH
		END	

		
 /****** Object:  StoredProcedure [dbo].[sp_TB_Getlist_ByKeyHDM]   ******/
	SET ANSI_NULLS ON
	GO
	SET QUOTED_IDENTIFIER ON
	GO
	CREATE PROCEDURE [dbo].sp_TB_Getlist_ByKeyHDM
	@MAH CHAR(10)
		AS
		BEGIN
			SELECT MAH,TENH,TENLOAI,TENDVT,DONGIAM,TGBH FROM HANG H, LOAIHANG L,DVT D
			WHERE H.MAH=@MAH AND H.MADVT=D.MADVT AND L.MALOAI=H.MALOAI
		END	

 --------------------------------------------------------------------------------------------------------
	/****** Object:  StoredProcedure [dbo].[sp_TB_Delete]    NGUYEN THI YEN NHI ******/
	SET ANSI_NULLS ON
	GO
	SET QUOTED_IDENTIFIER ON
	GO
	CREATE PROCEDURE [dbo].[sp_TB_Delete]
	@MATB CHAR(10),
	@ErrCode int = 0 OUTPUT,
	@ErrMsg NVARCHAR(200) = '' OUTPUT
	AS
	BEGIN
		IF EXISTS (SELECT * FROM CTHDB WHERE  MAH=@MATB)
		BEGIN
			SELECT @ErrCode = 1, @ErrMsg = N'Mặt hàng này đang được sử dụng'
			RETURN
		END
		IF EXISTS (SELECT * FROM CTHDM WHERE  MAH=@MATB)
		BEGIN
			SELECT @ErrCode = 1, @ErrMsg = N'Mặt hàng này đang được sử dụng '
			RETURN
		END
		IF EXISTS (SELECT * FROM PHIEUBAOHANH WHERE  MAH=@MATB)
		BEGIN
			SELECT @ErrCode = 1, @ErrMsg = N'Mặt hàng này đang được sử dụng '
			RETURN
		END
		IF EXISTS (SELECT * FROM CTHDSC WHERE MAH=@MATB)
		BEGIN
			SELECT @ErrCode = 1, @ErrMsg = N'Mặt hàng này đang được sử dụng '
			RETURN
		END
		IF EXISTS (SELECT 1 FROM CTDOIHANG WHERE MAH=@MATB)
		BEGIN
			SELECT @ErrCode = 1, @ErrMsg = N'Mặt hàng này đang được sử dụng '
			RETURN
		END
		DELETE CT_HANG WHERE MAH=@MATB
		DELETE HANG WHERE MAH=@MATB
		SELECT @ErrCode = 0, @ErrMsg =N'Xóa thành công hàng có mã: '+@MATB	
	END
	GO
	
/****** Object:  StoredProcedure [dbo].[sp_TB_GetlistMAH_TENH]   ******/
	SET ANSI_NULLS ON
	GO
	SET QUOTED_IDENTIFIER ON
	GO
	CREATE PROCEDURE [dbo].sp_TB_GetlistMAH_TENH
		@MANCC INT 
		AS
		BEGIN
			SELECT  MAH ,CONCAT(MAH,'  ',TENH,'  ',l.TENLOAI) AS TEN
			FROM HANG h,LOAIHANG l
			WHERE H.MANCC=@MANCC and h.MALOAI=l.MALOAI and TINHTRANGH =1
			ORDER BY MAH ASC
			
		END	
	go

	/****** Object:  StoredProcedure [dbo].[sp_TB_GetlistMAH_TENH]   ******/
	SET ANSI_NULLS ON
	GO
	SET QUOTED_IDENTIFIER ON
	GO
	create PROCEDURE [dbo].sp_TB_GetlistMAH_TENH_HDB
		AS
		BEGIN
			SELECT  MAH ,CONCAT(MAH,'  ',TENH,'  ',l.TENLOAI) AS TEN
			FROM HANG h,LOAIHANG l
			WHERE h.MALOAI=l.MALOAI and TINHTRANGH =1
			ORDER BY MAH ASC
			
		END	
	go


	/****** Object:  StoredProcedure [dbo].[sp_KH_GetlistMAKH_TENKH] NGUYEN THI YEN NHI   ******/
	SET ANSI_NULLS ON
	GO
	SET QUOTED_IDENTIFIER ON
	GO
	create PROCEDURE [dbo].sp_KH_GetlistMAKH_TENKH_HDB
		AS
		BEGIN
			SELECT MAKH ,CONCAT(MAKH,'---',TENKH,'--',SDTKH) AS TEN
			FROM KHACHHANG
			WHERE TINHTRANGKH =1
			ORDER BY TEN ASC
			
		END	
	go


	 /****** Object:  StoredProcedure [dbo].[[sp_TB_Getlist_SapHet]]   ******/
	SET ANSI_NULLS ON
	GO
	SET QUOTED_IDENTIFIER ON
	GO
	CREATE PROCEDURE [dbo].[sp_TB_Getlist_SapHet]
		AS
		BEGIN
			SELECT ROW_NUMBER() OVER (ORDER BY (SELECT 1)) AS STT,H.MAH AS N'Mã',
			TENH AS N'Tên',TENLOAI AS N'Loại',TENDVT AS N'Đơn vị tính',SLH AS N'Số lượng',DONGIAM AS N'Giá nhập',
			DONGIAB AS N'Giá bán',KHUYENMAI_H AS N'Khuyến mãi',NSX AS N'Nhà sản xuất',TGBH AS N'Bảo hành (tháng)',TINHTRANGH AS N'Trình trạng',
			MOTAH AS N'Mô tả',TENNCC AS N'Tên nhà cung cấp',TON_MAX AS N'Tồn max',TON_MIN AS N'Tồn min'
			FROM HANG H,LOAIHANG L,DVT D,CT_HANG C,NCC N
			where H.MALOAI=L.MALOAI AND D.MADVT=H.MADVT AND H.MAH=C.MAH AND H.MANCC=N.MANCC and H.SLH <= TON_MIN
		END	

	 /****** Object:  StoredProcedure [dbo].[[[sp_TB_Getlist_SoLuongBanNgay]]]   ******/
	SET ANSI_NULLS ON
	GO
	SET QUOTED_IDENTIFIER ON
	GO
	CREATE PROCEDURE [dbo].[sp_TB_Getlist_SoLuongBanNgay]
	@FromDate DATE = NULL ,
	@ToDate DATE = NULL,
	@TINHTRANG TINYINT =4
		AS
		BEGIN
			SET DATEFORMAT DMY
			IF(@TINHTRANG=4)
				BEGIN
						SELECT  ROW_NUMBER() OVER (ORDER BY (SELECT 1)) AS STT, H.MAH,TENH, ISNULL(SUM(C.SOLUONGB),0) AS SL 
					FROM HANG H LEFT JOIN (
					SELECT HD.MAHDB,CT.MAH,CT.SOLUONGB FROM HDB HD,CTHDB CT 
					WHERE CT.MAHDB = HD.MAHDB
					AND CAST(HD.NGAYBAN AS DATE) BETWEEN @FromDate  AND @ToDate) AS C
					ON H.MAH = C.MAH GROUP BY H.MAH,TENH
					RETURN 
				END
				SELECT  ROW_NUMBER() OVER (ORDER BY (SELECT 1)) AS STT, H.MAH,TENH, ISNULL(SUM(C.SOLUONGB),0) AS SL 
			FROM HANG H LEFT JOIN (
			SELECT HD.MAHDB,CT.MAH,CT.SOLUONGB FROM HDB HD,CTHDB CT 
			WHERE CT.MAHDB = HD.MAHDB and hd.TINHTRANGHDB =@TINHTRANG
			AND CAST(HD.NGAYBAN AS DATE) BETWEEN @FromDate  AND @ToDate) AS C
			ON H.MAH = C.MAH GROUP BY H.MAH,TENH
		END	
		 /****** Object:  StoredProcedure [dbo].[[[sp_TB_Getlist_AllSoLuongBanNgay]]]   ******/
	SET ANSI_NULLS ON
	GO
	SET QUOTED_IDENTIFIER ON
	GO
	create PROCEDURE [dbo].sp_TB_Getlist_AllSoLuongBanNgay
		AS
		BEGIN
	SELECT  ROW_NUMBER() OVER (ORDER BY (SELECT 1)) AS STT, H.MAH,TENH, ISNULL(SUM(C.SOLUONGB),0) AS SL 
			FROM HANG H LEFT JOIN (
			SELECT HD.MAHDB,CT.MAH,CT.SOLUONGB FROM HDB HD,CTHDB CT 
			WHERE CT.MAHDB = HD.MAHDB) AS C
			ON H.MAH = C.MAH GROUP BY H.MAH,TENH
		END	
		 /****** Object:  StoredProcedure [dbo].[[[sp_TB_Getlist_SoLuongMuaNgay]]]   ******/
	SET ANSI_NULLS ON
	GO
	SET QUOTED_IDENTIFIER ON
	GO
	CREATE PROCEDURE [dbo].[sp_TB_Getlist_SoLuongMuaNgay]
	@FromDate DATE = NULL ,
	@ToDate DATE = NULL,
	@TINHTRANG TINYINT =3
		AS
		BEGIN
			SET DATEFORMAT DMY
			if(@TINHTRANG =3)
				begin
					SELECT  ROW_NUMBER() OVER (ORDER BY (SELECT 1)) AS STT, H.MAH,TENH, ISNULL(SUM(C.SLUONGHM),0) AS SL 
					FROM HANG H LEFT JOIN (
					SELECT HD.MAHDM,CT.MAH,CT.SLUONGHM FROM HDM HD,CTHDM CT 
					WHERE CT.MAHDM = HD.MAHDM
					AND CAST(HD.NGAYTAOHDM AS DATE) BETWEEN @FromDate  AND @ToDate) AS C
					ON H.MAH = C.MAH GROUP BY H.MAH,TENH		
					RETURN
				END
			SELECT  ROW_NUMBER() OVER (ORDER BY (SELECT 1)) AS STT, H.MAH,TENH, ISNULL(SUM(C.SLUONGHM),0) AS SL 
			FROM HANG H LEFT JOIN (
			SELECT HD.MAHDM,CT.MAH,CT.SLUONGHM FROM HDM HD,CTHDM CT 
			WHERE CT.MAHDM = HD.MAHDM and hd.TINHTRANGHDM = @TINHTRANG
			AND CAST(HD.NGAYTAOHDM AS DATE) BETWEEN @FromDate  AND @ToDate) AS C
			ON H.MAH = C.MAH GROUP BY H.MAH,TENH		

		END	
 /****** Object:  StoredProcedure [dbo].[[[sp_TB_Getlist_AllSoLuongMuaNgay]]]   ******/
	SET ANSI_NULLS ON
	GO
	SET QUOTED_IDENTIFIER ON
	GO
	create PROCEDURE [dbo].sp_TB_Getlist_AllSoLuongMuaNgay
		AS
		BEGIN			
			SELECT  ROW_NUMBER() OVER (ORDER BY (SELECT 1)) AS STT, H.MAH,TENH, ISNULL(SUM(C.SLUONGHM),0) AS SL 
			FROM HANG H LEFT JOIN (
			SELECT HD.MAHDM,CT.MAH,CT.SLUONGHM FROM HDM HD,CTHDM CT 
			WHERE CT.MAHDM = HD.MAHDM ) AS C
			ON H.MAH = C.MAH GROUP BY H.MAH,TENH		

		END	

		