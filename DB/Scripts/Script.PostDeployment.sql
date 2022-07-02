USE [msdb]
GO

-- JOBS

IF NOT EXISTS (SELECT * FROM msdb.dbo.sysjobs_view WHERE [name] = N'SubscriptionResolve') BEGIN
  BEGIN TRANSACTION
  DECLARE @ReturnCode AS int = 0
  IF NOT EXISTS (SELECT [name] FROM msdb.dbo.syscategories WHERE [name] = N'[Uncategorized (Local)]' AND category_class = 1) BEGIN
    EXEC @ReturnCode = msdb.dbo.sp_add_category @class  = N'JOB',
                                                @type   = N'LOCAL',
                                                @name   = N'[Uncategorized (Local)]'
    IF (@@ERROR <> 0 OR @ReturnCode <> 0) BEGIN
      GOTO QuitWithRollback
    END
  END

  DECLARE @jobId AS binary(16)
  EXEC @ReturnCode =  msdb.dbo.sp_add_job @job_name               = N'SubscriptionResolve',
                                          @enabled                = 1,
                                          @notify_level_eventlog  = 0,
                                          @notify_level_email     = 0,
                                          @notify_level_netsend   = 0,
                                          @notify_level_page      = 0,
                                          @delete_level           = 0,
                                          @description            = N'No description available.',
                                          @category_name          = N'[Uncategorized (Local)]',
                                          @owner_login_name       = N'sa',
                                          @job_id                 = @jobId OUTPUT
  IF (@@ERROR <> 0 OR @ReturnCode <> 0) BEGIN
    GOTO QuitWithRollback
  END

  EXEC @ReturnCode = msdb.dbo.sp_add_jobstep @job_id                = @jobId,
                                             @step_name             = N'CallSubscriptionEndpoint', 
                                             @step_id               = 1, 
                                             @cmdexec_success_code  = 0, 
                                             @on_success_action     = 1, 
                                             @on_success_step_id    = 0, 
                                             @on_fail_action        = 2, 
                                             @on_fail_step_id       = 0, 
                                             @retry_attempts        = 0, 
                                             @retry_interval        = 0, 
                                             @os_run_priority       = 0,
                                             @subsystem             = N'PowerShell', 
                                             @command               = N'PowerShell.exe -Command "& { Invoke-WebRequest -URI http://localhost:50513/Subscription/Resolve }"', 
                                             @database_name         = N'BOOKSTORE', 
                                             @flags                 = 0
  IF (@@ERROR <> 0 OR @ReturnCode <> 0) BEGIN
    GOTO QuitWithRollback
  END

  EXEC @ReturnCode = msdb.dbo.sp_update_job @job_id         = @jobId,
                                            @start_step_id  = 1
  IF (@@ERROR <> 0 OR @ReturnCode <> 0) BEGIN
    GOTO QuitWithRollback
  END

  EXEC @ReturnCode = msdb.dbo.sp_add_jobschedule @job_id                  = @jobId,
                                                 @name                    = N'CallSubscriptionEndpointSchedule', 
                                                 @enabled                 = 1, 
                                                 @freq_type               = 4, 
                                                 @freq_interval           = 1, 
                                                 @freq_subday_type        = 4, 
                                                 @freq_subday_interval    = 10, 
                                                 @freq_relative_interval  = 0, 
                                                 @freq_recurrence_factor  = 0, 
                                                 @active_start_date       = 20220619, 
                                                 @active_end_date         = 99991231, 
                                                 @active_start_time       = 0, 
                                                 @active_end_time         = 235959, 
                                                 @schedule_uid            = N'db385f89-a88f-48c9-8a12-7a29bada5a0b'
  IF (@@ERROR <> 0 OR @ReturnCode <> 0) BEGIN
    GOTO QuitWithRollback
  END

  EXEC @ReturnCode = msdb.dbo.sp_add_jobserver @job_id = @jobId,
                                               @server_name = N'(local)'
  IF (@@ERROR <> 0 OR @ReturnCode <> 0) BEGIN
    GOTO QuitWithRollback
  END

  COMMIT TRANSACTION
  GOTO EndSave

  QuitWithRollback:
      IF (@@TRANCOUNT > 0) BEGIN
        ROLLBACK TRANSACTION
      END

  EndSave:
END
GO

IF NOT EXISTS (SELECT * FROM msdb.dbo.sysjobs_view WHERE [name] = N'DeleteTimeoutRegistration') BEGIN
  BEGIN TRANSACTION
  DECLARE @ReturnCode AS int = 0
  IF NOT EXISTS (SELECT [name] FROM msdb.dbo.syscategories WHERE [name] = N'[Uncategorized (Local)]' AND category_class = 1) BEGIN
    EXEC @ReturnCode = msdb.dbo.sp_add_category @class  = N'JOB',
                                                @type   = N'LOCAL',
                                                @name   = N'[Uncategorized (Local)]'
    IF (@@ERROR <> 0 OR @ReturnCode <> 0) BEGIN
      GOTO QuitWithRollback
    END
  END

  DECLARE @jobId AS binary(16)
  EXEC @ReturnCode =  msdb.dbo.sp_add_job @job_name               = N'DeleteTimeoutRegistration',
                                          @enabled                = 1,
                                          @notify_level_eventlog  = 0,
                                          @notify_level_email     = 0,
                                          @notify_level_netsend   = 0,
                                          @notify_level_page      = 0,
                                          @delete_level           = 0,
                                          @description            = N'No description available.',
                                          @category_name          = N'[Uncategorized (Local)]',
                                          @owner_login_name       = N'sa',
                                          @job_id                 = @jobId OUTPUT
  IF (@@ERROR <> 0 OR @ReturnCode <> 0) BEGIN
    GOTO QuitWithRollback
  END

  EXEC @ReturnCode = msdb.dbo.sp_add_jobstep @job_id                = @jobId,
                                             @step_name             = N'CallProcedureUserDeleteTimeoutRegistration', 
                                             @step_id               = 1, 
                                             @cmdexec_success_code  = 0, 
                                             @on_success_action     = 1, 
                                             @on_success_step_id    = 0, 
                                             @on_fail_action        = 2, 
                                             @on_fail_step_id       = 0, 
                                             @retry_attempts        = 0, 
                                             @retry_interval        = 0, 
                                             @os_run_priority       = 0,
                                             @subsystem             = N'TSQL', 
                                             @command               = N'EXECUTE [dbo].[UserDeleteTimeoutRegistration]', 
                                             @database_name         = N'BOOKSTORE', 
                                             @flags                 = 0
  IF (@@ERROR <> 0 OR @ReturnCode <> 0) BEGIN
    GOTO QuitWithRollback
  END

  EXEC @ReturnCode = msdb.dbo.sp_update_job @job_id         = @jobId,
                                            @start_step_id  = 1
  IF (@@ERROR <> 0 OR @ReturnCode <> 0) BEGIN
    GOTO QuitWithRollback
  END

  EXEC @ReturnCode = msdb.dbo.sp_add_jobschedule @job_id                  = @jobId,
                                                 @name                    = N'CallProcedureUserDeleteTimeoutRegistrationSchedule', 
                                                 @enabled                 = 1, 
                                                 @freq_type               = 4, 
                                                 @freq_interval           = 1, 
                                                 @freq_subday_type        = 4, 
                                                 @freq_subday_interval    = 5, 
                                                 @freq_relative_interval  = 0, 
                                                 @freq_recurrence_factor  = 0, 
                                                 @active_start_date       = 20220619, 
                                                 @active_end_date         = 99991231, 
                                                 @active_start_time       = 0, 
                                                 @active_end_time         = 235959, 
                                                 @schedule_uid            = N'249c38f5-624b-49ac-b6ba-c2e89856eca8'
  IF (@@ERROR <> 0 OR @ReturnCode <> 0) BEGIN
    GOTO QuitWithRollback
  END

  EXEC @ReturnCode = msdb.dbo.sp_add_jobserver @job_id = @jobId,
                                               @server_name = N'(local)'
  IF (@@ERROR <> 0 OR @ReturnCode <> 0) BEGIN
    GOTO QuitWithRollback
  END

  COMMIT TRANSACTION
  GOTO EndSave

  QuitWithRollback:
      IF (@@TRANCOUNT > 0) BEGIN
        ROLLBACK TRANSACTION
      END

  EndSave:
END
GO

IF NOT EXISTS (SELECT * FROM msdb.dbo.sysjobs_view WHERE [name] = N'DisableTimeoutResetPassword') BEGIN
  BEGIN TRANSACTION
  DECLARE @ReturnCode AS int = 0
  IF NOT EXISTS (SELECT [name] FROM msdb.dbo.syscategories WHERE [name] = N'[Uncategorized (Local)]' AND category_class = 1) BEGIN
    EXEC @ReturnCode = msdb.dbo.sp_add_category @class  = N'JOB',
                                                @type   = N'LOCAL',
                                                @name   = N'[Uncategorized (Local)]'
    IF (@@ERROR <> 0 OR @ReturnCode <> 0) BEGIN
      GOTO QuitWithRollback
    END
  END

  DECLARE @jobId AS binary(16)
  EXEC @ReturnCode =  msdb.dbo.sp_add_job @job_name               = N'DisableTimeoutResetPassword',
                                          @enabled                = 1,
                                          @notify_level_eventlog  = 0,
                                          @notify_level_email     = 0,
                                          @notify_level_netsend   = 0,
                                          @notify_level_page      = 0,
                                          @delete_level           = 0,
                                          @description            = N'No description available.',
                                          @category_name          = N'[Uncategorized (Local)]',
                                          @owner_login_name       = N'sa',
                                          @job_id                 = @jobId OUTPUT
  IF (@@ERROR <> 0 OR @ReturnCode <> 0) BEGIN
    GOTO QuitWithRollback
  END

  EXEC @ReturnCode = msdb.dbo.sp_add_jobstep @job_id                = @jobId,
                                             @step_name             = N'CallProcedureUserDisableTimeoutResetPassword', 
                                             @step_id               = 1, 
                                             @cmdexec_success_code  = 0, 
                                             @on_success_action     = 1, 
                                             @on_success_step_id    = 0, 
                                             @on_fail_action        = 2, 
                                             @on_fail_step_id       = 0, 
                                             @retry_attempts        = 0, 
                                             @retry_interval        = 0, 
                                             @os_run_priority       = 0,
                                             @subsystem             = N'TSQL', 
                                             @command               = N'EXECUTE [dbo].[UserDisableTimeoutResetPassword]', 
                                             @database_name         = N'BOOKSTORE', 
                                             @flags                 = 0
  IF (@@ERROR <> 0 OR @ReturnCode <> 0) BEGIN
    GOTO QuitWithRollback
  END

  EXEC @ReturnCode = msdb.dbo.sp_update_job @job_id         = @jobId,
                                            @start_step_id  = 1
  IF (@@ERROR <> 0 OR @ReturnCode <> 0) BEGIN
    GOTO QuitWithRollback
  END

  EXEC @ReturnCode = msdb.dbo.sp_add_jobschedule @job_id                  = @jobId,
                                                 @name                    = N'CallProcedureUserDisableTimeoutResetPasswordSchedule', 
                                                 @enabled                 = 1, 
                                                 @freq_type               = 4, 
                                                 @freq_interval           = 1, 
                                                 @freq_subday_type        = 4, 
                                                 @freq_subday_interval    = 5, 
                                                 @freq_relative_interval  = 0, 
                                                 @freq_recurrence_factor  = 0, 
                                                 @active_start_date       = 20220619, 
                                                 @active_end_date         = 99991231, 
                                                 @active_start_time       = 0, 
                                                 @active_end_time         = 235959, 
                                                 @schedule_uid            = N'9b9711ba-b844-4f23-9819-df95a97c33da'
  IF (@@ERROR <> 0 OR @ReturnCode <> 0) BEGIN
    GOTO QuitWithRollback
  END

  EXEC @ReturnCode = msdb.dbo.sp_add_jobserver @job_id = @jobId,
                                               @server_name = N'(local)'
  IF (@@ERROR <> 0 OR @ReturnCode <> 0) BEGIN
    GOTO QuitWithRollback
  END

  COMMIT TRANSACTION
  GOTO EndSave

  QuitWithRollback:
      IF (@@TRANCOUNT > 0) BEGIN
        ROLLBACK TRANSACTION
      END

  EndSave:
