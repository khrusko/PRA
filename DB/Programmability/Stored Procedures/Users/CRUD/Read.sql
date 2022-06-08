﻿CREATE PROCEDURE [dbo].[UserRead] (@ID AS int = NULL)
AS BEGIN
  IF @ID IS NULL BEGIN
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
  ELSE BEGIN
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
