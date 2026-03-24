
using Flight_Alert_API.Enuns;

namespace Flight_Alert_API.Validations;

public interface IValidationProvider
{
    List<IValidation> GetValidations(ValidationsSelection scenario);

}