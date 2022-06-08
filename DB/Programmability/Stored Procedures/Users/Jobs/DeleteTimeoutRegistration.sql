CREATE PROCEDURE [dbo].[UserDeleteTimeoutRegistration]
AS BEGIN
  UPDATE [dbo].[Users]
  SET
    [DeleteDate] = GETDATE()
  WHERE [DeleteDate] IS NULL AND
        [RegistrationIsApproved] = 0 AND
        DATEDIFF(MINUTE, [CreateDate], GETDATE()) > 15
END
GO
