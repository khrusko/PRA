CREATE PROCEDURE [dbo].[UserLogin] (@Email AS nvarchar(100),
                                    @Password AS nvarchar(512))
AS BEGIN
  DECLARE @PasswordHash AS nvarchar(512) = CONVERT(nvarchar(512), HASHBYTES('SHA2_512', @Password), 2)

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
  WHERE [Email] = @Email AND [PasswordHash] = @PasswordHash AND [DeleteDate] IS NULL
END
GO
