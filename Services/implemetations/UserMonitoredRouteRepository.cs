using Flight_Alert_API.Database;
using Flight_Alert_API.Models;
using Flight_Alert_API.Repositories.Interfaces;

using Microsoft.EntityFrameworkCore;

namespace Flight_Alert_API.Services.implemetations;

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
            .Include(umr => umr.MonitoredRoute)
            .ThenInclude(mr => mr.FlightNotifications)
            .ToListAsync();
    }
    
}