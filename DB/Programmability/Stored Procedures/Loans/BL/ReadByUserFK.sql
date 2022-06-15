CREATE PROCEDURE [dbo].[LoanReadByUserFK] (@Active AS bit,
                                           @UserFK AS int)
AS BEGIN
  IF @Active = 0 BEGIN
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
          [UserFK] = @UserFK
  END
  ELSE BEGIN
    SELECT ALL TOP 3
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
          [ReturnDate] IS NULL AND
          [UserFK] = @UserFK
  END
END
