
using Flight_Alert_API.DTOs.Route;

namespace Flight_Alert_API.Services.Interfaces;

public interface IUserMonitoredRouteService
{
    public Task UpdateMonitoredRouteAsync(UserMonitoredRouteUpdateDto data);
}