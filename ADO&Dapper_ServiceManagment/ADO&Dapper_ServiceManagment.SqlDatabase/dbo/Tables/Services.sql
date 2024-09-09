CREATE TABLE [dbo].[Services] (
    [id]           INT             IDENTITY (1, 1) NOT NULL,
    [desription]   NVARCHAR (MAX)  NULL,
    [category_id]  INT             NULL,
    [price]        DECIMAL (18, 2) NULL,
    [created_at]   DATETIME        DEFAULT (getdate()) NULL,
    [updated_at]   DATETIME        DEFAULT (getdate()) NULL,
    [service_name] NVARCHAR (255)  NOT NULL,
    PRIMARY KEY CLUSTERED ([id] ASC),
    FOREIGN KEY ([category_id]) REFERENCES [dbo].[Categories] ([id])
);

