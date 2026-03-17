
using Flight_Alert_API.Services.Interfaces;

namespace Flight_Alert_API.Services.implemetations;

public class FlightPriceService(
    IWhatsappService whatsappService
) : IFlightPriceService
{
    private readonly IWhatsappService _whatsappService = whatsappService;


    public async Task CheckAllFlightPricesAsync()
    {
        await _whatsappService.SendMessage();
    }
}