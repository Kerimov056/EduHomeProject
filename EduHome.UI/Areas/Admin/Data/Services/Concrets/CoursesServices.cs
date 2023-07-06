using EduHome.Core.Entities;
using EduHome.UI.Areas.Admin.Data.Base;
using EduHome.UI.Areas.Admin.Data.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EduHome.UI.Areas.Admin.Data.Services.Concrets;

public class CoursesServices : ICoursesServices
{
    private readonly IEntityBaseRepository<Courses> _coursesRepository;
    public CoursesServices(IEntityBaseRepository<Courses> coursesRepository)
    {
        _coursesRepository = coursesRepository;
    }

    public Task DeleteAsync(int id) => _coursesRepository.DeleteAsync(id);

    public async Task<Courses> GetByIdAsync(int id) => await _coursesRepository.GetByIdAsync(id);

    public async Task<IEnumerable<Courses>> GetCourses() => await _coursesRepository.GetAllAsync();
    public async Task<Courses> CreateAsync(Courses courses)
    {
        await _coursesRepository.AddAsync(courses);
        return courses;
    }

    public async Task<Courses> UpdateAsync(int id, Courses courses)
    {
        await _coursesRepository.UpdateAsync(id, courses);
        return courses;
    }
}
