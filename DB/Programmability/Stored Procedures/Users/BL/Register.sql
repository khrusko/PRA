CREATE PROCEDURE [dbo].[UserRegister] (@UserID AS char(11),
                                       @FName AS nvarchar(50),
                                       @LName AS nvarchar(50),
                                       @Email AS nvarchar(100),
                                       @Password AS nvarchar(512),
                                       @IsAdmin AS bit,
                                       @CreatedBy AS int)
AS BEGIN
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
  IF @IsUnique > 1 BEGIN
    RETURN 0
  END

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

  DECLARE @ID AS int = SCOPE_IDENTITY()

  SELECT ALL TOP 1
    [ID],
    [CreateDate],
    [CreatedBy],
    [UpdateDate],
    [UpdatedBy],
    [DeleteDate],
    [DeletedBy],
    [UserID], 
    [FName], 
    [LName], 
    [Email], 
    [PasswordHash], 
    [ImagePath],
    [Address],
    [IsAdmin]
  FROM [dbo].[Users]
  WHERE [ID] = @ID AND [DeleteDate] IS NULL
END
GO
