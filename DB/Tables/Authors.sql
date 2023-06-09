﻿CREATE TABLE [dbo].[Authors]
(
  [ID]          int           NOT NULL  IDENTITY(1,1),
  [CreateDate]  datetime      NOT NULL
    CONSTRAINT [DF_Authors_CreateDate] DEFAULT GETDATE(),
  [CreatedBy]   int           NOT NULL,
  [UpdateDate]  datetime      NOT NULL
    CONSTRAINT [DF_Authors_UpdateDate] DEFAULT GETDATE(),
  [UpdatedBy]   int           NOT NULL,
  [DeleteDate]  datetime      NULL,
  [DeletedBy]   int           NULL,

  [FName]       nvarchar(50)  NOT NULL,
  [LName]       nvarchar(50)  NOT NULL,
  [BirthDate]   date          NOT NULL,
  [ImagePath]   nvarchar(500) NULL,
  [Biography]   nvarchar(2000) NULL,

  CONSTRAINT [PK_Authors] PRIMARY KEY CLUSTERED ([ID])
    WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF),
  CONSTRAINT [FK_Authors_CreatedBy] FOREIGN KEY ([CreatedBy]) REFERENCES [dbo].[Users] ([ID]),
  CONSTRAINT [FK_Authors_UpdatedBy] FOREIGN KEY ([UpdatedBy]) REFERENCES [dbo].[Users] ([ID]),
  CONSTRAINT [FK_Authors_DeletedBy] FOREIGN KEY ([DeletedBy]) REFERENCES [dbo].[Users] ([ID])
)
GO
