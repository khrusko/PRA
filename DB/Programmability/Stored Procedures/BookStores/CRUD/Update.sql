CREATE PROCEDURE [dbo].[BookStoreUpdate] (@ID AS int,
                                          @Name AS nvarchar(100),
                                          @OIB AS char(11),
                                          @DelayPricePerDay AS decimal(5, 2),
                                          @Address AS nvarchar(200),
                                          @Telephone AS nvarchar(50),
                                          @Mobile AS  nvarchar(50),
                                          @Email AS nvarchar(100),
                                          @UpdatedBy AS int)
AS BEGIN
  UPDATE [dbo].[BookStores]
  SET
    [UpdatedBy]         = @UpdatedBy,
    [UpdateDate]        = GETDATE(),
    [Name]              = @Name,
    [OIB]               = @OIB,
    [DelayPricePerDay]  = @DelayPricePerDay,
    [Address]           = @Address,
    [Telephone]         = @Telephone,
    [Mobile]            = @Mobile,
    [Email]             = @Email
  WHERE [ID] = @ID AND [DeleteDate] IS NULL

  RETURN @@ROWCOUNT
END
GO