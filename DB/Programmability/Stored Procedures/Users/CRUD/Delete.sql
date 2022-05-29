CREATE PROCEDURE [dbo].[UserDelete] (@ID AS int,
                                     @DeletedBy AS int)
AS BEGIN
  UPDATE [dbo].[Users]
  SET
    [DeletedBy]   = @DeletedBy,
    [DeleteDate]  = GETDATE()
  WHERE [ID] = @ID AND [DeleteDate] IS NULL

  RETURN @@ROWCOUNT
END
GO
