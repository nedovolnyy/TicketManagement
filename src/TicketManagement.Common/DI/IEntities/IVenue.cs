namespace TicketManagement.Common.DI
{
    public interface IVenue : IBaseEntity
    {
        string Name { get; set; }
        string Description { get; set; }
        string Address { get; set; }
        string Phone { get; set; }
    }
}
