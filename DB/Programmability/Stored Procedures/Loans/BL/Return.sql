CREATE PROCEDURE [dbo].[LoanReturn] (@ID AS int,
                                     @UpdatedBy AS int)
AS BEGIN
  DECLARE @PlannedReturnDate AS datetime
  DECLARE @BookFK AS int
  
  SELECT ALL TOP 1 
    @PlannedReturnDate = [PlannedReturnDate],
    @BookFK = [BookFK]
  FROM [dbo].[Loans]
  WHERE [ID] = @ID

  DECLARE @ReturnDate AS datetime = GETDATE()

  DECLARE @DelayDays AS int = 0
  IF @ReturnDate > @PlannedReturnDate BEGIN
    SET @DelayDays = DATEDIFF(DAY, @PlannedReturnDate, @ReturnDate)
  END

  UPDATE [dbo].[Loans]
  SET
    [UpdatedBy]   = @UpdatedBy,
    [UpdateDate]  = @ReturnDate,
    [ReturnDate]  = @ReturnDate,
    [DelayDays]   = @DelayDays
  WHERE [ID] = @ID AND 
        [ReturnDate] IS NULL AND 
        [DeleteDate] IS NULL

  DECLARE @RowCount AS int = @@ROWCOUNT
  IF @RowCount = 0 BEGIN
    RETURN 0
  END

  UPDATE [dbo].[Books]
  SET
    [Quantity] = [Quantity] + 1
  WHERE [ID] = @BookFK

  RETURN @RowCount
END
GO
