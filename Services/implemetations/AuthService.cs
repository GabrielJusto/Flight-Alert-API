
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

using Flight_Alert_API.Configuration;
using Flight_Alert_API.DTOs;
using Flight_Alert_API.Exceptions;
using Flight_Alert_API.Models;
using Flight_Alert_API.Services.Interfaces;
using Flight_Alert_API.Validations;

using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Flight_Alert_API.Services.implemetations;

public class AuthService(
    IOptions<JwtConfiguration> jwtConfig,
    UserManager<User> userManager,
    IValidationProvider validationProvider,
    ILogger<AuthService> logger) : IAuthService
{
    private readonly JwtConfiguration _jwtConfig = jwtConfig.Value;
    private readonly UserManager<User> _userManager = userManager;
    private readonly IValidationProvider _validationProvider = validationProvider;
    private readonly ILogger<AuthService> _logger = logger;

    public async Task<UserRegisterResponseDto> RegisterUser(UserRegisterRequestDto data)
    {
        try
        {
            List<IValidation> validations = _validationProvider.GetValidations(Enuns.ValidationsSelection.UserRegister);
            Validator validator = new(validations, data);
            await validator.Validate();

            User user = new()
            {
                Email = data.Email,
                UserName = data.Email,
                PhoneNumber = data.PhoneNumber,
                Name = data.Name,
                LastName = data.LastName
            };
            IdentityResult result = await _userManager.CreateAsync(user, data.Password);

            if(!result.Succeeded)
            {
                _logger.LogWarning("User registration failed: {Errors}", result.Errors);
                throw new UserRegisterException("User registration failed: ", result.Errors);
            }
            return new UserRegisterResponseDto(user.Id, CreateToken(user.Id));
        }
        catch(ValidationException ex)
        {
            _logger.LogWarning("Validation failed during user registration: {Errors}", ex.Errors);
            throw;
        }

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
        return new AuthResponse(user.Id, user.Name, user.LastName, user.Email ?? string.Empty, token);
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

    public async Task<AuthResponse?> RenewTokenAsync(string token)
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

            User user = await _userManager.FindByIdAsync(userId.ToString()) ?? throw new EntityNotFoundException("User not found");
            return new AuthResponse(userId, user.Name, user.LastName, user.Email ?? string.Empty, newToken);
        }
        catch
        {
            throw;
        }
    }
}