END
GO

IF NOT EXISTS (SELECT * FROM msdb.dbo.sysjobs_view WHERE [name] = N'NotifyLoanTimeout') BEGIN
  BEGIN TRANSACTION
  DECLARE @ReturnCode AS int = 0
  IF NOT EXISTS (SELECT [name] FROM msdb.dbo.syscategories WHERE [name] = N'[Uncategorized (Local)]' AND category_class = 1) BEGIN
    EXEC @ReturnCode = msdb.dbo.sp_add_category @class  = N'JOB',
                                                @type   = N'LOCAL',
                                                @name   = N'[Uncategorized (Local)]'
    IF (@@ERROR <> 0 OR @ReturnCode <> 0) BEGIN
      GOTO QuitWithRollback
    END
  END

  DECLARE @jobId AS binary(16)
  EXEC @ReturnCode =  msdb.dbo.sp_add_job @job_name               = N'NotifyLoanTimeout',
                                          @enabled                = 1,
                                          @notify_level_eventlog  = 0,
                                          @notify_level_email     = 0,
                                          @notify_level_netsend   = 0,
                                          @notify_level_page      = 0,
                                          @delete_level           = 0,
                                          @description            = N'No description available.',
                                          @category_name          = N'[Uncategorized (Local)]',
                                          @owner_login_name       = N'sa',
                                          @job_id                 = @jobId OUTPUT
  IF (@@ERROR <> 0 OR @ReturnCode <> 0) BEGIN
    GOTO QuitWithRollback
  END

  EXEC @ReturnCode = msdb.dbo.sp_add_jobstep @job_id                = @jobId,
                                             @step_name             = N'CallLoanEndpoint', 
                                             @step_id               = 1, 
                                             @cmdexec_success_code  = 0, 
                                             @on_success_action     = 1, 
                                             @on_success_step_id    = 0, 
                                             @on_fail_action        = 2, 
                                             @on_fail_step_id       = 0, 
                                             @retry_attempts        = 0, 
                                             @retry_interval        = 0, 
                                             @os_run_priority       = 0,
                                             @subsystem             = N'PowerShell', 
                                             @command               = N'PowerShell.exe -Command "& { Invoke-WebRequest -URI http://localhost:50513/Loan/CheckTimeout }"', 
                                             @database_name         = N'BOOKSTORE', 
                                             @flags                 = 0
  IF (@@ERROR <> 0 OR @ReturnCode <> 0) BEGIN
    GOTO QuitWithRollback
  END

  EXEC @ReturnCode = msdb.dbo.sp_update_job @job_id         = @jobId,
                                            @start_step_id  = 1
  IF (@@ERROR <> 0 OR @ReturnCode <> 0) BEGIN
    GOTO QuitWithRollback
  END

  EXEC @ReturnCode = msdb.dbo.sp_add_jobschedule @job_id                  = @jobId,
                                                 @name                    = N'CallLoanEndpointShcedule', 
                                                 @enabled                 = 1, 
                                                 @freq_type               = 4, 
                                                 @freq_interval           = 1, 
                                                 @freq_subday_type        = 1, 
                                                 @freq_subday_interval    = 0, 
                                                 @freq_relative_interval  = 0, 
                                                 @freq_recurrence_factor  = 0, 
                                                 @active_start_date       = 20220619, 
                                                 @active_end_date         = 99991231, 
                                                 @active_start_time       = 0, 
                                                 @active_end_time         = 235959, 
                                                 @schedule_uid            = N'9b9711ba-b844-4f23-9819-df95a97c33da'
  IF (@@ERROR <> 0 OR @ReturnCode <> 0) BEGIN
    GOTO QuitWithRollback
  END

  EXEC @ReturnCode = msdb.dbo.sp_add_jobserver @job_id = @jobId,
                                               @server_name = N'(local)'
  IF (@@ERROR <> 0 OR @ReturnCode <> 0) BEGIN
    GOTO QuitWithRollback
  END

  COMMIT TRANSACTION
  GOTO EndSave

  QuitWithRollback:
      IF (@@TRANCOUNT > 0) BEGIN
        ROLLBACK TRANSACTION
      END

  EndSave:
END
GO

USE [BOOKSTORE]
GO

-- USERS [ADMIN]

IF NOT EXISTS (SELECT ALL * FROM [dbo].[Users] WHERE [Email] = N'admin@admin.com') BEGIN
  ALTER TABLE [Users] NOCHECK CONSTRAINT [FK_Users_CreatedBy]
  ALTER TABLE [Users] NOCHECK CONSTRAINT [FK_Users_UpdatedBy]
  ALTER TABLE [Users] NOCHECK CONSTRAINT [FK_Users_DeletedBy]

  DECLARE @Password AS nvarchar(512) = N'ALFzjq3PKYZF2jYZVII8dtpAqShmR2KM1GCSA2BkgVLfdMmTSb5c9dtw17Ys'

  INSERT INTO [dbo].[Users] 
  (
    [CreatedBy], 
    [UpdatedBy], 
    [UserID], 
    [FName], 
    [LName], 
    [Email], 
    [PasswordHash], 
    [IsAdmin],
    [RegistrationIsApproved],
    [RegistrationDate]
  )
  VALUES 
  (
    1, 
    1, 
    N'D0000000001', 
    N'db', 
    N'db', 
    N'jI6630EVKfSnW1qfIWtrBguTUuePOfxvf93ZCWj1WS2HIC92Dby3wMwEvwuA', 
    CONVERT(nvarchar(512), HASHBYTES('SHA2_512', @Password), 2), 
    1,
    1,
    GETDATE()
  )

  ALTER TABLE [Users] CHECK CONSTRAINT [FK_Users_CreatedBy]
  ALTER TABLE [Users] CHECK CONSTRAINT [FK_Users_UpdatedBy]
  ALTER TABLE [Users] CHECK CONSTRAINT [FK_Users_DeletedBy]
END
GO

-- USERS [KUPCI]

IF NOT EXISTS (SELECT ALL * FROM [dbo].[Users] WHERE [Email] = N'azver@gmail.com') BEGIN
  DECLARE @ID AS int
  EXECUTE @ID = [dbo].[UserRegister] N'Andrija', N'Zver', N'azver@gmail.com', N'azverPass', 0
  
  DECLARE @GUID AS uniqueidentifier
  SELECT ALL @GUID = [GUID] FROM [dbo].[Users] WHERE [ID] = @ID

  EXECUTE [dbo].[UserConfirmRegistration] @GUID
END
GO

IF NOT EXISTS (SELECT ALL * FROM [dbo].[Users] WHERE [Email] = N'mkos@gmail.com') BEGIN
  DECLARE @ID AS int
  EXECUTE @ID = [dbo].[UserRegister] N'Marija', N'Kos', N'mkos@gmail.com', N'mkosPass', 0
  
  DECLARE @GUID AS uniqueidentifier
  SELECT ALL @GUID = [GUID] FROM [dbo].[Users] WHERE [ID] = @ID

  EXECUTE [dbo].[UserConfirmRegistration] @GUID
END
GO

IF NOT EXISTS (SELECT ALL * FROM [dbo].[Users] WHERE [Email] = N'isimic@gmail.com') BEGIN
  DECLARE @ID AS int
  EXECUTE @ID = [dbo].[UserRegister] N'Ilija', N'Šimić', N'isimic@gmail.com', N'isimicPass', 0
  
  DECLARE @GUID AS uniqueidentifier
  SELECT ALL @GUID = [GUID] FROM [dbo].[Users] WHERE [ID] = @ID

  EXECUTE [dbo].[UserConfirmRegistration] @GUID
END
GO

IF NOT EXISTS (SELECT ALL * FROM [dbo].[Users] WHERE [Email] = N'sambrus@gmail.com') BEGIN
  DECLARE @ID AS int
  EXECUTE @ID = [dbo].[UserRegister] N'Stjepan', N'Ambruš', N'sambrus@gmail.com', N'sambrusPass', 0
  
  DECLARE @GUID AS uniqueidentifier
  SELECT ALL @GUID = [GUID] FROM [dbo].[Users] WHERE [ID] = @ID

  EXECUTE [dbo].[UserConfirmRegistration] @GUID
END
GO

IF NOT EXISTS (SELECT ALL * FROM [dbo].[Users] WHERE [Email] = N'dtopolnjak@gmail.com') BEGIN
  DECLARE @ID AS int
  EXECUTE @ID = [dbo].[UserRegister] N'Dario', N'Topolnjak', N'dtopolnjak@gmail.com', N'dtopolnjakPass', 0
  
  DECLARE @GUID AS uniqueidentifier
  SELECT ALL @GUID = [GUID] FROM [dbo].[Users] WHERE [ID] = @ID

  EXECUTE [dbo].[UserConfirmRegistration] @GUID
END
GO

IF NOT EXISTS (SELECT ALL * FROM [dbo].[Users] WHERE [Email] = N'fkocijan@gmail.com') BEGIN
  DECLARE @ID AS int
  EXECUTE @ID = [dbo].[UserRegister] N'Filip', N'Kocijan', N'fkocijan@gmail.com', N'fkocijanPass', 0
  
  DECLARE @GUID AS uniqueidentifier
  SELECT ALL @GUID = [GUID] FROM [dbo].[Users] WHERE [ID] = @ID

  EXECUTE [dbo].[UserConfirmRegistration] @GUID
END
GO

IF NOT EXISTS (SELECT ALL * FROM [dbo].[Users] WHERE [Email] = N'ljuric@gmail.com') BEGIN
  DECLARE @ID AS int
  EXECUTE @ID = [dbo].[UserRegister] N'Lovro', N'Jurić', N'ljuric@gmail.com', N'ljuricPass', 0
  
  DECLARE @GUID AS uniqueidentifier
  SELECT ALL @GUID = [GUID] FROM [dbo].[Users] WHERE [ID] = @ID

  EXECUTE [dbo].[UserConfirmRegistration] @GUID
END
GO

IF NOT EXISTS (SELECT ALL * FROM [dbo].[Users] WHERE [Email] = N'nkresoje@gmail.com') BEGIN
  DECLARE @ID AS int
  EXECUTE @ID = [dbo].[UserRegister] N'Nikola', N'Kresoje', N'nkresoje@gmail.com', N'nkresojePass', 0
  
  DECLARE @GUID AS uniqueidentifier
  SELECT ALL @GUID = [GUID] FROM [dbo].[Users] WHERE [ID] = @ID

  EXECUTE [dbo].[UserConfirmRegistration] @GUID
END
GO

