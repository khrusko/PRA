CREATE PROCEDURE [dbo].[UserRegister] (@UserID AS char(11),
                                       @FName AS nvarchar(50),
                                       @LName AS nvarchar(50),
                                       @Email AS nvarchar(100),
                                       @Password AS nvarchar(512),
                                       @IsAdmin AS bit,
                                       @CreatedBy AS int)
AS BEGIN
  DECLARE @PasswordHash AS nvarchar(512) = CONVERT(nvarchar(512), HASHBYTES('SHA2_512', @Password), 2)

  INSERT INTO [dbo].[Users] 
  (
    [CreatedBy], 
    [UpdatedBy],
    [UserID], 
    [FName], 
    [LName], 
    [Email], 
    [PasswordHash], 
    [IsAdmin]
  )
  VALUES 
  (
    @CreatedBy,
    @CreatedBy,
    @UserID,
    @FName,
    @LName,
    @Email,
    @PasswordHash,
    @IsAdmin
  )

  RETURN SCOPE_IDENTITY()
END
GO
