--[dbo].[sp_PHIEUBAOHANH_Add]
--[dbo].[sp_PHIEUBAOHANH_Update]
--[dbo].[sp_PHIEUBAOHANH_Getlist]
--[dbo].[sp_PHIEUBAOHANH_Delete]
---[dbo].[sp_TB_HDB_ChuaPBH_Getlist]
---[dbo].[sp_TB_HDB_DaPBH_Getlist]
--[dbo].[sp_BAOHANH_Add]
--[dbo].[sp_BAOHANH_Update]
--[dbo].[sp_BAOHANH_Delete]
--[dbo].[sp_BAOHANH_Getlist]
--[dbo].[sp_SUACHUA_Update]
--[dbo].[sp_SUACHUA_Add] 
--[dbo].[sp_SUACHUA_Delete]
--[dbo].[sp_SUACHUA_Getlist]
--[dbo].[sp_CTHDSC_Add]
--[dbo].[sp_CTHDSC_Update]
--[dbo].[sp_CTHDSC_Getlist]
/****** Object:  StoredProcedure [dbo].[sp_PHIEUBAOHANH_Add]     ******/
	SET ANSI_NULLS ON
	GO
	SET QUOTED_IDENTIFIER ON
	GO
	CREATE PROCEDURE [dbo].[sp_PHIEUBAOHANH_Add] 
			@MAHDB CHAR(10) ='',
			@MAH CHAR(10)='',
			@MANV CHAR(6)='',
			@SERIALBH VARCHAR(30)='',
			@ErrCode int = 0 OUTPUT,
			@ErrMsg NVARCHAR(200) = '' OUTPUT
		AS
		BEGIN
			SELECT @ErrCode = 0, @ErrMsg = N'Thêm phiếu bảo hành mới thành công!'
	
			IF  EXISTS (SELECT 1 FROM PHIEUBAOHANH WHERE SERIALBH=@SERIALBH)
				BEGIN
					SELECT @ErrCode = 1, @ErrMsg = N'Phiếu bảo hành này đã tồn tại!'
					return
				END
			DECLARE @NGAYLAPPBH DATETIME
			SELECT @NGAYLAPPBH=NGAYBAN FROM HDB WHERE MAHDB=@MAHDB
			IF  (DATEADD(month, (SELECT TGBHBAN FROM CTHDB  WHERE MAH=@MAH and MAHDB = @MAHDB),@NGAYLAPPBH)<GETDATE())
				BEGIN
					SELECT @ErrCode = 1, @ErrMsg = N'Hàng không còn thời hạn bảo hành'
					return
				END
			INSERT INTO PHIEUBAOHANH (MAHDB,MAH,MANV,NGAYLAPPBH,SERIALBH,TINHTRANGPBH) VALUES(@MAHDB,@MAH,@MANV,@NGAYLAPPBH,@SERIALBH,1)
			SELECT @ErrCode=0, @ErrMsg = N'Thêm thành công phiếu bảo hành: '+@MAHDB+'_'+@MAH
		END	
		GO
	------------------------------------------------------------------------------------------
	/****** Object:  StoredProcedure [dbo].[sp_PHIEUBAOHANH_Update]     ******/
	SET ANSI_NULLS ON
	GO
	SET QUOTED_IDENTIFIER ON
	GO
	CREATE PROCEDURE [dbo].[sp_PHIEUBAOHANH_Update]	
			AS
				BEGIN
					SET DATEFORMAT DMY
					UPDATE PHIEUBAOHANH 
					SET TINHTRANGPBH = CASE 
										WHEN (DATEADD(month, (SELECT C.TGBHBAN FROM CTHDB C WHERE MAH=C.MAH and c.MAHDB =MAHDB),NGAYLAPPBH)>GETDATE()) THEN 2 
										ELSE 1
									 END
					WHERE TINHTRANGPBH !=0
					
				END
				 GO
	----------------------------------------------------------------------------------------------------------
	/****** Object:  StoredProcedure [dbo].[sp_PHIEUBAOHANH_Getlist]   ******/
	SET ANSI_NULLS ON
	GO
	SET QUOTED_IDENTIFIER ON
	GO
	CREATE PROCEDURE [dbo].[sp_PHIEUBAOHANH_Getlist]
		AS
		BEGIN
			SELECT ROW_NUMBER() OVER (ORDER BY (SELECT 1)) AS STT,IDPBH AS N'Mã phiếu bảo  hành',
			MAHDB AS N'Mã hóa đơn bán',p.MAH AS N'Mã hàng',TENH AS N'Tên hàng',p.MANV AS N'Mã nhân viên',TENNV AS N'Tên nhân viên',SERIALBH AS N'Số Serial thiết bị',
			case when TINHTRANGPBH =1 then N'Còn bảo hành' when TINHTRANGPBH =2 then N'Hết hạn bảo hành' end  AS N'Tình trạng bảo hành'
			FROM PHIEUBAOHANH p,NHANVIEN n, HANG h 
			where p.MAH = h.MAH and p.MANV = n.MANV	
		END	
		GO

	---[dbo].[sp_TB_HDB_ChuaPBH_Getlist]
	/****** Object:  StoredProcedure [dbo].[[sp_TB_HDB_PBH_GetlistMAHB]]   ******/
	SET ANSI_NULLS ON
	GO
	SET QUOTED_IDENTIFIER ON
	GO
	create PROCEDURE [dbo].[sp_TB_HDB_PBH_GetlistMAHB]
	@MAHDB CHAR(10)=''
		AS
		BEGIN
		
			SELECT ROW_NUMBER() OVER (ORDER BY (SELECT 1)) AS STT,C.MAH as N'Mã hàng',TENH as N'Tên hàng',SOLUONGB as N'Số lượng đã bán',
			TGBH as N'Thời gian bảo hành'
			,(SELECT COUNT(MAH)AS 'SLDL'  FROM PHIEUBAOHANH P WHERE P.MAH=C.MAH AND P.MAHDB=C.MAHDB) AS 'Số lượng đã lập bảo hành',
			NGAYBAN as N'Ngày bán'
			FROM HDB B,CTHDB C,HANG H WHERE C.MAH=H.MAH AND C.MAHDB=@MAHDB AND B.MAHDB=C.MAHDB
		END	
		GO
		
