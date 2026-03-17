
using Flight_Alert_API.Services.Interfaces;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace Flight_Alert_API.Services.implemetations;

using Flight_Alert_API.Configuration;
using Microsoft.Extensions.Options;

public class TwilioService : IWhatsappService
{
    private readonly TwilioConfiguration _twilioConfig;

    public TwilioService(IOptions<TwilioConfiguration> twilioOptions)
    {
        _twilioConfig = twilioOptions.Value;
    }

    public async Task SendMessage()
    {
        TwilioClient.Init(_twilioConfig.AccountSid, _twilioConfig.AuthToken);

        CreateMessageOptions messageOptions = new(
            new PhoneNumber("whatsapp:+55")
        )
        {
            From = new PhoneNumber("whatsapp:"),
            ContentSid = _twilioConfig.ContentSid,
            ContentVariables = "{\"1\":\"12/1\",\"2\":\"3pm\"}"
        };

        MessageResource message = await MessageResource.CreateAsync(messageOptions);
        Console.WriteLine(message.Body);
    }
}