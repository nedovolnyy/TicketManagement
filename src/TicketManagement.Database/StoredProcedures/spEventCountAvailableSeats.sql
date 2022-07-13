CREATE PROCEDURE dbo.spEventCountAvailableSeats
(
	@Id INT = null,
	@CountAvailableSeats INT = null OUTPUT
)
AS

SELECT @CountAvailableSeats = COUNT(Id) FROM dbo.EventSeat
	WHERE State = 0 AND
		  EventAreaId IN (SELECT Id FROM dbo.EventArea WHERE EventId = @Id)