using Microsoft.AspNetCore.Identity;

namespace Revas.Core.Entities;

public class UserSide:IdentityUser
{
    public string? Fullname { get; set; }
    public bool IsActive { get; set; }
}
