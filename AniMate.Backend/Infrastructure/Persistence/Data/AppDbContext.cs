using Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Data;

public sealed class AppDbContext : IdentityDbContext<User, IdentityRole<long>, long>
{
    public DbSet<User> Users { get; set; } = null!;
    
    public AppDbContext (DbContextOptions<AppDbContext> options) : base(options)
    {
        Database.Migrate();
    }
}