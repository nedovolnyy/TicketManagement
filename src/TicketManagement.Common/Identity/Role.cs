using Microsoft.AspNetCore.Identity;

namespace TicketManagement.Common.Identity
{
    public enum Roles
    {
        Administrator,
        EventManager,
        User,
    }

    public class Role : IdentityRole
    {
        public Role()
            : base()
        {
        }

        public Role(string roleName)
            : base(roleName)
        {
        }
    }
}
