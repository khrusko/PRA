CREATE PROCEDURE [dbo].[UserDeleteTimeoutRegistration]
AS BEGIN
  DELETE FROM [dbo].[Users]
  WHERE [ConfirmationIsApproved] = 0 AND
        DATEDIFF(MINUTE, [CreateDate], GETDATE()) > 15
END
GO
