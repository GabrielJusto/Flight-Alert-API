using Flight_Alert_API.DTOs.User;

namespace Flight_Alert_API.Services.Interfaces;

public interface IUserService
{
    public Task UpdateUserAsync(UserUpdateDto data);
}