
using Flight_Alert_API.Database;
using Flight_Alert_API.Models;
using Flight_Alert_API.Repositories.Interfaces;

using Microsoft.EntityFrameworkCore;

namespace Flight_Alert_API.Repositories.Implementations;

public class FlightNotificationRepository(
    AppDbContext context
) : IFlightNotificationRepository
{
    private readonly AppDbContext _context = context;
    public async Task AddAllFlightNotificationsAsync(List<FlightNotification> notifications)
    {
        try
        {
            _context.FlightNotifications.AddRange(notifications);
            await _context.SaveChangesAsync();

        }catch (Exception)
        {
            //log
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
        catch (Exception)
        {
            //log
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
        catch (Exception)
        {
            //log
            throw;
        }
    }
}