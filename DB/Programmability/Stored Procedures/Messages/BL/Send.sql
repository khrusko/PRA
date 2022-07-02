CREATE PROCEDURE [dbo].[MessageSend] (@SenderFName AS nvarchar(50),
                                      @SenderLName AS nvarchar(50),
                                      @SenderEmail AS nvarchar(100),
                                      @SenderMessage AS nvarchar(MAX))
AS BEGIN
  DECLARE @CreatedBy AS int = (
    SELECT ALL TOP 1
      [ID]
    FROM [dbo].[Users]
    WHERE [IsAdmin] = 1
    ORDER BY [ID] ASC
  )
  DECLARE @SenderDate AS datetime = GETDATE()

  INSERT INTO [dbo].[Messages] 
  (
    [CreatedBy],
    [UpdatedBy],
    [SenderFName],
    [SenderLName],
    [SenderEmail],
    [SenderDate],
    [SenderMessage]
  )
  VALUES 
  (
    @CreatedBy,
    @CreatedBy,
    @SenderFName,
    @SenderLName,
    @SenderEmail,
    @SenderDate,
    @SenderMessage
  )

  RETURN SCOPE_IDENTITY()
END
GO
