using Flight_Alert_API.DTOs;
using Flight_Alert_API.Services.Interfaces;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Flight_Alert_API.Controllers;


[ApiController]
[Route("airports")]
[Authorize(AuthenticationSchemes = "Bearer")]
public class AirportController(
    IAirportService airportService,
    ILogger<AirportController> logger
) : ControllerBase
{

    private readonly IAirportService _airportService = airportService;
    private readonly ILogger _logger = logger;
    [HttpGet]
    public async Task<IActionResult> GetAirports()
    {
        try
        {
            List<AirportDetail> airports = await _airportService.GetAirportsAsync();
            return Ok(airports);

        }
        catch(Exception ex)
        {
            _logger.LogError(ex, "Request error on route GET /airports.");
            return StatusCode(500, new { error = "An unexpected error occurred." });
        }
    }
}