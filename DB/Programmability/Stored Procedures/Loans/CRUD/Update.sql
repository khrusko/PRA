CREATE PROCEDURE [dbo].[LoanUpdate] (@ID AS int,
                                     @BookFK AS int,
                                     @UserFK AS int,
                                     @LoanPrice AS decimal(5, 2),
                                     @LoanDate AS datetime,
                                     @PlannedReturnDate AS smalldatetime,
                                     @ReturnDate AS smalldatetime,
                                     @DelayDays AS int,
                                     @DelayPricePerDay AS decimal(5, 2),
                                     @UpdatedBy AS int)
AS BEGIN
  UPDATE [dbo].[Loans]
  SET
    [UpdatedBy]         = @UpdatedBy,
    [UpdateDate]        = GETDATE(),
    [BookFK]            = @BookFK,
    [UserFK]            = @UserFK,
    [LoanPrice]         = @LoanPrice,
    [LoanDate]          = @LoanDate,
    [PlannedReturnDate] = @PlannedReturnDate,
    [ReturnDate]        = @ReturnDate,
    [DelayDays]         = @DelayDays,
    [DelayPricePerDay]  = @DelayPricePerDay
  WHERE [ID] = @ID AND [DeleteDate] IS NULL

  RETURN @@ROWCOUNT
END
GO