/****** Object:  StoredProcedure [dbo].[sp_TB_HDB_PBH_GetlistMAHDB_MH]   ******/
	SET ANSI_NULLS ON
	GO
	SET QUOTED_IDENTIFIER ON
	GO
	create PROCEDURE [dbo].sp_TB_HDB_PBH_GetlistMAHDB_MH
	@MAHDB CHAR(10),
	@MAH CHAR(10)
		AS
		BEGIN
		
			SELECT ROW_NUMBER() OVER (ORDER BY (SELECT 1)) AS STT,C.MAHDB,C.MAH,TENH,SOLUONGB,TGBH
			,(SELECT COUNT(MAH)AS 'SLDL'  FROM PHIEUBAOHANH P WHERE P.MAH=C.MAH AND P.MAHDB=C.MAHDB) AS 'SLL',NGAYBAN
			FROM HDB B,CTHDB C,HANG H WHERE c.MAH=@MAH and C.MAH=H.MAH AND C.MAHDB=@MAHDB AND B.MAHDB=C.MAHDB 
		END	
	GO
---[dbo].[sp_TB_HDB_DaPBH_Getlist]
	SET ANSI_NULLS ON
	GO
	SET QUOTED_IDENTIFIER ON
	GO
	create PROCEDURE [dbo].[sp_TB_HDB_DaPBH_GetlistMAHB]
	@MAHDB CHAR(10)=''
		AS
		BEGIN
			SELECT ROW_NUMBER() OVER (ORDER BY (SELECT 1)) AS STT,P.IDPBH AS N'Mã phiếu',P.MAHDB AS N'Mã hóa đơn bán',P.MAH AS N'Mã hàng',
			TENH AS N'Tên hàng',NGAYLAPPBH AS N'Ngày lập',SERIALBH AS N'Số serial'
			,CASE WHEN TINHTRANGPBH =0 THEN  N'Đã hủy' WHEN TINHTRANGPBH=1 THEN N'Còn hạn bảo hành'	else N'Hết hạn bảo hành' end AS N'Tình trạng'
				FROM HANG H, PHIEUBAOHANH P WHERE P.MAHDB=@MAHDB AND P.MAH=H.MAH			
		END	
