CREATE PROCEDURE [dbo].[MessageSend] (@SenderUserFK AS int,
                                      @SenderMessage AS nvarchar(MAX),
                                      @CreatedBy AS int)
AS BEGIN
  DECLARE @SenderDate AS datetime = GETDATE()

  INSERT INTO [dbo].[Messages] 
  (
    [CreatedBy],
    [UpdatedBy],
    [SenderUserFK],
    [SenderDate],
    [SenderMessage]
  )
  VALUES 
  (
    @CreatedBy,
    @CreatedBy,
    @SenderUserFK,
    @SenderDate,
    @SenderMessage
  )

  RETURN SCOPE_IDENTITY()
END
GO
