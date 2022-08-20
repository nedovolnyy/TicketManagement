using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TicketManagement.Common.Identity
{
    public static class UserRoles
    {
        public const string Administrator = "Administrator";

        public const string EventManager = "EventManager";
    }

    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.HasData(
            new IdentityRole
            {
                Name = "Administrator",
                NormalizedName = "ADMINISTRATOR",
            },
            new IdentityRole
            {
                Name = "EventManager",
                NormalizedName = "EVENTMANAGER",
            });
        }
    }
}
