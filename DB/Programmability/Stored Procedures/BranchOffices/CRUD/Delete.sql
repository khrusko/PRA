CREATE PROCEDURE [dbo].[BranchOfficeDelete] (@ID AS int,
                                             @DeletedBy AS int)
AS BEGIN
  UPDATE [dbo].[BranchOffices]
  SET
    [DeletedBy]   = @DeletedBy,
    [DeleteDate]  = GETDATE()
  WHERE [ID] = @ID AND [DeleteDate] IS NULL

  RETURN @@ROWCOUNT
END
GO
