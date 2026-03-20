using Newtonsoft.Json;

namespace Flight_Alert_API.DTOs.Flight;

public class SerpGoogleFlightsRequest
{
    [JsonProperty("engine")]
    public string Engine { get; set; } = "google_flights";
    [JsonProperty("departure_id")]
    public required string DepartureId { get; set; }
    [JsonProperty("arrival_id")]
    public required string ArrivalId { get; set; }
    [JsonProperty("currency")]
    public string Currency { get; set; } = "BRL";
    [JsonProperty("outbound_date")]
    public required string OutboundDate { get; set; }
    [JsonProperty("return_date")]
    public string? ReturnDate { get; set; }
    [JsonProperty("booking_token")]
    public string? BookingToken { get; set; }

}