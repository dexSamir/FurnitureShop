using Microsoft.AspNetCore.Identity;

namespace FurnitureShop.Core.Entities;

public class User : IdentityUser
{
    public string Name { get; set; } = null!; 
    public string? Surname { get; set; }
    public int Age { get; set; }
    public bool Gender { get; set; }
    public bool IsVerified { get; set; }
    public DateTime CreatedTime { get; set; }
}