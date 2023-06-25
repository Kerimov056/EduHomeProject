using EduHome.Core.Entities;

namespace EduHome.UI.ViewModel;

public class HomeViewModel
{
    public IEnumerable<Blog> blogs { get; set; }
    public IEnumerable<Slider> sliders { get; set; }
    public IEnumerable<Notice> notice { get; set; }
    public IEnumerable<Info> info { get; set; }

}
