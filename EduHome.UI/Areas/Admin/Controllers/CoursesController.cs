using AutoMapper;
using EduHome.Core.Entities;
using EduHome.UI.Areas.Admin.Extension;
using EduHome.UI.Areas.Admin.ViewModel;
using EduHome.UI.ViewModel;
using EduHomeDataAccess.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EduHome.UI.Areas.Admin.Controllers;
[Area("Admin")]
public class CoursesController : Controller
{
    private readonly AppDbContext _context;
    private readonly IWebHostEnvironment _env;
    private readonly IMapper _mapper;
    public CoursesController(AppDbContext context, IWebHostEnvironment env, IMapper mapper)
    {
        _context = context;
        _env = env;
        _mapper = mapper;
    }

    public async Task<IActionResult> Index()
    {
        HomeViewModel homeViewModel = new()
        {
            blogs = await _context.Blogs.ToListAsync(),
            courses = await _context.Coursess.Include(c => c.CoursesDetails).ToListAsync()
        };
        return View(homeViewModel);
    }
    public async Task<IActionResult> Details(int id)
    {
        if (id==0 || id==null)
        {
            return NotFound();
        }
        var cours = _context.Coursess.Find(id);
        if (cours is null)
        {
            return NotFound();
        }
        ViewBag.coursId = cours.Id;
        HomeViewModel homeViewModel = new()
        {
            blogs = await _context.Blogs.ToListAsync(),
            courses = await _context.Coursess.Include(c => c.CoursesDetails).ToListAsync()
        };
        return View(homeViewModel);
    }

    //----------------------------------------------------------------------------------


    public async Task<IActionResult> Create()
    {
        ViewBag.catagory = await _context.Categoriess.ToListAsync();
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CourseFullDetailsViewModel courseFullDetailsViewModel)
    {
        if (!ModelState.IsValid)
        {
            return View(courseFullDetailsViewModel);
        }
        if (!courseFullDetailsViewModel.ImagePath.FormatFile("image"))
        {
            ModelState.AddModelError("ImagePath", "Select correct image format!");
            return View(courseFullDetailsViewModel);
        }
        if (!courseFullDetailsViewModel.ImagePath.FormatLength(100))
        {
            ModelState.AddModelError("ImagePath", "Size must be less than 100 kb");
            return View(courseFullDetailsViewModel);
        }

        string filePath = await courseFullDetailsViewModel.ImagePath.CopyFileAsync(_env.WebRootPath, "assets", "img", "course");
        Courses courses = _mapper.Map<Courses>(courseFullDetailsViewModel);
        courses.ImagePath = filePath;

        _context.Coursess.Add(courses);
        _context.SaveChanges();
        return RedirectToAction("Index");
    }
}
