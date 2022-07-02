CREATE PROCEDURE [dbo].[PurchaseUpdate] (@ID AS int,
                                         @BookFK AS int,
                                         @UserFK AS int,
                                         @Quantity AS int,
                                         @UnitPrice AS decimal(6, 2),
                                         @PurchaseDate AS datetime,
                                         @UpdatedBy AS int)
AS BEGIN
  UPDATE [dbo].[Purchases]
  SET
    [UpdatedBy]     = @UpdatedBy,
    [UpdateDate]    = GETDATE(),
    [BookFK]        = @BookFK,
    [UserFK]        = @UserFK,
    [Quantity]      = @Quantity,
    [UnitPrice]     = @UnitPrice,
    [PurchaseDate]  = @PurchaseDate
  WHERE [ID] = @ID AND [DeleteDate] IS NULL

  RETURN @@ROWCOUNT
END
GO
