using System.Security.Claims;

using Flight_Alert_API.Services.Interfaces;

namespace Flight_Alert_API.Services.implemetations;

public class JwtTokenService : IJwtTokenService
{
    public void CheckUserId(int userId, ClaimsPrincipal user)
    {
        int userIdFromToken = int.Parse(user.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value ?? "0");
        if(userIdFromToken != userId)
        {
            throw new UnauthorizedAccessException("User ID does not match token.");
        }
    }
}