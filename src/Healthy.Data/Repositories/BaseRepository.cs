using System.Linq.Expressions;
using Healthy.Data.Context;
using Healthy.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Healthy.Data.Repositories;

public class BaseRepository<T> : IBaseRepository<T> where T : class
{
    private readonly HealtyDbContext _context;

    public BaseRepository(HealtyDbContext context)
    {
        _context = context;
    }

    public virtual async Task<(List<T> entities, int total)> GetAllAsync(int page, int pageSize,
        Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, bool includeAll = false)
    {
        IQueryable<T> query = _context.Set<T>();

        if (orderBy != null)
        {
            query = orderBy(query);
        }
        
        if (includeAll)
        {
            IncludeAll(query);
        }

        var total = await query.CountAsync();

        var entities = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

        return (entities, total);
    }

    public virtual async Task<(List<T> entities, int total)> GetAllAsync(Expression<Func<T, bool>>? filter, int page,
        int pageSize, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, bool includeAll = false)
    {
        IQueryable<T> query = _context.Set<T>();

        if (filter != null)
        {
            query = query.Where(filter);
        }

        if (orderBy != null)
        {
            query = orderBy(query);
        }
        
        if (includeAll)
        {
            IncludeAll(query);
        }

        var total = await query.CountAsync();

        var entities = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

        return (entities, total);
    }

    public virtual IQueryable<T> GetQueryable()
    {
        return _context.Set<T>().AsQueryable();
    }

    public virtual async Task<T?> GetByIdAsync(object? id, bool includeAll = false)
    {
        var entity = await _context.Set<T>().FindAsync(id);
        if (entity == null)
            throw new ArgumentException("Entity not found");
        
        if (includeAll)
            IncludeAll(_context.Set<T>());
        
        return entity;
    }

    public virtual async Task<T> AddAsync(T entity)
    {
        await _context.Set<T>().AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public virtual async Task<T> UpdateAsync(T entity)
    {
        _context.Set<T>().Update(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public virtual async Task<T> DeleteAsync(object id)
    {
        var entity = await _context.Set<T>().FindAsync(id);
        if (entity == null)
            throw new ArgumentException("Entity not found");

        _context.Set<T>().Remove(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    private void IncludeAll(IQueryable<T> query)
    {
        var properties = typeof(T).GetProperties()
            .Where(x => x.PropertyType.IsClass && x.PropertyType != typeof(string));
        foreach (var property in properties)
        {
            query = query.Include(property.Name);
        }
    }
}