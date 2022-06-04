CREATE PROCEDURE [dbo].[UserConfirmRegistration] (@ConfirmationGUID AS uniqueidentifier)
AS BEGIN
  DECLARE @UpdatedBy AS int = (
    SELECT ALL TOP 1
      [ID]
    FROM [dbo].[Users]
    WHERE [IsAdmin] = 1
    ORDER BY [ID] ASC
  )

  DECLARE @ConfirmationDate AS datetime = GETDATE()

  UPDATE [dbo].[Users]
  SET
    [UpdatedBy]               = @UpdatedBy,
    [UpdateDate]              = @ConfirmationDate,
    [ConfirmationIsApproved]  = 1,
    [ConfirmationDate]        = @ConfirmationDate
  WHERE [ConfirmationGUID] = @ConfirmationGUID AND [DeleteDate] IS NULL

  RETURN @@ROWCOUNT
END
GO
