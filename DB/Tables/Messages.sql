﻿CREATE TABLE [dbo].[Messages]
(
  [ID]          int           NOT NULL  IDENTITY(1,1),
  [CreateDate]  datetime      NOT NULL
    CONSTRAINT [DF_Messages_CreateDate] DEFAULT GETDATE(),
  [CreatedBy]   int           NOT NULL,
  [UpdateDate]  datetime      NOT NULL
    CONSTRAINT [DF_Messages_UpdateDate] DEFAULT GETDATE(),
  [UpdatedBy]   int           NOT NULL,
  [DeleteDate]  datetime      NULL,
  [DeletedBy]   int           NULL,

  [SenderFName]       nvarchar(50)  NOT NULL,
  [SenderLName]       nvarchar(50)  NOT NULL,
  [SenderEmail]       nvarchar(100) NOT NULL,
  [SenderDate]        datetime      NOT NULL
    CONSTRAINT [DF_Messages_SenderDate] DEFAULT GETDATE(),
  [SenderMessage]     nvarchar(2000) NOT NULL,
  [ResponderUserFK]   int           NULL,
  [ResponderDate]     datetime      NULL,
  [ResponderMessage]  nvarchar(2000) NULL,

  CONSTRAINT [PK_Messages] PRIMARY KEY ([ID]),
  CONSTRAINT [FK_Messages_CreatedBy] FOREIGN KEY ([CreatedBy]) REFERENCES [dbo].[Users] ([ID]),
  CONSTRAINT [FK_Messages_UpdatedBy] FOREIGN KEY ([UpdatedBy]) REFERENCES [dbo].[Users] ([ID]),
  CONSTRAINT [FK_Messages_DeletedBy] FOREIGN KEY ([DeletedBy]) REFERENCES [dbo].[Users] ([ID]),

  CONSTRAINT [FK_Messages_Users_Responder]  FOREIGN KEY ([ResponderUserFK]) REFERENCES [dbo].[Users] ([ID]),
)
GO
