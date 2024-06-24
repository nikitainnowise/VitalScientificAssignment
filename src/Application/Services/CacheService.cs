using Assignment.Application.Common.Interfaces;
using Assignment.Domain.Interfaces;

namespace Assignment.Application.Services;
public class CacheService : ICacheService
{
    private readonly ICacheProvider _cacheProvider;

    public CacheService(ICacheProvider cacheProvider)
    {
        _cacheProvider = cacheProvider;
    }

    public Task<T> GetAsync<T>(string key)
    {
        return _cacheProvider.GetAsync<T>(key);
    }

    public Task SetAsync<T>(string key, T value)
    {
        return _cacheProvider.SetAsync(key, value);
    }

    public Task RemoveAsync(string key)
    {
        return _cacheProvider.RemoveAsync(key);
    }
}
