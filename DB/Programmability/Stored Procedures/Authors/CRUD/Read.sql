CREATE PROCEDURE [dbo].[AuthorRead] (@Method AS int,
                                     @ID AS int = NULL)
AS BEGIN
  IF @Method = 0 BEGIN
    SELECT ALL
      [ID],
      [CreateDate],
      [CreatedBy],
      [UpdateDate],
      [UpdatedBy],
      [DeleteDate],
      [DeletedBy],
      [FName],
      [LName],
      [BirthDate],
      [ImagePath],
      [Biography]
    FROM [dbo].[Authors]
    ORDER BY [FName] ASC, [LName] ASC
  END
  ELSE IF @Method = 1 BEGIN
        SELECT ALL
      [ID],
      [CreateDate],
      [CreatedBy],
      [UpdateDate],
      [UpdatedBy],
      [DeleteDate],
      [DeletedBy],
      [FName],
      [LName],
      [BirthDate],
      [ImagePath],
      [Biography]
    FROM [dbo].[Authors]
    WHERE [DeleteDate] IS NULL
    ORDER BY [FName] ASC, [LName] ASC
  END
  ELSE IF @Method = 2 BEGIN
    SELECT ALL
      [ID],
      [CreateDate],
      [CreatedBy],
      [UpdateDate],
      [UpdatedBy],
      [DeleteDate],
      [DeletedBy],
      [FName],
      [LName],
      [BirthDate],
      [ImagePath],
      [Biography]
    FROM [dbo].[Authors]
    WHERE [ID] = @ID
  END
  ELSE IF @Method = 3 BEGIN
    SELECT ALL
      [ID],
      [CreateDate],
      [CreatedBy],
      [UpdateDate],
      [UpdatedBy],
      [DeleteDate],
      [DeletedBy],
      [FName],
      [LName],
      [BirthDate],
      [ImagePath],
      [Biography]
    FROM [dbo].[Authors]
    WHERE [ID] = @ID AND [DeleteDate] IS NULL
  END
END
GO
