CREATE PROCEDURE dbo.spEventInsert
(
	@Name NVARCHAR(120) = null,
    @EventTime DATETIMEOFFSET = null,
	@Description NVARCHAR(max) = null,
	@LayoutId INT = null,
	@EventEndTime DATETIME2 = null,
	@EventLogoImage NVARCHAR(max) = null,
	@Price DECIMAL(18,2) = null
)
AS
BEGIN
		--INSERT INTO EVENT
		INSERT INTO dbo.Event (Name, EventTime, Description, LayoutId, EventEndTime, EventLogoImage)
			VALUES (@Name, @EventTime, @Description, @LayoutId, @EventEndTime, @EventLogoImage)
		DECLARE @eventId INT = @@IDENTITY

		--INSERT INTO EVENT AREA
		INSERT INTO dbo.EventArea (EventId, Description, CoordX, CoordY, Price)
			SELECT @eventId, areaTable.areaDescription, areaTable.CoordX, areaTable.CoordY, @Price FROM
				(SELECT A.Description AS areaDescription, A.CoordX, A.CoordY FROM Layout L
					Join Area A ON A.LayoutId=L.Id                  
					WHERE L.Id = @LayoutId
				)  as areaTable

		--INSERT INTO EVENT SEAT
		INSERT INTO dbo.EventSeat (EventAreaId, Row, Number, State)
			SELECT tempTable.EventAreaId,  tempTable.Row, tempTable.Number, 0 FROM
				(SELECT DISTINCT E.Id AS EventId, EA.Id AS EventAreaId, S.Row, S.Number FROM (SELECT Id, LayoutId FROM dbo.Event) E 
					join (SELECT Id, LayoutId FROM dbo.Area) A ON E.LayoutId = A.LayoutId
					join (SELECT Id, EventId FROM dbo.EventArea) EA ON E.Id = EA.EventId
					join (SELECT AreaId, Row, Number FROM dbo.Seat) S ON A.Id = S.AreaId
					WHERE E.LayoutId = @LayoutId AND E.Id = @eventId
				) as tempTable
END