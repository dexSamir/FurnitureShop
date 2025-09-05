using System.Linq.Expressions;
using FurnitureShop.Core.Entities.Base;
using FurnitureShop.Core.Repositories;
using FurnitureShop.DAL.Context;
using Microsoft.EntityFrameworkCore;

namespace FurnitureShop.DAL.Repositories;

public class GenericRepository<T>(AppDbContext context) :  IGenericRepository<T> where T : BaseEntity, new() 
{
    protected DbSet<T> Table => context.Set<T>();
    
    // Get All Async
    public async Task<IEnumerable<T>> GetAllAsync(bool asNoTrack = true, Expression<Func<T, bool>>? predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, params string[]? includes)
    {
        IQueryable<T> query = Table;
        if(predicate != null)
            query = query.Where(predicate); 
        
        if(orderBy != null)
            query = orderBy(query);

        return await _includeAndTracking(query, includes, asNoTrack).ToListAsync(); 
    }

    // Get ById Async
    public async Task<T?> GetByIdAsync(int id, bool asNoTrack = true, params string[]? includes)
        => await _includeAndTracking(Table, includes, asNoTrack).FirstOrDefaultAsync(x=> x.Id == id); 

    // Get All Ids Async
    public async Task<IEnumerable<T>> GetByIdsAsync(int[] ids, bool asNoTrack = true, params string[]? includes)
    {
        IQueryable<T> query = Table.Where(x => ids.Contains(x.Id));
        query = _includeAndTracking(query, includes, asNoTrack);
        return await query.ToListAsync();
    }

    // Get Where Async
    public async Task<IEnumerable<T>> GetWhereAsync(Expression<Func<T, bool>> expression, bool asNoTrack = true, params string[]? includes)
        => await _includeAndTracking(Table.Where(expression), includes, asNoTrack).ToListAsync();        

    // Get First Async 
    public async Task<T?> GetFirstAsync(Expression<Func<T, bool>> expression, bool asNoTrack = true, params string[]? includes)
        => await _includeAndTracking(Table, includes, asNoTrack).FirstOrDefaultAsync(expression);   
    
    // Is Exist Async
    public async Task<bool> IsExistAsync(Expression<Func<T, bool>> expression)
        => await Table.AnyAsync(expression);
    
    // Is Exist Async WithId 
    public async Task<bool> IsExistAsync(int id)
        => await Table.AnyAsync(x => x.Id == id);
    
    // Pagination
    public async Task<IEnumerable<T>> GetPagedAsync(Expression<Func<T, bool>>? expression, int pageNumber = 1, bool asNoTrack = true, int pageSize = 30,
        Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, params string[]? includes)
    {
        IQueryable<T> query = Table; 
        if(expression != null) 
            query = query.Where(expression);

        query = _includeAndTracking(query, includes, asNoTrack);
        
        if(orderBy != null)
            query = orderBy(query);
        
        var items = await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();

        return items; 
    }

    // Create 
    public async Task AddAsync(T entity)
        => await Table.AddAsync(entity);

    // Create Range 
    public async Task AddRangeAsync(IEnumerable<T> entities)
        => await Table.AddRangeAsync(entities);

    // Update
    public Task UpdateAsync(T entity)
    {
        Table.Update(entity);
        return Task.CompletedTask;
    }      

    // Hard Delete
    public async Task HardDeleteAsync(int id)
    {
        var entity = await Table.FindAsync(id); 
        Table.Remove(entity);
    }
    
    // Soft Delete
    public async Task SoftDeleteAsync(int id)
    {
        var entity = await Table.FindAsync(id);
        if(!entity.IsDeleted)
            entity.IsDeleted = true;
    }
    
    // Reverse Delete
    public async Task ReverseDeleteAsync(int id)
    {
        var entity = await Table.FindAsync(id);
        if(entity.IsDeleted)
            entity.IsDeleted = false;
    }

    // Hard Delete Range 
    public async Task<int> HardDeleteRangeAsync(int[] ids)
    {
        var entities = await Table.Where(x => ids.Contains(x.Id)).ToListAsync();
        if (!entities.Any())
            return await Task.FromResult(0);

        Table.RemoveRange(entities);
        return await Task.FromResult(entities.Count);
    }

    // Soft Delete Range Async 
    public async Task<int> SoftDeleteRangeAsync(int[] ids)
        => await Table.Where(x => ids.Contains(x.Id))
            .ExecuteUpdateAsync(s => s.SetProperty(x => x.IsDeleted, true));

    // Reverse Delete Range Async 
    public async Task<int> ReverseDeleteRangeAsync(int[] ids)
        => await Table.Where(x => ids.Contains(x.Id))
            .ExecuteUpdateAsync(s => s.SetProperty(x => x.IsDeleted, false));

    // Delete And Save Async 
    public async Task<int> DeleteAndSaveAsync(int id)
        => await Table.Where(x => x.Id == id).ExecuteDeleteAsync();

    // Save Async 
    public async Task<int> SaveAsync()
        => await context.SaveChangesAsync();     
    
    // Private methods
    private IQueryable<T> _includeAndTracking(IQueryable<T> query, string[]? includes, bool asNoTrack)
    {
        if (includes is not null && includes.Length > 0)
        {
            query = _checkIncludes(query, includes);
            if(asNoTrack)
                query = query.AsNoTrackingWithIdentityResolution();
        }
        else 
            if(asNoTrack)
                query = query.AsNoTracking();
        
        return query;
    }

    private IQueryable<T> _checkIncludes(IQueryable<T> query, string[] includes)
    {
        foreach (var include in includes)
            query = query.Include(include);
        return query;
    }
}