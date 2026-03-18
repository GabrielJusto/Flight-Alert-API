namespace Flight_Alert_API.DTOs.Route;

public class MonitoredRouteDetail
{
    public string OriginIataCode { get; set; } = null!;
    public string DestinationIataCode { get; set; } = null!;
    public DateOnly DepartureDay { get; set; }
    public decimal Price { get; set; }
}