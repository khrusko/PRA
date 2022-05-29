CREATE PROCEDURE [dbo].[LoanDelete] (@ID AS int,
                                     @DeletedBy AS int)
AS BEGIN
  UPDATE [dbo].[Loans]
  SET
    [DeletedBy]   = @DeletedBy,
    [DeleteDate]  = GETDATE()
  WHERE [ID] = @ID AND [DeleteDate] IS NULL

  RETURN @@ROWCOUNT
END
GO
