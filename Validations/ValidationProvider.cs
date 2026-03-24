using Flight_Alert_API.Enuns;
using Flight_Alert_API.Validations.Auth;

namespace Flight_Alert_API.Validations;

public class ValidationProvider : IValidationProvider
{
    private readonly IServiceProvider _serviceProvider;

    public ValidationProvider(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public List<IValidation> GetValidations(ValidationsSelection scenario)
    {
        switch(scenario)
        {
            case ValidationsSelection.UserRegister:
                return GetUserRegisterValidations();
            default:
                return new List<IValidation>();
        }

    }

    private List<IValidation> GetUserRegisterValidations(object? context = null)
    {
        return new List<IValidation>
        {
            _serviceProvider.GetService<PhoneNumberAlreadyExistsValidation>()!
        };
    }
}