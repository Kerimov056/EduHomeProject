using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace EduHome.Core.Entities;

public class User : IdentityUser
{
    public string Fullname { get; set; } = null!;
    public ICollection<CourseComment>? CourseComments { get; set; }
}
