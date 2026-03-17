using Flight_Alert_API.Models;

namespace Flight_Alert_API.Repositories.Interfaces;

public interface IMonitoredRouteRepository
{
    public Task Insert(MonitoredRoute monitoredRoute);
    public Task<MonitoredRoute?> GetByIdAsync(int id);
    public Task<List<MonitoredRoute>> GetAllAsync();
    public Task<MonitoredRoute?> GetByOriginAndDestinationAsync(int originAirportId, int destinationAirportId);
    public Task DeleteAsync(MonitoredRoute monitoredRoute);
}