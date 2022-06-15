CREATE PROCEDURE [dbo].[BookRead] (@Method AS int,
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
    ORDER BY [Title] ASC
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
  ELSE IF @Method = 2 BEGIN
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
