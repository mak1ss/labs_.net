﻿/*
Deployment script for service_db

This code was generated by a tool.
Changes to this file may cause incorrect behavior and will be lost if
the code is regenerated.
*/

GO
SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, CONCAT_NULL_YIELDS_NULL, QUOTED_IDENTIFIER ON;

SET NUMERIC_ROUNDABORT OFF;


GO
:setvar DatabaseName "service_db"
:setvar DefaultFilePrefix "service_db"
:setvar DefaultDataPath "C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\"
:setvar DefaultLogPath "C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\"

GO
:on error exit
GO
/*
Detect SQLCMD mode and disable script execution if SQLCMD mode is not supported.
To re-enable the script after enabling SQLCMD mode, execute the following:
SET NOEXEC OFF; 
*/
:setvar __IsSqlCmdEnabled "True"
GO
IF N'$(__IsSqlCmdEnabled)' NOT LIKE N'True'
    BEGIN
        PRINT N'SQLCMD mode must be enabled to successfully execute this script.';
        SET NOEXEC ON;
    END


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET AUTO_CLOSE OFF 
            WITH ROLLBACK IMMEDIATE;
    END


GO
USE [$(DatabaseName)];


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET ANSI_NULLS ON,
                ANSI_PADDING ON,
                ANSI_WARNINGS ON,
                ARITHABORT ON,
                CONCAT_NULL_YIELDS_NULL ON,
                QUOTED_IDENTIFIER ON,
                ANSI_NULL_DEFAULT ON,
                CURSOR_DEFAULT LOCAL,
                RECOVERY FULL 
            WITH ROLLBACK IMMEDIATE;
    END


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET PAGE_VERIFY NONE,
                DISABLE_BROKER 
            WITH ROLLBACK IMMEDIATE;
    END


GO
ALTER DATABASE [$(DatabaseName)]
    SET TARGET_RECOVERY_TIME = 0 SECONDS 
    WITH ROLLBACK IMMEDIATE;


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET QUERY_STORE (QUERY_CAPTURE_MODE = ALL, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 367), MAX_STORAGE_SIZE_MB = 100) 
            WITH ROLLBACK IMMEDIATE;
    END


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET QUERY_STORE = OFF 
            WITH ROLLBACK IMMEDIATE;
    END


GO
PRINT N'Creating Table [dbo].[Categories]...';


GO
CREATE TABLE [dbo].[Categories] (
    [id]            INT            IDENTITY (1, 1) NOT NULL,
    [category_name] NVARCHAR (255) NOT NULL,
    [description]   NVARCHAR (MAX) NULL,
    [created_at]    DATETIME       NULL,
    [updated_at]    DATETIME       NULL,
    PRIMARY KEY CLUSTERED ([id] ASC)
);


GO
PRINT N'Creating Table [dbo].[Services]...';


GO
CREATE TABLE [dbo].[Services] (
    [id]           INT             IDENTITY (1, 1) NOT NULL,
    [desription]   NVARCHAR (MAX)  NULL,
    [category_id]  INT             NULL,
    [price]        DECIMAL (18, 2) NULL,
    [created_at]   DATETIME        NULL,
    [updated_at]   DATETIME        NULL,
    [service_name] NVARCHAR (255)  NOT NULL,
    PRIMARY KEY CLUSTERED ([id] ASC)
);


GO
PRINT N'Creating Table [dbo].[ServiceTags]...';


GO
CREATE TABLE [dbo].[ServiceTags] (
    [service_id] INT NOT NULL,
    [tag_id]     INT NOT NULL,
    PRIMARY KEY CLUSTERED ([service_id] ASC, [tag_id] ASC)
);


GO
PRINT N'Creating Table [dbo].[Tags]...';


GO
CREATE TABLE [dbo].[Tags] (
    [id]         INT            IDENTITY (1, 1) NOT NULL,
    [tag_name]   NVARCHAR (255) NOT NULL,
    [created_at] DATETIME       NULL,
    [updated_at] DATETIME       NULL,
    PRIMARY KEY CLUSTERED ([id] ASC)
);


GO
PRINT N'Creating Default Constraint unnamed constraint on [dbo].[Categories]...';


GO
ALTER TABLE [dbo].[Categories]
    ADD DEFAULT (getdate()) FOR [created_at];


GO
PRINT N'Creating Default Constraint unnamed constraint on [dbo].[Categories]...';


GO
ALTER TABLE [dbo].[Categories]
    ADD DEFAULT (getdate()) FOR [updated_at];


GO
PRINT N'Creating Default Constraint unnamed constraint on [dbo].[Services]...';


GO
ALTER TABLE [dbo].[Services]
    ADD DEFAULT (getdate()) FOR [created_at];


GO
PRINT N'Creating Default Constraint unnamed constraint on [dbo].[Services]...';


GO
ALTER TABLE [dbo].[Services]
    ADD DEFAULT (getdate()) FOR [updated_at];


