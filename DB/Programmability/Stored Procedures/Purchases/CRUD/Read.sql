CREATE PROCEDURE [dbo].[PurchaseRead] (@Method AS int,
                                       @ID AS int = NULL)
AS BEGIN
  IF @Method = 0 BEGIN
    SELECT ALL
      [ID],
      [CreateDate],
      [CreatedBy],
      [UpdateDate],
      [UpdatedBy],
      [DeleteDate],
      [DeletedBy],
      [BookFK],
      [UserFK],
      [Quantity],
      [UnitPrice],
      [PurchaseDate]
    FROM [dbo].[Purchases]
    ORDER BY [PurchaseDate] DESC
  END
  ELSE IF @Method = 1 BEGIN
    SELECT ALL
      [ID],
      [CreateDate],
      [CreatedBy],
      [UpdateDate],
      [UpdatedBy],
      [DeleteDate],
      [DeletedBy],
      [BookFK],
      [UserFK],
      [Quantity],
      [UnitPrice],
      [PurchaseDate]
    FROM [dbo].[Purchases]
    WHERE [DeleteDate] IS NULL
    ORDER BY [PurchaseDate] DESC
  END
  ELSE IF @Method = 2 BEGIN
    SELECT ALL
      [ID],
      [CreateDate],
      [CreatedBy],
      [UpdateDate],
      [UpdatedBy],
      [DeleteDate],
      [DeletedBy],
      [BookFK],
      [UserFK],
      [Quantity],
      [UnitPrice],
      [PurchaseDate]
    FROM [dbo].[Purchases]
    WHERE [ID] = @ID
  END
  ELSE IF @Method = 3 BEGIN
    SELECT ALL
      [ID],
      [CreateDate],
      [CreatedBy],
      [UpdateDate],
      [UpdatedBy],
      [DeleteDate],
      [DeletedBy],
      [BookFK],
      [UserFK],
      [Quantity],
      [UnitPrice],
      [PurchaseDate]
    FROM [dbo].[Purchases]
    WHERE [ID] = @ID AND [DeleteDate] IS NULL
  END
END
GO
