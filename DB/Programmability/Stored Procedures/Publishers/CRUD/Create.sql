CREATE PROCEDURE [dbo].[PublisherCreate] (@Name AS nvarchar(100),
                                          @CreatedBy AS int)
AS BEGIN
  DECLARE @IsUnique AS int = (
    SELECT ALL
      COUNT(*)
    FROM [dbo].[Publishers]
    WHERE [DeleteDate] IS NULL AND
          [Name] = @Name
  )
  IF @IsUnique > 0 BEGIN
    RETURN 0
  END

  INSERT INTO [dbo].[Publishers] 
  (
    [CreatedBy], 
    [UpdatedBy], 
    [Name]
  )
  VALUES 
  (
    @CreatedBy, 
    @CreatedBy, 
    @Name
  )

  RETURN SCOPE_IDENTITY()
END
GO
