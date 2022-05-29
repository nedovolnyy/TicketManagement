using System;
using System.Collections;

namespace TicketManagement.Common.Entities
{
    public class Event : BaseEntity
    {
        public Event()
        {
        }

        public Event(int? id, string name, string description, int? layoutId)
        {
            Id = id;
            Name = name;
            Description = description;
            LayoutId = layoutId;
        }

        public string Name { get; private set; }
        public string Description { get; private set; }
        public int? LayoutId { get; private set; }
        protected override string ForEquals(BaseEntity entity) =>
                Name+Description+LayoutId;
        protected override bool IsNull(BaseEntity entity) =>
                   Name == null
                 & Description == null
                 & LayoutId == null;
        protected override void Validate()
        {
            throw new NotImplementedException();
        }
    }
}
