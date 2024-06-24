using Assignment.Application.Common.Interfaces;

namespace Assignment.Infrastructure.Services;

public class InMemoryCacheProvider : ICacheProvider
{
    private readonly Dictionary<string, object?> _cache = new Dictionary<string, object?>();

    private readonly object _lock = new object();

    public Task<T> GetAsync<T>(string key)
    {
        lock (_lock)
        {
            _cache.TryGetValue(key, out var value);

            return Task.FromResult((T)value!);
        }
    }

    public Task SetAsync<T>(string key, T value)
    {
        lock (_lock)
        {
            if (_cache.ContainsKey(key))
            {
                _cache[key] = value;
            }
            else
            {
                _cache.Add(key, value);
            }

            return Task.CompletedTask;
        }
    }

    public Task RemoveAsync(string key)
    {
        lock (_lock)
        {
            if (_cache.ContainsKey(key))
            {
                _cache.Remove(key);
            }

            return Task.CompletedTask;
        }
    }
}
