CREATE PROCEDURE dbo.spEventGetById
(
	@Id INT = null
)
AS
        SELECT Id, Name, EventTime, Description, LayoutId FROM dbo.Event
			WHERE Id = @Id