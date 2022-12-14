CREATE TABLE [dbo].[Event]
(
	[Id] int identity primary key,
	[Name] nvarchar(120) NOT NULL,
    [EventTime]	  datetimeoffset(0) NOT NULL,
	[Description] nvarchar(max) NOT NULL,
	[LayoutId] int NOT NULL DEFAULT 1, 
    [EventEndTime] DATETIME2(0) NOT NULL,
	[EventLogoImage] nvarchar(max) NOT NULL
)
