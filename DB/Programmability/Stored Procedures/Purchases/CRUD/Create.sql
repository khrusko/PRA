CREATE PROCEDURE [dbo].[PurchaseCreate] (@BookFK AS int,
                                         @UserFK AS int,
                                         @Quantity AS int,
                                         @UnitPrice AS decimal(6, 2),
                                         @PurchaseDate AS datetime,
                                         @CreatedBy AS int)
AS BEGIN
  INSERT INTO [dbo].[Purchases] 
  (
    [CreatedBy],
    [UpdatedBy],
    [BookFK],
    [UserFK],
    [Quantity],
    [UnitPrice],
    [PurchaseDate]
  )
  VALUES 
  (
    @CreatedBy,
    @CreatedBy,
    @BookFK,
    @UserFK,
    @Quantity,
    @UnitPrice,
    @PurchaseDate
  )

  RETURN SCOPE_IDENTITY()
END
GO
