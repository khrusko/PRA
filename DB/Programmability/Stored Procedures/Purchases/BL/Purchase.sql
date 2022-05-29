CREATE PROCEDURE [dbo].[PurchasePurchase] (@BookFK AS int,
                                           @UserFK AS int,
                                           @Quantity AS int,
                                           @CreatedBy AS int)
AS BEGIN
  DECLARE @UnitPrice AS decimal(6, 2)
  DECLARE @AvailableQuantity AS int
  SELECT ALL TOP 1
    @UnitPrice = [PurchasePrice],
    @AvailableQuantity = [Quantity]
  FROM [dbo].[Books]
  WHERE [ID] = @BookFK AND
        [DeleteDate] IS NULL

  IF @AvailableQuantity IS NULL OR @Quantity > @AvailableQuantity BEGIN
    RETURN 0
  END

  INSERT INTO [dbo].[Purchases] 
  (
    [CreatedBy],
    [UpdatedBy],
    [BookFK],
    [UserFK],
    [Quantity],
    [UnitPrice]
  )
  VALUES 
  (
    @CreatedBy,
    @CreatedBy,
    @BookFK,
    @UserFK,
    @Quantity,
    @UnitPrice
  )

  DECLARE @ID AS int = SCOPE_IDENTITY()

  UPDATE [dbo].[Books]
  SET
    [Quantity] = [Quantity] - @Quantity
  WHERE [ID] = @BookFK

  RETURN @ID
END
GO
