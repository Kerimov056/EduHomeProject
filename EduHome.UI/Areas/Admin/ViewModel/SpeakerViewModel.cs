using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;
using RequiredAttribute = System.ComponentModel.DataAnnotations.RequiredAttribute;

namespace EduHome.UI.Areas.Admin.ViewModel;

public class SpeakerViewModel
{
    public IFormFile? Image { get; set; } 
    [Required, MaxLength(20)]
    public string Name { get; set; } = null!;
    [Required, MaxLength(30)]
    public string Postions { get; set; } = null!;
    [Required, MaxLength(40)]
    public string JobName { get; set; } = null!;
    public List<int>? SelectedEventIds { get; set; }
}
