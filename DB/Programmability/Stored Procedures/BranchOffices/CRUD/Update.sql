CREATE PROCEDURE [dbo].[BranchOfficeUpdate] (@ID AS int,
                                             @Name AS nvarchar(100),
                                             @Address AS nvarchar(200),
                                             @Telephone AS nvarchar(50),
                                             @Email AS nvarchar(100),
                                             @UpdatedBy AS int)
AS BEGIN
  UPDATE [dbo].[BranchOffices]
  SET
    [UpdatedBy]   = @UpdatedBy,
    [UpdateDate]  = GETDATE(),
    [Name]        = @Name,
    [Address]     = @Address,
    [Telephone]   = @Telephone,
    [Email]       = @Email
  WHERE [ID] = @ID AND [DeleteDate] IS NULL

  RETURN @@ROWCOUNT
END
GO
