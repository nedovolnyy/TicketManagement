using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TicketManagement.Common.Identity;

public enum Roles
{
    Administrator,
    EventManager,
    User,
}

public class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.HasData(
        new IdentityRole
        {
            Name = nameof(Roles.Administrator),
            NormalizedName = nameof(Roles.Administrator).Normalize(),
        },
        new IdentityRole
        {
            Name = nameof(Roles.EventManager),
            NormalizedName = nameof(Roles.EventManager).Normalize(),
        },
        new IdentityRole
        {
            Name = nameof(Roles.User),
            NormalizedName = nameof(Roles.User).Normalize(),
        });
    }
}
