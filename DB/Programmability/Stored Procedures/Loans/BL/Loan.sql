CREATE PROCEDURE [dbo].[LoanLoan] (@BookFK AS int,
                                   @UserFK AS int,
                                   @PlannedReturnDate AS smalldatetime,
                                   @CreatedBy AS int)
AS BEGIN
  DECLARE @LoanPrice AS decimal(5, 2)
  DECLARE @Quantity AS int
  
  SELECT ALL TOP 1 
    @LoanPrice = [LoanPrice],
    @Quantity = [Quantity]
  FROM [dbo].[Books] 
  WHERE [DeleteDate] IS NULL AND
        [ID] = @BookFK

  IF @Quantity IS NULL OR @Quantity = 0 BEGIN
    RETURN 0
  END

  DECLARE @DelayPricePerDay AS decimal(5, 2) = (SELECT ALL TOP 1 [DelayPricePerDay] FROM [dbo].[BookStores])

  INSERT INTO [dbo].[Loans] 
  (
    [CreatedBy],
    [UpdatedBy],
    [BookFK],
    [UserFK],
    [LoanPrice],
    [PlannedReturnDate],
    [DelayPricePerDay]
  )
  VALUES 
  (
    @CreatedBy,
    @CreatedBy,
    @BookFK,
    @UserFK,
    @LoanPrice,
    @PlannedReturnDate,
    @DelayPricePerDay
  )

  DECLARE @ID AS int = SCOPE_IDENTITY()

  UPDATE [dbo].[Books]
  SET
    [Quantity] = [Quantity] - 1
  WHERE [ID] = @BookFK

  RETURN @ID
END
GO