GO
/****** Object:  StoredProcedure [dbo].[sp_PHIEUBAOHANH_GetlistByID]   ******/
	SET ANSI_NULLS ON
	GO                
	SET QUOTED_IDENTIFIER ON
	GO
	CREATE PROCEDURE [dbo].[sp_PHIEUBAOHANH_GetlistByID]
	@IDPBH INT=0
		AS
		BEGIN
			select 
			p.MAHDB,p.MAH,TENH ,p.MANV,TENNV ,SERIALBH , TINHTRANGPBH,
			case when TINHTRANGPBH =1 then N'Còn bảo hành' when TINHTRANGPBH =2 then N'Hết hạn bảo hành' end  AS N'Tình trạng bảo hành',
			hd.MAKH,hd.TENKHB,hd.SDTKHB
			FROM PHIEUBAOHANH p,NHANVIEN n, HANG h ,HDB hd
			where p.IDPBH = @IDPBH AND p.MAH = h.MAH and p.MANV = n.MANV and p.MAHDB=hd.MAHDB
		END
			GO
---------------------------------------------
/****** Object:  StoredProcedure [dbo].[sp_PHIEUBAOHANH_GetlistByID_Export]   ******/
	SET ANSI_NULLS ON
	GO                
	SET QUOTED_IDENTIFIER ON
	GO
	CREATE PROCEDURE [dbo].sp_PHIEUBAOHANH_GetlistByID_Export
	@IDPBH INT=0
		AS
		BEGIN
			select 
			p.MAHDB,p.MAH,TENH ,p.MANV,TENNV ,SERIALBH ,NGAYLAPPBH,
			DATEADD(month, (SELECT C.TGBHBAN FROM CTHDB C WHERE MAH=P.MAH and c.MAHDB =HD.MAHDB),NGAYLAPPBH) as 'NGAYHETHAN',
			hd.MAKH,hd.TENKHB,hd.SDTKHB,hd.DIACHIKHB
			FROM PHIEUBAOHANH p,NHANVIEN n, HANG h ,HDB hd
			where p.IDPBH = @IDPBH AND p.MAH = h.MAH and p.MANV = n.MANV and p.MAHDB=hd.MAHDB
		END
			GO	
-------------------------------------------------------------------------------------------------
/****** Object:  StoredProcedure [dbo].[sp_BAOHANH_Add]     ******/
	SET ANSI_NULLS ON
	GO
	SET QUOTED_IDENTIFIER ON
	GO
	create PROCEDURE [dbo].[sp_BAOHANH_Add] 			
		    @IDPBH			int=0,
		    @NGUYENNHAN		NVARCHAR(200)='',
		    @MANV                 char(6)='',
		    @NGAYGIAOBH           datetime,		    
			@SDT         varchar(11) =null,
			@ErrCode int = 0 OUTPUT,
			@ErrMsg NVARCHAR(200) = '' OUTPUT
		AS
		BEGIN
			if exists (SELECT * FROM BAOHANH WHERE IDPBH=@IDPBH AND TINHTRANGBH = 2)
				BEGIN
					SELECT @ErrCode = 1, @ErrMsg = N'Thiết bị đang được bảo hành.Không thể lập'	
					return
				END
			Set Dateformat DMY
			SELECT @ErrCode = 0, @ErrMsg = N'Thêm bảo hành mới thành công!'	
			INSERT INTO BAOHANH (IDPBH,NGUYENNHAN,MANV,NGAYLAPBH,NGAYGIAOBH,TINHTRANGBH,SDTNHAN)
			VALUES(@IDPBH,@NGUYENNHAN, @MANV, GETDATE(), @NGAYGIAOBH, 2,@SDT)					
		END	
		 GO
/****** Object:  StoredProcedure [dbo].[sp_BAOHANH_Update]     ******/
	SET ANSI_NULLS ON
	GO
	SET QUOTED_IDENTIFIER ON
	GO
	create PROCEDURE [dbo].[sp_BAOHANH_UpdateByID]
			@IDBH INT,
		    @NGUYENNHAN		NVARCHAR(200),
		    @NGAYGIAOBH           datetime,
		    @TINHTRANGBH         tinyint
			
			AS
				BEGIN
					Set Dateformat DMY
					UPDATE BAOHANH
					SET NGUYENNHAN=@NGUYENNHAN, NGAYGIAOBH=@NGAYGIAOBH, TINHTRANGBH=@TINHTRANGBH
					WHERE IDBH=@IDBH
					return
				END
	go
				
