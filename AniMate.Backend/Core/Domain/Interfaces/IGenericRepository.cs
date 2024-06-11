namespace Domain.Interfaces;

public interface IGenericRepository<T>
{
    Task<IList<T>> GetAllAsync(CancellationToken cancellationToken);

    Task<T> GetByParamAsync(dynamic param, CancellationToken cancellationToken);

    Task<T> AddAsync(T model, CancellationToken cancellationToken);

    Task<T> UpdateAsync(T model, CancellationToken cancellationToken);

    Task<T> DeleteAsync(long id, CancellationToken cancellationToken);
}