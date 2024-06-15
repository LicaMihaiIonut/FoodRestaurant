using FoodRestaurant.Domain.BaseEntities;

namespace FoodRestaurant.Abstractions.Context;

public interface IBaseRepository<T> where T : TEntity
{
    Task<T> GetAsync(int id);
    Task<IEnumerable<T>> GetAsync();
    Task AddAsync(T entity);
    Task AddRangeAsync(IEnumerable<T> entities);
    Task DeleteAsync(T entity);
    Task DeleteRangeAsync(IEnumerable<T> entities);
}
