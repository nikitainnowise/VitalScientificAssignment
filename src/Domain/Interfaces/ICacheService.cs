﻿namespace Assignment.Domain.Interfaces;

public interface ICacheService
{
    Task<T> GetAsync<T>(string key);
    Task SetAsync<T>(string key, T value);
    Task RemoveAsync(string key);
}
