namespace Flight_Alert_API.DTOs.User;

public class UserUpdateDto(UserUpdateRequest request, int id)
{
    public int Id { get; } = id;
    public string? PhoneNumber { get; } = request.PhoneNumber;
}