
using Flight_Alert_API.Models;

namespace Flight_Alert_API.Repositories.Interfaces;

public interface IFlightNotificationRepository
{
    public Task AddAllFlightNotificationsAsync(List<FlightNotification> notifications);
    public Task<List<FlightNotification>> GetAllToNotifyAsync();
    public Task DeleteAllAsync();
    public Task SetNotifiedAsync(FlightNotification notification);
}