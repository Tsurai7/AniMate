using Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Data;

public sealed class ApplicationContext : IdentityDbContext<User, IdentityRole<long>, long>
{
    public DbSet<User> Users { get; set; } = null!;
    
    public ApplicationContext (DbContextOptions<ApplicationContext> options) : base(options)
    {
        Database.Migrate();
    }
}