CREATE PROCEDURE [dbo].[BookStoreUpdate] (@ID AS int,
                                          @Name AS nvarchar(100),
                                          @OIB AS char(11),
                                          @DelayPricePerDay AS decimal(5, 2),
                                          @Email AS nvarchar(100),
                                          @UpdatedBy AS int)
AS BEGIN
  DECLARE @IsUnique AS int = (
    SELECT ALL
      COUNT(*)
    FROM [dbo].[BookStores]
    WHERE [ID] != @ID AND
          (
            [OIB] = @OIB OR 
            [Email] = @Email
          ) AND
          [DeleteDate] IS NULL
  )
  IF @IsUnique > 0 BEGIN
    RETURN 0
  END

  UPDATE [dbo].[BookStores]
  SET
    [UpdatedBy]         = @UpdatedBy,
    [UpdateDate]        = GETDATE(),
    [Name]              = @Name,
    [OIB]               = @OIB,
    [DelayPricePerDay]  = @DelayPricePerDay,
    [Email]             = @Email
  WHERE [ID] = @ID AND [DeleteDate] IS NULL

  RETURN @@ROWCOUNT
END
GO