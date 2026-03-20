
using Flight_Alert_API.DTOs.Flight;
using Flight_Alert_API.Models;
using Flight_Alert_API.Repositories.Interfaces;
using Flight_Alert_API.Services.Interfaces;

namespace Flight_Alert_API.Services.implemetations;

public class FlightPriceService(
        ISerpGoogleFlightsService serpGoogleFlightsService,
        IMonitoredRouteRepository monitoredRouteRepository,
        IFlightNotificationRepository flightNotificationRepository,
        ILogger<FlightPriceService> logger
) : IFlightPriceService
{

    private readonly ISerpGoogleFlightsService _serpGoogleFlightsService = serpGoogleFlightsService;
    private readonly IMonitoredRouteRepository _monitoredRouteRepository = monitoredRouteRepository;
    private readonly IFlightNotificationRepository _flightNotificationRepository = flightNotificationRepository;
    private readonly ILogger<FlightPriceService> _logger = logger;
    public async Task CheckAllFlightPricesAsync()
    {
        try
        {
            _logger.LogInformation("Starting flight price check for all monitored routes at {Time}", DateTime.UtcNow);
            List<MonitoredRoute> monitoredRoutes = await _monitoredRouteRepository.GetAllAsync();

            foreach(MonitoredRoute route in monitoredRoutes)
            {
                SerpGoogleFlightsRequest request = new()
                {
                    DepartureId = route.OriginAirport.IataCode,
                    ArrivalId = route.DestinationAirport.IataCode,
                    OutboundDate = route.DepartureDay.ToString("yyyy-MM-dd"),
                    Currency = "BRL"
                };

                List<FlightSearchResult> flightResults = _serpGoogleFlightsService.GetFlights(request);
                _logger.LogInformation("Found {Count} flight results for route {RouteId}", flightResults.Count, route.Id);

                FlightSearchResult? cheapestFlight = flightResults
                    .Where(result => result.Price is not null && result.Price > 0)
                    .MinBy(result => result.Price);

                if(cheapestFlight != null)
                {
                    _logger.LogInformation("Found cheapest flight for route {RouteId} with price {Price}", route.Id, cheapestFlight.Price);
                    request.BookingToken = cheapestFlight.BookingToken;
                    List<BookingOptionDto> bookingOptions = await _serpGoogleFlightsService.GetBookingOptionsAsync(request);
                    _logger.LogInformation("Retrieved {Count} booking options for route {RouteId}", bookingOptions.Count, route.Id);

                    foreach(UserMonitoredRoute umr in route.UserMonitoredRoutes)
                    {
                        BookingOptionDto? firstBookingOption = bookingOptions.FirstOrDefault();
                        string link = $"{firstBookingOption?.Together?.BookingRequest?.Url}?{firstBookingOption?.Together?.BookingRequest?.PostData}";
                        string? departureTimeStr = cheapestFlight.Segments.FirstOrDefault()?.DepartureTime;
                        DateTime flightDate = DateTime.ParseExact(
                            departureTimeStr ?? DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm"),
                            "yyyy-MM-dd HH:mm",
                            System.Globalization.CultureInfo.InvariantCulture,
                            System.Globalization.DateTimeStyles.AssumeUniversal | System.Globalization.DateTimeStyles.AdjustToUniversal);

                        FlightNotification notification = new()
                        {
                            UserMonitoredRouteId = umr.UserMonitoredRouteId,
                            Price = cheapestFlight.Price ?? 0,
                            CreatedAt = DateTime.UtcNow,
                            FlightDate = flightDate,
                            Link = link
                        };
                        await _flightNotificationRepository.AddAllFlightNotificationsAsync(new List<FlightNotification> { notification });
                        _logger.LogInformation("Created flight notification for UserMonitoredRouteId: {UserMonitoredRouteId} with price {Price} and flight date {FlightDate}", umr.UserMonitoredRouteId, notification.Price, notification.FlightDate);
                    }
                }
            }
            _logger.LogInformation("Completed flight price check for all monitored routes at {Time}", DateTime.UtcNow);
        }
        catch(Exception ex)
        {
            _logger.LogError(ex, "An error occurred while checking flight prices");
            throw;
        }

    }
}