using Newtonsoft.Json;

namespace Flight_Alert_API.DTOs.Flight;

public class SerpBookingOptionsResponse
{
    [JsonProperty("search_metadata")]
    public required SearchMetadata SearchMetadata { get; set; }

    [JsonProperty("search_parameters")]
    public required SearchParametersBooking SearchParameters { get; set; }

    [JsonProperty("selected_flights")]
    public List<SelectedFlight>? SelectedFlights { get; set; }

    [JsonProperty("baggage_prices")]
    public BaggagePrices? BaggagePrices { get; set; }

    [JsonProperty("booking_options")]
    public List<BookingOption>? BookingOptions { get; set; }

    [JsonProperty("price_insights")]
    public PriceInsights? PriceInsights { get; set; }
}

public class SearchParametersBooking
{
    [JsonProperty("engine")]
    public required string Engine { get; set; }

    [JsonProperty("hl")]
    public string? Hl { get; set; }

    [JsonProperty("gl")]
    public string? Gl { get; set; }

    [JsonProperty("type")]
    public string? Type { get; set; }

    [JsonProperty("departure_id")]
    public required string DepartureId { get; set; }

    [JsonProperty("arrival_id")]
    public required string ArrivalId { get; set; }

    [JsonProperty("outbound_date")]
    public string? OutboundDate { get; set; }

    [JsonProperty("booking_token")]
    public string? BookingToken { get; set; }

    [JsonProperty("currency")]
    public string? Currency { get; set; }
}

public class SelectedFlight
{
    [JsonProperty("flights")]
    public required List<FlightInfo> Flights { get; set; }

    [JsonProperty("total_duration")]
    public int TotalDuration { get; set; }

    [JsonProperty("carbon_emissions")]
    public CarbonEmissions? CarbonEmissions { get; set; }

    [JsonProperty("type")]
    public string? Type { get; set; }

    [JsonProperty("airline_logo")]
    public string? AirlineLogo { get; set; }
}

public class FlightInfo
{
    [JsonProperty("departure_airport")]
    public required Airport DepartureAirport { get; set; }

    [JsonProperty("arrival_airport")]
    public required Airport ArrivalAirport { get; set; }

    [JsonProperty("duration")]
    public int Duration { get; set; }

    [JsonProperty("airplane")]
    public string? Airplane { get; set; }

    [JsonProperty("airline")]
    public required string Airline { get; set; }

    [JsonProperty("airline_logo")]
    public string? AirlineLogo { get; set; }

    [JsonProperty("travel_class")]
    public string? TravelClass { get; set; }

    [JsonProperty("flight_number")]
    public string? FlightNumber { get; set; }

    [JsonProperty("legroom")]
    public string? Legroom { get; set; }

    [JsonProperty("extensions")]
    public List<string>? Extensions { get; set; }

    [JsonProperty("plane_and_crew_by")]
    public string? PlaneAndCrewBy { get; set; }
}

public class BaggagePrices
{
    [JsonProperty("together")]
    public List<string>? Together { get; set; }
}

public class BookingOption
{
    [JsonProperty("together")]
    public BookingOptionDetails? Together { get; set; }
}

public class BookingOptionDetails
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

    [JsonProperty("local_prices")]
    public List<LocalPrice>? LocalPrices { get; set; }

    [JsonProperty("baggage_prices")]
    public List<string>? BaggagePrices { get; set; }

    [JsonProperty("booking_request")]
    public BookingRequest? BookingRequest { get; set; }
}

public class LocalPrice
{
    [JsonProperty("currency")]
    public required string Currency { get; set; }

    [JsonProperty("price")]
    public decimal Price { get; set; }
}

public class BookingRequest
{
    [JsonProperty("url")]
    public required string Url { get; set; }

    [JsonProperty("post_data")]
    public string? PostData { get; set; }
}
