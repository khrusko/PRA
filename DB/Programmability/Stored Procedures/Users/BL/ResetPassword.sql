CREATE PROCEDURE [dbo].[UserResetPassword] (@GUID AS uniqueidentifier,
                                            @Password AS nvarchar(512))
AS BEGIN
  DECLARE @PasswordHash AS nvarchar(512) = CONVERT(nvarchar(512), HASHBYTES('SHA2_512', @Password), 2)

  DECLARE @UpdatedBy AS int = (
    SELECT ALL TOP 1
      [ID]
    FROM [dbo].[Users]
    WHERE [IsAdmin] = 1
    ORDER BY [ID] ASC
  )

  DECLARE @UpdateDate AS datetime = GETDATE()

  UPDATE [dbo].[Users]
  SET
    [UpdatedBy]               = @UpdatedBy,
    [UpdateDate]              = @UpdateDate,
    [PasswordHash]            = @PasswordHash,
    [ResetPasswordIsApproved] = 0
  WHERE [DeleteDate] IS NULL AND
        [GUID] = @GUID

  RETURN @@ROWCOUNT
END
GO
