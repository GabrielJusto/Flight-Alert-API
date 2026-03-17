
using Flight_Alert_API.Database;
using Flight_Alert_API.Models;
using Flight_Alert_API.Repositories.Interfaces;

using Microsoft.EntityFrameworkCore;

namespace Flight_Alert_API.Repositories.Implementations;

public class MonitoredRouteRepository(
    AppDbContext dbContext
) : IMonitoredRouteRepository
{
    private readonly AppDbContext _dbContext = dbContext;

    public async Task Insert(MonitoredRoute monitoredRoute)
    {
        try
        {
            _dbContext.MonitoredRoutes.Add(monitoredRoute);
            await _dbContext.SaveChangesAsync();
        }
        catch(Exception)
        {
            //log
            throw;
        }

    }

    public async Task<MonitoredRoute?> GetByIdAsync(int id)
    {
        try
        {
            return await _dbContext.MonitoredRoutes.FindAsync(id);
        }
        catch(Exception)
        {
            //log
            throw;
        }
    }

    public async Task<List<MonitoredRoute>> GetAllAsync()
    {
        try
        {
            return await _dbContext.MonitoredRoutes.ToListAsync();
        }
        catch(Exception)
        {
            //log
            throw;
        }
    }

    public async Task<MonitoredRoute?> GetByOriginAndDestinationAsync(int originAirportId, int destinationAirportId)
    {
        try
        {
            return await _dbContext.MonitoredRoutes.FirstOrDefaultAsync(mr => mr.OriginAirportId == originAirportId && mr.DestinationAirportId == destinationAirportId);
        }
        catch(Exception)
        {
            //log
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
        catch(Exception)
        {
            //log
            throw;
        }
    }
}