IF NOT EXISTS (SELECT ALL * FROM [dbo].[Users] WHERE [Email] = N'astunkovic@gmail.com') BEGIN
  DECLARE @ID AS int
  EXECUTE @ID = [dbo].[UserRegister] N'Arjan', N'Stunković', N'astunkovic@gmail.com', N'astunkovicPass', 0
  
  DECLARE @GUID AS uniqueidentifier
  SELECT ALL @GUID = [GUID] FROM [dbo].[Users] WHERE [ID] = @ID

  EXECUTE [dbo].[UserConfirmRegistration] @GUID
END
GO

IF NOT EXISTS (SELECT ALL * FROM [dbo].[Users] WHERE [Email] = N'msprajc@gmail.com') BEGIN
  DECLARE @ID AS int
  EXECUTE @ID = [dbo].[UserRegister] N'Mihael', N'Šprajc', N'msprajc@gmail.com', N'msprajcPass', 0
  
  DECLARE @GUID AS uniqueidentifier
  SELECT ALL @GUID = [GUID] FROM [dbo].[Users] WHERE [ID] = @ID

  EXECUTE [dbo].[UserConfirmRegistration] @GUID
END
GO

IF NOT EXISTS (SELECT ALL * FROM [dbo].[Users] WHERE [Email] = N'bpetko@gmail.com') BEGIN
  DECLARE @ID AS int
  EXECUTE @ID = [dbo].[UserRegister] N'Borna', N'Petko', N'bpetko@gmail.com', N'bpetkoPass', 0
  
  DECLARE @GUID AS uniqueidentifier
  SELECT ALL @GUID = [GUID] FROM [dbo].[Users] WHERE [ID] = @ID

  EXECUTE [dbo].[UserConfirmRegistration] @GUID
END
GO

IF NOT EXISTS (SELECT ALL * FROM [dbo].[Users] WHERE [Email] = N'jturkovic@gmail.com') BEGIN
  DECLARE @ID AS int
  EXECUTE @ID = [dbo].[UserRegister] N'Julia', N'Turković', N'jturkovic@gmail.com', N'jturkovicPass', 0
  
  DECLARE @GUID AS uniqueidentifier
  SELECT ALL @GUID = [GUID] FROM [dbo].[Users] WHERE [ID] = @ID

  EXECUTE [dbo].[UserConfirmRegistration] @GUID
END
GO

-- USERS [DJELATNICI]

IF NOT EXISTS (SELECT ALL * FROM [dbo].[Users] WHERE [Email] = N'mmatic@racunarstvo.com') BEGIN
  DECLARE @ID AS int
  EXECUTE @ID = [dbo].[UserRegister] N'Mihael', N'Matić', N'mmatic@racunarstvo.com', N'mmaticPass', 1
  
  DECLARE @GUID AS uniqueidentifier
  SELECT ALL @GUID = [GUID] FROM [dbo].[Users] WHERE [ID] = @ID

  EXECUTE [dbo].[UserConfirmRegistration] @GUID
END
GO

IF NOT EXISTS (SELECT ALL * FROM [dbo].[Users] WHERE [Email] = N'jtatare@racunarstvo.com') BEGIN
  DECLARE @ID AS int
  EXECUTE @ID = [dbo].[UserRegister] N'Jan', N'Tatarević', N'jtatare@racunarstvo.com', N'jtatarePass', 1
  
  DECLARE @GUID AS uniqueidentifier
  SELECT ALL @GUID = [GUID] FROM [dbo].[Users] WHERE [ID] = @ID

  EXECUTE [dbo].[UserConfirmRegistration] @GUID
END
GO

IF NOT EXISTS (SELECT ALL * FROM [dbo].[Users] WHERE [Email] = N'khrusko@racunarstvo.com') BEGIN
  DECLARE @ID AS int
  EXECUTE @ID = [dbo].[UserRegister] N'Karlo', N'Hruškovec', N'khrusko@racunarstvo.com', N'khruskoPass', 1
  
  DECLARE @GUID AS uniqueidentifier
  SELECT ALL @GUID = [GUID] FROM [dbo].[Users] WHERE [ID] = @ID

  EXECUTE [dbo].[UserConfirmRegistration] @GUID
END
GO

IF NOT EXISTS (SELECT ALL * FROM [dbo].[Users] WHERE [Email] = N'mvujnov@racunarstvo.com') BEGIN
  DECLARE @ID AS int
  EXECUTE @ID = [dbo].[UserRegister] N'Mario', N'Vujnović', N'mvujnov@racunarstvo.com', N'mvujnovPass', 1
  
  DECLARE @GUID AS uniqueidentifier
  SELECT ALL @GUID = [GUID] FROM [dbo].[Users] WHERE [ID] = @ID

  EXECUTE [dbo].[UserConfirmRegistration] @GUID
END
GO

-- AUTHORS

IF NOT EXISTS (SELECT ALL * FROM [dbo].[Authors] WHERE [FName] = N'Vjenceslav' AND [LName] = N'Novak' AND [BirthDate] = N'18590911') BEGIN
  EXECUTE [dbo].[AuthorCreate] N'Vjenceslav', N'Novak', N'18590911', NULL, N'Novak, književnik realizma, širem je čitateljstvu najpoznatiji po svome epohalnom romanu „Posljednji Stipančići“, koji je dio srednjoškolske lektire. No, on je bio mnogo više od toga. Štoviše, za svoga života bio je najugledniji književnik realizma te je nazivan i hrvatskim Balzacom, no unatoč tome, živio je u velikoj neimaštini. Pisao je romane, feljtone, pripovijetke i kritike, a u svemu tome okrenuo se malom čovjeku. Značajan je po tome što je prvi u hrvatsku književnost unio malog čovjeka, sa svim njegovim problemima, slabostima i nadanjima. Prikazivao je pravu sliku hrvatskog siromaštva, propadanja patricija, ali i nezavidan položaj žena. Prisjetimo se samo Lucije Stipančić koja je imala neusporedivo lošiji tretman kod svog oca (ako se to može nazvati ocem) nego njezin stariji brat. Osim „Posljednjih Stipančića“, svakako preporučujem i bildungsroman „Tito Dorčić“, „Dva svijeta“ i pripovijetke „Iz velegradskog podzemlja“ te „Nezasitnost i bijeda“.', 1
END
GO

IF NOT EXISTS (SELECT ALL * FROM [dbo].[Authors] WHERE [FName] = N'Miroslav' AND [LName] = N'Krleža' AND [BirthDate] = N'18930707') BEGIN
  EXECUTE [dbo].[AuthorCreate] N'Miroslav', N'Krleža', N'18930707', NULL, N'Sažeti Krležino stvaralaštvo u svega nekoliko rečenica nije nimalo jednostavno. Njegove se drame smatraju najboljima u hrvatskoj književnosti, Glembajevi su postali svojevrsno nacionalno blago, po njemu je nazvan današnji Leksikografski zavod te postoji i velika enciklopedija posvećena njegovom životu i djelu nazvana „Krležijana“. Jedan od najboljih književnika svih vremena hrvatske književnosti pisao je pjesme, novele, drame, romane, putopise, eseje i polemike. Progovarao je o sukobu čovjeka i Boga, o besmislenosti rata i o njegovom pravom licu koje deformira ljudskost, o društvenoj nepravdi, staležima i složenim međuljudskim odnosima. Uz „Glembajeve“ svakako zavirite  barem u zbirku novela „Hrvatski bog Mars,“, prvi moderni hrvatski roman „Povratak Filipa Latinovicza“ i dijalektalnu liriku u „Baladama Petrice Kerempuha“.', 1
END
GO

IF NOT EXISTS (SELECT ALL * FROM [dbo].[Authors] WHERE [FName] = N'Dragutin' AND [LName] = N'Tadijanović' AND [BirthDate] = N'19051104') BEGIN
  EXECUTE [dbo].[AuthorCreate] N'Dragutin', N'Tadijanović', N'19051104', NULL, N'Kada bih morala izdvojiti samo jednog najdražeg hrvatskog pjesnika, tada bi to zasigurno bio Tadijanović. Još i sada u pola noći mogu izrecitirati nekoliko njegovih pjesama koje su sve redom jedne od najboljih ikada zabilježenih na ovim prostorima. U vrijeme intelektualiziranja i pokušaja da se poezija što više „kiti“, unio je dašak svježine. Jednostavnim lirskim govorom u poeziju je donio iznimnu količinu emocija i stvarnih životnih, ali na toliko jednostavan način, razumljiv i djetetu, kakav prije nikada nije zabilježen u našoj književnosti. Svakako zavirite u njegove zbirke „Sunce nad oranicama“, „Dani djetinjstva“, „Svjetiljka ljubavi“, „Sabrane pjesme“ itd. Koju god njegovu zbirku potegnuli s police, nećete požaliti.', 1
END
GO

IF NOT EXISTS (SELECT ALL * FROM [dbo].[Authors] WHERE [FName] = N'Tin' AND [LName] = N'Ujević' AND [BirthDate] = N'18910705') BEGIN
  EXECUTE [dbo].[AuthorCreate] N'Tin', N'Ujević', N'18910705', NULL, N'Ujevića pamtio kao jednog od najaktivnijih hrvatskih pjesnika koji je svoja opažanja o svijetu, borbi pojedinca sa samim sobom, traženju smisla u suvremenome svijetu otuđenih ljudi pretočio u antologijske stihove. Prošao je put od traženja uzora u Matošu pa sve do Baudelairea i barokonih motiva, da bi se najbolje našao upravo u spomenutoj tematici. Oštro je odbijao pisati poeziju u funkciji patetičnog rodoljublja. Njegova najbolja djela mogu se naći u zbirkama „Lelek sebra“, „Kolajna“, „Ojađeno zvono“, „Ljudi za vratima gostionice“ i drugima. Koliko je bio značajan za našu književnosti, govori i to što je danas prema njemu nazvana i najveća pjesnička nagrada.', 1
END
GO

IF NOT EXISTS (SELECT ALL * FROM [dbo].[Authors] WHERE [FName] = N'Ivo' AND [LName] = N'Andrić' AND [BirthDate] = N'18921009') BEGIN
  EXECUTE [dbo].[AuthorCreate] N'Ivo', N'Andrić', N'18921009', NULL, N'Hrvatsko-srpsko-bosanskohercegovački književnik i ujedno jedini nobelovac s ovih prostora Ivo Andrić ušao je u književnost pjesmama objavljenima u „Hrvatskoj mladoj lirici“, čime se odredio kao jedan od posljednjih sljedbenika hrvatske moderne. Teške godine koje je proveo u zatvoru opisao je u svojoj zbirci „Ex ponto“, a nakon konzultantske karijere i završetka Drugog svjetskog rata potpuno se posvetio književnosti. Tada su i nastala njegova najveća djela kao što su „Prokleta avlija“ i povijesni roman „Na Drini ćupriji“, za koji je i primio Nobelovu nagradu. Nemojte propustiti pročitati barem potonji roman.', 1
END
GO

