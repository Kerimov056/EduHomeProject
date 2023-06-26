using EduHome.UI.Areas.Admin.Extension;
using EduHome.UI.Areas.Admin.ViewModel;
using EduHomeDataAccess.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EduHome.UI.Areas.Admin.Controllers;
[Area("Admin")]
public class CoursesController : Controller
{
    private readonly AppDbContext _context;
    public CoursesController(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        return View(await _context.Coursess.ToListAsync());
    }

    public IActionResult Create()
    {
        return View();
    }

    //[HttpPost]
    //[ValidateAntiForgeryToken]
    //public async Task<IActionResult> Create(CourseFullDetailsViewModel courseFullDetailsViewModel)
    //{
    //    if (!ModelState.IsValid)
    //    {
    //        return View(courseFullDetailsViewModel);
    //    }
    //    if (!courseFullDetailsViewModel.ImagePath.FormatFile("image"))
    //    {
    //        ModelState.AddModelError("ImagePath","Select correct image format!");
    //        return View(courseFullDetailsViewModel);
    //    }
    //    if (!courseFullDetailsViewModel.ImagePath.FormatLength(100))
    //    {
    //        ModelState.AddModelError("ImagePath", "Size must be less than 100 kb");
    //        return View(courseFullDetailsViewModel);
    //    }


    //    return RedirectToAction("Index");
    //}
}
