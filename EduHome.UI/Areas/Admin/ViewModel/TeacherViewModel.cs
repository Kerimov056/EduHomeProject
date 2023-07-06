using System.ComponentModel.DataAnnotations;

namespace EduHome.UI.Areas.Admin.ViewModel;

public class TeacherViewModel
{

    public IFormFile Image { get; set; }
    [Required,MaxLength(20)]
    public string Name { get; set; } = null!;
    [Required,MaxLength(30)]
    public string Posistion { get; set; } = null!;
    [Required,MaxLength(50)]
    public string Degree { get; set; }
    [Required]
    public int Experince { get; set; }
    [Required, MaxLength(50)]
    public string Hobbies { get; set; } = null!;
    [Required, MaxLength(50)]
    public string Facultry { get; set; } = null!;
    [Required, MaxLength(200)]
    public string Email { get; set; } = null!;
    [Required,MaxLength(40)]
    public string Phone { get; set; } = null!;
    [Required,MaxLength(40)]
    public string Skype { get; set; }
    [Required]
    public int Language { get; set; }
    [Required]
    public int TeamLeader { get; set; }
    [Required]
    public int Development   { get; set; }
    [Required]
    public int Design { get; set; }
    [Required]
    public int Innovation { get; set; }
    [Required]
    public int Communication { get; set; }

    [Required, MaxLength(700)]
    public string AboutMe { get; set; } = null!;
}
