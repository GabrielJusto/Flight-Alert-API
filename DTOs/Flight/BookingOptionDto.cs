using Newtonsoft.Json;

namespace Flight_Alert_API.DTOs.Flight;

public class BookingOptionDto
{
    [JsonProperty("together")]
    public BookingDetails? Together { get; set; }
}

public class BookingDetails
{
    [JsonProperty("book_with")]
    public required string BookWith { get; set; }

    [JsonProperty("airline")]
    public bool? Airline { get; set; }

    [JsonProperty("airline_logos")]
    public List<string>? AirlineLogos { get; set; }

    [JsonProperty("marketed_as")]
    public List<string>? MarketedAs { get; set; }

    [JsonProperty("price")]
    public decimal Price { get; set; }

    [JsonProperty("baggage_prices")]
    public List<string>? BaggagePrices { get; set; }

    [JsonProperty("booking_request")]
    public BookingRequestDetails? BookingRequest { get; set; }
}

public class BookingRequestDetails
{
    [JsonProperty("url")]
    public required string Url { get; set; }

    [JsonProperty("post_data")]
    public string? PostData { get; set; }
}
