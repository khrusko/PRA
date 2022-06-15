CREATE PROCEDURE [dbo].[UserRead] (@Method AS int,
                                   @ID AS int = NULL)
AS BEGIN
  IF @Method = 0 BEGIN
    SELECT ALL
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
      [IsAdmin],
      [GUID],
      [RegistrationIsApproved],
      [RegistrationDate],
      [ResetPasswordIsApproved],
      [ResetPasswordDate]
    FROM [dbo].[Users]
    ORDER BY [FName] ASC, [LName] ASC
  END
  IF @Method = 1 BEGIN
    SELECT ALL
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
      [IsAdmin],
      [GUID],
      [RegistrationIsApproved],
      [RegistrationDate],
      [ResetPasswordIsApproved],
      [ResetPasswordDate]
    FROM [dbo].[Users]
    WHERE [DeleteDate] IS NULL
    ORDER BY [FName] ASC, [LName] ASC
  END
  ELSE IF @Method = 2 BEGIN
    SELECT ALL
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
      [IsAdmin],
      [GUID],
      [RegistrationIsApproved],
      [RegistrationDate],
      [ResetPasswordIsApproved],
      [ResetPasswordDate]
    FROM [dbo].[Users]
    WHERE [ID] = @ID
  END
  ELSE IF @Method = 3 BEGIN
    SELECT ALL
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
      [IsAdmin],
      [GUID],
      [RegistrationIsApproved],
      [RegistrationDate],
      [ResetPasswordIsApproved],
      [ResetPasswordDate]
    FROM [dbo].[Users]
    WHERE [ID] = @ID AND [DeleteDate] IS NULL
  END
END
GO
