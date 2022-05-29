CREATE PROCEDURE [dbo].[BranchOfficeCreate] (@Name AS nvarchar(100),
                                             @Address AS nvarchar(200),
                                             @Telephone AS nvarchar(50),
                                             @Email AS nvarchar(100),
                                             @CreatedBy AS int)
AS BEGIN
  INSERT INTO [dbo].[BranchOffices] 
  (
    [CreatedBy], 
    [UpdatedBy], 
    [BookStoreFK],
    [Name],
    [Address],
    [Telephone],
    [Email]
  )
  VALUES 
  (
    @CreatedBy,
    @CreatedBy,
    (SELECT ALL TOP 1 [ID] FROM [dbo].[BookStores]),
    @Name,
    @Address,
    @Telephone,
    @Email
  )

  RETURN SCOPE_IDENTITY()
END
GO
