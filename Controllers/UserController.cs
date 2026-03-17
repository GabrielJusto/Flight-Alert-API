
using Flight_Alert_API.DTOs.User;
using Flight_Alert_API.Exceptions;
using Flight_Alert_API.Services.Interfaces;

using Microsoft.AspNetCore.Mvc;

namespace Flight_Alert_API.Controllers;


[ApiController]
[Route("/users")]
public class UserController(
    IUserService userService
) : ControllerBase
{

    private readonly IUserService _userService = userService;

    [HttpPatch("update/{id}")]
    public async Task<IActionResult> UpdateUserInfo(int id, UserUpdateRequest request)
    {
        try
        {
            UserUpdateDto updateDto = new(request, id);
            await _userService.UpdateUserAsync(updateDto);

            return Ok();
        }
        catch(EntityNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch(Exception)
        {
            return StatusCode(500, "Unexpected error");
        }

    }

}