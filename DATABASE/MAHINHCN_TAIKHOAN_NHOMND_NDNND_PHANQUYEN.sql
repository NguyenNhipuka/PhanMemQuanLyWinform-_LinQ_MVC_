
----[dbo].[sp_User_Login]
----[dbo].[sp_CheckStatus_User]
----[dbo].[sp_User_ResetPassword]
--- [dbo].[sp_Delete_User]
----[dbo].[sp_Add_User] 
----[dbo].[sp_Update_User] 
----[dbo].[sp_Update_QuyenUser] //cập nhật trạng thái và các nhóm quyền của user
----[dbo].[sp_LayQuyen] //LAY QUYEN THEO MA NHANVIEN
----[dbo].[sp_LayQuyen_NV]  // lấy quyền và nhóm quyền (checkbox)
----[dbo].[sp_TaiKhoan_Getlist_ByName]
----[dbo].[sp_TaiKhoan_Info]//lấy thông tin tài khoản để đổi mk
----[dbo].[sp_Create_TaiKhoan]
-----[dbo].[sp_NhomND_Getlist]
----[dbo].[sp_NhomND_Getlist_ByMaNV] 
------[dbo].[sp_Add_NhomQuyen_Cho_User]
------[dbo].[sp_Update_NhomQuyen_Cho_User] 
-----[dbo].[sp_NhomND_QUYEN_Getlist]
-----[dbo].[sp_PHANQUYEN_MANH_Getlist]
-----[dbo].[sp_Update_PHANQUYEN]
-----[dbo].[sp_Create_NhomQuyen]
-----[dbo].[sp_Delete_NhomQuyen]
-----[dbo].[sp_Update_NhomQuyen]
/****** Object:  StoredProcedure [dbo].[sp_User_Login]    NHI ******/
	SET ANSI_NULLS ON
	GO
	SET QUOTED_IDENTIFIER ON
	GO

	create PROCEDURE [dbo].[sp_User_Login] 
		@Username VARCHAR(50),
		@Password VARCHAR(100),
		@ErrCode int = 0 OUTPUT,
		@ErrMsg NVARCHAR(200) = '' OUTPUT
	AS
	BEGIN
		SELECT @ErrCode = 0, @ErrMsg = N'Đăng nhập thành công!'
	
		IF NOT EXISTS (SELECT 1 FROM TAIKHOANDN WHERE MANV = @Username)
		BEGIN
			SELECT @ErrCode = 1, @ErrMsg = N'Tên tài khoản không tồn tại.'
			RETURN
		END
	
		IF NOT EXISTS (SELECT 2 FROM TAIKHOANDN WHERE [MATKHAUTK] = @Password AND [MANV] = @Username)
		BEGIN
			SELECT @ErrCode = 2, @ErrMsg = N'Sai mật khẩu.'
			RETURN
		END
		DECLARE @TRANGTHAI INT=0;
		SELECT @TRANGTHAI=[TRANGTHAITK] FROM TAIKHOANDN WHERE [MANV] = @Username
		IF(@TRANGTHAI =0)
			BEGIN
				set @ErrCode = 3
				SET @ErrMsg = N'Tài khoản này đang bị khóa'
			END
		SELECT [MANV], [TENTK],[TRANGTHAITK]
		FROM TAIKHOANDN
		WHERE [MANV] = @Username
	END
	GO


/****** Object:  StoredProcedure [dbo].[sp_CheckStatus_User]   NHI  ******/
	SET ANSI_NULLS ON
	GO
	SET QUOTED_IDENTIFIER ON
	GO

	CREATE PROCEDURE [dbo].[sp_CheckStatus_User] 
		@Username VARCHAR(50)
	AS
	BEGIN

		SELECT [TRANGTHAITK] FROM TAIKHOANDN WHERE [MANV] = @Username
	END
	GO

