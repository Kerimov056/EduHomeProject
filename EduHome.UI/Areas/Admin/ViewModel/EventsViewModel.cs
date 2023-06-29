using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;
using RequiredAttribute = Microsoft.Build.Framework.RequiredAttribute;

namespace EduHome.UI.Areas.Admin.ViewModel;

public class EventsViewModel
{
    [Required,MaxLength(40)]
    public string Name { get; set; } = null!;
    [Required,MaxLength(60)]
    public string Location { get; set; } = null!;
    public DateTime DateTime { get; set; } 
    public IFormFile Image { get; set; }
    [RequiredAttribute,MaxLength(1200)]
    public string Description { get; set; } = null!;
}
