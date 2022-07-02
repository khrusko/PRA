CREATE PROCEDURE [dbo].[UserCreate] (@UserID AS char(11),
                                     @FName AS nvarchar(50),
                                     @LName AS nvarchar(50),
                                     @Email AS nvarchar(100),
                                     @PasswordHash AS nvarchar(512),
                                     @ImagePath AS nvarchar(500),
                                     @Address AS nvarchar(200),
                                     @IsAdmin AS bit,
                                     @GUID AS uniqueidentifier,
                                     @RegistrationIsApproved AS bit,
                                     @RegistrationDate AS datetime,
                                     @ResetPasswordIsApproved AS bit,
                                     @ResetPasswordDate AS datetime,
                                     @CreatedBy AS int)
AS BEGIN
  DECLARE @IsUnique AS int = (
    SELECT ALL
      COUNT(*)
    FROM [dbo].[Users]
    WHERE [DeleteDate] IS NULL AND
          (
            [UserID] = @UserID OR
            [Email] = @Email
          )
  )
  IF @IsUnique > 1 BEGIN
    RETURN 0
  END

  INSERT INTO [dbo].[Users] 
  (
    [CreatedBy], 
    [UpdatedBy],
    [UserID], 
    [FName], 
    [LName], 
    [Email], 
    [PasswordHash], 
    [ImagePath],
    [Address],
    [IsAdmin],
    [GUID],
    [RegistrationIsApproved],
    [RegistrationDate],
    [ResetPasswordIsApproved],
    [ResetPasswordDate]
  )
  VALUES 
  (
    @CreatedBy,
    @CreatedBy,
    @UserID,
    @FName,
    @LName,
    @Email,
    @PasswordHash,
    @ImagePath,
    @Address,
    @IsAdmin,
    @GUID,
    @RegistrationIsApproved,
    @RegistrationDate,
    @ResetPasswordIsApproved,
    @ResetPasswordDate
  )

  RETURN SCOPE_IDENTITY()
END
GO
