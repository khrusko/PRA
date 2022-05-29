CREATE PROCEDURE [dbo].[SubscriptionRead] (@ID AS int = NULL)
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
      [SubscriptionDate],
      [IsResolved],
      [ResolvedDate]
    FROM [dbo].[Subscriptions]
    WHERE [DeleteDate] IS NULL
    ORDER BY [SubscriptionDate] DESC
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
      [SubscriptionDate],
      [IsResolved],
      [ResolvedDate]
    FROM [dbo].[Subscriptions]
    WHERE [ID] = @ID AND [DeleteDate] IS NULL
  END
END
GO
