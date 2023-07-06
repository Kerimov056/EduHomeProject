using EduHome.Core.Entities;
using EduHome.UI.Areas.Admin.Extension;
using EduHome.UI.Areas.Admin.ViewModel;
using EduHome.UI.ViewModel;
using EduHomeDataAccess.Database;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EduHome.UI.Areas.Admin.Controllers;
[Area("Admin")]
[Authorize(Roles = "Admin")]

public class TeacheraController : Controller
{
    private readonly AppDbContext _context;
    private readonly IWebHostEnvironment _environment;
    public TeacheraController(AppDbContext context, IWebHostEnvironment environment)
    {
        _context = context;
        _environment = environment;
    }

    public async Task<IActionResult> Index()
    {
        int sum = 0;
        var teacher = await _context.Teachers.ToListAsync();
        foreach (var item in teacher)
        {
            sum++;
        }

        TempData["TeacherSum"] = sum;
        HomeViewModel model = new HomeViewModel
        {
            teachers = await _context.Teachers.ToListAsync(),
        };
        return View(model);
    }

    public async Task<IActionResult> Details(int id)
    {
        if (!ModelState.IsValid) return NotFound();
        Teacher teacher = await _context.Teachers.FindAsync(id);
        if (teacher == null) return NotFound();
        ViewBag.TeacherId = teacher.Id;

        HomeViewModel model = new HomeViewModel
        {
            teachers = await _context.Teachers.Include(e=>e.teacherDetails).ToListAsync(),
        };
        return View(model);
    }

    public async Task<IActionResult> Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]

    public async Task<IActionResult> Create(TeacherViewModel teacherViewModel)
    {
        if (!ModelState.IsValid)
        {
            return View(teacherViewModel);
        }
        if (!teacherViewModel.Image.FormatFile("image"))
        {
            ModelState.AddModelError("ImagePath", "Select correct image format!");
            return View(teacherViewModel);
        }
        if (!teacherViewModel.Image.FormatLength(1000))
        {
            ModelState.AddModelError("ImagePath", "Size must be less than 100 kb");
            return View(teacherViewModel);
        }

        string filePath = await teacherViewModel.Image.CopyFileAsync(_environment.WebRootPath, "assets", "img", "course");
        Teacher teacher = new Teacher
        {
            Name = teacherViewModel.Name,
            ImagePath= filePath,
            Posistion = teacherViewModel.Posistion,
            teacherDetails = new TeacherDetails
            {
                Degree = teacherViewModel.Degree,
                Experince = teacherViewModel.Experince,
                Hobbies = teacherViewModel.Hobbies,
                Facultry = teacherViewModel.Facultry,
                Email= teacherViewModel.Email,
                Phone = teacherViewModel.Phone,
                Skaype = teacherViewModel.Skype,
                LanguageDegree = teacherViewModel.Language,
                TeamLeaderDegree = teacherViewModel.TeamLeader,
                DevelopmentDegree= teacherViewModel.Development,
                DesignDegree = teacherViewModel.Design,
                InnovationDegree= teacherViewModel.Innovation,
                CommunicationDegree= teacherViewModel.Communication,
                Description = teacherViewModel.AboutMe
            }
        };
        await _context.AddAsync(teacher);
        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int id)
    {
        if (id==0)
        {
            return NotFound();
        }
        var teacher = await _context.Teachers.Include(e=>e.teacherDetails).FirstOrDefaultAsync(n => n.Id == id);
        if (teacher is null)
        {
            return NotFound();
        }

        TeacherViewModel teacherView = new TeacherViewModel
        {
            //Image = teacher.ImagePath
            Name = teacher.Name,
            Posistion = teacher.Posistion,
            Degree = teacher.teacherDetails.Degree,
            Experince = teacher.teacherDetails.Experince,
            Hobbies = teacher.teacherDetails.Hobbies,
            Facultry = teacher.teacherDetails.Facultry,
            Email = teacher.teacherDetails.Email,
            Phone = teacher.teacherDetails.Phone,
            Skype = teacher.teacherDetails.Skaype,
            Language = teacher.teacherDetails.LanguageDegree,
            Development = teacher.teacherDetails.DevelopmentDegree,
            Design = teacher.teacherDetails.DesignDegree,
            Innovation = teacher.teacherDetails.InnovationDegree,
            Communication = teacher.teacherDetails.CommunicationDegree,
            AboutMe = teacher.teacherDetails.Description
        };
        return View(teacherView);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]

    public async Task<IActionResult> Edit(int id,TeacherViewModel teacherViewModel)
    {
        if (!ModelState.IsValid)
        {
            return View(teacherViewModel);
        }
        if (!teacherViewModel.Image.FormatFile("image"))
        {
            ModelState.AddModelError("ImagePath", "Select correct image format!");
            return View(teacherViewModel);
        }
        if (!teacherViewModel.Image.FormatLength(100))
        {
            ModelState.AddModelError("ImagePath", "Size must be less than 100 kb");
            return View(teacherViewModel);
        }

        string filePath = await teacherViewModel.Image.CopyFileAsync(_environment.WebRootPath, "assets", "img", "course");

        Teacher? teacher = await _context.Teachers.Include(td => td.teacherDetails).FirstOrDefaultAsync(e =>e.Id == id);
        if (teacher is null)
        {
            return NotFound();
        }

        teacher.ImagePath = filePath;
        teacher.Name = teacherViewModel.Name;
        teacher.Posistion = teacherViewModel.Posistion;
        teacher.teacherDetails.Degree= teacherViewModel.Degree;
        teacher.teacherDetails.Experince= teacherViewModel.Experince;
        teacher.teacherDetails.Hobbies= teacherViewModel.Hobbies;
        teacher.teacherDetails.Facultry= teacherViewModel.Facultry;
        teacher.teacherDetails.Email = teacherViewModel.Email;
        teacher.teacherDetails.Phone= teacherViewModel.Phone;
        teacher.teacherDetails.Skaype = teacherViewModel.Skype;
        teacher.teacherDetails.LanguageDegree = teacherViewModel.Language;
        teacher.teacherDetails.TeamLeaderDegree = teacherViewModel.TeamLeader;
        teacher.teacherDetails.DevelopmentDegree = teacherViewModel.Development;
        teacher.teacherDetails.DesignDegree= teacherViewModel.Design;
        teacher.teacherDetails.InnovationDegree= teacherViewModel.Innovation;
        teacher.teacherDetails.CommunicationDegree= teacherViewModel.Communication;
        teacher.teacherDetails.Description = teacherViewModel.AboutMe;

        _context.Entry(teacher).State= EntityState.Modified;
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }


    public async Task<IActionResult> Delete(int id )
    {
        if (id == 0)
        {
            return NotFound();
        }

        Teacher? teacher = await _context.Teachers.Include(td => td.teacherDetails).FirstOrDefaultAsync(i=>i.Id==id);
        if (teacher is null)
        {
            return BadRequest();
        }
        ViewBag.TeId = teacher.Id;

        HomeViewModel model = new HomeViewModel
        {
            teachers = await _context.Teachers.Include(td => td.teacherDetails).ToListAsync()
        };
        return View(model);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeletePost(int id)
    {
        if (id == 0)
        {
            return NotFound();
        }
        var teacher = await _context.Teachers.FindAsync(id);
        if (teacher == null)
        {
            return BadRequest();
        }

        //_context.Entry(teacher).State = EntityState.Detached;
        _context.Teachers.Remove(teacher);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
}
