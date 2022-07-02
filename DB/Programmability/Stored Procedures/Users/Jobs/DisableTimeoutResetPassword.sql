CREATE PROCEDURE [dbo].[UserDisableTimeoutResetPassword]
AS BEGIN
  DECLARE @UpdatedBy AS int = (
    SELECT ALL TOP 1
      [ID]
    FROM [dbo].[Users]
    WHERE [IsAdmin] = 1
    ORDER BY [ID] ASC
  )

  UPDATE [dbo].[Users]
  SET
    [UpdatedBy]               = @UpdatedBy,
    [UpdateDate]              = GETDATE(),
    [ResetPasswordIsApproved] = 0
  WHERE [DeleteDate] IS NULL AND
        [ResetPasswordIsApproved] = 1 AND
        DATEDIFF(MINUTE, [ResetPasswordDate], GETDATE()) > 5
END
GO
