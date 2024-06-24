using Caliburn.Micro;
using MediatR;

namespace Assignment.UI.ViewModels;
public class WeatherForecastViewModel : Screen
{
    private readonly ISender _sender;

    private string _name;
    public string Name
    {
        get => _name;
        set
        {
            _name = value;
        }
    }

    public WeatherForecastViewModel(ISender sender)
    {
        _sender = sender;

        Initialize();
    }

    private void Initialize()
    {
        Name = "Weather forecast!";
    }
}
