using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TicketManagement.Common.Identity;

namespace TicketManagement.UserAPI.DataAccess;

public partial class UserApiDbContext : IdentityDbContext<User, Role, string>
{
    public UserApiDbContext(DbContextOptions<UserApiDbContext> options)
        : base(options)
    {
        ConnectionString = Database.GetConnectionString();
    }

    public string ConnectionString { get; private set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ApplyConfiguration(new RoleConfiguration());
    }
}
