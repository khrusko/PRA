CREATE PROCEDURE [dbo].[UserDeleteTimeoutRegistration]
AS BEGIN
  UPDATE [dbo].[Users]
  SET
    [DeleteDate] = GETDATE()
  WHERE [ConfirmationIsApproved] = 0 AND
        DATEDIFF(MINUTE, [CreateDate], GETDATE()) > 15
END
GO
