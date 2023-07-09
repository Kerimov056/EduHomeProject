using Microsoft.AspNetCore.Identity;

namespace EduHome.Core.Entities;

public class User: IdentityUser
{
    public string Fullname { get; set; } = null!;

}
