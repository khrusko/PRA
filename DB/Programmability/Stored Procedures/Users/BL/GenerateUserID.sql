CREATE PROCEDURE [dbo].[UserGenerateUserID] (@IsAdmin AS bit,
                                             @UserID AS char(11) OUTPUT)
AS BEGIN
  DECLARE @UserIdentifier AS char(1) = 
    CASE @IsAdmin
      WHEN 0 THEN 'K'
      ELSE 'D'
    END

  DECLARE @DateStr AS char(6) = (SELECT FORMAT(GETDATE(), 'yyMMdd'))

  DECLARE @UserNumber AS char(4) = (
    SELECT ALL 
      FORMAT(COUNT(*) + 1, '0000')
    FROM [dbo].[Users] 
    WHERE CONVERT(DATE, [CreateDate]) = CONVERT(DATE, GETDATE())
  )

  SET @UserID = CONVERT(char(11), CONCAT(@UserIdentifier, @DateStr, @UserNumber))
END
GO