using EduHome.Core.Entities;
using EduHome.UI.Areas.Admin.ViewModel;

namespace EduHome.UI.Areas.Admin.Data.Services.Interfaces;

public interface ITeacherService
{
    Task<IEnumerable<Teacher>> GetTeacher();
    Task CreateAsync(TeacherViewModel teacherViewModel);
    Task EditAsync(int id, TeacherViewModel teacherViewModel);
    Task<Teacher> FindByTeacherAsync(int id);
    Task DeleteAsync(int id);
}
