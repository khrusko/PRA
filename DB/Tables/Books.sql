CREATE TABLE [dbo].[Books]
(
  [ID]          int           NOT NULL  IDENTITY(1,1),
  [CreateDate]  datetime      NOT NULL
    CONSTRAINT [DF_Books_CreateDate] DEFAULT GETDATE(),
  [CreatedBy]   int           NOT NULL,
  [UpdateDate]  datetime      NOT NULL
    CONSTRAINT [DF_Books_UpdateDate] DEFAULT GETDATE(),
  [UpdatedBy]   int           NOT NULL,
  [DeleteDate]  datetime      NULL,
  [DeletedBy]   int           NULL,

  [ISBN]          char(17)      NOT NULL,
  [Title]         nvarchar(100) NOT NULL,
  [Summary]       nvarchar(MAX) NULL,
  [Description]   nvarchar(MAX) NOT NULL,
  [IsNew]         bit           NOT NULL
    CONSTRAINT [DF_Books_IsNew] DEFAULT 1,
  [PublisherFK]   int           NOT NULL,
  [AuthorFK]      int           NOT NULL,
  [PageCount]     int           NOT NULL,
  [PurchasePrice] decimal(6, 2) NOT NULL,
  [LoanPrice]     decimal(6, 2) NOT NULL,
  [Quantity]      int           NOT NULL
    CONSTRAINT [DF_Books_Quantity] DEFAULT 0,
  [ImagePath]     nvarchar(500) NULL,

  CONSTRAINT [PK_Books] PRIMARY KEY ([ID]),
  CONSTRAINT [FK_Books_CreatedBy] FOREIGN KEY ([CreatedBy]) REFERENCES [dbo].[Users] ([ID]),
  CONSTRAINT [FK_Books_UpdatedBy] FOREIGN KEY ([UpdatedBy]) REFERENCES [dbo].[Users] ([ID]),
  CONSTRAINT [FK_Books_DeletedBy] FOREIGN KEY ([DeletedBy]) REFERENCES [dbo].[Users] ([ID]),

  CONSTRAINT [FK_Books_Publishers]    FOREIGN KEY ([PublisherFK]) REFERENCES [dbo].[Publishers] ([ID]),
  CONSTRAINT [FK_Books_Authors]       FOREIGN KEY ([AuthorFK])    REFERENCES [dbo].[Authors] ([ID]),

  CONSTRAINT [CK_Books_PurchasePrice] CHECK ([PurchasePrice] >= 0),
  CONSTRAINT [CK_Books_LoanPrice]     CHECK ([LoanPrice] >= 0),
  CONSTRAINT [CK_Books_Quantity]      CHECK ([Quantity] >= 0)
)
GO