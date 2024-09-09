CREATE PROCEDURE [dbo].[SP_GetServicesByTagIds]
		@P_tags NVARCHAR(MAX)
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @V_sql AS NVARCHAR(MAX) = null;
	
	SELECT @V_sql = 
		'SELECT DISTINCT S.* ' +
		'FROM Services S ' +
		'JOIN ServiceTags ST ON S.id = ST.service_id ' +
		'WHERE ST.tag_id IN (' + @P_tags + ');';

	IF(@V_sql IS NOT NULL)
		EXEC(@V_sql)
	ELSE
		SELECT -1;
END
