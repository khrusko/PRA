CREATE PROCEDURE [dbo].[PurchaseDelete] (@ID AS int,
                                         @DeletedBy AS int)
AS BEGIN
  UPDATE [dbo].[Purchases]
  SET
    [DeletedBy]   = @DeletedBy,
    [DeleteDate]  = GETDATE()
  WHERE [ID] = @ID AND [DeleteDate] IS NULL

  RETURN @@ROWCOUNT
END
GO
