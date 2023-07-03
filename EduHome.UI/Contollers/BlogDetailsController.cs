using EduHome.Core.Entities;
using EduHome.UI.ViewModel;
using EduHomeDataAccess.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EduHome.UI.Contollers;

public class BlogDetailsController : Controller
{
    private readonly AppDbContext _context;
    public BlogDetailsController(AppDbContext context)
    {
        _context = context;
    }
    public async Task<IActionResult> Index(int id )
    {
        if (id == 0)
        {
            return NotFound();
        }
        var blog = await _context.Blogs.FindAsync(id);
        if (blog == null)
        {
            return NotFound();
        }
        ViewBag.BlogId = blog.Id;

        HomeViewModel homeViewModel = new()
        {
            blogs = await _context.Blogs.ToListAsync(),
        };
        return View(homeViewModel);
    }
}
