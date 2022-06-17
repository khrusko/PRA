CREATE TABLE [dbo].[Loans]
(
  [ID]          int           NOT NULL  IDENTITY(1,1),
  [CreateDate]  datetime      NOT NULL
    CONSTRAINT [DF_Loans_CreateDate] DEFAULT GETDATE(),
  [CreatedBy]   int           NOT NULL,
  [UpdateDate]  datetime      NOT NULL
    CONSTRAINT [DF_Loans_UpdateDate] DEFAULT GETDATE(),
  [UpdatedBy]   int           NOT NULL,
  [DeleteDate]  datetime      NULL,
  [DeletedBy]   int           NULL,

  [BookFK]            int           NOT NULL,
  [UserFK]            int           NOT NULL,
  [LoanPrice]         decimal(5, 2) NOT NULL,
  [LoanDate]          datetime      NOT NULL
    CONSTRAINT [DF_Loads_LoanDate] DEFAULT GETDATE(),
  [PlannedReturnDate] smalldatetime NOT NULL,
  [ReturnDate]        smalldatetime NULL,
  [DelayDays]         int           NOT NULL
    CONSTRAINT [DF_Loans_DelayDays] DEFAULT 0,
  [DelayPricePerDay]  decimal(5, 2) NOT NULL,
 
  CONSTRAINT [PK_Loans] PRIMARY KEY ([ID]),
  CONSTRAINT [FK_Loans_CreatedBy] FOREIGN KEY ([CreatedBy]) REFERENCES [dbo].[Users] ([ID]),
  CONSTRAINT [FK_Loans_UpdatedBy] FOREIGN KEY ([UpdatedBy]) REFERENCES [dbo].[Users] ([ID]),
  CONSTRAINT [FK_Loans_DeletedBy] FOREIGN KEY ([DeletedBy]) REFERENCES [dbo].[Users] ([ID]),

  CONSTRAINT [FK_Loans_Books]             FOREIGN KEY ([BookFK]) REFERENCES [dbo].[Books] ([ID]),
  CONSTRAINT [FK_Loans_Users]             FOREIGN KEY ([UserFK]) REFERENCES [dbo].[Users] ([ID]),
)
GO
