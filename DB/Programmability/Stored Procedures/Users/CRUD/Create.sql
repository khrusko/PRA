CREATE PROCEDURE [dbo].[UserCreate] (@UserID AS char(11),
                                     @FName AS nvarchar(50),
                                     @LName AS nvarchar(50),
                                     @Email AS nvarchar(100),
                                     @PasswordHash AS nvarchar(512),
                                     @ImagePath AS nvarchar(500),
                                     @Address AS nvarchar(200),
                                     @IsAdmin AS bit,
                                     @ConfirmationGUID AS uniqueidentifier,
                                     @ConfirmationIsApproved AS bit,
                                     @ConfirmationDate AS datetime,
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

  INSERT INTO [dbo].[Users] 
  (
    [CreatedBy], 
    [UpdatedBy],
    [UserID], 
    [FName], 
    [LName], 
    [Email], 
    [PasswordHash], 
    [ImagePath],
    [Address],
    [IsAdmin],
    [ConfirmationGUID],
    [ConfirmationIsApproved],
    [ConfirmationDate]
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
    @ImagePath,
    @Address,
    @IsAdmin,
    @ConfirmationGUID,
    @ConfirmationIsApproved,
    @ConfirmationDate
  )

  RETURN SCOPE_IDENTITY()
END
GO
