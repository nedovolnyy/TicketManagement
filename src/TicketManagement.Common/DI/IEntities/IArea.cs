namespace TicketManagement.Common.DI
{
    public interface IArea : IBaseEntity
    {
        int LayoutId { get; set; }
        string Description { get; set; }
        int CoordX { get; set; }
        int CoordY { get; set; }
    }
}
