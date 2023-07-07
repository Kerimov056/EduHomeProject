using EduHome.Core.Entities;
using EduHome.UI.Areas.Admin.Data.Base;
using EduHome.UI.Areas.Admin.Data.Exception;
using EduHome.UI.Areas.Admin.Data.Services.Interfaces;
using EduHome.UI.Areas.Admin.ViewModel;
using EduHome.UI.ViewModel;
using EduHomeDataAccess.Database;
using Microsoft.EntityFrameworkCore;

namespace EduHome.UI.Areas.Admin.Data.Services.Concrets;

public class CoursesServices : ICoursesServices
{
    private readonly AppDbContext _context;
    public CoursesServices(AppDbContext context)
    {
        _context = context;
    }

    public Task CreateAsync(CourseFullDetailsViewModel CourseFullDetailsViewModel)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<Courses> FindByIdAsync(int id)
    {
        var course = await _context.Coursess.Include(cd=>cd.CoursesDetails).FirstOrDefaultAsync(c=>c.Id==id);
        if (course is null)  throw new NotFoundException("Course not found");
        return course;
    }

    public async Task<IEnumerable<Courses>> GetCourses()
    {
       var course = await _context.Coursess
            .Include(c => c.Categories)
            .ThenInclude(cd => cd.Courses)
            .Include(c => c.CoursesDetails)
            .ToListAsync();

        return course;
    }
    public Task UpdateAsync(int id, CourseFullDetailsViewModel CourseFullDetailsViewModel)
    {
        throw new NotImplementedException();
    }
}
