CREATE TABLE [dbo].[BookStores]
(
  [ID]          int           NOT NULL  IDENTITY(1,1),
  [CreateDate]  datetime      NOT NULL
    CONSTRAINT [DF_BookStores_CreateDate] DEFAULT GETDATE(),
  [CreatedBy]   int           NOT NULL,
  [UpdateDate]  datetime      NOT NULL
    CONSTRAINT [DF_BookStores_UpdateDate] DEFAULT GETDATE(),
  [UpdatedBy]   int           NOT NULL,
  [DeleteDate]  datetime      NULL,
  [DeletedBy]   int           NULL,

  [Name]              nvarchar(100) NOT NULL,
  [OIB]               char(11)      NOT NULL,
  [DelayPricePerDay]  decimal(5, 2) NOT NULL,
  [Address]           nvarchar(200) NOT NULL,
  [Telephone]         nvarchar(50)  NOT NULL,
  [Mobile]            nvarchar(50)  NULL,
  [Email]             nvarchar(100) NOT NULL,

  CONSTRAINT [PK_BookStores] PRIMARY KEY ([ID]),
  CONSTRAINT [FK_BookStores_CreatedBy] FOREIGN KEY ([CreatedBy]) REFERENCES [dbo].[Users] ([ID]),
  CONSTRAINT [FK_BookStores_UpdatedBy] FOREIGN KEY ([UpdatedBy]) REFERENCES [dbo].[Users] ([ID]),
  CONSTRAINT [FK_BookStores_DeletedBy] FOREIGN KEY ([DeletedBy]) REFERENCES [dbo].[Users] ([ID]),
)
GO
