
using Flight_Alert_API.Database;
using Flight_Alert_API.Models;
using Flight_Alert_API.Repositories.Interfaces;

namespace Flight_Alert_API.Repositories.Implementations;

public class UserRepository(
    AppDbContext context,
    ILogger<UserRepository> logger) : IUserRepository
{
    private readonly AppDbContext _context = context;
    private readonly ILogger<UserRepository> _logger = logger;
    public async Task<int> CreateUserAsync(User user)
    {
        try
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }
        catch(Exception ex)
        {
            _logger.LogError(ex, "An error occurred while creating a new user with email: {Email}", user.Email);
            throw;
        }

        return user.Id;
    }

    public async Task UpdateUserAsync(User user)
    {
        try
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }
        catch(Exception ex)
        {
            _logger.LogError(ex, "An error occurred while updating user with Id: {Id}", user.Id);
            throw;
        }
    }

    public async Task<User?> GetByIdAsync(int id)
    {
        try
        {
            return await _context.Users.FindAsync(id);
        }
        catch(Exception ex)
        {
            _logger.LogError(ex, "An error occurred while fetching user with Id: {Id}", id);
            throw;
        }
    }
}