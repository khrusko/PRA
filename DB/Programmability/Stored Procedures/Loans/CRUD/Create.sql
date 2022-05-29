CREATE PROCEDURE [dbo].[LoanCreate] (@BookFK AS int,
                                     @UserFK AS int,
                                     @LoanPrice AS decimal(5, 2),
                                     @LoanDate AS datetime,
                                     @PlannedReturnDate AS smalldatetime,
                                     @ReturnDate AS smalldatetime,
                                     @DelayDays AS int,
                                     @DelayPricePerDay AS decimal(5, 2),
                                     @CreatedBy AS int)
AS BEGIN
  INSERT INTO [dbo].[Loans] 
  (
    [CreatedBy],
    [UpdatedBy],
    [BookFK],
    [UserFK],
    [LoanPrice],
    [LoanDate],
    [PlannedReturnDate],
    [ReturnDate],
    [DelayDays],
    [DelayPricePerDay]
  )
  VALUES 
  (
    @CreatedBy,
    @CreatedBy,
    @BookFK,
    @UserFK,
    @LoanPrice,
    @LoanDate,
    @PlannedReturnDate,
    @ReturnDate,
    @DelayDays,
    @DelayPricePerDay
  )

  RETURN SCOPE_IDENTITY()
END
GO
