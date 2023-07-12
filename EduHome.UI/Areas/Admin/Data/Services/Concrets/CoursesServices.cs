using EduHome.Core.Entities;
using EduHome.UI.Areas.Admin.Data.Exception;
using EduHome.UI.Areas.Admin.Data.Services.Interfaces;
using EduHome.UI.Areas.Admin.Extension;
using EduHome.UI.Areas.Admin.ViewModel;
using EduHomeDataAccess.Database;
using EduHomeDataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EduHome.UI.Areas.Admin.Data.Services.Concrets;

public class CoursesServices : ICoursesServices
{
    private readonly AppDbContext _context;
    private readonly IWebHostEnvironment _env;
    private readonly IEntityBaseRepository<Courses> _entityBaseRepository;
    private readonly ICoursRepository<Courses> _coursRepository;
    public CoursesServices(
        AppDbContext context, 
        IWebHostEnvironment env,
        ICoursRepository<Courses> coursRepository)
    {
        _context = context; 
        _env = env;
        _coursRepository = coursRepository;
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
    public async Task CreateAsync(CourseFullDetailsViewModel CourseFullDetailsViewModel, int CategoryId)
    {
        if (!CourseFullDetailsViewModel.ImagePath.FormatFile("image"))
        {
            throw new ArgumentException("Select correct image format!");
        }
        if (!CourseFullDetailsViewModel.ImagePath.FormatLength(1000))
        {
            throw new ArgumentException("Size must be less than 1000 kb");
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

        await _coursRepository.AddAsync(courses);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        if (id == 0) throw new NullReferenceException("Course is Null");
        var course = await _context.Coursess.FindAsync(id);
        if (course is null) throw new NotFoundException("Course is Null");

        await _coursRepository.DeleteAsync(id);
        await _context.SaveChangesAsync();
    }

    public async Task<Courses> FindByIdAsync(int id)
    {
        var course = await _context.Coursess.Include(cd=>cd.CoursesDetails).FirstOrDefaultAsync(c=>c.Id==id);
        if (course is null)  throw new NotFoundException("Course not found");
        return course;
    }

    public async Task<CourseFullDetailsViewModel> GetEdit(int id, CourseFullDetailsViewModel courseFullDetailsViewModel)
    {
        Courses? course = await _context.Coursess.FindAsync(id);
        if (course is null) throw new NotFoundException("Course is Null");

        if (courseFullDetailsViewModel.ImagePath is not null)
        {
            if (!courseFullDetailsViewModel.ImagePath.FormatFile("image"))
            {
                throw new ArgumentException("Select correct image format!");
            }

            if (!courseFullDetailsViewModel.ImagePath.FormatLength(1000))
            {
                throw new ArgumentException("Size must be less than 1000 kb");
            }

            string filePath = await courseFullDetailsViewModel.ImagePath.CopyFileAsync(_env.WebRootPath, "assets", "img", "course");
            course.ImagePath = filePath;
        }

        courseFullDetailsViewModel.CategorId = course.CategoriesId;
        courseFullDetailsViewModel.Description = course.Descripton;
        courseFullDetailsViewModel.Cours = course.Name;
        courseFullDetailsViewModel.CategorId = course.CategoriesId;
        courseFullDetailsViewModel.Starts = course.CoursesDetails.Starts;
        courseFullDetailsViewModel.Month = course.CoursesDetails.Month;
        courseFullDetailsViewModel.Hours = course.CoursesDetails.Hours;
        courseFullDetailsViewModel.Level = course.CoursesDetails.Level;
        courseFullDetailsViewModel.Language = course.CoursesDetails.Language;
        courseFullDetailsViewModel.Students = course.CoursesDetails.Students;
        courseFullDetailsViewModel.Assesments = course.CoursesDetails.Assesments;
        courseFullDetailsViewModel.CourseFee = course.CoursesDetails.CourseFee;

        return courseFullDetailsViewModel;
    }

    public async Task UpdateAsync(int id, CourseFullDetailsViewModel viewModel, int CategoryId)
    {
        Courses? course = await _context.Coursess.Include(c => c.CoursesDetails).FirstOrDefaultAsync(c => c.Id == id);

        if (course is null)  throw new NullReferenceException("Course is null");

        Categories? category = await _context.Categoriess.FindAsync(CategoryId);

        if (category is null)  throw new ArgumentException("Invalid Category");

        if (viewModel.ImagePath is not null)
        {
            if (!viewModel.ImagePath.FormatFile("image"))
            {
                throw new ArgumentException("Select correct image format!");
            }

            if (!viewModel.ImagePath.FormatLength(1000))
            {
                throw new ArgumentException("Size must be less than 1000 kb");
            }

            string filePath = await viewModel.ImagePath.CopyFileAsync(_env.WebRootPath, "assets", "img", "course");
            course.ImagePath = filePath;
        }

        course.Descripton = viewModel.Description;
        course.Name = viewModel.Cours;
        course.CategoriesId = category.Id;
        course.CoursesDetails.Starts = viewModel.Starts;
        course.CoursesDetails.Month = viewModel.Month;
        course.CoursesDetails.Hours = viewModel.Hours;
        course.CoursesDetails.Level = viewModel.Level;
        course.CoursesDetails.Language = viewModel.Language;
        course.CoursesDetails.Students = viewModel.Students;
        course.CoursesDetails.Assesments = viewModel.Assesments;
        course.CoursesDetails.CourseFee = viewModel.CourseFee;

        _context.Entry(course).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

}
