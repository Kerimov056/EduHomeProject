using EduHome.Core.Entities;
using EduHome.UI.ViewModel;
using EduHomeDataAccess.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EduHome.UI.Controllers;

public class CourseDetailsController : Controller
{
    private readonly AppDbContext _context;

    public CourseDetailsController(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index(int id)
    {
        if (id == 0 || id == null)
        {
            return NotFound();
        }
        var cart = _context.Coursess.Find(id);
        if (cart == null)
        {
            return NotFound();
        }
        ViewBag.Id = cart.Id;
        HomeViewModel homeViewModel = new()
        {
            blogs = await _context.Blogs.ToListAsync(),
            courses = await _context.Coursess.Include(c => c.CoursesDetails).ToListAsync()
        };
        return View(homeViewModel);
    }
}




