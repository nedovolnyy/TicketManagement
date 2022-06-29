namespace TicketManagement.Common.DI
{
    public interface IEventSeat : IBaseEntity
    {
        int EventAreaId { get; set; }
        int Row { get; set; }
        int Number { get; set; }
        int State { get; set; }
    }
}
