using EduHome.Core.Entities;
using EduHome.UI.ShopServices.Interfaces;
using EduHome.UI.ViewModel;
using EduHomeDataAccess.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EduHome.UI.Contollers;

public class CoursesController : Controller
{
    private readonly AppDbContext _context;
    private readonly ILogger<CoursesController> _logger;
    private readonly ISearchServices _searchServices;
    public CoursesController(
        AppDbContext context,
        ILogger<CoursesController> logger,
        ISearchServices searchServices)
    {
        _context = context;
        _logger = logger;
        _searchServices = searchServices;
    }

    public async Task<IActionResult> Index(string sTrem = "", int catagoryId = 0)
    {
        IEnumerable<Courses> cours = await _searchServices.GetCourses(sTrem, catagoryId);
        IEnumerable<Categories> categories = await _searchServices.Categories(catagoryId);


        HomeViewModel model = new HomeViewModel
        {
            courses = cours,
            categories = categories,
            blogs = await _context.Blogs.ToListAsync(),
            sTrem = sTrem,
            catagoryId= catagoryId
        };
        return View(model);
    }
}
