using EduHome.UI.ViewModel;
using EduHomeDataAccess.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EduHome.UI.Areas.Admin.Controllers;
[Area("Admin")]
public class TeacheraController : Controller
{
    private readonly AppDbContext _context;
    public TeacheraController(AppDbContext context)
    {
        _context = context;
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


}
