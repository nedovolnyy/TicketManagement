CREATE PROCEDURE dbo.spEventUpdate
(
	@Id INT = null,
	@Name NVARCHAR(120) = null,
    @EventTime DATETIMEOFFSET = null,
	@Description NVARCHAR(max) = null,
	@LayoutId INT = null,
	@EventEndTime DATETIME2 = null,
	@EventLogoImage NVARCHAR(max) = null
)
AS
BEGIN
	IF EXISTS (SELECT Id FROM dbo.Event WHERE LayoutId = @LayoutId AND Id = @Id)
        UPDATE dbo.Event SET Name = @Name, EventTime = @EventTime, Description = @Description, LayoutId = @LayoutId, EventEndTime = @EventEndTime, EventLogoImage = @EventLogoImage WHERE Id = @Id
	
	ELSE BEGIN
		
		--DELETE EVENT AREA, EVENT SEAT
		DELETE dbo.EventSeat WHERE EventAreaId IN (SELECT Id FROM dbo.EventArea WHERE EventId = @Id)
        DELETE dbo.EventArea WHERE EventId = @Id

		--UPDATE EVENT
		UPDATE dbo.Event SET Name = @Name, EventTime = @EventTime, Description = @Description, LayoutId = @LayoutId, EventEndTime = @EventEndTime, EventLogoImage = @EventLogoImage WHERE Id = @Id
		
		--INSERT INTO EVENT AREA
		INSERT INTO dbo.EventArea (EventId, Description, CoordX, CoordY, Price)
			SELECT @Id, areaTable.areaDescription, areaTable.CoordX, areaTable.CoordY, 0.00 FROM
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
					WHERE E.LayoutId = @LayoutId AND E.Id = @Id
				) as tempTable

	END
END