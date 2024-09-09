CREATE TABLE [dbo].[ServiceTags] (
    [service_id] INT NOT NULL,
    [tag_id]     INT NOT NULL,
    PRIMARY KEY CLUSTERED ([service_id] ASC, [tag_id] ASC),
    FOREIGN KEY ([service_id]) REFERENCES [dbo].[Services] ([id]),
    FOREIGN KEY ([tag_id]) REFERENCES [dbo].[Tags] ([id])
);

