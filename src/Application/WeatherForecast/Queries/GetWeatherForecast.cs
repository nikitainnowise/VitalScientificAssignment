using Assignment.Application.Common.Interfaces;
using Assignment.Application.Common.Security;

namespace Assignment.Application.WeatherForecast.Queries;
[Authorize]
public record GetWeatherForecastQuery(string CityName) : IRequest<int>;

public class GetWeatherForecastQueryHandler : IRequestHandler<GetWeatherForecastQuery, int>
{
    private readonly IWeatherForecastApi _weatherForecastApi;

    public GetWeatherForecastQueryHandler(IWeatherForecastApi weatherForecastApi)
    {
        _weatherForecastApi = weatherForecastApi;
    }

    public Task<int> Handle(GetWeatherForecastQuery request, CancellationToken cancellationToken)
    {
        return Task.FromResult(_weatherForecastApi.GetTemperature(request.CityName, DateTime.Now));
    }
}
