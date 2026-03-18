
using Flight_Alert_API.Services.Interfaces;

using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace Flight_Alert_API.Services.implemetations;

using Flight_Alert_API.Configuration;
using Flight_Alert_API.Models;

using Microsoft.Extensions.Options;

public class TwilioService : IWhatsappService
{
    private readonly TwilioConfiguration _twilioConfig;

    public TwilioService(IOptions<TwilioConfiguration> twilioOptions)
    {
        _twilioConfig = twilioOptions.Value;
    }

    public async Task SendMessage(FlightNotification data)
    {
        TwilioClient.Init(_twilioConfig.AccountSid, _twilioConfig.AuthToken);

        CreateMessageOptions messageOptions = new(
            new PhoneNumber("whatsapp:+55")
        )
        {
            From = new PhoneNumber("whatsapp:+"),
            ContentSid = "HX2d281e2dc32c80ae3802a14c9c00c0d3",
            ContentVariables = $"{{\"Origin\":\"{data.MonitoredRoute.OriginAirport.Name}\",\"Destination\":\"{data.MonitoredRoute.DestinationAirport.Name}\",\"Price\":\"{data.Price}\"}}"
        };

        MessageResource message = await MessageResource.CreateAsync(messageOptions);
        Console.WriteLine(message.Body);
    }
}