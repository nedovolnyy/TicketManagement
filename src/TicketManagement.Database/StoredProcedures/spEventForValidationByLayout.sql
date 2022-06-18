CREATE PROCEDURE dbo.spEventForValidationByLayout
(
	@LayoutId INT = null
)
AS
        SELECT Id, Name, EventTime, Description, LayoutId FROM dbo.Event
			WHERE LayoutId = @LayoutId