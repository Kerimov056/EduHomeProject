using EduHome.UI.ViewModel;
using EduHomeDataAccess.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EduHome.UI.Contollers;

public class EventController : Controller
{
    private readonly AppDbContext _context;
    public EventController(AppDbContext context)
    {
        _context= context;
    }
    public async Task<IActionResult> Index()
    {
        var events = await _context.Eventss.Include(ed=>ed.Details).ToListAsync();
        return View(events);
    }
}
