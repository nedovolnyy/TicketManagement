﻿CREATE PROCEDURE dbo.spEventCountEmptySeats
(
	@Id INT = null,
	@CountEmptySeats INT = null OUTPUT
)
AS

SELECT @CountEmptySeats = COUNT(Id) FROM dbo.EventSeat
	WHERE State = 0 AND
		  EventAreaId IN (SELECT Id FROM dbo.EventArea WHERE EventId = @Id)