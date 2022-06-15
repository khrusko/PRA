CREATE PROCEDURE [dbo].[BookReadByAuthorFK] (@AuthorFK AS int)
AS BEGIN
  SELECT ALL
    [ID],
    [CreateDate],
    [CreatedBy],
    [UpdateDate],
    [UpdatedBy],
    [DeleteDate],
    [DeletedBy],
    [ISBN], 
    [Title], 
    [Summary], 
    [Description], 
    [IsNew], 
    [PublisherFK], 
    [PageCount],
    [PurchasePrice], 
    [LoanPrice], 
    [Quantity], 
    [ImagePath]
  FROM [dbo].[Books]
    INNER JOIN [dbo].[BooksAuthors]
      ON [BookFK] = [ID]
  WHERE [AuthorFK] = @AuthorFK
END
GO
