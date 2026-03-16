namespace Flight_Alert_API.DTOs.Route;

public class RouteRegisterRequest
{
    public string OriginIataCode { get; set; } = null!;
    public string DestinationIataCode { get; set; } = null!;
    public decimal TargetPrice { get; set; }
}