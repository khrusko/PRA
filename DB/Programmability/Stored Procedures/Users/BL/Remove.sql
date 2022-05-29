﻿CREATE PROCEDURE [dbo].[UserRemove] (@Email AS nvarchar(100),
                                     @Password AS nvarchar(512))
AS BEGIN
  DECLARE @PasswordHash AS nvarchar(512) = CONVERT(nvarchar(512), HASHBYTES('SHA2_512', @Password), 2)

  SELECT ALL
    [ID],
    [UserID], 
    [FName], 
    [LName], 
    [Email], 
    [PasswordHash], 
    [ImagePath],
    [Address],
    [IsAdmin]
  FROM [dbo].[Users]
  WHERE [Email] = @Email AND [PasswordHash] = @PasswordHash AND [DeleteDate] IS NULL
END
GO