namespace Flight_Alert_API.DTOs.Route;

public class UserMonitoredRouteUpdateDto
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public bool IsActive { get; set; }
}