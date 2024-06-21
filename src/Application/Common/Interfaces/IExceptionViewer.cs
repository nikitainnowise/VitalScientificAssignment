using FluentValidation.Results;
using ValidationException = Assignment.Application.Common.Exceptions.ValidationException;

namespace Assignment.Application.Common.Interfaces;

public interface IExceptionViewer
{
    Task ShowErrors(IEnumerable<ValidationFailure> failures);
    Task ShowErrors(ValidationException validationException);
}
