using System.Linq.Expressions;
using FurnitureShop.Core.Entities.Base;

namespace FurnitureShop.Core.Repositories;

public interface IGenericRepository<T>  where T : BaseEntity, new()
{
    // Get All  
    Task<IEnumerable<T>> GetAllAsync(bool asNoTrack = true,  Expression<Func<T, bool>>? predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, params string[]? includes);
    
    // GetById
    Task<T?> GetByIdAsync(int id, bool asNoTrack = true,  params string[]? includes); 
    Task<IEnumerable<T>> GetByIdsAsync(int[] ids, bool asNoTrack = true,  params string[]? includes);
    
    // Get Where Async
    Task<IEnumerable<T>> GetWhereAsync(Expression<Func<T, bool>> expression, bool asNoTrack = true, params string[]? includes);
    
    // Get First Async 
    Task<T?> GetFirstAsync(Expression<Func<T, bool>> expression, bool asNoTrack = true, params string[]? includes);
    
    // Is Exist Async
    Task<bool> IsExistAsync(Expression<Func<T, bool>> expression);
    Task<bool> IsExistRangeAsync(int[] ids);
    Task<bool> IsExistAsync(int id); 
    
    // Pagination
    Task<IEnumerable<T>> GetPagedAsync(Expression<Func<T, bool>>? expression, int pageNumber = 1, bool asNoTrack = true,
        int pageSize = 30,Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, params string[]? includes);
    
    // Create 
    Task AddAsync(T entity);
    Task AddRangeAsync(IEnumerable<T> entities); 

    // Update
    Task UpdateAsync(T entity);
    
    // Delete Async
    Task HardDeleteAsync(int id);
    Task SoftDeleteAsync(int id);
    Task ReverseDeleteAsync(int id);
    
    // Delete Range Async 
    Task<int>  HardDeleteRangeAsync(int[] ids);
    Task<int> SoftDeleteRangeAsync(int[] ids); 
    Task<int> ReverseDeleteRangeAsync(int[] ids);
    
    // // Delete 
    // void HardDelete(T entity);
    // void SoftDelete(T entity);
    // void ReverseDelete(T entity);
    //
    // // Delete Range 
    // void HardDeleteRange(IEnumerable<T> entities);
    // void SoftDeleteRange(IEnumerable<T> entities);
    // void ReverseDeleteRange(IEnumerable<T> entities);

    // Save
    Task<int> DeleteAndSaveAsync(int id);
    Task<int> SaveAsync(); 
    
}