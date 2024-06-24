namespace Assignment.Application.Common.Interfaces;

public interface ICacheProvider
{
    Task<T> GetAsync<T>(string key);
    Task SetAsync<T>(string key, T value);
    Task RemoveAsync(string key);
}
