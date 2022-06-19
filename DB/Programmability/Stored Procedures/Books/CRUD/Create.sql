CREATE PROCEDURE [dbo].[BookCreate] (@ISBN AS nvarchar(20), 
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
                                     @CreatedBy AS int)
AS BEGIN
  DECLARE @IsUnique AS int = (
    SELECT ALL
      COUNT(*)
    FROM [dbo].[Books]
    WHERE [ISBN] = @ISBN AND 
          [DeleteDate] IS NULL
  )
  IF @IsUnique > 0 BEGIN
    RETURN 0
  END

  INSERT INTO [dbo].[Books]
  (
    [CreatedBy], 
    [UpdatedBy], 
    [ISBN], 
    [Title], 
    [Summary], 
    [Description], 
    [IsNew], 
    [PublisherFK],
    [AuthorFK],
    [PageCount],
    [PurchasePrice], 
    [LoanPrice], 
    [Quantity], 
    [ImagePath]
  )
  VALUES 
  (
    @CreatedBy,
    @CreatedBy,
    @ISBN,
    @Title,
    @Summary,
    @Description,
    @IsNew,
    @PublisherFK,
    @AuthorFK,
    @PageCount,
    @PurchasePrice,
    @LoanPrice,
    @Quantity,
    @ImagePath
  )

  RETURN SCOPE_IDENTITY()
END
GO