using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Seedium.Data;

public class AuthDbContext : IdentityDbContext
{
    public AuthDbContext(DbContextOptions<AuthDbContext> options)
        : base(options) { }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        var readerId = "16417416-6b9d-4271-b4e5-afdd93fdeb65";
        var writerId = "29a21764-dce6-4321-b6cf-46bee625156c";

        var roles = new List<IdentityRole>
        {
            new()
            {
                Id = readerId,
                Name = "Reader",
                NormalizedName = "Reader".ToUpper(),
                ConcurrencyStamp = readerId
            },
            new()
            {
                Id = writerId,
                Name = "Writer",
                NormalizedName = "Writer".ToUpper(),
                ConcurrencyStamp = writerId
            },
        };

        builder.Entity<IdentityRole>().HasData(roles);

        var adminUserId = "1a9a085a-8a89-4097-9fd1-4502288cb381";
        var admin = new IdentityUser
        {
            Id = adminUserId,
            UserName = "admin@seedium.com",
            Email = "admin@seedium.com",
            NormalizedEmail = "admin@seedium.com".ToUpper(),
            NormalizedUserName = "admin@seedium.com".ToUpper(),
        };

        admin.PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(admin, "Admin@1234");

        builder.Entity<IdentityUser>().HasData(admin);

        var adminRoles = new List<IdentityUserRole<string>>
        {
            new() { UserId = adminUserId, RoleId = readerId },
            new() { UserId = adminUserId, RoleId = writerId }
        };

        builder.Entity<IdentityUserRole<string>>().HasData(adminRoles);
    }
}
