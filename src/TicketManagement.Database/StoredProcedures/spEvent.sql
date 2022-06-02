CREATE PROCEDURE dbo.spEvent
(
	@Id INT = null,
	@Name NVARCHAR(120) = null,
    @EventTime DATETIMEOFFSET = null,
	@Description NVARCHAR(max) = null,
	@LayoutId INT = null,
	@Action NVARCHAR(25)
)
AS
BEGIN
    IF @Action = 'Save'
    BEGIN  
        IF NOT EXISTS (SELECT Id, Name, EventTime, Description, LayoutId FROM dbo.Event WHERE Id=@Id)
        BEGIN
            INSERT INTO dbo.Event (Name, EventTime, Description, LayoutId)
            VALUES (@Name, @EventTime, @Description, @LayoutId)
        END
        ELSE
        BEGIN
            UPDATE dbo.Event SET Name = @Name, EventTime = @EventTime, Description = @Description, LayoutId = @LayoutId WHERE Id = @Id
        END
    END
    IF @Action = 'Delete'
    BEGIN
        DELETE dbo.Event WHERE Id=@Id
    END
    IF @Action = 'GetById'
    BEGIN
        SELECT Id, Name, EventTime, Description, LayoutId FROM dbo.Event
		WHERE Id = @Id
    END
    IF @Action = 'GetAll'
    BEGIN
        SELECT Id, Name, EventTime, Description, LayoutId FROM dbo.Event
    END
    IF @Action = 'ForValidate'
    BEGIN
        SELECT Id, Name, EventTime, Description, LayoutId FROM dbo.Event
        WHERE LayoutId = @LayoutId
    END
END