namespace Flight_Alert_API.DTOs.Flight;

public class FlightSearchResult
{
    public decimal? Price { get; set; }
    public string? Currency { get; set; }
    public int TotalDuration { get; set; }
    public string? Type { get; set; }
    public string? BookingToken { get; set; }
    public List<FlightSegmentInfo> Segments { get; set; } = new();
    public List<LayoverInfo> Layovers { get; set; } = new();

}

public class FlightSegmentInfo
{
    public string Airline { get; set; } = string.Empty;
    public string? FlightNumber { get; set; }
    public string DepartureAirportName { get; set; } = string.Empty;
    public string DepartureAirportCode { get; set; } = string.Empty;
    public string? DepartureTime { get; set; }
    public string ArrivalAirportName { get; set; } = string.Empty;
    public string ArrivalAirportCode { get; set; } = string.Empty;
    public string? ArrivalTime { get; set; }
    public int Duration { get; set; }
    public string? Airplane { get; set; }
    public string? TravelClass { get; set; }
    public string? Legroom { get; set; }
    public bool IsOvernight { get; set; }
    public bool IsOftenDelayed { get; set; }
}

public class LayoverInfo
{
    public string Name { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
    public int Duration { get; set; }
    public bool IsOvernight { get; set; }
}

