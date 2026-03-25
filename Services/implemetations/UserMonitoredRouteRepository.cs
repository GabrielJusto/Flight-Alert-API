using Flight_Alert_API.Database;
using Flight_Alert_API.Models;
using Flight_Alert_API.Repositories.Interfaces;

using Hangfire.Logging;

using Microsoft.EntityFrameworkCore;

namespace Flight_Alert_API.Services.implemetations;

public class UserMonitoredRouteRepository(
    ILogger<UserMonitoredRouteRepository> logger,
    AppDbContext context
) : IUserMonitoredRouteRepository
{
    private readonly AppDbContext _context = context;
    private readonly ILogger<UserMonitoredRouteRepository> _logger = logger;

    public async Task Insert(UserMonitoredRoute userMonitoredRoute)
    {
        await _context.UserMonitoredRoutes.AddAsync(userMonitoredRoute);
        await _context.SaveChangesAsync();
    }

    public async Task<List<UserMonitoredRoute>> GetAllForSendingAlertsAsync()
    {
        return await _context.UserMonitoredRoutes
            .Include(umr => umr.User)
            .Include(umr => umr.MonitoredRoute)
            .ThenInclude(mr => mr.OriginAirport)
            .Include(umr => umr.MonitoredRoute)
            .ThenInclude(mr => mr.DestinationAirport)
            .ToListAsync();
    }

    public async Task<List<UserMonitoredRoute>> GetAllByUserIdAsync(int userId)
    {
        return await _context.UserMonitoredRoutes
            .Where(umr => umr.UserId == userId)
            .Include(umr => umr.MonitoredRoute)
            .ThenInclude(mr => mr.OriginAirport)
            .Include(umr => umr.MonitoredRoute)
            .ThenInclude(mr => mr.DestinationAirport)
            .Include(umr => umr.FlightNotifications)
            .ToListAsync();
    }

    public async Task<UserMonitoredRoute?> GetToDeleteAsync(int id, int userId)
    {
        try
        {
            return await _context.UserMonitoredRoutes
                .Where(umr => umr.MonitoredRouteId == id && umr.UserId == userId)
                .FirstOrDefaultAsync();

        }
        catch(Exception ex)
        {
            _logger.LogError(ex, "An error occurred while retrieving UserMonitoredRoute:{@data}", new { id, userId });
            throw;
        }
    }

    public async Task Delete(UserMonitoredRoute userMonitoredRoute)
    {
        try
        {
            _context.UserMonitoredRoutes.Remove(userMonitoredRoute);
            await _context.SaveChangesAsync();
        }
        catch(Exception ex)
        {
            _logger.LogError(ex, "An error occurred while deleting UserMonitoredRoute: {@data}", userMonitoredRoute);
            throw;
        }
    }

}