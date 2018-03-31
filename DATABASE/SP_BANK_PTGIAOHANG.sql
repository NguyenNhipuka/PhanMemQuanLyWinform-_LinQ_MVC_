------------------------------------------------------------------------------------------	
    /****** Object:  StoredProcedure [dbo].[sp_BANK_Add]     ******/
	SET ANSI_NULLS ON
	GO
	SET QUOTED_IDENTIFIER ON
	GO
	CREATE PROCEDURE [dbo].[sp_BANK_Add] 
	@TENNH NVARCHAR(30),
	@ErrCode int = 0 OUTPUT,
	@ErrMsg NVARCHAR(100) = '' OUTPUT
	AS
	BEGIN
		IF EXISTS (SELECT 1 FROM BANK WHERE UPPER(TENNH) = UPPER(@TENNH))
		BEGIN
			SELECT @ErrCode = 1, @ErrMsg = N'Đã tồn tại tên ngân hàng: '+@TENNH +' này'
			RETURN
		END
		INSERT INTO BANK VALUES(@TENNH)
		SELECT  @ErrMsg = N'Thêm thành công ngân hàng: '+@TENNH
	END
	GO
	------------------------------------------------------------------------------------------
	/****** Object:  StoredProcedure [dbo].[sp_BANK_Update]     ******/
	SET ANSI_NULLS ON
	GO
	SET QUOTED_IDENTIFIER ON
	GO
	create PROCEDURE [dbo].[sp_BANK_Update]
	@MANH INT,
	@TENNH NVARCHAR(30),
	@ErrCode int = 0 OUTPUT,
	@ErrMsg NVARCHAR(100) = '' OUTPUT
	AS
	BEGIN
		IF NOT EXISTS (SELECT * FROM BANK WHERE UPPER(TENNH) = UPPER(@TENNH))
		BEGIN
			Declare @tennh_cu nvarchar(30)
			SELECT @tennh_cu = TENNH FROM BANK WHERE MANH = @MANH
			UPDATE BANK
			SET TENNH= @TENNH WHERE MANH=@MANH
			SELECT  @ErrMsg = N'Cập nhật thành công ngân hàng: '+@tennh_cu +' thành '+@TENNH
			RETURN
		END
			SELECT @ErrCode = 1, @ErrMsg = N'Tên ngân hàng đã tồn tại!'

	END
	GO
	
	---------------------------------------------------------------------------------------------------------------
	/****** Object:  StoredProcedure [dbo].[sp_BANK_Getlist]    ******/
	SET ANSI_NULLS ON
	GO
	SET QUOTED_IDENTIFIER ON
	GO
	CREATE PROCEDURE [dbo].[sp_BANK_Getlist]
		AS
		BEGIN
			SELECT ROW_NUMBER() OVER (ORDER BY (SELECT 1)) AS STT,*
			FROM BANK	
		END
		GO
	----------------------------------------------------------------------------------------------------------------
	/****** Object:  StoredProcedure [dbo].[sp_BANK_Getlist_ByKey]   ******/
	SET ANSI_NULLS ON
	GO
	SET QUOTED_IDENTIFIER ON
	GO
	CREATE PROCEDURE [dbo].[sp_BANK_Getlist_ByKey]
			@TENNH NVARCHAR(30) = '',
			@MANH INT
		AS
		BEGIN
			IF(@TENNH !='' and @MANH ='')
			BEGIN
				SELECT *
				FROM BANK
				WHERE @TENNH LIKE '%' + @TENNH + '%'	
				RETURN
			END
				SELECT *
				FROM BANK
				WHERE  @MANH=MANH		
		END
		GO
	--------------------------------------------------------------------------------------------------------
	/****** Object:  StoredProcedure [dbo].[sp_BANK_Delete]     ******/
	SET ANSI_NULLS ON
	GO
	SET QUOTED_IDENTIFIER ON
	GO
	create PROCEDURE [dbo].[sp_BANK_Delete]
	@MANH INT,
	@ErrCode int = 0 OUTPUT,
	@ErrMsg NVARCHAR(200) = '' OUTPUT
	AS
	BEGIN
		SET NOCOUNT ON
		IF EXISTS (SELECT 1 FROM NCC WHERE MANH = @MANH)
		BEGIN
			SELECT @ErrCode = 1, @ErrMsg = N'Ngân hàng này đang được sử dụng: '
			RETURN
		END
		IF (@MANH=1)
		BEGIN
			SELECT @ErrCode = 1, @ErrMsg = N'Ngân hàng này đang được sử dụng: '
			RETURN
		END
		DELETE FROM BANK
		WHERE MANH=@MANH
		SELECT @ErrCode = 0,@ErrMsg =N'Xóa thành công ngân hàng có mã: '+@MANH	
	END
	GO



	------------------------------------------------------------------------------------------------------------
	/****** Object:  StoredProcedure [dbo].[sp_PTGIAOHANG_Add]     ******/
	SET ANSI_NULLS ON
	GO
	SET QUOTED_IDENTIFIER ON
	GO
	CREATE PROCEDURE [dbo].[sp_PTGIAOHANG_Add] 
			@TENPTGH nVARCHAR(50) ='',			
			@PHIPTGH FLOAT,			
			@ErrCode int = 0 OUTPUT,
			@ErrMsg NVARCHAR(200) = '' OUTPUT		
		AS
	BEGIN
		IF EXISTS (SELECT 1 FROM PTGIAOHANG WHERE UPPER(TENPTGH) = UPPER(@TENPTGH))
		BEGIN
			SELECT @ErrCode = 1, @ErrMsg = N'Đã tồn tại tên PTGH: '+@TENPTGH +' này'
			RETURN
		END
		INSERT INTO PTGIAOHANG(TENPTGH, PHIPTGH) VALUES(@TENPTGH, @PHIPTGH)
		SELECT  @ErrMsg = N'Thêm thành công PTGH: '+@TENPTGH+N'với phí: '+@PHIPTGH
	END
	GO

	------------------------------------------------------------------------------------------
	/****** Object:  StoredProcedure [dbo].[sp_PTGIAOHANG_Update]     ******/
	SET ANSI_NULLS ON
	GO
	SET QUOTED_IDENTIFIER ON
	GO
	CREATE PROCEDURE [dbo].[sp_PTGIAOHANG_Update]
		@MAPTGH INT,
		@TENPTGH NVARCHAR(50),
		@PHIPTGH FLOAT,
		@ErrCode int = 0 OUTPUT,
		@ErrMsg NVARCHAR(100) = '' OUTPUT
	
	AS
	BEGIN
		SELECT @ErrCode = 0, @ErrMsg = N'Cập nhật PTGH thành công!'	
		IF NOT  EXISTS (SELECT 1 FROM PTGIAOHANG WHERE @MAPTGH=MAPTGH)
			BEGIN
				SELECT @ErrCode = 1, @ErrMsg = N'PTGH này không tồn tại!'
				return
			END
		UPDATE PTGIAOHANG
		SET TENPTGH=@TENPTGH, PHIPTGH=@PHIPTGH
		WHERE MAPTGH=@MAPTGH		
	END
	GO		
	---------------------------------------------------------------------------------------------------------------
	/****** Object:  StoredProcedure [dbo].[sp_PTGIAOHANG_Getlist]    ******/
	SET ANSI_NULLS ON
	GO
	SET QUOTED_IDENTIFIER ON
	GO
	CREATE PROCEDURE [dbo].[sp_PTGIAOHANG_Getlist]
		AS
		BEGIN
			SELECT ROW_NUMBER() OVER (ORDER BY (SELECT 1)) AS STT,*
			FROM PTGIAOHANG	
		END
	GO	
	/****** Object:  StoredProcedure [dbo].[sp_PTGIAOHANG_Getlist_ByKey]   ******/
	SET ANSI_NULLS ON
	GO
	SET QUOTED_IDENTIFIER ON
	GO
	CREATE PROCEDURE [dbo].[sp_PTGIAOHANG_Getlist_ByKey]
			@TENPTGH NVARCHAR(50) = '',
			@MAPTGH INT
		AS
		BEGIN
			IF(@TENPTGH !='' and @MAPTGH ='')
			BEGIN
				SELECT *
				FROM PTGIAOHANG
				WHERE @TENPTGH LIKE '%' + @TENPTGH + '%'	
				RETURN
			END
				SELECT *
				FROM PTGIAOHANG
				WHERE  @MAPTGH=MAPTGH
			END
		GO
	--------------------------------------------------------------------------------------------------------
	/****** Object:  StoredProcedure [dbo].[sp_PTGIAOHANG_Delete]     ******/
	SET ANSI_NULLS ON
	GO
	SET QUOTED_IDENTIFIER ON
	GO

	CREATE PROCEDURE [dbo].[sp_PTGIAOHANG_Delete]
	@MAPTGH INT,
	@ErrCode int = 0 OUTPUT,
	@ErrMsg NVARCHAR(200) = '' OUTPUT
	AS
	BEGIN
		IF EXISTS (SELECT 1 FROM HDB WHERE MAPTGH = @MAPTGH)
		BEGIN
			SELECT @ErrCode = 1, @ErrMsg = N'Phương thức giao hàng này đang được sử dụng: '
			RETURN
		END
		DELETE FROM PTGIAOHANG
		WHERE MAPTGH=@MAPTGH
		SELECT @ErrMsg =N'Xóa thành công phương thức giao hàng có mã: '+@MAPTGH	
	END
	GO		