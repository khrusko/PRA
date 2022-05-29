CREATE PROCEDURE [dbo].[PublisherDelete] (@ID AS int,
                                          @DeletedBy AS int)
AS BEGIN
  UPDATE [dbo].[Publishers]
  SET
    [DeletedBy]   = @DeletedBy,
    [DeleteDate]  = GETDATE()
  WHERE [ID] = @ID AND [DeleteDate] IS NULL

  RETURN @@ROWCOUNT
END
GO
