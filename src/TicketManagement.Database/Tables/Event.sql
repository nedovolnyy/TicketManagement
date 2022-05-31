CREATE TABLE [dbo].[Event]
(
	[Id] int identity primary key,
	[Name] nvarchar(120) NOT NULL,
    [EventTime]	  datetime NOT NULL,
	[Description] nvarchar(max) NOT NULL,
	[LayoutId] int NOT NULL,
)
