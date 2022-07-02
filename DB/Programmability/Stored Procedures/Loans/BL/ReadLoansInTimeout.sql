CREATE PROCEDURE [dbo].[LoanReadLoansInTimeout]
AS BEGIN
    SELECT ALL
      [ID],
      [CreateDate],
      [CreatedBy],
      [UpdateDate],
      [UpdatedBy],
      [DeleteDate],
      [DeletedBy],
      [BookFK], 
      [UserFK], 
      [LoanPrice], 
      [LoanDate], 
      [PlannedReturnDate],
      [ReturnDate],
      [DelayDays],
      [DelayPricePerDay]
    FROM [dbo].[Loans]
    WHERE [DeleteDate] IS NULL AND
          GETDATE() > [PlannedReturnDate] AND
          [ReturnDate] IS NULL
END
