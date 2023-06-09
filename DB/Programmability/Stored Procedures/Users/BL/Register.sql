﻿CREATE PROCEDURE [dbo].[UserRegister] (@FName AS nvarchar(50),
                                       @LName AS nvarchar(50),
                                       @Email AS nvarchar(100),
                                       @Password AS nvarchar(512),
                                       @IsAdmin AS bit)
AS BEGIN
  DECLARE @UserID AS char(11)
  EXECUTE [dbo].[UserGenerateUserID] @IsAdmin = @IsAdmin, @UserID = @UserID OUTPUT

  DECLARE @IsUnique AS int = (
    SELECT ALL
      COUNT(*)
    FROM [dbo].[Users]
    WHERE [DeleteDate] IS NULL AND
          (
            [UserID] = @UserID OR
            [Email] = @Email
          )
  )
  IF @IsUnique > 0 BEGIN
    RETURN 0
  END

  DECLARE @PasswordHash AS nvarchar(512) = CONVERT(nvarchar(512), HASHBYTES('SHA2_512', @Password), 2)

  DECLARE @CreatedBy AS int = (
    SELECT ALL TOP 1
      [ID]
    FROM [dbo].[Users]
    WHERE [IsAdmin] = 1
    ORDER BY [ID] ASC
  )

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
