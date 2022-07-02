using System;

namespace TicketManagement.Common.DI
{
    public interface IEvent : IBaseEntity
    {
        string Name { get; set; }
        DateTimeOffset EventTime { get; set; }
        string Description { get; set; }
        int LayoutId { get; set; }
        DateTime EventEndTime { get; set; }
        string EventLogoImage { get; set; }
    }
}