IF NOT EXISTS (SELECT ALL * FROM [dbo].[Authors] WHERE [FName] = N'Ivana' AND [LName] = N'Brlić Mažuranić' AND [BirthDate] = N'18740418') BEGIN
  EXECUTE [dbo].[AuthorCreate] N'Ivana', N'Brlić Mažuranić', N'18740418', NULL, N'Jedna od meni najdražih književnica i ujedno prva hrvatska književnica koja je postala članica JAZU, napisala je prave bisere dječje književnosti. „Priče iz davnine“ i „Čudnovate zgode šegrta Hlapić“ bile su neizostavan dio brojnih djetinjstva. U njihovom je pisanju autorica tražila spas od depresije od koje je bolovala, a ujedno i snažno djelovala na razvoj hrvatske dječje književnosti. Putem dječjih likova progovorila je o odnosima muškaraca i žena te napuštenoj djeci, a koliko je njezin doprinos zaista relevantan, govori i to što naši najbolji književni teoretičari poput Jože Skoka i Stjepana Hranjeca „Hlapića“ nazivaju kamenom temeljcem hrvatske dječje književnosti. Jednaki uspjeh postigle su i “Priče iz davnine”, koje su proglašene najboljom hrvatskom zbirkom umjetničkih bajki svih vremena. Dok u njima kombinira slavensku mitologiju i andersenovske elemente, obrađuje važne teme majčinstva, prirode, žetve, religije, ljudskih odnosa i pronalaska samog sebe koristeći u gotovo svakoj svojoj bajci, baš kao i u “Hlapiću”, element putovanja. No, pisala je i basne i pjesme u koje također vrijedi zaviriti.', 1
END
GO

IF NOT EXISTS (SELECT ALL * FROM [dbo].[Authors] WHERE [FName] = N'Marija' AND [LName] = N'Jurić Zagorka' AND [BirthDate] = N'18730302') BEGIN
  EXECUTE [dbo].[AuthorCreate] N'Marija', N'Jurić Zagorka', N'18730302', NULL, N'Ivanina suvremenica, prva hrvatska novinarka i najčitanija hrvatska književnica svih vremena također se morala nositi s brojnim poteškoćama na svome putu. Dok je Ivana patila od depresije, Marija je nailazila ne brojne prepreke u svome uspinjanju karijernom ljestvicom – i to samo zato što je bila žena. Zato je pokrenula i Ženski list, prvi hrvatski ženski časopis, te neumorno radila na svojim djelima. Pisala je komedije, jednočinke i satire, no najpoznatija je po svojim povijesnim romanima kao što su „Gordana“, „Grička vještica“ (ciklus od sedam romana), „Kći Lotrščaka“, „Kraljica Hrvata“… Iako su se neki književnici poput Matoša i Gjalskog bunili protiv nje i govorili da piše „šund za kravarice“, činjenica je da im samo nije bila po volji jer je žena. Bila je daleko ispred svog vremena i to joj mnogi „velikani“ nisu mogli oprostiti. Budete li u prilici, svakako zavirite u njezin stan na zagrebačkom Dolcu u kojem se sada nalazi Centar za ženske studije koji održava Zagorkinu ostavštinu i utječe na percepciju žena u kulturi i svakodnevnom životu.', 1
END
GO

IF NOT EXISTS (SELECT ALL * FROM [dbo].[Authors] WHERE [FName] = N'Vesna' AND [LName] = N'Parun' AND [BirthDate] = N'19220410') BEGIN
  EXECUTE [dbo].[AuthorCreate] N'Vesna', N'Parun', N'19220410', NULL, N'Pjesnikinja, esejistica i dramska autorica Druge moderne koju se često označavalo kao „žensku“ pjesnikinju, čime ju se opet nastojalo degradirati, svojim je radom pokazala da je itekako jedna od ponajboljih koje je naša književnost imala. Pišući o snažnim osjećajima svojstvenim snažnoj ženi, razmišljanjima o vječnim zagonetkama života i smrti, smislu postojanja, ali i prirodi, razvila je široku lepezu motiva. Ipak, do danas je ostala najupamćenija po svojoj ljubavnoj poeziji, a posebno po pjesmi „Ti koja imaš nevinije ruke“. No, koju god njezinu pjesmu primite u ruke, nećete požaliti.', 1
END
GO

IF NOT EXISTS (SELECT ALL * FROM [dbo].[Authors] WHERE [FName] = N'Kristian' AND [LName] = N'Novak' AND [BirthDate] = N'19981019') BEGIN
  EXECUTE [dbo].[AuthorCreate] N'Kristian', N'Novak', N'19981019', NULL, N'Prvi suvremeni književnik na našoj listi ujedno je i trenutno najčitaniji hrvatski autor. Mnogi ga prozivaju i novim Krležom, iako sam za sebe kaže da se baš nikada ne bi usporedio s njime. Ne samo da niže uspjeh za uspjehom, da za njegove naslove postoje liste čekanja u knjižnicama te da je gotovo nemoguće doći do karata za predstave napravljene prema njegovim romanima, nego se prema njegovim predlošcima snimaju i filmovi i radio-drame. Osim toga, jedan je od najprizemljenijih autora s kojima sam imala priliku razgovarati. No, što je toliko magično u njegovim djelima? Prije svega, Novak piše o onome što i sam poznaje. I u „Črnoj mati zemli“ i u „Ciganinu, ali najljepšem“ aktualizira naš međimurski kraj, svoj zavičaj, te daje realan pogled u kajkavsko društvo i suživot s Romima, bez uljepšavanja. Istodobno nevjerojatno spretno upliće obične ljudske sudbine, ponekad potresne, no uvijek humane. Spretno kombinira različite pripovjedačke tehnike, kajkavštinu i hrvatski standardni jezik. Iza njegovih romana stoje godine istraživanja jer čitateljima ne želi pružiti ništa manje nego ono najbolje što im može dati u danom trenutku. Ako ove godine posegnete makar i za jednom jedinom knjigom, neka to bude Novakova.', 1
END
GO

IF NOT EXISTS (SELECT ALL * FROM [dbo].[Authors] WHERE [FName] = N'Slavenka' AND [LName] = N'Drakulić' AND [BirthDate] = N'19490704') BEGIN
  EXECUTE [dbo].[AuthorCreate] N'Slavenka', N'Drakulić', N'19490704', NULL, N'Popis završavamo jednom od najčitanijih suvremenih književnica, čija su djela prevedena na više od dvadeset jezika. Istaknuta feministica, ujedno i autorica kultne knjige hrvatskog feminizma „Smrtni grijesi feminizma“, često se okreće ženi kao središnjem motivu, kombinirajući fikciju s autobiografskim elementima. No, također poseže i za ratnim motivima, ali i ličnostima slavnih žena. Primjerice, njezini romani „Frida ili o boli“ te najnoviji „Mileva Einstein: teorija tuge“ dat će vam potpuno novi pogled na ove iznimne žene i pokazati koliku snagu krije naizgled krhki ženski duh i tijelo.', 1
END
GO

IF NOT EXISTS (SELECT ALL * FROM [dbo].[Authors] WHERE [FName] = N'Mirko' AND [LName] = N'Ilić' AND [BirthDate] = N'19560301') BEGIN
  EXECUTE [dbo].[AuthorCreate] N'Mirko', N'Ilić', N'19560301', NULL, N'Rođen u Bosni 1956. godine. U Europi je autor ilustrirao i uređivao postere, knjige i stripove, među ostalim. U SAD stigao 1986. te je vrlo brzo angažiran u brojnim uglednim američkim i svjetskim magazinima i novinama. Godine 1991. Time ga imenuje art direktorom zaduženim za svoja međunarodna izdanja, a 1992. postaje art direktor Op-Ed stranica New York Timesa. Godine 1995. osnovao je studio za grafički dizajn i 3-D računalnu grafiku i filmske špice, Mirko Ilić Corp. Primio je  nagrade sljedećih udruženja: Society of Illustrators, Society of Publication Designers, Art Directors Club, I.D. Magazine, Society of Newspaper Design, i drugih. Njegovi radovi prisutni su u zbirkama institucija poput Smithsonian Museum, SFMOMA u San Franciscu, a MoMA u New Yorku posjeduje 38 njegovih dizajnerskih djela u svojoj kolekciji. Držao je naprednu nastavu dizajna na Cooper Unionu s Miltonom Glaserom, a predaje ilustraciju polaznicima magistarskih studija School of Visual Arts u New Yorku. Također organizira i kurira izložbe te predaje širom svijeta. Najpoznatija od ovih izložbi je Tolerance Poster Show, postavljena više od sto puta u zemljama diljem svijeta. N', 1
END
GO

IF NOT EXISTS (SELECT ALL * FROM [dbo].[Authors] WHERE [FName] = N'Rujana' AND [LName] = N'Jeger' AND [BirthDate] = N'19681101') BEGIN
  EXECUTE [dbo].[AuthorCreate] N'Rujana', N'Jeger', N'19681101', NULL, N'Rujana Jeger rođena je kao Rujana Drakulić 1968. u Zagrebu, gdje je pohađala osnovnu i srednju školu. Srednjoškolsko obrazovanje stekla je u Centru za obrazovanje u kulturi i umjetnosti, gdje je stekla zvanje "suradnika u sredstvima javnog informiranja". Diplomirala je arheologiju na zagrebačkom Filozofskom fakultetu 1996. Godine 1991. otišla je živjeti u Beč, gdje je radila razne poslove, od prevodilačkih, preko novinarskih, do arheoloških. Godinama je bila stalna kolumnistica časopisa Cosmopolitan, Elle, Ženska posla, Stilist i Storybook, a objavljivala je i u švedskom časopisu Amelia, švicarskom Du i austrijskom Die Bunte Zeitung. Nekoliko godina pisala je blog Sexomat na tportalu. Profesionalno se bavi teorijskim radovima na području arheologije domestikacije psa, 2019./2020. uređivala je časopis o kućnim ljubimcima Njuškica, a od 2013. djeluje kao urednica vlastitog edukativnog portala o psima i ljudima Pasji život. Trenutačno je kolumnistica i stalna suradnica časopisa Storybook i Elle.', 1
END
GO

IF NOT EXISTS (SELECT ALL * FROM [dbo].[Authors] WHERE [FName] = N'Kristijan' AND [LName] = N'Vujičić' AND [BirthDate] = N'19731008') BEGIN
  EXECUTE [dbo].[AuthorCreate] N'Kristijan', N'Vujičić', N'19731008', NULL, N'Kristijan Vujičić rođen je 1973. u Zagrebu gdje je završio osnovnu i srednju školu te diplomirao filozofiju i religiologiju na Zagrebačkom sveučilištu. Objavljivao je prozu i recenzije u više časopisa za kulturu i dnevnih listova. Radi kao freelance urednik. Usto već desetak godina uređuje i vodi tribine društveno-humanističke tematike pod nazivom “Paralelni svjetovi” u zagrebačkoj Knjižnici i čitaonici Bogdana Ogrizovića. Godine 2017. primio je prvu nagradu na 51. natječaju Večernjega lista “Ranko Marinković” za kratku priču “Zid mora pasti”. Dvaput je dobio Nagradu Kiklop: 2006. za roman Welcome to Croatia i 2014. za urednika godine. Dosad je objavio pet romana: Welcome to Croatia: doživljaji jednog turističkog vodiča (napisan u suradnji sa Željkom Špoljarom, Nagrada Kiklop 2006.), Gospodin Bezimeni (uži izbor za Nagradu “Fran Galović” 2007.), Udruženje za mravlje igre (uži izbor za Nagradu SFERA 2009.), Ponavljanje: zaigranost proljeća i života (polufinalist nagrade Tportala 2012. za roman godine) i Knjiga izlazaka: Izvještaj Odjela za ćudoređe (uži izbor za Nagradu Ksaver Šandor Gjalski 2016.).', 1
END
GO

