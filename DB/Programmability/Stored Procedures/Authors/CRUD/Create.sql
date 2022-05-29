CREATE PROCEDURE [dbo].[AuthorCreate] (@FName AS nvarchar(50),
                                       @LName AS nvarchar(50),
                                       @BirthDate AS date,
                                       @ImagePath AS nvarchar(500),
                                       @Biography AS nvarchar(MAX),
                                       @CreatedBy AS int)
AS BEGIN
  INSERT INTO [dbo].[Authors] 
  (
    [CreatedBy], 
    [UpdatedBy], 
    [FName], 
    [LName], 
    [BirthDate], 
    [ImagePath], 
    [Biography]
  )
  VALUES 
  (
    @CreatedBy,
    @CreatedBy,
    @FName,
    @LName,
    @BirthDate,
    @ImagePath,
    @Biography
  )

  RETURN SCOPE_IDENTITY()
END
GO