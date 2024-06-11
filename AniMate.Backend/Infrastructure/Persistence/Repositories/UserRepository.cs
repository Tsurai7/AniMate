using Domain.Interfaces;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;

namespace Persistence.Repositories;

public class UserRepository : IGenericRepository<User>
{
    private readonly ApplicationContext _context;

    public UserRepository(ApplicationContext context)
    {
        _context = context;
    }

    public async Task<IList<User>> GetAllAsync(CancellationToken cancellationToken) =>
        await _context.Users.ToListAsync(cancellationToken);
    
    public Task<User> GetByParamAsync(dynamic param, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<User> AddAsync(User user, CancellationToken cancellationToken)
    {
        await _context.AddAsync(user, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return user;
    }

    public async Task<User> UpdateAsync(User user, CancellationToken cancellationToken)
    {
        _context.Update(user);
        await _context.SaveChangesAsync(cancellationToken);
        return user;
    }

    public Task<User> DeleteAsync(long id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}