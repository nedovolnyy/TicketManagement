CREATE PROCEDURE dbo.spEventCountSeats
(
	@LayoutId INT = null
)
AS

SELECT COUNT(Id) FROM dbo.Seat
	WHERE AreaId IN (SELECT Id FROM dbo.Area WHERE LayoutId = @LayoutId)