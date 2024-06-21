using Assignment.Application.Common.Exceptions;
using Assignment.Application.Common.Interfaces;
using Assignment.UI.Controls;
using Caliburn.Micro;
using FluentValidation.Results;

namespace Assignment.UI.Services;
public class ExceptionViewer : IExceptionViewer
{
    private readonly IWindowManager _windowManager;

    public ExceptionViewer(IWindowManager windowManager)
    {
        _windowManager = windowManager;
    }

    public Task ShowErrors(IEnumerable<ValidationFailure> failures)
    {
        return _windowManager.ShowDialogAsync(
            new ErrorPopupViewModel(failures.Select(f => f.ErrorMessage).ToList()));
    }

    public Task ShowErrors(ValidationException validationException)
    {
        return _windowManager.ShowDialogAsync(
            new ErrorPopupViewModel(validationException.Errors.SelectMany(e => e.Value).ToList()));
    }
}
