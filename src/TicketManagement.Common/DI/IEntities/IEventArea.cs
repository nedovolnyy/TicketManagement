namespace TicketManagement.Common.DI
{
    public interface IEventArea : IBaseEntity
    {
        public int EventId { get; set; }
        public string Description { get; set; }
        public int CoordX { get; set; }
        public int CoordY { get; set; }
        public decimal Price { get; set; }
    }
}
