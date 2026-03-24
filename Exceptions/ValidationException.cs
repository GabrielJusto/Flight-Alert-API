
using Flight_Alert_API.Validations;

namespace Flight_Alert_API.Exceptions;

public class ValidationException(IEnumerable<ValidationError> errors) : Exception()
{
    public IEnumerable<ValidationError> Errors { get; } = errors;
}