/****** Object:  StoredProcedure [dbo].[[sp_BAOHANH_GetlistByIDPBH]]   ******/
	SET ANSI_NULLS ON
	GO
	SET QUOTED_IDENTIFIER ON
	GO
	CREATE PROCEDURE [dbo].[sp_BAOHANH_GetlistByIDPBH]
	@IDPBH int=0
		AS
		BEGIN
			SELECT ROW_NUMBER() OVER (ORDER BY (SELECT 1)) AS STT,IDBH AS N'Mã bảo hành',b.IDPBH AS N'Mã phiếu bảo hành',p.MAH AS N'Mã hàng',h.TENH AS N'Tên hàng',
			p.SERIALBH AS N'Số Serial',
			NGUYENNHAN AS N'Nguyên nhân',hb.MAKH AS N'Mã Khách hàng',hb.TENKHB AS N'Tên khách hàng',b.SDTNHAN AS N'Số điện thoại liên lạc',TINHTRANGBH AS N'Tình trạng phiếu bảo hành'
			,b.MANV AS N'Mã nhân viên lập ', TENNV AS N'Tên nhân viên',NGAYLAPBH AS N'Ngày lập',NGAYGIAOBH AS N'Ngày giao',b.TINHTRANGBH AS N'Tình trạng bảo hành'
			FROM BAOHANH b,NHANVIEN n,PHIEUBAOHANH p,HANG h,HDB hb
			where	b.IDPBH=@IDPBH and b.MANV =n.MANV and b.IDPBH = p.IDPBH and p.MAH=h.MAH and p.MAHDB=hb.MAHDB
		END	
		GO

	/****** Object:  StoredProcedure [dbo].[[sp_BAOHANH_GetlistByIDBH]]   ******/
	SET ANSI_NULLS ON
	GO
	SET QUOTED_IDENTIFIER ON
	GO
		CREATE PROCEDURE [dbo].[sp_BAOHANH_GetlistByIDBH]
		@IDBH int=0
		AS
		BEGIN
			SELECT ROW_NUMBER() OVER (ORDER BY (SELECT 1)) AS STT,IDBH AS N'Mã bảo hành',b.IDPBH AS N'Mã phiếu bảo hành',p.MAH AS N'Mã hàng',h.TENH AS N'Tên hàng',
			NGUYENNHAN AS N'Nguyên nhân',b.SDTNHAN AS N'Số điện thoại liên lạc',TINHTRANGBH AS N'Tình trạng phiếu bảo hành'
			,b.MANV AS N'Mã nhân viên lập ', TENNV AS N'Tên nhân viên',NGAYLAPBH AS N'Ngày lập',NGAYGIAOBH AS N'Ngày giao'
			FROM BAOHANH b,NHANVIEN n,PHIEUBAOHANH p,HANG h
			where	b.IDBH=@IDBH and b.MANV =n.MANV and b.IDPBH = p.IDPBH and p.MAH=h.MAH
		END
		GO

		/****** Object:  StoredProcedure [dbo].[[sp_BAOHANH_Getlist]]   ******/
	SET ANSI_NULLS ON
	GO
	SET QUOTED_IDENTIFIER ON
	GO
		CREATE PROCEDURE [dbo].[sp_BAOHANH_Getlist]
		AS
		BEGIN
			SELECT ROW_NUMBER() OVER (ORDER BY (SELECT 1)) AS STT,IDBH AS N'Mã bảo hành',b.IDPBH AS N'Mã phiếu bảo hành',p.MAH AS N'Mã hàng',h.TENH AS N'Tên hàng',
			NGUYENNHAN AS N'Nguyên nhân',b.SDTNHAN AS N'Số điện thoại liên lạc',TINHTRANGBH AS N'Tình trạng phiếu bảo hành'
			,b.MANV AS N'Mã nhân viên lập ', TENNV AS N'Tên nhân viên',NGAYLAPBH AS N'Ngày lập',NGAYGIAOBH AS N'Ngày giao'
			FROM BAOHANH b,NHANVIEN n,PHIEUBAOHANH p,HANG h
			where	 b.MANV =n.MANV and b.IDPBH = p.IDPBH and p.MAH=h.MAH
		END
		 GO

				/****** Object:  StoredProcedure [dbo].[[sp_BAOHANH_GetlistEX]   ******/
	SET ANSI_NULLS ON
	GO
	SET QUOTED_IDENTIFIER ON
	GO
		alter PROCEDURE [dbo].[sp_BAOHANH_GetlistEx]
		@IDBH int=0
		AS
		BEGIN

			SELECT IDBH,b.IDPBH,h.TENH ,p.SERIALBH,
			NGUYENNHAN ,b.SDTNHAN,CASE WHEN TINHTRANGBH =0 THEN N'Đã hủy' when TINHTRANGBH =1 then N'Đã trả' else N'Đang bảo hành' end as
			TINHTRANGBH 
			,b.MANV, TENNV,NGAYLAPBH,NGAYGIAOBH,HD.TENKHB,hd.SDTKHB
			FROM BAOHANH b,NHANVIEN n,PHIEUBAOHANH p,HANG h,HDB hd
			where	 b.MANV =n.MANV and b.IDPBH = p.IDPBH and p.MAH=h.MAH and b.IDBH =@IDBH  and p.MAHDB=hd.MAHDB
		END
		 GO
