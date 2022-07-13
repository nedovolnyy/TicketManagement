using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace TicketManagement.Common.Identity
{
    public class ChangeRole
    {
        public ChangeRole()
        {
            AllRoles = new List<Role>();
            UserRoles = new List<string>();
        }

        public string UserId { get; set; }
        public string UserEmail { get; set; }
        public List<Role> AllRoles { get; set; }
        public IList<string> UserRoles { get; set; }
    }
}
