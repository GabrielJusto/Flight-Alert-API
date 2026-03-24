

using Flight_Alert_API.DTOs;
using Flight_Alert_API.Models;
using Flight_Alert_API.Repositories.Interfaces;

namespace Flight_Alert_API.Validations.Auth;

public class PhoneNumberAlreadyExistsValidation(
    IUserRepository userRepository,
    ILogger<PhoneNumberAlreadyExistsValidation> logger
) : IValidation
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly ILogger<PhoneNumberAlreadyExistsValidation> _logger = logger;
    public async Task<List<ValidationError>> Validate(object? context = null)
    {
        try
        {
            _logger.LogInformation("Validating phone number with input: {data}", context);
            if(context is not UserRegisterRequestDto userRequest)
            {
                throw new ArgumentException("Context must be a UserRegisterRequestDto representing the phone number.");
            }

            User? user = await _userRepository.GetUserByPhoneNumberAsync(userRequest.PhoneNumber);
            if(user != null)
            {
                _logger.LogInformation("User found with phone number {phoneNumber}: UserId: {id}", userRequest.PhoneNumber, user.Id);
                return new List<ValidationError>
                {
                    new(0, "Phone number already in use.", "PhoneNumber")
                };
            }
        }
        catch(Exception ex)
        {
            _logger.LogError(ex, "An error occurred while validating the phone number with input: {data}", context);
        }
        var errors = new List<ValidationError>();

        return errors;
    }
}