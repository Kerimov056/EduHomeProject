using Microsoft.AspNetCore.Authentication;

namespace EduHome.UI.ViewModel;

public class LoginVM
{
    public string Email { get; set; }
    public string Password { get; set; }
    public bool IsLoggedIn { get; set; }

    public string ReturnUrl { get; set; }

    public IList<AuthenticationScheme> ExternalLogins { get; set; }
}
