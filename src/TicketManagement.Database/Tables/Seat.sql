CREATE TABLE [dbo].[Seat]
(
	[Id] int identity primary key,
	[AreaId] int NOT NULL DEFAULT 1,
	[Row] int NOT NULL,
	[Number] int NOT NULL,
)
