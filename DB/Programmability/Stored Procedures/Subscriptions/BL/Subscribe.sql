CREATE PROCEDURE [dbo].[SubscriptionSubscribe] (@BookFK AS int,
                                                @UserFK AS int,
                                                @CreatedBy AS int)
AS BEGIN
  DECLARE @IsUnique AS int = (
    SELECT ALL
      COUNT(*)
    FROM [dbo].[Subscriptions]
    WHERE [DeleteDate] IS NULL AND
          [BookFK] = @BookFK AND
          [UserFK] = @UserFK AND
          [IsResolved] = 0
  )
  IF @IsUnique > 0 BEGIN
    RETURN 0
  END

  DECLARE @SubscriptionDate AS datetime = GETDATE()

  INSERT INTO [dbo].[Subscriptions] 
  (
    [CreatedBy],
    [UpdatedBy],
    [BookFK],
    [UserFK],
    [SubscriptionDate],
    [IsResolved],
    [ResolvedDate]
  )
  VALUES 
  (
    @CreatedBy,
    @CreatedBy,
    @BookFK,
    @UserFK,
    @SubscriptionDate
  )

  RETURN SCOPE_IDENTITY()
END
GO
