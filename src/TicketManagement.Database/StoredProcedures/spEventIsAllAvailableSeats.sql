CREATE PROCEDURE dbo.spEventIsAllAvailableSeats
(
	@Id INT = null,
	@IsAllAvailableSeats BIT = null OUTPUT
)
AS

IF (SELECT COUNT(Id) FROM dbo.EventSeat WHERE State = 0 AND
		  EventAreaId IN (SELECT Id FROM dbo.EventArea WHERE EventId = @Id))
		  = (SELECT COUNT(Id) FROM dbo.Seat WHERE AreaId IN
				(SELECT Id FROM dbo.Area WHERE LayoutId IN (SELECT LayoutId FROM dbo.Event WHERE Id = @Id)))
	SELECT @IsAllAvailableSeats = 1
ELSE
	SELECT @IsAllAvailableSeats = 0 ;