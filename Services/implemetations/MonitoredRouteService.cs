
using Flight_Alert_API.DTOs.Route;
using Flight_Alert_API.Exceptions;
using Flight_Alert_API.Models;
using Flight_Alert_API.Repositories.Interfaces;
using Flight_Alert_API.Services.Interfaces;

namespace Flight_Alert_API.Services.implemetations;

public class MonitoredRouteService(
    IAirportRepository airportRepository,
    IMonitoredRouteRepository monitoredRouteRepository
) : IMonitoredRouteService
{

    private readonly IAirportRepository _airportRepository = airportRepository;
    private readonly IMonitoredRouteRepository _monitoredRouteRepository = monitoredRouteRepository;
    public async Task InsertMonitoredRouteAsync(RouteRegisterRequest request)
    {
        Airport? originAirport = await _airportRepository.GetByIATACodeAsync(request.OriginIataCode);
        Airport? destinationAirport = await _airportRepository.GetByIATACodeAsync(request.DestinationIataCode);
        
        if (originAirport == null || destinationAirport == null)
        {
            throw new EntityNotFoundException("Invalid IATA code for origin or destination airport.");
        }

        MonitoredRoute monitoredRoute = new()
        {
            OriginAirportId = originAirport.Id,
            DestinationAirportId = destinationAirport.Id,
            TargetPrice = request.TargetPrice
        };

        await _monitoredRouteRepository.Insert(monitoredRoute);
    }
    
}