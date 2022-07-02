CREATE PROCEDURE [dbo].[AuthorUpdate] (@ID AS int,
                                       @FName AS nvarchar(50),
                                       @LName AS nvarchar(50),
                                       @BirthDate AS date,
                                       @ImagePath AS nvarchar(500),
                                       @Biography AS nvarchar(MAX),
                                       @UpdatedBy AS int)
AS BEGIN
  UPDATE [dbo].[Authors]
  SET
    [UpdatedBy]   = @UpdatedBy,
    [UpdateDate]  = GETDATE(),
    [FName]       = @FName,
    [LName]       = @LName,
    [BirthDate]   = @BirthDate,
    [ImagePath]   = @ImagePath,
    [Biography]   = @Biography
  WHERE [ID] = @ID AND [DeleteDate] IS NULL

  RETURN @@ROWCOUNT
END
GO
