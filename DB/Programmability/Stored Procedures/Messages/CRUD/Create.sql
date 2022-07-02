CREATE PROCEDURE [dbo].[MessageCreate] (@SenderFName AS nvarchar(50),
                                        @SenderLName AS nvarchar(50),
                                        @SenderEmail AS nvarchar(100),
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
    [SenderFName],
    [SenderLName],
    [SenderEmail],
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
    @SenderFName,
    @SenderLName,
    @SenderEmail,
    @SenderDate,
    @SenderMessage,
    @ResponderUserFK,
    @ResponderDate,
    @ResponderMessage
  )

  RETURN SCOPE_IDENTITY()
END
GO
