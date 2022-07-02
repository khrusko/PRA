CREATE PROCEDURE [dbo].[UserDeleteTimeoutRegistration]
AS BEGIN
  DECLARE @DeletedBy AS int = (
    SELECT ALL TOP 1
      [ID]
    FROM [dbo].[Users]
    WHERE [IsAdmin] = 1
    ORDER BY [ID] ASC
  )

  UPDATE [dbo].[Users]
  SET
    [DeletedBy]  = @DeletedBy,
    [DeleteDate] = GETDATE()
  WHERE [DeleteDate] IS NULL AND
        [RegistrationIsApproved] = 0 AND
        DATEDIFF(MINUTE, [CreateDate], GETDATE()) > 15
END
GO
