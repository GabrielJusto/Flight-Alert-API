
using Newtonsoft.Json;

namespace Flight_Alert_API.DTOs.Flight;

public class SerpGoogleFlightsResponse
{
    [JsonProperty("search_metadata")]
    public required SearchMetadata SearchMetadata { get; set; }

    [JsonProperty("search_parameters")]
    public required SearchParameters SearchParameters { get; set; }

    [JsonProperty("best_flights")]
    public List<FlightOption>? BestFlights { get; set; }

    [JsonProperty("other_flights")]
    public List<FlightOption>? OtherFlights { get; set; }

    [JsonProperty("price_insights")]
    public PriceInsights? PriceInsights { get; set; }

    [JsonProperty("airports")]
    public List<AirportInfo>? Airports { get; set; }
}

public class SearchMetadata
{
    [JsonProperty("id")]
    public required string Id { get; set; }

    [JsonProperty("status")]
    public required string Status { get; set; }

    [JsonProperty("json_endpoint")]
    public string? JsonEndpoint { get; set; }

    [JsonProperty("created_at")]
    public string? CreatedAt { get; set; }

    [JsonProperty("processed_at")]
    public string? ProcessedAt { get; set; }

    [JsonProperty("google_flights_url")]
    public string? GoogleFlightsUrl { get; set; }

    [JsonProperty("raw_html_file")]
    public string? RawHtmlFile { get; set; }

    [JsonProperty("prettify_html_file")]
    public string? PrettifyHtmlFile { get; set; }

    [JsonProperty("total_time_taken")]
    public double? TotalTimeTaken { get; set; }
}

public class SearchParameters
{
    [JsonProperty("engine")]
    public required string Engine { get; set; }

    [JsonProperty("hl")]
    public string? Hl { get; set; }

    [JsonProperty("gl")]
    public string? Gl { get; set; }

    [JsonProperty("departure_id")]
    public required string DepartureId { get; set; }

    [JsonProperty("arrival_id")]
    public required string ArrivalId { get; set; }

    [JsonProperty("outbound_date")]
    public string? OutboundDate { get; set; }

    [JsonProperty("return_date")]
    public string? ReturnDate { get; set; }

    [JsonProperty("currency")]
    public string? Currency { get; set; }
}

public class FlightOption
{
    [JsonProperty("flights")]
    public required List<FlightSegment> Flights { get; set; }

    [JsonProperty("layovers")]
    public List<Layover>? Layovers { get; set; }

    [JsonProperty("total_duration")]
    public int TotalDuration { get; set; }

    [JsonProperty("carbon_emissions")]
    public CarbonEmissions? CarbonEmissions { get; set; }

    [JsonProperty("price")]
    public decimal? Price { get; set; }

    [JsonProperty("type")]
    public string? Type { get; set; }

    [JsonProperty("airline_logo")]
    public string? AirlineLogo { get; set; }

    [JsonProperty("departure_token")]
    public string? DepartureToken { get; set; }
    [JsonProperty("booking_token")]
    public string? BookingToken { get; set; }
}

public class FlightSegment
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

    [JsonProperty("ticket_also_sold_by")]
    public List<string>? TicketAlsoSoldBy { get; set; }

    [JsonProperty("legroom")]
    public string? Legroom { get; set; }

    [JsonProperty("extensions")]
    public List<string>? Extensions { get; set; }

    [JsonProperty("overnight")]
    public bool? Overnight { get; set; }

    [JsonProperty("often_delayed_by_over_30_min")]
    public bool? OftenDelayedByOver30Min { get; set; }

    [JsonProperty("plane_and_crew_by")]
    public string? PlaneAndCrewBy { get; set; }
}

public class Airport
{
    [JsonProperty("name")]
    public required string Name { get; set; }

    [JsonProperty("id")]
    public required string Id { get; set; }

    [JsonProperty("time")]
    public string? Time { get; set; }
}

public class Layover
{
    [JsonProperty("duration")]
    public int Duration { get; set; }

    [JsonProperty("name")]
    public required string Name { get; set; }

    [JsonProperty("id")]
    public required string Id { get; set; }

    [JsonProperty("overnight")]
    public bool? Overnight { get; set; }
}

public class CarbonEmissions
{
    [JsonProperty("this_flight")]
    public int ThisFlight { get; set; }

    [JsonProperty("typical_for_this_route")]
    public int TypicalForThisRoute { get; set; }

    [JsonProperty("difference_percent")]
    public int DifferencePercent { get; set; }
}

public class PriceInsights
{
    [JsonProperty("lowest_price")]
    public decimal LowestPrice { get; set; }

    [JsonProperty("price_level")]
    public string? PriceLevel { get; set; }

    [JsonProperty("typical_price_range")]
    public List<decimal>? TypicalPriceRange { get; set; }

    [JsonProperty("price_history")]
    public List<List<long>>? PriceHistory { get; set; }
}

public class AirportInfo
{
    [JsonProperty("departure")]
    public List<AirportDetail>? Departure { get; set; }

    [JsonProperty("arrival")]
    public List<AirportDetail>? Arrival { get; set; }
}

public class AirportDetail
{
    [JsonProperty("airport")]
    public required AirportBasic Airport { get; set; }

    [JsonProperty("city")]
    public string? City { get; set; }

    [JsonProperty("country")]
    public string? Country { get; set; }

    [JsonProperty("country_code")]
    public string? CountryCode { get; set; }

    [JsonProperty("image")]
    public string? Image { get; set; }

    [JsonProperty("thumbnail")]
    public string? Thumbnail { get; set; }
}

public class AirportBasic
{
    [JsonProperty("id")]
    public required string Id { get; set; }

    [JsonProperty("name")]
    public required string Name { get; set; }
}


