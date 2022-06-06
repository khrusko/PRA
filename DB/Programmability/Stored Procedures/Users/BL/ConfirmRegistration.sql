CREATE PROCEDURE [dbo].[UserConfirmRegistration] (@GUID AS uniqueidentifier)
AS BEGIN
  DECLARE @UpdatedBy AS int = (
    SELECT ALL TOP 1
      [ID]
    FROM [dbo].[Users]
    WHERE [IsAdmin] = 1
    ORDER BY [ID] ASC
  )

  DECLARE @RegistrationDate AS datetime = GETDATE()

  UPDATE [dbo].[Users]
  SET
    [UpdatedBy]               = @UpdatedBy,
    [UpdateDate]              = @RegistrationDate,
    [RegistrationIsApproved]  = 1,
    [RegistrationDate]        = @RegistrationDate
  WHERE [GUID] = @GUID AND 
        [DeleteDate] IS NULL

  RETURN @@ROWCOUNT
END
GO
