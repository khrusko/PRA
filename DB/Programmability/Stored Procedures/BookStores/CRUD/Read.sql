CREATE PROCEDURE [dbo].[BookStoreRead]
AS BEGIN
  SELECT ALL TOP 1
    [ID],
    [CreateDate],
    [CreatedBy],
    [UpdateDate],
    [UpdatedBy],
    [DeleteDate],
    [DeletedBy],
    [Name],
    [OIB],
    [DelayPricePerDay],
    [Email]
  FROM [dbo].[BookStores]
END
GO
