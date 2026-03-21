
using System.Text.Json.Serialization;

namespace Flight_Alert_API.DTOs;

public class UserRegisterRequestDto
{
    [JsonPropertyName("email")]
    public required string Email { get; set; } = null!;

    [JsonPropertyName("password")]
    public required string Password { get; set; } = null!;

    [JsonPropertyName("phoneNumber")]
    public required string PhoneNumber { get; set; } = null!;

    [JsonPropertyName("name")]
    public required string Name { get; set; } = null!;

    [JsonPropertyName("lastName")]
    public required string LastName { get; set; } = null!;
}