namespace Flight_Alert_API.Models;

public class MonitoredRoute
{
    public int Id { get; set; }
    public int OriginAirportId { get; set; }
    public int DestinationAirportId { get; set; }
    public DateOnly DepartureDay { get; set; }
    public DateOnly ReturnDay { get; set; }

    public virtual Airport OriginAirport { get; set; } = null!;
    public virtual Airport DestinationAirport { get; set; } = null!;
    public virtual List<UserMonitoredRoute> UserMonitoredRoutes { get; set; } = new List<UserMonitoredRoute>();
}