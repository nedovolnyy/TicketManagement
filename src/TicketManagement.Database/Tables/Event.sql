CREATE TABLE [dbo].[Event]
(
	[Id] int identity primary key,
	[Name] nvarchar(120) NOT NULL,
    [EventTime]	  datetimeoffset(0) NOT NULL,
	[Description] nvarchar(max) NOT NULL,
	[LayoutId] int NOT NULL, 
    [EventEndTime] DATETIME2(0) NOT NULL 
)
