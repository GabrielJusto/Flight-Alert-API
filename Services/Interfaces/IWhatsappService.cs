
using Flight_Alert_API.Models;

namespace Flight_Alert_API.Services.Interfaces;

public interface IWhatsappService
{
    public Task SendMessage(FlightNotification data);
}