IF NOT EXISTS (SELECT ALL * FROM [dbo].[Authors] WHERE [FName] = N'Ivica' AND [LName] = N'Đikić' AND [BirthDate] = N'19770303') BEGIN
  EXECUTE [dbo].[AuthorCreate] N'Ivica', N'Đikić', N'19770303', NULL, N'Ivica Đikić rođen je 1977. u Tomislavgradu (BiH). Novinarstvom se profesionalno počeo baviti 1994. u Slobodnoj Dalmaciji, a u tjedniku Feral Tribune bio je novinar i urednik od 1997. do gašenja lista 2008. Od 2009. do 2010. bio je glavni urednik riječkog Novog lista, a od 2010. do 2016. glavni urednik tjednika Novosti iz Zagreba, nakon čega je nastavio pisati u tom listu. Objavio je romane Cirkus Columbia (Biblioteka Feral Tribune, Split, 2003.), Sanjao sam slonove (Naklada Ljevak, Zagreb, 2011.), Ponavljanje (Naklada Ljevak, 2014.), Beara – dokumentarni roman o genocidu u Srebrenici (Naklada Ljevak, 2016.) i Ukazanje (Fraktura, Zaprešić, 2018.). Cirkus Columbia dobio je nagradu Meša Selimović za najbolji roman objavljen u Hrvatskoj, Srbiji, BiH i Crnoj Gori. Po motivima tog romana oskarovac Danis Tanović snimio je 2010. istoimeni igrani film. Roman Sanjao sam slonove osvojio je nagradu Tportala za roman godine u Hrvatskoj, Ponavljanje je nagrađeno Kočićevim perom (Banja Luka – Beograd), a Beara priznanjem Krunoslav Sukić Centra za mir, nenasilje i ljudska prava iz Osijeka.', 1
END
GO

IF NOT EXISTS (SELECT ALL * FROM [dbo].[Authors] WHERE [FName] = N'Mira' AND [LName] = N'Furlan' AND [BirthDate] = N'19550907') BEGIN
  EXECUTE [dbo].[AuthorCreate] N'Mira', N'Furlan', N'19550907', NULL, N'Mira Furlan rodila se u Zagrebu 7. rujna 1955., a preminula je u Los Angelesu 20. siječnja 2021. godine. U Zagrebu je završila osnovnu školu i XVI. gimnaziju u Mesićevoj ulici te je diplomirala na Akademiji dramskih umjetnosti u Zagrebu 1978. Kao najtalentiranija glumica u klasi već za vrijeme studija počela je dobivati uloge u raznim zagrebačkim kazalištima i u filmovima, a ubrzo je dobila i stalni angažman u zagrebačkom HNK-u. Slavu je stekla ulogom Kate u seriji Velo misto. Nedugo zatim postala je jedna od najtraženijih glumica u Jugoslaviji te snimila brojne filmove i TV serije. Nagrađena je Zlatnom arenom za ulogu u filmu Kiklop 1983. i za najbolju glumicu 1986. u filmu Lepota poroka. Svjetska ju je javnost upoznala ulogom u filmu Otac na službenom putu. U drugoj polovini 1980-ih imala je brojne angažmane diljem Jugoslavije, posebno u zagrebačkim i beogradskim kazalištima, te živjela između dva grada, a 1990. dobila je najvažniju hrvatsku kazališnu nagradu Dubravko Dujšin. Svojim angažmanom nije se uklapala u poželjnu sliku rastućih nacionalizama, te je optužena da je izdajica, a početkom rata doživjela je rijetko viđen medijski napad, politički i institucionalni progon. Sa suprugom redateljem Goranom Gajićem 1991. odlazi u emigraciju u Sjedinjene Američke Države, gdje je ostvarila zavidnu glumačku karijeru, a posebno se istaknula ulogama u serijama Babylon 5 i Izgubljeni. Glumila je u brojnim američkim predstavama i filmovima te predavala na New York Film Academy. Od početka novog tisućljeća glumila je i u filmovima i kazalištima u zemljama bivše Jugoslavije. Bila je angažirana i u riječkom HNK-u, gdje je odigrala i svoju posljednju ulogu, u predstavi Vježbanje života – drugi put. Autorica je drame Dok nas smrt ne razdvoji i knjige kolumni koje je objavljivala u Feral Tribuneu Totalna rasprodaja.', 1
END
GO

IF NOT EXISTS (SELECT ALL * FROM [dbo].[Authors] WHERE [FName] = N'David' AND [LName] = N'Grossman' AND [BirthDate] = N'19540307') BEGIN
  EXECUTE [dbo].[AuthorCreate] N'David', N'Grossman', N'19540307', NULL, N'David Grossman rođen je u Jeruzalemu 1954. Autor je mnogobrojnih beletrističkih i publicističkih djela te knjiga za djecu, prevedenih na trideset i šest jezika u čitavom svijetu. Primio je mnoge nagrade, uključujući francusku Chevalier de l’Ordre des Arts et des Lettres, njemačku Buxtehuder Bulle, rimsku Premio per la Pace e l`Azione Umanitaria, Premio Ischia – međunarodnu nagradu za novinarstvo, izraelsku nagradu Emet i nagradu Albatros Fondacije Güntera Grassa. Uz književni rad istaknuti je mirotvorni aktivist. Za svoj roman Ušao konj u bar 2017. godine primio je međunarodnu nagradu Man Booker te 2018. nagradu Države Izraela za književno stvaralaštvo.', 1
END
GO

-- PUBLISHERS

IF NOT EXISTS (SELECT ALL * FROM [dbo].[Publishers] WHERE [Name] = N'Alfa') BEGIN
  EXECUTE [dbo].[PublisherCreate] N'Alfa', 1
END
GO

IF NOT EXISTS (SELECT ALL * FROM [dbo].[Publishers] WHERE [Name] = N'Globus') BEGIN
  EXECUTE [dbo].[PublisherCreate] N'Globus', 1
END
GO

IF NOT EXISTS (SELECT ALL * FROM [dbo].[Publishers] WHERE [Name] = N'Hrvatska akademija znanosti i umjetnosti') BEGIN
  EXECUTE [dbo].[PublisherCreate] N'Hrvatska akademija znanosti i umjetnosti', 1
END
GO

IF NOT EXISTS (SELECT ALL * FROM [dbo].[Publishers] WHERE [Name] = N'Matica hrvatska') BEGIN
  EXECUTE [dbo].[PublisherCreate] N'Matica hrvatska', 1
END
GO

IF NOT EXISTS (SELECT ALL * FROM [dbo].[Publishers] WHERE [Name] = N'Školska knjiga') BEGIN
  EXECUTE [dbo].[PublisherCreate] N'Školska knjiga', 1
END
GO

IF NOT EXISTS (SELECT ALL * FROM [dbo].[Publishers] WHERE [Name] = N'Večernji list') BEGIN
  EXECUTE [dbo].[PublisherCreate] N'Večernji list', 1
END
GO

IF NOT EXISTS (SELECT ALL * FROM [dbo].[Publishers] WHERE [Name] = N'Znanje') BEGIN
  EXECUTE [dbo].[PublisherCreate] N'Znanje', 1
END
GO

IF NOT EXISTS (SELECT ALL * FROM [dbo].[Publishers] WHERE [Name] = N'Vjesnik') BEGIN
  EXECUTE [dbo].[PublisherCreate] N'Vjesnik', 1
END
GO

IF NOT EXISTS (SELECT ALL * FROM [dbo].[Publishers] WHERE [Name] = N'Mozaik') BEGIN
  EXECUTE [dbo].[PublisherCreate] N'Mozaik', 1
END
GO

IF NOT EXISTS (SELECT ALL * FROM [dbo].[Publishers] WHERE [Name] = N'Nova knjiga') BEGIN
  EXECUTE [dbo].[PublisherCreate] N'Nova knjiga', 1
END
GO

IF NOT EXISTS (SELECT ALL * FROM [dbo].[Publishers] WHERE [Name] = N'Mladost') BEGIN
  EXECUTE [dbo].[PublisherCreate] N'Mladost', 1
END
GO

-- BOOKS

IF NOT EXISTS (SELECT ALL * FROM [dbo].[Books] WHERE [ISBN] = N'978-953-0-60095-9') BEGIN
  INSERT INTO [dbo].[Books]
          ([CreatedBy]
          ,[UpdatedBy]
          ,[ISBN]
          ,[Title]
          ,[Summary]
          ,[Description]
          ,[IsNew]
          ,[PublisherFK]
          ,[AuthorFK]
          ,[PageCount]
          ,[PurchasePrice]
          ,[LoanPrice]
          ,[Quantity]
          ,[ImagePath])
  VALUES
        (1
        ,1
        ,N'978-953-0-60095-9'
        ,N'Posljednji Stipančići'
        ,NULL
        ,N'U Posljednjim Stipančićima, jednom od najboljih i najznačajnih romana hrvatskog realizma, Vjenceslav Novak s jedne strane opisuje društveni i javni život Senja u prvoj polovici XIX. stoljeća, koji obilježavaju propadanje pomorske trgovine, klasna i politička previranja te osobito rađanje ilirskih ideja, a s druge privatni život obitelji Stipančić, s naglaskom na muško-ženske odnose. Politički život Senja prikazan je s gledišta muškarca, Ante Stipančića, a privatni sa ženskoga gledišta, koje dijele majka i kći, Valpurga i Lucija Stipančić.'
        ,1
        ,9
        ,1
        ,208
        ,99.00
        ,10.00
        ,30
        ,NULL)
END
GO

IF NOT EXISTS (SELECT ALL * FROM [dbo].[Books] WHERE [ISBN] = N'978-953-0-62162-6') BEGIN
  INSERT INTO [dbo].[Books]
        ([CreatedBy]
        ,[UpdatedBy]
        ,[ISBN]
        ,[Title]
        ,[Summary]
        ,[Description]
        ,[IsNew]
        ,[PublisherFK]
        ,[AuthorFK]
        ,[PageCount]
        ,[PurchasePrice]
        ,[LoanPrice]
        ,[Quantity]
        ,[ImagePath])
  VALUES
        (1
        ,1
        ,N'978-953-0-62162-6'
        ,N'Gospoda Glembajevi'
        ,NULL
        ,N'Drama je podijeljena u 3 čina, a bavi se događanjima i raskolom unutar obitelji Glembay. Gospoda Glembajevi je prva od tri drame iz glembajevskog ciklusa u koji spadaju i drame U agoniji i Leda. Radnja se zbiva u Zagrebu, u kući Ignjata Glembaja u noći nakon proslave jubileja banke Glembay Ltd. 1913. godine.'
        ,1
        ,10
        ,1
        ,164
        ,40.00
        ,4.00
        ,5
        ,NULL)
END
GO

IF NOT EXISTS (SELECT ALL * FROM [dbo].[Books] WHERE [ISBN] = N'978-953-0-62227-2') BEGIN
  INSERT INTO [dbo].[Books]
        ([CreatedBy]
        ,[UpdatedBy]
        ,[ISBN]
        ,[Title]
        ,[Summary]
        ,[Description]
        ,[IsNew]
        ,[PublisherFK]
        ,[AuthorFK]
        ,[PageCount]
        ,[PurchasePrice]
        ,[LoanPrice]
        ,[Quantity]
        ,[ImagePath])
  VALUES
        (1
        ,1
        ,N'978-953-0-62227-2'
        ,N'Čudnovate zgode šegrta Hlapića'
        ,NULL
        ,N'Hlapić je bio dječak koji je radio kao šegrt kod majstora Mrkonje i njegove žene. Majstor se prema njemu nije lijepo odnosio, pa je Hlapić odlučio otići. Na svom putu 6 dana i 7 noći, upoznao je mnogo zanimljivih ljudi i životinja, a i zaljubio se u Gitu koja je tražila svoje roditelje. Usput, upoznao je starog štakora koji je sa svojim prijateljem ktao po selu, a Hlapiću već prvu noć ukrao čizmice. Na kraju putovanja, Hlapić je odveo Gitu svojoj kući majstoru i majstorici, za koje se na kraju saznalo da su Gitini pravi roditelji.'
        ,1
        ,9
        ,6
        ,150
        ,79.90
        ,9.00
        ,15
        ,NULL)
