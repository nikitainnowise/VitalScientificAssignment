using System.Windows.Input;
using Caliburn.Micro;

namespace Assignment.UI.Controls;
public class ErrorPopupViewModel : Screen
{
    private List<string> _exceptionMessage;
    public List<string> ExceptionMessages
    {
        get => _exceptionMessage;
        set
        {
            _exceptionMessage = value;
            NotifyOfPropertyChange(() => _exceptionMessage);
        }
    }

    public ICommand CloseCommand { get; init; }

    public ErrorPopupViewModel(List<string> errors)
    {
        ExceptionMessages = errors;

        CloseCommand = new RelayCommand(CloseCommandExecute);
    }

    private async void CloseCommandExecute(object obj)
    {
        try
        {
            await TryCloseAsync();
        }
        catch (Exception)
        {
           //TODO: Add logging as we have async void here
        }
    }
}
