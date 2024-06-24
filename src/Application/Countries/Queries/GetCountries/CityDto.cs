using Assignment.Domain.Entities;

namespace Assignment.Application.Countries.Queries.GetCountries;
public class CityDto
{
    public int Id { get; init; }

    public string? Name { get; init; }

    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<City, CityDto>();
        }
    }
}
