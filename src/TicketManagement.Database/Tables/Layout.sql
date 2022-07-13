CREATE TABLE [dbo].[Layout]
(
	[Id] int identity primary key,
	[Name] nvarchar(120) NOT NULL,
	[VenueId] int NOT NULL DEFAULT 1,
	[Description] nvarchar(120) NOT NULL,
)
