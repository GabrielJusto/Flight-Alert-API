using System.Collections;

using Flight_Alert_API.Configuration;
using Flight_Alert_API.DTOs.Flight;
using Flight_Alert_API.Services.Interfaces;

using Microsoft.Extensions.Options;

using Newtonsoft.Json;

using SerpApi;

namespace Flight_Alert_API.Services.implemetations;

public class SerpGoogleFlightsService : ISerpGoogleFlightsService
{
    private readonly string _apiKey;

    public SerpGoogleFlightsService(IOptions<SerpApiConfiguration> serpApiConfig)
    {
        _apiKey = serpApiConfig.Value.ApiKey;
    }

    public List<FlightSearchResult> GetFlights(SerpGoogleFlightsRequest request)
    {
        var results = new List<FlightSearchResult>();
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
                    var flightResult = new FlightSearchResult
                    {
                        Price = flightOption.Price,
                        Currency = request.Currency,
                        TotalDuration = flightOption.TotalDuration,
                        Type = flightOption.Type
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
                            var layoverInfo = new LayoverInfo
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
}