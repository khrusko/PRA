CREATE PROCEDURE [dbo].[MessageUpdate] (@ID AS int,
                                        @SenderUserFK AS int,
                                        @SenderDate AS datetime,
                                        @SenderMessage AS nvarchar(MAX),
                                        @ResponderUserFK AS int,
                                        @ResponderDate AS datetime,
                                        @ResponderMessage AS nvarchar(MAX),
                                        @UpdatedBy AS int)
AS BEGIN
  UPDATE [dbo].[Messages]
  SET
    [UpdatedBy]         = @UpdatedBy,
    [UpdateDate]        = GETDATE(),
    [SenderUserFK]      = @SenderUserFK,
    [SenderDate]        = @SenderDate,
    [SenderMessage]     = @SenderMessage,
    [ResponderUserFK]   = @ResponderUserFK,
    [ResponderDate]     = @ResponderDate,
    [ResponderMessage]  = @ResponderMessage
  WHERE [ID] = @ID AND [DeleteDate] IS NULL

  RETURN @@ROWCOUNT
END
GO
