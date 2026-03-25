
using Flight_Alert_API.Models;

namespace Flight_Alert_API.Repositories.Interfaces;

public interface IUserMonitoredRouteRepository
{
    public Task Insert(UserMonitoredRoute userMonitoredRoute);
    public Task<List<UserMonitoredRoute>> GetAllForSendingAlertsAsync();
    public Task<List<UserMonitoredRoute>> GetAllByUserIdAsync(int userId);
    public Task<UserMonitoredRoute?> GetToDeleteAsync(int id, int userId);
    public Task Delete(UserMonitoredRoute userMonitoredRoute);
    public Task<UserMonitoredRoute?> GetByIdAsync(int id);
}