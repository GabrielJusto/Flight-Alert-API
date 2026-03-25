

using Flight_Alert_API.DTOs.Route;
using Flight_Alert_API.Exceptions;
using Flight_Alert_API.Services.Interfaces;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Flight_Alert_API.Controllers;

[Authorize(AuthenticationSchemes = "Bearer")]
[ApiController]
[Route("/routes")]
public class RouteController(
    IMonitoredRouteService monitoredRouteService,
    ILogger<RouteController> logger,
    IWebHostEnvironment env,
    IJwtTokenService jwtTokenService
) : ControllerBase
{
    private readonly IMonitoredRouteService _monitoredRouteService = monitoredRouteService;
    private readonly ILogger<RouteController> _logger = logger;
    private readonly IWebHostEnvironment _env = env;
    private readonly IJwtTokenService _jwtTokenService = jwtTokenService;

    [HttpPost("insert")]
    public async Task<IActionResult> InsertRoute([FromBody] RouteRegisterRequest request)
    {
        try
        {
            _logger.LogInformation("Inserting monitored route: {@request}", request);
            _jwtTokenService.CheckUserId(request.UserId, User);
            await _monitoredRouteService.InsertMonitoredRouteAsync(request);
            return Ok();
        }
        catch(EntityNotFoundException ex)
        {
            _logger.LogWarning("Entity not found while inserting route for user {UserId}: {Message}", request.UserId, ex.Message);
            return NotFound(new { error = ex.Message });
        }
        catch(UnauthorizedAccessException ex)
        {
            _logger.LogWarning("Unauthorized access attempt to insert route for user {UserId}: {Message}", request.UserId, ex.Message);
            return StatusCode(403, new { error = ex.Message });
        }
        catch(Exception ex)
        {
            _logger.LogError(ex, "An unexpected error occurred while inserting route for user {UserId}", request.UserId);
            return StatusCode(500, new { error = "An unexpected error occurred." });
        }
    }

    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> DeleteRoute(int id)
    {
        try
        {
            await _monitoredRouteService.DeleteMonitoredRouteAsync(id);
            return Ok();
        }
        catch(EntityNotFoundException ex)
        {
            return NotFound(new { error = ex.Message });
        }
        catch(Exception)
        {
            return StatusCode(500, new { error = "An unexpected error occurred." });
        }
    }

    [HttpGet("get-all/{userId}")]
    public async Task<IActionResult> GetAllRoutes(int userId)
    {
        try
        {
            _jwtTokenService.CheckUserId(userId, User);
            _logger.LogInformation("Getting all monitored routes for user {UserId}", userId);
            List<MonitoredRouteDetail> routes = await _monitoredRouteService.GetUserMonitoredRoutesAsync(userId);
            return Ok(routes);
        }
        catch(UnauthorizedAccessException ex)
        {
            _logger.LogWarning("Unauthorized access attempt to get routes for user {UserId}: {Message}", userId, ex.Message);
            return StatusCode(403, new { error = ex.Message });
        }
        catch(Exception ex)
        {
            _logger.LogError(ex, "An unexpected error occurred while getting all routes for user {UserId}", userId);
            return StatusCode(500, new { error = "An unexpected error occurred." });
        }
    }

    [HttpPost("process-notifications")]
    public async Task<IActionResult> ProcessNotifications([FromServices] IFlightPriceService priceService)
    {
        if(!_env.IsDevelopment())
            return NotFound();

        await priceService.CheckAllFlightPricesAsync();
        return Ok(new { message = "Rota disponível apenas em desenvolvimento." });
    }

    [HttpPost("send-alerts")]
    public async Task<IActionResult> SendAlerts([FromServices] ISendAlertsService sendAlertsService)
    {
        if(!_env.IsDevelopment())
            return NotFound();

        await sendAlertsService.SendAlertsAsync();
        return Ok(new { message = "Rota disponível apenas em desenvolvimento." });
    }
}