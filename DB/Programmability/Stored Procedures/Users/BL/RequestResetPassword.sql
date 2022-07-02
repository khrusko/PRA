CREATE PROCEDURE [dbo].[UserRequestResetPassword] (@Email AS nvarchar(100))
AS BEGIN
  DECLARE @ResetPasswordDate AS datetime = GETDATE()
  DECLARE @UpdatedBy AS int = (
    SELECT ALL TOP 1
      [ID]
    FROM [dbo].[Users]
    WHERE [IsAdmin] = 1
    ORDER BY [ID] ASC
  )

  UPDATE [dbo].[Users]
  SET 
    [UpdateDate]              = @ResetPasswordDate,
    [UpdatedBy]               = @UpdatedBy,
    [ResetPasswordIsApproved] = 1,
    [ResetPasswordDate]       = @ResetPasswordDate
  WHERE [DeleteDate] IS NULL AND
        [Email] = @Email AND
        [ResetPasswordIsApproved] = 0

  RETURN @@ROWCOUNT
END
