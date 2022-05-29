CREATE PROCEDURE [dbo].[SubscriptionCreate] (@BookFK AS int,
                                             @UserFK AS int,
                                             @SubscriptionDate AS datetime,
                                             @IsResolved AS bit,
                                             @ResolvedDate AS datetime,
                                             @CreatedBy AS int)
AS BEGIN
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
    @SubscriptionDate,
    @IsResolved,
    @ResolvedDate
  )

  RETURN SCOPE_IDENTITY()
END
GO
