CREATE TABLE [dbo].[BranchOffices]
(
  [ID]          int           NOT NULL  IDENTITY(1,1),
  [CreateDate]  datetime      NOT NULL
    CONSTRAINT [DF_BranchOffices_CreateDate] DEFAULT GETDATE(),
  [CreatedBy]   int           NOT NULL,
  [UpdateDate]  datetime      NOT NULL
    CONSTRAINT [DF_BranchOffices_UpdateDate] DEFAULT GETDATE(),
  [UpdatedBy]   int           NOT NULL,
  [DeleteDate]  datetime      NULL,
  [DeletedBy]   int           NULL,

  [BookStoreFK] int           NOT NULL,
  [Name]        nvarchar(100) NOT NULL,
  [Address]     nvarchar(200) NOT NULL,
  [Telephone]   nvarchar(50)  NOT NULL,
  [Email]       nvarchar(100) NOT NULL,

  CONSTRAINT [PK_BranchOffices] PRIMARY KEY ([ID]),
  CONSTRAINT [FK_BranchOffices_CreatedBy] FOREIGN KEY ([CreatedBy]) REFERENCES [dbo].[Users] ([ID]),
  CONSTRAINT [FK_BranchOffices_UpdatedBy] FOREIGN KEY ([UpdatedBy]) REFERENCES [dbo].[Users] ([ID]),
  CONSTRAINT [FK_BranchOffices_DeletedBy] FOREIGN KEY ([DeletedBy]) REFERENCES [dbo].[Users] ([ID]),

  CONSTRAINT [FK_BranchOffices_BookStores] FOREIGN KEY([BookStoreFK]) REFERENCES [dbo].[BookStores] ([ID])
)
GO
