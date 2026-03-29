
using Flight_Alert_API.DTOs.Route;
using Flight_Alert_API.Exceptions;
using Flight_Alert_API.Models;
using Flight_Alert_API.Repositories.Interfaces;
using Flight_Alert_API.Services.Interfaces;

using Hangfire;

namespace Flight_Alert_API.Services.implemetations;

public class MonitoredRouteService(
    IAirportRepository airportRepository,
    IMonitoredRouteRepository monitoredRouteRepository,
    IUserMonitoredRouteRepository userMonitoredRouteRepository,
    IFlightPriceService flightPriceService,
    IGoogleLinkService googleLinkService
) : IMonitoredRouteService
{

    private readonly IAirportRepository _airportRepository = airportRepository;
    private readonly IMonitoredRouteRepository _monitoredRouteRepository = monitoredRouteRepository;
    private readonly IUserMonitoredRouteRepository _userMonitoredRouteRepository = userMonitoredRouteRepository;
    private readonly IFlightPriceService _flightPriceService = flightPriceService;
    private readonly IGoogleLinkService _googleLinkService = googleLinkService;
    public async Task InsertMonitoredRouteAsync(RouteRegisterRequest request)
    {
        Airport? originAirport = await _airportRepository.GetByIATACodeAsync(request.OriginIataCode);
        Airport? destinationAirport = await _airportRepository.GetByIATACodeAsync(request.DestinationIataCode);

        if(originAirport == null || destinationAirport == null)
        {
            throw new EntityNotFoundException("Invalid IATA code for origin or destination airport.");
        }

        MonitoredRoute monitoredRoute = await GetMonitoredRoute(originAirport.Id, destinationAirport.Id, request.DepartureDay, request.ReturnDay);

        UserMonitoredRoute userMonitoredRoute = new()
        {
            UserId = request.UserId,
            MonitoredRouteId = monitoredRoute.Id,
            TargetPrice = request.TargetPrice
        };

        await _userMonitoredRouteRepository.Insert(userMonitoredRoute);
        BackgroundJob.Enqueue(() => _flightPriceService.ProcessMonitoredRouteAsync(userMonitoredRoute.UserMonitoredRouteId));

    }

    public async Task DeleteMonitoredRouteAsync(int id, int userId)
    {
        UserMonitoredRoute? userMonitoredRoute = await _userMonitoredRouteRepository.GetToDeleteAsync(id, userId);
        if(userMonitoredRoute == null)
        {
            throw new EntityNotFoundException("Monitored route not found for the user.");
        }
        if(userMonitoredRoute.UserId != userId)
        {
            throw new UnauthorizedAccessException("User does not have permission to delete this monitored route.");
        }
        await _userMonitoredRouteRepository.Delete(userMonitoredRoute);

        MonitoredRoute? monitoredRoute = await _monitoredRouteRepository.GetToDeleteAsync(id);

        if(monitoredRoute == null)
        {
            throw new EntityNotFoundException("Monitored route not found.");
        }
        if(monitoredRoute.UserMonitoredRoutes.Count == 0)
        {
            await _monitoredRouteRepository.DeleteAsync(monitoredRoute);
        }
    }

    public async Task<List<MonitoredRouteDetail>> GetUserMonitoredRoutesAsync(int userId)
    {
        List<UserMonitoredRoute> userMonitoredRoutes = await _userMonitoredRouteRepository.GetAllByUserIdAsync(userId);

        MonitoredRouteDetail[] details = await Task.WhenAll(userMonitoredRoutes.Select(async umr =>
        {
            FlightNotification? latestNotification = umr.FlightNotifications.OrderByDescending(fn => fn.NotificationDate).FirstOrDefault();
            string? linkUrl = latestNotification?.Link;
            string redirectUrl = linkUrl != null ? await _googleLinkService.GetRedirectUrlAsync(linkUrl) : string.Empty;

            return new MonitoredRouteDetail
            {
                RouteId = umr.MonitoredRoute.Id,
                UserId = umr.UserId,
                OriginIataCode = umr.MonitoredRoute.OriginAirport.IataCode,
                DestinationIataCode = umr.MonitoredRoute.DestinationAirport.IataCode,
                DepartureDay = umr.MonitoredRoute.DepartureDay,
                CurrentPrice = latestNotification?.Price ?? 0,
                TargetPrice = umr.TargetPrice,
                Link = redirectUrl
            };
        }));

        return details.ToList();
    }


    private async Task<MonitoredRoute> GetMonitoredRoute(
        int originAirportId,
        int destinationAirportId,
        DateOnly departureDay,
        DateOnly returnDay
    )
    {
        MonitoredRoute? monitoredRoute = await _monitoredRouteRepository.GetByOriginAndDestinationAsync(originAirportId, destinationAirportId, departureDay, returnDay);
        if(monitoredRoute is null)
        {
            monitoredRoute = new()
            {
                OriginAirportId = originAirportId,
                DestinationAirportId = destinationAirportId,
                DepartureDay = departureDay,
                ReturnDay = returnDay
            };
            await _monitoredRouteRepository.Insert(monitoredRoute);
        }

        return monitoredRoute;
    }
}