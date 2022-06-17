CREATE TABLE [dbo].[Purchases]
(
  [ID]          int           NOT NULL  IDENTITY(1,1),
  [CreateDate]  datetime      NOT NULL
    CONSTRAINT [DF_Purchases_CreateDate] DEFAULT GETDATE(),
  [CreatedBy]   int           NOT NULL,
  [UpdateDate]  datetime      NOT NULL
    CONSTRAINT [DF_Purchases_UpdateDate] DEFAULT GETDATE(),
  [UpdatedBy]   int           NOT NULL,
  [DeleteDate]  datetime      NULL,
  [DeletedBy]   int           NULL,

  [BookFK]        int           NOT NULL,
  [UserFK]        int           NOT NULL,
  [Quantity]      int           NOT NULL,
  [UnitPrice]     decimal(6, 2) NOT NULL,
  [PurchaseDate]  datetime      NOT NULL
    CONSTRAINT [DF_Purchases_PurchaseDate] DEFAULT GETDATE(),
 
  CONSTRAINT [PK_Purchases] PRIMARY KEY ([ID]),
  CONSTRAINT [FK_Purchases_CreatedBy] FOREIGN KEY ([CreatedBy]) REFERENCES [dbo].[Users] ([ID]),
  CONSTRAINT [FK_Purchases_UpdatedBy] FOREIGN KEY ([UpdatedBy]) REFERENCES [dbo].[Users] ([ID]),
  CONSTRAINT [FK_Purchases_DeletedBy] FOREIGN KEY ([DeletedBy]) REFERENCES [dbo].[Users] ([ID]),

  CONSTRAINT [FK_Purchases_Books]     FOREIGN KEY ([BookFK]) REFERENCES [dbo].[Books] ([ID]),
  CONSTRAINT [FK_Purchases_Users]     FOREIGN KEY ([UserFK]) REFERENCES [dbo].[Users] ([ID]),
)
GO
