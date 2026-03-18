
using Flight_Alert_API.DTOs.Flight;
using Flight_Alert_API.Models;
using Flight_Alert_API.Repositories.Interfaces;
using Flight_Alert_API.Services.Interfaces;

namespace Flight_Alert_API.Services.implemetations;

public class FlightPriceService(
        ISerpGoogleFlightsService serpGoogleFlightsService,
        IMonitoredRouteRepository monitoredRouteRepository,
        IFlightNotificationRepository flightNotificationRepository
) : IFlightPriceService
{

    private readonly ISerpGoogleFlightsService _serpGoogleFlightsService = serpGoogleFlightsService;
    private readonly IMonitoredRouteRepository _monitoredRouteRepository = monitoredRouteRepository;
    private readonly IFlightNotificationRepository _flightNotificationRepository = flightNotificationRepository;
    public async Task CheckAllFlightPricesAsync()
    {
        List<MonitoredRoute> monitoredRoutes = await _monitoredRouteRepository.GetAllAsync();

        foreach(var route in monitoredRoutes)
        {
            SerpGoogleFlightsRequest request = new()
            {
                DepartureId = route.OriginAirport.IataCode,
                ArrivalId = route.DestinationAirport.IataCode,
                OutboundDate = route.DepartureDay.ToString("yyyy-MM-dd"),
                Currency = "BRL"
            };

            List<FlightSearchResult> flightResults = _serpGoogleFlightsService.GetFlights(request);

            FlightSearchResult? cheapestFlight = flightResults
                .Where(result => result.Price is not null && result.Price > 0)
                .MinBy(result => result.Price);

            if(cheapestFlight != null)
            {
                FlightNotification notification = new()
                {
                    MonitoredRouteId = route.Id,
                    Price = cheapestFlight.Price ?? 0,
                };

                await _flightNotificationRepository.AddAllFlightNotificationsAsync(new List<FlightNotification> { notification });
            }

        }
    }
}