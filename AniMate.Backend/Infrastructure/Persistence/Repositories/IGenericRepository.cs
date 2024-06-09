namespace Persistence.Repositories;

public interface IGenericRepository<T> : IDisposable
{
    Task<T> GetAllAsync(CancellationToken cancellationToken);

    Task<T> GetByParamAsync(dynamic param, CancellationToken cancellationToken);

    Task<T> AddAsync(T model, CancellationToken cancellationToken);

    Task<T> UpdateAsync(T model, CancellationToken cancellationToken);

    Task<T> DeleteAsync(long id, CancellationToken cancellationToken);
}