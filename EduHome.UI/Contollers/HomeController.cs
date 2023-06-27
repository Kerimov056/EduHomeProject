using EduHome.UI.ViewModel;
using EduHomeDataAccess.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace EduHome.UI.Contollers;

public class HomeController : Controller
{
    private readonly AppDbContext _context;

    public HomeController(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        HomeViewModel homeViewModel = new()
        {
            blogs = await _context.Blogs.ToListAsync(),
            sliders = await _context.Sliders.ToListAsync(),
            notice = await _context .Notices.ToListAsync(),
            info = await _context .Infos.ToListAsync(),
            courses = await _context.Coursess.ToListAsync()
            // = await _context.Upcommings.ToListAsync(),
            //upcommingCategories = await _context.upcommingCategories.ToListAsync()
        };
        
        return View(homeViewModel);
    }

}
