using System;

namespace TicketManagement.Common.Entities
{
    public class Venue : BaseEntity
    {
        public Venue()
        {
        }

        public Venue(int? id, string description, string address, string phone)
        {
            Id = id;
            Description = description;
            Address = address;
            Phone = phone;
        }

        public string Description { get; private set; }
        public string Address { get; private set; }
        public string Phone { get; private set; }
        protected override string ForEquals(BaseEntity entity) =>
                Description+Address+Phone;
        protected override bool IsNull(BaseEntity entity) =>
                   Description == null
                 & Address == null
                 & Phone == null;

        protected override void Validate()
        {
            throw new NotImplementedException();
        }
    }
}