/****** Object:  StoredProcedure [dbo].[sp_User_ResetPassword]     ******/
	SET ANSI_NULLS ON
	GO
	SET QUOTED_IDENTIFIER ON
	GO

	create PROCEDURE [dbo].[sp_User_ResetPassword]
	@MANV CHAR(6),
	@MATKHAUMOI VARCHAR(32)
	AS
	BEGIN
	
		UPDATE TAIKHOANDN 
		SET [MATKHAUTK] = @MATKHAUMOI, [NGAYRESET] = GETDATE() WHERE MANV = @MANV 
	
	END
	GO

	/****** Object:  StoredProcedure [dbo].[sp_Delete_User]     ******/
	SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

	create PROCEDURE [dbo].[sp_Delete_User]
	@MANV CHAR(6),---mã nhân viên xóa
	@MANVDELE CHAR(6),---mã nhân viên sẽ bị xóa
	@ErrCode int = 0 OUTPUT,
	@ErrMsg NVARCHAR(200) = N'' OUTPUT
	AS
	BEGIN
	--------
		SELECT @ErrCode=0,@ErrMsg= N'Xóa thành công tài khoản: '+@MANVDELE
		----QUYỀN QUẢN LÝ
		DECLARE @NHOMQUYEN_DELETE INT =0, @NHOMQUYENNV INT =0
		select @NHOMQUYEN_DELETE =MANHOM from NDNHOMND where MANHOM = 2 and MANV = @MANVDELE
		select @NHOMQUYENNV =MANHOM from NDNHOMND where MANHOM = 2 and MANV = @MANV
		---NHÓM QUYỀN IT, XÓA THẲNG
		IF(@NHOMQUYENNV =1)
			BEGIN
				DELETE NDNHOMND WHERE MANV=@MANVDELE
				DELETE TAIKHOANDN WHERE MANV=@MANVDELE
				RETURN
			END
		---- 2 nhân viên cùng nhóm quyền quản lý
		IF(@NHOMQUYENNV !=0 and @NHOMQUYEN_DELETE !=0 )
			BEGIN
				IF(@NHOMQUYEN_DELETE =@NHOMQUYENNV)
				 BEGIN
					 SELECT @ErrCode=1,@ErrMsg= N'Không thể xóa tài khoản đồng cấp quản lý'
					 return
				 END
			END
		ELSE----nhân viên xóa không không thuộc nhóm quyền quản lý, lại có quyền đucợ xóa tài khoản
			BEGIN
				IF(@NHOMQUYEN_DELETE = 2)
				 BEGIN
					 SELECT @ErrCode=1,@ErrMsg= N'Không thể xóa tài khoản có cấp cao hơn'
					 return
				 END
				
			END

		DELETE NDNHOMND WHERE MANV=@MANVDELE
		DELETE TAIKHOANDN WHERE MANV=@MANVDELE
	END
	GO

		/****** Object:  StoredProcedure [dbo].[sp_Add_User]     ******/
	SET ANSI_NULLS ON
	GO
	SET QUOTED_IDENTIFIER ON
	GO

	create PROCEDURE [dbo].[sp_Add_User]
	@MANV CHAR(6),
	@TEN NVARCHAR(50),
	@MATKHAUDN VARCHAR(100),
	@HOATDONG BIT=0,
	@ErrCode int = 0 OUTPUT,
	@ErrMsg NVARCHAR(200) = '' OUTPUT
	AS
	BEGIN
		IF EXISTS (SELECT * FROM TAIKHOANDN WHERE [MANV] = @MANV)
		BEGIN
			SELECT @ErrCode = 1, @ErrMsg = N'Đã tồn tại tài khoản:'+@MANV
			RETURN
		END
		IF not EXISTS (SELECT * FROM NHANVIEN WHERE [MANV] = @MANV)
		BEGIN
			SELECT @ErrCode = 1, @ErrMsg = N'Không tồn tại mã nhân viên này:'+@MANV
			RETURN
		END
		INSERT INTO TAIKHOANDN VALUES(@MANV,@TEN,@MATKHAUDN,@HOATDONG,0,GETDATE(),GETDATE())
		SELECT @ErrCode = 0, @ErrMsg = N'Tạo thành công tài khoản: '+@MANV
	END
	GO


	/****** Object:  StoredProcedure [dbo].[sp_Update_User]     ******/
	SET ANSI_NULLS ON
	GO
	SET QUOTED_IDENTIFIER ON
	GO

	CREATE PROCEDURE [dbo].[sp_Update_User]
	@MANV CHAR(6),
	@MK VARCHAR(100),
	@TEN NVARCHAR(50),
	@QUENMK BIT=1,
	@HOATDONG BIT=0,
	@ErrCode int = 0 OUTPUT,
	@ErrMsg NVARCHAR(200) = '' OUTPUT
	AS
	BEGIN
		IF not EXISTS (SELECT 1 FROM TAIKHOANDN WHERE [MANV] = @MANV)
		BEGIN
			SELECT @ErrCode = 1, @ErrMsg = N'Không tại tài khoản:'+@MANV
			RETURN
		END
		UPDATE TAIKHOANDN
		SET TENTK = @TEN,MATKHAUTK=@MK,QUENMK=@QUENMK,TRANGTHAITK = @HOATDONG
		WHERE MANV=@MANV
		SELECT  @ErrMsg = N'Cập nhật thành công tài khoản: '+@MANV
	END
	GO


