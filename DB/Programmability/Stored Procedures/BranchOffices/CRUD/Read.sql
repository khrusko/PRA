CREATE PROCEDURE [dbo].[BranchOfficeRead] (@Method AS int,
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
      [Name],
      [Address],
      [Telephone],
      [Email]
    FROM [dbo].[BranchOffices]
    ORDER BY [Name] ASC
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
      [Name],
      [Address],
      [Telephone],
      [Email]
    FROM [dbo].[BranchOffices]
    WHERE [DeleteDate] IS NULL
    ORDER BY [Name] ASC
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
      [Name],
      [Address],
      [Telephone],
      [Email]
    FROM [dbo].[BranchOffices]
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
      [Name],
      [Address],
      [Telephone],
      [Email]
    FROM [dbo].[BranchOffices]
    WHERE [ID] = @ID AND [DeleteDate] IS NULL
  END
END
GO
