CREATE PROCEDURE [dbo].[MessageCreate] (@SenderUserFK AS int,
                                        @SenderDate AS datetime,
                                        @SenderMessage AS nvarchar(MAX),
                                        @ResponderUserFK AS int,
                                        @ResponderDate AS datetime,
                                        @ResponderMessage AS nvarchar(MAX),
                                        @CreatedBy AS int)
AS BEGIN
  INSERT INTO [dbo].[Messages] 
  (
    [CreatedBy],
    [UpdatedBy],
    [SenderUserFK],
    [SenderDate],
    [SenderMessage],
    [ResponderUserFK],
    [ResponderDate],
    [ResponderMessage]
  )
  VALUES 
  (
    @CreatedBy,
    @CreatedBy,
    @SenderUserFK,
    @SenderDate,
    @SenderMessage,
    @ResponderUserFK,
    @ResponderDate,
    @ResponderMessage
  )

  RETURN SCOPE_IDENTITY()
END
GO
