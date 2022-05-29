CREATE PROCEDURE [dbo].[SubscriptionUpdate] (@ID AS int,
                                             @BookFK AS int,
                                             @UserFK AS int,
                                             @SubscriptionDate AS datetime,
                                             @IsResolved AS bit,
                                             @ResolvedDate AS datetime,
                                             @UpdatedBy AS int)
AS BEGIN
  UPDATE [dbo].[Subscriptions]
  SET
    [UpdatedBy]         = @UpdatedBy,
    [UpdateDate]        = GETDATE(),
    [BookFK]            = @BookFK,
    [UserFK]            = @UserFK,
    [SubscriptionDate]  = @SubscriptionDate,
    [IsResolved]        = @IsResolved,
    [ResolvedDate]      = @ResolvedDate
  WHERE [ID] = @ID AND [DeleteDate] IS NULL

  RETURN @@ROWCOUNT
END
GO
