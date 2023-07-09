using EduHome.Core.Entities;
using EduHome.UI.Areas.Admin.Data.Exception;
using EduHome.UI.Areas.Admin.Data.Services.Interfaces;
using EduHome.UI.Areas.Admin.Extension;
using EduHome.UI.Areas.Admin.ViewModel;
using EduHomeDataAccess.Database;
using EduHomeDataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EduHome.UI.Areas.Admin.Data.Services.Concrets;

public class TeacherService : ITeacherService
{
    private readonly AppDbContext _context;
    private readonly IWebHostEnvironment _environment;
    private readonly IEntityBaseRepository<Teacher> _entityBaseRepository;
    public TeacherService(AppDbContext context, IWebHostEnvironment environment, IEntityBaseRepository<Teacher> entityBaseRepository)
    {
        _context = context;
        _environment = environment;
        _entityBaseRepository = entityBaseRepository;
    }

    public async Task CreateAsync(TeacherViewModel teacherViewModel)
    {
        //if (!teacherViewModel.Image.FormatFile("Image"))
        //{
        //    throw new ArgumentNullException("Select correct image format!");
        //}
        if (!teacherViewModel.Image.FormatLength(1000))
        {
            throw new ArgumentNullException("Size must be less than 1000 kb");
        }
        string filePath = await teacherViewModel.Image.CopyFileAsync(_environment.WebRootPath, "assets", "img", "course");

        Teacher teacher = new Teacher
        {
            Name = teacherViewModel.Name,
            ImagePath = filePath,
            Posistion = teacherViewModel.Posistion,
            teacherDetails = new TeacherDetails
            {
                Degree = teacherViewModel.Degree,
                Experince = teacherViewModel.Experince,
                Hobbies = teacherViewModel.Hobbies,
                Facultry = teacherViewModel.Facultry,
                Email = teacherViewModel.Email,
                Phone = teacherViewModel.Phone,
                Skaype = teacherViewModel.Skype,
                LanguageDegree = teacherViewModel.Language,
                TeamLeaderDegree = teacherViewModel.TeamLeader,
                DevelopmentDegree = teacherViewModel.Development,
                DesignDegree = teacherViewModel.Design,
                InnovationDegree = teacherViewModel.Innovation,
                CommunicationDegree = teacherViewModel.Communication,
                Description = teacherViewModel.AboutMe
            }
        };
        await _entityBaseRepository.AddAsync(teacher);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        if (id == 0) throw new NullReferenceException("Teacher is Null");
        var DeletedTeacehr = await _context.Teachers.FindAsync(id);
        if (DeletedTeacehr is null) throw new NotFoundException("Teacher is Null");
        await _entityBaseRepository.DeleteAsync(id);
        await _context.SaveChangesAsync();
    }

    public async Task EditAsync(int id, TeacherViewModel teacherViewModel)
    {
        Teacher? teacher = await _context.Teachers.Include(td => td.teacherDetails).FirstOrDefaultAsync(e => e.Id == id);
        if (teacher is null) throw new NotFoundException("Teacher is Null");
        if (teacherViewModel.Image is not null)
        {
            if (!teacherViewModel.Image.FormatFile("image"))
            {
                throw new ArgumentNullException("Select correct image format!");
            }
            if (!teacherViewModel.Image.FormatLength(1000))
            {
                throw new ArgumentNullException("Size must be less than 1000 kb");
            }
            string filePath = await teacherViewModel.Image.CopyFileAsync(_environment.WebRootPath, "assets", "img", "course");
            teacher.ImagePath= filePath;
        }

        teacher.Name = teacherViewModel.Name;
        teacher.Posistion = teacherViewModel.Posistion;
        teacher.teacherDetails.Degree = teacherViewModel.Degree;
        teacher.teacherDetails.Experince = teacherViewModel.Experince;
        teacher.teacherDetails.Hobbies = teacherViewModel.Hobbies;
        teacher.teacherDetails.Facultry = teacherViewModel.Facultry;
        teacher.teacherDetails.Email = teacherViewModel.Email;
        teacher.teacherDetails.Phone = teacherViewModel.Phone;
        teacher.teacherDetails.Skaype = teacherViewModel.Skype;
        teacher.teacherDetails.LanguageDegree = teacherViewModel.Language;
        teacher.teacherDetails.TeamLeaderDegree = teacherViewModel.TeamLeader;
        teacher.teacherDetails.DevelopmentDegree = teacherViewModel.Development;
        teacher.teacherDetails.DesignDegree = teacherViewModel.Design;
        teacher.teacherDetails.InnovationDegree = teacherViewModel.Innovation;
        teacher.teacherDetails.CommunicationDegree = teacherViewModel.Communication;
        teacher.teacherDetails.Description = teacherViewModel.AboutMe;

        await _entityBaseRepository.UpdateAsync(id,teacher);
        await _context.SaveChangesAsync();
    }

    public async Task<Teacher> FindByTeacherAsync(int id)
    {
        if (id == 0) throw new NullReferenceException("Teacher is Null");
        var teacher = await _context.Teachers.Include(td => td.teacherDetails).FirstOrDefaultAsync(a => a.Id == id);
        if (teacher is null) throw new NotFoundException("Teacher is Null");
        return teacher;
    }

    public async Task<IEnumerable<Teacher>> GetTeacher() => await _context.Teachers.Include(e => e.teacherDetails).ToListAsync();
}
