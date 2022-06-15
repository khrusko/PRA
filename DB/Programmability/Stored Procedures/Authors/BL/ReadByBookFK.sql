CREATE PROCEDURE [dbo].[AuthorReadByBookFK] (@BookFK AS int)
AS BEGIN
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
    INNER JOIN [dbo].[BooksAuthors]
      ON [AuthorFK] = [ID]
  WHERE [BookFK] = @BookFK
END
GO
