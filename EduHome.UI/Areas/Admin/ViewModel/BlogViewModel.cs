using System.ComponentModel.DataAnnotations;

namespace EduHome.UI.Areas.Admin.ViewModel;

public class BlogViewModel
{
    public IFormFile ImagePath { get; set; } = null!;
    [Required, MaxLength(90)]
    public string Name { get; set; } = null!;
    [Required, MaxLength(30)]
    public string PersonName { get; set; } = null!;
    public DateTime Data_Time { get; set; }
    public int MessageNum { get; set; }
}
