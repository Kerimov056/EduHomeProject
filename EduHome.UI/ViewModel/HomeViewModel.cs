using EduHome.Core.Entities;

namespace EduHome.UI.ViewModel;

public class HomeViewModel
{
    public IEnumerable<Blog> blogs { get; set; }
    public IEnumerable<Slider> sliders { get; set; }
    public IEnumerable<Notice> notice { get; set; }
    public IEnumerable<Info> info { get; set; }
    public IEnumerable<Courses> courses { get; set; }
    public IEnumerable<Categories> categories { get; set; }
    public IEnumerable<CoursesDetails> courses_details { get; set; }
    //public IEnumerable<Upcomming> upcommings { get; set; }
    //public IEnumerable<UpcommingCategory> upcommingCategories { get; set; }
    //public IEnumerable<UpcommingDetails> upcomming_details { get; set; }
    //public IEnumerable<Speakers> speakers { get; set; }
    //public IEnumerable<Company> companies { get; set; }
    //public IEnumerable<CompanySpeakers> companySpeakers { get; set; }


}