/****** Object:  StoredProcedure [dbo].[sp_Update_QuyenUser]     ******/
	SET ANSI_NULLS ON
	GO
	SET QUOTED_IDENTIFIER ON
	GO

	CREATE PROCEDURE [dbo].[sp_Update_QuyenUser]
	@MANV CHAR(6),
	@HOATDONG BIT=0,
	@ErrCode int = 0 OUTPUT,
	@ErrMsg NVARCHAR(200) = '' OUTPUT
	AS
	BEGIN
		IF not EXISTS (SELECT 1 FROM TAIKHOANDN WHERE [MANV] = @MANV)
		BEGIN
			SELECT @ErrCode = 1, @ErrMsg = N'Không tại tài khoản:'+@MANV
			RETURN
		END
		UPDATE TAIKHOANDN
		SET TRANGTHAITK = @HOATDONG
		WHERE MANV=@MANV
		SELECT @ErrCode = 0, @ErrMsg = N'Cập nhật thành công tài khoản: '+@MANV
	END
	GO
/****** Object:  StoredProcedure [dbo].[sp_LayQuyen]    Script Date: 30/09/2017 10:27:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

	Create PROCEDURE [dbo].[sp_LayQuyen] 
		@MANV CHAR(6)
	AS
	BEGIN
		
		SELECT DISTINCT p.MACN
		FROM NDNHOMND nh, PHANQUYEN p
		WHERE [MANV] = @MANV and nh.MANHOM=p.MANHOM 
	END


/****** Object:  StoredProcedure [dbo].[sp_LayQuyen_NV]    Script Date: 30/09/2017 10:27:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

	CREATE PROCEDURE [dbo].[sp_LayQuyen_NV] 
		@MANV CHAR(6),
		@MANHOM INT
	AS
	BEGIN
		
		select n.MANHOM as N'Mã nhóm',p.MACN as N'Mã chức năng' ,m.TENMANHINH as N'Tên màn hình'
		from NDNHOMND n,PHANQUYEN p,MANCHUCNANG m 
		where n.MANHOM = p.MANHOM and n.MANV=@MANV and p.MACN = m.MACN AND P.MANHOM = @MANHOM

	END
	--------------------------------------------------------
	/****** Object:  StoredProcedure [dbo].[sp_TaiKhoan_Getlist_ByName]   nguyen thi yen nhi  ******/
	SET ANSI_NULLS ON
	GO
	SET QUOTED_IDENTIFIER ON
	GO

	create PROCEDURE [dbo].[sp_TaiKhoan_Getlist_ByName]
		@TENNV NVARCHAR(60) = '',
		@MANV CHAR(6)=''
	AS
	BEGIN
		IF(@TENNV !='')
		BEGIN
			SELECT *
			FROM TAIKHOANDN t
			WHERE T.TENTK LIKE '%' + @TENNV + '%'	
			RETURN
		END
			SELECT MANV as N'Mã tài khoản',TENTK as N'Tên tài khoản',TRANGTHAITK as N'Trạng thái',
			NGAYTAO as N'Ngày tạo',NGAYRESET as N'Ngày reset'
			FROM TAIKHOANDN
			WHERE  @MANV=MANV		
	END
	GO

	------------------------------------------------------
	/****** Object:  StoredProcedure [dbo].[sp_TaiKhoan_Info]   nguyen thi yen nhi  ******/
	SET ANSI_NULLS ON
	GO
	SET QUOTED_IDENTIFIER ON
	GO
	create PROCEDURE [dbo].[sp_TaiKhoan_Info]
		@MANV CHAR(6)='',
		@TENNV NVARCHAR(50)=''
	AS
	BEGIN
		IF(@TENNV !='')
		BEGIN
			SELECT *
			FROM TAIKHOANDN t
			WHERE T.TENTK LIKE '%' + @TENNV + '%'	
			RETURN
		END
			SELECT MANV as N'Mã tài khoản',TENTK as N'Tên tài khoản',TRANGTHAITK as N'Trạng thái',
			NGAYTAO as N'Ngày tạo',NGAYRESET as N'Ngày reset'
			FROM TAIKHOANDN
			WHERE  @MANV=MANV and MANV != '000001'
	END
	GO

	--------------------------------------------------------
	/****** Object:  StoredProcedure [dbo].[sp_TaiKhoan_Getlist]     ******/
	SET ANSI_NULLS ON
	GO
	SET QUOTED_IDENTIFIER ON
	GO

	create PROCEDURE [dbo].[sp_TaiKhoan_Getlist]
	AS
	BEGIN
			SELECT ROW_NUMBER() OVER (ORDER BY (SELECT 1)) AS STT, MANV AS N'Mã tài khoản' ,TENTK AS N'Tên tài khoản',TRANGTHAITK AS N'Trạng thái hoạt động',NGAYTAO AS N'Ngày tạo',NGAYRESET AS N'Ngày reset'
			FROM TAIKHOANDN		where MANV !='000001'
	END
	GO
		--------------------------------------------------------
	/****** Object:  StoredProcedure [dbo].[sp_Create_TaiKhoan]     ******/
	SET ANSI_NULLS ON
	GO
	SET QUOTED_IDENTIFIER ON
	GO

	CREATE PROCEDURE [dbo].[sp_Create_TaiKhoan]
		@MANV char(6),
		@TENNV NVARCHAR(60) = '',
		@MATKHAUTK varchar(100),
		@TRANGTHAITK bit =0,
		@ErrCode int = 0 OUTPUT,
		@ErrMsg NVARCHAR(200) = '' OUTPUT

	AS
	BEGIN
		SELECT @ErrCode = 0, @ErrMsg = 'Tạo tài khoản thành công'
		
		IF EXISTS (SELECT 1 FROM TAIKHOANDN WHERE MANV = @MANV)
			BEGIN
				SELECT @ErrCode = 1, @ErrMsg = 'Tài khoản đã tồn tại'
				RETURN
			END
		
		INSERT INTO TAIKHOANDN (MANV,TENTK,MATKHAUTK,TRANGTHAITK)
		VALUES (@MANV,@TENNV,@MATKHAUTK,@TRANGTHAITK)	
	END
	GO
	
