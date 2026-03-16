
using Flight_Alert_API.Database;
using Flight_Alert_API.Models;
using Flight_Alert_API.Repositories.Interfaces;

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
}