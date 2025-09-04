namespace FurnitureShop.BL.OtherServices.Interfaces;

public interface ICacheService
{ 
    Task<T> GetOrSetAsync<T>(string key, Func<Task<T>> getData, TimeSpan expiration);
    Task RemoveAsync(string key); 
}