END
GO

IF NOT EXISTS (SELECT ALL * FROM [dbo].[Books] WHERE [ISBN] = N'978-953-8017-18-6') BEGIN
  INSERT INTO [dbo].[Books]
        ([CreatedBy]
        ,[UpdatedBy]
        ,[ISBN]
        ,[Title]
        ,[Summary]
        ,[Description]
        ,[IsNew]
        ,[PublisherFK]
        ,[AuthorFK]
        ,[PageCount]
        ,[PurchasePrice]
        ,[LoanPrice]
        ,[Quantity]
        ,[ImagePath])
  VALUES
        (1
        ,1
        ,N'978-953-8017-18-6'
        ,N'Gordana - kraljica Hrvata, 12 svezaka'
        ,N'U dvanaest svesaka povijesna i ljubavna priča o srednjovjekovnoj Hrvatskoj Zagorkina magična sposobnost da poput Šeherezade iz Tisuću i jedne noći neprestance izmišlja nove priče, da u njih uvodi nove junake, smišlja nove epizode kroz koje defiliraju lica odjevena u blistave kostime, snažnija je od čitateljeva racionalnog poimanja stvarnih povijesnih činjenica. U Gordani-kraljici Hrvata čitatelji se susreću s pokušajem sentimentalnoga i iracionalnog opravdanja i objašnjenja hrvatske povijesti. Pisana u tradiciji feljton-romana, Gordana-kraljica Hrvata pokazuje, možda više nego njezina ostala djela, težnju da se uhvati u koštac s velikim temama nacionalne povijesti. Jedan je lik prikazan u kontrapunktu s povijesnim zbivanjima u rasponu od prvih godina vladavine Matijaša Korvina (1458.-1490.) pa do bitke na Mohačkom polju (1526.). Lijepa i odvažna žena biva neprestano razapinjana između svete dužnosti prema domovini i osobnih osjećaja te je tako upletena u spletke oko nasljeđa, nestanka kraljevske loze, stranih političkih interesa. Njezina se sudbina u Zagorkinoj romanesknoj obradi pretvara u zrcalo u kojemu se odražavaju običaji epohe, putovi povijesnih zbivanja, konfrontiraju želje i nade pobjednika i pobjeđenih. (Iz pogovora Branimira Donata)'
        ,N'Ovaj je ciklus povijesnih romana najopsežnije djelo Marije Jurić Zagorke. U napetoj radnji smještenoj u 15. stoljeće, u doba hrvatsko-ugarskoga kralja Matije Korvina, glavna junakinja, Gordana, stoji pred teškim izazovom. Razapeta između osjećaja dužnosti prema domovini i prepuštanja vlastitim osjećajima, mora odlučiti: ljubav ili politika. Inspirirana realnim povijesnim okvirom, spisateljica uvlači čitatelja u svijet intriga i igre političkih i ljubavnih interesa. Razgranate priče nadilaze očekivanja i pretpostavke.'
        ,1
        ,5
        ,7
        ,8768
        ,500.00
        ,60.00
        ,0
        ,NULL)
END
GO

IF NOT EXISTS (SELECT ALL * FROM [dbo].[Books] WHERE [ISBN] = N'978-953-8017-17-9') BEGIN
  INSERT INTO [dbo].[Books]
        ([CreatedBy]
        ,[UpdatedBy]
        ,[ISBN]
        ,[Title]
        ,[Summary]
        ,[Description]
        ,[IsNew]
        ,[PublisherFK]
        ,[AuthorFK]
        ,[PageCount]
        ,[PurchasePrice]
        ,[LoanPrice]
        ,[Quantity]
        ,[ImagePath])
  VALUES
        (1
        ,1
        ,N'978-953-8017-17-9'
        ,N'Moj prijatelj Šišmiš'
        ,NULL
        ,N'Izabrane dječje pjesme'
        ,0
        ,11
        ,8
        ,172
        ,40.00
        ,10.00
        ,14
        ,NULL)
END
GO

IF NOT EXISTS (SELECT ALL * FROM [dbo].[Books] WHERE [ISBN] = N'978-953-0-51777-6') BEGIN
  INSERT INTO [dbo].[Books]
        ([CreatedBy]
        ,[UpdatedBy]
        ,[ISBN]
        ,[Title]
        ,[Summary]
        ,[Description]
        ,[IsNew]
        ,[PublisherFK]
        ,[AuthorFK]
        ,[PageCount]
        ,[PurchasePrice]
        ,[LoanPrice]
        ,[Quantity]
        ,[ImagePath])
  VALUES
        (1
        ,1
        ,N'978-953-0-51777-6'
        ,N'Priče iz davnine'
        ,NULL
        ,N'Ove bajke autorici su donijele svjetsku slavu te je čak bila predložena za Nobelovu nagradu. U njima je autorica ispričala svoje umjetničke bajke u kojima je svojom maštom kombinirala realne i nestvarne događaje.'
        ,0
        ,3
        ,6
        ,265
        ,107.10
        ,25.10
        ,3
        ,NULL)
END
GO

IF NOT EXISTS (SELECT ALL * FROM [dbo].[Books] WHERE [ISBN] = N'978-953-0-40606-3') BEGIN
  INSERT INTO [dbo].[Books]
        ([CreatedBy]
        ,[UpdatedBy]
        ,[ISBN]
        ,[Title]
        ,[Summary]
        ,[Description]
        ,[IsNew]
        ,[PublisherFK]
        ,[AuthorFK]
        ,[PageCount]
        ,[PurchasePrice]
        ,[LoanPrice]
        ,[Quantity]
        ,[ImagePath])
  VALUES
        (1
        ,1
        ,N'978-953-0-40606-3'
        ,N'Šuma Striborova'
        ,N'U šumu Striborovu zašao je mladić kako bi nasjekao drva za ogrjev. Dok je sjedio na panju ugledao je prekrasnu zmiju, koja je na suncu izgledala baš poput srebra. Mladić ju je zbog njene ljepote poželio odmah odnijeti kući. Nije prošlo puno i zmija se odjednom pretvori u prekrasnu djevojku koju je mladić odlučio oženiti. Djevojku je poveo svojoj kući gdje ih je dočekala mladićeva majka. Nije prošlo dugo i majka je u ustima djevojke primijetila zmijski jezik. Odmah je upozorila sina da se pazi jer je ta djevojka zla, ali on opijen ljepotom djeve nije htio slušati majku.'
        ,N'Zašao neki momak u šumu Striborovu, a nije znao da je ono šuma začarana i da se u njoj svakojaka čuda zbivaju. Zbivala se u njoj čuda dobra, ali i naopaka - svakome po zasluzi. Morala je pak ta šuma ostati začarana, doklegod u nju ne stupi onaj, kojemu je milija njegova nevolja, nego sva sreća ovoga svijeta.'
        ,0
        ,3
        ,6
        ,31
        ,44.10
        ,13.10
        ,1
        ,NULL)
END
GO

IF NOT EXISTS (SELECT ALL * FROM [dbo].[Books] WHERE [ISBN] = N'978-953-0-61799-5') BEGIN
  INSERT INTO [dbo].[Books]
        ([CreatedBy]
        ,[UpdatedBy]
        ,[ISBN]
        ,[Title]
        ,[Summary]
        ,[Description]
        ,[IsNew]
        ,[PublisherFK]
        ,[AuthorFK]
        ,[PageCount]
        ,[PurchasePrice]
        ,[LoanPrice]
        ,[Quantity]
        ,[ImagePath])
  VALUES
        (1
        ,1
        ,N'978-953-0-61799-5'
        ,N'Regoč'
        ,NULL
        ,N'Regoč je priča jedne od naših najpoznatijih književnica za djecu, Ivane Brlić-Mažuranić. Priča se nalazi u njenoj najpoznatijoj zbirci bajki Priče iz davnine, prvi put objavljene 1916. godine. Priča Regoč, poput većine bajki iz ove zbirke, inspirirana je slavenskom mitologijom. Mitološko biće, div Regoč staro je vjerovanje Hrvata još iz pretkršćanskih vremena, te se u našoj kulturi dugo vremena kroz povijest prenosila usmenom predajom.'
        ,1
        ,2
        ,4
        ,40
        ,44.10
        ,13.10
        ,11
        ,NULL)
END
GO

IF NOT EXISTS (SELECT ALL * FROM [dbo].[Books] WHERE [ISBN] = N'978-953-342-081-3') BEGIN
    INSERT INTO [dbo].[Books]
        ([CreatedBy]
        ,[UpdatedBy]
        ,[ISBN]
        ,[Title]
        ,[Summary]
        ,[Description]
        ,[IsNew]
        ,[PublisherFK]
        ,[AuthorFK]
        ,[PageCount]
        ,[PurchasePrice]
        ,[LoanPrice]
        ,[Quantity]
        ,[ImagePath])
  VALUES
        (1
        ,1
        ,N'978-953-342-081-3'
        ,N'U kavani Europa: Život poslije socijalizma'
        ,NULL
        ,N'Nakon što je u knjizi Kako smo preživjeli opisala iskustvo života u socijalizmu, Slavenka Drakulić u esejima U kavani Europa: život poslije socijalizma, analizirajući niz svakodnevnih, životnih detalja, odgovara na pitanje kako je tekla preobrazba društva iz socijalizma u nove demokracije. Ovi će vas tekstovi podsjetiti da su u Europi nekada postojale granice, da se u bivšoj Jugoslaviji živjelo drugačije, slobodnije nego u nizu drugih socijalističkih zemalja, da je većina ljudi na socijalističkim prostorima imala zapuštene zube, a moći ćete promisliti i o ulozi uniforme na prostoru Balkana. Susrest ćete se u ovoj knjizi s “običnim” ljudima koje je tranzicija razočarala jer su očekivali blagostanje koje mnogima nije došlo, a morali su se suočiti i s činjenicom da veća sloboda podrazumijeva i veću odgovornost. Ako vas zanima kako su se promjene u Istočnoj i Zapadnoj Europi nastavile, pročitajte i upravo objavljene eseje Ponovo u kavani Europa: kako preživjeti postsocijalizam koji su svojevrsni nastavak knjige U kavani Europa, prvi put objavljene 1996. kod američkog izdavača i prevedene na brojne svjetske jezike. Povijest se ne sastoji samo od prašnjavih knjiga i suhih činjenica i godina, čini je i svakodnevica ljudi koji su živjeli prije nas, a na to nas Slavenka Drakulić stalno podsjeća, jasno ukazujući da trivijalno zaista jest političko.'
        ,1
        ,5
        ,10
        ,208
        ,125.10
        ,33.10
        ,7
        ,NULL)
END
GO

