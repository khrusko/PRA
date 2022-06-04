CREATE TABLE [dbo].[Users]
(
  [ID]          int           NOT NULL IDENTITY(1,1),
  [CreateDate]  datetime      NOT NULL
    CONSTRAINT [DF_Users_CreateDate] DEFAULT GETDATE(),
  [CreatedBy]   int           NOT NULL,
  [UpdateDate]  datetime      NOT NULL
    CONSTRAINT [DF_Users_UpdateDate] DEFAULT GETDATE(),
  [UpdatedBy]   int           NOT NULL,
  [DeleteDate]  datetime      NULL,
  [DeletedBy]   int           NULL,

  [UserID]        char(11)      NOT NULL,
  [FName]         nvarchar(50)  NOT NULL,
  [LName]         nvarchar(50)  NOT NULL,
  [Email]         nvarchar(100) NOT NULL,
  [PasswordHash]  nvarchar(512) NOT NULL,
  [ImagePath]     nvarchar(500) NULL,
  [Address]       nvarchar(200) NULL,
  [IsAdmin]       bit           NOT NULL
    CONSTRAINT [DF_Users_IsAdmin] DEFAULT 0,

  [ConfirmationGUID]        uniqueidentifier  NOT NULL
    CONSTRAINT [DF_Users_ConfirmationGUID] DEFAULT NEWID(),
  [ConfirmationIsApproved]  bit               NOT NULL
    CONSTRAINT [DF_Users_ConfirmationIsApproved] DEFAULT 0,
  [ConfirmationDate]        datetime          NULL,

  CONSTRAINT [PK_Users] PRIMARY KEY ([ID]),
  CONSTRAINT [FK_Users_CreatedBy] FOREIGN KEY ([CreatedBy]) REFERENCES [dbo].[Users] ([ID]),
  CONSTRAINT [FK_Users_UpdatedBy] FOREIGN KEY ([UpdatedBy]) REFERENCES [dbo].[Users] ([ID]),
  CONSTRAINT [FK_Users_DeletedBy] FOREIGN KEY ([DeletedBy]) REFERENCES [dbo].[Users] ([ID]),

  CONSTRAINT [CK_Users_UserID] CHECK ([UserID] LIKE '[D|K][0-9][0-9][0-1][0-9][0-3][0-9][0-9][0-9][0-9][0-9]')
)
GO
