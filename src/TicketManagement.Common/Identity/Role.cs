using Microsoft.AspNetCore.Identity;

namespace TicketManagement.Common.Identity;

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
