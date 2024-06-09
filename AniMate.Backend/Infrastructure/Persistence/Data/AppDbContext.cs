using Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Data;

public sealed class DataContext : IdentityDbContext<User, IdentityRole<long>, long>
{
    public DbSet<User> Users { get; set; } = null!;
    
    public DataContext (DbContextOptions<DataContext> options) : base(options)
    {
        Database.Migrate();
    }
}