/****** Object:  StoredProcedure [dbo].[sp_NhomND_Getlist]    Script Date: 10/10/2017 12:01:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

	create PROCEDURE [dbo].[sp_NhomND_Getlist]
	AS
	BEGIN
			select MANHOM AS N'Mã nhóm' ,TEMNHOM AS N'Tên nhóm',GHICHU AS N'Ghi chú' 
			FROM NHOMNGUOIDUNG where MANHOM !=1
	END

	go

	/****** Object:  StoredProcedure [dbo].[sp_NhomND_Getlist_ByMaNV]    Script Date: 10/10/2017 12:01:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

	create PROCEDURE [dbo].[sp_NhomND_Getlist_ByMaNV]
	@MANV char(6)
	AS
	BEGIN
			select n.MANHOM AS N'Mã nhóm' ,n.TEMNHOM AS N'Tên nhóm',nd.MANHOM AS N'Thuộc' 
			FROM NDNHOMND nd 
			RIGHT JOIN  NHOMNGUOIDUNG n 
			ON n.MANHOM <> 1 and nd.MANHOM != 1 and  n.MANHOM = nd.MANHOM  and nd.MANV = @MANV
			
	END
	go
/****** Object:  StoredProcedure [dbo].[sp_Add_NhomQuyen_Cho_User]    Script Date: 10/10/2017 12:01:07 ******/
	
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

	create PROCEDURE [dbo].[sp_Add_NhomQuyen_Cho_User]
	@MANV CHAR(6),
	@MANHOM INT
	AS
	BEGIN
		INSERT INTO NDNHOMND VALUES(@MANV,@MANHOM,N'Không có')
	END

