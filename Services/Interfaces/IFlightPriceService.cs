
using Flight_Alert_API.Models;

namespace Flight_Alert_API.Services.Interfaces;

public interface IFlightPriceService
{
    public Task CheckAllFlightPricesAsync();
    public Task ProcessMonitoredRouteAsync(MonitoredRoute route);
}