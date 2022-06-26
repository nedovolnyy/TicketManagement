CREATE TABLE [dbo].[EventArea]
(
	[Id] int identity primary key,
	[EventId] int NOT NULL DEFAULT 1,
	[Description] nvarchar(200) NOT NULL,
	[CoordX] int NOT NULL,
	[CoordY] int NOT NULL,
	[Price] decimal(18, 2) NOT NULL
)
