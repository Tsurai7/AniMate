using Domain.Models;
using Persistence.Data;

namespace Persistence.Repositories;

public class UserRepository : IGenericRepository<User>
{
    private readonly AppDbContext _context;
    
    private bool _disposed = false;

    public UserRepository(AppDbContext context)
    {
        _context = context;
    }

    public Task<User> GetAllAsync(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<User> GetByParamAsync(dynamic param, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<User> AddAsync(User model, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<User> UpdateAsync(User model, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<User> DeleteAsync(long id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
    
    public void Dispose()
    {
        throw new NotImplementedException();
    }
}