using System.Windows.Input;
using Assignment.Application.Countries.Queries.GetCountries;
using Assignment.Application.WeatherForecast.Queries;
using Caliburn.Micro;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Assignment.UI.ViewModels;
public class WeatherForecastViewModel : Screen
{
    private const string NoDataSign = "-";

    private readonly ISender _sender;
    private readonly ILogger _logger;

    private string _temperature = NoDataSign;
    public string Temperature
    {
        get => _temperature;
        set
        {
            _temperature = value;
            NotifyOfPropertyChange(() => Temperature);
        }
    }

    private string _skyCover = NoDataSign;
    public string SkyCover
    {
        get => _skyCover;
        set
        {
            _skyCover = value;
            NotifyOfPropertyChange(() => SkyCover);
        }
    }

    private IList<CountryDto> _countries;
    public IList<CountryDto> Countries
    {
        get
        {
            return _countries;
        }
        set
        {
            _countries = value;
            NotifyOfPropertyChange(() => Countries);
        }
    }

    private CountryDto _selectedCountry;
    public CountryDto SelectedCountry
    {
        get
        {
            return _selectedCountry;
        }
        set
        {
            _selectedCountry = value;
            SelectedCity = SelectedCountry.Cities.First();
            NotifyOfPropertyChange(() => SelectedCountry);
        }
    }

    private CityDto _selectedCity;
    public CityDto SelectedCity
    {
        get
        {
            return _selectedCity;
        }
        set
        {
            _selectedCity = value;
            SetDefaultForecastData();
            NotifyOfPropertyChange(() => SelectedCity);

            if (RefreshWeatherCommand.CanExecute(null))
            {
                RefreshWeatherCommand.Execute(null);
            }
        }
    }

    public ICommand RefreshWeatherCommand { get; private set; }

    public WeatherForecastViewModel(
        ISender sender,
        ILogger<WeatherForecastViewModel> logger)
    {
        _sender = sender;
        _logger = logger;

        Initialize();
    }

    private void Initialize()
    {
        RefreshWeatherCommand = new RelayCommand(TryRefreshWeather);

        TryLoadCountriesList();
    }

    private async void TryRefreshWeather(object obj)
    {
        try
        {
            await Task.Delay(1000);

            var temperature = await _sender.Send(new GetWeatherForecastQuery(SelectedCity.Name));

            Temperature = temperature.ToString();
            SkyCover = temperature > 20 ? "Sunny" : "Cloudy";
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "TryRefreshWeather");
        }
    }

    private async void TryLoadCountriesList()
    {
        try
        {
            Countries = await _sender.Send(new GetCountriesQuery());

            if (Countries.Any())
            {
                SelectedCountry = Countries.First();
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "TryLoadCountriesList");
        }
    }

    private void SetDefaultForecastData()
    {
        Temperature = NoDataSign;
        SkyCover = NoDataSign;
    }
}
