CREATE TABLE [dbo].[Categories] (
    [id]            INT            IDENTITY (1, 1) NOT NULL,
    [category_name] NVARCHAR (255) NOT NULL,
    [description]   NVARCHAR (MAX) NULL,
    [created_at]    DATETIME       DEFAULT (getdate()) NULL,
    [updated_at]    DATETIME       DEFAULT (getdate()) NULL,
    PRIMARY KEY CLUSTERED ([id] ASC)
);

