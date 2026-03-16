
using System.Text.Json.Serialization;

namespace Flight_Alert_API.DTOs;

public class UserRegisterRequestDto
{
    [JsonPropertyName("email")]
    public string Email { get; set; } = string.Empty;

    [JsonPropertyName("password")]
    public string Password { get; set; } = string.Empty;
}