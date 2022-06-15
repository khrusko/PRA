CREATE PROCEDURE [dbo].[SubscriptionRead] (@Method AS int,
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
      [SubscriptionDate],
      [IsResolved],
      [ResolvedDate]
    FROM [dbo].[Subscriptions]
    ORDER BY [SubscriptionDate] DESC
  END
  IF @Method = 1 BEGIN
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
      [SubscriptionDate],
      [IsResolved],
      [ResolvedDate]
    FROM [dbo].[Subscriptions]
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
      [SubscriptionDate],
      [IsResolved],
      [ResolvedDate]
    FROM [dbo].[Subscriptions]
    WHERE [ID] = @ID AND [DeleteDate] IS NULL
  END
END
GO
