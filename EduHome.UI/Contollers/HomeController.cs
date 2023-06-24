using EduHomeDataAccess.Database;
using Microsoft.AspNetCore.Mvc;

namespace EduHome.UI.Contollers;

public class HomeController : Controller
{
    private readonly AppDbContext _context;

    public HomeController(AppDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        return View();
    }
}
