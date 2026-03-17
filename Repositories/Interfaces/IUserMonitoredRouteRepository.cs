
using Flight_Alert_API.Models;

namespace Flight_Alert_API.Repositories.Interfaces;

public interface IUserMonitoredRouteRepository
{
    public Task Insert(UserMonitoredRoute userMonitoredRoute);
    public Task<List<UserMonitoredRoute>> GetAllForSendingAlertsAsync();
}