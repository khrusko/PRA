CREATE PROCEDURE [dbo].[MessageDelete] (@ID AS int,
                                        @DeletedBy AS int)
AS BEGIN
  UPDATE [dbo].[Messages]
  SET
    [DeletedBy]   = @DeletedBy,
    [DeleteDate]  = GETDATE()
  WHERE [ID] = @ID AND [DeleteDate] IS NULL

  RETURN @@ROWCOUNT
END
GO
