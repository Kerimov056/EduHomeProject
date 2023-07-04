using EduHome.UI.ViewModel;
using EduHomeDataAccess.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EduHome.UI.Contollersş
{
    public class TeachersController : Controller
    {
        private readonly AppDbContext _context;
        public TeachersController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            HomeViewModel model = new HomeViewModel
            {
                teachers = await _context.Teachers.ToListAsync()
            };
            return View(model);
        }
    }
}
