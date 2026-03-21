
using Microsoft.AspNetCore.Identity;

namespace Flight_Alert_API.Models;

public class User : IdentityUser<int>
{
    public string Name { get; set; } = null!;
    public string LastName { get; set; } = null!;
}