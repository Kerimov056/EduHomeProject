using EduHome.Core.Interface;

namespace EduHomeDataAccess.Interfaces;

public interface IEntityBaseRepository<T> where T : class, IEntity, new()
{
    Task<IEnumerable<T>> GetAllAsync();
    Task AddAsync(T entity);
    Task UpdateAsync(int id, T entity);
    Task<T> GetByIdAsync(int id);
    Task DeleteAsync(int id);
}
