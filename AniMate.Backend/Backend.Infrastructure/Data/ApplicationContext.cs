using Backend.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Infrastructure.Data;

public sealed class ApplicationContext : DbContext
{
    public DbSet<User> Users { get; set; } = null!;
    
    public ApplicationContext (DbContextOptions<ApplicationContext> options) : base(options)
    {
        Database.EnsureCreated();
        
        SeedDb();
    }
    
    private void SeedDb()
    {
        if (!Users.Any())
        {
            Users.AddAsync(new User("Ken Kaneki", "test@test.com", "123", 
                "https://i.pinimg.com/originals/e6/94/e9/e694e991d7ec9af6330657eb6ee479f5.jpg",
                new List<string>() {"kizumonogatari-iii-reiketsu-hen", "anatsu-no-taizai-kamigami-no-gekirin"},
                new List<string>() {"kizumonogatari-iii-reiketsu-hen"}));
            
            SaveChangesAsync();
        }
        
    }
}