IF NOT EXISTS (SELECT ALL * FROM [dbo].[Books] WHERE [ISBN] = N'978-953-342-022-6') BEGIN
  INSERT INTO [dbo].[Books]
        ([CreatedBy]
        ,[UpdatedBy]
        ,[ISBN]
        ,[Title]
        ,[Summary]
        ,[Description]
        ,[IsNew]
        ,[PublisherFK]
        ,[AuthorFK]
        ,[PageCount]
        ,[PurchasePrice]
        ,[LoanPrice]
        ,[Quantity]
        ,[ImagePath])
  VALUES
        (1
        ,1
        ,N'978-953-342-022-6'
        ,N'Ponovo u kavani Europa: Kako preživjeti postsocijalizam'
        ,NULL
        ,N'Prije trideset godina Istočnu Europu zatekle su velike promjene. Od prijelaza iz socijalizma u nove demokracije ili tzv. tranzicije očekivalo se mnogo, ne samo instant-kava i mnoštvo luksuznih proizvoda koji su preplavili samoposluge nego i instant-bogaćenje – ili barem bolji život. O tome je Slavenka Drakulić elaborirano pisala prije dvadeset pet godina u vrlo uspješnoj knjizi U kavani Europa: život poslije socijalizma koja svoje prvo hrvatsko samostalno izdanje ima ovih dana. U zbirci eseja Ponovo u kavani Europa: kako preživjeti postsocijalizam Slavenka Drakulić pita se što se od toga ostvarilo i jesu li građani Istočne Europe računali da sa slobodama dolaze i velike odgovornosti. Financijska kriza, masovna imigracija, rast nacionalizma, ksenofobije i Brexit samo su dio slike današnje, još uvijek podijeljene Europe. Smanjuju li se ili produbljuju razlike između Istoka i Zapada Europe? Zbirka eseja Ponovo u kavani Europa, prvobitno objavljena na engleskom jeziku, opisuje niz svakodnevnih situacija koje ukazuju na širu političku i društvenu situaciju u današnjoj Europi. Esej o papagaju iz Stockholma ponukat će vas da se zamislite imaju li životinje ponekad veća prava od ljudi, a priča o razlici u prehrambenim proizvodima u Istočnoj i Zapadnoj Europi da opet probate Nutellu, voćni jogurt ili riblje štapiće. Eseji o autoričinoj najdražoj kartici i onaj o imigrantskom orkestru s Trga Vittorio u Rimu mogu se čitati i kao vapaj za nekom novom europskom solidarnošću.'
        ,1
        ,5
        ,10
        ,256
        ,134.10
        ,35.10
        ,3
        ,NULL)
END
GO

IF NOT EXISTS (SELECT ALL * FROM [dbo].[Books] WHERE [ISBN] = N'978-953-342-038-7') BEGIN
  INSERT INTO [dbo].[Books]
        ([CreatedBy]
        ,[UpdatedBy]
        ,[ISBN]
        ,[Title]
        ,[Summary]
        ,[Description]
        ,[IsNew]
        ,[PublisherFK]
        ,[AuthorFK]
        ,[PageCount]
        ,[PurchasePrice]
        ,[LoanPrice]
        ,[Quantity]
        ,[ImagePath])
  VALUES
        (1
        ,1
        ,N'978-953-342-038-7'
        ,N'Smrtni grijesi feminizma: Ogledi o mudologiji'
        ,NULL
        ,N'Kada je 1984. objavljena u kultnoj biblioteci &td, prva knjiga Slavenke Drakulić Smrtni grijesi feminizma izazvala je buru u javnosti, a ta se bura, za divno čudo, nije stišala do dana današnjeg. Ponovno objavljivanje ovih tekstova koji su nastajali krajem sedamdesetih i početkom osamdesetih godina prošlog stoljeća pokazuje koliko su teme o kojima je pisala jedna od pionirki feminizma na ovim prostorima i dalje bitne, čak nezaobilazne. Kada tome dodamo novih dvadesetak tekstova objavljivanih od prvog izdanja knjige do danas, bjelodano je da Slavenka Drakulić ne samo da se konstantno bavi feminističkim temama već da im i dalje pristupa jednako oštro, beskompromisno i aktualno. Nasilje nad ženama ne jenjava, reproduktivna ženska prava ponovno su ugrožena, djevojčicama kroz odgoj i obrazovanje šaljemo drugačije poruke nego dječacima, ukratko, patrijarhat je žilav. Stoga je pitanje ravnopravnog položaja žene u društvu i obrane njezinih prava vječno aktualno, čini se kao da ga svaka generacija žena mora ponovno izboriti, samo je pitanje jesu li mlade žene toga svjesne. Smrtni grijesi feminizma u ovom proširenom izdanju idealni su prilog tom osvještavanju. Pokazuje se ne samo da su eseji Slavenke Drakulić itekako aktualni već i da se bez njih ne može ni početi misliti o feminizmu jučer, danas i sutra.'
        ,0
        ,5
        ,10
        ,400
        ,134.10
        ,38.10
        ,1
        ,NULL)
END
GO

IF NOT EXISTS (SELECT ALL * FROM [dbo].[Books] WHERE [ISBN] = N'978-953-0-60723-1') BEGIN
  INSERT INTO [dbo].[Books]
        ([CreatedBy]
        ,[UpdatedBy]
        ,[ISBN]
        ,[Title]
        ,[Summary]
        ,[Description]
        ,[IsNew]
        ,[PublisherFK]
        ,[AuthorFK]
        ,[PageCount]
        ,[PurchasePrice]
        ,[LoanPrice]
        ,[Quantity]
        ,[ImagePath])
  VALUES
        (1
        ,1
        ,N'978-953-0-60723-1'
        ,N'Moje mjesto pod suncem'
        ,NULL
        ,N'“Inspirativna ideja, malo razgovora i trud da se uskladi crtež i riječi– i eto slikovnice Moje mjesto pod suncem”, riječi su autora Mirka Ilića, Slavenke Drakulić i Rujane Jeger. “Moje mjesto pod suncem” je program socijalnog uključivanja namijenjen djeci i roditeljima iz obitelji koje žive u lošijim socijalnim i ekonomskim uvjetima. Cilj programa je prekinuti krug siromaštva u kojemu se djeca nalaze time što će im program omogućiti aktivnosti koje su nužne za njihov razvoj, a koje im njihovi roditelji ne mogu priuštiti. Program provodi udruga Centar za kulturu dijaloga (CeKaDe) iz Rijeke u suradnji s mnogobrojnim volonterima i donatorima. U  listopadu 2020. godine udruga je započela s nacionalnom donatorskom kampanjom s ciljem sakupljanja sredstava za uređenje prostora za rad s djecom ispod ruba siromaštva. Prostor je ustupilo Sveučilište u Rijeci, a cilj je sakupiti 1,2 milijuna kuna za potpunu adaptaciju i uređenje prostora od 380 kvadratnih metara. Kampanji se pridružio i poznati hrvatsko-američki dizajner Mirko Ilić, koji je autor loga kampanje te koji je, zajedno sa Slavenkom Drakulić i Rujanom Jeger, pripremio slikovnicu Moje mjesto pod suncem, s ciljem sakupljanja sredstava za kampanju. Kampanji se pridružila i naša nova izdavačka kuća Bodoni. Sav prihod od prodaje knjige namijenjen je kampanji “Moje mjesto pod suncem”.'
        ,1
        ,5
        ,10
        ,26
        ,59.00
        ,10.10
        ,25
        ,NULL)
END
GO

IF NOT EXISTS (SELECT ALL * FROM [dbo].[Books] WHERE [ISBN] = N'978-953-342-040-0') BEGIN
  INSERT INTO [dbo].[Books]
        ([CreatedBy]
        ,[UpdatedBy]
        ,[ISBN]
        ,[Title]
        ,[Summary]
        ,[Description]
        ,[IsNew]
        ,[PublisherFK]
        ,[AuthorFK]
        ,[PageCount]
        ,[PurchasePrice]
        ,[LoanPrice]
        ,[Quantity]
        ,[ImagePath])
  VALUES
        (1
        ,1
        ,N'978-953-342-040-0'
        ,N'Mileva Einstein, teorija tuge'
        ,N'Kada u osvit Prvoga svjetskoga rata, tek što je sa sinovima došla u Berlin, gdje je njezin suprug, najslavniji znanstvenik dvadesetog stoljeća Albert Einstein upravo dobio posao, primi pismo s Uvjetima o njihovu međusobnom životu, Milevi Einstein ionako napuknut život sasvim se izokrene. Ostaje sama, s dvojicom sinova, s golemim teretom tuge koju nosi u sebi, sa svim padovima i slomovima koje je imala u životu i koje će tek doživjeti.'
        ,N'Osoba koja je bila predodređena za najveće znanstvene dosege, genijalna matematičarka, prva žena na politehničkom fakultetu Sveučilišta u Zürichu, uvijek druga i drugačija, Mileva Einstein rođena Marić, iz bogate vojvođanske obitelji, u svojoj je sudbini gotovo paradigmatična kada razmišljamo o položaju žena. Njezina teorija tuge teška je poput bilo koje teorije relativnosti, ali i mnogo češća od nje. Prtljaga tuge koju je nosila od najranije mladosti do smrti, u kojoj su se tijekom vremena samo dodavali tegobni trenuci, od šepanja i ruganja djece, preko ljubavi i neuspjelog braka s Albertom, do smrti djeteta, bolesne sestre, nezavršenog fakulteta, loše materijalne situacije i teške bolesti najmlađega sina, teret je koji može razumjeti svaka čitateljica ili čitatelj.'
        ,0
        ,5
        ,10
        ,224
        ,116.10
        ,33.10
        ,4
        ,NULL)
END
GO

IF NOT EXISTS (SELECT ALL * FROM [dbo].[Books] WHERE [ISBN] = N'978-953-0-62193-0') BEGIN
  INSERT INTO [dbo].[Books]
        ([CreatedBy]
        ,[UpdatedBy]
        ,[ISBN]
        ,[Title]
        ,[Summary]
        ,[Description]
        ,[IsNew]
        ,[PublisherFK]
        ,[AuthorFK]
        ,[PageCount]
        ,[PurchasePrice]
        ,[LoanPrice]
        ,[Quantity]
        ,[ImagePath])
  VALUES
        (1
        ,1
        ,N'978-953-0-62193-0'
        ,N'Kad su padali zidovi'
        ,NULL
        ,N'Konac je 1980-ih, vrijeme kada Zagreb izranja iz sna, klubovi su prepuni, a noć se čini još itekako mlada. Dok velikani svjetske glazbe dolaze održati koncerte i demokracija kuca na vrata, budućnost se, barem većini, čini sretna i dobra. Istovremeno se osjećaju i promjene u zraku, koji se čak kao čisti prodaje u konzervama. Crni, glavni junak romana Kad su padali zidovi, mladi je nadobudan dečko koji voli filozofiju i rock `n` roll, odrasta u pomalo nesređenim obiteljskim odnosima u centru grada i asfalt je njegov drugi dom. On i njegovi prijatelji na kraju srednjoškolskog obrazovanja razmišljaju o djevojkama i o tome kako će promijeniti svijet.'
        ,1
        ,5
        ,13
        ,256
        ,125.10
        ,35.10
        ,6
        ,NULL)
END
GO

