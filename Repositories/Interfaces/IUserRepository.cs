using Flight_Alert_API.Models;

namespace Flight_Alert_API.Repositories.Interfaces;

public interface IUserRepository
{
public Task<int> CreateUserAsync(User user);
public Task UpdateUserAsync(User user);
}