CREATE PROCEDURE dbo.spEventGetAll
AS
SELECT Id, Name, EventTime, Description, LayoutId FROM dbo.Event