-------------------------------------------------------------------------------------------------
CREATE FUNCTION F_CREATE_ID_HDSC()
	RETURNS CHAR(10)
	AS
	BEGIN
		DECLARE @ID INT =0;
		if(select COUNT(ID) from SUACHUA) >0
			begin
				SELECT @ID = MAX(ID) FROM SUACHUA
			end
		else set @ID  =0
		SET @ID =@ID+1;
		IF(@ID <10)
			RETURN CONCAT('S00000000', @ID);
		IF (@ID <100)
			RETURN CONCAT('S0000000', @ID);
		IF (@ID<1000) 
			RETURN CONCAT('S000000', @ID);
		IF (@ID <10000) 
			RETURN CONCAT('S00000',@ID);
		IF (@ID <100000) 
			RETURN CONCAT('S0000',@ID);
		IF (@ID <1000000) 
			RETURN CONCAT('S000',@ID);
		IF (@ID<10000000) 
			RETURN CONCAT('S00',@ID);
		IF (@ID <100000000) 
			RETURN CONCAT('S0',@ID);
			RETURN CONCAT('0000000000',@ID);
	END
	 GO
/****** Object:  StoredProcedure [dbo].[sp_SUACHUA_Add]     ******/
	SET ANSI_NULLS ON
	GO
	SET QUOTED_IDENTIFIER ON
	GO
	CREATE PROCEDURE [dbo].[sp_SUACHUA_Add] 
			
			@MAKH INT,
			@MANV CHAR(6)='',
		    @TENKHSC nvarchar(50),
			@SDTKHSC CHAR(11),
			@NGAYGIAOSC DATETIME,
			@TONGCHIPHISC FLOAT=0,			
			@TINHTRANGSC tinyint =0,
			@ErrCode int = 0 OUTPUT,
		@ErrMsg NVARCHAR(100) = '' OUTPUT,
			@MASC CHAR(10)='0000000000' OUTPUT
			
		AS
		BEGIN
			
			SELECT  @MASC = DBO.F_CREATE_ID_HDSC()
			IF( CONVERT(char,@MASC) = '0000000000')
			BEGIN
				SELECT @ErrCode = 1, @ErrMsg = N'Dữ liệu chứa đã đầy'
				return
			END
			set dateformat DMY
			INSERT INTO SUACHUA (MASC,MAKH,MANV,TENKHSC,SDTKHSC,NGAYNHANSC,NGAYGIAOSC,TONGCHIPHISC,TINHTRANGSC)
			 VALUES(@MASC,@MAKH,@MANV,@TENKHSC,@SDTKHSC, GETDATE(),@NGAYGIAOSC, @TONGCHIPHISC, @TINHTRANGSC)
			
			SELECT @ErrCode = 0, @ErrMsg = N'Cất hóa đơn thành công'
			
		END
			 GO
	------------------------------------------------------------------------------------------
	/****** Object:  StoredProcedure [dbo].[sp_SUACHUA_Update]     ******/
	SET ANSI_NULLS ON
	GO
	SET QUOTED_IDENTIFIER ON
	GO
	CREATE PROCEDURE [dbo].[sp_SUACHUA_Update]
			@MASC char(10),
		    @TENKHSC nvarchar(50),
			@SDTKHSC CHAR(11),
			@NGAYGIAOSC DATETIME,
			@TONGCHIPHISC FLOAT=0,			
			@TINHTRANGSC tinyint =0,
			@ErrCode int = 0 OUTPUT,
			@ErrMsg NVARCHAR(200) = '' OUTPUT
	AS
	BEGIN
		SELECT @ErrCode = 0, @ErrMsg = N'Cập nhật sửa chữa thành công!'	
		set dateformat DMY
		UPDATE SUACHUA
		SET TENKHSC=@TENKHSC,SDTKHSC=@SDTKHSC,NGAYGIAOSC=@NGAYGIAOSC,TONGCHIPHISC=@TONGCHIPHISC,TINHTRANGSC=@TINHTRANGSC
		WHERE MASC=@MASC
	END 
	GO
	------------------------------------------------------------------------------------------------------------

