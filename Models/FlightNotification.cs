namespace Flight_Alert_API.Models;

public class FlightNotification
{
    public int FlightNotificationId { get; set; }
    public int MonitoredRouteId { get; set; }
    public decimal Price { get; set; }
    public virtual MonitoredRoute MonitoredRoute { get; set; } = null!;
    public DateTime NotificationDate { get; set; }
}