CREATE PROCEDURE [dbo].[BookDelete] (@ID AS int,
                                     @DeletedBy AS int)
AS BEGIN
  UPDATE [dbo].[Books]
  SET
    [DeletedBy]   = @DeletedBy,
    [DeleteDate]  = GETDATE()
  WHERE [ID] = @ID AND [DeleteDate] IS NULL

  RETURN @@ROWCOUNT
END
GO
