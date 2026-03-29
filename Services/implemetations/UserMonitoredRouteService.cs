
using Flight_Alert_API.DTOs.Route;
using Flight_Alert_API.Exceptions;
using Flight_Alert_API.Models;
using Flight_Alert_API.Repositories.Interfaces;
using Flight_Alert_API.Services.Interfaces;

namespace Flight_Alert_API.Services.implemetations;

public class UserMonitoredRouteService(
    IUserMonitoredRouteRepository userMonitoredRouteRepository,
    ILogger<UserMonitoredRouteService> logger
) : IUserMonitoredRouteService
{
    private readonly IUserMonitoredRouteRepository _userMonitoredRouteRepository = userMonitoredRouteRepository;
    private readonly ILogger<UserMonitoredRouteService> _logger = logger;

    public async Task UpdateMonitoredRouteAsync(UserMonitoredRouteUpdateDto data)
    {

        UserMonitoredRoute? route = await _userMonitoredRouteRepository.GetByIdAsync(data.Id);
        if(route == null)
        {
            _logger.LogWarning("Monitored route not found for update: {@data}", data);
            throw new EntityNotFoundException($"Monitored route with ID {data.Id} not found.");
        }
        if(route.UserId != data.UserId)
        {
            _logger.LogWarning("Unauthorized update attempt for user monitored route: {@data}", data);
            throw new UnauthorizedAccessException("You do not have permission to update this monitored route.");
        }

        route.IsActive = data.IsActive;
        await _userMonitoredRouteRepository.UpdateAsync(route);
    }
}