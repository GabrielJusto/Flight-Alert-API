
namespace Flight_Alert_API.Models;

public class UserMonitoredRoute
{
    public int UserMonitoredRouteId { get; set; }
    public int UserId { get; set; }
    public int MonitoredRouteId { get; set; }
    public decimal TargetPrice { get; set; }

    public virtual User User { get; set; } = null!;
    public virtual MonitoredRoute MonitoredRoute { get; set; } = null!;
    public virtual List<FlightNotification> FlightNotifications { get; set; } = new List<FlightNotification>();
}