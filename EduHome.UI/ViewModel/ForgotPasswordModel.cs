using System.ComponentModel.DataAnnotations;

namespace EduHome.UI.ViewModel;

public class ForgotPasswordModel
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }
}
