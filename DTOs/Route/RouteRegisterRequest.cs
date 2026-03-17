namespace Flight_Alert_API.DTOs.Route;

public class RouteRegisterRequest
{
    public string OriginIataCode { get; set; } = null!;
    public string DestinationIataCode { get; set; } = null!;
    public DateOnly DepartureDay { get; set; }
    public DateOnly ReturnDay { get; set; }
    public decimal TargetPrice { get; set; }
    public int UserId { get; set; }
}