namespace TicketManagement.Common.DI
{
    public interface ILayout : IBaseEntity
    {
        string Name { get; set; }
        int VenueId { get; set; }
        string Description { get; set; }
    }
}
