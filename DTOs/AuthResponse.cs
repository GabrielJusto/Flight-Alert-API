
namespace Flight_Alert_API.DTOs;

public record AuthResponse(
    int UserId,
    string Token
);