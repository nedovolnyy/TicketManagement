CREATE PROCEDURE dbo.spEventGetPriceByEventId
(
	@EventId INT = null,
	@Price DECIMAL(18,2) = null OUTPUT
)
AS
SELECT @Price = Price FROM dbo.EventArea WHERE EventId = @EventId