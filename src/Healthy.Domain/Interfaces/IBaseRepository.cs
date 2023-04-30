using System.Linq.Expressions;

namespace Healthy.Domain.Interfaces;

public interface IBaseRepository<TEntity> where TEntity : class
{
    Task<(List<TEntity> entities, int total)> GetAllAsync(int page, int pageSize,
        Expression<Func<TEntity, object>>? orderBy = null, bool includeAll = false);

    Task<(List<TEntity> entities, int total)> GetAllAsync(Expression<Func<TEntity, bool>>? filter, int page,
        int pageSize,
        Expression<Func<TEntity, object>>? orderBy = null, bool includeAll = false);

    IQueryable<TEntity> GetQueryable();
    Task<TEntity?> GetByIdAsync(int id, bool includeAll = false);
    Task<TEntity> AddAsync(TEntity entity);
    Task<TEntity> UpdateAsync(TEntity entity);
    Task<TEntity> DeleteAsync(int id);
}