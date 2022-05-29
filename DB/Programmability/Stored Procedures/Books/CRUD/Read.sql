CREATE PROCEDURE [dbo].[BookRead] (@ID AS int = NULL)
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
    WHERE [DeleteDate] IS NULL
    ORDER BY [Title] ASC
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
    WHERE [ID] = @ID AND [DeleteDate] IS NULL
  END
END
GO
