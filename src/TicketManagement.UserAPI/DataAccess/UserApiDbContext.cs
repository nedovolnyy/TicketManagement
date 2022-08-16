using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace TicketManagement.UserAPI.DataAccess
{
    public class UserApiDbContext : IdentityDbContext
    {
        public UserApiDbContext(DbContextOptions options)
            : base(options)
        {
        }
    }
}
