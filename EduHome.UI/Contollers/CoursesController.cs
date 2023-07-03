using EduHome.UI.ViewModel;
using EduHomeDataAccess.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EduHome.UI.Contollers;

public class CoursesController : Controller
{
    private readonly AppDbContext _context;
    public CoursesController(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        HomeViewModel model = new HomeViewModel
        {
            courses = await _context.Coursess.ToListAsync(),
            blogs = await _context.Blogs.ToListAsync() 
        };
        return View(model);
    }
}
