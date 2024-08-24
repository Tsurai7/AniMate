using Domain.Interfaces;
using Backend.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Backend.Infrastructure.Data;

namespace Backend.Infrastructure.Repositories;

public class UserRepository : IGenericRepository<User>
{
    private readonly ApplicationContext _context;

    public UserRepository(ApplicationContext context)
    {
        _context = context;
    }

    public async Task<IList<User>> GetAllAsync() =>
        await _context.Users.ToListAsync();

    public async Task<User?> GetByEmail(string email) =>
        await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
    
    public async Task<User> AddAsync(User user)
    {
        await _context.AddAsync(user);
        await _context.SaveChangesAsync();
        return user;
    }

    public async Task<User> UpdateAsync(User user)
    {
        _context.Update(user);
        await _context.SaveChangesAsync();
        return user;
    }

    public Task<User> DeleteAsync(long id)
    {
        throw new NotImplementedException();
    }
}