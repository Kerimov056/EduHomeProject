using EduHome.Core.Entities;

namespace EduHome.UI.HomeVM;

public class HomeVM
{
    public IEnumerable<Blog> blogs { get; set; }
    public IEnumerable<Slider> sliders { get; set; }
}
