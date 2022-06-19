CREATE PROCEDURE [dbo].[SubscriptionReadAllUnresolved]
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
    [SubscriptionDate],
    [IsResolved],
    [ResolvedDate]
  FROM [dbo].[Subscriptions]
  WHERE [DeleteDate] IS NULL AND
        [IsResolved] = 0
END
GO
