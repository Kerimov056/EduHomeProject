using EduHome.Core.Entities;

namespace EduHome.UI.Areas.Admin.Data.Services.Interfaces;

public interface ICourseDetailsServices
{
    Task<CoursesDetails> FindByIdAsync(int id);
}
