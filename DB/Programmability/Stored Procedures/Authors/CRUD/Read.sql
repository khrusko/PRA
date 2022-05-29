CREATE PROCEDURE [dbo].[AuthorRead] (@ID AS int = NULL)
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
      [FName], 
      [LName], 
      [BirthDate], 
      [ImagePath], 
      [Biography]
    FROM [dbo].[Authors]
    WHERE [DeleteDate] IS NULL
    ORDER BY [FName] ASC, [LName] ASC
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
      [FName], 
      [LName], 
      [BirthDate], 
      [ImagePath], 
      [Biography]
    FROM [dbo].[Authors]
    WHERE [ID] = @ID AND [DeleteDate] IS NULL
  END
END
GO
