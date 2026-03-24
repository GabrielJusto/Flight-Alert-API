using Flight_Alert_API.Repositories.Implementations;
using Flight_Alert_API.Repositories.Interfaces;
using Flight_Alert_API.Services.implemetations;
using Flight_Alert_API.Services.Interfaces;
using Flight_Alert_API.Validations;
using Flight_Alert_API.Validations.Auth;
namespace Flight_Alert_API.Configuration;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddFlightAlertServices(this IServiceCollection services)
    {
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IMonitoredRouteService, MonitoredRouteService>();
        services.AddScoped<IMonitoredRouteRepository, MonitoredRouteRepository>();
        services.AddScoped<IAirportRepository, AirportRepository>();
        services.AddScoped<IFlightPriceService, FlightPriceService>();
        services.AddScoped<IWhatsappService, TwilioService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IUserMonitoredRouteRepository, UserMonitoredRouteRepository>();
        services.AddScoped<IMonitoredRouteService, MonitoredRouteService>();
        services.AddScoped<ISendAlertsService, SendAlertsService>();
        services.AddScoped<ISerpGoogleFlightsService, SerpGoogleFlightsService>();
        services.AddScoped<IFlightNotificationRepository, FlightNotificationRepository>();
        services.AddScoped<IGoogleLinkService, GoogleLinkService>();
        services.AddScoped<IValidationProvider, ValidationProvider>();
        services.AddScoped<PhoneNumberAlreadyExistsValidation>();
        services.AddScoped<IAirportService, AirportService>();
        return services;
    }
}