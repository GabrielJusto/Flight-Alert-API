
using Flight_Alert_API.Configuration;
using Flight_Alert_API.Database;
using Flight_Alert_API.Services.Interfaces;

using Hangfire;

using Microsoft.EntityFrameworkCore;

using Serilog;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("Logs/log-.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog();

builder.Services.Configure<JwtConfiguration>(builder.Configuration.GetSection("Jwt"));
builder.Services.Configure<TwilioConfiguration>(builder.Configuration.GetSection("Twilio"));
builder.Services.Configure<SerpApiConfiguration>(builder.Configuration.GetSection("SerpApi"));

builder.Services.AddControllers();

// Configuração de CORS extraída para extensão
builder.Services.AddCorsPolicy(builder.Configuration);

// Configuração de autenticação extraída para extensão
builder.Services.AddJwtAuthentication(builder.Configuration);

// Configurar DbContext com PostgreSQL
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Configuração do Identity extraída para extensão
builder.Services.AddIdentityConfiguration();

// Configuração do Hangfire extraída para extensão
builder.Services.AddHangfireConfiguration(builder.Configuration);

builder.Services.AddHttpClient();

// Registrar serviços personalizados
builder.Services.AddFlightAlertServices();

builder.Services.AddOpenApi();

WebApplication app = builder.Build();


if(app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseHangfireDashboard("/hangfire");
}

if(app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseCors("AllowMyWebsite");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

using(IServiceScope scope = app.Services.CreateScope())
{
    IRecurringJobManager recurringJobManager = scope.ServiceProvider.GetRequiredService<IRecurringJobManager>();

    recurringJobManager.AddOrUpdate<IFlightPriceService>(
        "check-flight-prices",
        service => service.CheckAllFlightPricesAsync(),
        Cron.Daily(7));

    recurringJobManager.AddOrUpdate<ISendAlertsService>(
        "send-alerts",
        service => service.SendAlertsAsync(),
        Cron.Daily(7, 30));
}

app.Run();
