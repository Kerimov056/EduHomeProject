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
    public IEnumerable<Events> events { get; set; }
    public IEnumerable<EventsDetails> eventsDetails { get; set; }
    public IEnumerable<Speakers> speakers { get; set; }
    public IEnumerable<Events_Speakers> events_Speakers { get; set; }
    public IEnumerable<About> abouts { get; set; }
    public IEnumerable<TeacherDetails> teacherDetails { get; set; }
    public IEnumerable<Teacher> teachers { get; set; }
    public IEnumerable<Setting> settings { get; set; }
    public IEnumerable<Viewer> viewers { get; set; }

}
