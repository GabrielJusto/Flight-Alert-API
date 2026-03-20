
using Flight_Alert_API.Services.Interfaces;

using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace Flight_Alert_API.Services.implemetations;

using Flight_Alert_API.Configuration;
using Flight_Alert_API.Models;

using Microsoft.Extensions.Options;

public class TwilioService(
    ILogger<TwilioService> logger,
    IOptions<TwilioConfiguration> twilioOptions
) : IWhatsappService
{
    private readonly TwilioConfiguration _twilioConfig = twilioOptions.Value;
    private readonly ILogger<TwilioService> _logger = logger;


    public async Task SendMessage(FlightNotification data)
    {
        try
        {
            _logger.LogInformation("Start processing notification FlightNotificationId: {FlightNotificationId}", data.FlightNotificationId);
            TwilioClient.Init(_twilioConfig.AccountSid, _twilioConfig.AuthToken);

            string? phoneNumber = data.UserMonitoredRoute.User.PhoneNumber;
            if(string.IsNullOrEmpty(phoneNumber))
            {
                _logger.LogError("User with ID: {UserId}, does not have a phone number. Cannot send WhatsApp message for FlightNotificationId: {FlightNotificationId}.", data.UserMonitoredRoute.User?.Id, data.FlightNotificationId);
                throw new ArgumentException("User phone number is not provided.");
            }

            _logger.LogInformation("Sending WhatsApp message to {PhoneNumber} for FlightNotificationId: {FlightNotificationId}", phoneNumber, data.FlightNotificationId);
            CreateMessageOptions messageOptions = new(
                new PhoneNumber($"whatsapp:{phoneNumber}")
            )
            {
                From = new PhoneNumber(_twilioConfig.FromPhoneNumber),
                ContentSid = "HX696be6112000fe7b227d78ba94b48d63",
                ContentVariables = $"{{\"origin\":\"{data.UserMonitoredRoute.MonitoredRoute.OriginAirport.IataCode}\",\"destination\":\"{data.UserMonitoredRoute.MonitoredRoute.DestinationAirport.IataCode}\",\"price\":\"{data.Price}\",\"date\":\"{data.FlightDate:dd/MM/yyyy HH:mm}\"}}"
            };

            MessageResource message = await MessageResource.CreateAsync(messageOptions);
            _logger.LogInformation("WhatsApp message sent for FlightNotificationId: {FlightNotificationId}. Message SID: {MessageSid}", data.FlightNotificationId, message.Sid);
        }
        catch(Exception ex)
        {
            _logger.LogError(ex, "An error occurred while sending WhatsApp message for FlightNotificationId: {FlightNotificationId}", data.FlightNotificationId);
            throw;
        }
    }
}