CREATE PROCEDURE [dbo].[UserCheckRegistrationStatus] (@GUID AS uniqueidentifier)
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
    DECLARE @CreateDate AS datetime
    DECLARE @RegistrationIsApproved AS bit

    SELECT ALL TOP 1
      @CreateDate = [CreateDate],
      @RegistrationIsApproved = [RegistrationIsApproved]
    FROM [dbo].[Users]
    WHERE [GUID] = @GUID

    DECLARE @PassedTime AS int = DATEDIFF(MINUTE, @CreateDate, GETDATE())

    IF @PassedTime <= 15 AND @RegistrationIsApproved = 0 BEGIN
      RETURN 1
    END
    ELSE IF @PassedTime <= 15 AND @RegistrationIsApproved = 1 BEGIN
      RETURN 2
    END
    ELSE BEGIN
      RETURN 3
    END
  END
END
GO