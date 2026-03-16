
using Flight_Alert_API.DTOs;

namespace Flight_Alert_API.Services.Interfaces;

public interface IAuthService
{
    public Task<UserRegisterResponseDto> RegisterUser(UserRegisterRequestDto data);
    public Task<AuthResponse?> LoginAsync(AuthenticationDto authDto);
    public AuthResponse? RenewToken(string token);
}
