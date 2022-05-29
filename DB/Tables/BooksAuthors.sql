CREATE TABLE [dbo].[BooksAuthors]
(
  [BookFK]      int           NOT NULL,
  [AuthorFK]    int           NOT NULL,

  CONSTRAINT [PK_BooksAuthors]          PRIMARY KEY ([BookFK], [AuthorFK]),
  CONSTRAINT [FK_BooksAuthors_Books]    FOREIGN KEY ([BookFK]) REFERENCES [dbo].[Books] ([ID]),
  CONSTRAINT [FK_BooksAuthors_Authors]  FOREIGN KEY ([AuthorFK]) REFERENCES [dbo].[Authors] ([ID])
)
GO