/****** Object:  StoredProcedure [dbo].[sp_Update_NhomQuyen_Cho_User]    Script Date: 10/10/2017 12:01:07 ******/
	
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

	create PROCEDURE [dbo].[sp_Update_NhomQuyen_Cho_User]
	@MANV CHAR(6),
	@MANHOM INT,
	@FLAT BIT =0
	AS
	BEGIN
	SELECT * FROM NDNHOMND
		------XÓA 
		IF(@FLAT =0)
		BEGIN
			IF NOT EXISTS (SELECT * FROM NDNHOMND WHERE MANHOM=@MANHOM AND MANV=@MANV)
			BEGIN
				----CHƯA CHO PHÉP
				RETURN
			END	
			DELETE NDNHOMND WHERE MANHOM=@MANHOM AND MANV=@MANV
			RETURN
		END	
		---THÊM
		IF EXISTS (SELECT * FROM NDNHOMND WHERE MANHOM=@MANHOM AND MANV=@MANV)
			BEGIN
				----ĐÃ CHO PHÉP RỒI
				RETURN
			END	
		INSERT INTO NDNHOMND VALUES(@MANV,@MANHOM,N'Không có')
	END

/****** Object:  StoredProcedure [dbo].[sp_NhomND_QUYEN_Getlist]    Script Date: 10/10/2017 12:01:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

	CREATE PROCEDURE [dbo].[sp_PHANQUYEN_MANH_Getlist]
	@MANHOM INT
	AS
	BEGIN
			select M.MACN AS N'Mã chức năng',M.TENMANHINH AS N'Tên chức năng', P.MACN AS N'Có quyền'
			FROM MANCHUCNANG M 
			LEFT JOIN  PHANQUYEN P
			ON M.MACN = P.MACN AND P.MANHOM =@MANHOM
	END
GO


	--------------------------------------------------------
	/****** Object:  StoredProcedure [dbo].[sp_Create_NhomQuyen]     ******/
	SET ANSI_NULLS ON
	GO
	SET QUOTED_IDENTIFIER ON
	GO

	CREATE PROCEDURE [dbo].[sp_Create_NhomQuyen]
		@TEMNHOM NVARCHAR(50) = '',
		@GHICHU varchar(100),
		@ErrCode int = 0 OUTPUT,
		@ErrMsg NVARCHAR(200) = N'Thêm nhóm thành công' OUTPUT

	AS
	BEGIN
		SELECT @ErrCode = 0, @ErrMsg = N'Thêm nhóm thành công'
		
		IF EXISTS (SELECT * FROM NHOMNGUOIDUNG WHERE UPPER(TEMNHOM) = UPPER(@TEMNHOM))
			BEGIN
				SELECT @ErrCode = 1, @ErrMsg = N'Tên nhóm đã tồn tại'
				RETURN
			END
		
		INSERT INTO NHOMNGUOIDUNG VALUES(@TEMNHOM,@GHICHU)	
	END
	GO


	/****** Object:  StoredProcedure [dbo].[sp_Delete_NhomQuyen]     ******/
	SET ANSI_NULLS ON
	GO
	SET QUOTED_IDENTIFIER ON
	GO

	create PROCEDURE [dbo].[sp_Delete_NhomQuyen]
		@MANHOM INT,
		@ErrCode int = 0 OUTPUT,
		@ErrMsg NVARCHAR(200) = N'Xóa nhóm thành công' OUTPUT

	AS
	BEGIN
		SELECT @ErrCode = 0, @ErrMsg = N'Xóa nhóm thành công'
		
		IF EXISTS (SELECT * FROM NDNHOMND WHERE MANHOM=@MANHOM)
			BEGIN
				SELECT @ErrCode = 1, @ErrMsg = N'Nhóm này đang được sử dụng'
				RETURN
			END
		DELETE PHANQUYEN WHERE MANHOM=@MANHOM
		DELETE NHOMNGUOIDUNG WHERE MANHOM = @MANHOM
		
	END
	GO
