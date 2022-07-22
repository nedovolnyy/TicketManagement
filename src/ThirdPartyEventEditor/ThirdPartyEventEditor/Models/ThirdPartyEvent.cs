using System;

namespace ThirdPartyEventEditor.Models
{
    public class ThirdPartyEvent
    {
        public string Name { get; set; }
        public DateTimeOffset EventTime { get; set; }
        public string Description { get; set; }
        public int LayoutId { get; set; }
        public DateTime EventEndTime { get; set; }
        public string EventLogoImage { get; set; }
    }
}