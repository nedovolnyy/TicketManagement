CREATE TABLE [dbo].[EventSeat]
(
	[Id] int identity primary key,
	[EventAreaId] int NOT NULL DEFAULT 1,
	[Row] int NOT NULL,
	[Number] int NOT NULL,
	[State] int NOT NULL
)
