using EduHome.UI.ViewModel;
using EduHomeDataAccess.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EduHome.UI.Contollers
{
    public class AboutController : Controller
    {
        private readonly AppDbContext _context;
        public AboutController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            HomeViewModel model = new HomeViewModel
            {
                abouts = await _context.Abouts.ToListAsync(),
                teachers = await _context.Teachers.ToListAsync(),
                notice = await _context.Notices.ToListAsync()
            };
            return View(model);
        }
    }
}
