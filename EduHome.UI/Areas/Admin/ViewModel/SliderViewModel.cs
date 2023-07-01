using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;
using RequiredAttribute = System.ComponentModel.DataAnnotations.RequiredAttribute;

namespace EduHome.UI.Areas.Admin.ViewModel;

public class SliderViewModel
{
    public IFormFile image { get; set; }
    [Required,MaxLength(40)]
    public string Name { get; set; } = null!;
    [Required, MaxLength(40)]
    public string NameTwo { get; set; } = null!;
    [Required, MaxLength(120)]
    public string Information { get; set; } = null!;
}
