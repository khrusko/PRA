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
    [Address],
    [Telephone],
    [Mobile],
    [Email]
  FROM [dbo].[BookStores]
END
GO
