using Assignment.Domain.Entities;

namespace Assignment.Application.Countries.Queries.GetCountries;
public class CountryDto
{
    public CountryDto()
    {
        Items = Array.Empty<CityDto>();
    }

    public int Id { get; init; }

    public string? Name { get; init; }

    public IList<CityDto> Items { get; init; }

    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Country, CountryDto>();
        }
    }
}
