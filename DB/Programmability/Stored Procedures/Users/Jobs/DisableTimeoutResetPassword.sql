CREATE PROCEDURE [dbo].[UserDisableTimeoutResetPassword]
AS BEGIN
  UPDATE [dbo].[Users]
  SET
    [ResetPasswordIsApproved] = 0
  WHERE [ResetPasswordIsApproved] = 1 AND
        DATEDIFF(MINUTE, [ResetPasswordDate], GETDATE()) > 5
END
GO
