﻿using System.Data;
using System.Linq.Expressions;
using Healthy.Data.Context;
using Healthy.Domain.Entities;
using Healthy.Domain.Interfaces;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace Healthy.Data.Repositories;

public class BaseRepository<T> : IBaseRepository<T> where T : class, IEntity
{
    private readonly HealtyDbContext _context;

    public BaseRepository(HealtyDbContext context)
    {
        _context = context;
    }

    public virtual async Task<(List<T> entities, int total)> GetAllAsync(int? page = null, int? pageSize = null,
        Expression<Func<T, object>>? orderBy = null, bool includeAll = false)
    {
        IQueryable<T> query = _context.Set<T>();

        if (orderBy != null)
        {
            query = query.OrderBy(orderBy);
        }

        if (includeAll)
        {
            IncludeAll(ref query);
        }

        var total = await query.CountAsync();

        List<T> entities;
        if (page != null && pageSize != null)
            entities = await query.Skip((int)((page - 1) * pageSize)).Take((int)pageSize).ToListAsync();
        else
            entities = await query.ToListAsync();

        return (entities, total);
    }

    public virtual async Task<(List<T> entities, int total)> GetAllAsync(Expression<Func<T, bool>>? filter = null,
        int? page = null,
        int? pageSize = null, Expression<Func<T, object>>? orderBy = null, bool includeAll = false)
    {
        IQueryable<T> query = _context.Set<T>();

        if (filter != null)
        {
            query = query.Where(filter);
        }

        if (orderBy != null)
        {
            query = query.OrderBy(orderBy);
        }

        if (includeAll)
        {
            IncludeAll(ref query);
        }

        var total = await query.CountAsync();

        List<T> entities;
        if (page != null && pageSize != null)
            entities = await query.Skip((int)((page - 1) * pageSize)).Take((int)pageSize).ToListAsync();
        else
            entities = await query.ToListAsync();

        return (entities, total);
    }

    public virtual IQueryable<T> GetQueryable()
    {
        return _context.Set<T>().AsQueryable();
    }

    public virtual async Task<T?> GetByIdAsync(int id, bool includeAll = false)
    {
        var query = _context.Set<T>().AsQueryable();
        if (includeAll)
        {
            IncludeAll(ref query);
        }

        var entity = await query.FirstOrDefaultAsync(x => x.Id == id);
        if (entity == null)
            throw new ArgumentException("Entity not found");

        return entity;
    }

    public virtual async Task<T> AddAsync(T entity)
    {
        try
        {
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
        catch (DbUpdateException e)
        {
            if (e.InnerException is SqliteException sqliteException)
            {
                switch (sqliteException)
                {
                    case { SqliteExtendedErrorCode: 2067 }:
                        throw new ConstraintException(
                            "Não é possível inserir o registro pois há conflito de chave única");
                    default:
                        throw;
                }
            }

            throw;
        }
    }

    public virtual async Task<T> UpdateAsync(T entity)
    {
        try
        {
            _context.Set<T>().Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
        catch (DbUpdateException e)
        {
            if (e.InnerException is SqliteException sqliteException)
            {
                switch (sqliteException)
                {
                    case { SqliteExtendedErrorCode: 2067 }:
                        throw new ConstraintException(
                            "Não é possível atualizar o registro pois há conflito de chave única");
                    default:
                        throw;
                }
            }

            throw;
        }
    }

    public virtual async Task<T> DeleteAsync(int id)
    {
        try
        {
            var entity = await GetByIdAsync(id);
            if (entity == null)
                throw new ArgumentException("Entity not found");

            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
        catch (DbUpdateException e)
        {
            if (e.InnerException is SqliteException sqliteException)
            {
                switch (sqliteException)
                {
                    case { SqliteExtendedErrorCode: 1811 }:
                        throw new ConstraintException(
                            "Não é possível excluir o registro pois há conflito em outros registros");
                    default:
                        throw;
                }
            }

            throw;
        }
    }

    private static void IncludeAll(ref IQueryable<T> query)
    {
        var properties = typeof(T).GetProperties();
        query = properties.Where(property => property.PropertyType.GetInterfaces().Contains(typeof(IEntity)))
            .Aggregate(query, (current, property) => current.Include(property.Name));
    }
}