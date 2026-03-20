using System.Collections;

using Flight_Alert_API.Configuration;
using Flight_Alert_API.DTOs.Flight;
using Flight_Alert_API.Services.Interfaces;

using Microsoft.Extensions.Options;

using Newtonsoft.Json;

using SerpApi;

namespace Flight_Alert_API.Services.implemetations;

public class SerpGoogleFlightsService(
    ILogger<SerpGoogleFlightsService> logger,
    IOptions<SerpApiConfiguration> serpApiConfig
) : ISerpGoogleFlightsService
{
    private readonly string _apiKey = serpApiConfig.Value.ApiKey;
    private readonly ILogger<SerpGoogleFlightsService> _logger = logger;

    public List<FlightSearchResult> GetFlights(SerpGoogleFlightsRequest request)
    {
        List<FlightSearchResult> results = new();
        Hashtable ht = new();
        ht.Add("engine", request.Engine);
        ht.Add("departure_id", request.DepartureId);
        ht.Add("arrival_id", request.ArrivalId);
        ht.Add("currency", request.Currency);
        ht.Add("outbound_date", request.OutboundDate);
        ht.Add("type", "2");

        try
        {
            GoogleSearch search = new(ht, _apiKey);
            string jsonString = search.GetJson().ToString();

            SerpGoogleFlightsResponse? response = JsonConvert.DeserializeObject<SerpGoogleFlightsResponse>(jsonString);

            if(response?.BestFlights != null)
            {
                foreach(var flightOption in response.BestFlights)
                {
                    FlightSearchResult flightResult = new()
                    {
                        Price = flightOption.Price,
                        Currency = request.Currency,
                        TotalDuration = flightOption.TotalDuration,
                        Type = flightOption.Type,
                        BookingToken = flightOption.BookingToken
                    };

                    foreach(var segment in flightOption.Flights)
                    {
                        FlightSegmentInfo segmentInfo = new()
                        {
                            Airline = segment.Airline,
                            FlightNumber = segment.FlightNumber,
                            DepartureAirportName = segment.DepartureAirport.Name,
                            DepartureAirportCode = segment.DepartureAirport.Id,
                            DepartureTime = segment.DepartureAirport.Time,
                            ArrivalAirportName = segment.ArrivalAirport.Name,
                            ArrivalAirportCode = segment.ArrivalAirport.Id,
                            ArrivalTime = segment.ArrivalAirport.Time,
                            Duration = segment.Duration,
                            Airplane = segment.Airplane,
                            TravelClass = segment.TravelClass,
                            Legroom = segment.Legroom,
                            IsOvernight = segment.Overnight ?? false,
                            IsOftenDelayed = segment.OftenDelayedByOver30Min ?? false
                        };
                        flightResult.Segments.Add(segmentInfo);
                    }


                    if(flightOption.Layovers != null)
                    {
                        foreach(var layover in flightOption.Layovers)
                        {
                            LayoverInfo layoverInfo = new()
                            {
                                Name = layover.Name,
                                Code = layover.Id,
                                Duration = layover.Duration,
                                IsOvernight = layover.Overnight ?? false
                            };
                            flightResult.Layovers.Add(layoverInfo);
                        }
                    }

                    results.Add(flightResult);
                }
            }
        }
        catch(SerpApiSearchException)
        {
            throw;
        }

        return results;
    }

    public async Task<List<BookingOptionDto>> GetBookingOptionsAsync(SerpGoogleFlightsRequest request)
    {
        Hashtable ht = new();
        ht.Add("engine", request.Engine);
        ht.Add("departure_id", request.DepartureId);
        ht.Add("arrival_id", request.ArrivalId);
        ht.Add("currency", request.Currency);
        ht.Add("outbound_date", request.OutboundDate);
        ht.Add("type", "2");
        ht.Add("booking_token", request.BookingToken);

        try
        {
            _logger.LogInformation("Getting booking options for booking token: {BookingToken}", request.BookingToken);
            GoogleSearch search = new(ht, _apiKey);
            string jsonString = search.GetJson().ToString();
            SerpBookingOptionsResponse? response = JsonConvert.DeserializeObject<SerpBookingOptionsResponse>(jsonString);
            _logger.LogInformation("Received booking options response for booking token: {BookingToken}", request.BookingToken);

            List<BookingOptionDto> bookingOptions = new();

            if(response?.BookingOptions != null)
            {
                foreach(var option in response.BookingOptions)
                {
                    if(option.Together != null)
                    {
                        BookingOptionDto bookingOptionDto = new()
                        {
                            Together = new BookingDetails
                            {
                                BookWith = option.Together.BookWith,
                                Airline = option.Together.Airline,
                                AirlineLogos = option.Together.AirlineLogos,
                                MarketedAs = option.Together.MarketedAs,
                                Price = option.Together.Price,
                                BaggagePrices = option.Together.BaggagePrices,
                                BookingRequest = option.Together.BookingRequest != null ? new BookingRequestDetails
                                {
                                    Url = option.Together.BookingRequest.Url,
                                    PostData = option.Together.BookingRequest.PostData
                                } : null
                            }
                        };
                        bookingOptions.Add(bookingOptionDto);
                    }
                }
            }
            _logger.LogInformation("Parsed {Count} booking options for booking token: {BookingToken}", bookingOptions.Count, request.BookingToken);
            return bookingOptions;
        }
        catch(SerpApiSearchException ex)
        {
            _logger.LogError(ex, "Error occurred while fetching booking options for booking token: {BookingToken}", request.BookingToken);
            throw;
        }

    }
}