-----[dbo].[sp_Update_NhomQuyen]

/****** Object:  StoredProcedure [dbo].[sp_Update_NhomQuyen]     ******/
	SET ANSI_NULLS ON
	GO
	SET QUOTED_IDENTIFIER ON
	GO

	CREATE PROCEDURE [dbo].[sp_Update_NhomQuyen]
		@MANHOM INT,
		@TEMNHOM NVARCHAR(50)='',
		@GHICHU NVARCHAR(100)='',
		@ErrCode int = 0 OUTPUT,
		@ErrMsg NVARCHAR(200) = N'Cập nhật thành công' OUTPUT

	AS
	BEGIN
		SELECT @ErrCode = 0, @ErrMsg =  N'Cập nhật thành công'
		
		IF EXISTS (SELECT * FROM NHOMNGUOIDUNG WHERE UPPER(TEMNHOM) = UPPER(@TEMNHOM) and MANHOM <>@MANHOM)
			BEGIN
				SELECT @ErrCode = 1, @ErrMsg = N'Nhóm này đã tồn tại'
				RETURN
			END
		
		UPDATE NHOMNGUOIDUNG
		SET TEMNHOM= @TEMNHOM , GHICHU=@GHICHU
		WHERE MANHOM = @MANHOM
	END
	GO


-----[dbo].[sp_Update_PHANQUYEN]
/****** Object:  StoredProcedure [dbo].[sp_Update_PHANQUYEN]     ******/
	SET ANSI_NULLS ON
	GO
	SET QUOTED_IDENTIFIER ON
	GO

	CREATE PROCEDURE [dbo].[sp_Update_PHANQUYEN]
		@MANHOM INT,
		@MACN INT,
		@FLAT BIT =0 -- 0 XÓA,1 THÊM
	AS
	BEGIN
		------XÓA 
		IF(@FLAT =0)
		BEGIN
			IF NOT EXISTS (SELECT * FROM PHANQUYEN WHERE MANHOM=@MANHOM AND MACN=@MACN)
			BEGIN
				----CHƯA CHO PHÉP
				RETURN
			END	
			DELETE PHANQUYEN WHERE MACN = @MACN AND MANHOM=@MANHOM
			RETURN
		END	
		---THÊM
		IF EXISTS (SELECT * FROM PHANQUYEN WHERE MANHOM=@MANHOM AND MACN=@MACN)
			BEGIN
				----ĐÃ CHO PHÉP RỒI
				RETURN
			END	
		INSERT INTO PHANQUYEN VALUES(@MACN,@MANHOM)	
	END
	GO
------------------------------------------------------------------
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

	CREATE PROCEDURE [dbo].[sp_NHANVIEN_TK_Getlist]
	AS
	BEGIN
			select MANV,TENNV from NHANVIEN n where n.MANV not in (select t.MANV from TAIKHOANDN t ) and TRANGTHAINV =1
	END
GO

exec sp_NHANVIEN_TK_Getlist