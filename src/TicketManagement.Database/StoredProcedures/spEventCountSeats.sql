CREATE PROCEDURE dbo.spEventCountSeats
(
	@LayoutId INT = null,
	@CountSeats INT = null OUTPUT
)
AS

SELECT @CountSeats = COUNT(Id) FROM dbo.Seat
	WHERE AreaId IN (SELECT Id FROM dbo.Area WHERE LayoutId = @LayoutId)