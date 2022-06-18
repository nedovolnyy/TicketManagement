CREATE PROCEDURE dbo.spEventGetAll
AS
SELECT Id, Name, EventTime, Description, LayoutId, EventEndTime FROM dbo.Event