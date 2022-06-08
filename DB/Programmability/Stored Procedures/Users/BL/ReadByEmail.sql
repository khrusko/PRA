CREATE PROCEDURE [dbo].[UserReadByEmail] (@Email AS nvarchar(100))
AS BEGIN
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
    [IsAdmin],
    [GUID],
    [RegistrationIsApproved],
    [RegistrationDate],
    [ResetPasswordIsApproved],
    [ResetPasswordDate]
  FROM [dbo].[Users]
  WHERE [DeleteDate] IS NULL AND
        [Email] = @Email
END
GO
