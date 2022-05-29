CREATE PROCEDURE [dbo].[PurchaseRead] (@ID AS int = NULL)
AS BEGIN
  IF @ID IS NULL BEGIN
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
  ELSE BEGIN
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
