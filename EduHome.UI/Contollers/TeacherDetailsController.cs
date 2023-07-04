using EduHome.Core.Entities;
using EduHome.UI.ViewModel;
using EduHomeDataAccess.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EduHome.UI.Contollers;

public class TeacherDetailsController : Controller
{
    private readonly AppDbContext _context;
    public TeacherDetailsController(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index(int id)
    {
        if (id == 0) BadRequest();
        var teacher = await _context.Teachers.FindAsync(id);
        if (teacher is null) return NotFound();
        ViewBag.TeacherId = teacher.Id;
        //var teacherDetails = await _context.TeacherDetails.FindAsync(teacher.Id);
        //if (teacherDetails is null) return NotFound();

        HomeViewModel model = new HomeViewModel
        {
            teachers = await _context.Teachers.Include(td=>td.teacherDetails).ToListAsync(),
        };
        return View(model);
    }
}
