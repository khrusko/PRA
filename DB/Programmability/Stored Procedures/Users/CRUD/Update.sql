CREATE PROCEDURE [dbo].[UserUpdate] (@ID AS int,
                                     @UserID AS char(11),
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
                                     @UpdatedBy AS int)
AS BEGIN
  DECLARE @IsUnique AS int = (
    SELECT ALL
      COUNT(*)
    FROM [dbo].[Users]
    WHERE [DeleteDate] IS NULL AND
          [ID] <> @ID AND
          (
            [UserID] = @UserID OR
            [Email] = @Email
          )
  )
  IF @IsUnique > 1 BEGIN
    RETURN 0
  END

  UPDATE [dbo].[Users]
  SET
    [UpdatedBy]               = @UpdatedBy,
    [UpdateDate]              = GETDATE(),
    [UserID]                  = @UserID,
    [FName]                   = @FName,
    [LName]                   = @LName,
    [Email]                   = @Email,
    [PasswordHash]            = @PasswordHash,
    [ImagePath]               = @ImagePath,
    [Address]                 = @Address,
    [IsAdmin]                 = @IsAdmin,
    [GUID]                    = @GUID,
    [RegistrationIsApproved]  = @RegistrationIsApproved,
    [RegistrationDate]        = @RegistrationDate,
    [ResetPasswordIsApproved] = @ResetPasswordIsApproved,
    [ResetPasswordDate]       = @ResetPasswordDate
  WHERE [ID] = @ID AND [DeleteDate] IS NULL

  RETURN @@ROWCOUNT
END
GO
