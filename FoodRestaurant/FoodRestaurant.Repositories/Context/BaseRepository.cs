using FoodRestaurant.Abstractions.Context;
using FoodRestaurant.Domain.BaseEntities;

using Microsoft.EntityFrameworkCore;

namespace FoodRestaurant.Domain.Context;

public abstract class BaseRepository<T> : IBaseRepository<T> where T : TEntity
{
    protected readonly FoodRestaurantContext _dbContext;

    protected BaseRepository(FoodRestaurantContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddAsync(T entity)
    {
        await _dbContext.Set<T>().AddAsync(entity);
    }

    public async Task AddRangeAsync(IEnumerable<T> entities)
    {
        await _dbContext.Set<T>().AddRangeAsync(entities);
    }

    public Task DeleteAsync(T entity)
    {
        _dbContext.Set<T>().Remove(entity);

        return Task.FromResult(Task.CompletedTask);
    }

    public Task DeleteRangeAsync(IEnumerable<T> entities)
    {
        _dbContext.Set<T>().RemoveRange(entities);

        return Task.FromResult(Task.CompletedTask);
    }

    public virtual async Task<T> GetAsync(int id)
    {
        return await _dbContext.Set<T>().FindAsync(id);
    }

    public virtual async Task<IEnumerable<T>> GetAsync()
    {
        return await _dbContext.Set<T>().ToListAsync();
    }
}
