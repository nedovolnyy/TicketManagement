CREATE PROCEDURE dbo.spEventGetById
(
	@Id INT = null
)
AS
        SELECT Id, Name, EventTime, Description, LayoutId, EventEndTime, EventLogoImage FROM dbo.Event
			WHERE Id = @Id