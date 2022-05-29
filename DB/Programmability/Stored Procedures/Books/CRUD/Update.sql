CREATE PROCEDURE [dbo].[BookUpdate] (@ID AS int,
                                     @ISBN AS nvarchar(20), 
                                     @Title AS nvarchar(100), 
                                     @Summary AS nvarchar(MAX), 
                                     @Description AS nvarchar(MAX), 
                                     @IsNew AS bit, 
                                     @PublisherFK AS int, 
                                     @PageCount AS int,
                                     @PurchasePrice AS decimal(6, 2), 
                                     @LoanPrice AS decimal(5, 2), 
                                     @Quantity AS int, 
                                     @ImagePath AS nvarchar(500),
                                     @Authors AS [dbo].[Ids] READONLY,
                                     @UpdatedBy AS int)
AS BEGIN
  DECLARE @IsUnique AS int = (
    SELECT ALL
      COUNT(*)
    FROM [dbo].[Books]
    WHERE [ID] != @ID AND
          [ISBN] = @ISBN AND 
          [DeleteDate] IS NULL
  )
  IF @IsUnique > 0 BEGIN
    RETURN 0
  END

  UPDATE [dbo].[Books]
  SET
    [UpdatedBy]     = @UpdatedBy, 
    [UpdateDate]    = GETDATE(),
    [ISBN]          = @ISBN, 
    [Title]         = @Title, 
    [Summary]       = @Summary, 
    [Description]   = @Description, 
    [IsNew]         = @IsNew, 
    [PublisherFK]   = @PublisherFK, 
    [PageCount]     = @PageCount,
    [PurchasePrice] = @PurchasePrice,
    [LoanPrice]     = @LoanPrice, 
    [Quantity]      = @Quantity, 
    [ImagePath]     = @ImagePath
  WHERE [ID] = @ID AND [DeleteDate] IS NULL

  DECLARE @RowCount AS int = @@ROWCOUNT
  IF @RowCount = 0 BEGIN
    RETURN 0
  END

  DELETE FROM [dbo].[BooksAuthors]
  WHERE [BookFK] = @ID

  INSERT INTO [dbo].[BooksAuthors]
  (
    [BookFK],
    [AuthorFK]
  )
  SELECT ALL
    @ID,
    [ID]
  FROM @Authors
  
  RETURN @RowCount
END
GO