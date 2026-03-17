
using System.Text.Json.Serialization;

namespace Flight_Alert_API.DTOs.User;

public class UserUpdateRequest
{
    [JsonPropertyName("phoneNumber")]
    public string PhoneNumber { get; set; } = string.Empty;
}
