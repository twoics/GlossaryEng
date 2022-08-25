using GlossaryEng.Auth.Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GlossaryEng.Auth.Data;

public class UsersDbContext : IdentityDbContext<IdentityUser>
{
    public UsersDbContext(DbContextOptions<UsersDbContext> options) : base(options)
    {
    }

    public DbSet<RefreshToken> RefreshTokens { get; set; }
}