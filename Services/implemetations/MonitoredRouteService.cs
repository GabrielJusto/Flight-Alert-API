
using Flight_Alert_API.DTOs.Route;
using Flight_Alert_API.Exceptions;
using Flight_Alert_API.Models;
using Flight_Alert_API.Repositories.Interfaces;
using Flight_Alert_API.Services.Interfaces;

namespace Flight_Alert_API.Services.implemetations;

public class MonitoredRouteService(
    IAirportRepository airportRepository,
    IMonitoredRouteRepository monitoredRouteRepository,
    IUserMonitoredRouteRepository userMonitoredRouteRepository
) : IMonitoredRouteService
{

    private readonly IAirportRepository _airportRepository = airportRepository;
    private readonly IMonitoredRouteRepository _monitoredRouteRepository = monitoredRouteRepository;
    private readonly IUserMonitoredRouteRepository _userMonitoredRouteRepository = userMonitoredRouteRepository;
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
            MonitoredRouteId = monitoredRoute.Id
        };

        await _userMonitoredRouteRepository.Insert(userMonitoredRoute);
    }

    public async Task DeleteMonitoredRouteAsync(int id)
    {
        MonitoredRoute? monitoredRoute = await _monitoredRouteRepository.GetByIdAsync(id);

        if(monitoredRoute == null)
        {
            throw new EntityNotFoundException("Monitored route not found.");
        }

        await _monitoredRouteRepository.DeleteAsync(monitoredRoute);
    }

    public async Task<List<MonitoredRouteDetail>> GetUserMonitoredRoutesAsync(int userId)
    {
        List<UserMonitoredRoute> userMonitoredRoutes = await _userMonitoredRouteRepository.GetAllByUserIdAsync(userId);

        return userMonitoredRoutes.Select(umr => new MonitoredRouteDetail
        {
            OriginIataCode = umr.MonitoredRoute.OriginAirport.IataCode,
            DestinationIataCode = umr.MonitoredRoute.DestinationAirport.IataCode,
            DepartureDay = umr.MonitoredRoute.DepartureDay,
            Price = umr.FlightNotifications.OrderByDescending(fn => fn.NotificationDate).FirstOrDefault()?.Price ?? 0
        }).ToList();
    }


    private async Task<MonitoredRoute> GetMonitoredRoute(int originAirportId, int destinationAirportId, DateOnly departureDay, DateOnly returnDay)
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