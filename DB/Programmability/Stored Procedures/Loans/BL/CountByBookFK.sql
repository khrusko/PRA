CREATE PROCEDURE [dbo].[LoanCountByBookFK] (@BookFK AS int)
AS BEGIN
  SELECT ALL
    COUNT(*)
  FROM [dbo].[Loans]
  WHERE [DeleteDate] IS NULL AND
        [BookFK] = @BookFK
END
