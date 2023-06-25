using Microsoft.Build.Framework;

namespace EduHome.UI.Areas.Admin.ViewModel;

public class InfoViewModel
{
    [Required]
    public string? Title { get; set; }
    [Required]
    public string? Description { get; set; }
}
