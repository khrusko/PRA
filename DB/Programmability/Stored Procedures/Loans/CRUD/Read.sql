CREATE PROCEDURE [dbo].[LoanRead] (@Method AS int,
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
      [BookFK], 
      [UserFK], 
      [LoanPrice], 
      [LoanDate], 
      [PlannedReturnDate],
      [ReturnDate],
      [DelayDays],
      [DelayPricePerDay]
    FROM [dbo].[Loans]
    ORDER BY [ReturnDate] ASC, [LoanDate] DESC
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
  ELSE IF @Method = 2 BEGIN
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
