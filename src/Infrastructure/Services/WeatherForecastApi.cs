using Assignment.Application.Common.Interfaces;

namespace Assignment.Infrastructure.Services;
public class WeatherForecastApi : IWeatherForecastApi
{
    public int GetTemperature(string cityName, DateTime time)
    {
        switch (cityName)
        {
            case "Berlin": return 20;
            case "Jena": return 21;
            case "Warsaw": return 19;
            case "Wroclav": return 18;
            default: return 0;
        }
    }
}
