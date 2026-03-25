using Flight_Alert_API.DTOs.Route;

namespace Flight_Alert_API.Services.Interfaces;

public interface IMonitoredRouteService
{
    public Task InsertMonitoredRouteAsync(RouteRegisterRequest request);
    public Task DeleteMonitoredRouteAsync(int id, int userId);
    public Task<List<MonitoredRouteDetail>> GetUserMonitoredRoutesAsync(int userId);
}