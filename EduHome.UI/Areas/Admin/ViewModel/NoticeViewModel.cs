using Microsoft.Build.Framework;

namespace EduHome.UI.Areas.Admin.ViewModel;

public class NoticeViewModel
{
    [Required]
    public string? Description { get; set; }
    [Required]
    public DateTime? Datatime { get; set; }
}
