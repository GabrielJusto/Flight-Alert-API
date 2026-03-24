
namespace Flight_Alert_API.Validations;

public class ValidationError(
    int code,
    string message,
    string? field = null
)
{
    public string? Field { get; set; } = field;
    public string Message { get; set; } = message;
    public int Code { get; set; } = code;
}