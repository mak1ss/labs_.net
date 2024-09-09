CREATE TABLE [dbo].[Tags] (
    [id]         INT            IDENTITY (1, 1) NOT NULL,
    [tag_name]   NVARCHAR (255) NOT NULL,
    [created_at] DATETIME       DEFAULT (getdate()) NULL,
    [updated_at] DATETIME       DEFAULT (getdate()) NULL,
    PRIMARY KEY CLUSTERED ([id] ASC)
);

