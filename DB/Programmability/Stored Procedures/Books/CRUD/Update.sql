CREATE PROCEDURE [dbo].[BookUpdate] (@ID AS int,
                                     @ISBN AS nvarchar(20), 
                                     @Title AS nvarchar(100), 
                                     @Summary AS nvarchar(MAX), 
                                     @Description AS nvarchar(MAX), 
                                     @IsNew AS bit, 
                                     @PublisherFK AS int, 
                                     @AuthorFK AS int,
                                     @PageCount AS int,
                                     @PurchasePrice AS decimal(6, 2), 
                                     @LoanPrice AS decimal(6, 2), 
                                     @Quantity AS int, 
                                     @ImagePath AS nvarchar(500),
                                     @UpdatedBy AS int)
AS BEGIN
  DECLARE @IsUnique AS int = (
    SELECT ALL
      COUNT(*)
    FROM [dbo].[Books]
    WHERE [ID] <> @ID AND
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
    [AuthorFK]      = @AuthorFK,
    [PageCount]     = @PageCount,
    [PurchasePrice] = @PurchasePrice,
    [LoanPrice]     = @LoanPrice, 
    [Quantity]      = @Quantity, 
    [ImagePath]     = @ImagePath
  WHERE [DeleteDate] IS NULL AND
        [ID] = @ID

  RETURN @@ROWCOUNT
END
GO