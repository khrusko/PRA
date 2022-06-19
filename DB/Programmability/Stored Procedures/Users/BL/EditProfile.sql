CREATE PROCEDURE [dbo].[UserEditProfile] (@ID AS int,
                                          @FName AS nvarchar(50),
                                          @LName AS nvarchar(50),
                                          @ImagePath AS nvarchar(500),
                                          @Address AS nvarchar(200),
                                          @UpdatedBy AS int)
AS BEGIN
  UPDATE [dbo].[Users]
  SET
    [UpdatedBy]   = @UpdatedBy,
    [UpdateDate]  = GETDATE(),
    [FName]       = @FName,
    [LName]       = @LName,
    [ImagePath]   = @ImagePath,
    [Address]     = @Address
  WHERE [DeleteDate] IS NULL AND
        [ID] = @ID

  RETURN @@ROWCOUNT
END
