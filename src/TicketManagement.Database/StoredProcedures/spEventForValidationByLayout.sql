CREATE PROCEDURE dbo.spEventForValidationByLayout
(
	@LayoutId INT = null
)
AS
        SELECT Id, Name, EventTime, Description, LayoutId, EventEndTime, EventLogoImage FROM dbo.Event
			WHERE LayoutId = @LayoutId