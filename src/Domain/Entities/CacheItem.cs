namespace Assignment.Domain.Entities;

public class CacheItem
{
    public string Key { get; set; }
    public object Value { get; set; }

    public CacheItem(string key, string value)
    {
        Key = key;
        Value = value;
    }
}
