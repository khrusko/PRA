CREATE TABLE [dbo].[Books]
(
  [ID]          int           NOT NULL IDENTITY(1,1),
  [CreateDate]  datetime      NOT NULL
    CONSTRAINT [DF_Books_CreateDate] DEFAULT GETDATE(),
  [CreatedBy]   int           NOT NULL,
  [UpdateDate]  datetime      NOT NULL
    CONSTRAINT [DF_Books_UpdateDate] DEFAULT GETDATE(),
  [UpdatedBy]   int           NOT NULL,
  [DeleteDate]  datetime      NULL,
  [DeletedBy]   int           NULL,

  [ISBN]          nvarchar(20)  NOT NULL,
  [Title]         nvarchar(100) NOT NULL,
  [Summary]       nvarchar(2000) NULL,
  [Description]   nvarchar(2000) NOT NULL,
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

  CONSTRAINT [PK_Books] PRIMARY KEY  CLUSTERED ([ID] ASC)
    WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF),
  CONSTRAINT [FK_Books_CreatedBy] FOREIGN KEY ([CreatedBy]) REFERENCES [dbo].[Users] ([ID]),
  CONSTRAINT [FK_Books_UpdatedBy] FOREIGN KEY ([UpdatedBy]) REFERENCES [dbo].[Users] ([ID]),
  CONSTRAINT [FK_Books_DeletedBy] FOREIGN KEY ([DeletedBy]) REFERENCES [dbo].[Users] ([ID]),

  CONSTRAINT [FK_Books_Publishers]    FOREIGN KEY ([PublisherFK]) REFERENCES [dbo].[Publishers] ([ID]),
  CONSTRAINT [FK_Books_Authors]       FOREIGN KEY ([AuthorFK])    REFERENCES [dbo].[Authors] ([ID])
)
GO