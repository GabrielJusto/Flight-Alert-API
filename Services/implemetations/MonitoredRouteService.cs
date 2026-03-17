
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

        MonitoredRoute monitoredRoute = await GetMonitoredRoute(originAirport.Id, destinationAirport.Id);

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

    private async Task<MonitoredRoute> GetMonitoredRoute(int originAirportId, int destinationAirportId)
    {
        MonitoredRoute? monitoredRoute = await _monitoredRouteRepository.GetByOriginAndDestinationAsync(originAirportId, destinationAirportId);
        if(monitoredRoute is null)
        {
            monitoredRoute = new()
            {
                OriginAirportId = originAirportId,
                DestinationAirportId = destinationAirportId
            };
            await _monitoredRouteRepository.Insert(monitoredRoute);
        }

        return monitoredRoute;
    }

}