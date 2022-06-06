CREATE PROCEDURE [dbo].[UserCheckRegistrationStatus] (@ConfirmationGUID AS uniqueidentifier)
AS BEGIN
  DECLARE @RecordExists AS int = (
    SELECT ALL TOP 1
      COUNT(*)
    FROM [dbo].[Users]
    WHERE [ConfirmationGUID] = @ConfirmationGUID
  )
  
  IF @RecordExists = 0 BEGIN
    RETURN 0
  END
  ELSE BEGIN
    DECLARE @CreateDate AS datetime
    DECLARE @ConfirmationIsApproved AS bit

    SELECT ALL TOP 1
      @CreateDate = [CreateDate],
      @ConfirmationIsApproved = [ConfirmationIsApproved]
    FROM [dbo].[Users]
    WHERE [ConfirmationGUID] = @ConfirmationGUID

    DECLARE @PassedTime AS int = DATEDIFF(MINUTE, @CreateDate, GETDATE())

    IF @PassedTime <= 15 AND @ConfirmationIsApproved = 0 BEGIN
      RETURN 1
    END
    ELSE IF @PassedTime <= 15 AND @ConfirmationIsApproved = 1 BEGIN
      RETURN 2
    END
    ELSE BEGIN
      RETURN 3
    END
  END
END
GO