CREATE PROCEDURE dbo.spEventDelete
(
	@Id INT = null
)
AS
BEGIN
        DELETE dbo.EventSeat WHERE EventAreaId IN (SELECT Id FROM dbo.EventArea WHERE EventId = @Id)
        DELETE dbo.EventArea WHERE EventId = @Id
        DELETE dbo.Event WHERE Id = @Id
END