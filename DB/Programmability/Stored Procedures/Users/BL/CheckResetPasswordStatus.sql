CREATE PROCEDURE [dbo].[UserCheckResetPasswordStatus] (@GUID AS uniqueidentifier)
AS BEGIN
  DECLARE @RecordExists AS int = (
    SELECT ALL TOP 1
      COUNT(*)
    FROM [dbo].[Users]
    WHERE [GUID] = @GUID
  )
  
  IF @RecordExists = 0 BEGIN
    RETURN 0
  END
  ELSE BEGIN
    DECLARE @ResetPasswordDate AS datetime
    DECLARE @ResetPasswordIsApproved AS bit

    SELECT ALL TOP 1
      @ResetPasswordDate = [CreateDate],
      @ResetPasswordIsApproved = [ResetPasswordIsApproved]
    FROM [dbo].[Users]
    WHERE [GUID] = @GUID

    DECLARE @PassedTime AS int = DATEDIFF(MINUTE, @ResetPasswordDate, GETDATE())

    IF @PassedTime <= 5 AND @ResetPasswordIsApproved = 1 BEGIN
      RETURN 1
    END
    ELSE IF @PassedTime <= 5 AND @ResetPasswordIsApproved = 0 BEGIN
      RETURN 2
    END
    ELSE BEGIN
      RETURN 3
    END
  END
END
GO
