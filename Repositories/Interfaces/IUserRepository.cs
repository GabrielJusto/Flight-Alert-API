using Flight_Alert_API.Models;

namespace Flight_Alert_API.Repositories.Interfaces;

public interface IUserRepository
{
    public Task<int> CreateUserAsync(User user);
    public Task<User?> GetByIdAsync(int id);
    public Task UpdateUserAsync(User user);
}