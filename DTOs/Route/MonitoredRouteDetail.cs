namespace Flight_Alert_API.DTOs.Route;

public class MonitoredRouteDetail
{
    public int RouteId { get; set; }
    public int UserMonitoredRouteId { get; set; }
    public int UserId { get; set; }
    public string OriginIataCode { get; set; } = null!;
    public string DestinationIataCode { get; set; } = null!;
    public DateOnly DepartureDay { get; set; }
    public decimal CurrentPrice { get; set; }
    public decimal? TargetPrice { get; set; }
    public string? Link { get; set; }
    public bool IsActive { get; set; }
}