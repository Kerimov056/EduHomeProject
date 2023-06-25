using EduHomeDataAccess.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EduHome.UI.Areas.Admin.Controllers;
[Area("Admin")]
public class InfoController : Controller
{
    private readonly AppDbContext _context;
    public InfoController(AppDbContext context)
    {
        _context= context;
    }
    public async Task<IActionResult> Index()
    {
        return View(await _context.Infos.ToListAsync());
    }
}
