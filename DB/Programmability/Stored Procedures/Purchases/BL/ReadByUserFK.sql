CREATE PROCEDURE [dbo].[PurchaseReadByUserFK] (@UserFK AS int)
AS BEGIN
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
  WHERE [DeleteDate] IS NULL AND
        [UserFK] = @UserFK
END
