
using Flight_Alert_API.Database;
using Flight_Alert_API.Models;
using Flight_Alert_API.Repositories.Interfaces;

using Microsoft.EntityFrameworkCore;

namespace Flight_Alert_API.Repositories.Implementations;

public class FlightNotificationRepository(
    AppDbContext context,
    ILogger<FlightNotificationRepository> logger
) : IFlightNotificationRepository
{
    private readonly AppDbContext _context = context;
    private readonly ILogger<FlightNotificationRepository> _logger = logger;
    public async Task AddAllFlightNotificationsAsync(List<FlightNotification> notifications)
    {
        try
        {
            _context.FlightNotifications.AddRange(notifications);
            await _context.SaveChangesAsync();

        }
        catch(Exception ex)
        {
            _logger.LogError(ex, "An error occurred while adding FlightNotifications");
            throw;
        }
    }

    public async Task<List<FlightNotification>> GetAllAsync()
    {
        try
        {
            return await _context.FlightNotifications
            .Include(fn => fn.MonitoredRoute)
                .ThenInclude(mr => mr.OriginAirport)
            .Include(fn => fn.MonitoredRoute)
                .ThenInclude(mr => mr.DestinationAirport)
            .ToListAsync();
        }
        catch(Exception)
        {
            _logger.LogError("An error occurred while fetching all FlightNotifications");
            throw;
        }
    }

    public async Task DeleteAllAsync()
    {
        try
        {
            _context.FlightNotifications.RemoveRange(_context.FlightNotifications);
            await _context.SaveChangesAsync();
        }
        catch(Exception ex)
        {
            _logger.LogError(ex, "An error occurred while deleting all FlightNotifications");
            throw;
        }
    }
}