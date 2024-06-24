namespace Assignment.Domain.Entities;

public class Country : BaseEntity
{
    public string? Name { get; set; }

    public IList<City> Cities { get; private set; } = new List<City>();
}
