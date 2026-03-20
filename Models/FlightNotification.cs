namespace Flight_Alert_API.Models;

public class FlightNotification
{
    public int FlightNotificationId { get; set; }
    public int UserMonitoredRouteId { get; set; }
    public decimal Price { get; set; }
    public virtual UserMonitoredRoute UserMonitoredRoute { get; set; } = null!;
    public DateTime FlightDate { get; set; }
    public DateTime? NotificationDate { get; set; }
    public DateTime CreatedAt { get; set; }
    public string? Link { get; set; }
}