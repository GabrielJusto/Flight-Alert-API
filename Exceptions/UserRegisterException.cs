
using Microsoft.AspNetCore.Identity;

namespace Flight_Alert_API.Exceptions;

public class UserRegisterException(string message, IEnumerable<IdentityError> errors) : Exception(message)
{
    public IEnumerable<IdentityError> Errors { get; } = errors;
}