
using Flight_Alert_API.DTOs.User;
using Flight_Alert_API.Exceptions;
using Flight_Alert_API.Models;
using Flight_Alert_API.Repositories.Interfaces;
using Flight_Alert_API.Services.Interfaces;

namespace Flight_Alert_API.Services.implemetations;

public class UserService(
    IUserRepository userRepository
) : IUserService
{
    
    private readonly IUserRepository _userRepository = userRepository;
    public async Task UpdateUserAsync(UserUpdateDto data)
    {
        User? user = await _userRepository.GetByIdAsync(data.Id);
        if (user == null)
        {
            throw new EntityNotFoundException("User not found");
        }

        if(data.PhoneNumber != null)
            user.PhoneNumber = data.PhoneNumber;

        await _userRepository.UpdateUserAsync(user);
    }
}