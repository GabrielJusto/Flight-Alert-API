using Flight_Alert_API.DTOs.Route;
using Flight_Alert_API.Exceptions;
using Flight_Alert_API.Services.Interfaces;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Flight_Alert_API.Controllers;

[Authorize(AuthenticationSchemes = "Bearer")]
[ApiController]
[Route("/user-monitored-routes")]
public class UserMonitoredRouteController(
    IUserMonitoredRouteService userMonitoredRouteService,
    IJwtTokenService jwtTokenService,
    ILogger<UserMonitoredRouteController> logger
) : ControllerBase
{

    private readonly IUserMonitoredRouteService _userMonitoredRouteService = userMonitoredRouteService;
    private readonly IJwtTokenService _jwtTokenService = jwtTokenService;
    private readonly ILogger<UserMonitoredRouteController> _logger = logger;

    [HttpPatch("update/{id}")]
    public async Task<IActionResult> UpdateRoute(int id, [FromBody] UserMonitoredRouteUpdateRequest request)
    {
        try
        {
            int userId = _jwtTokenService.GetUserId(User);
            await _userMonitoredRouteService.UpdateMonitoredRouteAsync(new UserMonitoredRouteUpdateDto
            {
                Id = id,
                IsActive = request.IsActive,
                UserId = userId
            });

            return Ok();
        }
        catch(EntityNotFoundException ex)
        {
            _logger.LogWarning("Entity not found while updating route {RouteId}: {Message}", id, ex.Message);
            return NotFound(new { error = ex.Message });
        }
        catch(UnauthorizedAccessException ex)
        {
            _logger.LogWarning("Unauthorized access attempt to update user monitored route {RouteId}: {Message}", id, ex.Message);
            return StatusCode(403, new { error = ex.Message });
        }
        catch(Exception ex)
        {
            _logger.LogError(ex, "An unexpected error occurred while updating user monitored route:{@data}", new { RouteId = id, Request = request });
            return StatusCode(500, new { error = "An unexpected error occurred." });
        }
    }
}