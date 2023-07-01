using EduHome.Core.Entities;

namespace EduHome.UI.Areas.Admin.Data.Services;

public interface ICoursesServices
{
    Task<IEnumerable<Courses>> GetCourses();
    Task<Courses> CreateAsync(Courses courses);
    Task<Courses> UpdateAsync(int id, Courses courses);
    Task<Courses> GetByIdAsync(int id);
    Task DeleteAsync(int id);
}
