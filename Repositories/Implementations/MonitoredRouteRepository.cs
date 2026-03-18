
using Flight_Alert_API.Database;
using Flight_Alert_API.Models;
using Flight_Alert_API.Repositories.Interfaces;

using Microsoft.EntityFrameworkCore;

namespace Flight_Alert_API.Repositories.Implementations;

public class MonitoredRouteRepository(
    AppDbContext dbContext,
    ILogger<MonitoredRouteRepository> logger
) : IMonitoredRouteRepository
{
    private readonly AppDbContext _dbContext = dbContext;
    private readonly ILogger<MonitoredRouteRepository> _logger = logger;

    public async Task Insert(MonitoredRoute monitoredRoute)
    {
        try
        {
            _dbContext.MonitoredRoutes.Add(monitoredRoute);
            await _dbContext.SaveChangesAsync();
        }
        catch(Exception ex)
        {
            _logger.LogError(ex, "An error occurred while inserting a MonitoredRoute");
            throw;
        }

    }

    public async Task<MonitoredRoute?> GetByIdAsync(int id)
    {
        try
        {
            return await _dbContext.MonitoredRoutes.FindAsync(id);
        }
        catch(Exception ex)
        {
            _logger.LogError(ex, "An error occurred while fetching a MonitoredRoute by Id: {Id}", id);
            throw;
        }
    }

    public async Task<List<MonitoredRoute>> GetAllAsync()
    {
        try
        {
            return await _dbContext.MonitoredRoutes
            .Include(mr => mr.OriginAirport)
            .Include(mr => mr.DestinationAirport)
            .ToListAsync();
        }
        catch(Exception ex)
        {
            _logger.LogError(ex, "An error occurred while fetching all MonitoredRoutes");
            throw;
        }
    }

    public async Task<MonitoredRoute?> GetByOriginAndDestinationAsync(int originAirportId, int destinationAirportId, DateOnly departureDay, DateOnly returnDay)
    {
        try
        {
            return await _dbContext.MonitoredRoutes
                .FirstOrDefaultAsync(mr => mr.OriginAirportId == originAirportId && mr.DestinationAirportId == destinationAirportId && mr.DepartureDay == departureDay && mr.ReturnDay == returnDay);
        }
        catch(Exception ex)
        {
            _logger.LogError(ex, """
                An error occurred while fetching a MonitoredRoute by origin and destination:
                Origin: {OriginAirportId},
                Destination: {DestinationAirportId}
                Departure: {DepartureDay},
                Return: {ReturnDay}
            """, originAirportId, destinationAirportId, departureDay, returnDay);
            throw;
        }
    }

    public async Task DeleteAsync(MonitoredRoute monitoredRoute)
    {
        try
        {
            _dbContext.MonitoredRoutes.Remove(monitoredRoute);
            await _dbContext.SaveChangesAsync();
        }
        catch(Exception ex)
        {
            _logger.LogError(ex, "An error occurred while deleting a MonitoredRoute with Id: {Id}", monitoredRoute.Id);
            throw;
        }
    }
}