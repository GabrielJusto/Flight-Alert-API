using Flight_Alert_API.Models;
using Flight_Alert_API.Repositories.Interfaces;
using Flight_Alert_API.Services.Interfaces;

namespace Flight_Alert_API.Services.implemetations;

public class SendAlertsService(
    IWhatsappService whatsappService,
    IUserMonitoredRouteRepository userMonitoredRouteRepository
) : ISendAlertsService
{

    private readonly IWhatsappService _whatsappService = whatsappService;
    private readonly IUserMonitoredRouteRepository _userMonitoredRouteRepository = userMonitoredRouteRepository;

    public async Task SendAlertsAsync()
    {
        List<UserMonitoredRoute> userMonitoredRoutes = await _userMonitoredRouteRepository.GetAllForSendingAlertsAsync();

        foreach (var userMonitoredRoute in userMonitoredRoutes)
        {
            string message = $"Alert for {userMonitoredRoute.User.Email}: Price drop detected for route {userMonitoredRoute.MonitoredRoute.OriginAirport.Name} to {userMonitoredRoute.MonitoredRoute.DestinationAirport.Name}!";

            Console.WriteLine(message);
            // await _whatsappService.SendMessage();
        }
    }
}