using System;

namespace TicketManagement.Common.Entities
{
    public class Venue : BaseEntity
    {
        public Venue(int id, string name, string description, string address, string phone)
        {
            Id = id;
            Name = name;
            Description = description;
            Address = address;
            Phone = phone;
        }

        public string Name { get; private set; }
        public string Description { get; private set; }
        public string Address { get; private set; }
        public string Phone { get; private set; }
    }
}
