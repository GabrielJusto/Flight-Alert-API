
using System.Security.Claims;

namespace Flight_Alert_API.Services.Interfaces;

public interface IJwtTokenService
{
    public void CheckUserId(int userId, ClaimsPrincipal user);
    public int GetUserId(ClaimsPrincipal user);
}