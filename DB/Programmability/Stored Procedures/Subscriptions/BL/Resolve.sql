CREATE PROCEDURE [dbo].[SubscriptionResolve] (@ID AS int)
AS BEGIN
  DECLARE @UpdatedBy AS int = (
    SELECT ALL TOP 1
      [ID]
    FROM [dbo].[Users]
    WHERE [IsAdmin] = 1
    ORDER BY [ID] ASC
  )

  DECLARE @UpdateDate AS datetime = GETDATE()

  UPDATE [dbo].[Subscriptions]
  SET
    [UpdatedBy]     = @UpdatedBy,
    [UpdateDate]    = @UpdateDate,
    [IsResolved]    = 1,
    [ResolvedDate]  = @UpdateDate
  WHERE [DeleteDate] IS NULL AND
        [IsResolved] = 0 AND
        [ID] = @ID

  RETURN @@ROWCOUNT
END
GO