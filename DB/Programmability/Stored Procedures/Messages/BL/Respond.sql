CREATE PROCEDURE [dbo].[MessageRespond] (@ID AS int,
                                         @ResponderUserFK AS int,
                                         @ResponderMessage AS nvarchar(MAX),
                                         @UpdatedBy AS int)
AS BEGIN
  DECLARE @ResponderDate AS datetime = GETDATE()

  UPDATE [dbo].[Messages]
  SET
    [UpdatedBy]         = @UpdatedBy,
    [UpdateDate]        = @ResponderDate,
    [ResponderUserFK]   = @ResponderUserFK,
    [ResponderDate]     = @ResponderDate,
    [ResponderMessage]  = @ResponderMessage
  WHERE [ID] = @ID AND 
        [DeleteDate] IS NULL AND
        [ResponderDate] IS NULL

  RETURN @@ROWCOUNT
END
GO
