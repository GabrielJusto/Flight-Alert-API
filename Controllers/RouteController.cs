

using Flight_Alert_API.DTOs.Route;
using Flight_Alert_API.Exceptions;
using Flight_Alert_API.Services.Interfaces;

using Microsoft.AspNetCore.Mvc;

namespace Flight_Alert_API.Controllers;

[ApiController]
[Route("/routes")]
public class RouteController(
    IMonitoredRouteService monitoredRouteService
): ControllerBase
{
    private readonly IMonitoredRouteService _monitoredRouteService = monitoredRouteService;
    
    
    [HttpPost("insert")]
    public async Task<IActionResult> InsertRoute([FromBody] RouteRegisterRequest request)
    {
        try
        {
            await _monitoredRouteService.InsertMonitoredRouteAsync(request);
            return Ok();
        }catch(EntityNotFoundException ex)
        {
            return NotFound(new { error = ex.Message });
        }
        catch(Exception)
        {
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
        catch (EntityNotFoundException ex)
        {
            return NotFound(new { error = ex.Message });
        }
        catch (Exception)
        {
            return StatusCode(500, new { error = "An unexpected error occurred." });
        }
    }
}