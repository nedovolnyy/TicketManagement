CREATE PROCEDURE dbo.spEventInsert
(
	@Name NVARCHAR(120) = null,
    @EventTime DATETIMEOFFSET = null,
	@Description NVARCHAR(max) = null,
	@LayoutId INT = null,
	@EventEndTime DATETIME2 = null
)
AS
BEGIN
		INSERT INTO dbo.Event (Name, EventTime, Description, LayoutId, EventEndTime)
			VALUES (@Name, @EventTime, @Description, @LayoutId, @EventEndTime)
		DECLARE @tempEventId INT = @@IDENTITY
		DECLARE @Enumerator TABLE (tempId INT)

		INSERT INTO @Enumerator SELECT Id FROM dbo.Area WHERE LayoutId = @LayoutId
		DECLARE @tempId INT
		
		WHILE EXISTS (SELECT tempId from @Enumerator) BEGIN
			SELECT TOP 1 @tempId = tempId FROM @Enumerator

			INSERT INTO dbo.EventArea (EventId, Description, CoordX, CoordY, Price)
				SELECT @tempEventId, Description, CoordX, CoordY, 0.00 FROM dbo.Area
					WHERE Id = @tempId
			DECLARE @tempEventAreaId INT = @@IDENTITY

			INSERT INTO dbo.EventSeat (EventAreaId, Row, Number, State)
				SELECT @tempEventAreaId, Row, Number, 0 FROM dbo.Seat
					WHERE AreaId = @tempId
		
			DELETE FROM @Enumerator WHERE tempId = @tempId
		END
END