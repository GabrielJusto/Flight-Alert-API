using Flight_Alert_API.Database;
using Flight_Alert_API.Models;
using Flight_Alert_API.Repositories.Interfaces;

namespace Flight_Alert_API.Services;

public class UserMonitoredRouteRepository(
    AppDbContext context
) : IUserMonitoredRouteRepository
{
    private readonly AppDbContext _context = context;

    public async Task Insert(UserMonitoredRoute userMonitoredRoute)
    {
        await _context.UserMonitoredRoutes.AddAsync(userMonitoredRoute);
        await _context.SaveChangesAsync();
    }
    
}