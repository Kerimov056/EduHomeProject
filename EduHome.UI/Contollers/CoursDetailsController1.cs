using EduHomeDataAccess.Database;
using Microsoft.AspNetCore.Mvc;

namespace EduHome.UI.Contollers;

public class CoursDetailsController1 : Controller
{
    private readonly AppDbContext _context;

    public CoursDetailsController1(AppDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        return View();
    }
}