/****** Object:  StoredProcedure [dbo].[sp_SUACHUA_Getlist]    ******/
	SET ANSI_NULLS ON
	GO
	SET QUOTED_IDENTIFIER ON
	GO
	CREATE PROCEDURE [dbo].[sp_SUACHUA_Getlist]
		AS
		BEGIN
			SELECT ROW_NUMBER() OVER (ORDER BY (SELECT 1)) AS STT,MASC AS N'Mã sửa chữa',MAKH AS N'Mã khách hàng',TENKHSC AS N'Tên khách hàng',SDTKHSC AS N'SDT'
			,NGAYNHANSC AS N'Ngày nhận',NGAYGIAOSC AS N'Ngày giao',TONGCHIPHISC AS N'Tổng chi phí',
			CASE WHEN TINHTRANGSC=0 THEN N'Đang xử lý' WHEN TINHTRANGSC=1 THEN N'Đã sửa' ELSE N'Đã giao' end  AS N'Tình trạng'
			FROM SUACHUA	
		END	
		GO

/****** Object:  StoredProcedure [dbo].[sp_SUACHUA_GetlistByID]    ******/
	SET ANSI_NULLS ON
	GO
	SET QUOTED_IDENTIFIER ON
	GO
	CREATE PROCEDURE [dbo].sp_SUACHUA_GetlistByID
	@MASC CHAR(10) ='0000000000'
		AS
		BEGIN
			SELECT ROW_NUMBER() OVER (ORDER BY (SELECT 1)) AS STT,MASC AS N'Mã sửa chữa',MAKH AS N'Mã khách hàng',TENKHSC AS N'Tên khách hàng',SDTKHSC AS N'SDT'
			,NGAYNHANSC AS N'Ngày nhận',NGAYGIAOSC AS N'Ngày giao',TONGCHIPHISC AS N'Tổng chi phí',TINHTRANGSC AS N'Tình trạng'
			FROM SUACHUA
			where MASC = @MASC	
		END	
		 GO
 ------------
 -------------------------------------------------------------------------------------------------
 ---
/****** Object:  StoredProcedure [dbo].[sp_CTHDSC_Add]     ******/
	SET ANSI_NULLS ON
	GO
	SET QUOTED_IDENTIFIER ON
	GO
	CREATE PROCEDURE [dbo].[sp_CTHDSC_Add] 
			@MASC                char(10),
			@MAH                  char(10),
			@TENTBSC              nvarchar(50),
			@LOISC                nvarchar(200),
			@MOTASC				nvarchar(200),
			@CHIPHISC             float=0,
			@TINHTRANGCTSC		BIT=0
		AS
		BEGIN
			INSERT INTO CTHDSC (MASC,MAH,TENTBSC,LOISC,MOTASC,CHIPHISC,TINHTRANGCTSC)
			VALUES (@MASC,@MAH,@TENTBSC,@LOISC,@MOTASC, @CHIPHISC, @TINHTRANGCTSC)
			if not existS (select * from CTHDSC  where MASC=@MASC and TINHTRANGCTSC = 0)
			begin
			UPDATE SUACHUA SET TINHTRANGSC=1 WHERE MASC=@MASC
			end		
		END	
		 GO
