CREATE PROCEDURE [dbo].[SP_GetServicesByCategoryId]
	@P_categoryId INT
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @V_sql AS NVARCHAR(MAX) = null;

	SELECT @V_sql = 
		'SELECT * ' +
		'FROM Services ' +
		'WHERE category_id = ' + CAST(@P_categoryId AS NVARCHAR) + ';';

	IF(@V_sql IS NOT NULL)
		EXEC(@V_sql)
	ELSE
		SELECT -1;
END