GO
PRINT N'Creating Default Constraint unnamed constraint on [dbo].[Tags]...';


GO
ALTER TABLE [dbo].[Tags]
    ADD DEFAULT (getdate()) FOR [created_at];


GO
PRINT N'Creating Default Constraint unnamed constraint on [dbo].[Tags]...';


GO
ALTER TABLE [dbo].[Tags]
    ADD DEFAULT (getdate()) FOR [updated_at];


GO
PRINT N'Creating Foreign Key unnamed constraint on [dbo].[Services]...';


GO
ALTER TABLE [dbo].[Services] WITH NOCHECK
    ADD FOREIGN KEY ([category_id]) REFERENCES [dbo].[Categories] ([id]);


GO
PRINT N'Creating Foreign Key unnamed constraint on [dbo].[ServiceTags]...';


GO
ALTER TABLE [dbo].[ServiceTags] WITH NOCHECK
    ADD FOREIGN KEY ([service_id]) REFERENCES [dbo].[Services] ([id]);


GO
PRINT N'Creating Foreign Key unnamed constraint on [dbo].[ServiceTags]...';


GO
ALTER TABLE [dbo].[ServiceTags] WITH NOCHECK
    ADD FOREIGN KEY ([tag_id]) REFERENCES [dbo].[Tags] ([id]);


GO
PRINT N'Creating Procedure [dbo].[SP_DeleteRecord]...';


GO
CREATE PROCEDURE [dbo].[SP_DeleteRecord]
	-- Add the parameters for the stored procedure here
	@P_tableName nvarchar(50) = null,
	@P_Id nvarchar = null
AS
BEGIN
	SET NOCOUNT ON;
	
	declare @V_table nvarchar(50) = null
	if (@P_tableName is not null)
		select @V_table = QUOTENAME( TABLE_NAME )
		FROM INFORMATION_SCHEMA.TABLES
		WHERE TABLE_NAME = @P_tableName

	declare @V_sql as nvarchar(MAX) = null
	if (@V_table is not null and @P_Id is not null)
		SELECT @V_sql = 'DELETE FROM ' + @V_table + 'WHERE ID = ' + @P_Id + ';'

	if(@V_sql is not null)
		EXEC(@V_sql)
	else
		select 0;
END
GO
PRINT N'Creating Procedure [dbo].[SP_GetAllRecords]...';


GO
CREATE PROCEDURE [dbo].[SP_GetAllRecords]
	-- Add the parameters for the stored procedure here
	@P_tableName nvarchar(50) = null
AS
BEGIN
	SET NOCOUNT ON;
	
	declare @V_table nvarchar(50) = null
	if (@P_tableName is not null)
		select @V_table = QUOTENAME( TABLE_NAME )
		FROM INFORMATION_SCHEMA.TABLES
		WHERE TABLE_NAME = @P_tableName

	declare @V_sql as nvarchar(MAX) = null
	if (@V_table is not null)
		select @V_sql = 'select * from ' + @V_table + ';'

	if(@V_sql is not null)
		exec(@V_sql)
	else
		select -1;
END
GO
PRINT N'Creating Procedure [dbo].[SP_GetRecordById]...';


GO
CREATE PROCEDURE [dbo].[SP_GetRecordById]
	-- Add the parameters for the stored procedure here
	@P_tableName nvarchar(50) = null,
	@P_Id nvarchar = null
AS
BEGIN
	SET NOCOUNT ON;
	
	declare @V_table nvarchar(50) = null
	if (@P_tableName is not null)
		select @V_table = QUOTENAME( TABLE_NAME )
		FROM INFORMATION_SCHEMA.TABLES
		WHERE TABLE_NAME = @P_tableName

	declare @V_sql as nvarchar(MAX) = null
	if (@V_table is not null and @P_Id is not null)
		select @V_sql = 'select * from ' + @V_table + ' where Id = ' + @P_Id + ';'

	if(@V_sql is not null)
		exec(@V_sql)
	else
		select -1;
END
GO
PRINT N'Creating Procedure [dbo].[SP_GetServicesByCategoryId]...';


GO
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
GO
PRINT N'Creating Procedure [dbo].[SP_GetServicesByTagIds]...';


GO
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
GO
PRINT N'Creating Procedure [dbo].[SP_InsertRecord]...';


GO
CREATE PROCEDURE [dbo].[SP_InsertRecord]
	-- Add the parameters for the stored procedure here
	@P_tableName nvarchar(50) = null,
	@P_columnsString nvarchar(MAX) = null, 
	@P_propertiesString nvarchar(MAX) = null
