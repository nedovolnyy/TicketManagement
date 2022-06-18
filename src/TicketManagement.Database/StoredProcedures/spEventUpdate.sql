CREATE PROCEDURE dbo.spEventUpdate
(
	@Id INT = null,
	@Name NVARCHAR(120) = null,
    @EventTime DATETIMEOFFSET = null,
	@Description NVARCHAR(max) = null,
	@LayoutId INT = null,
	@EventEndTime DATETIME2 = null
)
AS
BEGIN
	IF EXISTS (SELECT Id FROM dbo.Event WHERE LayoutId = @LayoutId AND Id = @Id)
        UPDATE dbo.Event SET Name = @Name, EventTime = @EventTime, Description = @Description, LayoutId = @LayoutId, EventEndTime = @EventEndTime WHERE Id = @Id
	
	ELSE BEGIN

		DELETE dbo.EventSeat WHERE EventAreaId IN (SELECT Id FROM dbo.EventArea WHERE EventId = @Id)
        DELETE dbo.EventArea WHERE EventId = @Id

		UPDATE dbo.Event SET Name = @Name, EventTime = @EventTime, Description = @Description, LayoutId = @LayoutId, EventEndTime = @EventEndTime WHERE Id = @Id
		
		DECLARE @Enumerator TABLE (tempId INT)

		INSERT INTO @Enumerator SELECT Id FROM dbo.Area WHERE LayoutId = @LayoutId
		DECLARE @tempId INT
		
		WHILE EXISTS (SELECT tempId from @Enumerator) BEGIN
			SELECT TOP 1 @tempId = tempId FROM @Enumerator

			INSERT INTO dbo.EventArea (EventId, Description, CoordX, CoordY, Price)
				SELECT @Id, Description, CoordX, CoordY, 0.00 FROM dbo.Area
					WHERE Id = @tempId
			DECLARE @tempEventAreaId INT = @@IDENTITY

			INSERT INTO dbo.EventSeat (EventAreaId, Row, Number, State)
				SELECT @tempEventAreaId, Row, Number, 0 FROM dbo.Seat
					WHERE AreaId = @tempId
		
			DELETE FROM @Enumerator WHERE tempId = @tempId
		END
	END
END