CREATE PROCEDURE [dbo].[MessageRead] (@Method AS int,
                                      @ID AS int = NULL)
AS BEGIN
  IF @Method = 0 BEGIN
    SELECT ALL
      [ID],
      [CreateDate],
      [CreatedBy],
      [UpdateDate],
      [UpdatedBy],
      [DeleteDate],
      [DeletedBy],
      [SenderFName],
      [SenderLName],
      [SenderEmail],
      [SenderDate],
      [SenderMessage],
      [ResponderUserFK],
      [ResponderDate],
      [ResponderMessage]
    FROM [dbo].[Messages]
    ORDER BY [SenderDate] DESC
  END
  ELSE IF @Method = 1 BEGIN
    SELECT ALL
      [ID],
      [CreateDate],
      [CreatedBy],
      [UpdateDate],
      [UpdatedBy],
      [DeleteDate],
      [DeletedBy],
      [SenderFName],
      [SenderLName],
      [SenderEmail],
      [SenderDate],
      [SenderMessage],
      [ResponderUserFK],
      [ResponderDate],
      [ResponderMessage]
    FROM [dbo].[Messages]
    WHERE [DeleteDate] IS NULL
    ORDER BY [SenderDate] DESC
  END
  ELSE IF @Method = 2 BEGIN
    SELECT ALL
      [ID],
      [CreateDate],
      [CreatedBy],
      [UpdateDate],
      [UpdatedBy],
      [DeleteDate],
      [DeletedBy],
      [SenderFName],
      [SenderLName],
      [SenderEmail],
      [SenderDate],
      [SenderMessage],
      [ResponderUserFK],
      [ResponderDate],
      [ResponderMessage]
    FROM [dbo].[Messages]
    WHERE [ID] = @ID
  END
  ELSE IF @Method = 3 BEGIN
    SELECT ALL
      [ID],
      [CreateDate],
      [CreatedBy],
      [UpdateDate],
      [UpdatedBy],
      [DeleteDate],
      [DeletedBy],
      [SenderFName],
      [SenderLName],
      [SenderEmail],
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