AS
BEGIN
	SET NOCOUNT ON;
	
	declare @V_table nvarchar(50) = null
	if (@P_tableName is not null)
		select @V_table = QUOTENAME( TABLE_NAME )
		FROM INFORMATION_SCHEMA.TABLES
		WHERE TABLE_NAME = @P_tableName

	declare @V_sql as nvarchar(MAX) = null
	if (@V_table is not null and @P_columnsString is not null and @P_propertiesString is not null)
		SET @V_sql = 'INSERT INTO ' + @V_table + ' (' + @P_columnsString + ') VALUES (' + @P_propertiesString + ');'
	
	if(@V_sql is not null)
		SELECT @V_sql;
	else
		SELECT -1;
END
GO
PRINT N'Creating Procedure [dbo].[SP_UpdateRecord]...';


GO
CREATE PROCEDURE [dbo].[SP_UpdateRecord]
	-- Add the parameters for the stored procedure here
	@P_tableName nvarchar(50) = null,
	@P_columnsString nvarchar(MAX) = null,
	@P_Id nvarchar = null
AS
BEGIN
	SET NOCOUNT ON;
	
	declare @V_table nvarchar(50) = null
	if (@P_tableName is not null)
		select @V_table = QUOTENAME( TABLE_NAME )
		FROM INFORMATION_SCHEMA.TABLES
		WHERE TABLE_NAME = @P_tableName

	declare @V_sql as nvarchar(MAX) = null
	if (@V_table is not null and @P_columnsString is not null and @P_Id is not null)
		SET @V_sql = 'UPDATE ' + @V_table + ' SET ' + @P_columnsString + ' WHERE Id = ' + @P_Id + ';'

	if(@V_sql is not null)
		SELECT(@V_sql)
	else
		SELECT -1;
END
GO
PRINT N'Checking existing data against newly created constraints';


GO
USE [$(DatabaseName)];


GO
CREATE TABLE [#__checkStatus] (
    id           INT            IDENTITY (1, 1) PRIMARY KEY CLUSTERED,
    [Schema]     NVARCHAR (256),
    [Table]      NVARCHAR (256),
    [Constraint] NVARCHAR (256)
);

SET NOCOUNT ON;

DECLARE tableconstraintnames CURSOR LOCAL FORWARD_ONLY
    FOR SELECT SCHEMA_NAME([schema_id]),
               OBJECT_NAME([parent_object_id]),
               [name],
               0
        FROM   [sys].[objects]
        WHERE  [parent_object_id] IN (OBJECT_ID(N'dbo.Services'), OBJECT_ID(N'dbo.ServiceTags'))
               AND [type] IN (N'F', N'C')
                   AND [object_id] IN (SELECT [object_id]
                                       FROM   [sys].[check_constraints]
                                       WHERE  [is_not_trusted] <> 0
                                              AND [is_disabled] = 0
                                       UNION
                                       SELECT [object_id]
                                       FROM   [sys].[foreign_keys]
                                       WHERE  [is_not_trusted] <> 0
                                              AND [is_disabled] = 0);

DECLARE @schemaname AS NVARCHAR (256);

DECLARE @tablename AS NVARCHAR (256);

DECLARE @checkname AS NVARCHAR (256);

DECLARE @is_not_trusted AS INT;

DECLARE @statement AS NVARCHAR (1024);

BEGIN TRY
    OPEN tableconstraintnames;
    FETCH tableconstraintnames INTO @schemaname, @tablename, @checkname, @is_not_trusted;
    WHILE @@fetch_status = 0
        BEGIN
            PRINT N'Checking constraint: ' + @checkname + N' [' + @schemaname + N'].[' + @tablename + N']';
            SET @statement = N'ALTER TABLE [' + @schemaname + N'].[' + @tablename + N'] WITH ' + CASE @is_not_trusted WHEN 0 THEN N'CHECK' ELSE N'NOCHECK' END + N' CHECK CONSTRAINT [' + @checkname + N']';
            BEGIN TRY
                EXECUTE [sp_executesql] @statement;
            END TRY
            BEGIN CATCH
                INSERT  [#__checkStatus] ([Schema], [Table], [Constraint])
                VALUES                  (@schemaname, @tablename, @checkname);
            END CATCH
            FETCH tableconstraintnames INTO @schemaname, @tablename, @checkname, @is_not_trusted;
        END
END TRY
BEGIN CATCH
    PRINT ERROR_MESSAGE();
END CATCH

IF CURSOR_STATUS(N'LOCAL', N'tableconstraintnames') >= 0
    CLOSE tableconstraintnames;

IF CURSOR_STATUS(N'LOCAL', N'tableconstraintnames') = -1
    DEALLOCATE tableconstraintnames;

SELECT N'Constraint verification failed:' + [Schema] + N'.' + [Table] + N',' + [Constraint]
FROM   [#__checkStatus];

IF @@ROWCOUNT > 0
    BEGIN
        DROP TABLE [#__checkStatus];
        RAISERROR (N'An error occurred while verifying constraints', 16, 127);
    END

SET NOCOUNT OFF;

DROP TABLE [#__checkStatus];


GO
PRINT N'Update complete.';


GO
