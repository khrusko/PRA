CREATE PROCEDURE [dbo].[AuthorDelete] (@ID AS int,
                                       @DeletedBy AS int)
AS BEGIN
  UPDATE [dbo].[Authors]
  SET
    [DeletedBy]   = @DeletedBy,
    [DeleteDate]  = GETDATE()
  WHERE [ID] = @ID AND [DeleteDate] IS NULL

  RETURN @@ROWCOUNT
END
GO
