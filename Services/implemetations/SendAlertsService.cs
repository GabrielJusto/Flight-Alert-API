using Flight_Alert_API.Models;
using Flight_Alert_API.Repositories.Interfaces;
using Flight_Alert_API.Services.Interfaces;

namespace Flight_Alert_API.Services.implemetations;

public class SendAlertsService(
    IWhatsappService whatsappService,
    IFlightNotificationRepository flightNotificationRepository
) : ISendAlertsService
{

    private readonly IWhatsappService _whatsappService = whatsappService;
    private readonly IFlightNotificationRepository _flightNotificationRepository = flightNotificationRepository;

    public async Task SendAlertsAsync()
    {

        List<FlightNotification> notifications = await _flightNotificationRepository.GetAllAsync();

        foreach(var notification in notifications)
        {
            await _whatsappService.SendMessage(notification);
        }

        await _flightNotificationRepository.DeleteAllAsync();



    }
}