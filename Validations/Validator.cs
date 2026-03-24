
using Flight_Alert_API.Exceptions;

namespace Flight_Alert_API.Validations;

public class Validator(List<IValidation> validations, object? context = null)
{
    private readonly List<IValidation> _validations = validations;
    private readonly object? _context = context;

    /// <exception cref="ValidationException"></exception>
    public async Task Validate()
    {
        List<ValidationError> errors = new();
        foreach(IValidation validation in _validations)
        {
            errors.AddRange(await validation.Validate(_context));
        }

        if(errors.Count != 0)
        {
            throw new ValidationException(errors);
        }
    }
}