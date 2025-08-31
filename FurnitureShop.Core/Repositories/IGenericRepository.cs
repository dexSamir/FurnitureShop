using System.Linq.Expressions;
using FurnitureShop.Core.Entities.Base;

namespace FurnitureShop.Core.Repositories;

public interface IGenericRepository<T>  where T : BaseEntity, new()
{
    Task<IEnumerable<T>> GetAllAsync(bool asNoTrack = true,  Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, params string[] includes); 
    
    Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null  ,params string[] includes);
    
    
}