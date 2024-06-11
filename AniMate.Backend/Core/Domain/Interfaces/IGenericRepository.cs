namespace Domain.Interfaces;

public interface IGenericRepository<T>
{
    Task<IList<T>> GetAllAsync();

    Task<T> AddAsync(T model);
    
    Task<T> DeleteAsync(long id);

    Task<T> UpdateAsync(T model);
}