using EduHome.Core.Entities;
using EduHome.UI.Areas.Admin.Data.Base;
using EduHome.UI.Areas.Admin.Data.Exception;
using EduHome.UI.Areas.Admin.Data.Services.Interfaces;
using EduHome.UI.Areas.Admin.Extension;
using EduHome.UI.Areas.Admin.ViewModel;
using EduHome.UI.ViewModel;
using EduHomeDataAccess.Database;
using Microsoft.EntityFrameworkCore;

namespace EduHome.UI.Areas.Admin.Data.Services.Concrets;

public class CoursesServices : ICoursesServices
{
    private readonly AppDbContext _context;
    private readonly IWebHostEnvironment _env;
    public CoursesServices(AppDbContext context, IWebHostEnvironment env)
    {
        _context = context;
        _env = env;
    }

    public async Task CreateAsync(CourseFullDetailsViewModel CourseFullDetailsViewModel, int CategoryId)
    {
        if (!CourseFullDetailsViewModel.ImagePath.FormatFile("image"))
        {
            throw new ArgumentException("Select correct image format!");
        }
        if (!CourseFullDetailsViewModel.ImagePath.FormatLength(100))
        {
            throw new ArgumentException("Size must be less than 100 kb");
        }

        string filePath = await CourseFullDetailsViewModel.ImagePath.CopyFileAsync(_env.WebRootPath, "assets", "img", "course");

        var category = await _context.Categoriess.FindAsync(CategoryId);
        if (category is null)
        {
            throw new ArgumentNullException("Invalid category selected!");
        }
        var cata = await _context.Categoriess.ToListAsync();

        Courses courses = new Courses
        {
            ImagePath = filePath,
            Name = CourseFullDetailsViewModel.Cours,
            Descripton = CourseFullDetailsViewModel.Description,
            CategoriesId = CategoryId,
            CoursesDetails = new CoursesDetails
            {
                Starts = CourseFullDetailsViewModel.Starts,
                Month = CourseFullDetailsViewModel.Month,
                Hours = CourseFullDetailsViewModel.Hours,
                Level = CourseFullDetailsViewModel.Level,
                Language = CourseFullDetailsViewModel.Language,
                Students = CourseFullDetailsViewModel.Students,
                Assesments = CourseFullDetailsViewModel.Assesments,
                CourseFee = CourseFullDetailsViewModel.CourseFee
            }
        };

        await _context.AddAsync(courses);
        /*_context.Entry(courses).State = EntityState.Added;*/
        await _context.SaveChangesAsync();
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
