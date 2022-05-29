CREATE PROCEDURE [dbo].[MessageRead] (@ID AS int = NULL)
AS BEGIN
  IF @ID IS NULL BEGIN
    SELECT ALL
      [ID],
      [CreateDate],
      [CreatedBy],
      [UpdateDate],
      [UpdatedBy],
      [DeleteDate],
      [DeletedBy],
      [SenderUserFK],
      [SenderDate],
      [SenderMessage],
      [ResponderUserFK],
      [ResponderDate],
      [ResponderMessage]
    FROM [dbo].[Messages]
    WHERE [DeleteDate] IS NULL
    ORDER BY [SenderDate] DESC
  END
  ELSE BEGIN
    SELECT ALL
      [ID],
      [CreateDate],
      [CreatedBy],
      [UpdateDate],
      [UpdatedBy],
      [DeleteDate],
      [DeletedBy],
      [SenderUserFK],
      [SenderDate],
      [SenderMessage],
      [ResponderUserFK],
      [ResponderDate],
      [ResponderMessage]
    FROM [dbo].[Messages]
    WHERE [ID] = @ID AND [DeleteDate] IS NULL
  END
END
GO