/****** Object:  StoredProcedure [dbo].[sp_CTHDSC_Update]     ******/
	SET ANSI_NULLS ON
	GO
	SET QUOTED_IDENTIFIER ON
	GO
	CREATE PROCEDURE [dbo].[sp_CTHDSC_Update]
			@SOHIEU				INT,
			@LOISC                nvarchar(200)='',
			@MOTASC				nvarchar(200)='',
			@CHIPHISC             float=0,
			@TINHTRANGCTSC		bit=0
			
	AS
	BEGIN
		UPDATE CTHDSC
		SET  LOISC=@LOISC, MOTASC=@MOTASC, CHIPHISC=@CHIPHISC, TINHTRANGCTSC=@TINHTRANGCTSC
		WHERE SOHIEU = @SOHIEU
		
		Declare @HDSC char(10)
		select @HDSC=MASC from CTHDSC  where SOHIEU=@SOHIEU
		UPdate SUACHUA
		set TONGCHIPHISC=(select sum(CHIPHISC) from CTHDSC where MASC=@HDSC)
		where MASC=@HDSC
				if not existS (select * from CTHDSC  where MASC=@HDSC and TINHTRANGCTSC = 0)
				begin
					UPDATE SUACHUA SET TINHTRANGSC=1 WHERE MASC=@HDSC
				end
				else 
						UPDATE SUACHUA SET TINHTRANGSC=0 WHERE MASC=@HDSC
					
	END
	 GO
/****** Object:  StoredProcedure [dbo].[sp_CTHDSC_Getlist]    ******/
	SET ANSI_NULLS ON
	GO
	SET QUOTED_IDENTIFIER ON
	GO
	CREATE PROCEDURE [dbo].[sp_CTHDSC_GetlistByID]
	@MASC CHAR(10) =''
		AS
		BEGIN
			SELECT ROW_NUMBER() OVER (ORDER BY (SELECT 1)) AS STT,MASC AS N'Mã sửa chữa',
			SOHIEU AS N'Số hiệu',
			MAH AS N'Mã hàng',TENTBSC AS N'Tên hàng',CHIPHISC AS N'Chi phí',
			CASE WHEN TINHTRANGCTSC =0 THEN N'Đang sửa' else N'Đã sửa'end  AS N'Tình trạng',MOTASC AS N'Mô tả',LOISC AS N'Lỗi',TINHTRANGCTSC AS N'TT'
			FROM CTHDSC
			WHERE MASC=@MASC
		END	 
		GO
/****** Object:  StoredProcedure [dbo].[sp_CTHDSC_Getlist]    ******/
	SET ANSI_NULLS ON
	GO
	SET QUOTED_IDENTIFIER ON
	GO
	create PROCEDURE [dbo].[sp_CTHDSC_GetlistByIDInfo]
	@MASC CHAR(10) =''
		AS
		BEGIN
			SELECT ROW_NUMBER() OVER (ORDER BY (SELECT 1)) AS STT,
			SOHIEU AS N'Số hiệu',
			MAH AS N'Mã hàng',TENTBSC AS N'Tên hàng',CHIPHISC AS N'Chi phí',
			CASE WHEN TINHTRANGCTSC =0 THEN N'Đang sửa' else N'Đã sửa'end  AS N'Tình trạng',MOTASC AS N'Mô tả',LOISC AS N'Lỗi'
			FROM CTHDSC
			WHERE MASC=@MASC
		END	
	
			 GO


/****** Object:  StoredProcedure [dbo].[sp_SUACHUA_Getlistexport]    ******/
	SET ANSI_NULLS ON
	GO
	SET QUOTED_IDENTIFIER ON
	GO
	alter PROCEDURE [dbo].sp_SUACHUA_Getlistexport
	@MASC CHAR(10) ='0000000000'
		AS
		BEGIN
			 select s.MASC,s.MAKH,s.TENKHSC,s.SDTKHSC,s.NGAYNHANSC,s.NGAYGIAOSC,s.TONGCHIPHISC,c.SOHIEU,c.TENTBSC,c.LOISC,c.MOTASC,c.CHIPHISC,
			 CASE WHEN TINHTRANGSC=0 THEN N'Đang xử lý' WHEN TINHTRANGSC=1 THEN N'Đã sửa' ELSE N'Đã giao' end  AS N'TTSC'
			from SUACHUA s,CTHDSC c 
			where s.MASC=c.MASC and s.MASC = @MASC	
		END	
		 GO
		