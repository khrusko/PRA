CREATE PROCEDURE [dbo].[SubscriptionDelete] (@ID AS int,
                                             @DeletedBy AS int)
AS BEGIN
  UPDATE [dbo].[Subscriptions]
  SET
    [DeletedBy]   = @DeletedBy,
    [DeleteDate]  = GETDATE()
  WHERE [ID] = @ID AND [DeleteDate] IS NULL

  RETURN @@ROWCOUNT
END
GO
