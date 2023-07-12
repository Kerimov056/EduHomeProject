using EduHome.UI.ViewModel;
using EduHomeDataAccess.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EduHome.UI.Contollersş
{
    public class TestController : Controller
    {
        private readonly AppDbContext _context;
        public TestController(AppDbContext context)
        {
            _context= context;
        }
        public async Task<IActionResult> Index()
        {
            HomeViewModel homeViewModel = new()
            {
                blogs = await _context.Blogs.ToListAsync(),
                courses = await _context.Coursess
                                 .Include(c => c.CoursesDetails)
                                 .Include(c => c.CourseComments)
                                 .ToListAsync(),
                categories = await _context.Categoriess.ToListAsync(),
            };
            return View(homeViewModel);
        }
    }
}
