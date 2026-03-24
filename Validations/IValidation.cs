namespace Flight_Alert_API.Validations;

public interface IValidation
{
    public Task<List<ValidationError>> Validate(object? context = null);
}