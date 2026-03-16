
using Flight_Alert_API.DTOs;
using Flight_Alert_API.Exceptions;
using Flight_Alert_API.Services.Interfaces;

using Microsoft.AspNetCore.Mvc;

namespace Flight_Alert_API.Controllers;

[ApiController]
[Route("/auth")]
public class AuthController(
    IAuthService authService

    ) : ControllerBase
{
    private readonly IAuthService _authService = authService;



    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] UserRegisterRequestDto data)
    {
        try
        {
            UserRegisterResponseDto response = await _authService.RegisterUser(data);
            return Ok(response);
        }
        catch(UserRegisterException ex)
        {
            return BadRequest(new { error = ex.Message, details = ex.Errors });
        }
        catch(Exception)
        {
            return StatusCode(500, new { error = "An unexpected error occurred." });
        }

    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] AuthenticationDto authDto)
    {
        AuthResponse? response = await _authService.LoginAsync(authDto);
        if(response == null)
        {
            return Unauthorized();
        }

        return Ok(response);
    }

    [HttpPost("refresh-token")]
    public IActionResult RefreshToken([FromHeader(Name = "Authorization")] string authorization)
    {
        try
        {
            string token = authorization?.Replace("Bearer ", "") ?? string.Empty;
            AuthResponse? response = _authService.RenewToken(token);
            if(response == null)
            {
                return Unauthorized();
            }

            return Ok(response);
        }
        catch(Exception)
        {
            return StatusCode(500, new { error = "An unexpected error occurred." });
        }
    }
}