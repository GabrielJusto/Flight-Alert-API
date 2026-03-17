
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

using Flight_Alert_API.Configuration;
using Flight_Alert_API.DTOs;
using Flight_Alert_API.Exceptions;
using Flight_Alert_API.Models;
using Flight_Alert_API.Services.Interfaces;

using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Flight_Alert_API.Services.implemetations;

public class AuthService(
    IOptions<JwtConfiguration> jwtConfig,
    UserManager<User> userManager) : IAuthService
{
    private readonly JwtConfiguration _jwtConfig = jwtConfig.Value;
    private readonly UserManager<User> _userManager = userManager;

    public async Task<UserRegisterResponseDto> RegisterUser(UserRegisterRequestDto data)
    {
        User user = new()
        {
            Email = data.Email,
            UserName = data.Email,
            PhoneNumber = data.PhoneNumber
        };
        IdentityResult result = await _userManager.CreateAsync(user, data.Password);

        if(!result.Succeeded)
        {
            throw new UserRegisterException("User registration failed: ", result.Errors);
        }
        return new UserRegisterResponseDto(user.Id, CreateToken(user.Id));
    }

    public async Task<AuthResponse?> LoginAsync(AuthenticationDto authDto)
    {

        User? user = await _userManager.FindByEmailAsync(authDto.Username);
        if(user == null)
        {
            return null;
        }

        bool isPasswordValid = await _userManager.CheckPasswordAsync(user, authDto.Password);
        if(!isPasswordValid)
        {
            return null;
        }

        string token = CreateToken(user.Id);
        return new AuthResponse(token);
    }


    private string CreateToken(int userId)
    {
        SymmetricSecurityKey key = new(Encoding.ASCII.GetBytes(_jwtConfig.Secret));
        SigningCredentials creds = new(key, SecurityAlgorithms.HmacSha256);

        Claim[] claims =
        [
            new Claim(JwtRegisteredClaimNames.Sub, userId.ToString()),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        ];

        JwtSecurityToken token = new(
            issuer: _jwtConfig.Issuer,
            audience: _jwtConfig.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(_jwtConfig.ExpiryInMinutes),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public AuthResponse? RenewToken(string token)
    {
        try
        {
            JwtSecurityTokenHandler tokenHandler = new();
            byte[] key = Encoding.ASCII.GetBytes(_jwtConfig.Secret);

            TokenValidationParameters validationParameters = new()
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = true,
                ValidIssuer = _jwtConfig.Issuer,
                ValidateAudience = true,
                ValidAudience = _jwtConfig.Audience,
                ValidateLifetime = false,
                ClockSkew = TimeSpan.Zero
            };

            ClaimsPrincipal principal = tokenHandler.ValidateToken(token, validationParameters, out SecurityToken validatedToken);


            string? userIdClaim = principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if(string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out int userId))
            {
                return null;
            }

            string newToken = CreateToken(userId);
            return new AuthResponse(newToken);
        }
        catch
        {
            return null;
        }
    }
}