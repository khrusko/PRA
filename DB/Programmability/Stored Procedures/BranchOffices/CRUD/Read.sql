CREATE PROCEDURE [dbo].[BranchOfficeRead] (@ID AS int = NULL)
AS BEGIN
  IF @ID IS NULL BEGIN
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
  ELSE BEGIN
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
