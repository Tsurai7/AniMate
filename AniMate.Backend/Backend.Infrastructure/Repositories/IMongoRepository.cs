namespace Backend.Infrastructure.Repositories;

public interface IMongoRepository<T>
{
    Task<T> GetByIdAsync(string id);
    Task AddAsync(T entity);
    Task UpdateOneAsync(T entity);
    Task DeleteAsync(string id);
}