--IF NOT EXISTS (SELECT ALL * FROM [dbo].[Books] WHERE [ISBN] = N'9789531323307') BEGIN
--  EXECUTE [dbo].[BookCreate] N'9789531323307', N'Štapići za pričanje', NULL, N'Udba, politička emigracija, naše godine kojima smo opsjednuti, 1941., 1991., 1945., 1995..., generali, pukovnici i pokojnici, politika, želja za što većom moći, sve su to teme o kojima na jedinstven, neponovljiv, literarno maestralan način progovara Ivica Đikić. Sedam priča u Štapićima za pričanje natopljeno je pitanjima o zlu i njegovu izvoru, o mračnim dubinama ljudske duše, o životnim nesrećama i biblijskome ljudskom bijesu. I sasvim je svejedno piše li Đikić o svojem djetinjstvu izmaštanim i udaljenim likovima, o Slobodanu Praljku, ili pak još jednu priču o Blagi Antiću, nezaboravnom špijunu iz njegove serije Novine, jer svi su ti likovi itekako višedimenzionalni i njihove sudbine tiču se i nas kao čitatelja. Štapići za pričanje ovdje nisu samo naslov, već su i magičan aparat koji ovaj istinski pripovjedač ima duboko u sebi. Svaka priča u Štapićima za pričanje dolazi iz različite poetike, ali sve zajedno ulaze u jedinstven đikićevski svijet, svijet u kojemu su protagonisti i njihov unutarnji nemir pokretači ne samo radnje već i svega što će se neukrotivo i neobuzdano sručiti na njih same i njihovu okolinu.', 1, 5, 14, 192, 125.10, 35.10, 12, NULL, 1
--END
--GO

--IF NOT EXISTS (SELECT ALL * FROM [dbo].[Books] WHERE [ISBN] = N'978-953-0-61224-2') BEGIN
--  EXECUTE [dbo].[BookCreate] N'978-953-0-61224-2', N'Ušao konj u bar', NULL, N'Ušao konj u bar, roman kojim je David Grossman, između ostalih, osvojio i prestižnu nagradu Man Booker International, nesumnjivo je jedno od književnih remek-djela našega doba. Mali klub u Nataniji u kojem nastupa pedesetosmogodišnji stand-up-komičar Dovale Dži pozornica je na kojoj Grossman još jednom beskompromisno i neustrašivo osvjetljava najtamnije kutke čovjeka, obitelji i svijeta u kojemu živimo. Jedan komičar izložen na sceni i jedan umirovljeni sudac skriven u publici – dvojica prijatelja iz djetinjstva, desetljećima razdvojena krivnjom i sramom – susrest će se u noći u kojoj padaju maske, a svijet je opijen moćnim koktelom kabarea i života, apsurda i nevinosti, tragike i crnoga humora. Ušao konj u bar velika je i neodoljiva knjiga jednoga od najznačajnijih svjetskih pisaca, svjetionik ljudskosti, roman uz koji ćete se smijati noseći srce u grlu.', 1, 5, 16, 192, 125.10, 27.10, 10, NULL, 1
--END
--GO

--IF NOT EXISTS (SELECT ALL * FROM [dbo].[Books] WHERE [ISBN] = N'978-953-342-169-8') BEGIN
--  EXECUTE [dbo].[BookCreate] N'978-953-342-169-8', N'Do kraja zemlje', N'Žena koja od vijesti bježi zove se Ora, a cijeli njezin život mogao bi se ispričati kroz ratove. Šestodnevni, kad je upoznala Avrama i Ilana – ljubav i prijateljstvo koje će trajati zauvijek; Jomkipurski, u kojemu im je sudbina baš njezinim prstima promiješala živote, a nju učinila majkom, i ovaj novi, rat koji joj prijeti uzeti Ofera.', N'Do kraja zemlje Davida Grossmana nije samo remek-djelo i jedan od najvećih romana našega stoljeća, to je jedna od onih knjiga koje doista mijenjaju svijet. Nabolje.', 1, 5, 16, 608, 179.10, 56.10, 1, NULL, 1
--END
--GO


-- BOOKSTORES

IF NOT EXISTS (SELECT ALL * FROM [dbo].[BookStores] WHERE [OIB] = N'09195262064' AND [Email] = N'knjizara.jez@gmail.com') BEGIN
  INSERT INTO [dbo].[BookStores] 
  (
    [CreatedBy],
    [UpdatedBy],
    [Name],
    [OIB],
    [DelayPricePerDay],
    [Address],
    [Telephone],
    [Mobile],
    [Email]
  )
  VALUES 
  (
    1,
    1,
    N'Knjižara Jež',
    N'09195262064',
    1,
    N'Ilica 127, Zagreb',
    N'040855525',
    N'0916235432',
    N'knjizara.jez@gmail.com'
  )
END
GO

-- LOANS

IF NOT EXISTS (SELECT ALL * FROM [dbo].[Loans] WHERE [BookFK] = 1 AND [UserFK] = 4) BEGIN
  DECLARE @LoanDate AS smalldatetime = DATEADD(DAY, -20, GETDATE())
  DECLARE @PlannedReturnDate AS smalldatetime = DATEADD(DAY, -10, GETDATE())
  INSERT INTO [dbo].[Loans] 
  (
    [CreatedBy],
    [UpdatedBy],
    [BookFK],
    [UserFK],
    [LoanPrice],
    [LoanDate],
    [PlannedReturnDate],
    [DelayPricePerDay]
  )
  VALUES 
  (
    1,
    1,
    1,
    4,
    10.20,
    @LoanDate,
    @PlannedReturnDate,
    2
  )
END
GO

IF NOT EXISTS (SELECT ALL * FROM [dbo].[Loans] WHERE [BookFK] = 2 AND [UserFK] = 5) BEGIN
  DECLARE @PlannedReturnDate AS smalldatetime = DATEADD(DAY, 15, GETDATE())
  EXECUTE [dbo].[LoanLoan] 2, 5, @PlannedReturnDate, 1
END
GO

IF NOT EXISTS (SELECT ALL * FROM [dbo].[Loans] WHERE [BookFK] = 3 AND [UserFK] = 8) BEGIN
  DECLARE @PlannedReturnDate AS smalldatetime = DATEADD(DAY, 20, GETDATE())
  EXECUTE [dbo].[LoanLoan] 3, 8, @PlannedReturnDate, 1
END
GO

IF NOT EXISTS (SELECT ALL * FROM [dbo].[Loans] WHERE [BookFK] = 3 AND [UserFK] = 6) BEGIN
  DECLARE @LoanDate AS smalldatetime = DATEADD(DAY, -10, GETDATE())
  DECLARE @PlannedReturnDate AS smalldatetime = DATEADD(DAY, 3, GETDATE())
  INSERT INTO [dbo].[Loans] 
  (
    [CreatedBy],
    [UpdatedBy],
    [BookFK],
    [UserFK],
    [LoanPrice],
    [LoanDate],
    [PlannedReturnDate],
    [DelayPricePerDay]
  )
  VALUES 
  (
    1,
    1,
    1,
    4,
    10.20,
    @LoanDate,
    @PlannedReturnDate,
    2
  )
END
GO

IF NOT EXISTS (SELECT ALL * FROM [dbo].[Loans] WHERE [BookFK] = 2 AND [UserFK] = 9) BEGIN
  DECLARE @PlannedReturnDate AS smalldatetime = DATEADD(DAY, 10, GETDATE())
  EXECUTE [dbo].[LoanLoan] 2, 9, @PlannedReturnDate, 1
END
GO

IF NOT EXISTS (SELECT ALL * FROM [dbo].[Loans] WHERE [BookFK] = 3 AND [UserFK] = 4) BEGIN
  DECLARE @LoanDate AS smalldatetime = DATEADD(DAY, -14, GETDATE())
  DECLARE @PlannedReturnDate AS smalldatetime = DATEADD(DAY, -3, GETDATE())
  INSERT INTO [dbo].[Loans] 
  (
    [CreatedBy],
    [UpdatedBy],
    [BookFK],
    [UserFK],
    [LoanPrice],
    [LoanDate],
    [PlannedReturnDate],
    [DelayPricePerDay]
  )
  VALUES 
  (
    1,
    1,
    1,
    4,
    10.20,
    @LoanDate,
    @PlannedReturnDate,
    2
  )
END
GO

-- PURCHASES

IF NOT EXISTS (SELECT ALL * FROM [dbo].[Purchases] WHERE [BookFK] = 3 AND [UserFK] = 4) BEGIN
  EXECUTE [dbo].[PurchasePurchase] 3, 4, 1, 1
END
GO

IF NOT EXISTS (SELECT ALL * FROM [dbo].[Purchases] WHERE [BookFK] = 4 AND [UserFK] = 4) BEGIN
  EXECUTE [dbo].[PurchasePurchase] 4, 4, 1, 1
END
GO

IF NOT EXISTS (SELECT ALL * FROM [dbo].[Purchases] WHERE [BookFK] = 5 AND [UserFK] = 4) BEGIN
  EXECUTE [dbo].[PurchasePurchase] 5, 4, 1, 1
END
GO

IF NOT EXISTS (SELECT ALL * FROM [dbo].[Purchases] WHERE [BookFK] = 4 AND [UserFK] = 4) BEGIN
  EXECUTE [dbo].[PurchasePurchase] 4, 4, 1, 1
END
GO

-- MESSAGES

IF NOT EXISTS (SELECT ALL * FROM [dbo].[Messages] WHERE [SenderMessage] = N'Do kada radite ponedjeljkom?') BEGIN
  INSERT INTO [dbo].[Messages] 
  (
    [CreatedBy],
    [UpdatedBy],
    [SenderFName],
    [SenderLName],
    [SenderEmail],
    [SenderDate],
    [SenderMessage],
    [ResponderUserFK],
    [ResponderDate],
    [ResponderMessage]
  )
  VALUES 
  (
    1,
    1,
    N'Andrija',
    N'Brežni',
    N'abrezni@gmail.com',
    GETDATE(),
    N'Do kada radite ponedjeljkom?',
    NULL,
    NULL,
    NULL
  )
END
GO

IF NOT EXISTS (SELECT ALL * FROM [dbo].[Messages] WHERE [SenderMessage] = N'Imate li knjigu Gospoda Glembajevi na lageru?') BEGIN
  INSERT INTO [dbo].[Messages] 
  (
    [CreatedBy],
    [UpdatedBy],
    [SenderFName],
    [SenderLName],
    [SenderEmail],
    [SenderDate],
    [SenderMessage],
    [ResponderUserFK],
    [ResponderDate],
    [ResponderMessage]
  )
  VALUES 
  (
    1,
    1,
    N'Julia',
    N'Božić',
    N'jbozic@gmail.com',
    GETDATE(),
    N'Imate li knjigu Gospoda Glembajevi na lageru?',
    NULL,
    NULL,
    NULL
  )
END
GO

IF NOT EXISTS (SELECT ALL * FROM [dbo].[Messages] WHERE [SenderMessage] = N'Zapošljavate li nove radnike?') BEGIN
  INSERT INTO [dbo].[Messages] 
  (
    [CreatedBy],
    [UpdatedBy],
    [SenderFName],
    [SenderLName],
    [SenderEmail],
    [SenderDate],
    [SenderMessage],
    [ResponderUserFK],
    [ResponderDate],
    [ResponderMessage]
  )
  VALUES 
  (
    1,
    1,
    N'Julia',
    N'Brežni',
    N'abrezni@gmail.com',
    GETDATE(),
    N'Zapošljavate li nove radnike?',
    16,
    GETDATE(),
    N'Poštovani, nažalost ne zapošljavamo nove radnike?'
  )
END
GO

-- SUBSCRIPTIONS

EXECUTE [dbo].[SubscriptionSubscribe] 4, 4, 1
GO

EXECUTE [dbo].[SubscriptionSubscribe] 4, 4, 1
GO

EXECUTE [dbo].[SubscriptionSubscribe] 4, 5, 1
GO

EXECUTE [dbo].[SubscriptionSubscribe] 5, 6, 1
GO