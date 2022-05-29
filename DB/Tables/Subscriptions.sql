CREATE TABLE [dbo].[Subscriptions]
(
  [ID]          int           NOT NULL  IDENTITY(1,1),
  [CreateDate]  datetime      NOT NULL
    CONSTRAINT [DF_Subscriptions_CreateDate] DEFAULT GETDATE(),
  [CreatedBy]   int           NOT NULL,
  [UpdateDate]  datetime      NOT NULL
    CONSTRAINT [DF_Subscriptions_UpdateDate] DEFAULT GETDATE(),
  [UpdatedBy]   int           NOT NULL,
  [DeleteDate]  datetime      NULL,
  [DeletedBy]   int           NULL,

  [BookFK]            int           NOT NULL,
  [UserFK]            int           NOT NULL,
  [SubscriptionDate]  datetime      NOT NULL
    CONSTRAINT [DF_Subscriptions_SubscriptionDate] DEFAULT GETDATE(),
  [IsResolved]        bit           NOT NULL
    CONSTRAINT [DF_Subscriptions_IsResolved] DEFAULT 0,
  [ResolvedDate]      datetime      NULL,

  CONSTRAINT [PK_Subscriptions] PRIMARY KEY ([ID]),
  CONSTRAINT [FK_Subscriptions_CreatedBy] FOREIGN KEY ([CreatedBy]) REFERENCES [dbo].[Users] ([ID]),
  CONSTRAINT [FK_Subscriptions_UpdatedBy] FOREIGN KEY ([UpdatedBy]) REFERENCES [dbo].[Users] ([ID]),
  CONSTRAINT [FK_Subscriptions_DeletedBy] FOREIGN KEY ([DeletedBy]) REFERENCES [dbo].[Users] ([ID]),

  CONSTRAINT [FK_Subscriptions_Books] FOREIGN KEY ([BookFK]) REFERENCES [dbo].[Books] ([ID]),
  CONSTRAINT [FK_Subscriptions_Users] FOREIGN KEY ([UserFK]) REFERENCES [dbo].[Users] ([ID])
)
GO
