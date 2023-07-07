using EduHome.Core.Entities;
using EduHome.UI.Areas.Admin.ViewModel;
using EduHome.UI.ViewModel;

namespace EduHome.UI.Areas.Admin.Data.Services.Interfaces;

public interface ICoursesServices
{
    Task<IEnumerable<Courses>> GetCourses();
    Task CreateAsync(CourseFullDetailsViewModel CourseFullDetailsViewModel);
    Task UpdateAsync(int id, CourseFullDetailsViewModel CourseFullDetailsViewModel);
    Task<Courses> FindByIdAsync(int id);
    Task DeleteAsync(int id);
}
