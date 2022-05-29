CREATE PROCEDURE [dbo].[LoanRead] (@ID AS int = NULL)
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
      [BookFK], 
      [UserFK], 
      [LoanPrice], 
      [LoanDate], 
      [PlannedReturnDate],
      [ReturnDate],
      [DelayDays],
      [DelayPricePerDay]
    FROM [dbo].[Loans]
    WHERE [DeleteDate] IS NULL
    ORDER BY [ReturnDate] ASC, [LoanDate] DESC
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
      [BookFK], 
      [UserFK], 
      [LoanPrice], 
      [LoanDate], 
      [PlannedReturnDate],
      [ReturnDate],
      [DelayDays],
      [DelayPricePerDay]
    FROM [dbo].[Loans]
    WHERE [ID] = @ID AND [DeleteDate] IS NULL
  END
END
GO
