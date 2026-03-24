
namespace Flight_Alert_API.Configuration;

public static class CorsExtensions
{
    public static IServiceCollection AddCorsPolicy(this IServiceCollection services, IConfiguration configuration)
    {
        string[]? allowedGateways = configuration.GetSection("AllowedGateways").Get<string[]>();
        services.AddCors(options =>
        {
            options.AddPolicy("AllowMyWebsite", policy =>
            {
                policy.WithOrigins(allowedGateways ?? System.Array.Empty<string>())
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials();
            });
        });
        return services;
    }
}