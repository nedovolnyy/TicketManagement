namespace TicketManagement.Common.DI
{
    public interface ISeat : IBaseEntity
    {
        int AreaId { get; set; }
        int Row { get; set; }
        int Number { get; set; }
    }
}
