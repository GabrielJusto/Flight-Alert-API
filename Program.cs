using Flight_Alert_API.Configuration;
using Flight_Alert_API.Database;
using Flight_Alert_API.Models;
using Flight_Alert_API.Repositories.Implementations;
using Flight_Alert_API.Repositories.Interfaces;
using Flight_Alert_API.Services.implemetations;
using Flight_Alert_API.Services.Interfaces;

using Hangfire;
using Hangfire.PostgreSql;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.Configure<JwtConfiguration>(builder.Configuration.GetSection("Jwt"));
builder.Services.Configure<TwilioConfiguration>(builder.Configuration.GetSection("Twilio"));
builder.Services.Configure<SerpApiConfiguration>(builder.Configuration.GetSection("SerpApi"));

builder.Services.AddControllers();
// Configurar DbContext com PostgreSQL
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Configurar Identity
builder.Services.AddIdentity<User, IdentityRole<int>>(options =>
{
    // Configurações de senha
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequiredLength = 8;

    // Configurações de usuário
    options.User.RequireUniqueEmail = true;

    // Configurações de login
    options.SignIn.RequireConfirmedEmail = false;
})
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();


// Adicionar Hangfire
builder.Services.AddHangfire(config => config
    .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
    .UseSimpleAssemblyNameTypeSerializer()
    .UseRecommendedSerializerSettings()
    .UsePostgreSqlStorage(options =>
        options.UseNpgsqlConnection(builder.Configuration.GetConnectionString("DefaultConnection"))));

builder.Services.AddHangfireServer();


builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IMonitoredRouteService, MonitoredRouteService>();
builder.Services.AddScoped<IMonitoredRouteRepository, MonitoredRouteRepository>();
builder.Services.AddScoped<IAirportRepository, AirportRepository>();
builder.Services.AddScoped<IFlightPriceService, FlightPriceService>();
builder.Services.AddScoped<IWhatsappService, TwilioService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserMonitoredRouteRepository, UserMonitoredRouteRepository>();
builder.Services.AddScoped<IMonitoredRouteService , MonitoredRouteService>();
builder.Services.AddScoped<ISendAlertsService, SendAlertsService>();
builder.Services.AddScoped<ISerpGoogleFlightsService, SerpGoogleFlightsService>();
builder.Services.AddScoped<IFlightNotificationRepository, FlightNotificationRepository>();

builder.Services.AddOpenApi();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseHangfireDashboard("/hangfire");
}

if(app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.MapControllers();

RecurringJob.AddOrUpdate<IFlightPriceService>(
    "check-flight-prices",
    service => service.CheckAllFlightPricesAsync(),
    Cron.Daily(7)); 

RecurringJob.AddOrUpdate<ISendAlertsService>(
    "send-alerts",
    service => service.SendAlertsAsync(),
    Cron.Daily(7, 30));
app.Run();
